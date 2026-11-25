# Review of `schema.generated.sql` (PR #344)

This review describes the schema at PR #344 (`feat/sql-schema-and-more`, `d5a90f2e`). Line numbers refer to `SysML2.NET.CodeGenerator.Tests/Expected/UML/AutoGenSql/schema.generated.sql` at that commit and no longer match the current file. The metamodel was checked against Hypha (knowledge base 2026-08). Defaults and uniqueness came from the repo's own XMI, `Resources/*.uml`, because Hypha doesn't record them. The PIM was checked against the normative PIM model (`ptc/25-02-29`, `Systems-Modeling-API.xmi`, downloaded from OMG). Runtime findings were reproduced on Postgres 17.

## Status

| Finding | Issue | State |
|---|---|---|
| 1. History across a merge | #409 | open |
| 2. Redefined properties stored more than once | #410 | open; the duplicated columns remain, while the guide's trap 2 and the `SqlSchemaExtensions.cs` doc comment no longer claim the DTOs store both |
| 3. Rows pointing into another project | #411 | open; the `project_usage` part is fixed by #415's composite FK onto `commit (id, project_id)` |
| 4. PIM columns vs the PIM model | #412 | open; the missing `ExternalData` / `ExternalRelationship` tables are fixed by #415, and `doc/SQLSCHEMA.md` no longer claims `ProjectUsage : Record` |
| 5. Same-transaction commits rejected | #413 | open |
| 6. Uniqueness on the link tables | #414 | open |
| 7. Two PIM services without storage | #415 | **fixed**: the three records are versioned through the commit stream (schema §7b, guide §6.5) |
| 8. O(model) branch-head reads | #416 | open |
| Low items | — | not filed |

Found after this review: NOT NULL and DEFAULT on columns a subclass redefines as derived (#453, **fixed** with the `ck_` CHECKs of guide §8.3). The smoke script has grown from 59 to 76 assertions.

## Confirmed correct

- **`class_kind`:** 175 metaclasses; exactly 8 are abstract (Element, Relationship, Import, Expose, InstantiationExpression, ControlNode, LoopActionUsage, ConnectorAsUsage).
- **Subtype tables:** no stored attribute is missing. None of the derived ones (`referencingFeature`, `crossingFeature`, `PortConjugation::conjugatedPortDefinition`, `memberElementId`) got a column.
- **Columns:** every default and nullability matches the XMI. (Matching the XMI lower bound was itself wrong where a derived property redefines the column; see #453.)
- **Multi-valued properties:** there are exactly 7 stored ones, and all are ordered.
- **Smoke script:** `schema.smoke.sql` passed 59/59 at the time.

## Critical

### 1. Reading history across a merge returns wrong data (`:1455-1591`)

- **Defect:** `resolve_commit_state` and `resolve_element_at_commit` use a checkpoint row only when the history walk found no version of that element. Through a merge, the walk can reach commits older than the checkpoint.
- **Reproduced:** graph R → Q → X → Y and R → Z, merged into M, with X checkpointed. Element f was changed at Q; element g was deleted at Q.
  - M returns f's version from R instead of Q.
  - The deleted g comes back.
  - With checkpoints at both X and Z, an element is returned four times.

  | Resolving M | Correct result | Generated schema |
  |---|---|---|
  | No checkpoint | f2, h1 | f2, h1 |
  | Checkpoint at X | f2, h1 | f1, g1, h1 |
  | `resolve_element_at_commit` for f | f2 | f1 |
  | Checkpoints at X and Z | each element once | f and h four times each |

- **Fix:** store deletions in `commit_checkpoint` too. Then rank checkpoint rows and walked versions together, each by the commit that actually wrote the version, and keep the newest. A prototype gives the correct result on every row above, and the smoke script still passes 59/59.

### 2. Redefined properties are stored more than once (`:770-1312`)

The generator rule responsible is in `SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs:74-77`.

- **Defect:** almost every relationship end redefines `Relationship::source`/`target`, and some go further down a chain (`redefinedFeature → subsettedFeature → general → target`). The generated DTOs keep one slot per chain. The schema gives each level its own column, so a Redefinition's target is written up to four times (`specialization_version`, `subsetting_version`, `redefinition_version`, `relationship_target`) with nothing keeping the copies equal.
- **Same issue:** `req_id` (`:1299`, `:1308`) redefines `Element::declaredShortName`, yet duplicates `declared_short_name`.
- **Guide rationale (`doc/SQLSCHEMA-GUIDE.md` §3, Trap 2) is half wrong:** it says the DTOs store both `memberElement` and `target`. They don't: `IRelationship.Target` is an explicit-interface alias, and the same section counts 683 such aliases as having no storage. Payloads do carry both, but `stored_json` already holds the payload.
- **Wrong doc comment:** the generator's comment says renamed redefinitions get their own storage "exactly as it does in the generated DTOs". The DTOs don't do that.
- **Design intent (doc/IMPACT-RADIUS.md, a sketch, not built):** the derived-property impact-radius engine walks typed reverse indexes per relationship kind (`ix_specialization_version_general`, the import and annotation indexes). That justifies one typed column per relationship kind. It doesn't justify the deeper levels: `specialization_version.general` already covers every Specialization subtype. Nor does it justify `req_id`. Copies that can disagree are a direct source of the missed invalidations the engine must avoid.
- **Fix:**
  - Keep the relationship-kind columns, and enforce equality with `relationship_source`/`target`.
  - Turn the deeper redefinitions and `req_id` into view aliases.
  - Check reference types per `class_kind` on the shared columns.

## High

### 3. Rows can point into another project (`:294-626`, `:1402-1441`)

- **Scope:** references to identities in other projects are deliberate (`doc/SQLSCHEMA-GUIDE.md` §7) and out of scope. The defect concerns rows that must belong to their own project.
- **Defect:** nothing checks that a row's `project_id` matches the project of the commit, branch or identity it points to. Affected: `element_version`, `commit_parent`, `branch`, `tag`, `project_usage`, `branch_head`, and `element_version` against its own identity's project.
- **Reproduced:** Postgres accepted a P2 commit whose parent is a P commit. It also accepted a version in P2's partition for an identity that belongs to P.
- **Consequence:** tables are partitioned by project, so a mismatched row isn't an error. It silently disappears from the resolvers.
- **Spec constraint:** for `project_usage`, the PIM states it outright: ProjectUsage carries `usedProject = usedProjectCommit.owningProject`.
- **Fix:** add `UNIQUE (project_id, id)` on `commit`, `branch` and `data_identity`, and make the row's own commit, branch and identity foreign keys composite. Reference columns stay single-column, so cross-project references keep working.

### 4. The PIM columns don't match the PIM model (`:268-512`)

- **Missing `NOT NULL`:**
  - `name` is `Record::name [0..1]`, redefined as `[1..1]` on Project, CommitReference and Query. All three are nullable here; the comment at `:498` calls `name` optional.
  - `Record::description` is `[1..1]`, but nullable on every table.
  - `Project::defaultBranch` is `[1..1]`, but nullable. The deferred foreign key would allow `NOT NULL`.
- **Wrong ordering claim:** `Commit::previousCommits` is unordered, yet `commit_parent.ordinal`, the comment at `:272` and `doc/SQLSCHEMA.md` say it is ordered. `doc/SQLSCHEMA.md` also claims `ProjectUsage : Record`; in the model it has no supertype.
- **Missing Record fields:** `Record::alias [0..*]` is missing everywhere. `Record::resourceIdentifier [0..1]` appears only on `project`, though both belong to every Record subclass (Project, Commit, CommitReference, Query, DataIdentity, DataVersion).
- **Not a schema defect:** `CommitReference::deleted` is `[1..1]` in the model, which cannot hold for a live branch; the nullable `deleted` column is the reasonable reading. This is a question for the specification.
- **Missing tables:** `ExternalData` and `ExternalRelationship` have none.

## Medium

### 5. Same-transaction commits are rejected (`:297`, `:354-379`)

- **Defect:** `created DEFAULT now()` gives every commit in one transaction the same timestamp, so the strict child-newer-than-parent trigger rejects the second commit.
- **Reproduced:** yes.
- **Guide:** `doc/SQLSCHEMA-GUIDE.md` §18.2 already requires `clock_timestamp()`; the schema default contradicts it.
- **Fix:** default to `clock_timestamp()`, or drop the default.

### 6. Uniqueness isn't enforced on the multi-valued tables (`:650-736`)

- **Defect:** all 7 properties are unique, but the link tables are keyed only by `ordinal`, so the same target can appear twice.
- **Fix:** add `UNIQUE (project_id, version_id, target_identity)` (and on `value` for `element_alias_ids`).

## PIM operation coverage

The operation list comes from the normative PIM model (`ptc/25-02-29`, `Systems-Modeling-API.xmi`). "O(model)" means the cost grows with the whole model size, not with the result size.

### 7. Two PIM services have no storage (High)

- **`ProjectUsageService`** (`getProjectUsages(project, commit)`, `createProjectUsage(project, branch, …)`, `deleteProjectUsage`): the getters take a `commit`, and `createProjectUsage` / `deleteProjectUsage` take a `branch` and return a new `Commit`, so usages are versioned per commit. `project_usage` (`:486`) has no commit or version column, so usages can't be read at a commit, and a create or delete isn't recorded in any commit.
- **`ExternalRelationshipService`** (`getExternalRelationships`, `getExternalRelationshipsByElement`, `getExternalRelationshipById`, `createExternalRelationship`, `deleteExternalRelationship`): read at a `commit` and written through a `branch`, like usages, and there's no table at all.
- **Cause:** the versioning stream (`data_identity`, `element_version`, the checkpoints) only accepts KerML/SysML metaclasses through `class_kind`, but the PIM services version usages and external relationships too.
- **Fix:** version both through the identity/version stream, or through parallel tables with their own commit history.

### 8. Three branch-head reads cost O(model) per call (Medium)

The schema is designed for fast reads of each branch's latest state. These three operations miss that goal even at a branch head. They follow the query shapes in `schema.smoke.sql`; none was benchmarked.

- **`getRootElements`:** it reads the whole overlay plus checkpoint, then filters on `derived_version.owner IS NULL`. Nothing finds roots directly, so returning a few roots scans the whole model.
- **`getRelationshipsByRelatedElement`:** the design allows a cheap path, but no function provides it, and the smoke script shows the full-snapshot pattern. The cheap path: look the element up in the `ix_relationship_source_target` / `ix_relationship_target_target` reverse indexes, then keep only relationship versions the overlay or checkpoint marks as current. That costs the number of references to the element, not the model size.
- **`executeQuery` / `executeQueryById`:**
  - The `vw_*` views cover every version in history, not the branch head, so a query must join the full head state.
  - Only `class_kind`, `owning_relationship`, `owner` and `qualified_name` are indexed. A filter on any other property scans the model.
- **Fix:**
  - Add a root flag to `branch_head` and `commit_checkpoint`, with a partial index on it.
  - Add a branch-head relationships function built on the reverse indexes.
  - Add branch-head-scoped views or query helpers.
  - Add a benchmark case for each.

### Accepted by design: reads at historical commits

Reads at a commit that isn't a branch head rebuild the snapshot from the nearest checkpoint, so their cost grows with the model and with history depth. This trade-off is intended and needs no change. It covers:

- `getElements`, `getRootElements` and `getRelationshipsByRelatedElement` at a past commit.
- `diffCommits`.
- `mergeIntoBranch`.
- Telling created from updated in `getCommitChange(…, changeTypes)`.

### Supported efficiently

These are fast through primary keys, the `(project_id, created)` commit index, the branch-head overlay or the checkpoint walk:

- Project, branch and tag CRUD.
- `getHeadCommit`, `getDefaultBranch`, `setDefaultBranch`, `getTaggedCommit`.
- `getCommits`, `getCommitById`, `getCommitChangeById`.
- `createBranch`, `deleteBranch`.
- `getElementById`, at a branch head or at any commit.
- `getElements` at a branch head, paged with keyset paging.
- `createCommit`. Its derived-property writes grow with how far a change reaches; this is inherent and documented.

## Low

- **Redefined defaults aren't carried into the column defaults:** Expose (`isImportAll` true, `visibility` protected), EnumerationDefinition (`isVariation` true), ConnectionDefinition (`isSufficient` true) and `FramedConcernMembership::kind`.
- **`literal_integer_version.value` is a 32-bit `integer`** (`:1236`). KerML's `Integer` is unbounded, so `bigint` or `numeric` would be safer.
- **`data_identity.class_kind` can point at an abstract metaclass** (`:558`). Nothing prevents it.
