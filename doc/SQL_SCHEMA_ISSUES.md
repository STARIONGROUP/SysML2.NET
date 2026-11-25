# Draft issues — SQL schema review (PR #344)

Eight drafts following `.github/ISSUE_TEMPLATE/bug_or_feature.md`, written from `doc/SQL_SCHEMA_REVIEW.md`. All eight have been filed; the bodies below are the drafts as filed, so their line numbers and the `project_usage` table refer to the PR #344 snapshot (`d5a90f2e`). GitHub holds the current state of each issue.

| Draft | Issue | State |
|---|---|---|
| 1 | #409 | open |
| 2 | #410 | open |
| 3 | #411 | open (the `project_usage` item is covered by #415) |
| 4 | #412 | open |
| 5 | #413 | open |
| 6 | #414 | open |
| 7 | #415 | closed — fixed |
| 8 | #416 | open |

---

## Issue 1

**Filed as:** #409 (open)
**Title:** `[Bug]: resolve_commit_state returns stale or deleted elements when a merge reaches a checkpoint`
**Labels:** `bug`, `CodeGeneration`

```markdown
### What type of issue is this?

- [x] Bug report
- [ ] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

`sysml2.resolve_commit_state` and `sysml2.resolve_element_at_commit` (`SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema.hbs`, `schema.generated.sql:1455-1591`) use a checkpoint row only when the history walk found no version of the element. Through a merge, the walk reaches ancestors older than the checkpoint, and their versions override it.

Resolving the merge commit returns:

- an element's older version instead of its newest one;
- elements that were deleted before the checkpoint;
- the same element several times when two checkpoints are reached.

A merge that restates its conflicts does not avoid this: a change made on one lineage before its checkpoint is not a conflict, so the merge never restates it. The cadence policy in the §9 banner ("always at branch-fork bases") makes a walk reach two checkpoints, which produces the duplicate rows.

**Expected:** resolving a commit gives the same result with or without checkpoints.

**Fix:**

- [ ] Store tombstones in `commit_checkpoint`, so a checkpoint is a complete record of that commit's state.
- [ ] Rank checkpoint rows and walked versions together, each by the `created` of the commit that wrote the version, and keep the newest.
- [ ] Apply the same change to `resolve_element_at_commit` and `build_commit_checkpoint`.
- [ ] Add the scenario below to `SysML2.NET.CodeGenerator.Tests/Sql/schema.smoke.sql`.

### Steps to Reproduce

1. Build commits R → Q → X → Y and R → Z, merged into M (parents Y, Z), each strictly newer than its parents.
2. Element f: version f1 at R, f2 at Q. Element g: version g1 at R, tombstone at Q. Element h: version h1 at R.
3. `SELECT * FROM sysml2.resolve_commit_state(p, M);` returns f2, h1, which is correct.
4. `SELECT sysml2.build_commit_checkpoint(p, X);`, then resolve M again: it returns **f1, g1, h1**.
5. `SELECT sysml2.build_commit_checkpoint(p, Z);`, then resolve M again: f and h are each returned **4 times**.

A prototype fix returns f2, h1 in every step and keeps `schema.smoke.sql` at 59/59 PASS.

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11, PostgreSQL 17 (Docker)
- .NET Framework version: .NET 10
- Additional information: none
```

---

## Issue 2

**Filed as:** #410 (open)
**Title:** `[Bug]: SQL schema stores redefined properties in several unsynchronised columns`
**Labels:** `bug`, `CodeGeneration`

```markdown
### What type of issue is this?

- [x] Bug report
- [ ] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

`SqlSchemaExtensions.QueryStoredOwnProperties` (`SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs:74-77`) skips only same-name redefinitions. A redefinition under a new name gets its own column, so one value is stored once per level of the redefinition chain, with nothing keeping the copies equal:

- A `Redefinition`'s target is written to `specialization_version.general`, `subsetting_version.subsetted_feature`, `redefinition_version.redefined_feature` and `relationship_target` (KerML: `redefinedFeature` redefines `subsettedFeature`, which redefines `general`, which redefines `Relationship::target`).
- The same applies to nearly every relationship end redefining `Relationship::source`/`target`: `annotatedElement`, `memberElement`, `importedNamespace`, `Dependency::client`/`supplier` and others.
- `requirement_usage_version.req_id` and `requirement_definition_version.req_id` duplicate `element_version.declared_short_name` (`reqId` redefines `Element::declaredShortName`).

The generated DTOs keep one slot per chain: in `Redefinition`, `ISubsetting.SubsettedFeature`, `ISpecialization.General` and `IRelationship.Target` all read and write `RedefinedFeature`.

### Investigation

The per-kind columns may be deliberate. The §7 banner of `schema.generated.sql` calls the specialization-graph indexes "load-bearing for derived-property impact analysis": typed reverse indexes per relationship kind (`ix_specialization_version_general`, `ix_namespace_import_version_imported_namespace`, `ix_annotation_version_annotated_element`, …) let the service find the elements a change affects without scanning all of `relationship_target`.

Analysis to confirm:

- **Plausibly justified:** one typed column per relationship kind beside the generic `relationship_source` / `relationship_target`, for example `specialization_version.general`/`specific`, `annotation_version.annotated_element`, `namespace_import_version.imported_namespace`.
- **Not justified by that reason:** the deeper levels of a chain. Every FeatureTyping, Subclassification, Subsetting, Redefinition, ReferenceSubsetting and CrossSubsetting already has a `specialization_version` row, so `ix_specialization_version_general` alone covers a walk over the specialization graph. `feature_typing_version.type`, `subclassification_version.superclassifier`, `subsetting_version.subsetted_feature`, `redefinition_version.redefined_feature`, `reference_subsetting_version.referenced_feature`, `cross_subsetting_version.crossed_feature`, `conjugated_port_typing_version.conjugated_port_definition` and `port_conjugation_version.original_port_definition` repeat a column higher up the chain.
- **Not justified:** `req_id`, which repeats `declared_short_name`, an input of the derived `shortName` and `qualifiedName`.
- **Raises the stakes:** an impact analysis walking `ix_specialization_version_general` is complete only if `general` is written for every subtype. Copies that can disagree make derived values silently stale.
- **The DTO argument does not hold:** the generator's doc comment says renamed redefinitions get their own storage "exactly as it does in the generated DTOs". The DTOs alias them instead (`Membership`'s `IRelationship.Target` returns `[this.MemberElement]`). The API payloads do carry both names (`MembershipSerializer` writes `memberElement` and `target`), but the payload is already stored whole in `element_version.stored_json`.
- `SysML2.NET.CodeGenerator.Tests/Sql/schema.golden.sql:702-703` keeps the Specialization endpoints "stored independently" because they *subset* `Relationship::source`/`target`. In KerML they *redefine* them (Hypha, knowledge base 2026-08).

Tasks:

- [ ] For each duplicated column family, record which consumer needs it (impact analysis, `validate_references_*`, views, the Query service).
- [ ] Decide, per duplicated column, whether it stays as a typed index column or becomes a view alias.
- [ ] Record the decision and its reason in `schema.golden.sql` / `SysML2.NET.CodeGenerator.Tests/Sql/SQL.md`.

### Expected

No two stored copies of one value can disagree.

- [ ] Keep the relationship-kind columns the impact analysis needs (`specialization_version.general`/`specific`, the conjugation, disjoining, featuring, annotation, membership and import columns), and enforce equality with `relationship_source` / `relationship_target`, or stop writing those link rows for relationships whose ends are redefined. Document which copy is authoritative.
- [ ] Stop storing the deeper redefinitions; expose them as column aliases in the `vw_*` views (`vw_feature_typing.type` = `general`, and so on).
- [ ] Drop `req_id`; expose it as an alias of `declared_short_name`.
- [ ] Check reference types in `validate_references_*` per `class_kind` on the shared columns.
- [ ] Correct the doc comment in `SqlSchemaExtensions.cs`, which says renamed redefinitions get their own storage "exactly as it does in the generated DTOs", and the "subset" wording in `schema.golden.sql:702-703`.
- [ ] Regenerate `Expected/UML/AutoGenSql/schema.generated.sql`.

### Steps to Reproduce

1. Open `vw_redefinition` in `schema.generated.sql`: it selects `general`, `specific`, `subsetted_feature`, `subsetting_feature`, `redefined_feature` and `redefining_feature` from four tables.
2. Nothing ties these columns together, so a `Redefinition` whose `redefinition_version.redefined_feature` differs from its `specialization_version.general` would be accepted.

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11
- .NET Framework version: .NET 10
- Additional information: metamodel facts checked with Hypha (KerML/SysML knowledge base 2026-08)
```

---

## Issue 3

**Filed as:** #411 (open)
**Title:** `[Bug]: SQL schema accepts versions, refs and checkpoints whose own commit or identity belongs to another project`
**Labels:** `bug`, `CodeGeneration`

```markdown
### What type of issue is this?

- [x] Bug report
- [ ] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

References from an element to an identity in another project are deliberate (§4 of `schema.generated.sql`: `data_identity` has a bare-uuid key so cross-project references stay FK-able) and are out of scope. This issue covers rows that must belong to their own project.

No constraint ties a row's `project_id` to the project of the commit, branch or identity it references (`schema.generated.sql:294-626`, `:1402-1441`). Affected:

- `element_version.commit_id`, and `element_version.project_id` against the project of its own identity (`identity_id`), not of the identities it references
- `commit_parent`: a parent may belong to another project
- `branch.head_commit_id`, `branch.base_commit_id` and `tag.tagged_commit_id`
- `branch_head` and `commit_checkpoint_registry`
- `project_usage`: the PIM requires `usedProject = usedProjectCommit.owningProject` (constraint on `ProjectUsage`, `ptc/25-02-29`); a composite foreign key enforces it at no cost

Tables are partitioned by `project_id`, so a mismatched row is not an error: it silently disappears from the resolvers.

**Expected:** a row's own commit, branch and identity belong to the row's project, while references to elements of a used project stay accepted.

**Fix:**

- [ ] Add `UNIQUE (project_id, id)` on `commit`, `branch` and `data_identity`.
- [ ] Make these foreign keys composite on `(project_id, …)`:
  - `element_version (project_id, commit_id)` → `commit`; `element_version (project_id, identity_id, class_kind)` → `data_identity`
  - `derived_version (project_id, commit_id)` → `commit`; `derived_version (project_id, identity_id)` → `data_identity`
  - `branch (project_id, head_commit_id)`, `branch (project_id, base_commit_id)`, `tag (project_id, tagged_commit_id)` → `commit`
  - `branch_head (project_id, branch_id)` → `branch`; `branch_head (project_id, identity_id)` → `data_identity`
  - `commit_checkpoint (project_id, commit_id)` and `commit_checkpoint_registry (project_id, commit_id)` → `commit`
  - `project_usage (used_project_id, used_project_commit_id)` → `commit (project_id, id)`
- [ ] Enforce same-project parents in `commit_parent`, which carries no `project_id`, through the existing insert trigger or an added column.
- [ ] Leave every reference column single-column (`target_identity` in the link tables, `general`, `member_element`, `owning_relationship`, …), so cross-project references keep working.
- [ ] Add rejection checks to `schema.smoke.sql`, plus one check that a reference to a used project's element is still accepted.

### Steps to Reproduce

1. Create projects P and P2, and a commit C in P.
2. Insert a P2 commit with C as parent in `commit_parent`: accepted.
3. Insert an `element_version` with `project_id = P2`, an identity of P and `commit_id = C`: accepted.

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11, PostgreSQL 17 (Docker)
- .NET Framework version: .NET 10
- Additional information: none
```

---

## Issue 4

**Filed as:** #412 (open)
**Title:** `[Bug]: SQL schema PIM tables do not match the multiplicities of the PIM model`
**Labels:** `bug`, `CodeGeneration`

```markdown
### What type of issue is this?

- [x] Bug report
- [ ] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

The PIM section of the schema (`schema.generated.sql:268-512`) differs from the normative PIM model (`ptc/25-02-29`, `Systems-Modeling-API.xmi`):

| PIM property | Model | Schema |
|---|---|---|
| `Project::name`, `CommitReference::name`, `Query::name` | `[1..1]` (redefines `Record::name`) | nullable |
| `Record::description` | `[1..1]` | nullable on every table |
| `Project::defaultBranch` | `[1..1]` | nullable |
| `Commit::previousCommits` | unordered | `commit_parent.ordinal`, comment at `:272` says ordered |
| `Record::alias` | `[0..*]` | missing |
| `Record::resourceIdentifier` | `[0..1]` on every Record | only on `project` |

`CommitReference::deleted` is `[1..1]` in the model, which cannot hold for a live branch; the nullable column is kept, and the discrepancy belongs to the specification.

The §3 banner of `schema.generated.sql` states that the C# PIM DTOs carry "a multi-valued ordered `PreviousCommit`". The C# types say otherwise (`isOrdered: false`), in line with the model.

**Expected:** PIM tables enforce the PIM multiplicities.

**Fix:**

- [ ] `NOT NULL` on `project.name`, `branch.name`, `tag.name`, `query.name`, `project.default_branch_id`, and `description` on every Record table. Correct the comment at `:498`.
- [ ] Drop `commit_parent.ordinal` (or document it as non-normative) and correct the comment at `:272`.
- [ ] Add `resource_identifier` and an alias table for every Record subclass (Project, Commit, Branch, Tag, Query, DataIdentity, DataVersion).

### Steps to Reproduce

1. `INSERT INTO sysml2.project (id) VALUES (gen_random_uuid());` is accepted with no name, no description and no default branch.

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11
- .NET Framework version: .NET 10
- Additional information: verified against `https://www.omg.org/spec/SystemsModelingAPI/20250201/Systems-Modeling-API.xmi`
```

---

## Issue 5

**Filed as:** #413 (open)
**Title:** `[Bug]: two commits created in one transaction fail the monotonic-commit trigger`
**Labels:** `bug`, `CodeGeneration`

```markdown
### What type of issue is this?

- [x] Bug report
- [ ] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

`commit.created` defaults to `now()` (`schema.generated.sql:297`), which returns the transaction start time. Two commits created in one transaction get identical timestamps, and `trg_commit_parent_monotonic` (`:354-379`) rejects the parent edge between them as not strictly newer.

`SysML2.NET.CodeGenerator.Tests/Sql/schema.concurrency.setup.sql:20` already stamps commits with `clock_timestamp()` for this reason; the schema default contradicts it, and any insert that omits `created` hits the problem.

**Expected:** a parent and child commit created in one transaction get distinct, increasing timestamps.

**Fix:**

- [ ] Change the default to `clock_timestamp()`, or drop the default so the service must supply `created`.
- [ ] Add the case to `schema.smoke.sql`.

### Steps to Reproduce

```sql
BEGIN;
INSERT INTO sysml2.commit (id, project_id, model_version_id) VALUES (:c1, :p, 1), (:c2, :p, 1);
INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES (:c2, :c1, 0);
-- ERROR: commit … is not strictly newer than its parent …
```

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11, PostgreSQL 17 (Docker)
- .NET Framework version: .NET 10
- Additional information: none
```

---

## Issue 6

**Filed as:** #414 (open)
**Title:** `[Bug]: SQL link tables allow duplicate entries in unique collections`
**Labels:** `bug`, `CodeGeneration`

```markdown
### What type of issue is this?

- [x] Bug report
- [ ] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

The 7 stored multi-valued properties are all ordered and unique: `Element::ownedRelationship`, `Element::aliasIds`, `Relationship::ownedRelatedElement`, `Relationship::source`, `Relationship::target`, `Dependency::client` and `Dependency::supplier`. Their link tables (`schema.generated.sql:650-736`) are keyed by `(project_id, version_id, ordinal)` only, so one version can list the same target twice. The link rows can also disagree with the element's `stored_json`.

**Expected:** a target appears at most once per collection per version.

**Fix:**

- [ ] Add `UNIQUE (project_id, version_id, target_identity)` to the six reference link tables, and `UNIQUE (project_id, version_id, value)` to `element_alias_ids`.
- [ ] Generate the constraint from `isUnique` in the template.

### Steps to Reproduce

1. Insert an `element_owned_relationship` row for an existing version with an `ordinal` not yet used and a `target_identity` already listed: accepted.

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11, PostgreSQL 17 (Docker)
- .NET Framework version: .NET 10
- Additional information: uniqueness taken from `Resources/KerML_only_xmi.uml`
```

---

## Issue 7

**Filed as:** #415 (closed — fixed by versioning the three records through the commit stream, schema §7b)
**Title:** `[Feature]: SQL schema storage for ProjectUsageService and ExternalRelationshipService`
**Labels:** `enhancement`, `CodeGeneration`

```markdown
### What type of issue is this?

- [ ] Bug report
- [x] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

Two PIM services (`ptc/25-02-29`) cannot be implemented on the schema:

- **`ProjectUsageService`:** `getProjectUsages(project, commit)` reads at a commit, and `createProjectUsage` / `deleteProjectUsage` take a `branch` and return a new `Commit`. `project_usage` (`schema.generated.sql:486`) has no commit or version column, so usages cannot be read at a commit, and a create or delete is not recorded in any commit.
- **`ExternalRelationshipService`:** five operations, read at a `commit` and written through a `branch`. There is no table for `ExternalRelationship` or `ExternalData`.

The versioning stream (`data_identity`, `element_version`, checkpoints) accepts only KerML/SysML metaclasses through `class_kind`.

**Fix:**

- [ ] Version `ProjectUsage` and `ExternalRelationship` per commit, either through the identity/version stream or through parallel tables with their own history.
- [ ] Add an `ExternalData` table (`resourceIdentifier [1..1]`).
- [ ] Cover each service operation in `schema.smoke.sql`.

### Steps to Reproduce

Not applicable (missing feature).

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11
- .NET Framework version: .NET 10
- Additional information: service signatures taken from `Systems-Modeling-API.xmi`
```

---

## Issue 8

**Filed as:** #416 (open)
**Title:** `[Feature]: efficient branch-head reads for root elements, related relationships and queries`
**Labels:** `enhancement`, `CodeGeneration`

```markdown
### What type of issue is this?

- [ ] Bug report
- [x] Feature request

### Prerequisites

- [x] I have written a descriptive issue title
- [x] I have verified that I am running the latest version of the SysML2.NET
- [x] I have searched [open](https://github.com/STARIONGROUP/SysML2.NET/issues) and [closed](https://github.com/STARIONGROUP/SysML2.NET/issues?q=is%3Aissue+is%3Aclosed) issues to ensure it has not already been reported

### Description

The schema is designed for fast reads of each branch's latest state. Three PIM read operations still cost O(model) per call at a branch head (query shapes from `schema.smoke.sql`; not benchmarked):

- **`getRootElements`:** reads the whole overlay plus checkpoint, then filters on `derived_version.owner IS NULL`. Nothing finds roots directly.
- **`getRelationshipsByRelatedElement`:** no function provides the cheap path (reverse index `ix_relationship_source_target` / `ix_relationship_target_target`, then keep only versions current at the head), and the smoke script resolves the full snapshot instead.
- **`executeQuery` / `executeQueryById`:** the `vw_*` views cover every version in history rather than the branch head, and only `class_kind`, `owning_relationship`, `owner` and `qualified_name` are indexed.

Reads at historical commits are slower by design and are out of scope.

**Fix:**

- [ ] Add a root flag to `branch_head` and `commit_checkpoint`, with a partial index on it.
- [ ] Add a branch-head relationships function built on the reverse indexes.
- [ ] Add branch-head-scoped views or query helpers.
- [ ] Add a case for each to `SqlSchemaBenchmarkTestFixture`.

### Steps to Reproduce

Not applicable (performance feature). See the ROOTS and REVERSE-RELATIONSHIP scenarios in `SysML2.NET.CodeGenerator.Tests/Sql/schema.smoke.sql`.

### System Configuration

- SysML2.NET version: PR #344 (`feat/sql-schema-and-more`, d5a90f2e), `SysML2.NET.CodeGenerator`
- Environment (Operating system, version and so on): Windows 11
- .NET Framework version: .NET 10
- Additional information: none
```
