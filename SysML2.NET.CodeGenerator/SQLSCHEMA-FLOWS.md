# SQLSCHEMA-FLOWS.md — Functional flows through the SysML2 persistence layer

This document walks the **main use cases** of a SysML v2 model repository — create a project,
commit a change, branch, merge, tag, query, delete — and for each one names the **technical
moving parts** that are involved, in the order they are touched, and **why they exist**. It is
the "what is everything for, and when do I use it" companion to:

- `SysML2.NET.CodeGenerator/SQLSCHEMA.md` — the compact design reference (tables, numbers, audit)
- `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md` — the long-form architectural guide (the *why* in depth)
- `SysML2.NET.CodeGenerator/IMPACT-RADIUS.md` — the derived-property engine design sketch
- `SysML2.NET.CodeGenerator/Sql/schema.smoke.sql` — the executable proof of every flow below (59 `PASS` assertions)

Every flow cites the smoke-test assertions that prove it (`PASS 9a–9f`, etc.) and the guide
section that explains it in depth (`guide §10.2`, etc.). If you want to *see* a flow run, open
`schema.smoke.sql` at the cited PASS and read the SQL above it.

---

## Table of contents

- [0. The cast: every moving part in one table](#0-the-cast-every-moving-part-in-one-table)
- [1. The two ideas everything else follows from](#1-the-two-ideas-everything-else-follows-from)
- [2. Install the schema](#2-install-the-schema)
- [3. Create a project](#3-create-a-project)
- [4. Create elements (the first commit)](#4-create-elements-the-first-commit)
- [5. Change an element (a normal commit)](#5-change-an-element-a-normal-commit)
- [6. Delete an element](#6-delete-an-element)
- [7. Read the model](#7-read-the-model)
- [8. Create a branch](#8-create-a-branch)
- [9. Commit on a branch, concurrently with others](#9-commit-on-a-branch-concurrently-with-others)
- [10. Merge two branches](#10-merge-two-branches)
- [11. Tag a commit](#11-tag-a-commit)
- [12. Delete a branch or tag](#12-delete-a-branch-or-tag)
- [13. Checkpoints: the background maintenance loop](#13-checkpoints-the-background-maintenance-loop)
- [14. Validate the model](#14-validate-the-model)
- [15. Query the model](#15-query-the-model)
- [16. Diff two commits](#16-diff-two-commits)
- [17. Use another project (cross-project references)](#17-use-another-project-cross-project-references)
- [18. Upgrade to a new metamodel release](#18-upgrade-to-a-new-metamodel-release)
- [19. Delete a project](#19-delete-a-project)
- [20. Change the metamodel: regenerate the schema](#20-change-the-metamodel-regenerate-the-schema)
- [21. The test pyramid: which test proves what](#21-the-test-pyramid-which-test-proves-what)
- [22. What the service layer must still build](#22-what-the-service-layer-must-still-build)

---

## 0. The cast: every moving part in one table

Before the flows, the inventory. Everything in the schema belongs to one of four layers; the
"When it is touched" column is the quick answer to "when do I use this?".

| Layer | Part | What it is | When it is touched |
|---|---|---|---|
| **A. PIM / versioning** (hand-written) | `project` | The container record. Carries `default_branch_id` and `target_model_version_id` (upgrade policy). | create / rename / delete project |
| | `commit` | One immutable node of history. `created` + `model_version_id` are load-bearing. | every commit; never updated (`trg_commit_immutable`) |
| | `commit_parent` | The DAG edges — a commit has a *set* of ordered parents. | every commit (1 edge), every merge (2+ edges) |
| | `branch` | The only mutable thing: `head_commit_id` moves on every commit; `base_commit_id` anchors the overlay. | create branch, commit, compaction, soft delete |
| | `tag` | A frozen pointer to a commit. | tag, soft delete tag |
| | `query` | A stored spec Query definition as JSON. | save / run a query |
| | `project_usage` | "Project A uses project B at commit C". | cross-project import |
| | `data_identity` | The stable `@id` of an element plus its **immutable metaclass** (`class_kind`). The FK target of every reference. | create element (once, ever) |
| | `model_version`, `class_kind` | The append-only registry seeds: release ordinals and frozen metaclass ids. | install; metamodel upgrade |
| **B. Stored element state** (append-only) | `element_version` | One row per (element, commit) that changed it. Holds Element's own columns, `tombstone`, and `stored_json` (pre-serialized stored half of the payload). | every create / change / delete of an element |
| | 47 `*_version` subtype tables | The normalized stored columns of each storage-declaring metaclass (`feature_version`, `usage_version`, …). FKs and constraints live here. | same transaction as `element_version` |
| | 7 link tables | Ordered multi-valued stored properties (`element_owned_relationship`, `relationship_target`, …). Keyed by *version*. | same transaction; also reverse-lookup via `ix_*_target` |
| **C. Derived element state** (append-only) | `derived_version` | One row per (element, commit) whose **derived** values changed — the impact radius. Six promoted columns (`owner`, `qualified_name`, `name`, …) plus `derived_json`. | every commit, for the change set AND the elements it affects |
| **D. Snapshot resolution + read path** | `commit_checkpoint` + `commit_checkpoint_registry` | A fully materialized snapshot at one commit (one row per live element), and a one-row-per-checkpoint marker. | background cadence; branch-fork bases |
| | `branch_head` | The sparse **overlay**: only the branch's divergence from its base checkpoint. | every commit (upsert), branch delete (purge) |
| | `resolve_commit_state()`, `resolve_element_at_commit()` | The fold: the spec's `versionedData` algorithm, bounded by checkpoints. | historical reads, diff, validation, checkpoint build |
| | `get_element_at_branch_head()`, `get_elements_at_branch_head()`, `get_element_at_commit()`, `get_elements_at_commit()` | The API read functions: `stored_json || derived_json`. | every GET of an element |
| | `build_commit_checkpoint()` | Materializes a checkpoint from the resolver, idempotently. | background cadence |
| | `validate_references_in_commit()`, `validate_references_at_commit()` | Two-tier dangling/wrong-type reference validation. | per commit (incremental); periodic audit (full) |
| | `vw_*` flattening views (167) | Reconstruct the DTO row shape per metaclass. | ad-hoc SQL, reporting, debugging |
| **Triggers** | `trg_commit_immutable` | Rejects every UPDATE of `commit`. | guards every flow |
| | `trg_commit_parent_monotonic` | A child must be strictly newer than every parent. Doubles as the acyclicity proof. | every commit / merge |
| | `trg_commit_parent_version` | No downgrade; merge parents must share the merge's release. | commit, merge, conversion commit |
| **.NET side** | `SysML2.NET/PIM/DTO/*` | `Project`, `Commit`, `Branch`, `Tag`, `DataVersion`, `DataIdentity`, `Query`, `ProjectUsage` — the API records. | the REST API's wire shapes |
| | `SysML2.NET/Core/AutoGenEnum/ClassKind.cs` | The generated `enum ClassKind : short` with the frozen registry values. | every write that stamps `class_kind` |
| | `SysML2.NET/Extend/*.Compute*` | The 366 derived-property computations — the engine that fills `derived_version`. | commit time (impact radius) |
| | `SysML2.NET.Serializer.Json` | Produces `stored_json` (and splits payloads into stored / derived halves). | commit time |

---

## 1. The two ideas everything else follows from

Every flow below is a consequence of two axioms (guide §4). Keep them in mind and the shape of
each flow becomes predictable.

**Axiom 1 — references name identities, never versions.** A SysML reference targets a stable
element `@id`. Under branching, "does the target exist?" is only answerable against the
identity, so *every* reference column in the schema is an FK to `data_identity(id)`. Whether
the target is *alive at commit C* is not an FK question at all; it is a per-commit validation
(`validate_references_*`, flow 14).

**Axiom 2 — a derived value is a function of (identity, commit), not of (version).** Renaming
package `Old` to `New` changes the `qualifiedName` of every descendant without touching any
descendant's stored state. That is why derived state is a **second append-only stream**
(`derived_version`) resolved by the same fold as stored state — never a column on
`element_version`. Smoke PASS 2a/2b is the proof: after the rename, the child's derived
`qualifiedName` changed while the child still resolves to its *original* version row.

Two further consequences shape the write path:

- **Append-only is the concurrency model** (guide §18.1). `commit`, `commit_parent`,
  `element_version`, `derived_version`, `commit_checkpoint` are never updated. All mutable
  state is squeezed into one `branch` row per branch plus its overlay. Readers never block
  writers; writers on different branches never conflict; writers on the *same* branch conflict
  on exactly one row, which is the correct semantics for a serial history.
- **Reads must be cheap, so writes pay** (guide §9.1). Derived values are precomputed at commit
  time, serving an element is one jsonb concat over PK probes, and the historical fold is
  bounded by checkpoints.

---

## 2. Install the schema

**Use case:** stand up an empty database.

| Step | Part | Why |
|---|---|---|
| 1 | Start PostgreSQL 18 (17 works) with `max_locks_per_transaction >= 4096` | 58 partitioned tables × 16 hash partitions = 928 leaves; whole-schema DDL exhausts the default 64 locks. Verified: install fails without it. |
| 2 | Apply `SysML2.NET.CodeGenerator/Sql/schema2.generated.sql` | The generator output: enum types, registry seeds, PIM tables, element/derived tables, link + subtype tables, views, resolvers, validators, partitions. |
| 3 | The registry seeds run as `ON CONFLICT (id) DO NOTHING` | Idempotent: safe to re-apply on upgrade; `class_kind` ids are frozen forever by `ClassKindRegistry.cs`. PASS 11a. |

Nothing else is needed; the schema is self-contained. The .NET fixtures do exactly this in a
Testcontainer (`PostgreSqlSchemaTestHost.cs`), generating the schema in-process from the UML
model so registry drift is caught at the same time.

---

## 3. Create a project

**Use case:** `POST /projects`.

| Step | Part | Why |
|---|---|---|
| 1 | `INSERT project (id, name, created)` | The container. `target_model_version_id` stays NULL (= unrestricted release policy) unless the operator sets one. |
| 2 | Create the first commit (flow 4) and the default branch pointing at it | A project without a branch has no readable head. |
| 3 | `UPDATE project SET default_branch_id = …` | The FK `project → branch → project` is circular, so it is `DEFERRABLE INITIALLY DEFERRED`; create both in one transaction. |

PASS 19a/19b cover list, read and rename (`project` is mutable per Clause 7.1.2). The smoke
test's setup (lines 29–45) is the minimal version of this flow.

**Bootstrap note:** a brand-new branch has `base_commit_id = NULL`, meaning "the overlay *is*
the full head state" (guide §10.2). That mode is O(model) and only acceptable until the first
checkpoint exists; monitoring signal §15.15 alerts if any live branch is still in it.

---

## 4. Create elements (the first commit)

**Use case:** `POST /projects/{p}/commits` with a change set of new elements.

This is the canonical **commit transaction** (guide §15.3). Every later flow that writes is a
variation of it.

| Step | Part | Why |
|---|---|---|
| 1 | Read the branch's `head_commit_id` (the expected parent) | The parent the client built against — needed for the CAS in step 8. |
| 2 | `INSERT data_identity (id, project_id, class_kind)` — one per **new** element | Mints the stable `@id`, typed with the metaclass that will never change. Existing elements already have theirs. |
| 3 | `INSERT commit (…, created = clock_timestamp(), model_version_id)` | `clock_timestamp()`, not `now()`: a transaction that began before the current head committed would otherwise stamp *earlier* than its parent and trip the monotonicity trigger (guide §18.2). |
| 4 | `INSERT commit_parent (commit, parent, ordinal 0)` | `trg_commit_parent_monotonic` validates child newer than parent (PASS 6); `trg_commit_parent_version` validates the release (PASS 11c). |
| 5 | `INSERT element_version` — one row per element in the change set, with `class_kind`, Element's own columns, and `stored_json` | The system-of-record row plus the pre-serialized read model. The composite FK `(identity_id, class_kind)` makes a version that contradicts its identity's metaclass impossible (PASS 12a). `ux_element_version_identity_commit` enforces one version per element per commit. |
| 6 | `INSERT <subtype>_version` rows for each storage-declaring ancestor (a `PartUsage` → `type_version`, `feature_version`, `usage_version`, `occurrence_usage_version`) + link-table rows (`element_owned_relationship`, …) | Normalized columns carry the FKs, NOT NULLs, DEFAULTs and reverse-lookup indexes. Which tables a metaclass participates in comes from the generated descriptors (guide §12.2). |
| 7 | `INSERT derived_version` — for the change set **and** every element whose derived values changed (the impact radius), with the promoted six columns extracted | Axiom 2. Computed by `SysML2.NET/Extend/*.Compute*` against the parent snapshot + change set; the engine design is `IMPACT-RADIUS.md`. For the first commit the radius is just the new elements. |
| 8 | `UPDATE branch SET head_commit_id = :new WHERE id = :b AND head_commit_id = :expected` — rowcount 0 → ROLLBACK, 409 | The **compare-and-swap** (guide §18.2, normative). Without it two concurrent committers silently lose one commit. |
| 9 | `INSERT branch_head … ON CONFLICT (project_id, branch_id, identity_id) DO UPDATE` — one upsert per change-set element | Keeps the overlay = divergence from base, O(changeset). |
| 10 | `COMMIT` | One transaction: pure inserts + one branch-row update. |
| 11 | *(after commit, synchronous)* `validate_references_in_commit(project, commit)` | O(change set); gates acceptance (flow 14). |

The smoke test writes this by hand at lines 59–123 (c1 and c2); the benchmark's
`ReplayHistoryAsync` (`SqlSchemaBenchmarkTestFixture.cs:413`) does it from .NET with
`NpgsqlBatch` and authentic `SysML2.NET.Serializer.Json` payloads; the concurrency suite's
`bench_try_commit` (`schema.concurrency.setup.sql`) is the same protocol under a 16-client race.

**The "add a child" coupling** (guide §15.8–9): "add child B to package A" writes a new B, a new
`OwningMembership` M, **and a new version of A** — because A's `ownedRelationship` list is
stored, ordered state. One ownership fact lives in four places (A's list, M's
`owning_related_element`, M's `ownedRelatedElement` list, B's `owning_relationship`) and the
service must keep all four coherent in the change set. This is also why popular containers are
contention points (flow 9).

---

## 5. Change an element (a normal commit)

**Use case:** rename a package; edit a feature's `direction`; move an element.

Identical to flow 4 except:

- No new `data_identity` row — the element already has one. Identities are created once, ever.
- `element_version` gets a **new row** for the changed element only (new `version_id`, same
  `identity_id`). Nothing is updated; the old row remains for history.
- `derived_version` gets rows for the element **and its impact radius**. The smoke scenario is
  the archetype: c2 renames package `Old` → `New`; `element_version` gets one row (the
  package), `derived_version` gets two (the package *and the untouched child*, whose
  `qualifiedName` is now `New::wheel`). PASS 2a/2b, guide §16.1.
- The overlay upsert replaces the element's `version_id` / `derived_id` pointers on the branch.

Worst case to be aware of: renaming a namespace near the root invalidates `qualifiedName` for
most of the model, producing a burst of ~1M `derived_version` rows. That is inherent to the
spec's semantics; the schema makes it survivable (lz4, GIN pending-list tuning — audit R5), and
the benchmark's `Root_rename_burst_with_concurrent_reads` measures it with readers running.
Preferred service pattern: compute the derived delta *before* taking the branch lock, then CAS
(guide §18.3.1).

---

## 6. Delete an element

**Use case:** delete a part and everything it owns.

| Step | Part | Why |
|---|---|---|
| 1 | Compute the **owned closure**: the element, its memberships, all transitive children | Spec invariant 3: a deletion must delete something; and the schema will happily store a half-deleted tree (Axiom 1 — FKs check existence, not liveness). Only the service guarantees closure completeness (guide §15.11). |
| 2 | For each: `INSERT element_version (…, tombstone = true)` with no payload | A deletion is an **event in history**, not an absence of data: `DataVersion.payload = null` → `tombstone = true`. The two CHECK constraints make tombstones and payload rows mutually exclusive shapes. |
| 3 | A new version of the **owner** with the membership removed from its `ownedRelationship` list | Same coupling as flow 4. |
| 4 | Overlay upsert with `is_tombstone = true` | Masks the base checkpoint's row for this branch without touching the checkpoint (PASS 9d). |
| 5 | `validate_references_in_commit` | Catches the *reverse* direction: a live, unchanged element elsewhere that still references the deleted identity (PASS 13c). |

The resolver drops tombstoned identities from every snapshot (PASS 3). The identity row itself
stays — references from other branches and from history still need it.

---

## 7. Read the model

**Use case:** `GET /projects/{p}/branches/{b}/elements/{e}` (the hottest query in the system)
and its siblings.

| Read | Function | Path | Cost |
|---|---|---|---|
| One element at a branch head | `get_element_at_branch_head(branch, identity)` | `branch` → overlay row? else base `commit_checkpoint` row → `element_version.stored_json \|\| derived_version.derived_json` | 4 PK probes on one partition; measured 0.06 ms exec, 15/16 partitions never executed |
| All elements at a branch head | `get_elements_at_branch_head(branch)` | base checkpoint minus overlaid identities (anti-join) `UNION ALL` live overlay | O(model); ~1.1 s at 1M → **paginate** (flow 7b) |
| One element at a historical commit | `get_element_at_commit(project, commit, identity)` → `resolve_element_at_commit` | walk `commit_parent` back to the nearest checkpoint, filtered to one identity | O(walked ancestry); 2–4 ms at 500 commits from checkpoint |
| All elements at a historical commit | `get_elements_at_commit(project, commit)` → `resolve_commit_state` | the full fold (below) | 185 ms at 200k; 1.1 s at 1M |

Why the branch-head read starts from `branch` instead of filtering `branch_head` directly:
`branch` recovers `project_id`, and every partitioned table in the schema needs `project_id`
to prune to one partition and use its PK. The original version filtered on bare uuids and
scanned all 16 partitions (audit R2). Rule: **any query against a partitioned table whose only
keys are bare uuids is defective by construction.**

Why the read is one SQL statement: one statement sees one MVCC snapshot, so a reader can never
observe "new base pointer + old overlay" mid-compaction (guide §18.1). Splitting it into two
round-trips loses that guarantee.

**The fold** (`resolve_commit_state`, guide §10.4), in words: is this commit checkpointed? If
not, walk parents recursively, stopping at checkpointed commits; for every element changed in
that window take the newest version (`ORDER BY created DESC, id DESC`); for everything else
take the boundary checkpoint's row; drop tombstones; repeat for the derived stream. It is the
spec's own `versionedData` OCL (guide §6.2), executed by index.

### 7b. Paging

Set reads are O(model), so the API must paginate (guide §16.5). Two rules and one guard:

1. **Anchor to a commit, never to HEAD.** Resolve branch → commit once, put the commit id in
   the page token. Commits are immutable, so pages are torn-free forever with no server-side
   cursor. (Paging at HEAD while a colleague commits returns a torn collection — §18.3.2.)
2. **Keyset, never OFFSET.** `WHERE identity_id > :last ORDER BY identity_id LIMIT n` — both
   legs of the set-read function end in `identity_id` in their PKs, so each page is a range
   scan.
3. **The inlining guard.** `get_elements_at_branch_head` is a single-`SELECT` `LANGUAGE sql
   STABLE` function so PostgreSQL inlines it and the keyset predicate reaches the index scans.
   If the plan ever shows `Function Scan on get_elements_at_branch_head`, paging has silently
   degraded to O(model) per page. The benchmark asserts the plan shape
   (`Keyset_page_is_index_priced_and_inlining_holds`).

---

## 8. Create a branch

**Use case:** `POST /projects/{p}/branches` from a commit.

| Case | What happens | Cost |
|---|---|---|
| Fork at a **checkpointed** commit | `INSERT branch (…, head_commit_id = c, base_commit_id = c)` — **zero** overlay rows | O(1): 1.2–1.8 ms measured, independent of model size (PASS 9b) |
| Fork at a **non-checkpointed** commit | base = nearest checkpointed ancestor; write the (checkpoint → fork) delta into `branch_head` | O(delta) |
| Fork before any checkpoint exists | `base_commit_id = NULL`, overlay holds everything | O(model) — bootstrap only |

Why this shape: the naive "one row per (branch, element)" was the original design, and 500
branches × 1M elements = 500M rows per project. The overlay stores only **divergence**, and
hundreds of branches share the same few checkpoints (guide §10.2, audit R1). The benchmark's
`CreateBranchFleetAsync` creates 500 branches this way.

Invariant the service owns: `base_commit_id` must always point at a **checkpointed** commit
(guide §15.4) — which is why the cadence policy always checkpoints branch-fork bases.

Branch names are unique **among live branches only** (`ux_branch_project_name_live`), so a
name retired by a soft delete can be reused immediately (flow 12).

---

## 9. Commit on a branch, concurrently with others

**Use case:** many users editing; several on the same branch.

The commit transaction of flow 4 already contains the protocol; this flow explains what it
buys and what it does not.

**Different branches never conflict.** Each branch's mutable state is its own `branch` row and
its own overlay rows. Measured: 16 writers on 16 branches, 2,200–3,500 commits/s, 0 conflicts
(`SpreadBranches_OneWriterPerBranch_NoConflicts`).

**Same branch: exactly one winner per head value.** The CAS `UPDATE branch … WHERE
head_commit_id = :expected` lets exactly one of N racing transactions through; losers see
rowcount 0, roll back, return 409, and the client rebases. Measured: 16 writers hammering one
branch with zero think time, ~1,000 attempts/s, 83% conflict rate (the deliberate worst case),
0 errors, 0 deadlocks, heads strictly linear, losers wrote nothing
(`HotBranchRace_SixteenWriters_ExactlyOneWinnerPerHead`; verifier invariants C1–C5 in
`schema.concurrency.verify.sql`).

**Readers never block.** During the write storm, reads stayed at ~1.2 ms vs 0.8 ms idle
(`ReadsDuringWriteStorm_ReadersNeverBlocked`). This is MVCC on append-only tables; plain READ
COMMITTED suffices everywhere, nothing needs SERIALIZABLE.

**Lock ordering.** Touching the `branch` row first in every writer (CAS or `SELECT … FOR
UPDATE`) gives all writers the same lock order, which is deadlock prevention for free.
Compaction (flow 13) must take the same lock.

**What the client does on 409 — the rebase** (service work, guide §15.8–10, §18.3.6):

- Re-read the new head, three-way merge the change set against it.
- Additive, disjoint collection changes (two users each added a child to the same package)
  **must** auto-merge under a deterministic ordering policy; otherwise every shared container
  becomes a conflict magnet, because every child addition versions the parent.
- Conflict detection must be containment-aware: a tombstoned package conflicts with every edit
  *under* it, even though the identities are disjoint.
- Retry the commit with the new expected parent.

Small print: the monotonicity trigger rejects two commits in the same microsecond on one
lineage — re-stamp and retry; let the database assign `created` in multi-server deployments.

### 9b. Collaborate on one branch: staying current and change sets

**Use case:** ten people on the same branch, each committing five changes a minute, for an
hour. Three questions: does it hold up, how does every client stay current, and can a client
get "what changed" instead of re-reading the model.

**Does it hold up — the arithmetic.** That load is 50 commits/min, 3,000 commits in the hour,
one commit every ~1.2 s on average. Against the measured numbers:

| Quantity | The scenario | Measured |
|---|---|---|
| Commit rate on one branch | ~0.8/s | ~1,000 attempts/s, 16 writers, zero think time (hot-branch race) |
| Commits after an hour | 3,000 | the benchmark replay runs 20,000 (`SYSML2_BENCH_COMMITS`) |
| Head read while others write | | ~1.2 ms, never blocked (reads-under-storm) |
| Checkpoints the cadence triggers | ~15 (one per 200 commits) | 12–15 s each at 1M elements, sub-second for a 10k model, off the commit path |
| Overlay rows after the hour | bounded by the *distinct* identities touched (upsert) | compaction threshold ~100k rows / ~10% of model |

Throughput sits well under one percent of the hot-branch ceiling, and nothing grows with
history length: head reads go through the overlay (flow 7) and cost the same at commit 3,000
as at commit 1; historical reads walk at most ~200 commits to the nearest checkpoint
(flow 13). One spike to know about: renaming a high-level package writes a `derived_version`
row for every element under it (the root-rename burst in the benchmark) — O(model), not
O(change set), but rare at this rate.

**What actually matters: stale heads and shared containers.** The CAS of flow 9 needs the
expected head. With a commit landing every 1.2 s and a user taking ten seconds to make an edit,
almost every commit arrives with a stale head and gets a 409 — and that is normal, not a
failure. The service therefore **must rebase automatically**: re-read the head, three-way
merge the change set, retry. A retry colliding again is rare (commit rate × transaction
duration, well under one percent), so one retry nearly always suffices and users never see
the 409. The second rule is the one that decides whether the branch is usable at all: adding a
child to a package versions the package, so ten people filling the same package all touch the
same parent identity. Additive, disjoint collection changes **must auto-merge** under a
deterministic ordering (guide §18.3.6, obligation 4 in flow 22); if the merge calls that a
conflict, every shared container becomes a conflict magnet.

**Staying current — the sync protocol.** The schema is pull-based, like the SysML v2 API; the
pieces exist, the service wires them:

| Step | Part | Why |
|---|---|---|
| 1 | **Detect.** The client remembers the head it last synced; poll `SELECT head_commit_id FROM branch WHERE id = :b` | Sub-millisecond indexed read, so a 1–2 s poll is cheap. For push, the API server that won the CAS already knows the new head and can broadcast it (SignalR / WebSocket). PostgreSQL `LISTEN/NOTIFY` only becomes interesting with several API-server instances; the schema has no such trigger today, by choice. |
| 2 | **Walk.** Follow `commit_parent` from the new head back to the client's known head | On a single branch the CAS keeps heads strictly linear (invariant C2), so this is a straight chain — the client's `known..new` range. |
| 3 | **Fetch the change sets** of those commits (below), via `ix_element_version_commit` and `ix_derived_version_commit` | O(sum of change sets), independent of model size. |
| 4 | **Apply locally.** Replace each element by `identity_id`, treat `tombstone = true` as a deletion, overwrite the derived values of every identity in the impact radius | The local model now equals the snapshot at the new head without re-fetching anything else — Axiom 1: identities are stable across versions. |
| 5 | **Fall back** to the two-commit diff (flow 16) or a fresh set read of the branch head (flow 7) | When the client is far behind, or a merge commit (two parents) sits in the range — then the chain is no longer a chain. |

**Change sets — yes, natively.** A commit writes rows only for the elements it changed, one
version per element per commit (spec Clause 7.1.2 invariant 4), which is exactly the spec's
`Commit.change` list of DataVersions. Two queries, both served by the dedicated commit indexes:

```sql
-- what the user changed in commit :c (stored state, tombstones included)
SELECT identity_id, version_id, class_kind, tombstone, stored_json
FROM   sysml2.element_version
WHERE  project_id = :p AND commit_id = :c;

-- what changed as a consequence: the impact radius (qualifiedName, owner, ...)
SELECT identity_id, qualified_name, name, owner, derived_json
FROM   sysml2.derived_version
WHERE  project_id = :p AND commit_id = :c;
```

The second query is the part a naive change set misses (Axiom 2): a client that applies only
the stored rows keeps stale qualified names for everything under a renamed package — the
worked example of guide §16.1, where renaming P writes a derived row for the untouched W.
For an arbitrary pair of commits rather than a chain, flow 16 gives added / removed / changed
by identity (PASS 25a).

Small print: the measured numbers come from pgbench on PG18 on one machine with the smoke
model; real commits carry JSON payloads, subtype and link rows and derived rows, so they are
heavier per commit — the headroom is still two to three orders of magnitude.

---

## 10. Merge two branches

**Use case:** merge `feature` into `main`.

| Step | Part | Why |
|---|---|---|
| 1 | Resolve both heads to snapshots (flow 7) and find the common ancestor | Three-way merge input. |
| 2 | Compute conflicts (same-element edits, containment-aware, cycles, duplicate names) and their resolutions | Guide §15.10, §15.12. Two individually valid branches can merge into an invalid model (e.g. B specializes C on one branch, C specializes B on the other). |
| 3 | Build a commit whose change set **restates every conflict resolution** | Spec invariant 2 (Clause 7.1.2): a merge resolves all conflicts in its own change set. This is what makes "newest ancestor wins" correct: by monotonicity the merge is the newest commit in its ancestry, so its restatement beats both parents (PASS 8a). |
| 4 | Recompute derived state **on the merged snapshot**, not by unioning the two branches' derived rows | Cross-branch interactions (one branch adds a Specialization, the other adds a feature to its target) produce derived changes neither branch saw (guide §15.13). |
| 5 | Commit transaction of flow 4 with **two** `commit_parent` edges (`ordinal` 0 = first parent, 1 = second) | The DAG. `ordinal` preserves "first parent" semantics exactly as Git does. `trg_commit_parent_version` requires all parents in the merge's own release (PASS 11d/11e). |
| 6 | CAS on `main`'s head, overlay upserts for the restated change set | As every commit. |

What the resolver guarantees: elements deleted on a branch that is **not** an ancestor of the
merge stay alive in the merge's snapshot (PASS 8b — the wheel deleted at c3 is alive at c5),
and derived state folds across both parent lineages (PASS 8c). Guide §16.2 walks the example.

What happens if a merge illegally skips a restatement: monotonicity does not order siblings,
so `created DESC` alone would pick nondeterministically. The `id DESC` tiebreaker (audit R13)
makes the answer *stable* — wrong-ish for an illegal input, but testable and identical on every
read and replica (PASS 10a/10b). Restating is the service's obligation; the tiebreaker is only
the safety net.

---

## 11. Tag a commit

**Use case:** `POST /projects/{p}/tags` — "v1.0 is commit c2".

| Step | Part | Why |
|---|---|---|
| 1 | `INSERT tag (id, project_id, name, tagged_commit_id)` | A frozen pointer; FK to `commit` so it can never dangle (PASS 15a/15b). |
| 2 | Read the model at the tag | `get_elements_at_commit(project, tagged_commit_id)` — a historical read, flow 7. |

Tags are immutable (no head to move) and destructible (flow 12). Consider building a checkpoint
on heavily-read tagged commits (release baselines) so historical reads and paging there run
straight off the checkpoint PK (flow 13).

`Branch.Head` and `Tag.TaggedCommit` both redefine `CommitReference.ReferencedCommit` on the
.NET side (`SysML2.NET/PIM/DTO/CommitReference.cs`); the tables mirror that with the same
column shape.

---

## 12. Delete a branch or tag

**Use case:** `DELETE /projects/{p}/branches/{b}`.

The API delete is a **soft delete plus a cache purge** (guide §6.3, normative):

| Step | Part | Why |
|---|---|---|
| 1 | `UPDATE branch SET deleted = now() WHERE id = :b` | `Branch.deleted` is a spec property: deletion is a *recorded event*. The audit story (name, lifetime, final head) survives, and since commits are indestructible the branch's commits stay in the DAG. |
| 2 | `DELETE FROM branch_head WHERE project_id = :p AND branch_id = :b` | The overlay is a rebuildable cache and the only part whose size matters. O(divergence), index-backed by `ix_branch_head_branch`. |
| 3 | The name is immediately reusable | `ux_branch_project_name_live` is partial on `deleted IS NULL`; duplicate *live* names are still rejected (PASS 27a/27b). |

Same protocol for tags (PASS 27c). Hard `DELETE FROM branch` is the administrative purge path,
not the API path. One honest limit: no spec record carries an actor, so *who* deleted is not
auditable at this layer.

---

## 13. Checkpoints: the background maintenance loop

**Use case:** keep reads fast as history grows. Not user-triggered — a service-side cadence
policy (guide §10.1, §15.2), always asynchronous, **never on the commit path**.

| Trigger | Action | Why |
|---|---|---|
| ≥200 commits since the nearest checkpointed ancestor on a lineage, **or** cumulative change-set size since it ≥25% of the model | `SELECT build_commit_checkpoint(project, commit)` | Bounds how far any resolver walks. O(model) each (12–15 s at 1M, measured), hence churn-based, not every-N-commits. |
| A branch is forked | checkpoint the fork base | The overlay invariant: `base_commit_id` must be checkpointed. |
| A conversion commit (flow 18) | checkpoint it | So folds rarely cross a release boundary. |
| A branch's overlay exceeds ~10% of model or ~100k rows | **compaction**: checkpoint the branch head, repoint `base_commit_id`, truncate the overlay — under the branch lock | Keeps set reads and the anti-join small. Must take the same branch lock as commits, or the overlay ends up describing divergence from the wrong base. |
| A checkpoint no branch bases on and the historical ladder does not need | delete it: registry row first, then rows | Each retained 1M-element checkpoint is ~0.2 GB. |

`build_commit_checkpoint` is `INSERT … SELECT FROM resolve_commit_state` plus the registry
row in one statement, `ON CONFLICT DO NOTHING` — idempotent, so two workers building the same
checkpoint merely waste work, and it coexists with live commits because it reads only
immutable history.

Why `commit_checkpoint_registry` exists at all: the resolvers ask "is this commit
checkpointed?" at every recursion step. Probing `commit_checkpoint` for that looked index-
friendly but the planner saw `n_distinct(commit_id) ≈ 1` and chose a seq scan per step — a
single-element historical read took 3.5 s. A one-row-per-checkpoint registry makes the probe a
PK lookup: 1.8–4 ms (guide §10.3). General lesson: an EXISTS probe into a table keyed finer
than the question asked is a statistics trap.

The benchmark replays 2,000 commits with a checkpoint every quarter (`ReplayHistoryAsync`),
then measures reads at various distances from a checkpoint.

---

## 14. Validate the model

**Use case:** "is this commit referentially sound?" — dangling and wrong-type references.

FKs prove a reference targets an **existing identity**. They cannot prove the target is
**alive at commit C**, nor that its metaclass is legal for the referencing property. Both are
answered by generated functions, one `UNION ALL` arm per stored reference column (42), each
reporting `'dangling'` or `'wrong-type'` (guide §7):

| Tier | Function | When | Cost |
|---|---|---|---|
| Incremental | `validate_references_in_commit(project, commit)` | **Every commit**, synchronously, gating acceptance | O(change set): the change set's outgoing references **plus the reverse direction its tombstones break** (a live, unchanged holder left dangling — the case naive validation misses). 77–86 ms for 101 rows vs a 1M project. PASS 13a–13c. |
| Full audit | `validate_references_at_commit(project, commit)` | Periodically (checkpoint cadence is a natural rhythm) as the backstop | O(snapshot × log history): the snapshot is materialized into an ANALYZE'd temp table first so the planner never joins the full history. 2.5–4.3 s at 1M. PASS 12b/12c. |

Deliberately functions, not constraints: the spec allows transiently dangling references, and
cross-project liveness depends on `project_usage` (flow 17). The benchmark runs both tiers on
an authentic 100k-element load (`Reference_validation_is_clean_at_both_tiers`).

Beyond references, the service also owes post-merge **semantic** validation (duplicate names,
specialization / ownership cycles — guide §15.12).

---

## 15. Query the model

**Use case:** `POST /projects/{p}/queries` (store), `GET …/queries/{q}/results` (run), or an
ad-hoc query from the API.

**Storing** (PASS 23a–23c): `INSERT query (…, query_json)` with the spec's own JSON
`{select, where, orderBy, scope}`. Nothing in the database interprets it; it is mutable and
destructible per the API's PUT/DELETE. Storing the raw spec shape keeps it release-agnostic —
it is compiled against the *executing commit's* release at run time.

**Running** (guide §16.4, PASS 24a): the service's query translator resolves each
`PrimitiveConstraint.property` name through the generated **model-version descriptor** (guide
§12.2, the per-release C# that replaced the old `property_catalog` table) and emits SQL over
the snapshot:

| Property routes to | Emitted predicate |
|---|---|
| a stored column on `element_version` (`declaredName`) | `ev.declared_name = $v` |
| a stored column on a subtype table (`isVariation`) | join `usage_version`, `u.is_variation = $v` |
| a **promoted** derived column (`qualifiedName`, `name`, `owner`, …) | join `derived_version`, `dv.qualified_name LIKE $v` — a real btree + statistics |
| any other derived property | `dv.derived_json @> '{…}'` — the GIN fallback |
| a link table (`ownedRelationship`) | `EXISTS (SELECT 1 FROM element_owned_relationship …)` |
| the metaclass itself | `ev.class_kind = <ClassKind.PartUsage>` |

The row source is `get_elements_at_branch_head` (inlined, flow 7b) or a resolved commit.
Derived properties are first-class query targets precisely because they were precomputed at
commit time (Clause 2 full conformance).

Two ready-made shapes the smoke test proves: `GET /roots` is a query over the promoted
`owner IS NULL` column (PASS 17); "which relationships reference element X" is a reverse
lookup through `ix_<link>_target` joined to the snapshot (PASS 18a/18b).

---

## 16. Diff two commits

**Use case:** `GET /projects/{p}/commits/{c}/diff?baseCommitId=…`.

A `FULL JOIN` of two resolved snapshots on `identity_id`: present only on one side = added /
removed; present on both with different `version_id` = changed. Between c1 and c2 of the smoke
model only the renamed package differs; the untouched child resolves to the **same** version
row on both sides (PASS 25a). For historical commits that will be diffed often, checkpoint
them first (flow 13).

---

## 17. Use another project (cross-project references)

**Use case:** project A imports a library project B at B's release commit.

| Step | Part | Why |
|---|---|---|
| 1 | `INSERT project_usage (id, project_id = A, used_project_id = B, used_project_commit_id = cB)` | Pins B **at a commit**; FKs guarantee neither the project nor the commit dangles (PASS 16a/16b). |
| 2 | Elements in A reference identities in B directly | `data_identity`'s PK is the bare uuid, not `(project_id, id)` — deliberately, so cross-project references are FK-able (guide §7). |
| 3 | Liveness of B's targets is checked against `cB`, not B's head | Service-side resolution through `project_usage`; the validators type-check cross-project targets via the typed identity. |

Price of the bare-uuid PK (guide §18.3.3): `@id`s are instance-global. Cloning across projects
or importing a kpar with client-supplied ids must mint fresh ids or map the PK violation to a
clear 409. An inbound `project_usage` also blocks deletion of the used project (flow 19).

---

## 18. Upgrade to a new metamodel release

**Use case:** the OMG publishes a new release; some projects move to it, others do not yet.

Multiple releases coexist in one database because the stamp is **per commit** (guide §6.4):
`commit.model_version_id` is the truth of which release a commit's payloads are written in; a
branch "is" the release of its head; `project.target_model_version_id` is the operator's
upgrade *policy* (highest release new commits may use; NULL = unrestricted).

| Step | Part | Why |
|---|---|---|
| 1 | Operator appends the release to `ClassKindRegistry.cs` (new ordinal; new metaclasses after the highest id; dropped classes closed with `removed_in`), regenerates (flow 20), applies the new schema | The physical schema is the **superset** across releases: new subtype tables, new nullable columns, nothing dropped. Seeds are idempotent. |
| 2 | Operator raises `project.target_model_version_id` | Allows branches of that project to upgrade. |
| 3 | The service writes a **conversion commit**: single parent, stamp bumped, restating exactly the elements whose shape changed between releases | Upgrading is a commit, not a migration. O(changed shapes), not O(model): unchanged elements' old rows remain valid under the new release. `trg_commit_parent_version` accepts it (PASS 11b). If the conversion retypes an element, update `data_identity.class_kind` in the same transaction. |
| 4 | Force a checkpoint on the conversion commit | So folds rarely cross a release boundary. |
| 5 | Everything after is a normal commit in the new release | |

What the trigger refuses: downgrades (PASS 11c), merging a branch in release 1 into a head in
release 2 — "convert first, then merge" (PASS 11d), and a commit that converts *and* merges at
once (PASS 11e). The service should turn these into clear errors rather than surfacing the
trigger's exception.

The `ClassKind` enum (`SysML2.NET/Core/AutoGenEnum/ClassKind.cs`) is generated from the same
registry, so .NET and the database agree on ids by construction;
`ClassKindEnumGeneratorTestFixture` fails on drift.

---

## 19. Delete a project

**Use case:** `DELETE /projects/{p}`.

Not a cascade. Identity FKs are `NO ACTION` on purpose: a cascade executes per-row deletes
filtered on the FK column alone, and no index in the schema leads with a bare identity column,
so a cascaded project deletion would seq-scan the largest tables once per identity (audit R3).
Instead, an **ordered, per-table** procedure, each statement pruning to one partition
(documented at the `data_identity` DDL; PASS 26a–26c):

```sql
-- 0. inbound project_usage from OTHER projects must be gone first (26a blocks otherwise)
DELETE FROM sysml2.element_owned_relationship  WHERE project_id = :p;   -- and the other link tables
DELETE FROM sysml2.derived_version             WHERE project_id = :p;
DELETE FROM sysml2.element_version             WHERE project_id = :p;   -- subtype tables cascade from this composite FK
DELETE FROM sysml2.branch_head                 WHERE project_id = :p;
DELETE FROM sysml2.commit_checkpoint           WHERE project_id = :p;
DELETE FROM sysml2.commit_checkpoint_registry  WHERE project_id = :p;   -- NO ACTION FK to commit: skipping this step fails loudly (26b)
DELETE FROM sysml2.data_identity               WHERE project_id = :p;
DELETE FROM sysml2.project                     WHERE id = :p;           -- commit, commit_parent, branch, tag, query cascade from here
```

The remaining `NO ACTION` FKs are the safety net: out-of-order deletion fails loudly instead
of scanning silently. That trade — explicit procedure plus loud guard, instead of convenient
plus catastrophic — is the schema's general FK philosophy.

---

## 20. Change the metamodel: regenerate the schema

**Use case:** the UML XMI changed (new release, or a fix), and the schema must follow.

| Step | Part | Why |
|---|---|---|
| 1 | Edit `Resources/KerML_only_xmi.uml` / `Resources/SysML_only_xmi.uml` | The single source of truth. |
| 2 | Append to `ClassKindRegistry.cs` if metaclasses were added / removed | Ids are frozen forever; the generator **fails** on model ↔ registry drift rather than renumbering. |
| 3 | Run `SQLSchemaGeneratorTestFixture` | Drives `SQLSchemaGenerator.cs` → renders `Templates/Uml/core-sql-schema-2.hbs`: hand-written sections verbatim (PIM, resolvers), `[GENERATED]` sections via the eight helpers in `SqlSchemaHelpers.cs` (enums, registry seeds, link + subtype tables, views, validators, partition list) using the census in `SqlSchemaExtensions.cs`. Output: `Sql/schema2.generated.sql`. |
| 4 | Run `ClassKindEnumGeneratorTestFixture` | Regenerates `ClassKind.cs` from the same registry. |
| 5 | Run the `Integration` fixtures (`SqlSchemaSmokeTestFixture`, `SqlSchemaConcurrencyTestFixture`) | Installs the generated schema on a PostgreSQL 18 Testcontainer and runs the 59-assertion smoke + the concurrency suite. |
| 6 | Run `/sync-schema-guides` | The documentation set (`SQLSCHEMA.md`, both guides, glossaries) and the Schema Atlas artifact must follow the schema. CLAUDE.md makes this mandatory. |

Census rules the generator encodes (and that are easy to get wrong by hand, guide §3): declared
properties are computed as `QueryAllProperties(class)` minus the generals' — never from
`OwnedAttribute`, because association ends are owned by the association; only **same-name**
redefinitions are storage-free; a metaclass gets a subtype table iff it declares ≥1
single-valued stored property.

---

## 21. The test pyramid: which test proves what

All under `SysML2.NET.CodeGenerator.Tests/Generators/UmlHandleBarsGenerators/` unless noted.

| Test | Needs | Proves |
|---|---|---|
| `SQLSchemaGeneratorTestFixture` | nothing | The generator renders from the XMI and the output matches the checked-in `schema2.generated.sql`. |
| `ClassKindEnumGeneratorTestFixture` | nothing | The compiled `ClassKind` enum matches the registry (no drift). |
| `SqlSchemaSmokeTestFixture` (`Integration`) | Docker | Generates the schema in-process, installs it, runs `schema.smoke.sql`; the expected PASS count is read from the script itself. **Every flow in this document, end to end.** |
| `SqlSchemaConcurrencyTestFixture` (`Integration`) | Docker | Flows 9 and 9b under real concurrency: hot-branch race, spread branches, reads under write storm; invariants C1–C5 (one winner per head, linear chains, losers write nothing, overlay coherence). |
| `SqlSchemaBenchmarkTestFixture` (`Benchmark`) | Docker | Flows 4, 5, 7, 7b, 8, 13, 14 at scale with authentic serializer payloads: load throughput, commit latency, checkpoint builds, branch fleet, reads at every distance, keyset-page plan shape, both validation tiers, root-rename burst, UUIDv4 vs v7, bloat. Latencies are reported, invariants asserted. Knobs: `SYSML2_BENCH_ELEMENTS`, `SYSML2_BENCH_COMMITS`, `SYSML2_BENCH_BRANCHES`. |
| `schema.smoke.sql` directly via `psql` | PostgreSQL | Same 59 assertions, also runnable against the hand-written `schema.golden.sql` — both schemas must pass identically. |
| `schema.concurrency.*.sql` via `pgbench` | PostgreSQL | The SQL-only route to the concurrency suite. |
| `SysML2.NET.Serializer.Json.Tests` PIM fixtures | nothing | The .NET records round-trip (multi-parent `previousCommit`, `DataVersion` with null payload = tombstone, `Query.orderBy`, `ProjectUsage.usedProjectCommit`). |

Smoke assertion map, by flow:

| PASS | Flow |
|---|---|
| 1, 2a/2b, 3, 4, 5 | 4–7: create, rename ripple (Axiom 2), tombstone, merged read |
| 6, 14a/14b | commit invariants: monotonicity trigger, immutability |
| 7, 12a | Axiom 1: FK to identity; typed identity |
| 8a–8c, 10a/10b | 10: merge, non-ancestor deletion, tiebreaker |
| 9a–9f | 8, 12, 13: checkpoint, O(1) branch, read-through, tombstone mask, overlay-only delete |
| 11a–11e | 2, 18: idempotent seeds, conversion commit, release rules |
| 12b/12c, 13a–13c | 14: full audit, incremental tier, reverse direction |
| 15a–15c, 27c | 11, 12: tag lifecycle |
| 16a/16b, 26a | 17: project usage |
| 17, 18a/18b, 23a–24a | 15: roots, reverse lookup, stored query lifecycle + execution |
| 19a–22b | PIM record reads: project, branch, commit, change set, DataVersion, catalogs |
| 25a | 16: diff |
| 26a–26c | 19: ordered project deletion |
| 27a/27b | 12: branch soft delete, name reuse |

---

## 22. What the service layer must still build

The schema is deliberately not self-driving. These are the parts the flows above *assume* and
that live above the database (guide §15, in priority order):

1. **The impact-radius engine** (flow 5) — which elements' derived values a change set
   invalidates, recomputed via `SysML2.NET/Extend/*.Compute*`. Design: `IMPACT-RADIUS.md`.
   This is where the system's correctness bugs will live; it deserves the best tests.
2. **The commit transaction + CAS protocol** (flows 4, 9), with `clock_timestamp()` stamping
   and re-stamp-and-retry on microsecond collisions.
3. **Checkpoint cadence, retention, compaction, the base-commit invariant** (flow 13) and the
   **ref soft-delete protocol** (flow 12), all asynchronous.
4. **Three-way merge** with auto-merge of additive collection changes, containment-aware
   conflict detection, subtree-delete completeness, post-merge semantic validation including
   cycle guards, and merge impact radius on the merged state (flows 6, 9, 10).
5. **Reference validation wiring** (flow 14) — incremental per commit, full audit periodically,
   cross-project resolution through `project_usage`.
6. **The query translator** over the generated model-version descriptors (flow 15), and the
   descriptor generator itself.
7. **Release conversion** (flow 18) and the startup assertion that the `class_kind` table
   matches the compiled `ClassKind` enum.
8. **Monitoring** of the operational thresholds (overlay size, checkpoint distance, NULL bases,
   derived burst size, CAS-conflict rate, seq-scan counters, paging inlining) — guide §15.15.
9. **Pagination** anchored to commits with keyset cursors (flow 7b).
10. **Tenant isolation** — none at the database today, by decision; PostgreSQL row-level
    security on `project_id` is the natural hardening step.
11. **Client synchronisation** (flow 9b) — head polling or push, change-set replay by
    commit (stored rows *and* impact-radius rows), automatic rebase on 409 so users never see
    it; falls back to the diff (flow 16) when the range is not a chain.
