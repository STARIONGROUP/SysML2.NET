# SySML2.NET.REST.Tests — project structure

This page describes how the test project of the REST client `SySML2.NET.REST` is organised. It is written to be moved to the project wiki.

The project holds two kinds of tests:

| Kind | Needs | Runs in CI | What it verifies |
|---|---|---|---|
| **Offline tests** | nothing | yes | The client, the session and the test infrastructure itself, against mocked HTTP answers and recorded JSON. |
| **Integration scenario** (`[Category("Integration")]`) | Docker | no | The client against a live Flexo MMS SysML v2 server. A complete model is built step by step and every write is read back. |

The integration scenario verifies **our client**, not Flexo. Its measure is the OpenAPI document of the Systems Modeling API and Services specification (`Resources/ptc-25-02-30.json`): every request and response is checked against it. Where Flexo answers differently, the scenario reports a named warning instead of failing. Client defects are not fixed while the scenario is built: each gets a GitHub issue and its check stays red, so the scenario is the fixture the client is fixed against later. The plan behind the scenario is `SySML2.NET.REST.Tests/INTEGRATION_TEST_PLAN.md`.

## Layout

```
SySML2.NET.REST.Tests/
├── PsmApiClientTestFixture.cs        offline: every client operation against MockHttp
├── SessionTestFixture.cs             offline: Session against recorded JSON
├── QueryStringBuilderTestFixture.cs  offline: query string building
├── Exceptions/                       offline: PsmApiException, MergeConflictException
├── Helpers/                          the scenario's infrastructure (no tests)
│   ├── FlexoTestHost.cs, FlexoJwt.cs, ModelBuilder.cs, …   see below
│   └── Models/                       the model each step commits
│       ├── StepModel.cs              the base: naming and ownership conventions
│       ├── RootModel.cs              step 2: KerML Root and Packages
│       ├── CoreModel.cs              step 3: KerML Core
│       ├── KernelModel.cs            step 4: KerML Kernel
│       ├── KerMLUpdateModel.cs       step 5: updates U1–U4 of steps 2–4
│       ├── SystemsStructureModel.cs  step 6: SysML structure, on the branch systems
│       ├── SystemsBehaviorModel.cs   step 7: SysML behaviour, requirements, cases and views
│       ├── SystemsUpdateModel.cs     step 8: updates U1–U2 of steps 6–7
│       ├── LibraryModel.cs           step 10: the model of the project that the main model uses
│       ├── MoveModel.cs              step 12: the content that update U5 moves
│       └── DeletionModel.cs          step 12: the content that the deletion cases delete
├── HelpersTests/                     offline: one fixture per helper, mirroring Helpers/
│   ├── FlexoJwtTestFixture.cs, ModelBuilderTestFixture.cs, …
│   └── Models/                       one fixture per model class
├── IntegrationTests/                 needs Docker
│   └── FlexoModelScenarioTestFixture.cs   the scenario: one [Test] per step
├── Resources/Flexo/cluster.trig      Flexo's initial quad-store content (Apache-2.0, see NOTICE)
├── INTEGRATION_TEST_PLAN.md          the scenario plan
└── TEST_PROJECT_STRUCTURE.md         this page
```

| Folder | Namespace | Contains |
|---|---|---|
| `Helpers/` | `SySML2.NET.REST.Tests.Helpers` | Helper types only. No `[TestFixture]`. |
| `Helpers/Models/` | `SySML2.NET.REST.Tests.Helpers.Models` | One model class per scenario step. |
| `HelpersTests/` | `SySML2.NET.REST.Tests.HelpersTests` (and `.Models`) | The offline fixtures of the helpers, one per helper type. They run in CI. |
| `IntegrationTests/` | `SySML2.NET.REST.Tests.IntegrationTests` | The fixtures marked `[Category("Integration")]`, which need a live server. |

Files linked from elsewhere in the repository and copied to the output folder:

| Output path | Source | Used by |
|---|---|---|
| `Data/*.json` | `SysML2.NET.Serializer.Json.Tests/Data/` | `SessionTestFixture` |
| `Resources/ptc-25-02-30.json` | `Resources/ptc-25-02-30.json` (the normative OpenAPI document) | `OpenApiPathTemplates`, `CoverageVerdict` |

Every helper type with behaviour has its own offline fixture in `HelpersTests/`. A bug in a helper therefore shows up in CI, not as a confusing result in a Docker run. The exceptions are `FlexoTestHost`, which needs Docker and is exercised by the scenario itself, and the plain data types `ContractViolation`, `ContractViolationKind`, `MetamodelProperty`, `ScenarioState` and `ServerDivergence`, which the fixtures of their users cover.

## Helpers

The types fall into six groups.

### 1. Server host

| Type | Responsibility |
|---|---|
| `FlexoTestHost` | Starts Flexo in Docker with Testcontainers. On one network it starts, in order: Fuseki (the quad store, seeded with `cluster.trig`), the Flexo layer1 service, then the Flexo SysML v2 service, each image pinned by digest. Before that last start, it creates the `sysmlv2` organisation on layer1. If Docker is unreachable, the fixture is **ignored**. Any later start-up failure **fails** the run, with the container logs attached. |
| `FlexoJwt` | Creates the HS256 tokens layer1 accepts. The signing secret is random per run, so no secret is stored in source. |

### 2. HTTP wiring

The `Session` under test gets its `HttpClient` from `IntegrationHttpClientFactory`. All clients share one handler chain:

```
Session → PsmApiClient → HttpClient
                          └─ BearerTokenHandler   adds "Authorization: Bearer <token>"
                              └─ EndpointRecorder records (operation, status), checks request and response
                                                  against the OpenAPI document, flags empty GUIDs in bodies
                                  └─ HttpClientHandler → Flexo
```

| Type | Responsibility |
|---|---|
| `IntegrationHttpClientFactory` | `IHttpClientFactory` that hands out clients over the shared chain. The chain is disposed once, at the end of the fixture. |
| `BearerTokenHandler` | Adds the bearer token, because `Session` has no authentication option of its own. |
| `EndpointRecorder` | Records every request as an `EndpointCall`: the OpenAPI operation it matched, the request line, the status, and how the request and the response break the document. It buffers the response body, so the client still reads it. It also collects request bodies that reference `00000000-0000-0000-0000-000000000000`, and totals the time spent waiting for the server and checking the contract; the coverage step prints both. |
| `EndpointCall` | One recorded request, with its `RequestViolations` (client defects) and `ResponseViolations` (server divergences). |

### 3. The OpenAPI contract

| Type | Responsibility |
|---|---|
| `OpenApiDocument` | Reads the OpenAPI 3.1 document as JSON Schema 2020-12, with JsonSchema.Net, and matches a request path to its operation. When templates overlap, the one with the most literal segments wins. It removes the `$id`s of the component schemas and makes every reference absolute, because the document references its schemas both by JSON pointer and by those `$id`s. |
| `OpenApiOperation` | One operation: its parameters, request body and responses. `CheckRequest` checks the query and the body a client sends; `CheckResponse` checks the status and the body a server answers. |
| `OpenApiParameter`, `OpenApiContent` | A declared parameter, and a declared body (a schema per media type). They check one value or one body; schema errors are summarized to the branch whose `@type` matched. Where the declared schema is a union of object schemas that each fix their `@type`, as the element schemas are, an object is validated against the member of its `@type` alone: the verdict is the same and the evaluation is far cheaper. |
| `ContractViolation`, `ContractViolationKind` | One way a request or a response breaks the document, by kind, so a known divergence can match it. |
| `OpenApiConformance` | After each step: fails on any request violation; reports a response violation as the known divergence that explains it, or fails. |

### 4. Building the model

| Type | Responsibility |
|---|---|
| `ModelBuilder` | Holds the whole model across the steps: each step starts its own scope (`BeginStep`) and creates POCOs with deterministic ids, wired with `AssignOwnership`. It converts them with the Dal's `ToDto(includeDerivedProperties: true)`, `Changed` selects the delta a commit sends, `Copy` copies a DTO for a change outside the builder, `OwnedTree` finds everything an element owns, and `Find` looks an element up by its scoped name. An element whose derived values cannot be computed without the model library is converted without them and listed in `WithoutDerivedValues`. POCOs are used because a hand-built DTO cannot set members that a derived property redefines. |
| `ScenarioQueries` | The queries of step 9, each with a predicate that picks its results from the expected model. |
| `DeterministicGuid` | Name-based UUID v5 of `"<step>/<name>"`, so the elements have the same ids in every run. |
| `Models/*` | One class per step, deriving from `StepModel`, describing what that step adds to or changes in the model: `RootModel` (step 2), `CoreModel` (step 3), `KernelModel` (step 4), `KerMLUpdateModel` (step 5), `SystemsStructureModel` (step 6), `SystemsBehaviorModel` (step 7), `SystemsUpdateModel` (step 8), `LibraryModel` (step 10, in a second project), `MoveModel` and `DeletionModel` (step 12). The base class holds the helpers the steps share: naming, ownership, features, parameters, results, literals and multiplicities. |

### 5. Checking what came back

| Type | Responsibility |
|---|---|
| `ScenarioState` | What the steps hand on to each other: the ids the server assigned (projects, branches, tags, commits), the head of every branch the scenario committed on, the expected model at each commit, and everything the server returned. |
| `ReadBackAssertions` | The checks each model step runs after a commit: the commit and its predecessor, the branch head, the changes and their payloads, the changed elements by id, every element as listed, roots (the elements without an `owner`), relationships, the member element and name of every membership, that no returned DTO holds an empty GUID, and that an earlier commit still reads as it was. Element equality uses `ComparerProvider` from `SysML2.NET.Extensions`; a difference names the properties that differ. Lists are read in pages of 100, and paging is checked on the first three pages of 10. A commit of project usages or external data is checked through its changes, the way a client reads such data back; query results and project usages have their own checks, and a deletion of an owner is checked to remove what it owns. One changed element of each metaclass is read by id, four reads at a time. Every read is guarded: a read that throws fails, or warns when its own response shows a known divergence that breaks deserialization. |
| `KnownServerDivergences`, `ServerDivergence` | The named ways Flexo diverges from the specification, each with a reference: an upstream issue, or a Flexo source location at the pinned commit. A divergence can explain response violations. `Report` raises an `Assert.Warn`. `ExpectNotFoundAsync` expects a 404, tolerates one named divergence, and fails when a conforming 404 reaches the caller without its `Error`. |
| `IsolatedAssertions` | Runs a check in an isolated NUnit context and returns its failures and warnings, without failing the calling test. The offline fixtures use it to prove that a check fails when it should. |

### 6. Coverage verdict

| Type | Responsibility |
|---|---|
| `CoverageVerdict` | Lists what the scenario has not yet exercised: concrete metaclasses never sent; non-derived properties never set to a non-default value, or never changed; enumeration literals never used; OpenAPI operations never answered with a 2xx and with a 4xx. A 5xx answer does not count as coverage. Properties whose value a constraint fixes, or that no update may change, are exempt, each with its reason. |
| `DtoMetadata`, `MetamodelProperty` | Read the metaclasses and properties from the `[Class]`/`[Property]` decorators on the generated DTOs, so the lists keep up with the generator. |

## The scenario

`FlexoModelScenarioTestFixture` is one ordered scenario:

- `[OneTimeSetUp]` starts `FlexoTestHost` and opens a `Session` over the factory.
- `[TearDown]` runs `OpenApiConformance` over the calls of the step that just ran.
- Each step is its own `[Test]`. The steps are ordered with `[DependsOnTest(previous, AllowFailure = true)]`: a failed step does not stop the next one. A step is skipped only when state it needs is missing (for example, no project was created).
- The last test, `VerifyCoverage`, always runs and prints the coverage verdict. It stays red until the scenario covers everything.
- `[OneTimeTearDown]` closes the session and disposes the handler chain and the containers.
- Starting the host has a 15-minute timeout.
- Steps 1–8 take about 7 minutes and about 500 calls. Flexo slows down after several hundred requests (Open-MBEE/flexo-mms-layer1-service#293), so a check that adds many calls can lengthen the run far more than its own share.

Having one `[Test]` per step goes against the one-test-per-method rule in `TESTING.md`. This is accepted for this scenario only (see plan §9).

| Step | Test | Status |
|---|---|---|
| 1 | `VerifyEmptyServer` | implemented |
| 2 | `VerifyProjectAndRootModel` | implemented |
| 3 | `VerifyKerMLCore` | implemented |
| 4 | `VerifyKerMLKernel` | implemented, with the tag `kernel` |
| 5 | `VerifyKerMLUpdate` | implemented, with the diff from the tag `kernel` |
| 6 | `VerifySystemsStructure` | implemented, on the branch `systems` |
| 7 | `VerifySystemsBehavior` | implemented, on the branch `systems` |
| 8 | `VerifyMerge` | implemented: the SysML update on `systems`, a conflict, its resolution, and a clean merge into the default branch |
| 9 | `VerifyQueries` | implemented |
| 10 | `VerifyProjectUsage` | implemented, on the branch `data` |
| 11 | `VerifyExternalData` | implemented, on the branch `data` |
| 12 | `VerifyDeletions` | implemented: relationships moved to new owners, a deletion, an emptied package, and a deletion that must cascade |
| 13 | `VerifyTeardown` | implemented: tag, branches and both projects deleted |
| 14 | `VerifyCoverage` | implemented, red by design |

### Reading the outcome

| Outcome | Meaning |
|---|---|
| Passed | Every check held. |
| Warning (the VSTest console shows it as **Skipped**) | Every check held, but Flexo showed one or more known divergences. The message names each divergence and its reference. |
| Failed | A check failed. Either the client is at fault, or Flexo diverges in a way not yet in `KnownServerDivergences`. |
| Ignored | Docker was not reachable. |

## Running the tests

```bash
# offline tests only (what CI runs)
dotnet test SySML2.NET.REST.Tests/SySML2.NET.REST.Tests.csproj --filter "TestCategory!=Integration"

# the Flexo scenario (Docker must be running)
dotnet test SySML2.NET.REST.Tests/SySML2.NET.REST.Tests.csproj --filter "TestCategory=Integration" --logger "console;verbosity=detailed"
```

The Docker endpoint is taken from `DOCKER_HOST`, and otherwise, on Windows, from the Docker Desktop Linux engine pipe (`npipe://./pipe/dockerDesktopLinuxEngine`).

## Extending the scenario

To add a step:

1. Add a model class under `Helpers/Models/`, deriving from `StepModel`, that adds the step's elements to the shared `ModelBuilder`, and its offline fixture under `HelpersTests/Models/`.
2. Add a `[Test]` to `IntegrationTests/FlexoModelScenarioTestFixture.cs` that depends on the previous step with `AllowFailure = true`, commits the model, and calls `ReadBackAssertions`.
3. Commit with `CommitModelAsync` and verify with `VerifyCommittedModelAsync`, which send the delta and check the whole model.
4. Make `VerifyCoverage` depend on the new last step.
5. When Flexo diverges, add an entry to `KnownServerDivergences` with its reference, and list it under "Observed" in plan §8.
6. Check the coverage verdict output: the step should remove items from it.

Fixture rules (`TESTING.md`, `DEVELOPMENT_STANDARDS.md`) apply to every file here, with the one exception noted above.
