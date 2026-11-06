# REST client integration test plan — building a complete SysML v2 model against Flexo

Issue: #407 — *Verify REST Api client against compliant server*.

## 1. Goal

One integration scenario that, through `SysML2.NET.REST` only, **builds a model step by step on a
live Flexo server** until it contains an instance of every KerML / SysML v2 metaclass, and that on
the way **calls every operation of the OpenAPI document** — creating, reading, updating and
deleting both model elements and API objects (projects, branches, tags, commits, queries).

Nothing is pre-seeded: Flexo starts empty and the scenario is the seed. Each step commits the next
part of the model, reads it back, and asserts it against what was sent and against the
specification.

### What is under test

**The system under test is the SysML2.NET REST client** — `SysML2.NET.REST` (`Session`,
`PsmApiClient`, `QueryStringBuilder`) together with the JSON serializer and deserializer it uses
for request and response bodies. A step passes when our client sends what the OpenAPI document
specifies, and turns what the server returns into the right DTOs, including paging, error and
merge-conflict handling.

**Flexo is the counterpart, not the subject.** It is there because the client can only be
checked against a real server; it stands in for any compliant implementation. So:

- Expectations come from the specifications, never from what Flexo happens to do.
- A difference is triaged (§8) and never absorbed by weakening an assertion: either our client is
  wrong (we fix it), or Flexo is (we record it, report it upstream, and keep the scenario running).

### Non-goals

- **Verifying Flexo.** The scenario does not set out to test Flexo's correctness or completeness.
  It covers only the server behaviour our client depends on, and finds Flexo issues only as a side
  effect of that.
- **Claiming conformance** of the client or the serializer. This adds their first write → re-read →
  same-model tests; it does not cover per-invariant metamodel semantics.

## 2. Coverage target and its sources

| Axis | Count | Source of truth |
|---|---:|---|
| Concrete metaclasses | 167 | Hypha `hypha:metamodel-lookup`, release **2026-07**; identical to the 167 classes in `SysML2.NET/Core/AutoGenDto/` |
| Abstract metaclasses | 8 | Hypha 2026-07 — covered through concrete subclasses (Appendix A.2) |
| Non-derived attributes | 97 | Hypha 2026-07 (Appendix A.3) |
| Enumerations / literals | 7 / 21 | Hypha 2026-07 (Appendix A.4) |
| PIM types | 17 + PSM `Error` | `SysML2.NET/PIM/DTO/` (from `ptc/25-02-29`) |
| PIM enumerations | 4 | `Operator` (7), `JoinOperator` (2), `Direction` (3), `ChangeType` (3) |
| API operations | 35 | `Resources/ptc-25-02-30.json` = `IPsmApiClient` (Appendix C) |

Hypha does not cover the API & Services specification: PIM facts come from `ptc/25-02-29` and the
specification PDF, PSM facts from the OpenAPI document (CLAUDE.md, "PIM vs PSM").

The appendices are the progress checklist; the final step (§5, step 14) enforces them at run time.

## 3. Test environment

### 3.1 Flexo host — `Integration/FlexoTestHost.cs`

Testcontainers, three containers on one network, following `TESTING.md` §10 and
`PostgreSqlSchemaTestHost`:

| Container | Image (pinned) | Role |
|---|---|---|
| quad store | `atomgraph/fuseki:4.6` | triplestore, loaded from `cluster.trig` |
| layer1 | `openmbee/flexo-mms-layer1-service:v0.3.0` | Flexo MMS core |
| SysML v2 | `openmbee/flexo-sysmlv2:v0.2.1` | the API under test |

- `cluster.trig` and the env values are copied from the Flexo `v0.2.1` tag into
  `SySML2.NET.REST.Tests/Resources/Flexo/` (Apache-2.0; attribution in `NOTICE`). The file holds
  only users, access control and the metaclass vocabulary — no model data.
- Readiness: poll `GET /projects` until 200. Bootstrap: `PUT {layer1}/orgs/sysmlv2` through a plain
  `HttpClient` — layer1 is not the SysML v2 API and stays outside the client under test.
- Docker unavailable → `Assert.Ignore`. `[Category("Integration")]`, which CI already excludes.
- Fresh containers per run: every run starts from an empty server.

### 3.2 Client wiring

A test `IHttpClientFactory` returns an `HttpClient` whose handler chain is:

1. **`BearerTokenHandler`** — adds `Authorization: Bearer <JWT>`, the token generated in the test
   from the shared HMAC secret (Flexo requires it; `Session` has no auth option — separate issue).
2. **`EndpointRecorder`** — records `(method, path template, status)` of every request by matching
   the URL against the path templates of `Resources/ptc-25-02-30.json`. Step 14 uses it.

The scenario then uses the production entry point unchanged:
`new Session(factory, deSerializer, serializer).Open(flexoUri, …)`.

## 4. How the model is built

### 4.1 Scenario state and the model builder

- **`ScenarioState`** (one per fixture) — server-assigned ids (projects, branches, tags, commits,
  queries), plus a local **expected model**: the DTOs as they should exist at each commit.
- **`ModelBuilder`** — creates DTOs with readable deterministic ids (UUID v5 of
  `"<step>/<name>"`), and wires ownership consistently: owner `ownedRelationship` (ordered) ⇄
  membership `ownedRelatedElement` ⇄ member `owningRelationship`. A step only describes *what* it
  adds; the builder produces the `DataVersionRequest`s for the commit.
- Payloads are `Core.DTO` elements (`Core.DTO.IElement` implements `IDataRequest`). `PsmApiClient`
  serializes them without derived properties, and `ComparerProvider` (`SysML2.NET.Extensions`)
  compares non-derived content — the two agree.

### 4.2 Modelling rules

1. Each step's model extends the previous one: later steps type, specialize, redefine and connect
   what earlier steps created — so a SysML `PartUsage` in step 6 is typed by a `PartDefinition`
   from step 6 and nested in packages from step 2.
2. **Self-contained**: no model-library references (`ScalarValues`, `Parts::Part`, …) — Flexo has no
   libraries loaded. Implied relationships (`isImplied = true`) are not sent.
3. Before writing a step, look every metaclass it adds up with **`hypha:metamodel-lookup`** (owned
   and inherited features, multiplicities, ordering, redefinitions); use the
   **`hypha:metamodel-navigator`** agent for a whole metamodel package; satisfy every lower bound.
4. Write the step's model as textual notation too and validate it with
   **`hypha:sysml-validation`**; attach the report to the review, not to the code.
5. Consult **`hypha:spec-citation`** where well-formedness depends on prose (connector ends,
   conjugation, `namingFeature`).
6. Across the scenario, every attribute in A.3 gets a non-default value at least once, and every
   literal in A.4 appears at least once.

### 4.3 What every model step asserts

After each commit:

- `PostCommitByProjectAsync` returned a commit on the expected branch whose `previousCommits` is
  the previous head; the branch `head` moved (`GetBranchesByProjectAndIdAsync`).
- `GetChangesByProjectCommitAsync` / `GetChangeByProjectCommitIdAsync` → exactly the sent
  `DataVersion`s.
- `GetElementByProjectCommitIdAsync` for each element of the step → equal to the expected DTO under
  `ComparerProvider`.
- `GetElementsByProjectCommitAsync`, paged (`page[size]=25`, following `Link` to the end) → exactly
  the expected id set at that commit, no duplicates.
- `GetRootsByProjectCommitAsync` → only the root package.
- `GetRelationshipsByProjectCommitRelatedElementAsync`, `Direction` `in` / `out` / `both`, for the
  step's relationship ends.
- History is immutable: one element of an earlier commit is re-read at that commit and is unchanged.

Comparisons over a list of elements run inside `Assert.EnterMultipleScope`, so one run reports
every failing metaclass, not just the first.

## 5. The scenario

`FlexoModelScenarioTestFixture` — `[Category("Integration")]`, one `[Test]` per step ordered with
`[Order(n)]` (precedent: `SqlSchemaBenchmarkTestFixture`). `[SetUp]` ignores a step when an
earlier step failed (`Assert.Ignore("step N failed")`), so a failure is reported once, at its
origin.

| # | Step | Model content | Operations exercised (Appendix C #) |
|---:|---|---|---|
| 1 | **Empty server** | — | `GET /meta/datatypes` (34), by id (35); `GET /projects` is empty (1); unknown project → 404 `Error` (3) |
| 2 | **Project and root model** | `POST`/`GET`/`PUT` project (2, 3, 4, 1); commit on the default branch: root `Package`, nested `Package`s, `Namespace`, `LibraryPackage`, `AnnotatingElement`, `Comment`, `Documentation`, `TextualRepresentation`, `Annotation`, `OwningMembership`, `Membership`, `NamespaceImport`, `MembershipImport`, `Dependency`, `ElementFilterMembership` (Root, Packages — 14) | 15, 14, 16, 17, 18, 19, 20, 22, 23 |
| 3 | **KerML Core** | `Type`, `Classifier`, `Feature`, `Specialization`, `Subclassification`, `FeatureTyping`, `Subsetting`, `Redefinition`, `ReferenceSubsetting`, `CrossSubsetting`, `Conjugation`, `Disjoining`, `Differencing`, `Intersecting`, `Unioning`, `FeatureChaining`, `FeatureInverting`, `TypeFeaturing`, `FeatureMembership`, `EndFeatureMembership`, `Multiplicity` (21) — typing and specializing step-2 content | as step 2 |
| 4 | **KerML Kernel** | data types, classes, structures, associations, connectors, behaviors, steps, functions, all expression and literal kinds, feature values, multiplicity ranges, interactions, flows, metaclasses and metadata (43); then **tag** `kernel` on this commit | as step 2; `POST`/`GET` tags (11, 10, 12) |
| 5 | **Update the KerML model** | U1–U4 (§6.1) on step 2–4 content | 15, 17, 20; `DiffAsync` `kernel` → head shows `UPDATED` (24) |
| 6 | **Branch, SysML structure** | **branch** `systems` from head (7, 6, 8); commits with `branchId`: definitions and usages, attributes, enumerations, occurrences, items, parts, ports (incl. conjugated), connections, interfaces, allocations, flows (31) | 15 with `branchId`; as step 2 on the branch |
| 7 | **SysML behaviour and requirements** | actions (all node and loop kinds), states and transitions, calculations, constraints, requirements, concerns, cases, analysis / verification / use cases, views, viewpoints, renderings, metadata (58) | as step 6 |
| 8 | **Merge** | update the same element on default and on `systems` → merge reports the conflict (`MergeConflictException`, `MergeResult.conflict`); merge again with a resolution body; then a clean merge of the remaining `systems` work; U1–U2 on SysML content after merge | 25, 24, 8 |
| 9 | **Queries** | `POST` a query with nested `CompositeConstraint` (`AND`, `OR`) and `PrimitiveConstraint`s using all 7 `Operator` literals and `inverse`; `select`, `scope`, `orderBy`; list, get, `PUT`, results three ways, at head and at the `kernel` commit; `DELETE` | 27, 26, 28, 29, 31, 32, 33, 30 |
| 10 | **Project usage** | second project `Library` with a small model; the main model commits a `ProjectUsage` of its head; `excludeUsed` on elements and roots | 2, 15, 21, 19, 22 |
| 11 | **External data** | commit `ExternalData` and `ExternalRelationship`, update both, delete both | 15, 17, 18 |
| 12 | **Delete model elements** | per metamodel package, delete the subjects with their owning memberships (`payload = null`), updating owners' `ownedRelationship` in the same commit; the deleted elements are gone at head and readable at the previous commit; `DiffAsync` shows `DELETED` | 15, 19, 20, 24 |
| 13 | **Tear down** | delete tag (13), branch (9), the `Library` project and the main project (5); each is unreadable afterwards; negative cases (§6.3) | 13, 9, 5, 3, 8, 12 |
| 14 | **Coverage verdict** | — | asserts §7 |

Step numbers are fixed in Appendix A.1, which assigns every concrete metaclass to the step that
creates it.

## 6. Update, delete and negative cases

### 6.1 Update scenarios

Every concrete metaclass gets U1 and at least one of U2–U4, in step 5 (KerML) or step 8 (SysML).

| Id | Change | Applies to |
|---|---|---|
| U1 | `declaredName` / `declaredShortName` changed | all 167 |
| U2 | the carried non-derived attribute changed (flip a boolean, another literal value, another enum literal) | every row of A.3 / A.4 |
| U3 | a relationship end retargeted (`FeatureTyping.type`, `Redefinition.redefinedFeature`, …) | the 62 concrete `Relationship` subclasses |
| U4 | an ordered collection reordered, extended and shrunk (`ownedRelationship` of a `Namespace`) | one per metamodel package — ordering survives the round trip |

### 6.2 Delete scenarios

- D1 (step 12): a subject deleted together with its owning membership, the owner updated in the
  same commit — keeps the model well-formed.
- D2 (step 12): a whole nested package's content deleted in one commit.
- D3 (open question, §9): an owner deleted alone. The API specification does not visibly define a
  cascade; read the PIM clause before writing an expectation.

### 6.3 Negative cases (spread over the steps, every operation has at least one)

Unknown project / commit / element / branch / tag / query id; commit to an unknown branch;
malformed commit; merge into an unknown branch; reading a deleted project → `PsmApiException` with
status code and parsed `Error` body.

## 7. Coverage verdict (step 14)

Computed from what the **server returned**, not from what was sent:

- **Metaclasses**: the set of `@type`s read back across all commits = every concrete DTO class in
  `SysML2.NET.Core.DTO` (generated from the XMI, so a metaclass added to the metamodel fails the
  verdict until a step creates it).
- **Attributes**: for every non-derived property (`[Property(isDerived: false)]` on the generated
  DTO interfaces), some element read back carried a non-default value, and some update changed it.
- **Literals**: every enumeration literal was read back at least once.
- **Endpoints**: `EndpointRecorder` saw every `(method, path template)` of
  `Resources/ptc-25-02-30.json` (35), each with at least one success and one failure status.

All four lists are self-calibrating: extending the metamodel or the OpenAPI document never needs a
change to the verdict, only to the steps.

## 8. Handling differences

| Finding | Action |
|---|---|
| Our client or serializer is wrong per the spec | fix here, with a unit test |
| Flexo diverges from the spec | not a failure of the client, so not a failure of this suite: upstream issue on `Open-MBEE/flexo-mms-sysmlv2`; the case goes in one `KnownServerDivergences` table and is reported through `Assert.Warn` quoting the issue, so later steps still run — the expectation stays spec-based |
| The spec is ambiguous | pick a reading, cite the clause, note in one line that it admits more than one (CLAUDE.md) |

Known before starting: Flexo's `/meta/datatypes` serves a pre-final schema (`ItemFlow`,
`LifeClass`, …) — step 1 asserts shape only.

## 9. Open questions

1. **Delete cascade (D3)** — what the PIM says about deleting an owner without its owned elements.
2. **Query by `@type` and subtypes** — Flexo matches subtypes; check whether the spec requires it.
3. **ProjectUsage / ExternalData / ExternalRelationship on Flexo** — stored generically, untested.
4. **Flexo image** — `v0.2.1` (recommended) or `develop-SNAPSHOT`.
5. **CI** — local only (recommended for now), or a separate opt-in workflow.
6. **One `[Test]` per step vs `TESTING.md` §2** — the scenario's unit is the step, not a client
   method; confirm this exception for the integration fixture.

## 10. Work breakdown

| Step | Deliverable | Depends on |
|---|---|---|
| 1 | Branch from `origin/development`; `Testcontainers` `<PackageVersion>`; `FlexoTestHost`, handlers, factory, Flexo resources, `NOTICE` | — |
| 2 | `ScenarioState`, `ModelBuilder`, the step-4.3 read-back assertions; scenario steps 1–2 green | 1 |
| 3 | Step 14 (verdict) — red, listing everything still missing | 2 |
| 4 | Steps 3–4 (KerML), 5 (update) | 2 |
| 5 | Steps 6–8 (SysML, merge) | 4 |
| 6 | Steps 9–13 (queries, usage, external data, delete, teardown) | 2 |
| 7 | Triage (§8): upstream issues, divergence table; verdict green | 3–6 |

Writing the verdict early (deliverable 3) makes it the to-do list: each later deliverable shrinks
its output. Steps 3, 4, 6 and 7 of the scenario are the bulk of the work (153 of the 167
metaclasses).

Per CLAUDE.md: no commits by the agent; the `textual-notation-reviewer` is not required (no
textual-notation paths are touched).

## Appendix A — metamodel coverage (generated from Hypha, release 2026-07)

### A.1 Concrete metaclasses (167)

One row per concrete metaclass. Step = the scenario step (§5) that creates it. Every one is also updated in step 5 or 8 and deleted in step 12.

| Step | Package | Metaclass | Created | Read back | Updated | Deleted |
|:-:|---|---|:-:|:-:|:-:|:-:|
| 3 | KerML::Core::Classifiers | `Classifier` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Classifiers | `Subclassification` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `CrossSubsetting` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `EndFeatureMembership` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `Feature` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `FeatureChaining` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `FeatureInverting` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `FeatureTyping` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `Redefinition` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `ReferenceSubsetting` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `Subsetting` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Features | `TypeFeaturing` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Conjugation` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Differencing` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Disjoining` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `FeatureMembership` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Intersecting` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Multiplicity` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Specialization` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Type` | ☐ | ☐ | ☐ | ☐ |
| 3 | KerML::Core::Types | `Unioning` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Associations | `Association` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Associations | `AssociationStructure` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Behaviors | `Behavior` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Behaviors | `ParameterMembership` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Behaviors | `Step` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Classes | `Class` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Connectors | `BindingConnector` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Connectors | `Connector` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Connectors | `Succession` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::DataTypes | `DataType` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `CollectExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `ConstructorExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `FeatureChainExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `FeatureReferenceExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `IndexExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `InvocationExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `LiteralBoolean` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `LiteralExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `LiteralInfinity` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `LiteralInteger` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `LiteralRational` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `LiteralString` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `MetadataAccessExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `NullExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `OperatorExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Expressions | `SelectExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::FeatureValues | `FeatureValue` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `BooleanExpression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `Expression` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `Function` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `Invariant` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `Predicate` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `ResultExpressionMembership` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Functions | `ReturnParameterMembership` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Interactions | `Flow` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Interactions | `FlowEnd` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Interactions | `Interaction` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Interactions | `PayloadFeature` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Interactions | `SuccessionFlow` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Metadata | `Metaclass` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Metadata | `MetadataFeature` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Multiplicities | `MultiplicityRange` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Kernel::Packages | `ElementFilterMembership` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Kernel::Packages | `LibraryPackage` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Kernel::Packages | `Package` | ☐ | ☐ | ☐ | ☐ |
| 4 | KerML::Kernel::Structures | `Structure` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Annotations | `AnnotatingElement` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Annotations | `Annotation` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Annotations | `Comment` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Annotations | `Documentation` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Annotations | `TextualRepresentation` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Dependencies | `Dependency` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Namespaces | `Membership` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Namespaces | `MembershipImport` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Namespaces | `Namespace` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Namespaces | `NamespaceImport` | ☐ | ☐ | ☐ | ☐ |
| 2 | KerML::Root::Namespaces | `OwningMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `AcceptActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `ActionDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `ActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `AssignmentActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `DecisionNode` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `ForLoopActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `ForkNode` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `IfActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `JoinNode` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `MergeNode` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `PerformActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `SendActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `TerminateActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `TriggerInvocationExpression` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Actions | `WhileLoopActionUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Allocations | `AllocationDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Allocations | `AllocationUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::AnalysisCases | `AnalysisCaseDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::AnalysisCases | `AnalysisCaseUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Attributes | `AttributeDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Attributes | `AttributeUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Calculations | `CalculationDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Calculations | `CalculationUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Cases | `CaseDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Cases | `CaseUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Cases | `ObjectiveMembership` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Connections | `BindingConnectorAsUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Connections | `ConnectionDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Connections | `ConnectionUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Connections | `SuccessionAsUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Constraints | `AssertConstraintUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Constraints | `ConstraintDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Constraints | `ConstraintUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::DefinitionAndUsage | `Definition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::DefinitionAndUsage | `ReferenceUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::DefinitionAndUsage | `Usage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::DefinitionAndUsage | `VariantMembership` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Enumerations | `EnumerationDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Enumerations | `EnumerationUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Flows | `FlowDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Flows | `FlowUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Flows | `SuccessionFlowUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Interfaces | `InterfaceDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Interfaces | `InterfaceUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Items | `ItemDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Items | `ItemUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Metadata | `MetadataDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Metadata | `MetadataUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Occurrences | `EventOccurrenceUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Occurrences | `OccurrenceDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Occurrences | `OccurrenceUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Parts | `PartDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Parts | `PartUsage` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Ports | `ConjugatedPortDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Ports | `ConjugatedPortTyping` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Ports | `PortConjugation` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Ports | `PortDefinition` | ☐ | ☐ | ☐ | ☐ |
| 6 | SysML::Systems::Ports | `PortUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `ActorMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `ConcernDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `ConcernUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `FramedConcernMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `RequirementConstraintMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `RequirementDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `RequirementUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `SatisfyRequirementUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `StakeholderMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Requirements | `SubjectMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::States | `ExhibitStateUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::States | `StateDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::States | `StateSubactionMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::States | `StateUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::States | `TransitionFeatureMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::States | `TransitionUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::UseCases | `IncludeUseCaseUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::UseCases | `UseCaseDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::UseCases | `UseCaseUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::VerificationCases | `RequirementVerificationMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::VerificationCases | `VerificationCaseDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::VerificationCases | `VerificationCaseUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `MembershipExpose` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `NamespaceExpose` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `RenderingDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `RenderingUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `ViewDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `ViewRenderingMembership` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `ViewUsage` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `ViewpointDefinition` | ☐ | ☐ | ☐ | ☐ |
| 7 | SysML::Systems::Views | `ViewpointUsage` | ☐ | ☐ | ☐ | ☐ |

### A.2 Abstract metaclasses (8)

Not instantiable; covered through their concrete subclasses.

| Package | Metaclass | Concrete subclasses |
|---|---|---|
| KerML::Kernel::Expressions | `InstantiationExpression` | `CollectExpression`, `ConstructorExpression`, `FeatureChainExpression`, `IndexExpression`, `InvocationExpression`, `OperatorExpression` … (8 in total) |
| KerML::Root::Elements | `Element` | `AcceptActionUsage`, `ActionDefinition`, `ActionUsage`, `ActorMembership`, `AllocationDefinition`, `AllocationUsage` … (167 in total) |
| KerML::Root::Elements | `Relationship` | `ActorMembership`, `AllocationDefinition`, `AllocationUsage`, `Annotation`, `Association`, `AssociationStructure` … (62 in total) |
| KerML::Root::Namespaces | `Import` | `MembershipExpose`, `MembershipImport`, `NamespaceExpose`, `NamespaceImport` |
| SysML::Systems::Actions | `ControlNode` | `DecisionNode`, `ForkNode`, `JoinNode`, `MergeNode` |
| SysML::Systems::Actions | `LoopActionUsage` | `ForLoopActionUsage`, `WhileLoopActionUsage` |
| SysML::Systems::Connections | `ConnectorAsUsage` | `AllocationUsage`, `BindingConnectorAsUsage`, `ConnectionUsage`, `FlowUsage`, `InterfaceUsage`, `SuccessionAsUsage` … (7 in total) |
| SysML::Systems::Views | `Expose` | `MembershipExpose`, `NamespaceExpose` |

### A.3 Non-derived attributes (97)

Every non-derived attribute declared in the metamodel. Each must be set to a non-default value on at least one created element, and changed on at least one updated element. *Carrier* is the declaring metaclass if it is concrete, otherwise a concrete subclass.

| Declared on | Attribute | Type | Multiplicity | Carrier | Set | Changed |
|---|---|---|---|---|:-:|:-:|
| `Annotation` | `annotatedElement` | Element | [1..1] | `Annotation` | ☐ | ☐ |
| `CollectExpression` | `operator` | String | [1..1] | `CollectExpression` | ☐ | ☐ |
| `Comment` | `body` | String | [1..1] | `Comment` | ☐ | ☐ |
| `Comment` | `locale` | String | [0..1] | `Comment` | ☐ | ☐ |
| `ConjugatedPortTyping` | `conjugatedPortDefinition` | ConjugatedPortDefinition | [1..1] | `ConjugatedPortTyping` | ☐ | ☐ |
| `Conjugation` | `conjugatedType` | Type | [1..1] | `Conjugation` | ☐ | ☐ |
| `Conjugation` | `originalType` | Type | [1..1] | `Conjugation` | ☐ | ☐ |
| `ConnectionDefinition` | `isSufficient` | Boolean | [1..1] | `ConnectionDefinition` | ☐ | ☐ |
| `CrossSubsetting` | `crossedFeature` | Feature | [1..1] | `CrossSubsetting` | ☐ | ☐ |
| `Definition` | `isVariation` | Boolean | [1..1] | `Definition` | ☐ | ☐ |
| `Dependency` | `client` | Element | [1..*] | `Dependency` | ☐ | ☐ |
| `Dependency` | `supplier` | Element | [1..*] | `Dependency` | ☐ | ☐ |
| `Differencing` | `differencingType` | Type | [1..1] | `Differencing` | ☐ | ☐ |
| `Disjoining` | `disjoiningType` | Type | [1..1] | `Disjoining` | ☐ | ☐ |
| `Disjoining` | `typeDisjoined` | Type | [1..1] | `Disjoining` | ☐ | ☐ |
| `Element` | `aliasIds` | String | [0..*] | `AcceptActionUsage` | ☐ | ☐ |
| `Element` | `declaredName` | String | [0..1] | `AcceptActionUsage` | ☐ | ☐ |
| `Element` | `declaredShortName` | String | [0..1] | `AcceptActionUsage` | ☐ | ☐ |
| `Element` | `elementId` | String | [1..1] | `AcceptActionUsage` | ☐ | ☐ |
| `Element` | `isImpliedIncluded` | Boolean | [1..1] | `AcceptActionUsage` | ☐ | ☐ |
| `Element` | `ownedRelationship` | Relationship (composite) | [0..*] | `AcceptActionUsage` | ☐ | ☐ |
| `Element` | `owningRelationship` | Relationship | [0..1] | `AcceptActionUsage` | ☐ | ☐ |
| `EnumerationDefinition` | `isVariation` | Boolean | [1..1] | `EnumerationDefinition` | ☐ | ☐ |
| `Expose` | `isImportAll` | Boolean | [1..1] | `MembershipExpose` | ☐ | ☐ |
| `Expose` | `visibility` | VisibilityKind | [1..1] | `MembershipExpose` | ☐ | ☐ |
| `Feature` | `direction` | FeatureDirectionKind | [0..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isComposite` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isConstant` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isDerived` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isEnd` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isOrdered` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isPortion` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isUnique` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `Feature` | `isVariable` | Boolean | [1..1] | `Feature` | ☐ | ☐ |
| `FeatureChainExpression` | `operator` | String | [1..1] | `FeatureChainExpression` | ☐ | ☐ |
| `FeatureChaining` | `chainingFeature` | Feature | [1..1] | `FeatureChaining` | ☐ | ☐ |
| `FeatureInverting` | `featureInverted` | Feature | [1..1] | `FeatureInverting` | ☐ | ☐ |
| `FeatureInverting` | `invertingFeature` | Feature | [1..1] | `FeatureInverting` | ☐ | ☐ |
| `FeatureTyping` | `type` | Type | [1..1] | `FeatureTyping` | ☐ | ☐ |
| `FeatureTyping` | `typedFeature` | Feature | [1..1] | `FeatureTyping` | ☐ | ☐ |
| `FeatureValue` | `isDefault` | Boolean | [1..1] | `FeatureValue` | ☐ | ☐ |
| `FeatureValue` | `isInitial` | Boolean | [1..1] | `FeatureValue` | ☐ | ☐ |
| `FramedConcernMembership` | `kind` | RequirementConstraintKind | [1..1] | `FramedConcernMembership` | ☐ | ☐ |
| `Import` | `isImportAll` | Boolean | [1..1] | `MembershipExpose` | ☐ | ☐ |
| `Import` | `isRecursive` | Boolean | [1..1] | `MembershipExpose` | ☐ | ☐ |
| `Import` | `visibility` | VisibilityKind | [1..1] | `MembershipExpose` | ☐ | ☐ |
| `IndexExpression` | `operator` | String | [1..1] | `IndexExpression` | ☐ | ☐ |
| `Intersecting` | `intersectingType` | Type | [1..1] | `Intersecting` | ☐ | ☐ |
| `Invariant` | `isNegated` | Boolean | [1..1] | `Invariant` | ☐ | ☐ |
| `LibraryPackage` | `isStandard` | Boolean | [1..1] | `LibraryPackage` | ☐ | ☐ |
| `LiteralBoolean` | `value` | Boolean | [1..1] | `LiteralBoolean` | ☐ | ☐ |
| `LiteralInteger` | `value` | Integer | [1..1] | `LiteralInteger` | ☐ | ☐ |
| `LiteralRational` | `value` | Real | [1..1] | `LiteralRational` | ☐ | ☐ |
| `LiteralString` | `value` | String | [1..1] | `LiteralString` | ☐ | ☐ |
| `Membership` | `memberElement` | Element | [1..1] | `Membership` | ☐ | ☐ |
| `Membership` | `memberName` | String | [0..1] | `Membership` | ☐ | ☐ |
| `Membership` | `memberShortName` | String | [0..1] | `Membership` | ☐ | ☐ |
| `Membership` | `visibility` | VisibilityKind | [1..1] | `Membership` | ☐ | ☐ |
| `MembershipImport` | `importedMembership` | Membership | [1..1] | `MembershipImport` | ☐ | ☐ |
| `NamespaceImport` | `importedNamespace` | Namespace | [1..1] | `NamespaceImport` | ☐ | ☐ |
| `OccurrenceDefinition` | `isIndividual` | Boolean | [1..1] | `OccurrenceDefinition` | ☐ | ☐ |
| `OccurrenceUsage` | `isIndividual` | Boolean | [1..1] | `OccurrenceUsage` | ☐ | ☐ |
| `OccurrenceUsage` | `portionKind` | PortionKind | [0..1] | `OccurrenceUsage` | ☐ | ☐ |
| `OperatorExpression` | `operator` | String | [1..1] | `OperatorExpression` | ☐ | ☐ |
| `PortConjugation` | `originalPortDefinition` | PortDefinition | [1..1] | `PortConjugation` | ☐ | ☐ |
| `Redefinition` | `redefinedFeature` | Feature | [1..1] | `Redefinition` | ☐ | ☐ |
| `Redefinition` | `redefiningFeature` | Feature | [1..1] | `Redefinition` | ☐ | ☐ |
| `ReferenceSubsetting` | `referencedFeature` | Feature | [1..1] | `ReferenceSubsetting` | ☐ | ☐ |
| `Relationship` | `isImplied` | Boolean | [1..1] | `ActorMembership` | ☐ | ☐ |
| `Relationship` | `ownedRelatedElement` | Element (composite) | [0..*] | `ActorMembership` | ☐ | ☐ |
| `Relationship` | `owningRelatedElement` | Element | [0..1] | `ActorMembership` | ☐ | ☐ |
| `Relationship` | `source` | Element | [0..*] | `ActorMembership` | ☐ | ☐ |
| `Relationship` | `target` | Element | [0..*] | `ActorMembership` | ☐ | ☐ |
| `RequirementConstraintMembership` | `kind` | RequirementConstraintKind | [1..1] | `RequirementConstraintMembership` | ☐ | ☐ |
| `RequirementDefinition` | `reqId` | String | [0..1] | `RequirementDefinition` | ☐ | ☐ |
| `RequirementUsage` | `reqId` | String | [0..1] | `RequirementUsage` | ☐ | ☐ |
| `RequirementVerificationMembership` | `kind` | RequirementConstraintKind | [1..1] | `RequirementVerificationMembership` | ☐ | ☐ |
| `SelectExpression` | `operator` | String | [1..1] | `SelectExpression` | ☐ | ☐ |
| `Specialization` | `general` | Type | [1..1] | `Specialization` | ☐ | ☐ |
| `Specialization` | `specific` | Type | [1..1] | `Specialization` | ☐ | ☐ |
| `StateDefinition` | `isParallel` | Boolean | [1..1] | `StateDefinition` | ☐ | ☐ |
| `StateSubactionMembership` | `kind` | StateSubactionKind | [1..1] | `StateSubactionMembership` | ☐ | ☐ |
| `StateUsage` | `isParallel` | Boolean | [1..1] | `StateUsage` | ☐ | ☐ |
| `Subclassification` | `subclassifier` | Classifier | [1..1] | `Subclassification` | ☐ | ☐ |
| `Subclassification` | `superclassifier` | Classifier | [1..1] | `Subclassification` | ☐ | ☐ |
| `Subsetting` | `subsettedFeature` | Feature | [1..1] | `Subsetting` | ☐ | ☐ |
| `Subsetting` | `subsettingFeature` | Feature | [1..1] | `Subsetting` | ☐ | ☐ |
| `TextualRepresentation` | `body` | String | [1..1] | `TextualRepresentation` | ☐ | ☐ |
| `TextualRepresentation` | `language` | String | [1..1] | `TextualRepresentation` | ☐ | ☐ |
| `TransitionFeatureMembership` | `kind` | TransitionFeatureKind | [1..1] | `TransitionFeatureMembership` | ☐ | ☐ |
| `TriggerInvocationExpression` | `kind` | TriggerKind | [1..1] | `TriggerInvocationExpression` | ☐ | ☐ |
| `Type` | `isAbstract` | Boolean | [1..1] | `Type` | ☐ | ☐ |
| `Type` | `isSufficient` | Boolean | [1..1] | `Type` | ☐ | ☐ |
| `TypeFeaturing` | `featureOfType` | Feature | [1..1] | `TypeFeaturing` | ☐ | ☐ |
| `TypeFeaturing` | `featuringType` | Type | [1..1] | `TypeFeaturing` | ☐ | ☐ |
| `Unioning` | `unioningType` | Type | [1..1] | `Unioning` | ☐ | ☐ |
| `Usage` | `isVariation` | Boolean | [1..1] | `Usage` | ☐ | ☐ |

### A.4 Enumeration literals (7 enumerations, 21 literals)

Every literal must appear on at least one created element.

| Enumeration | Literals | Carried by |
|---|---|---|
| `FeatureDirectionKind` | `in`, `inout`, `out` | `Feature.direction` |
| `PortionKind` | `timeslice`, `snapshot` | `OccurrenceUsage.portionKind` |
| `RequirementConstraintKind` | `assumption`, `requirement` | `FramedConcernMembership.kind`, `RequirementConstraintMembership.kind`, `RequirementVerificationMembership.kind` |
| `StateSubactionKind` | `entry`, `do`, `exit` | `StateSubactionMembership.kind` |
| `TransitionFeatureKind` | `trigger`, `guard`, `effect` | `TransitionFeatureMembership.kind` |
| `TriggerKind` | `when`, `at`, `after` | `TriggerInvocationExpression.kind` |
| `VisibilityKind` | `private`, `protected`, `public` | `Expose.visibility`, `Import.visibility`, `Membership.visibility` |

## Appendix B — PIM type coverage

`—` means the API defines no such operation for the type; that is asserted by absence, not tested.

| PIM type | Create | Read | Update | Delete | Members to assert |
|---|---|---|---|---|---|
| `Project` | `PostProjectAsync` | `GetProjectsAsync`, `GetProjectByIdAsync` | `PutProjectByIdAsync` | `DeleteProjectByIdAsync` | name, description, alias, created, defaultBranch |
| `Branch` | `PostBranchByProjectAsync` | `GetBranchesByProjectAsync`, `GetBranchesByProjectAndIdAsync` | — (head moves by commit / merge) | `DeleteBranchByProjectAndIdAsync` | head, referencedCommit, owningProject, created, deleted |
| `Tag` | `PostTagByProjectAsync` | `GetTagsByProjectAsync`, `GetTagByProjectAndIdAsync` | — | `DeleteTagByProjectAndIdAsync` | taggedCommit / referencedCommit, owningProject, created |
| `CommitReference` (abstract) | via `Branch`, `Tag` | | | | |
| `Record` (abstract) | via every type above | | | | id, name, alias, description, resourceIdentifier |
| `Commit` | `PostCommitByProjectAsync` (with / without `branchId`) | `GetCommitsByProjectAsync`, `GetCommitByProjectAndIdAsync` | — (immutable) | — | owningProject, previousCommits, created, description |
| `DataVersion` | in `Commit.change` | `GetChangesByProjectCommitAsync`, `GetChangeByProjectCommitIdAsync` | new payload, same identity | `payload = null` | identity, payload |
| `DataIdentity` | in `DataVersion.identity` | as above | — | — | id |
| `DataDifference` | — | `DiffAsync` | — | — | baseData, compareData (all three `ChangeType`s) |
| `MergeResult` | `MergeAsync` | — | — | — | mergeCommit, conflict |
| `Query` | `PostQueryByProjectAsync` | `GetQueriesByProjectAsync`, `GetQueryByProjectAndIdAsync` | `PutQueryByProjectAndIdAsync` | `DeleteQueryByProjectAndIdAsync` | select, where, scope, orderBy, project |
| `PrimitiveConstraint` | in `Query.where` | the three query-result operations | via `PUT` query | via `PUT` / `DELETE` query | property, operator (7 literals), value, inverse |
| `CompositeConstraint` | in `Query.where` | as above | as above | as above | constraint (nested), operator (`AND`, `OR`) |
| `ProjectUsage` | commit payload | `GetProjectUsageByProjectCommitElementAsync`, `excludeUsed` | new payload | `payload = null` | usedProject, usedProjectCommit |
| `ExternalData` | commit payload | changes | new payload | `payload = null` | resourceIdentifier |
| `ExternalRelationship` | commit payload | changes | new payload | `payload = null` | elementEnd, externalDataEnd, specification, language |
| `Error` (PSM only) | — | every failing call | — | — | status code, parsed body |

## Appendix C — operation checklist (35, `IPsmApiClient`)

*Steps* = scenario steps (§5) that call the operation. Step 14 checks every row has a success and a
failure.

| # | Operation | Method | Steps | Success | Failure |
|---:|---|---|---|:-:|:-:|
| 1 | `GET /projects` | `GetProjectsAsync` | 1, 2 | ☐ | ☐ |
| 2 | `POST /projects` | `PostProjectAsync` | 2, 10 | ☐ | ☐ |
| 3 | `GET /projects/{projectId}` | `GetProjectByIdAsync` | 1, 2, 13 | ☐ | ☐ |
| 4 | `PUT /projects/{projectId}` | `PutProjectByIdAsync` | 2 | ☐ | ☐ |
| 5 | `DELETE /projects/{projectId}` | `DeleteProjectByIdAsync` | 13 | ☐ | ☐ |
| 6 | `GET …/branches` | `GetBranchesByProjectAsync` | 6 | ☐ | ☐ |
| 7 | `POST …/branches` | `PostBranchByProjectAsync` | 6 | ☐ | ☐ |
| 8 | `GET …/branches/{branchId}` | `GetBranchesByProjectAndIdAsync` | every commit, 13 | ☐ | ☐ |
| 9 | `DELETE …/branches/{branchId}` | `DeleteBranchByProjectAndIdAsync` | 13 | ☐ | ☐ |
| 10 | `GET …/tags` | `GetTagsByProjectAsync` | 4 | ☐ | ☐ |
| 11 | `POST …/tags` | `PostTagByProjectAsync` | 4 | ☐ | ☐ |
| 12 | `GET …/tags/{tagId}` | `GetTagByProjectAndIdAsync` | 4, 13 | ☐ | ☐ |
| 13 | `DELETE …/tags/{tagId}` | `DeleteTagByProjectAndIdAsync` | 13 | ☐ | ☐ |
| 14 | `GET …/commits` | `GetCommitsByProjectAsync` | 2 | ☐ | ☐ |
| 15 | `POST …/commits` | `PostCommitByProjectAsync` | 2–8, 10–12 | ☐ | ☐ |
| 16 | `GET …/commits/{commitId}` | `GetCommitByProjectAndIdAsync` | every commit | ☐ | ☐ |
| 17 | `GET …/commits/{commitId}/changes` | `GetChangesByProjectCommitAsync` | every commit | ☐ | ☐ |
| 18 | `GET …/changes/{changeId}` | `GetChangeByProjectCommitIdAsync` | every commit | ☐ | ☐ |
| 19 | `GET …/commits/{commitId}/elements` | `GetElementsByProjectCommitAsync` | every commit | ☐ | ☐ |
| 20 | `GET …/elements/{elementId}` | `GetElementByProjectCommitIdAsync` | every commit | ☐ | ☐ |
| 21 | `GET …/elements/{elementId}/projectUsage` | `GetProjectUsageByProjectCommitElementAsync` | 10 | ☐ | ☐ |
| 22 | `GET …/commits/{commitId}/roots` | `GetRootsByProjectCommitAsync` | every commit, 10 | ☐ | ☐ |
| 23 | `GET …/elements/{relatedElementId}/relationships` | `GetRelationshipsByProjectCommitRelatedElementAsync` | every commit | ☐ | ☐ |
| 24 | `GET …/commits/{compareCommitId}/diff` | `DiffAsync` | 5, 8, 12 | ☐ | ☐ |
| 25 | `POST …/branches/{targetBranchId}/merge` | `MergeAsync` | 8 | ☐ | ☐ |
| 26 | `GET …/queries` | `GetQueriesByProjectAsync` | 9 | ☐ | ☐ |
| 27 | `POST …/queries` | `PostQueryByProjectAsync` | 9 | ☐ | ☐ |
| 28 | `GET …/queries/{queryId}` | `GetQueryByProjectAndIdAsync` | 9 | ☐ | ☐ |
| 29 | `PUT …/queries/{queryId}` | `PutQueryByProjectAndIdAsync` | 9 | ☐ | ☐ |
| 30 | `DELETE …/queries/{queryId}` | `DeleteQueryByProjectAndIdAsync` | 9 | ☐ | ☐ |
| 31 | `GET …/queries/{queryId}/results` | `GetQueryResultsByProjectIdQueryIdAsync` | 9 | ☐ | ☐ |
| 32 | `GET …/query-results` | `GetQueryResultsByProjectIdQueryAsync` | 9 | ☐ | ☐ |
| 33 | `POST …/query-results` | `GetQueryResultsByProjectIdQueryPostAsync` | 9 | ☐ | ☐ |
| 34 | `GET /meta/datatypes` | `GetDatatypesAsync` | 1 | ☐ | ☐ |
| 35 | `GET /meta/datatypes/{datatypeId}` | `GetDatatypeByIdAsync` | 1 | ☐ | ☐ |

Every list operation is also exercised with `page[size]`, `page[after]` and `page[before]`.
