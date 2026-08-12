# SQLSCHEMA.md — PostgreSQL persistence schema for SysML v2 models

This document is the compact design reference for the SQL schema code-generation pipeline.
For the long-form architectural guide — the full reasoning chain behind every table, function,
and index, with worked examples and the performance war stories — see
`SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md`.

The pipeline artifacts:

| Artifact | Role |
|---|---|
| `SysML2.NET.CodeGenerator/Sql/schema.golden.sql` | Hand-written, annotated reference design. Carries the rationale comments. |
| `SysML2.NET.CodeGenerator/Sql/schema2.generated.sql` | The actual generator output, checked in for review. Supersedes the golden's `[GENERATED]` excerpts. |
| `SysML2.NET.CodeGenerator/Sql/schema.smoke.sql` | Functional test. 11 assertions; raises on any wrong answer. Runs against golden AND generated schema. |
| `SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs` | Handlebars template: hand-written sections verbatim, `[GENERATED]` sections via helpers. |
| `SysML2.NET.CodeGenerator/HandleBarHelpers/SqlSchemaHelpers.cs` | The nine `uml_template.SQL2.*` helpers. |
| `SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs` | Naming, type mapping, and the stored-property census logic. |
| `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/SQLSchemaGenerator.cs` | The generator (emits `schema2.sql`). |

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
   (`data_identity`); "does the target exist at commit C" is a validation query, not an FK.

## Architecture (four layers)

1. **PIM / versioning** (hand-written): `project`, `commit`, `commit_parent` (the DAG — a commit
   has a SET of parents; merges are real), `branch`, `tag`, `project_usage`, `data_identity`.
   A trigger enforces the spec's monotonic-commit-timestamp invariant because the snapshot
   resolver depends on it ("newest ancestor wins").
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

Derived-property conformance level implemented: **Full Conformance, precomputed at commit time**
(the existing 366 implemented `SysML2.NET/Extend/*.Compute*` methods are the computation engine;
the impact-radius analysis lives in .NET, not SQL).

## What is generated vs hand-written

Generated from the UML XMI (`Resources/KerML_only_xmi.uml` + `Resources/SysML_only_xmi.uml`):

- the 7 enum types (labels = lowercase C# literals, matching the JSON wire format);
- `class_kind` (175 interned metaclass ids, deterministic: 1-based index of the name-ordered
  class list), `class_kind_table` (which subtype tables each concrete class joins — the
  inheritance DAG flattened), and `property_catalog` (12,113 rows mapping every API property
  name of every concrete class to its storage: column / link_table / derived);
- the 7 link tables and 47 subtype tables, with NULL-ability from the property's lower bound,
  `DEFAULT` clauses from the XMI-declared default values, FKs to `data_identity`, and a
  reverse-lookup index on every reference column;
- 167 per-metaclass flattening views (`v_part_usage`, …) reconstructing the DTO row shape;
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
| R2 | 1 | `get_element_at_branch_head()` filtered on bare uuids → no partition pruning, no PK use (PG16/17 has no btree skip scan) | **Fixed**: every read function resolves `project_id` through `branch` first. Verified: 15/16 partitions "(never executed)", 0.061 ms execution. |
| R3 | 1 | Every `ON DELETE CASCADE` was unindexed → branch/project deletion seq-scanned the largest tables | **Fixed**: `ix_branch_head_branch` for the branch cascade; all `data_identity` cascades demoted to NO ACTION with an explicit ordered per-table project-deletion procedure (documented at the `data_identity` DDL). |
| R4 | 2 | No checkpoint cadence; resolvers degrade linearly with distance to the nearest checkpoint — but checkpoints are O(model) each | **Policy** (service layer): checkpoint when ≥200 commits since the nearest checkpointed ancestor on the lineage OR cumulative changeset ≥25% of model; always at branch-fork bases; retention drops checkpoints no branch bases on (registry row first). `build_commit_checkpoint()` provided; run async, never on the commit path. |
| — | 1* | *(found empirically)* the resolvers' checkpoint-existence probe seq-scanned the checkpoint partition per recursion step — `n_distinct=1` on `(project_id, commit_id)` makes the planner reject the index | **Fixed**: `commit_checkpoint_registry` (one row per checkpoint) serves all existence probes. Measured: single-element historical read 3,466 ms → 1.8–4 ms; full fold 4,012 ms → 185 ms. |
| R5 | 2 | Worst-case derived burst (root rename ≈ 1M multi-KB jsonb rows) × whole-document GIN | **Partially fixed**: lz4 on `stored_json`/`derived_json` (verified inherited by leaves). Bulk derived writes: raise `gin_pending_list_limit` (≥64MB) for the session + `gin_clean_pending_list()` after. GIN stays (spec requires arbitrary-property constraints); revisit as expression indexes if the Query service's filtered-property set proves narrow. |
| R6 | 2 | Single-element historical read folded the whole model | **Fixed**: `resolve_element_at_commit()` / `get_element_at_commit()` — O(walked ancestry). |
| R7 | 3 | Link-table write amplification: a new version re-inserts the element's whole collection (pathological for 100k-child packages) | **Documented, conditional**: content-addressed collections (`collection_id` digest on `element_version`, shared ordered rows) if benchmarks fail; reshapes generated tables. |
| R8 | 3 | Random UUID PKs fragment per-project index ranges | **.NET note**: generate `version_id`/`derived_id` as UUIDv7 (`Guid.CreateVersion7()`); `identity_id` is spec-visible, stays as supplied (library elements are normatively v5). |
| R9 | 3 | One-size autovacuum for opposite write profiles | **Fixed**: `branch_head` leaves get `fillfactor 90` + analyze@50k; append-only leaves get insert-driven vacuum (100k) + analyze@50k. |
| R10 | 3 | Generic-plan flips and fast-path lock exhaustion on the 6-join views | **Ops checklist**: prefer PG17 (fast-path slots scale with `max_locks_per_transaction`); verify hot plans show "Subplans Removed: 15"; pin `plan_cache_mode = force_custom_plan` on Query-service pools if generic plans misbehave. |
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

Follow-up gate before production (not built here): the full .NET benchmark harness — 3×1M-element
projects with authentic serializer payloads sharing hash partitions, 20k-commit history replay,
500 branches, root-rename burst with concurrent read-latency measurement, UUIDv4-vs-v7 A/B, and
`pgstattuple`/wait-event longevity checks.

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

# install + functional smoke (11 assertions) against a real PostgreSQL 17
docker run -d --name sysml2pg -e POSTGRES_PASSWORD=pg postgres:17 -c max_locks_per_transaction=4096
docker cp <schema file> sysml2pg:/tmp/schema.sql
docker cp SysML2.NET.CodeGenerator/Sql/schema.smoke.sql sysml2pg:/tmp/smoke.sql
docker exec sysml2pg psql -U postgres -v ON_ERROR_STOP=1 -f /tmp/schema.sql
docker exec sysml2pg psql -U postgres -v ON_ERROR_STOP=1 -f /tmp/smoke.sql
```

Note: `schema.smoke.sql` seeds `class_kind` itself (golden has an empty catalog). Against the
GENERATED schema — whose catalog is pre-filled — drop the smoke file's `class_kind` INSERT and
replace the hard-coded kind ids with `(SELECT id FROM sysml2.class_kind WHERE name = '…')`.

The smoke test's load-bearing assertions: a Package rename changes the child's derived
`qualifiedName` while the child still resolves to its ORIGINAL version row (2a/2b — the reason
derived state is a second stream), and a two-parent merge commit resolves to the merge's own
conflict resolution while elements deleted on non-ancestor branches stay alive (8a–8c).
