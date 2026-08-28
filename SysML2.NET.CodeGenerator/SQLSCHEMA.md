# SQLSCHEMA.md — PostgreSQL persistence schema for SysML v2 models

This document is the compact design reference for the SQL schema code-generation pipeline.
For the long-form architectural guide — the full reasoning chain behind every table, function,
and index, with worked examples and the performance war stories — see
`SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md` (Dutch translation:
`SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.nl.md`). Guide section map: §1–§5 problem, census
and rejected alternatives; §6–§13 layer-by-layer walkthrough (§6.4: multi-version support —
commit-stamped metamodel releases, the append-only class-kind registry, and conversion
commits; §9.4: how all three Derived Property Conformance levels — none / passthrough /
full — map onto the same schema as write-path policies); §14 performance audit; §15
service-layer obligations; §16 worked
examples (§16.5: the snapshot-paging recipe — commit-anchored keyset pages and the
function-inlining guard); §17 code generation; **§18 multi-user and concurrency — including the NORMATIVE
compare-and-swap commit protocol (§18.2) that every service implementation must follow**;
§19 glossary.

The pipeline artifacts:

| Artifact | Role |
|---|---|
| `SysML2.NET.CodeGenerator/Sql/schema.golden.sql` | Hand-written, annotated reference design. Carries the rationale comments. |
| `SysML2.NET.CodeGenerator/Sql/schema2.generated.sql` | The actual generator output, checked in for review. Supersedes the golden's `[GENERATED]` excerpts. |
| `SysML2.NET.CodeGenerator/Sql/schema.smoke.sql` | Functional test. 59 assertions; raises on any wrong answer. Runs against golden AND generated schema. |
| `SysML2.NET.CodeGenerator/Sql/schema.concurrency.{setup,hot,spread,read,verify}.sql` | Multi-user suite: pgbench scenarios racing the §18.2 CAS protocol (hot branch / spread / reads-under-write-storm) + invariant verifier C1–C5 (linear chains, losers write nothing, overlay coherence). |
| `SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs` | Handlebars template: hand-written sections verbatim, `[GENERATED]` sections via helpers. |
| `SysML2.NET.CodeGenerator/HandleBarHelpers/SqlSchemaHelpers.cs` | The eight `uml_template.SQL2.*` helpers. |
| `SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs` | Naming, type mapping, and the stored-property census logic. |
| `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/SQLSchemaGenerator.cs` | The generator (emits `schema2.sql`). |
| `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/ClassKindRegistry.cs` | The checked-in APPEND-ONLY registry freezing `class_kind` ids and `model_version` ordinals across metamodel releases; the generator validates the UML model against it and fails on drift. |
| `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/ClassKindEnumGenerator.cs` | Emits the generated `ClassKind` C# enum (`SysML2.NET/Core/AutoGenEnum/ClassKind.cs`: `enum ClassKind : short`, all 175 registry members with explicit frozen values) from `ClassKindRegistry.cs` via `Templates/Uml/core-classkind-enum-template.hbs`; shares the registry-drift fail-fast with the SQL helpers. |
| `SysML2.NET.CodeGenerator/IMPACT-RADIUS.md` | Design sketch for the derived-property impact-radius engine (obligation §15.1): propagation kinds, `derived_dependency` catalog, early cutoff, differential-testing oracle. |

The old `SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema.hbs` and the fully commented-out
COMET-style helpers it referenced are superseded by this pipeline and kept only for comparison.

## Why this shape

The metamodel realised in this repo has 175 metaclasses (167 concrete) and 12,963 flattened
properties — but only ~2,700 of them are stored (`{ get; set; }` in `SysML2.NET/Core/AutoGenDto/`);
the other ~9,600 (77%) are derived. The stored surface collapses to under 100 property
declarations across 49 metaclasses, with the widest metaclass at 24 stored columns. Two facts
force everything else:

1. **Derived values are functions of (version, snapshot), not of the version alone.** Renaming a
   Namespace changes every descendant's `qualifiedName` without any descendant's stored state
   changing. The OMG API spec (Systems Modeling API and Services v1.0, Clause 2) warns of exactly
   this: derived properties of an element may be affected by commits that do not directly change
   that element.
2. **References name identities, not versions.** A SysML2 reference targets a stable element
   `@id`. Under commits/branches, referential integrity can only be enforced against the identity
   (`data_identity` — TYPED: it carries the element's immutable `class_kind`, so a version can
   never contradict its identity's metaclass); "does the target exist at commit C" is a
   validation query, not an FK — generated as `validate_references_at_commit()` (guide §7).

## Architecture (four layers)

1. **PIM / versioning** (hand-written): `project`, `commit`, `commit_parent` (the DAG — a commit
   has a SET of parents; merges are real), `branch`, `tag`, `project_usage`, `query` (the spec's
   stored Query records, definitions kept as the spec's own JSON shape), `data_identity`.
   A trigger enforces the spec's monotonic-commit-timestamp invariant because the snapshot
   resolver depends on it ("newest ancestor wins"); `trg_commit_immutable` rejects every
   UPDATE of a commit row (Clause 7.1.2: commits are immutable) — with `created` frozen, the
   monotonicity check doubles as the commit DAG's acyclicity guarantee. **Multi-version
   support** lives here too:
   every commit carries `model_version_id` (the metamodel release its payloads are written in;
   guide §6.4), `project.target_model_version_id` is the upgrade policy, and a second trigger
   (`trg_commit_parent_version`) forbids downgrades and mixed-release merges — a branch
   upgrades via a single-parent CONVERSION COMMIT.
2. **Stored element state** (append-only): `element_version` (core; Element's own six stored
   properties folded in; `tombstone` = `DataVersion.payload = null`; `stored_json` = the
   pre-serialized stored half of the payload) + 47 generated subtype tables (one per
   storage-introducing metaclass, keyed `(project_id, version_id)`) + 7 generated ordered link
   tables for the multi-valued stored properties.
3. **Derived element state** (append-only, the second stream): `derived_version` keyed by
   `(identity, commit)` — a row is written only for elements whose derived values actually
   changed at a commit (the change set's impact radius). Six hot derived properties are promoted
   to real columns (`owner`, `owning_namespace`, `qualified_name`, `name`, `short_name`,
   `is_library_element`) so the Query service can filter/ORDER BY them; the remaining ~325
   distinct derived names live in `derived_json` behind a GIN index.
4. **Snapshot resolution + read path**: materialized `branch_head` (updated incrementally,
   O(changeset)), `commit_checkpoint` for historical commits, and `resolve_commit_state()` as the
   general recursive-CTE fold. `GET .../elements/{id}` is `stored_json || derived_json` — one
   jsonb concat, no joins, no recursion, no derived computation at read time.

Derived-property conformance level targeted: **Full Conformance, precomputed at commit time**
(the existing 366 implemented `SysML2.NET/Extend/*.Compute*` methods are the computation engine;
the impact-radius analysis lives in .NET, not SQL). The schema itself is conformance-agnostic —
the level is a write-path policy, and passthrough / no-conformance are supported by the same
DDL (guide §9.4).

## What is generated vs hand-written

Generated from the UML XMI (`Resources/KerML_only_xmi.uml` + `Resources/SysML_only_xmi.uml`):

- the 7 enum types (labels = lowercase C# literals, matching the JSON wire format);
- the `model_version` + `class_kind` registry seeds — emitted from the checked-in append-only
  `ClassKindRegistry.cs`, NOT computed from the model: **ids are frozen forever once assigned**
  (a new release appends after the highest id; dropped classes close with `removed_in`), the
  generator fails on any model↔registry drift, and the seeds are idempotent
  (`ON CONFLICT (id) DO NOTHING` — safe to re-apply; the old fresh-installs-only trap is gone;
  see guide §12.1/§15.14). The former `class_kind_table` and `property_catalog` tables are
  REMOVED: nothing in the schema read them, and their content becomes per-release generated C#
  (model-version descriptors, guide §12.2);
- the 7 link tables and 47 subtype tables, with NULL-ability from the property's lower bound,
  `DEFAULT` clauses from the XMI-declared default values, FKs to `data_identity`, and a
  reverse-lookup index on every reference column;
- 167 per-metaclass flattening views (`vw_part_usage`, …) reconstructing the DTO row shape;
- the TWO-TIER reference validation, one `UNION ALL` arm per stored reference column (42),
  reporting `'wrong-type'` (via the TYPED identity: `data_identity.class_kind` plus the
  composite FK from `element_version`, so a version can never contradict its identity's
  metaclass) and `'dangling'` (same-project target not alive in the snapshot):
  `validate_references_at_commit` (the full periodic audit, snapshot in an ANALYZE'd temp
  table — O(snapshot × log history)) and `validate_references_in_commit` (the incremental
  per-commit check — outgoing change-set references plus the reverse direction tombstones
  break; O(change set)). Deliberately functions, not constraints — the spec allows
  transiently dangling references, and liveness is per-commit (guide §7);
- the partition list and the model-version function.

Everything else (PIM, `element_version`, `derived_version`, snapshot functions) is hand-written
and lives verbatim in the template.

## Census rules (the part that was non-obvious)

- **Declared properties must NOT be read from `IClass.OwnedAttribute`.** Reference properties
  that are association ends (e.g. `Membership::memberElement`) are owned by the association, not
  the class. The generator computes declared = `QueryAllProperties(class)` minus the union of
  `QueryAllProperties(directGenerals)`.
- **Only SAME-NAME redefinitions are storage-free** (`CollectExpression::operator`,
  `ConnectionDefinition::isSufficient`, `Expose::visibility`, the four `kind`s, … — 9 in total).
  They resolve transitively to the root property's column. A redefinition under a NEW name
  (`memberElement` redefines `target`) is a distinct API property with storage of its own —
  exactly as in the generated DTOs, which store both.
- A metaclass gets a subtype table iff it declares ≥1 single-valued stored property and is not
  Element. Multi-valued stored properties become link tables (`{class}_{property}` snake_case).
  `value` being 4 different SQL types on the `Literal*` classes and `kind` being 4 different
  enums is what rules out a single wide table.

## Performance at scale

Audited against the confirmed profile: **~1M elements/project, 100–500 live branches/project,
tens of thousands of commits/project, tens-to-hundreds of projects per instance.** Findings
ranked; all SEV-1/2 fixes are implemented in the schema, the policy items are specified here.

| # | Sev | Finding | Resolution |
|---|---|---|---|
| R1 | 1 | `branch_head` was O(branches × elements): 500 × 1M = 500M rows/project (~85 GB); branch create copied the model | **Fixed**: sparse overlay — `branch.base_commit_id` (a checkpointed commit) + `branch_head` holds only divergence (`is_tombstone` masks deletions). Branch create/delete are O(divergence). |
| R2 | 1 | `get_element_at_branch_head()` filtered on bare uuids → no partition pruning, no PK use (no btree skip scan before PG18; and pruning needs `project_id` on every version) | **Fixed**: every read function resolves `project_id` through `branch` first. Verified: 15/16 partitions "(never executed)", 0.061 ms execution. |
| R3 | 1 | Every `ON DELETE CASCADE` was unindexed → branch/project deletion seq-scanned the largest tables | **Fixed**: `ix_branch_head_branch` for the branch cascade; all `data_identity` cascades demoted to NO ACTION with an explicit ordered per-table project-deletion procedure (documented at the `data_identity` DDL). |
| R4 | 2 | No checkpoint cadence; resolvers degrade linearly with distance to the nearest checkpoint — but checkpoints are O(model) each | **Policy** (service layer): checkpoint when ≥200 commits since the nearest checkpointed ancestor on the lineage OR cumulative changeset ≥25% of model; always at branch-fork bases; retention drops checkpoints no branch bases on (registry row first). `build_commit_checkpoint()` provided; run async, never on the commit path. |
| — | 1* | *(found empirically)* the resolvers' checkpoint-existence probe seq-scanned the checkpoint partition per recursion step — `n_distinct=1` on `(project_id, commit_id)` makes the planner reject the index | **Fixed**: `commit_checkpoint_registry` (one row per checkpoint) serves all existence probes. Measured: single-element historical read 3,466 ms → 1.8–4 ms; full fold 4,012 ms → 185 ms. |
| R5 | 2 | Worst-case derived burst (root rename ≈ 1M multi-KB jsonb rows) × whole-document GIN | **Partially fixed**: lz4 on `stored_json`/`derived_json` (verified inherited by leaves). Bulk derived writes: raise `gin_pending_list_limit` (≥64MB) for the session + `gin_clean_pending_list()` after. GIN stays (spec requires arbitrary-property constraints); revisit as expression indexes if the Query service's filtered-property set proves narrow. |
| R6 | 2 | Single-element historical read folded the whole model | **Fixed**: `resolve_element_at_commit()` / `get_element_at_commit()` — O(walked ancestry). |
| R7 | 3 | Link-table write amplification: a new version re-inserts the element's whole collection (pathological for 100k-child packages) | **Documented, conditional**: content-addressed collections (`collection_id` digest on `element_version`, shared ordered rows) if benchmarks fail; reshapes generated tables. |
| R8 | 3 | Random UUID PKs fragment per-project index ranges | **.NET note**: generate `version_id`/`derived_id` as UUIDv7 (`Guid.CreateVersion7()`); on PG18 the schema additionally self-activates `DEFAULT uuidv7()` on all server-minted keys (version-guarded DO block, no-op on 16/17). `identity_id` is spec-visible, stays as supplied (library elements are normatively v5). |
| R9 | 3 | One-size autovacuum for opposite write profiles | **Fixed**: `branch_head` leaves get `fillfactor 90` + analyze@50k; append-only leaves get insert-driven vacuum (100k) + analyze@50k. |
| R10 | 3 | Generic-plan flips and fast-path lock exhaustion on the 6-join views | **Ops checklist**: prefer PG18 where available (btree skip scan, AIO, `NOT VALID` FKs on partitioned tables, native `uuidv7()`), else PG17 (fast-path slots scale with `max_locks_per_transaction`); verify hot plans show "Subplans Removed: 15"; pin `plan_cache_mode = force_custom_plan` on Query-service pools if generic plans misbehave. |
| R11 | 3 | Bulk-import FK probes (~3M/1M elements). `DEFERRABLE` REJECTED (defers, doesn't reduce); `NOT VALID` needs PG18 on partitioned tables | **Documented**: measure first; importer may use `SET session_replication_role = replica` + post-import validation queries; or raise the floor to PG18 for `NOT VALID`+`VALIDATE`. |
| R12 | 4 | `data_identity` at 10⁸ rows unpartitioned | **Refuted**: narrow, read-mostly FK target; healthy. Hazards route through R3's explicit deletion. |
| R13 | 4 | Fold nondeterminism on sibling-commit timestamp ties (silent wrong answers) | **Fixed**: `id DESC` tiebreaker in every fold; smoke-tested (PASS 10a/10b). |

Measured on PostgreSQL 17 (docker, 200k elements, 2,000 commits, checkpoint at 1,500, 100
overlay branches — shape-faithful, scale ~5× below target):

| Operation | Legacy design | Hardened schema |
|---|---|---|
| Branch create | 2,964 ms (copy 200k rows) | **1.8 ms** (overlay) |
| Branch delete | unindexed seq scans | 34 ms overlay / 100 ms even at 200k rows (indexed cascade) |
| Single-element head read | all 16 partitions scanned | **0.061 ms** exec; 15/16 partitions never executed |
| Single-element historical read (500 commits from checkpoint) | 3,466 ms | **1.8–4 ms** |
| Full-model fold (500 commits from checkpoint) | 4,012 ms | **185 ms** |
| Branch-head set read (200k overlay merge) | — | 1,242 ms |
| `build_commit_checkpoint` (fold 1,500 commits × 200k) | — | 2,488 ms (async budget) |

**Extreme-scale validation** (PostgreSQL 17, docker, `shared_buffers=2GB`): a 1M-element
"giant" project (2,000 commits, checkpoints at 1,500 and 1,900, 200 overlay branches) plus
**40 co-tenant projects (20k elements each) sharing its hash partitions** — ~1.84M
`element_version`, ~1.84M `derived_version`, 2.8M `commit_checkpoint` rows, 2.6 GB total:

| Operation | @200k (single tenant) | @1M (41 tenants) | Verdict |
|---|---|---|---|
| Single-element head read (overlay / checkpoint-fallback) | 3–9 ms | 3.9–4.8 / 2.9–3.7 ms | **flat — log-N as designed** |
| Same read on a small tenant sharing the giant's partitions | — | 3.4–3.9 ms | **no cross-tenant interference** |
| Single-element historical read | 1.8–4 ms | 2.1–2.8 ms | flat |
| Full-model fold (from checkpoint) | 185 ms | 1,100 ms | linear O(model), as documented |
| Branch-head set read | 1,242 ms | 1,124 ms | O(model); **API pagination is mandatory at 1M** |
| GIN probe, non-promoted property, shared partition | — | 1.4–3.1 ms; recheck removed only 10 foreign-tenant rows | R5 cross-tenant concern minor at this shape |
| `qualified_name` equality / LIKE-prefix | — | 0.3 ms / 48 ms | prefix queries: revisit `text_pattern_ops` if hot |
| 100-row commit transaction (ev + dv + overlay upsert) | < 100 ms | ~18 ms | ✓ |
| Branch create / delete (overlay) | 1.8 / 34 ms | 1.2 / 1.9 ms | **O(1) confirmed at 1M** |
| `build_commit_checkpoint` at 1M | extrapolated 12–15 s | **14.9 s measured** | async-budget claim confirmed; the "incremental" build at 1,900 also cost ~17 s — checkpoint builds are O(model) regardless of ancestry |
| 5-join flattening-view planning (928 leaves in catalog) | — | 3.0 ms | R10 fine |
| GIN insert overhead during 1M bulk derived write | — | ~30k rows/s (vs ~31k/s without GIN on element_version) | modest with small `derived_json`; re-measure with production-size (~2–4 KB) documents |

Extrapolation to the edge profile (250 projects: 5×1M + 20×250k + 225×20k ≈ 14.5M identities,
~35M element_version, ~120M derived_version, ~30M checkpoint rows): the single-element paths
stay flat (all probes are PK/index lookups — 100× more rows adds one or two btree levels);
the O(model) operations scale linearly per project, not with instance size; storage lands at
roughly 15 GB element_version + 6 GB checkpoints + a `derived_json`-dominated derived stream
(~145 GB at realistic 1–1.5 KB compressed documents — the synthetic test used ~300 B docs, so
multiply its 1.1 GB accordingly). The instance-level totals are governed by two levers:
**checkpoint retention** (each retained giant checkpoint is ~0.2 GB + 1M rows) and
**derived_json size** (the only column measured in kilobytes).

**Typed-identity / validation / trigger round** (PostgreSQL 18, docker, `shared_buffers=2GB`,
1M elements — 500k Package + 500k OwningMembership — in one project, **~1 KB jsonb payloads**
this time, single import commit + 20k chained commits). A/B against a "legacy-shape" database
(same schema, composite typed-identity FK swapped back to the single-column FK, the
`UNIQUE (id, class_kind)` index and `trg_commit_parent_version` dropped):

Two full runs (fresh containers); both values shown where they differ materially — the
spread IS a finding (limitation 4 below):

| Operation | Typed (current) | Legacy shape | Verdict |
|---|---|---|---|
| 1M `data_identity` inserts | 17.3 s / 14.8 s | 15.5 s / 15.3 s | the A/B delta **flipped sign between runs** — the second index's cost is smaller than Docker-run noise (±15%); treat it as ~free |
| 1M `element_version` inserts (1 KB jsonb) | 40.0 s / 33.5 s | 39.9 s / 41.4 s | **composite FK is free** on the hot path (one index probe either way); same noise caveat |
| 20k commit + parent-edge inserts | 1.02 s / 0.93 s | 0.74 s / 0.71 s | version trigger ≈ **+11–14 µs/commit**, consistent across runs — noise in absolute terms |
| `build_commit_checkpoint` at 1M | 12.4–13.3 s | — | consistent with the earlier 14.9 s |
| `validate_references_at_commit` (temp-table form), clean 1M snapshot | 2.5–4.3 s across runs | — | was 6.9–7.0 s as a plain SQL function; **async-only budget**, never on the commit path |
| same, dirty (1,000 injected problems, full pass) | 3.3–5.8 s, exact counts | — | precision confirmed at scale |
| `validate_references_in_commit`, 101-row change set vs 1M project | **77–86 ms**, exact counts | — | fit for the synchronous commit-validation path |
| single-element `resolve_element_at_commit` | 0.9–2.4 ms | — | consistent with earlier 2–5 ms |
| implied element-write throughput at 1 KB payloads | ~25–30k rows/s | — | the earlier ~30k rows/s was measured at ~300 B docs |

A structural caveat surfaced by reading the plans — the validation arms joined *the full
(project-pruned) table history* against the snapshot, so validate cost grew with **total
history size** — and was then **fixed with a two-tier redesign**, re-measured on the same
1M dataset:

| Operation | Measured | Notes |
|---|---|---|
| Full pass, temp-table variant (`validate_references_at_commit`), clean 1M snapshot | **2.5–4.3 s across runs** (was 6.9–7.0 s as a plain SQL function) | the snapshot is materialized into an ANALYZE'd, indexed temp table, so the planner knows its true cardinality and can pick snapshot-driven PK probes on deep histories — bounding the pass at O(snapshot × log history), never O(history) |
| Incremental tier (`validate_references_in_commit`), 101-row change set against the 1M project | **77–86 ms**, exactly the 2 injected problems | outgoing references of the change set + the REVERSE direction its tombstones break (a live, unchanged holder left dangling — caught via the reverse-lookup indexes and per-target `resolve_element_at_commit` probes); O(change set), fit for the synchronous commit-validation path |

Working protocol: the incremental tier runs per commit; the full pass remains the periodic
audit (checkpoint cadence is a natural rhythm) that backstops it.

**Multi-user round** (PostgreSQL 18, docker, pgbench, 1,000-element project, 16 branches;
the first measurements under REAL concurrency — this partially retires limitation 2 below
for the SQL layer; service-level concurrency remains for the .NET harness):

| Scenario | Result | Invariants (verifier C1–C5) |
|---|---|---|
| HOT: 16 clients racing the full commit protocol on ONE branch, 20 s | 984 attempts/s, 166 winning commits/s, **83.2% CAS-conflict rate**, 0 errors, 0 deadlocks | all PASS: one winner per head value, strictly linear chain, losers wrote nothing |
| SPREAD: same 16 clients on 16 branches | **2,182 commits/s, 0% conflicts** | all PASS — contention is branch-local, as designed (§18.3.6) |
| READS during the hot write storm (8 clients) | 1.21 ms avg (0.79 ms idle baseline), 6,633 reads/s | MVCC promise measured: readers never block on writers |

Re-validated 2026-08-26 after the commit-immutability trigger, the `query` table, and the
live-only partial unique indexes landed: HOT conflict rate identical to the decimal (83.2%),
HOT 902 attempts/s (within machine noise), SPREAD 3,483 commits/s at 0% conflicts on a warmed
run (cold first runs measured as low as 1,174 — run-to-run warm-up variance dwarfs any schema
effect), reads 1.28 ms under storm vs 1.16 ms idle, C1–C5 all PASS in every scenario, and the
.NET fixtures 4/4. No regression from the schema changes.

Protocol notes the suite surfaced (both now normative in guide §18.2): stamp `commit.created`
with `clock_timestamp()`, not transaction-start `now()` — otherwise a transaction that began
before the current head committed stamps its commit earlier than its parent and trips the
monotonicity trigger; and the suite's CAS-FIRST ordering (lock-then-verify) is contention-
equivalent to §18.2's optimistic ordering, which pgbench cannot express (documented in the
setup script). The 83% conflict rate is the deliberate worst case — zero think time on one
branch; real editing sessions sit far below it, and §15.15's CAS-conflict-rate signal now
has a measured ceiling to calibrate against.

**Known limitations of ALL the measured numbers above** — read before quoting them:

1. **Payload realism.** The R1–R13 and extreme-scale rounds used ~300 B synthetic
   `stored_json`/`derived_json`; production documents are 1–4 KB. The typed-identity round
   moved to 1 KB (element-write throughput dropped from ~30k to ~25k rows/s — that ratio is
   the correction factor to keep in mind for the older write numbers). TOAST and GIN costs
   at 2–4 KB derived documents remain unmeasured.
2. **Warm cache; concurrency only at the SQL layer.** The load/read numbers are warm-cache,
   zero-concurrency measurements. The multi-user round above now covers CAS contention,
   deadlock-freedom, and read latency under write storms at the SQL layer — but on a tiny
   (1k-element) model with synthetic payloads; cold-partition p99s, WAL flush pressure at
   production payload sizes, autovacuum interference at sustained commit rates, and all
   service-level concurrency (rebase flows, merge conflicts) remain for the .NET harness
   (below).
3. **Docker-on-Windows I/O.** All rounds ran against Docker Desktop's virtualized filesystem;
   fsync and I/O latency differ from a tuned Linux host. Treat the *relative* comparisons
   (before/after, A/B, flat-vs-linear) as the reliable signal and the absolute milliseconds
   as indicative only.
4. **Single-shot timings.** Most figures are 1–3 runs, no variance reported. The flat-vs-
   linear verdicts are robust to that; the single-digit-millisecond figures are not.
5. **Statistics freshness was hand-managed.** Every round ran `ANALYZE` right after loading;
   production relies on autovacuum keeping statistics current, and the `n_distinct` incident
   proves the plans are sensitive to stale stats — which is why the §15.15 monitoring
   signals include seq-scan counters.

The full .NET benchmark harness for that gate is **built**:
`SysML2.NET.CodeGenerator.Tests/Generators/UmlHandleBarsGenerators/SqlSchemaBenchmarkTestFixture.cs`
(`TestCategory=Benchmark`, Testcontainers PostgreSQL 18, skips without Docker). It loads three
projects with AUTHENTIC `SysML2.NET.Serializer.Json` payloads (content elements + their
OwningMemberships) sharing the hash partitions, replays a commit history with checkpoint
cadence, creates the branch fleet, and measures: bulk-load throughput, per-commit transaction
latency (median/p95), checkpoint builds, branch creation, single-element head/historical
reads, the full fold, the branch-head set read, a keyset page (with a DETERMINISTIC plan-shape
assertion on the §16.5 inlining guard — no `Function Scan`), both validation tiers, the
root-rename derived burst with concurrent read-latency measurement and wait-event sampling,
a UUIDv4-vs-v7 bulk-insert A/B with `pgstatindex` density/fragmentation, and
`pgstattuple`/seq-scan longevity checks. Latencies are reported, never asserted (TESTING.md
§10); asserts cover only deterministic invariants. Scale knobs: `SYSML2_BENCH_ELEMENTS`
(default 100000), `SYSML2_BENCH_COMMITS` (default 2000), `SYSML2_BENCH_BRANCHES` (default
500) — the full production gate is `SYSML2_BENCH_ELEMENTS=1000000 SYSML2_BENCH_COMMITS=20000`.

```bash
dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj \
    --filter "TestCategory=Benchmark" --logger "console;verbosity=detailed"
```

The **generated `ClassKind` C# enum** is built: `ClassKindEnumGenerator` emits
`SysML2.NET/Core/AutoGenEnum/ClassKind.cs` (`enum ClassKind : short`, all 175 registry members
with explicit frozen values) from `ClassKindRegistry.cs`, so its values are frozen across
releases by construction, and a drift test (`ClassKindEnumGeneratorTestFixture`) compares the
compiled enum against the registry. Still noted for the build-out phase: the service startup
assertion against the `class_kind` table, turning the name-is-the-contract rule of guide
§12.1/§15.14 into a runtime fail-fast check.

## Operational requirements

- **`max_locks_per_transaction >= 4096`.** 58 partitioned tables × 16 hash partitions = 928
  leaves; Postgres clones every FK onto every leaf (2,600+ constraints). Whole-schema DDL
  (install, drop, migrate, `pg_dump --schema-only`) exhausts the default 64. Verified
  empirically on PostgreSQL 17: install fails without it. Hot-path queries are unaffected
  (partition pruning at plan time).
- Branch creation is O(model) (`branch_head` copy). Rare and COPY-speed, but with heavy branch
  fan-out on 1M-element projects, switch `branch_head` to a base-commit + delta overlay.
- `element_version` / `derived_version` never receive UPDATEs; autovacuum on their leaf
  partitions is driven by absolute thresholds, set inside the partition-creation loop.

## C# PIM alignment (fixed alongside this schema)

`SysML2.NET/PIM/DTO/` (and the mirroring, currently-unconsumed `SysML2.NET/PIM/POCO/`) now match
the Clause 7.1/7.2 model this schema implements:

- `Commit.PreviousCommit` is an ordered `List<Guid>` (multi-parent merges — `commit_parent`),
  and `Commit.Change` carries the `DataVersion` delta; a deletion is a `DataVersion` with a null
  `Payload` (→ `element_version.tombstone`).
- `PrimitiveConstraint : Constraint`, and `Query` has `OrderBy` — `Query.Where` /
  `CompositeConstraint` compose as the spec requires.
- `CommitReference` has `ReferencedCommit`; `Branch.Head` and `Tag.TaggedCommit` redefine it
  (same underlying value); `Deleted` is `DateTime?`.
- `ProjectUsage : Record` with `UsedProjectCommit` (spec name → `used_project_commit_id`);
  `DataIdentity` carries the derived `CreatedAt`/`DeletedAt` commit references.
- The JSON serializers (`SysML2.NET.Serializer.Json/PIM/`) round-trip all of it, including the
  single-object `previousCommit` form emitted by the Intercax reference implementation.

Deliberately NOT DTO properties: `Commit.versionedData` (derived, unbounded — resolved by
`branch_head` / `resolve_commit_state()`), and `Project.branch`/`tag`/`usage`/`identifiedData`
(derived subsets, absent from API payloads — modeled here as reverse FKs on `branch`/`tag`/
`project_usage`/`data_identity`).

## Verification

```bash
# generate (runs the generator against the checked-in XMI)
dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj \
    --filter "FullyQualifiedName~SQLSchemaGeneratorTestFixture"

# install + functional smoke (59 assertions) against a real PostgreSQL 18
# (both schemas verified on 17 AND 18.6; the recipe follows the prefer-18 version policy)
docker run -d --name sysml2pg -e POSTGRES_PASSWORD=pg postgres:18 -c max_locks_per_transaction=4096
docker cp <schema file> sysml2pg:/tmp/schema.sql
docker cp SysML2.NET.CodeGenerator/Sql/schema.smoke.sql sysml2pg:/tmp/smoke.sql
docker exec sysml2pg psql -U postgres -v ON_ERROR_STOP=1 -f /tmp/schema.sql
docker exec sysml2pg psql -U postgres -v ON_ERROR_STOP=1 -f /tmp/smoke.sql

# multi-user suite (scratch database; setup expects the generated schema installed first)
docker exec sysml2pg psql -U postgres -v ON_ERROR_STOP=1 -f /tmp/conc-setup.sql      # schema.concurrency.setup.sql
docker exec sysml2pg pgbench -n -U postgres -c 16 -j 4 -T 20 -f /tmp/conc-hot.sql    # or conc-spread.sql / conc-read.sql
docker exec sysml2pg psql -U postgres -v ON_ERROR_STOP=1 -f /tmp/conc-verify.sql     # invariants C1–C5, PASS/FAIL

# OR: the .NET route — the same smoke + concurrency suites as NUnit fixtures on a
# PostgreSQL 18 Testcontainer (Docker required; skips cleanly when unavailable).
# The fixtures generate the schema IN-PROCESS from the UML model, so they also catch
# generator/registry drift; the smoke PASS count is read from the script itself.
# Fixtures: SysML2.NET.CodeGenerator.Tests/Generators/UmlHandleBarsGenerators/
#   SqlSchemaSmokeTestFixture.cs, SqlSchemaConcurrencyTestFixture.cs (TESTING.md §10)
dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj \
    --filter "TestCategory=Integration"
```

Note: the smoke file runs UNCHANGED against both schemas. It seeds `model_version` and
`class_kind` itself, idempotently (`ON CONFLICT (id) DO NOTHING`) and **with the frozen
registry ids** (OwningMembership=116, Package=117, PartUsage=120 — identical to the generated
seeds, a no-op there), and references kinds everywhere else by name lookup
(`SELECT id FROM sysml2.class_kind WHERE name = '…'`). No adaptation step exists anymore.

The smoke test's load-bearing assertions: a Package rename changes the child's derived
`qualifiedName` while the child still resolves to its ORIGINAL version row (PASS 2a/2b — the
reason derived state is a second stream); a two-parent merge commit resolves to the merge's
own conflict resolution while elements deleted on non-ancestor branches stay alive (PASS
8a–8c); the branch_head overlay life cycle — O(1) branch creation at a checkpoint,
read-through to the base, tombstone masking, and overlay-only deletion (PASS 9a–9f);
deterministic fold resolution on sibling-commit timestamp ties, through both resolvers
(PASS 10a/10b); the multi-version rules — idempotent registry seeds, the conversion
commit as the only way up, and rejection of downgrades, mixed-release merges, and
convert+merge combos (PASS 11a–11e); the typed identity plus reference validation —
the composite FK rejecting a version whose class_kind contradicts its identity, a clean
snapshot validating clean, and wrong-type/dangling references being reported precisely
(PASS 12a–12c); the incremental tier — the change set's own problems, a healthy
commit validating clean, and a tombstone's reverse-direction dangling reference caught
in agreement with the full audit (PASS 13a–13c); commit immutability — UPDATEs of
`created` (the column the acyclicity proof and the fold rest on) and of any other commit
column both rejected by `trg_commit_immutable` (PASS 14a/14b); and the API-route storage
shapes — the tag lifecycle (create/read, dangling-commit FK guard, destructible; PASS
15a–15c), `project_usage` pinning a used project at a commit with its FK guard (PASS
16a/16b), the `GET /roots` query over the promoted `owner` column (PASS 17), and the
reverse relationship lookup by related element through the `ix_{link}_target` indexes and
the snapshot (PASS 18a/18b). The remaining API-route surface is asserted too: the plain PIM
record reads and updates — project list/read/rename, branch record, commit record, change set
and DataVersion by id, and the registry-frozen `class_kind`/`model_version` catalogs behind
`/meta/datatypes` (PASS 19a–22b); the stored-Query lifecycle — create/read, mutable update,
destructible delete — plus an end-to-end §16.4-translated execution returning exactly the
expected element at branch head (PASS 23a–24a); the commit-diff shape as a FULL JOIN of two
resolved snapshots (PASS 25a); and the ordered project-deletion procedure — blocked first by
an inbound `project_usage`, then by the checkpoint registry's NO ACTION FK when attempted out
of order (the guard that makes the registry step part of the documented order), then completing
cleanly with the neighbor project untouched (PASS 26a–26c). Finally the ref-deletion
protocol (guide §6.3): a soft-deleted branch keeps its audit record while its overlay is
purged, retired names are immediately reusable through the live-only partial unique
indexes, duplicate live names are still rejected, and the same holds for tags
(PASS 27a–27c).
