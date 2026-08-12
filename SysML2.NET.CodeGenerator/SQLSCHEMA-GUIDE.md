# The SysML2.NET PostgreSQL Schema — An Architectural Guide

> **Who this is for.** You know SQL. You want to understand *why* this schema looks the way it
> does — every table, every index, every function, and the reasoning chain that led there.
> This document is the long-form companion to `SysML2.NET.CodeGenerator/SQLSCHEMA.md` (the
> compact reference). Where SQLSCHEMA.md states decisions, this guide *derives* them.
>
> **The artifacts it explains:**
>
> | File | Role |
> |---|---|
> | `SysML2.NET.CodeGenerator/Sql/schema.golden.sql` | Hand-written, annotated reference design |
> | `SysML2.NET.CodeGenerator/Sql/schema2.generated.sql` | Actual generator output (checked in for review) |
> | `SysML2.NET.CodeGenerator/Sql/schema.smoke.sql` | 19-assertion functional test |
> | `SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs` | The Handlebars template that emits the schema |
>
> Section numbers like **§5** refer to the numbered banners inside the schema files themselves.
>
> Een Nederlandse vertaling van deze gids: `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.nl.md`.

---

## Table of contents

1. [The problem being solved](#1-the-problem-being-solved)
2. [The two worlds: element data and PIM data](#2-the-two-worlds-element-data-and-pim-data)
3. [The census: why 77% of the metamodel is not stored](#3-the-census-why-77-of-the-metamodel-is-not-stored)
4. [The two axioms everything follows from](#4-the-two-axioms-everything-follows-from)
5. [Rejected alternatives, and why](#5-rejected-alternatives-and-why)
6. [Layer A — the PIM: projects, commits, branches, tags (§3)](#6-layer-a--the-pim-projects-commits-branches-tags-3)
7. [Identity: `data_identity` and the referential-integrity philosophy (§4)](#7-identity-data_identity-and-the-referential-integrity-philosophy-4)
8. [Layer B — stored element state (§5, §6, §7)](#8-layer-b--stored-element-state-5-6-7)
9. [Layer C — derived element state (§8)](#9-layer-c--derived-element-state-8)
10. [Layer D — snapshot resolution (§9)](#10-layer-d--snapshot-resolution-9)
11. [The read path (§10)](#11-the-read-path-10)
12. [The metamodel catalogs and the Query service (§2, §11)](#12-the-metamodel-catalogs-and-the-query-service-2-11)
13. [Partitioning and physical tuning (§12)](#13-partitioning-and-physical-tuning-12)
14. [The performance audit: war stories with numbers](#14-the-performance-audit-war-stories-with-numbers)
15. [What the service layer still owes the schema](#15-what-the-service-layer-still-owes-the-schema)
16. [Worked examples — following data through the schema](#16-worked-examples--following-data-through-the-schema)
17. [Code generation: what is emitted from the UML model and how](#17-code-generation-what-is-emitted-from-the-uml-model-and-how)
18. [Multi-user and concurrency](#18-multi-user-and-concurrency)
19. [Glossary](#19-glossary)

---

## 1. The problem being solved

This schema is the persistence layer for a **SysML v2 model repository** that implements the
OMG *Systems Modeling API and Services* specification, version 1.0. That one sentence carries
three hard requirements, and each one shapes the schema more than any ordinary CRUD concern:

**Requirement 1 — it stores *models*, not records.** A SysML v2 model is a graph of typed
elements (`PartUsage`, `Membership`, `Specialization`, …) drawn from a metamodel of 175
metaclasses. Elements reference each other densely — ownership trees, type hierarchies,
namespace imports. A "row" here is one element of a systems-engineering model that may contain
a million of them.

**Requirement 2 — it is a *version control system*.** The OMG API is deliberately Git-shaped:
projects contain commits, commits form a directed acyclic graph (merges have multiple parents),
branches are movable pointers into that DAG, tags are frozen ones. Every API read happens *at*
a commit: `GET /projects/{p}/commits/{c}/elements/{e}`. Commits are immutable and indestructible
by specification. This rules out the classic "current-state tables + audit log" shape — history
is not an audit concern here, it *is* the data model.

**Requirement 3 — it must answer with *derived* properties.** This is the requirement that most
people underestimate, and it is the single biggest driver of this design. The SysML v2 metamodel
defines most of its properties as **derived**: computed from other elements by traversal rules
written in OCL. An element's `qualifiedName` is computed by walking its ownership chain to the
root namespace. A type's `feature` set is computed by folding memberships across its whole
specialization hierarchy. The OMG API (Clause 2, "Derived Property Conformance") lets a server
claim one of three levels:

- *no conformance* — never return derived properties;
- *passthrough* — store whatever derived values clients send and echo them back, never compute;
- **full conformance** — every response contains correctly computed, up-to-date derived values,
  and derived properties are usable in query filters.

This schema targets **full conformance with commit-time precomputation**: derived values are
computed once, when a commit is written, and reads just return bytes. Section 9 explains why
that choice (rather than compute-on-read) and what it costs.

Finally, the scale profile the schema is engineered for (confirmed with the project owner):

- **~1 million elements** per project,
- **100–500 concurrently live branches** per project, created and deleted routinely,
- **tens of thousands of commits** per project (years of daily editing),
- **tens to hundreds of projects** sharing one PostgreSQL instance,
- read traffic dominated by *branch-head* element reads and query filters; occasional
  historical reads.

Keep those numbers in mind throughout. Several designs that are perfectly fine at 100k elements
with 5 branches die at this profile, and section 14 shows the measurements.

---

## 2. The two worlds: element data and PIM data

The OMG specification splits its data model into two levels, and the schema mirrors the split.

**The PIM (Platform-Independent Model)** is the *repository machinery*: `Project`, `Commit`,
`Branch`, `Tag`, `DataVersion`, `DataIdentity`, `Query`. These types are defined in Clause 7 of
the API spec, not in the SysML metamodel. There are 16 of them, they are stable (they change
when OMG revises the API, roughly never), and their semantics are subtle (commit DAGs, merge
invariants). They are **hand-written** in the schema (§3) — code-generating 16 stable tables
would add machinery without adding value, and the subtle parts (the monotonicity trigger, the
deletion procedure) need human-written comments anyway.

**Element data** is the actual model content: the 175 metaclasses of KerML + SysML v2. This part
is **generated** from the same UML XMI files (`Resources/KerML_only_xmi.uml`,
`Resources/SysML_only_xmi.uml`) that generate the rest of SysML2.NET — the DTOs, POCOs, JSON
serializers, and so on. When OMG revises the language (they do, regularly), you re-run the
generator and get a schema that matches the new metamodel exactly, with no hand-maintenance of
167 table definitions. Section 17 covers the generation pipeline.

The boundary between the worlds is a single concept: the **DataVersion**. In the spec, a
`DataVersion` wraps an element payload in the context of a commit — "element X had these
contents at commit C". In the schema, that concept is the `element_version` row. The PIM tables
organize *which* versions exist; the element tables record *what* each version contained.

```mermaid
flowchart TB
    subgraph PIM["PIM — repository machinery (hand-written, §3)"]
        project --> commit
        commit --> commit_parent
        project --> branch
        project --> tag
    end
    subgraph ID["Identity (§4)"]
        data_identity
    end
    subgraph STORED["Stored element state (§5–§7, append-only)"]
        element_version --> subtype["47 subtype tables"]
        element_version --> link["7 link tables"]
    end
    subgraph DERIVED["Derived element state (§8, append-only)"]
        derived_version
    end
    subgraph SNAP["Snapshot resolution (§9)"]
        branch_head["branch_head (overlay)"]
        commit_checkpoint
        registry["commit_checkpoint_registry"]
    end
    commit -.->|"one version row per\nchanged element"| element_version
    commit -.->|"one derived row per\nimpacted element"| derived_version
    element_version -->|identity_id| data_identity
    derived_version -->|identity_id| data_identity
    branch --> branch_head
    branch -->|base_commit_id| commit_checkpoint
```

---

## 3. The census: why 77% of the metamodel is not stored

Before a single table was designed, the metamodel was counted. This step mattered more than any
other, because the numbers destroy the intuition you would otherwise design from.

The metamodel, as realized in this repository's generated code, contains:

| Measure | Count |
|---|---|
| Metaclasses | 175 (167 concrete, 8 abstract) |
| Flattened properties across all concrete classes (own + inherited) | 12,963 |
| …of which **stored** (`{ get; set; }` in the DTOs) | 2,698 |
| …of which **derived** (`{ get; internal set; }`) | 9,582 |
| …explicit-interface redefinition aliases (no storage) | 683 |
| Distinct *declarations* behind the 2,698 stored properties | **97, across 49 metaclasses** |
| Distinct stored property *names* | ~80 |
| Widest stored footprint of any single metaclass | **24 columns** (`FlowUsage` and kin) |
| Multi-valued stored reference properties, distinct | **6** (`ownedRelationship`, `ownedRelatedElement`, `source`, `target`, `client`, `supplier`) plus 1 multi-valued string (`aliasIds`) |
| Enumerations | 7, with 19 literals total |

Read those numbers again, because they are the whole game:

**First: the stored surface is tiny.** Twelve thousand flattened properties sounds enormous —
until you notice that only ~2,700 are stored, and those collapse to 97 declarations because
inheritance does the multiplying. `Element` declares 7 stored properties and every one of the
167 concrete classes inherits them; that is 1,169 of the 2,698 right there. The metamodel's
stored core is genuinely small: a handful of booleans, names, one enum here and there, and a
modest set of single-valued references on the relationship metaclasses.

**Second: the derived surface is enormous, and it is not decorative.** 9,582 flattened derived
properties, ~325 distinct names. These are not conveniences — they are the API's primary
vocabulary. `owner`, `qualifiedName`, `ownedElement`, `feature`, `membership`, `documentation` —
every one of them derived, every one of them expected in every API payload under full
conformance. And crucially, the important ones are **recursive**:

- `qualifiedName` walks the ownership chain to the root, consulting sibling names along the way;
- `Type::feature` and `inheritedMembership` fold across the *entire specialization closure* of
  a type (a breadth-first search over `Specialization` edges);
- `Namespace::importedMembership` is a recursive walk over imports, where `Import::isRecursive`
  makes it unbounded;
- `isLibraryElement` walks ownership to check for a library root.

None of these are computable in a single SQL `SELECT`. They need recursive CTEs or materialized
closures — or precomputation, which is the road taken.

**Third: the storage type conflicts are real and force structure.** The metamodel reuses
property names with *different types*: `LiteralBoolean::value` is a Boolean,
`LiteralInteger::value` an Integer, `LiteralRational::value` a Real, `LiteralString::value` a
String — four incompatible SQL types under one name. Likewise `kind` is a *different enum* on
each of `RequirementConstraintMembership`, `StateSubactionMembership`,
`TransitionFeatureMembership`, and `TriggerInvocationExpression`. Any design with one shared
`value` column is dead on arrival. This single fact eliminates "one wide table" (section 5).

**Fourth: inheritance is a DAG, not a tree.** 34 metaclasses have multiple direct supertypes
(up to 3: `FlowUsage` is simultaneously a `ConnectorAsUsage`, a `Flow`, and an `ActionUsage`,
which makes it both a *Feature* and a *Relationship*). Any design that assumes a linear
"join up the parent chain" is also dead on arrival. Deepest chain: 11 levels.

Everything in sections 5–9 is a consequence of these four facts.

### Two traps discovered while counting

The census also surfaced two facts about the UML source that a naive generator gets wrong, and
they are worth recording because they will bite anyone who touches the generator later:

**Trap 1 — association-owned ends.** In UML, a reference property that participates in an
association can be owned by the *association*, not by the class. `Membership::memberElement`,
`Specialization::general`, `FeatureTyping::type` — the load-bearing reference properties of the
entire metamodel — do **not** appear in `IClass.OwnedAttribute`. A generator that reads
`OwnedAttribute` silently produces a `membership_v` table *without the member element column*
(this actually happened during development; 22 of the 47 subtype tables came out wrong). The
correct definition of "declared by class C" is: *flattened properties of C, minus the union of
flattened properties of C's direct generalizations*. See
`SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs`, `QueryStoredOwnProperties`.

**Trap 2 — two kinds of redefinition.** UML property redefinition covers two very different
situations. *Same-name redefinition* (`CollectExpression::operator` redefines
`OperatorExpression::operator`) is a constraint restatement — the redefining property is the
same storage slot, and the DTOs give it no independent field. There are exactly 9 of these.
*New-name redefinition* (`Membership::memberElement` redefines `Relationship::target`) is a
**new API property with its own storage** — the DTOs store both `memberElement` *and* the
inherited `target` list, and API payloads carry both. The storage rule that matches the rest of
SysML2.NET is: only same-name redefinitions are storage-free; they resolve transitively to the
root property's column. (This is the same discriminator that
`SysML2.NET.CodeGenerator/HandleBarHelpers/PropertyHelper.cs` uses for the DTO generator.)

---

## 4. The two axioms everything follows from

If you remember nothing else from this document, remember these two statements. Every
structural decision in the schema is a corollary of one of them.

### Axiom 1 — References name identities, never versions

A SysML element's `@id` is stable across its entire life. When a `FeatureTyping` says "this
feature is typed by element `4ace3d89-…`", it means *whatever that element is at whatever commit
you are looking from* — not "version 17 of that element". Version-independence is the point:
you can retype, rename, and edit the target element for years, and the reference stays valid.

The schema consequence: **every element-to-element reference column is a foreign key to
`data_identity(id)` — never to `element_version`.** There is no FK anywhere in the schema from
one element version to another element version.

This also defines what referential integrity can and cannot mean here. The FK guarantees the
target *identity exists in the database*. It cannot guarantee the target *exists at the commit
you are reading* — an element can legitimately reference something that was deleted on this
branch (that's a dangling reference *in the model*, which is a model-validation concern the
service reports, not a database-integrity violation). Trying to make the database enforce
per-commit reference validity would require FKs into a virtual, computed set — impossible, and
also wrong, because the spec explicitly permits models to be in intermediate states across
commits.

### Axiom 2 — A derived value is a function of (identity, commit), not of (version)

This is subtler and more consequential. Consider:

```
Package "Old"            <- element P, version p1
  └── PartUsage "wheel"  <- element W, version w1, qualifiedName = "Old::wheel"
```

Now commit a rename of the package to `"New"`. The commit's change set contains **one** element:
P (new version p2). W is untouched — no new version, `w1` remains its current stored state on
every branch. And yet W's `qualifiedName` is now `"New::wheel"`.

So: W's derived state changed *without W changing*. A derived value is not a property of a
version — the same version `w1` has `qualifiedName = "Old::wheel"` at commit 1 and
`"New::wheel"` at commit 2. It is a property of the **(identity, snapshot)** pair. The OMG spec
says this in as many words (Clause 2): *"the values of derived properties of a given Element
may be affected by commits that do not directly change that Element."*

The schema consequence: derived state **cannot live on `element_version`**. If it did, the
rename would force writing a new `element_version` row for W (and every other descendant) whose
*stored* half is byte-identical to the old one — you would be versioning elements that did not
change, corrupting the very meaning of "change set", and multiplying stored-state storage by
the impact radius of every rename.

Instead the schema has **two parallel append-only streams**:

- `element_version` — keyed by version; a row exists per *(element, commit-that-changed-it)*;
  immutable; the system of record for stored state.
- `derived_version` — keyed by *(identity, commit)*; a row exists per *(element,
  commit-that-changed-its-derived-state)*; immutable; the read model for derived state.

At the rename commit, the write is: **one** new `element_version` row (for P) and **N + 1**
new `derived_version` rows (for P and every element whose derived values the rename affected —
its "impact radius"). W's stored state is untouched; W's derived state has a new row.

The 19-assertion smoke test (`SysML2.NET.CodeGenerator/Sql/schema.smoke.sql`) makes this exact
scenario its first and central assertion pair (PASS 2a/2b): after the rename, W's
`qualifiedName` resolves to `"New::wheel"` *while W still resolves to its original version row*.
If you ever refactor this schema, keep that test passing — it is the design's load-bearing wall.

---

## 5. Rejected alternatives, and why

Four plausible architectures were considered and rejected. Understanding why they fail
clarifies why the chosen one looks the way it does.

### 5.1 One wide table ("God table")

*One `element` table with a column for every stored property across all metaclasses.*

With ~80 distinct stored names this is superficially tempting — 80 columns is not insane. It
dies on the type-collision fact from the census: `value` must simultaneously be `boolean`,
`integer`, `double precision`, and `text`; `kind` must be four different enum types. You end up
either with typed column families (`value_bool`, `value_int`, …, `kind_req`, `kind_state`, …)
— at which point you have reinvented subtype tables inside one table, badly, with every row
mostly NULL and every CHECK constraint conditional on `class_kind` — or with everything as
`text` and casts everywhere, surrendering type safety in the layer whose entire purpose is to
provide it.

### 5.2 Table-per-metaclass (full TPT, the "COMET shape")

*One table per concrete metaclass (167), plus one link table per multi-valued property
(~230), joined up the inheritance chain for reads.*

This is the shape of the earlier `core-sql-schema.hbs` skeleton this project inherited (a port
from the CDP4-COMET server, which uses it successfully for a different metamodel). It fails
here for three reasons:

1. **The inheritance DAG breaks the join chain.** TPT reads reconstruct an instance by joining
   parent tables up the chain. With 34 multiply-inheriting metaclasses there is no chain — a
   `FlowUsage` read would join up *two* branches of an inheritance diamond. Doable, but every
   query generator now has to understand the DAG.
2. **Scale of machinery vs. scale of content.** 167 + ~230 tables to hold 97 property
   declarations. The overwhelming majority of those tables would contain *only* the `iid`
   column (most metaclasses declare no stored properties of their own — they exist for their
   derived semantics). Deepest reads become 11-way joins.
3. **The COMET shape assumes single-version storage.** Its FKs point at element rows and its
   `revisionNumber` is a monotonic integer — both incompatible with a commit DAG and
   version-independent references (Axioms 1 and 2). This is not a criticism of COMET; its
   problem domain has linear revisions and reference-to-current semantics. This one does not.

### 5.3 Pure generic EAV

*Two tables: `element_version(…, value_data jsonb)` and
`element_reference(version_id, property_id, ordinal, target_identity)`.*

Fastest to generate, and the reference table is genuinely attractive for graph traversal (one
index serves every "who references X?" question). Rejected because it flattens the type system
into data: no per-property FK semantics, no per-property NOT NULL/enum enforcement, no
column statistics for the planner (every property lookup has the same generic selectivity),
and CHECK-level guarantees ("`isParallel` is a boolean") become application discipline. The
chosen design keeps a *narrow* EAV-ish surface where it is justified (the 7 link tables, the
property catalog) without giving up typed columns for the scalar core.

### 5.4 Document store (jsonb-only)

*Store each element version as one jsonb document; index with GIN.*

This handles reads beautifully — and in fact the chosen design *contains* this design as its
read path (`stored_json`/`derived_json`). Rejected as the *system of record* because
referential integrity, typed constraints, reverse-reference indexes, and per-column statistics
all vanish; every integrity property of the model would live in application code. The lesson
taken instead: **normalize for writing and integrity, denormalize for reading** — keep both,
in the same rows, written in the same transaction.

### 5.5 What was chosen

**Element core + sparse subtype tables + typed link tables + a second derived stream:**

- one `element_version` core table carrying identity/commit bookkeeping plus `Element`'s own 7
  stored properties (every element has them; splitting them out would be a join for nothing);
- **47 subtype tables**, one per metaclass that *declares* stored scalar properties, keyed by
  `(project_id, version_id)` — an instance of a metaclass has rows in exactly the subtype
  tables of its storage-declaring ancestors (a set, not a chain — the DAG is handled by
  membership, not by joins);
- **7 link tables** for the 6 multi-valued reference properties + `aliasIds`, all ordered
  (`ordinal` in the PK — every one of these is `isOrdered` in the metamodel);
- `derived_version` as the second stream (Axiom 2);
- `stored_json` on the version row and `derived_json` on the derived row as the deliberate,
  transactionally-consistent read denormalization.

The count of 47 is not a design parameter — it falls out of the census (49 storage-declaring
metaclasses, minus `Element` which is folded into the core table, minus `Dependency` whose only
stored properties are the two multi-valued ones that become link tables).

---

## 6. Layer A — the PIM: projects, commits, branches, tags (§3)

### 6.1 The commit DAG

```sql
CREATE TABLE sysml2.commit (
    id             uuid        NOT NULL,
    project_id     uuid        NOT NULL REFERENCES sysml2.project (id) ON DELETE CASCADE,
    created        timestamptz NOT NULL DEFAULT now(),
    description    text        NULL,
    PRIMARY KEY (id)
);

CREATE TABLE sysml2.commit_parent (
    commit_id         uuid     NOT NULL REFERENCES sysml2.commit (id) ON DELETE CASCADE,
    parent_commit_id  uuid     NOT NULL REFERENCES sysml2.commit (id),
    ordinal           smallint NOT NULL,
    PRIMARY KEY (commit_id, parent_commit_id)
);
```

The spec is explicit that `Commit.previousCommit` is a **set** — a merge commit has two or more
parents. Hence the separate `commit_parent` edge table rather than a `previous_commit_id`
column. `ordinal` preserves parent order ("first parent" matters for merge semantics, exactly
as in Git). The commit's PK is the bare uuid because commits are referenced from everywhere
(branches, versions, checkpoints) and are project-scoped via their own `project_id`.

Two spec invariants are worth internalizing because the *resolvers depend on them*:

**Immutability.** *"Commits are immutable… Commits are not destructible"* (Clause 7.1.2). The
schema takes this at face value: nothing ever UPDATEs a commit or an `element_version` row.
Append-only is not an optimization here; it is the spec's own semantics.

**Monotonicity.** *"Version histories must monotonically increase in time: for Commit C, the
value of C.created must be strictly newer than the value of D.created for any commit D in
C.previousCommit."* The schema *enforces* this with a trigger:

```sql
CREATE TRIGGER trg_commit_parent_monotonic
    AFTER INSERT ON sysml2.commit_parent
    FOR EACH ROW
    EXECUTE FUNCTION sysml2.assert_commit_monotonic();
```

Why enforce rather than trust? Because the snapshot resolver (section 10) selects, for each
element, the version from the **newest ancestor commit** — "newest by `created`". If a commit
were ever inserted with a timestamp older than its parent, the resolver would silently return
the *wrong snapshot* — no error, just wrong data. Silent-wrong-answer classes of bug get
triggers; noisy ones can be left to the service layer. (Smoke assertion PASS 6 proves the
trigger fires.)

Note what monotonicity does *not* give you: an ordering between **siblings**. Two commits on
parallel branches may legally share a timestamp. Section 10.4 explains the tiebreaker that
handles this.

### 6.2 How deltas become snapshots — the spec's own algorithm

The spec defines `Commit.change` (the delta: the DataVersions written by this commit) as stored
and `Commit.versionedData` (the full model snapshot at this commit) as **derived**, with an OCL
algorithm that is worth reading because the schema's resolver is its direct translation:

```
let updatedNotDeleted = change->select(payload <> null) in
let updatedIdentities = change.identity in
let retainedWithDuplicates =
    previousCommits.versionedData->select(oldData |
        updatedIdentities->excludes(oldData.identity)) in
let retained = <pick one per identity from retainedWithDuplicates> in
versionedData = updatedNotDeleted->union(retained)
```

In words: a commit's snapshot is *its own changes, plus everything from its parents' snapshots
that it did not override*. Recursion over `previousCommit` bottoms out at the root. Deletions
are DataVersions whose `payload` is null — which the schema stores as `tombstone = true` on the
version row.

This algorithm is correct and hopeless to run per read at scale — it is a fold over the entire
commit history. The whole of §9 (section 10 of this guide) exists to make it cheap.

### 6.3 Branches and tags

```sql
CREATE TABLE sysml2.branch (
    id              uuid        NOT NULL,
    project_id      uuid        NOT NULL REFERENCES sysml2.project (id) ON DELETE CASCADE,
    name            text        NULL,
    description     text        NULL,
    head_commit_id  uuid        NOT NULL REFERENCES sysml2.commit (id),
    base_commit_id  uuid        NULL REFERENCES sysml2.commit (id),   -- see section 10.2
    created         timestamptz NOT NULL DEFAULT now(),
    deleted         timestamptz NULL,
    PRIMARY KEY (id),
    UNIQUE (project_id, name)
);
```

Per the spec's mutability table: branches are mutable and destructible (the *only* mutable
thing in the versioning core — `head_commit_id` moves on every commit); tags are immutable but
destructible; commits are neither. `deleted` is a nullable timestamp rather than a hard delete
because the spec models CommitReference deletion as a recorded event.

`base_commit_id` is a performance structure, not a spec concept — it anchors the branch-head
*overlay* and is explained fully in section 10.2.

Also in this layer: `tag` (same shape as branch, frozen), `project_usage` (cross-project
imports: "project A uses project B at commit C", with the spec constraint
`usedProject = usedProjectCommit.owningProject` left to the service), and `project` itself,
whose `default_branch_id` FK is added *after* `branch` exists and made
`DEFERRABLE INITIALLY DEFERRED` — project and its default branch are created in one
transaction, and the circular FK (project → branch → project) can only be satisfied at commit
time.

---

## 7. Identity: `data_identity` and the referential-integrity philosophy (§4)

```sql
CREATE TABLE sysml2.data_identity (
    id         uuid NOT NULL,
    project_id uuid NOT NULL REFERENCES sysml2.project (id),
    PRIMARY KEY (id)
);
```

Two columns. This tiny table is the anchor of Axiom 1: every element-reference column in the
entire schema — ~30 single-reference columns on subtype tables, 5 reference link tables,
`element_version.owning_relationship`, `branch_head.identity_id`, and so on — is a foreign key
to `data_identity(id)`.

Three deliberate choices here:

**The PK is the bare uuid, not `(project_id, id)`.** `ProjectUsage` lets an element in project
A reference an element in project B. A composite PK would make every cross-project reference
un-FK-able. Project scoping of references is a service-layer validation (via `project_usage`),
not an FK. The cost: `data_identity` cannot be partitioned by project like everything else. The
audit (section 14, finding R12) checked whether that matters at 10⁸ rows — it does not: two
uuid columns make a ~7 GB heap with a btree whose upper levels stay resident; every probe is
3–4 cached page reads. Read-mostly FK targets scale fine unpartitioned.

**`element_id` (the KerML property) is `text`, not `uuid`.** Careful distinction: the *API's*
`@id` is a UUID and maps to `data_identity.id`. But KerML's `Element::elementId` is declared
`String`; only standard-*library* elements are normatively required to use name-based (v5)
UUIDs, and user models carry no format constraint at all. A `uuid` column would reject
spec-valid data. So `element_version.element_id` is `text`, and the identity row's `id` is the
uuid the API layer manages.

**Deletion is explicit, never cascaded.** Originally the identity FKs were `ON DELETE CASCADE`
("delete a project → everything goes"). The performance audit killed this, for a mechanical
reason that generalizes: **a cascade executes per-row deletes filtered on the FK column
alone** — `DELETE FROM element_version WHERE identity_id = $1` — and *no index in this schema
leads with a bare identity column* (they all lead with `project_id`, for partition-locality).
Every cascaded identity would therefore sequentially scan the largest tables, once per
identity, a million times per project deletion. The fix is written into the schema as a
documented procedure at the `data_identity` DDL: project deletion is an *ordered, batched,
per-table* `DELETE … WHERE project_id = $1` (each statement prunes to one partition and uses a
PK prefix), finishing with `data_identity` and `project`. The remaining `NO ACTION` FKs act as
a safety net — they *block* out-of-order deletion loudly instead of scanning silently. That
trade (explicit procedure + loud guard, instead of convenient + catastrophic) is the schema's
general FK philosophy.

---

## 8. Layer B — stored element state (§5, §6, §7)

### 8.1 The core: `element_version` (§5)

```sql
CREATE TABLE sysml2.element_version (
    project_id           uuid       NOT NULL,
    version_id           uuid       NOT NULL,      -- the spec's DataVersion.id
    identity_id          uuid       NOT NULL REFERENCES sysml2.data_identity (id),
    commit_id            uuid       NOT NULL REFERENCES sysml2.commit (id),
    class_kind           smallint   NOT NULL REFERENCES sysml2.class_kind (id),
    tombstone            boolean    NOT NULL DEFAULT false,

    -- Element's own stored properties, folded in:
    element_id           text       NULL,
    declared_name        text       NULL,
    declared_short_name  text       NULL,
    is_implied_included  boolean    NULL,
    owning_relationship  uuid       NULL REFERENCES sysml2.data_identity (id),

    stored_json          jsonb      NULL,

    PRIMARY KEY (project_id, version_id),
    CONSTRAINT element_version_tombstone_empty
        CHECK (NOT tombstone OR (stored_json IS NULL AND element_id IS NULL)),
    CONSTRAINT element_version_payload_present
        CHECK (tombstone OR (stored_json IS NOT NULL AND element_id IS NOT NULL
                             AND is_implied_included IS NOT NULL))
) PARTITION BY HASH (project_id);

CREATE UNIQUE INDEX ux_element_version_identity_commit
    ON sysml2.element_version (project_id, identity_id, commit_id);
```

Design notes, column by column:

- **`project_id` leads every key.** All element-scoped tables are hash-partitioned by
  `project_id` with the same modulus (section 13), and their PKs lead with it. This makes
  every join between them *partition-local* and lets every project-scoped query prune to one
  partition at plan time. The corollary discipline — **never filter these tables on a bare
  uuid without `project_id`** — turned out to matter enormously; section 14 (finding R2) shows
  what happens when you forget.
- **`version_id` is the row's own identity** (the spec's `DataVersion.id`). It is
  app-generated, and the audit recommends UUIDv7 (time-ordered) so each project's inserts land
  on the rightmost btree leaf instead of splattering across the index (finding R8).
- **`class_kind` is a `smallint`, not a name.** 175 metaclass names are interned in the
  `class_kind` catalog (section 12). On the hottest, largest table in the database, that is a
  2-byte column instead of a ~15-byte text one, times hundreds of millions of rows, times its
  presence in indexes.
- **`tombstone` is the deletion marker** — the direct encoding of the spec's "a DataVersion
  with a null payload is a deletion". Deletions are *rows*, because in an append-only commit
  store a deletion is an event in history, not the absence of data. The two CHECK constraints
  make tombstones and payload rows mutually exclusive shapes: a tombstone must be empty, a
  non-tombstone must be complete. These CHECKs are the cheapest possible insurance against the
  service layer writing half-formed rows.
- **The five Element columns are folded in** rather than living in an `element_v` subtype
  table. Rationale: *every* element has them (they are Element's own declarations, and
  everything is an Element), so a separate table would add one join to every single read for
  zero storage benefit. `declared_name` is also the most-filtered stored column, and having it
  on the core table lets query plans avoid the join entirely.
- **`stored_json` is the read-model denormalization** — the element's stored half,
  pre-serialized in exactly the API's JSON shape. The normalized columns and link tables (which
  carry all the FKs and constraints) remain the system of record; `stored_json` exists so that
  serving an element never requires reassembling it from up to six subtype tables and three
  link tables. It is written in the same transaction as the normalized rows, so it cannot
  drift. Cost: roughly doubles `element_version` storage (mitigated by lz4 — section 13). The
  smoke test's PASS 4 verifies the concatenated read path produces a complete payload.
- **`ux_element_version_identity_commit`** enforces the spec invariant *"DataVersion.identity
  is unique among records listed in Commit.change"* — one version of an element per commit —
  and simultaneously serves as the index for "give me element X's row at commit C", which the
  single-element resolver leans on.

### 8.2 The link tables (§6)

The census found exactly six multi-valued stored reference properties in the whole metamodel,
plus one multi-valued string. Each becomes one table; all are ordered:

```sql
CREATE TABLE sysml2.element_owned_relationship (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_element_owned_relationship_target
    ON sysml2.element_owned_relationship (project_id, target_identity);
```

(and likewise `relationship_owned_related_element`, `relationship_source`,
`relationship_target`, `dependency_client`, `dependency_supplier`, and `element_alias_ids`
with a `text` value column instead of a reference.)

- `ordinal` is in the PK because every one of these properties is `isOrdered = true` in the
  metamodel — order is model content, not incidental.
- Rows are keyed by **version**, not identity: the collection is part of the element's stored
  state, so a new version carries its own collection rows. (This is the one place the audit
  flagged real write amplification — a new version of a 100k-child package re-inserts 100k
  rows even if only its name changed. Section 14, finding R7, documents the content-addressed
  fix that is designed but deliberately deferred until benchmarks demand it.)
- The `target_identity` reverse index is what answers "who references element X?" — the
  building block for reverse navigation, dangling-reference validation, and the derived-state
  impact analysis.
- The FK back to `element_version` is a *composite* on `(project_id, version_id)` and is the
  one cascade kept in this layer: deleting a version row (which only the explicit project
  deletion procedure does) takes its collection rows with it, and the composite FK is
  PK-prefixed on both sides so the cascade is index-backed.

### 8.3 The subtype tables (§7)

One table per storage-declaring metaclass — 47 of them, all with the same skeleton:

```sql
CREATE TABLE sysml2.feature_v (
    project_id   uuid    NOT NULL,
    version_id   uuid    NOT NULL,
    direction    sysml2.feature_direction_kind NULL,
    is_composite boolean NOT NULL DEFAULT false,
    is_constant  boolean NOT NULL DEFAULT false,
    is_derived   boolean NOT NULL DEFAULT false,
    is_end       boolean NOT NULL DEFAULT false,
    is_ordered   boolean NOT NULL DEFAULT false,
    is_portion   boolean NOT NULL DEFAULT false,
    is_unique    boolean NOT NULL DEFAULT true,
    is_variable  boolean NOT NULL DEFAULT false,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);
```

How an instance maps to tables: a `PartUsage` version has rows in `element_version` +
`type_v` + `feature_v` + `usage_v` + `occurrence_usage_v` — the subtype tables of exactly its
storage-declaring ancestors. A `FlowUsage` version has rows in **six** tables including *both*
`feature_v` and `relationship_v`, because `Connector` is simultaneously a Feature and a
Relationship. This is how the inheritance DAG is represented: **membership in a set of tables**,
not a chain of joins. The `class_kind_table` catalog (section 12) records the set per
metaclass so generic code never has to re-derive it.

Details that carry intent:

- **NOT NULL tracks the metamodel's lower bounds.** A `[1..1]` property is NOT NULL; a `[0..1]`
  property (like `direction`, `member_name`, `portion_kind`) is NULL. This is safe *because*
  the table only has rows for instances whose class actually declares the property — the
  sparse-table design is what makes honest NOT NULLs possible at all (contrast the God-table,
  where everything must be nullable).
- **DEFAULTs come from the XMI.** The generator emits a `DEFAULT` for every property whose UML
  declaration carries one: the Feature booleans default false (`is_unique` true),
  `Membership::visibility DEFAULT 'public'`, and — easy to get wrong by intuition —
  `Import::visibility DEFAULT 'private'` (imports are private by default in KerML, unlike
  memberships; a top-level import *must* be private). These defaults were cross-checked against
  the metamodel knowledge base during review.
- **Reference columns FK to `data_identity`** (Axiom 1) and every one gets a reverse-lookup
  index `ix_{table}_{column}`. Two of those indexes deserve a callout:
  `ix_specialization_v_general` and `ix_specialization_v_specific` index the *specialization
  graph* — the edges that derived properties like `Type::feature` fold over. When the service
  computes the impact radius of "a supertype gained a feature", these two indexes are what make
  "find all transitive specializations" affordable.
- **The four `kind` tables and four `literal_*` tables** are the visible proof of the census's
  type-collision fact: `requirement_constraint_membership_v.kind` is
  `sysml2.requirement_constraint_kind` while `state_subaction_membership_v.kind` is
  `sysml2.state_subaction_kind`; `literal_boolean_v.value` is `boolean` while
  `literal_rational_v.value` is `double precision`. One wide table cannot represent this.
- **Redefinitions have no columns.** `CollectExpression` has no subtype table at all — its only
  stored property is the same-name redefinition of `operator`, which lives in
  `operator_expression_v` (its ancestor's table). The property catalog records this resolution
  so query code doesn't need to know it.

### 8.4 The enum types (§1)

```sql
CREATE TYPE sysml2.visibility_kind AS ENUM ('private', 'protected', 'public');
```

Seven native enum types, one per metamodel enumeration. Native enums (vs. text + CHECK) cost 4
bytes, validate on write, and sort in declaration order. The **labels are lowercase** for a
specific reason: they match the JSON wire format byte-for-byte — the generated serializers
write `Direction.Value.ToString().ToLower()` (see
`SysML2.NET.Serializer.Json/Core/AutoGenSerializer/FeatureSerializer.cs`), so a value can flow
from the API payload into the enum column and back out without case mapping at any layer.

---

## 9. Layer C — derived element state (§8)

```sql
CREATE TABLE sysml2.derived_version (
    project_id         uuid    NOT NULL,
    derived_id         uuid    NOT NULL,
    identity_id        uuid    NOT NULL REFERENCES sysml2.data_identity (id),
    commit_id          uuid    NOT NULL REFERENCES sysml2.commit (id),

    -- promoted hot derived properties (declared by Element => present on all 167 metaclasses)
    owner              uuid    NULL REFERENCES sysml2.data_identity (id),
    owning_namespace   uuid    NULL REFERENCES sysml2.data_identity (id),
    qualified_name     text    NULL,
    name               text    NULL,
    short_name         text    NULL,
    is_library_element boolean NOT NULL DEFAULT false,

    derived_json       jsonb   NOT NULL,   -- everything else: ~325 distinct derived names

    PRIMARY KEY (project_id, derived_id)
) PARTITION BY HASH (project_id);

CREATE UNIQUE INDEX ux_derived_version_identity_commit
    ON sysml2.derived_version (project_id, identity_id, commit_id);
CREATE INDEX ix_derived_version_owner          ON sysml2.derived_version (project_id, owner);
CREATE INDEX ix_derived_version_qualified_name ON sysml2.derived_version (project_id, qualified_name);
CREATE INDEX ix_derived_version_json
    ON sysml2.derived_version USING gin (derived_json jsonb_path_ops);
```

### 9.1 Why precompute at commit time at all?

Three strategies were on the table for full derived-property conformance:

**Compute on read.** Zero write cost, no invalidation logic. But every element read pays the
recursive walks (`qualifiedName` = ownership chain; `feature` = specialization closure), every
*collection* read pays them per element, and — decisive — the Query service must filter and
sort on derived properties (`WHERE qualifiedName LIKE 'Vehicle::%' ORDER BY name`), which
means evaluating recursive CTEs per candidate row or building per-property materializations
anyway. Read-dominated workloads (this one, by profile) pay the computation on the hot path,
repeatedly, for values that change rarely.

**Passthrough.** Store client-sent derived values verbatim. Cheapest to build (and the DTO
layer already round-trips derived values). Rejected as the *target* because derived values
become client-trusted data that can silently drift from the model — acceptable as an interim
conformance level, corrosive as an architecture.

**Precompute at commit.** Writes pay for computing the change set's *impact radius*; reads pay
nothing; queries filter real columns. The existing 366 implemented `Compute*` methods in
`SysML2.NET/Extend/` are the computation engine — the .NET code already knows how to evaluate
every OCL derivation against an in-memory model; the service layer's job is to invoke them for
the affected elements at commit time and write the results here. Given the read-dominated
profile and the query requirement, this is the only strategy where the expensive thing happens
once, off the hot path.

The honest cost: **the impact radius is unbounded in the worst case.** Renaming a namespace one
level below the root invalidates `qualifiedName` for nearly the whole model → ~1M
`derived_version` rows in one commit. This is inherent to the spec's semantics (the derived
values genuinely all changed), not to this design; the schema's job is to make the bulk write
survivable (lz4 compression, GIN pending-list tuning, async-friendly append-only shape) and
the audit budgets it as a bulk operation (section 14, finding R5).

### 9.2 Keying: why `(identity, commit)` and why sparse

The key is Axiom 2 made concrete: `derived_version` rows are written **only for elements whose
derived values actually changed at that commit**. A leaf edit writes one row. The rename writes
the subtree. Nothing rewrites rows for unaffected elements — resolution (section 10) finds each
element's *newest derived row at or before the commit being read*, exactly as it does for
stored versions. The two streams resolve through the same fold, which is what keeps the whole
design coherent: one resolution algorithm, two payload halves.

`derived_id` exists (rather than using `(identity_id, commit_id)` as the PK) so that
`branch_head` and `commit_checkpoint` can point at a derived row with a single uuid, keeping
those hot tables narrow.

### 9.3 The promoted six, and the jsonb tail

Six derived properties get real columns; ~319 live in `derived_json`. The six are not
arbitrary: they are declared by `Element` (so they exist for every one of the 167 metaclasses,
making the columns dense, never wasted), and they are the properties a Query service filters
and sorts on constantly — `owner` (containment queries), `qualifiedName` (path lookup), `name`
(sorting/searching), `owning_namespace`, `short_name`, `is_library_element` (excluding library
content from user queries). Real columns mean real btree indexes and real per-column
statistics.

The tail stays jsonb behind a GIN (`jsonb_path_ops`) index, because the spec's Query service
allows a `PrimitiveConstraint` on *any* property, and pre-building 319 expression indexes for
properties that may never be filtered is worse than one containment index. The audit flags the
honest weaknesses (section 14, R5): GIN insertion is the dominant write amplifier during bulk
derived writes, and the index has no `project_id` component, so a probe on a shared partition
rechecks candidates from co-located projects. The standing guidance: promoted columns first,
GIN as fallback; if production telemetry shows the filtered-property set is actually narrow,
replace the whole-document GIN with targeted expression indexes.

---

## 10. Layer D — snapshot resolution (§9)

This layer answers one question: **"what does the model look like at commit C / at the head of
branch B?"** — cheaply, at the profile's scale. It is where the schema earns or loses its
performance, and it went through one major redesign (the overlay) plus one empirical fix (the
registry) during the audit. The final structure has four parts.

### 10.1 `commit_checkpoint` — materialized full folds

```sql
CREATE TABLE sysml2.commit_checkpoint (
    project_id  uuid NOT NULL,
    commit_id   uuid NOT NULL REFERENCES sysml2.commit (id) ON DELETE CASCADE,
    identity_id uuid NOT NULL REFERENCES sysml2.data_identity (id),
    version_id  uuid NOT NULL,
    derived_id  uuid NULL,
    PRIMARY KEY (project_id, commit_id, identity_id)
) PARTITION BY HASH (project_id);
```

A checkpoint is the spec's `versionedData` fold, fully evaluated and stored for one commit: one
row per live element, mapping identity → (version, derived row). `build_commit_checkpoint()`
constructs it from the general resolver, idempotently. Checkpoints bound how far any resolver
ever walks, and they are the *bases* that branch overlays diverge from.

Checkpoints are O(model) each — ~1M rows, on the order of 100 MB — which drives the **cadence
policy** (written into the §9 banner, executed by the service layer): checkpoint a commit when
≥200 commits have accumulated since the nearest checkpointed ancestor *on that lineage*, or
when the cumulative change-set size since it exceeds ~25% of the model, and always at
branch-fork bases. Churn-based, not merely count-based — "every N commits" alone would
accumulate terabytes of near-identical checkpoints on a busy project. Retention: a checkpoint
that no branch bases on and that is not needed for the historical ladder gets deleted (registry
row first, then rows — both PK-prefixed, index-backed deletes).

`build_commit_checkpoint` at 200k elements measured 2.5 s; extrapolated ~12–15 s at 1M — which
is why the banner says, in bold effect: **run it asynchronously, never on the commit path.**

### 10.2 `branch_head` — the sparse overlay

The naive materialization — one `(branch, identity) → version` row per element per branch —
was the original design, and the audit's arithmetic executed it: 500 branches × 1M elements =
**500M rows (~85 GB) per project**, branch creation = copying a million rows into a btree that
is already billions of entries deep, branch deletion = a million-row delete with vacuum churn.
For hundreds of routinely created and deleted branches, that is not a tuning problem; it is the
wrong data structure. The measured comparison (section 14): branch create 2,964 ms full-copy
vs **1.8 ms** overlay, at only one-fifth of target scale.

The overlay inverts the representation: a branch stores only its **divergence** from a base
checkpoint.

```sql
CREATE TABLE sysml2.branch_head (
    project_id   uuid    NOT NULL,
    branch_id    uuid    NOT NULL REFERENCES sysml2.branch (id) ON DELETE CASCADE,
    identity_id  uuid    NOT NULL REFERENCES sysml2.data_identity (id),
    version_id   uuid    NOT NULL,
    derived_id   uuid    NULL,
    is_tombstone boolean NOT NULL DEFAULT false,
    PRIMARY KEY (project_id, branch_id, identity_id)
) PARTITION BY HASH (project_id);

CREATE INDEX ix_branch_head_branch ON sysml2.branch_head (branch_id);
```

The semantics, precisely:

- `branch.base_commit_id` names a **checkpointed** commit (service-enforced invariant — which
  is also why the cadence policy checkpoints fork bases).
- The head state of an element on the branch is: **the overlay row if one exists, else the
  base checkpoint's row.**
- A row with `is_tombstone = true` means "deleted on this branch relative to the base" — it
  *masks* the checkpoint row. (It still points at the tombstone `element_version`, and the
  flag denormalizes `element_version.tombstone` so set-reads can exclude masked identities
  without visiting `element_version` at all.)
- `base_commit_id IS NULL` means the overlay *is* the complete head state — the bootstrap mode
  for a brand-new project before its first checkpoint exists.

Life-cycle costs under the overlay:

| Operation | Work |
|---|---|
| Branch create at a checkpointed commit | insert one `branch` row — **zero** overlay rows |
| Branch create at a non-checkpointed commit | base = nearest checkpointed ancestor; write the (checkpoint → fork) delta into the overlay — O(delta) |
| Commit to the branch | upsert the change-set rows into the overlay (`INSERT … ON CONFLICT (project_id, branch_id, identity_id) DO UPDATE`) — O(changeset) |
| Branch delete | cascade deletes the overlay rows only — O(divergence), index-backed via `ix_branch_head_branch` |
| Compaction (service policy) | when an overlay exceeds ~10% of model or ~100k rows: checkpoint the branch head, repoint `base_commit_id`, truncate the overlay |

Checkpoints are naturally **shared**: the hundreds of branches forked near main's head all base
on the same few checkpoints. That sharing is what fixes the storage arithmetic — total snapshot
storage is governed by the checkpoint cadence, not by branch count.

`ix_branch_head_branch` exists for one precise reason: the `ON DELETE CASCADE` from `branch`
filters on `branch_id` *alone*, and the PK leads with `project_id` — without this index, every
branch deletion sequentially scans every partition (audit finding R3; this is the same
mechanical trap as the identity cascades of section 7, solved oppositely because the
post-overlay table is small enough that the extra index is cheap).

The smoke test's PASS 9a–9f sequence walks the full overlay life cycle: checkpoint build, O(1)
branch creation, read-through to base, tombstone masking, merged set-read, and
overlay-only deletion.

### 10.3 `commit_checkpoint_registry` — a planner lesson

```sql
CREATE TABLE sysml2.commit_checkpoint_registry (
    project_id uuid        NOT NULL,
    commit_id  uuid        NOT NULL REFERENCES sysml2.commit (id),
    created    timestamptz NOT NULL DEFAULT now(),
    PRIMARY KEY (project_id, commit_id)
);
```

One row per checkpoint (not per identity). This table exists because of a measured planner
failure worth understanding in general terms.

The resolvers walk the commit DAG asking, at each step, *"is this commit checkpointed?"*.
Originally that probe was `EXISTS (SELECT 1 FROM commit_checkpoint WHERE project_id = …
AND commit_id = …)` — a PK-prefix probe, obviously index-friendly. Except: all ~200k rows of a
checkpoint share **one** `(project_id, commit_id)` value. The planner's statistics therefore
say `n_distinct(commit_id) ≈ 1`, its selectivity model concludes "any commit_id lookup returns
~all rows", the index scan is costed as returning 200k rows, and it **chooses a sequential
scan** — executed once per recursion step. Measured result: a 500-commit walk filtered 100
million rows (500 × 200k) and touched 1.33M buffers; the "cheap" single-element historical
read took 3.5 seconds.

The structural fix beats any planner coaxing: probe a table whose *shape matches the question*.
"Is this commit checkpointed?" is a question about commits, so the registry has one row per
checkpointed commit, and the probe is a one-row PK lookup no statistics model can
misunderstand. After the fix: the same read measured **1.8–4 ms** (≈1,900× improvement), and
the full-model fold went from 4,012 ms to 185 ms.

The general lesson, worth keeping: *an EXISTS probe into a table keyed by a finer grain than
the question being asked is a statistics trap.* Registries/marker tables are cheap insurance.

### 10.4 The resolvers

Three SQL functions implement the spec's fold. The general one:

```sql
CREATE FUNCTION sysml2.resolve_commit_state(p_project_id uuid, p_commit_id uuid)
RETURNS TABLE (identity_id uuid, version_id uuid, derived_id uuid) ...
```

Its internal CTE pipeline, in plain words:

1. **`checkpoint`** — is the requested commit itself checkpointed? (registry probe)
2. **`ancestry`** — recursive walk over `commit_parent` from the requested commit, marking
   each reached commit `at_checkpoint` via the registry, and **stopping the recursion at
   checkpointed commits** (`WHERE NOT a.at_checkpoint`). The walked window is therefore
   bounded by the cadence policy — ~200 commits, never the whole history.
3. **`folded`** — join the walked commits to `element_version`, take
   `DISTINCT ON (identity_id) … ORDER BY identity_id, created DESC, id DESC`: for each element
   changed *inside the window*, the newest version wins.
4. **`checkpoint_state` / `checkpointed`** — for every element *not* changed in the window,
   take its row from the boundary checkpoint.
5. **`resolved`** — union of 3 and 4, minus tombstones.
6. **`derived_folded`** — the *same* fold, over `derived_version`, with the same window; an
   element whose derived row predates the window falls back to the checkpoint's `derived_id`.
   (This fallback is not decorative — without it, derived state older than the checkpoint
   would silently resolve to NULL.)

Correctness leans on the two commit invariants from section 6.1:

- **"Newest ancestor wins" is sound because of monotonicity** — a commit is strictly newer
  than everything it can reach, so for a merge commit that restates its conflict resolutions
  (which the spec *requires*), the merge's own rows are the newest and win. Smoke PASS 8a–8c
  verify the merge case, including that a deletion on a *non-ancestor* branch correctly does
  not affect the merge's snapshot.
- **The `id DESC` tiebreaker (audit finding R13) handles what monotonicity does not**:
  *sibling* commits may share a timestamp, and if a merge illegally fails to restate a
  conflict, `created DESC` alone would pick between the siblings nondeterministically —
  potentially a different answer per read, per plan, per replica. `id DESC` is arbitrary but
  *stable*: a deterministic wrong-ish answer for an illegal input beats a nondeterministic
  one, because it is testable, cacheable, and consistent across reads. Smoke PASS 10a/10b
  construct the tie and assert the winner twice, through both resolvers.

The single-element variant, `resolve_element_at_commit(project, commit, identity)`, exists
because `GET /projects/{p}/commits/{c}/elements/{e}` would otherwise fold the *entire model*
to answer for one element (audit finding R6). It runs the same ancestry walk but filters both
fold arms to one identity — `ux_element_version_identity_commit` and its derived twin make
each probe an index hit, so the cost is O(walked ancestry): measured 1.8–4 ms at 500 commits
from the checkpoint.

And `build_commit_checkpoint(project, commit)` — `INSERT … SELECT FROM resolve_commit_state`
plus the registry row, in one statement so both become visible atomically, `ON CONFLICT DO
NOTHING` so it is idempotent and safe to re-run.

---

## 11. The read path (§10)

The functions the API layer actually calls. The design goal: **serving an element is a jsonb
concatenation over a handful of PK probes** — no per-read joins across subtype tables, no
recursion, no derived computation.

```sql
-- the hottest query in the system
CREATE FUNCTION sysml2.get_element_at_branch_head(p_branch_id uuid, p_identity_id uuid)
RETURNS jsonb ... AS $$
    SELECT ev.stored_json || COALESCE(dv.derived_json, '{}'::jsonb)
    FROM sysml2.branch b
    LEFT JOIN sysml2.branch_head bh
      ON bh.project_id = b.project_id AND bh.branch_id = b.id AND bh.identity_id = p_identity_id
    LEFT JOIN sysml2.commit_checkpoint cc
      ON cc.project_id = b.project_id AND cc.commit_id = b.base_commit_id
     AND cc.identity_id = p_identity_id AND bh.identity_id IS NULL
    JOIN sysml2.element_version ev
      ON ev.project_id = b.project_id AND ev.version_id = COALESCE(bh.version_id, cc.version_id)
    LEFT JOIN sysml2.derived_version dv
      ON dv.project_id = b.project_id AND dv.derived_id = COALESCE(bh.derived_id, cc.derived_id)
    WHERE b.id = p_branch_id
      AND bh.is_tombstone IS NOT TRUE
      AND NOT ev.tombstone;
$$;
```

Reading it: start from the tiny `branch` table (recovering `project_id` — see below), probe the
overlay; if no overlay row (`bh.identity_id IS NULL` gates the second join), probe the base
checkpoint; whichever won supplies the version and derived pointers; concatenate the two jsonb
halves. A tombstoned overlay row falls out of the WHERE — masking the base. Every access is a
PK probe on one partition.

**The `project_id` discipline, learned the hard way (audit finding R2):** the original version
of this function filtered `branch_head` on `(branch_id, identity_id)` alone. Consequences on
PG16/17: no partition pruning (the predicate lacks the hash key → all 16 leaves visited), and
no PK usage (`branch_id` is the PK's *second* column, and there is no btree skip scan). The
"hottest query in the system" was, silently, the worst one. The fix — joining through `branch`
to recover `project_id` — makes runtime pruning work through the join parameter. The measured
plan shows 15 of 16 partitions "(never executed)" and 0.061 ms execution. The rule generalizes
to every entry point: **any query against a partitioned table whose only keys are bare uuids is
defective by construction in this schema.**

The other functions: `get_elements_at_branch_head` (set read: base checkpoint minus overlaid
identities via anti-join, `UNION ALL` the live overlay — measured 1.24 s for a 200k merge),
`get_elements_at_commit` and `get_element_at_commit` (historical variants over the resolvers).

---

## 12. The metamodel catalogs and the Query service (§2, §11)

### 12.1 `class_kind` and `class_kind_table`

```sql
CREATE TABLE sysml2.class_kind (
    id          smallint NOT NULL,
    name        text     NOT NULL,   -- the API @type, e.g. 'PartUsage'
    is_abstract boolean  NOT NULL,
    PRIMARY KEY (id), UNIQUE (name)
);

CREATE TABLE sysml2.class_kind_table (
    class_kind smallint NOT NULL REFERENCES sysml2.class_kind (id),
    table_name text     NOT NULL,
    ordinal    smallint NOT NULL,    -- shallowest supertype first
    PRIMARY KEY (class_kind, table_name)
);
```

`class_kind` interns the 175 metaclass names (ids assigned deterministically: 1-based position
in the name-ordered list — stable across generator runs for an unchanged metamodel).
`class_kind_table` is the **flattened inheritance DAG**: for each concrete metaclass, exactly
which subtype tables an instance participates in (653 rows total; `FlowUsage` → 6 entries).
Any generic component — a bulk loader, a validator, an admin tool — reads this instead of
re-deriving UML generalization closures.

### 12.2 `property_catalog` — the API-to-storage bridge

```sql
CREATE TABLE sysml2.property_catalog (
    class_kind    smallint NOT NULL REFERENCES sysml2.class_kind (id),
    property_name text     NOT NULL,          -- API name: 'qualifiedName', 'ownedRelationship', ...
    location      sysml2.storage_location NOT NULL,   -- 'column' | 'link_table' | 'derived' | 'alias'
    table_name    text     NULL,
    column_name   text     NULL,
    json_key      text     NULL,
    is_reference  boolean  NOT NULL,
    is_collection boolean  NOT NULL,
    is_ordered    boolean  NOT NULL,
    lower_bound   integer  NOT NULL,
    upper_bound   integer  NOT NULL,          -- -1 = unbounded
    PRIMARY KEY (class_kind, property_name)
);
```

12,113 generated rows — one per (concrete metaclass, API property). This table is what makes
the OMG **Query service** implementable. The spec's query model is:

```
Query { select: [String], where: Constraint, orderBy: [String], scope: [...] }
Constraint = PrimitiveConstraint { property, operator, value, inverse }
           | CompositeConstraint { constraint: [Constraint], operator: and|or }
```

A `PrimitiveConstraint.property` is an API-level *name*. The query translator resolves it
through the catalog:

| Catalog row says | Translator emits |
|---|---|
| `('PartUsage', 'declaredName', 'column', 'element_version', 'declared_name', …)` | `ev.declared_name = $v` |
| `('PartUsage', 'isVariation', 'column', 'usage_v', 'is_variation', …)` | join `usage_v`, `u.is_variation = $v` |
| `('PartUsage', 'qualifiedName', 'derived', 'derived_version', 'qualified_name', …)` | join `derived_version`, `dv.qualified_name = $v` — an indexed column |
| `('PartUsage', 'featuringType', 'derived', 'derived_version', NULL, json_key='featuringType', …)` | `dv.derived_json @> '{"featuringType": …}'` — the GIN fallback |
| `('PartUsage', 'ownedRelationship', 'link_table', 'element_owned_relationship', …)` | `EXISTS (SELECT 1 FROM element_owned_relationship …)` |
| `('CollectExpression', 'operator', 'column', 'operator_expression_v', 'operator', …)` | the redefinition, already resolved to its storage root — the translator never learns UML redefinition rules |

Note that derived properties are first-class query targets — which is exactly what Clause 2's
full conformance demands ("derived properties can be used in Query structures as
PrimitiveConstraint properties … query execution will consider the correctly computed and
up-to-date values"). The commit-time precomputation strategy is what makes this row-source
cheap.

`lower_bound`/`upper_bound`/`is_ordered` ride along so the translator can also validate
constraint shapes and so API metadata endpoints can describe properties without loading the
.NET reflection model.

### 12.3 The flattening views (§11)

One generated view per concrete metaclass reconstructs the DTO's row shape:

```sql
CREATE VIEW sysml2.v_part_usage AS
    SELECT ev.project_id, ev.version_id, ev.identity_id, ev.commit_id,
           ev.element_id, ev.declared_name, ev.declared_short_name,
           ev.is_implied_included, ev.owning_relationship,
           type_v.is_abstract, type_v.is_sufficient,
           feature_v.direction, feature_v.is_composite, /* … */
           usage_v.is_variation,
           occurrence_usage_v.is_individual, occurrence_usage_v.portion_kind
    FROM sysml2.element_version ev
    JOIN sysml2.type_v             USING (project_id, version_id)
    JOIN sysml2.feature_v          USING (project_id, version_id)
    JOIN sysml2.usage_v            USING (project_id, version_id)
    JOIN sysml2.occurrence_usage_v USING (project_id, version_id)
    WHERE ev.class_kind = 94 AND NOT ev.tombstone;
```

These are for the Query service and human inspection — the API element read never touches
them (it serves `stored_json`). They are pass-through views: they expose `project_id`, and the
caller's `WHERE project_id = $1` propagates through the equivalence classes of the USING joins
to prune every joined partitioned table. The audit's ops checklist (R10) applies here: verify
hot plans show pruning; watch for generic-plan flips on the 6-join views
(`plan_cache_mode = force_custom_plan` is the lever); prefer PG17, where fast-path lock slots
scale with `max_locks_per_transaction` — a query touching 6 partitioned relations plus indexes
can otherwise spill into the shared lock manager under high QPS.

---

## 13. Partitioning and physical tuning (§12)

**Hash partitioning by `project_id`, 16-way, co-located across all 58 element-scoped tables.**
The profile says tens-to-hundreds of projects per instance: hash-by-project spreads them across
partitions while keeping each project's rows *together* — every project-scoped query prunes to
one partition, and every `(project_id, version_id)` join between element tables is
partition-local. (For a deployment dominated by one giant project, partitioning is neutral —
one partition holds it; the design does not depend on partitioning for single-project
performance, only for multi-tenant spread. The modulus is a deployment knob.)

Physical decisions that came out of the audit:

- **`max_locks_per_transaction = 4096` is a deployment requirement, not a suggestion.**
  58 partitioned tables × 16 leaves = 928 relations, and PostgreSQL clones every FK onto every
  leaf (2,600+ constraints). Whole-schema DDL — install, migration, `pg_dump --schema-only` —
  takes a lock per object and dies at the default 64 with `ERROR: out of shared memory`. Found
  empirically: the schema does not even install without it. Hot-path queries are unaffected
  (they prune to a handful of relations).
- **Differentiated autovacuum per write profile.** The partition-creation loop applies
  different storage parameters: `branch_head` leaves (the only upsert-heavy table — overlay
  rows update on every commit and die on compaction) get `fillfactor = 90` for HOT-update
  headroom and dead-tuple-driven vacuum; the append-only leaves (`element_version`,
  `derived_version`, subtype, link) get *insert*-driven vacuum
  (`autovacuum_vacuum_insert_threshold = 100000` — keeping the visibility map current for
  index-only scans) and analyze at 50k rows (the original blanket "analyze every 5000 rows"
  would sample a 60M-row leaf continuously during imports).
- **lz4 for the jsonb columns**, set on the parents *before* the partition loop so leaves
  inherit it (verified in `pg_attribute`). At this write volume, pglz compression cost is
  measurable on every commit; lz4 is strictly better here.
- **UUIDv7 for app-generated keys** (`version_id`, `derived_id`): time-ordered uuids turn each
  project's insert pattern from random-page btree scatter into rightmost-leaf appends. A .NET
  note (`Guid.CreateVersion7()`), not a schema change. `identity_id` stays as supplied — it is
  the spec-visible `@id` (and library elements are normatively v5).
- **Bulk import**: the ~3 FK probes per inserted row against a 10⁸-row `data_identity` are
  real but modest; if measurement demands it, the importer path is
  `SET session_replication_role = replica` plus post-import validation queries. The audit
  explicitly *rejected* two tempting alternatives: `DEFERRABLE` FKs (they defer the identical
  per-row work to commit time and balloon the trigger queue — not a bulk-load tool) and
  `NOT VALID` + `VALIDATE` (unsupported on partitioned tables before PostgreSQL 18).

---

## 14. The performance audit: war stories with numbers

The schema was audited against the scale profile and then *measured* — a shape-faithful
synthetic dataset (200k elements, 2,000 commits, checkpoint at 1,500, 100 overlay branches,
one legacy fully-materialized branch for comparison) on PostgreSQL 17 in Docker. Three of the
findings were outright bugs that survived design review and were caught only by adversarial
audit plus empirical runs — which is the meta-lesson of this section.

**The measured table:**

| Operation | Legacy design | Hardened schema |
|---|---|---|
| Branch create | 2,964 ms (copy 200k rows) | **1.8 ms** (overlay) |
| Branch delete | unindexed → seq scans | 34 ms (overlay); 100 ms even for 200k rows (indexed cascade) |
| Single-element head read | all 16 partitions scanned | **0.061 ms**; 15/16 partitions "(never executed)" |
| Single-element historical read (500 commits from checkpoint) | 3,466 ms | **1.8–4 ms** |
| Full-model fold (500 commits from checkpoint) | 4,012 ms | **185 ms** |
| Branch-head set read (200k merge) | — | 1,242 ms |
| `build_commit_checkpoint` (1,500-commit fold × 200k) | — | 2,488 ms (async budget) |

**The findings, compressed** (full table in `SysML2.NET.CodeGenerator/SQLSCHEMA.md`):

- **R1 (SEV-1)** — materialized `branch_head` was O(branches × elements). Fixed by the overlay
  (section 10.2). This was the only *architectural* rework.
- **R2 (SEV-1, bug)** — the hottest read function filtered on bare uuids → no pruning, no PK.
  Fixed by joining through `branch` (section 11). Nothing in the SQL *looked* wrong; only the
  plan shape revealed it.
- **R3 (SEV-1, bug)** — every `ON DELETE CASCADE` was unindexed on the cascade column. Fixed
  by one index + demoting the big-table cascades to explicit procedures (sections 7, 10.2).
- **R-registry (SEV-1, found only by running)** — the checkpoint-existence probe seq-scanned
  per recursion step because `n_distinct = 1` statistics defeat the index. Fixed structurally
  (section 10.3). *Design review cannot catch this class of problem; only `EXPLAIN (ANALYZE,
  BUFFERS)` on realistic data can.*
- **R4 (SEV-2)** — checkpoint cadence is a designed policy with a storage counterweight, not a
  free knob (section 10.1).
- **R5 (SEV-2)** — the derived-burst worst case × GIN write amplification: budgeted as a bulk
  operation; lz4 landed; GIN strategy documented (section 9.3).
- **R6 (SEV-2)** — single-element historical reads needed their own resolver (section 10.4).
- **R7 (SEV-3, deferred)** — link-table write amplification for huge collections; the
  content-addressed collection design (digest-keyed shared collection rows, reused by pointer
  when unchanged) is specified in SQLSCHEMA.md and waits for benchmark evidence, because it
  reshapes generated tables and touches the generator.
- **R8/R9/R10/R11 (SEV-3)** — UUIDv7, autovacuum differentiation, plan-cache/lock-manager ops
  checklist, bulk-import path (section 13).
- **R12 (refuted)** — `data_identity` unpartitioned at 10⁸ rows is fine (section 7).
- **R13 (SEV-4, silent-bug class)** — fold determinism on sibling-timestamp ties (section
  10.4).

The follow-up gate before production, documented in SQLSCHEMA.md: a full .NET benchmark
harness — 3×1M-element projects with authentic serializer payloads sharing partitions, 20k-
commit replay, 500 branches, the root-rename burst measured *concurrently with* read latency,
a UUIDv4-vs-v7 A/B, and longevity checks (`pgstattuple` bloat, wait events, WAL per commit).

---

## 15. What the service layer still owes the schema

The schema is deliberately not self-driving. These responsibilities live above it, and the
design assumes they exist:

1. **Impact-radius analysis** (the hard one). At commit time, compute which elements' derived
   values the change set invalidates, recompute them (the `SysML2.NET/Extend/*.Compute*`
   methods against the in-memory model), and write the `derived_version` rows. A leaf edit
   invalidates one element; a namespace rename invalidates its subtree (`qualifiedName`,
   `qualified` names of members); adding a supertype feature invalidates the specialization-
   descendant closure (`feature`, `membership`, `inheritedMembership`). The reverse-lookup
   indexes (`ix_*_target`, the specialization indexes) exist precisely to make these closures
   computable. This is where the correctness bugs of the whole system will live — it deserves
   the project's best tests.
2. **Checkpoint cadence, retention, and overlay compaction** — the policies of sections 10.1
   and 10.2, executed asynchronously.
3. **The commit transaction discipline**: one transaction writes `commit` + `commit_parent`
   (+ the trigger validates), `element_version` + subtype + link rows, `stored_json`,
   `derived_version` rows, and the `branch_head` overlay upserts, then moves
   `branch.head_commit_id`. Append-only tables make this a pure-insert transaction plus one
   branch-row update.
4. **The base-commit invariant**: never point `branch.base_commit_id` at an uncheckpointed
   commit.
5. **Project deletion** via the ordered explicit procedure (section 7) — never by deleting
   `data_identity` rows first.
6. **Model-level reference validation** (dangling references at a commit) — a query the
   reverse indexes support, but a *validation*, not an FK (Axiom 1).
7. **Merge conflict restatement** — the spec requires merges to restate conflicting elements
   in their own change set; the tiebreaker makes violations deterministic, not correct.

---

## 16. Worked examples — following data through the schema

These mirror the smoke test; running `schema.smoke.sql` and reading its output alongside this
section is the fastest way to internalize the design.

### 16.1 A rename ripples through derived state (Axiom 2 live)

Setup: Package **P** ("Old") owns PartUsage **W** ("wheel"). Commit **c1** creates both.

| Table | Rows after c1 |
|---|---|
| `element_version` | (P, p1, c1, "Old"), (W, w1, c1, "wheel") |
| `derived_version` | (P, c1, qn="Old"), (W, c1, qn="Old::wheel") |

Commit **c2** renames P to "New". The change set is **one element**:

| Table | New rows at c2 |
|---|---|
| `element_version` | (P, p2, c2, "New") — *nothing for W* |
| `derived_version` | (P, c2, qn="New"), **(W, c2, qn="New::wheel")** — W is in the impact radius |

Read W at c2: the stored fold finds w1 (unchanged since c1); the derived fold finds W's c2
row. Payload = `w1.stored_json || derived(c2).derived_json` → `"New::wheel"` with the original
stored content. (PASS 2a/2b.)

### 16.2 A merge, and why "newest wins" is right

History: c1 → c2 (rename P to "New") on main; c1 → c4 (rename P to "Other") on a side branch;
c5 = merge(c2, c4) resolving P to "Merged" *in its own change set*; meanwhile c3 (child of c2,
**not** an ancestor of c5) deleted W.

Resolving c5: ancestry = {c5, c2, c4, c1}. For P, candidates are p1@c1, p2@c2, p_side@c4,
p_merge@c5 — monotonicity makes c5 the newest → "Merged" wins (PASS 8a). For W: only w1@c1 in
the ancestry — the deletion at c3 is invisible because c3 is not an ancestor (PASS 8b). This
is the OCL fold of section 6.2, executed by index.

### 16.3 A branch's life under the overlay

1. `build_commit_checkpoint(project, c2)` → 2 checkpoint rows + 1 registry row (PASS 9a).
2. Create branch b2 with `base_commit_id = c2` → **zero** overlay rows; reading W on b2 serves
   the checkpoint row (PASS 9b/9c).
3. Delete W *on b2 only*: upsert overlay row (b2, W, → tombstone version, `is_tombstone =
   true`). Reading W on b2 now returns nothing — the overlay masks the base (PASS 9d); the set
   read returns 1 element (PASS 9e). Main's view of W is untouched.
4. Delete b2: the cascade removes only the overlay row; the checkpoint — shared with any other
   branch based on c2 — is intact (PASS 9f).

### 16.4 A query translation

*"All PartUsages under `Vehicle` whose `isVariation` is true, ordered by name"* — as a spec
Query: `where = and(PrimitiveConstraint(qualifiedName, like, 'Vehicle::%'),
PrimitiveConstraint(isVariation, =, true))`, `orderBy = [name]`. The translator resolves each
property through `property_catalog` (section 12.2) and emits, over the branch-head state:

```sql
SELECT h.identity_id
FROM sysml2.get_elements_at_branch_head($branch) h          -- or the overlay-merge inline
JOIN sysml2.element_version   ev USING (project_id, version_id)
JOIN sysml2.usage_v           u  USING (project_id, version_id)   -- catalog: isVariation -> usage_v
JOIN sysml2.derived_version   dv ON dv.project_id = ev.project_id AND dv.derived_id = h.derived_id
WHERE ev.class_kind = 94                                          -- catalog: PartUsage
  AND dv.qualified_name LIKE 'Vehicle::%'                         -- catalog: derived, promoted column
  AND u.is_variation
ORDER BY dv.name;
```

Every predicate landed on a real, indexed, statistics-bearing column — the payoff of promoted
derived columns plus the catalog.

---

## 17. Code generation: what is emitted from the UML model and how

The split follows volatility: **hand-written where semantics are subtle and stable
(PIM, versioning, resolvers), generated where the metamodel is large and changes with the
spec** (everything metaclass-shaped).

| Generated section | Source of truth | Emitting helper |
|---|---|---|
| §1 enum types | UML enumerations | `WriteEnumTypes` |
| §2 catalog rows (175 + 653 + 12,113) | classes, generalizations, flattened properties | `WriteClassKindRows`, `WriteClassKindTableRows`, `WritePropertyCatalogRows` |
| §6 link tables | multi-valued stored properties | `WriteLinkTables` |
| §7 subtype tables (47) | scalar stored declarations, bounds, XMI defaults | `WriteSubtypeTables` |
| §11 views (167) | storage-ancestor sets | `WriteFlatteningViews` |
| §12 partition list, §13 model version | table inventory, root package | `WritePartitionedTableArray`, `WriteModelVersion` |

Pipeline: `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/SQLSchemaGenerator.cs`
reads the XMI via uml4net, renders `core-sql-schema-2.hbs` (whose hand-written sections are
kept byte-identical with `schema.golden.sql`), with the census logic in
`SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs` (snake_casing, type mapping,
declared-property computation per the two traps of section 3) and the emitters in
`SysML2.NET.CodeGenerator/HandleBarHelpers/SqlSchemaHelpers.cs`. The generator is driven by
`SysML2.NET.CodeGenerator.Tests/Generators/UmlHandleBarsGenerators/SQLSchemaGeneratorTestFixture.cs`.

Verification loop, end to end: run the fixture → apply `schema2.generated.sql` to PostgreSQL 17
(`max_locks_per_transaction=4096`) → run `schema.smoke.sql` (19 assertions) → both the golden
and the generated schema must pass identically.

---

## 18. Multi-user and concurrency

The short version: **the schema is deliberately concurrency-friendly for readers, funnels all
writer contention into exactly one row per branch, and delegates five real multi-user
responsibilities to the service layer.** This section makes each of those statements precise —
including the one protocol that is *required but not enforced* by the schema.

### 18.1 What the design solves by construction

**Append-only is the concurrency strategy, not just the versioning strategy.**
`element_version`, `derived_version`, `commit`, `commit_parent`, and `commit_checkpoint` are
never UPDATEd or DELETEd in normal operation. Under PostgreSQL's MVCC that has a strong
consequence: readers never block writers, writers never block readers, and two writers can
only conflict where they write the *same row* — and immutable rows are never the same row. A
user reading a model at commit C reads data that *cannot change*: their view is perfectly
repeatable without locks, indefinitely cacheable, and consistent even if a colleague commits
mid-read.

**All mutable state was squeezed into two places on purpose:** `branch.head_commit_id`
(+ `base_commit_id`) and the `branch_head` overlay rows of that branch. Everything else a
commit writes is a pure insert. The *entire* write-conflict surface of a project is therefore
**one `branch` row per branch**. Committers on *different* branches touch disjoint mutable
rows and cannot conflict at all; committers on the *same* branch conflict on exactly one row —
which is correct, because a branch is by definition a serial history. The database contention
mirrors the domain semantics.

**Single-statement reads are tear-proof by construction.** The read functions of §10 join
`branch → overlay → checkpoint` in *one* SQL statement, and one statement in READ COMMITTED
sees one consistent snapshot — a reader can never observe "new base pointer + old overlay"
halfway through a compaction. This property is load-bearing: if the service ever splits that
read into two round-trips (fetch the branch row, then query the overlay), it silently loses
the guarantee. Keep such reads in one statement, or run them under REPEATABLE READ.

**Checkpoint building coexists with everything.** `build_commit_checkpoint()` reads only
immutable history at a fixed commit and writes with `ON CONFLICT DO NOTHING`: two workers
building the same checkpoint merely waste some work; a checkpoint building while users commit
sees a frozen past that new commits cannot alter. This is why the cadence policy can run fully
asynchronously without coordination.

**Plain READ COMMITTED is sufficient — nowhere is SERIALIZABLE needed.** That is a direct
payoff of append-only + the single-mutable-row funnel, and worth protecting when the service
is built.

### 18.2 The required commit protocol (normative for the service layer)

**Concurrent commits to the same branch are a lost-update bug unless the service uses
compare-and-swap on the head.** The failure: users A and B both read `head = c5`, both build
commits with parent c5, both write; the head moves twice and one user's commit becomes
unreachable from the branch — silently. The schema cannot prevent this, because "the parent I
built against" is application state. The protocol (the Git model; the OMG `createCommit`
taking `previousCommit` implies exactly this):

```sql
BEGIN;
-- Option A (optimistic, recommended): CAS on the head
UPDATE sysml2.branch
   SET head_commit_id = :new_commit
 WHERE id = :branch AND head_commit_id = :expected_parent;
-- rowcount 0  =>  someone committed first: ROLLBACK, return 409, client rebases

-- Option B (pessimistic): SELECT ... FOR UPDATE on the branch row at transaction
-- start, serializing committers per branch. Simpler; blocks instead of failing.

-- then, all conflict-free pure inserts:
--   commit + commit_parent (trigger validates monotonicity),
--   element_version + subtype + link rows + stored_json,
--   derived_version rows (the impact radius),
--   branch_head overlay upserts.
COMMIT;
```

Touching the branch row **first** also gives every writer the same lock ordering — deadlock
prevention for free. **Compaction (§10.2) must take the same branch lock**: repointing
`base_commit_id` and clearing the overlay interleaved with a commit's overlay upserts would
leave the overlay describing divergence from the wrong base.

### 18.3 Drawbacks and open decisions

1. **The derived-compute window stretches the critical section.** Derived values must be
   computed against the exact parent snapshot. Inside the branch lock that is trivially
   correct — but a root-namespace rename computes ~1M values, holding the lock for minutes and
   stalling every committer on that branch. The better pattern is optimistic: compute *before*
   locking, CAS, and on failure recompute the (usually tiny) difference and retry. More code,
   and where the subtle bugs will live. Cross-branch there is no issue: derived rows are keyed
   `(identity, commit)`, and different branches produce different commits.
2. **Pagination at HEAD is a multi-user trap.** If page 1 is served at head = c5 and a
   colleague commits before page 2, "read the head again" returns a torn collection. Resolve
   branch → commit once, embed the `commitId` in the page token, paginate against the
   immutable commit. The schema supports this perfectly — that is what commits are *for* — but
   the service must actually do it.
3. **`data_identity`'s bare-uuid PK makes `@id`s instance-global, not project-scoped** — the
   deliberate price for FK-able cross-project references (§7). Consequence: two projects
   cannot contain an element with the same `@id`. Non-event for random v4 ids; real for
   *client-supplied* ids (kpar imports, cross-project cloning, deterministic v5 ids) — the
   second insert fails with a PK violation. The service must mint fresh `@id`s when cloning
   across projects and map the violation to a clear 409 Conflict.
4. **The monotonicity trigger can reject legitimate rapid commits.** `created` must be
   *strictly* newer than every parent; two commits within the same microsecond on one lineage
   (burst automation) are rejected — loudly, by design — so the service needs a
   re-stamp-and-retry, and multi-app-server deployments should let the database assign
   `created` (the `DEFAULT now()`) rather than trusting skewed application clocks.
5. **No row-level security.** Tenant isolation (who may see project X) is entirely
   service-side today — by decision, not omission. PostgreSQL RLS on `project_id` composes
   cleanly with this schema (every element table carries the column) and is the natural
   hardening step if the database is ever exposed to less-trusted components.
6. **Small print.** `UNIQUE (project_id, name)` turns concurrent same-name branch creation
   into a constraint violation (map to 409, fine). GIN pending-list flushes on
   `derived_version` can briefly serialize concurrent derived-heavy commits on a shared
   partition (audit finding R5). `fillfactor = 90` on `branch_head` exists precisely to absorb
   per-commit overlay churn from many concurrently active branches without index bloat.

---

## 19. Glossary

| Term | Meaning here |
|---|---|
| **Identity** | The stable `@id` of an element across its whole life; a `data_identity` row; the only thing references point at (Axiom 1). |
| **Version** | One element's stored state as of one commit; an `element_version` row; immutable. |
| **Tombstone** | A version row marking deletion at a commit (`payload = null` in spec terms). |
| **Derived property** | A metamodel property computed from other elements (77% of the metamodel); lives in `derived_version`, keyed by (identity, commit) — Axiom 2. |
| **Impact radius** | The set of elements whose derived values a change set invalidates; determines which `derived_version` rows a commit writes. |
| **Promoted column** | One of the six derived properties given a real indexed column (`owner`, `qualified_name`, `name`, `short_name`, `owning_namespace`, `is_library_element`). |
| **Fold / snapshot** | The spec's `versionedData` computation: a commit's own changes plus everything inherited from its parents' snapshots. |
| **Checkpoint** | A fully materialized fold for one commit (`commit_checkpoint`), registered in `commit_checkpoint_registry`; bounds resolver walks and bases overlays. |
| **Overlay** | The sparse `branch_head` contents: only the identities on which a branch diverges from its base checkpoint. |
| **Cadence** | The service policy deciding which commits get checkpoints (churn-based: ~200 commits or ~25% model churn, plus fork bases). |
| **Compaction** | Re-basing an overgrown overlay onto a fresh checkpoint at the branch head. |
| **Stored / same-name-redefinition rule** | Only redefinitions under the *same name* are storage-free; new-name redefinitions (e.g. `memberElement` redefining `target`) get their own storage, matching the DTOs. |
| **Storage-declaring metaclass** | A metaclass that declares ≥1 stored scalar of its own → gets a subtype table (47 of them). |
| **Registry** | `commit_checkpoint_registry` — one row per checkpoint, existing solely so existence probes hit a table whose grain matches the question (the `n_distinct` lesson). |
| **Property catalog** | The generated map from every API property name to its physical storage — the bridge that makes the OMG Query service translatable to SQL. |

---

*Companion documents: `SysML2.NET.CodeGenerator/SQLSCHEMA.md` (compact reference, ranked audit
table, benchmark gate); the §-numbered banners inside
`SysML2.NET.CodeGenerator/Sql/schema.golden.sql` (per-object rationale in place);
`SysML2.NET.CodeGenerator/Sql/schema.smoke.sql` (the executable form of sections 4, 10 and 16).*
