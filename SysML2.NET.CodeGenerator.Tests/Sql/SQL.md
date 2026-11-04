# SQL test assets

Every `.sql` file in `SysML2.NET.CodeGenerator.Tests` supports the PostgreSQL persistence
schema that `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/SQLSchemaGenerator.cs`
emits from `SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema.hbs`. None of them is
production code: they are either the expected generator output, hand-written reference
material, or scripts the Docker-backed fixtures (and `pgbench`) run against a generated schema.

The fixtures that consume them live in
`SysML2.NET.CodeGenerator.Tests/Generators/UmlHandleBarsGenerators/`. The rules for those
fixtures are in `TESTING.md` §10.

## Expected generator output

| File | Role | Consumed by |
|---|---|---|
| `Expected/UML/AutoGenSql/schema.generated.sql` | The schema the generator must produce from `Resources/KerML_only_xmi.uml` + `Resources/SysML_only_xmi.uml`. It is the SQL counterpart of the `Expected/UML/Core/AutoGen*` C# files. | `SQLSchemaGeneratorTestFixture.Verify_that_expected_sql_schema_is_generated` (`[Category("Expected")]`) compares the live generator output with this file. |

The file is generated; never hand-edit it. When the template, the helpers, the
`ClassKindRegistry` or the XMI input change, regenerate it:

```bash
dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj --filter "FullyQualifiedName~SQLSchemaGeneratorTestFixture"
cp SysML2.NET.CodeGenerator.Tests/bin/Debug/net10.0/UML/_SysML2.NET.Sql/schema.sql SysML2.NET.CodeGenerator.Tests/Expected/UML/AutoGenSql/schema.generated.sql
```

Review the resulting diff before committing it: a changed `model_version` seed or a changed
`class_kind` id means the frozen registry moved, which is a deliberate decision, not a side
effect.

## Reference design

| File | Role | Consumed by |
|---|---|---|
| `Sql/schema.golden.sql` | The hand-written golden design the template was derived from. Sections marked `[GENERATED]` show a representative excerpt of what the template emits from the metamodel; the other sections appear verbatim in the template. It is the place to read the rationale (append-only versions, derived state as a second stream, typed identities, multi-release registry). | Nothing executes it. The generator sources (`SqlSchemaExtensions.cs`, `SqlSchemaHelpers.cs`, `SQLSchemaGenerator.cs`, `core-sql-schema.hbs`) cite it as the reference design. |

## Fixture scripts

These scripts are copied to the test output directory (`Sql/`) and read at runtime. Each
fixture generates the schema in-process and installs it on a PostgreSQL Testcontainer first,
so the scripts always run against the live generator output, never against the expected file.

| File | Role | Consumed by |
|---|---|---|
| `Sql/schema.smoke.sql` | End-to-end functional scenario on a clean database: create, rename, tombstone, merge, checkpoint, conversion commit, reference validation, tags, project usages, stored queries, diff, deletion ordering and soft delete. Every check raises on failure and emits a `RAISE NOTICE 'PASS …'` on success. | `SqlSchemaSmokeTestFixture` (`[Category("Integration")]`). The fixture counts the `PASS` notices declared in the script and asserts the same number came back, so adding a check to the script never requires touching the fixture. |
| `Sql/schema.concurrency.setup.sql` | Seeds one project with 1,000 elements, a checkpointed base commit and 16 branches, and installs two test-only functions: `sysml2.bench_try_commit(branch_index, seed)` (one compare-and-swap commit attempt) and `sysml2.bench_read(n)` (one branch-head element read). | `SqlSchemaConcurrencyTestFixture` (`[Category("Integration")]`) runs it once after installing the schema. Also the first step of a manual `pgbench` session. |
| `Sql/schema.concurrency.verify.sql` | Asserts the invariants C1 to C5 the commit protocol promises under contention (no accidental merges, every head walks back to the seed, no lost updates, attempts equal wins plus losses, overlay rows stay on their own branch) and reports the tallies. | `SqlSchemaConcurrencyTestFixture` runs it after every scenario. Also the last step of a manual `pgbench` session. |

## pgbench scripts

These are not used by any fixture. They drive the same `bench_try_commit` / `bench_read`
functions from `pgbench` for a manual load run; the `Run:` line in each header gives the
command. The .NET concurrency fixture replaces their `\set` loops with `Task`s and runs the
identical SQL.

| File | Scenario |
|---|---|
| `Sql/schema.concurrency.hot.sql` | Every client races the commit protocol on branch 1 (maximum contention). |
| `Sql/schema.concurrency.spread.sql` | Each client commits on its own branch (uncontended ceiling of the same protocol). |
| `Sql/schema.concurrency.read.sql` | Single-element branch-head reads on the hot branch; run concurrently with the hot scenario to show readers never block on writers. |

Manual sequence on a scratch database:

1. Install the generated schema (`Expected/UML/AutoGenSql/schema.generated.sql`, or the fresh
   generator output).
2. Run `Sql/schema.concurrency.setup.sql`.
3. Run one or more `pgbench` scenarios.
4. Run `Sql/schema.concurrency.verify.sql`.

## Fixture without a script

`SqlSchemaBenchmarkTestFixture` (`[Category("Benchmark")]`) builds its workload in C# from
authentic serializer payloads and reads no `.sql` file; it is listed here only so the
inventory of SQL fixtures is complete.
