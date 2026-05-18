# Extension-impl team template (v2 — reviewer + scope discipline + regression sweep)

Reusable team composition for implementing OCL-derived extension methods on a
SysML2.NET metaclass. v2 incorporates lessons from the `type-extensions-impl` task
that produced 1044/1044 green tests after a revert (TypeExtensions.cs, 33 methods,
6 slices).

## Path conventions (portable across contributors)

All paths in this file are **repo-relative with forward slashes** so the workflow
works regardless of where a contributor cloned the repo or which OS they use.
Forward slashes are accepted by both `dotnet` and `bash` on Windows.

When invoking tools that REQUIRE absolute paths (e.g. `Read`), the orchestrator
resolves the path at runtime by prepending the repo root (the current working
directory of `/implement-extensions`).

## When to use this template

Use this template when:

- Methods to implement are **derived properties** or **OCL-defined operations** on a
  single metaclass (interface like `IFoo`).
- They currently throw `NotSupportedException` (i.e. live as auto-generated stubs in
  a `*Extensions.cs` partial-style class).
- The OCL is the source of truth in `Resources/KerML_only_xmi.uml` or
  `Resources/SysML_only_xmi.uml`, and is already mirrored into each method's
  `<remarks>` XML doc block.
- There is **one** production file (`{{PRODUCTION_FILE}}`) and **one** test fixture
  (`{{TEST_FILE}}`).

Don't use this template when:

- The work spans multiple metaclasses (compose multiple instances instead).
- The methods need new infrastructure (spawn a design-discussion plan first).
- The work is in `TextualNotation/` or `LexicalRules/` — that has its own mandatory
  reviewer pattern; see `CLAUDE.md`.

## Placeholders

| Placeholder | Description | Example value |
|---|---|---|
| `{{TEAM_NAME}}` | Team identifier (kebab-case) | `feature-extensions-impl` |
| `{{TARGET_INTERFACE}}` | Subject interface | `IFeature` |
| `{{TARGET_METACLASS_NAME}}` | Subject metaclass (no `I` prefix) | `Feature` |
| `{{SUBJECT_PARAM}}` | Method first-arg name | `featureSubject` |
| `{{PRODUCTION_FILE}}` | Implementation target (repo-relative) | `SysML2.NET/Extend/FeatureExtensions.cs` |
| `{{TEST_FILE}}` | Test-fixture target (repo-relative) | `SysML2.NET.Tests/Extend/FeatureExtensionsTestFixture.cs` |
| `{{REFERENCE_PRODUCTION_FILE}}` | Working reference (repo-relative) | `SysML2.NET/Extend/NamespaceExtensions.cs` |
| `{{REFERENCE_TEST_FILE}}` | Working test reference (repo-relative) | `SysML2.NET.Tests/Extend/NamespaceExtensionsTestFixture.cs` |
| `{{INTERFACE_FILE}}` | Auto-gen POCO interface (repo-relative) | `SysML2.NET/Core/AutoGenPoco/IFeature.cs` |
| `{{NOTES_FILE}}` | Researcher's contract notes (repo-relative, optional) | `.team-notes/feature-extensions-spec.md` |
| `{{METHOD_LIST}}` | Bullet list of methods to implement, in dependency order | (per-task) |
| `{{ORCHESTRATOR_NAME}}` | Team-lead name for SendMessage | `team-lead` |

## Workflow

```
1. Researcher  → writes contract notes to {{NOTES_FILE}}: per method, the OCL
                 (or, if absent, the spec-text derivation), prose, dependencies,
                 suggested implementation, and test plan. ALWAYS RUN — it covers
                 the case where a method has no OCL body (e.g. Type::isConjugated
                 in the recent TypeExtensions task) and surfaces transitive
                 stub-blockers before the implementer hits them.

2. Implementer → implements all methods in {{PRODUCTION_FILE}}, sliced by
                 dependency tier (Tier 1: direct OfType filters; Tier 2:
                 chains over Tier 1; Tier 3: depends on operations; Tier 4:
                 closures with cycle protection). Reads {{NOTES_FILE}} first.

3. Tester      → rewrites {{TEST_FILE}} with one [Test] per method-under-test,
                 multiple Assert.That covering null + empty + populated. Reads
                 {{NOTES_FILE}} first for each method's test plan.

4. Reviewer    → READ-ONLY verdict against the OCL <remarks> blocks AND the
                 researcher's notes; flags any mistranslation; never edits.

5. Orchestrator → dotnet build, targeted dotnet test, REGRESSION SWEEP
                 (full solution test). Stub-blocker assertions in sibling
                 fixtures that now fail get updated as part of the same PR.
```

## Hard scope-discipline rule (v2)

**Honor user-memory `feedback_scope_discipline.md`**: when the task names a single
production file, the implementer MUST NOT touch other production files even when
an adjacent stub blocks dependent test coverage. Surface the blocker; let the
user decide on follow-up.

This rule overrides the urge to "just fix the one-line stub upstream." Even when
the side change is small and self-evidently correct, the user has explicitly
preferred narrow PRs with surfaced TODOs over scope creep. A reverted change is
worse than a NotSupportedException-asserting test, because the test makes the
blocker visible and the revert wastes work.

In-scope changes:
- `{{PRODUCTION_FILE}}` (implementer)
- `{{TEST_FILE}}` (tester)
- Sibling test fixtures whose `Throws.TypeOf<NotSupportedException>()` assertions
  now fail because the methods we just implemented succeed (regression sweep,
  orchestrator).

Out-of-scope changes (refuse and surface):
- Any other production file in `SysML2.NET/Extend/` or `SysML2.NET/Core/`.
- Auto-generated POCOs and interfaces.
- Code-generator templates.

## File-scope rules (instruction-based)

The Agent SDK has no per-file ACL, so each role's prompt restates the rule and tells
the agent to refuse or escalate when a tool call would violate it.

- **Researcher**: read-only everywhere except `{{NOTES_FILE}}`.
- **Implementer**: read-only everywhere except `{{PRODUCTION_FILE}}`. MUST read
  `{{NOTES_FILE}}` before starting.
- **Tester**: read-only everywhere except `{{TEST_FILE}}`. MUST read
  `{{NOTES_FILE}}` before starting.
- **Reviewer**: read-only everywhere — no Write/Edit at all. Cross-checks both
  `{{PRODUCTION_FILE}}` against `{{NOTES_FILE}}` and `{{TEST_FILE}}` against the
  test plans in `{{NOTES_FILE}}`.

No `>` redirects, `tee`, `sed -i`, `cp`, or `mv` into files outside the allowed
target. If a tool call would violate the rule, the agent must abort and message
`{{ORCHESTRATOR_NAME}}` instead of trying it.

## Coding conventions to enforce (canonical style)

Extracted from `NamespaceExtensions.cs` (the v2 reference template) and the user's
hand-reformat of `FeatureExtensions.cs`. All four roles must enforce these.

- **Imports**: `using System.Diagnostics.CodeAnalysis;` once, then short
  `[ExcludeFromCodeCoverage]` on stubs only. `using System.Linq;` once. Drop unused
  namespace imports.
- **`[ExcludeFromCodeCoverage]` removed from each method that gets a real
  implementation** — only stubs keep it.
- **XML doc cref**: `<see cref="X" />` with **space** before `/>`.
- **Preserve the existing `<remarks>` OCL block verbatim** on every method. Do NOT
  edit the OCL doc.
- **Two null-check patterns**:
  - **Pattern A — terse single-expression body**:
    ```csharp
    return {{SUBJECT_PARAM}} == null
        ? throw new ArgumentNullException(nameof({{SUBJECT_PARAM}}))
        : <expression>;
    ```
  - **Pattern B — block body with locals**:
    ```csharp
    if ({{SUBJECT_PARAM}} == null)
    {
        throw new ArgumentNullException(nameof({{SUBJECT_PARAM}}));
    }

    var x = ...;

    return ...;
    ```
- **OCL→C# operator translation** (the user-memory `feedback_ocl_union_translation.md`
  rule applies — `->union` MUST be `.Union(...)`, never spread or hand-rolled dedup):
  - `->select(...)` → `.Where(...)`
  - `->selectByKind(T)` → `.OfType<IT>()`
  - `->union(...)` → `.Union(...)` **(non-negotiable)**
  - `->reject(...)` → `.Where(! ...)`
  - `->closure(...)` → BFS/DFS with `visited` set seeded with self
  - `->first()` / `->at(1)` / "if isEmpty then null else first" → `.FirstOrDefault()`
  - `->including(x)` (parameter accumulator) → `new List<T>(existing) { x }` (fresh
    list, NEVER mutate the parameter)
  - `oclIsKindOf(T)` → `is IT`
  - `oclAsType(T)` → cast `as IT` or `(IT)`
- **Null propagation** preferred over explicit `if (x == null) return null;` chains:
  `crossedFeature?.chainingFeature`, `(x as T)?.Y as U`,
  `chainingFeatures?.Count >= 2 ? chainingFeatures[1] : null`.
- **Type narrowing**: `as T` for *single* casts; `.OfType<T>()` /
  `.OfType<T>().FirstOrDefault()` for *list* casts.
- **`List<T>` returns** from a single-expression body use
  `[..xs.OfType<T>()]` collection expressions (not `.ToList()`).
- **C# property patterns** (`x is IType { Prop: value }`) preferred over decomposed
  `x is IType name && name.Prop == value` when the narrowed variable is consulted
  once.
- **Switch expressions** preferred over if-else chains where applicable.
- **Multi-line LINQ chains**: each `.` on its own line, indented one level deeper
  than the declaration.
- **Meaningful variable names** — no single-letter `s`/`f`/`t`/`e`/`c`/`i` for
  domain objects (loop counter `i` excepted but discouraged). Transient projection
  results in `.Where(x => x != null)` may use `x`.
- **`Count == 0`** preferred over `.Any()`; **indexer / range** (`list[^1]`,
  `array[1..^1]`) preferred over `.Last()` / `.Skip().Take()`.
- **Use camelCase derived properties when in scope and implemented**: prefer
  `subject.fooBar` over re-deriving when that derived property is itself
  implemented.
- **Do not change the namespace, using directives, or method signatures.**

---

## Role prompt: researcher (DEFAULT — always run)

```
You are the **spec-researcher** on the `{{TEAM_NAME}}` team.

## Hard rule on file edits
You are READ-ONLY across the entire repository EXCEPT for ONE file:

  `{{NOTES_FILE}}`

You may use Write/Edit ONLY on that notes file. Do not Edit, Write, or NotebookEdit
any .cs, .uml, .xmi, .json, .csproj, .md, or any other file in the repo. Do not use
Bash to create or modify any other file (no `>` redirects, no `tee`, no `sed -i`, no
`cp`/`mv` into source). If a tool call would violate this, do NOT make it — instead
message `{{ORCHESTRATOR_NAME}}` via SendMessage and ask.

You MAY freely Read, Grep, Glob, and run read-only Bash commands across the repo.

## Goal
For each method in the list below, produce a contract entry in the notes file.
The notes are the source of truth that the implementer and tester will work from,
so be specific about types, navigation, edge cases, and the derivation source.

**Order of preference for the derivation source** (use the first that exists):

1. **OCL body in the XMI** — `<defaultValue language="OCL">` for derived properties,
   `<ownedRule>` body for operations. This is the canonical source.
2. **OCL block in the method's `<remarks>` XML doc** in `{{PRODUCTION_FILE}}`.
   These are mirrored from the XMI by the codegen — quote them in the notes.
3. **Spec-text-only methods** — when neither (1) nor (2) carries an OCL body
   (e.g. `Type::isConjugated` says only "Indicates whether this Type has an
   ownedConjugator"), record a short prose derivation rule plus a citation to
   the spec PDF/text. EXPLICITLY FLAG these in the notes so the implementer
   doesn't search for OCL that isn't there.

Do not skip the spec-text-only case — that's the failure mode this researcher
role exists to prevent.

## Methods to research
{{METHOD_LIST}}

## Where to look (paths are repo-relative; resolve against the working directory)
- `Resources/KerML_only_xmi.uml` — primary OCL source (KerML
  metaclasses). Search for the property/operation name, then for the nearby
  `<defaultValue ... language="OCL">` or `<ownedRule>` containing the body.
- `Resources/SysML_only_xmi.uml` — for SysML-specific metaclasses.
- `Resources/specification/1-Kernel_Modeling_Language.pdf.txt` —
  natural-language description of the same constraints.
- `{{INTERFACE_FILE}}` — already-generated interface; doc-comments often contain
  the OCL prose.
- `{{REFERENCE_PRODUCTION_FILE}}` — example of how derived properties are
  implemented in this codebase.

## Implementation-readiness notes (per method)
- Whether the implementation can use a sibling derived property that is itself
  stubbed (in which case the dev should bypass and filter `OwnedRelationship`
  directly).
- Whether `[..subject.OwnedRelationship.OfType<X>()]` is the correct pattern, or
  whether a `Where(...)` predicate is needed.
- Return-type quirks (`List<T>`, `T?`, single `T`).
- The null-check rule (every method takes `{{TARGET_INTERFACE}} {{SUBJECT_PARAM}}`
  and must throw `ArgumentNullException(nameof({{SUBJECT_PARAM}}))` when null).
- **Stub-blocker awareness**: if an OCL chain transitively reads a property that
  resolves to a still-stubbed `Compute*` method in another extension file, FLAG it
  in the notes (do not propose to fix the upstream stub — that violates scope
  discipline). The tester will assert `Throws.TypeOf<NotSupportedException>()` for
  the populated case of any test whose populated path hits the blocker.

## Output format
Append to `{{NOTES_FILE}}` (do NOT overwrite — the file may already contain entries
from prior phases of this task). One section per method. See the v1 template for the
exact section structure.

## When done
Send a SendMessage to `{{ORCHESTRATOR_NAME}}` with summary `spec ready` and a list
of methods where the OCL is ambiguous, missing, or transitively depends on other
stubbed extensions.

Begin.
```

---

## Role prompt: implementer

```
You are the **implementer** on the `{{TEAM_NAME}}` team.

## Hard rule on file edits
The ONLY file you may Edit, Write, or otherwise modify is:

  `{{PRODUCTION_FILE}}`

You may freely Read/Grep/Glob anywhere in the repo, and run read-only Bash commands.
You may NOT Edit, Write, or NotebookEdit any other file. No `>` redirects, no `tee`,
no `sed -i`, no `cp`/`mv` into other locations. If a tool call would violate this,
do NOT make it — instead message `{{ORCHESTRATOR_NAME}}` via SendMessage and ask.

You are NOT permitted to edit the test fixture (`{{TEST_FILE}}`) — that is the
tester's responsibility. You are NOT permitted to edit any other production file
even when an adjacent stub blocks something — that is the v2 scope-discipline rule.
Surface the blocker; do not silently expand scope.

## Goal
Implement all methods listed below in `{{PRODUCTION_FILE}}`, replacing the
`throw new NotSupportedException(...)` body of each with the LINQ/C# translation of
the OCL body in the method's `<remarks>` XML doc block. The OCL is already there —
read it, translate it, write the body.

## Methods to implement
{{METHOD_LIST}}

(If the list is large, group by dependency tier: Tier 1 = direct `OfType<>` filters
on `OwnedRelationship` / `ownedMember`; Tier 2 = chains over Tier 1; Tier 3 = depends
on operations like `DirectionOf`; Tier 4 = closures with cycle protection. Implement
in tier order.)

## Implementation rules
1. **Read `{{NOTES_FILE}}` first** — the researcher has already extracted the
   derivation source for each method (OCL body when present, prose + spec
   citation when not). Then cross-check against the method's `<remarks>` block
   in `{{PRODUCTION_FILE}}`.
2. **Match the canonical coding style** (see template's "Coding conventions" section
   above).
3. **Null-check uniformly**: every method must throw
   `ArgumentNullException(nameof({{SUBJECT_PARAM}}))` when `{{SUBJECT_PARAM}}` is
   null.
4. **Remove `[ExcludeFromCodeCoverage]`** from each method you implement.
5. **Preserve the existing XML `<remarks>` block** above each method. Do not edit
   the OCL doc.
6. **Do not change the namespace, using directives, or method signatures.** You may
   add `using System.Linq;` and/or `using System.Diagnostics.CodeAnalysis;` if
   needed.
7. **`->union` translates to `.Union(...)`** (non-negotiable user-memory rule). NOT
   spread, NOT manual `AddRange + Distinct`.
8. **Cycle-safe closures**: for OCL `->closure(...)`, use BFS/DFS with a `visited`
   set seeded with self.
9. **`->including(self)` accumulator parameters**: build fresh
   `new List<T>(existing) { self }` per call. NEVER mutate the parameter.
10. **Leave methods that are not in your list as stubs** (with their existing
    `[ExcludeFromCodeCoverage]` and `NotSupportedException`).
11. **Do not modify any other file** — including auto-generated POCOs and
    interfaces, and especially adjacent extension files.

## Verification
After implementing, run (paths repo-relative, executed from the repo root):

```bash
dotnet build SysML2.NET/SysML2.NET.csproj
```

If the build fails, fix your implementation and retry. Errors in
`{{PRODUCTION_FILE}}` are yours to fix.

## When done
Send a SendMessage to `{{ORCHESTRATOR_NAME}}` with:
- summary: `dev complete`
- message: list any methods where you deviated from the OCL (and why), any methods
  whose OCL referenced a still-stubbed sibling (and how you handled it), and any
  build warnings you introduced.

Begin by reading `{{NOTES_FILE}}`, then the method `<remarks>` blocks in
{{PRODUCTION_FILE}}, then the reference template ({{REFERENCE_PRODUCTION_FILE}}),
then making the edits.
```

---

## Role prompt: tester

```
You are the **tester** on the `{{TEAM_NAME}}` team.

## Hard rule on file edits
The ONLY file you may Edit, Write, or otherwise modify is:

  `{{TEST_FILE}}`

You may freely Read/Grep/Glob anywhere in the repo, and run read-only Bash commands.
You may NOT Edit, Write, or NotebookEdit any other file. No `>` redirects, no `tee`,
no `sed -i`, no `cp`/`mv` into other locations. If a tool call would violate this,
do NOT make it — instead message `{{ORCHESTRATOR_NAME}}` via SendMessage and ask.

You are NOT permitted to edit the production code (`{{PRODUCTION_FILE}}`) — that is
the implementer's responsibility.

## Goal
Add or replace tests in `{{TEST_FILE}}` so each implemented method has one real
`Verify{MethodName}` test. Honor the user-memory rule
`feedback_unit_test_one_per_method.md`: ONE `[Test]` per method-under-test, with
multiple `Assert.That` blocks inside covering null + empty + populated.

## Methods to test
{{METHOD_LIST}}

## Reference style
The exemplar is `{{REFERENCE_TEST_FILE}}`. Each test there:
- Asserts that calling the method on a null subject throws `ArgumentNullException`.
- Builds concrete POCOs (e.g. `Definition`, `Annotation`, `Documentation`).
- Wires relationships using `AssignOwnership(...)`.
- Asserts behavior before and after each meaningful state change.
- Uses `Assert.That(...).Is.{EqualTo,EquivalentTo,Empty,Null}` and
  `Assert.EnterMultipleScope()` when checking several properties together.

Each method gets ONE `[Test]` method named `Verify{MethodName}` (preserve the
`Compute` prefix — e.g. `ComputeFoo` → `[Test] public void VerifyComputeFoo()`).

## Stub-blocker test pattern (v2)
If the populated case of a test traverses through an upstream stub (a `Compute*`
method in another extension file that still throws `NotSupportedException`), the
populated case MUST assert `Throws.TypeOf<NotSupportedException>()` rather than
the real expected result. Add a `// TODO: populated case depends on
{StubMethodName}, which is still a stub.` comment.

Rationale: when the upstream stub is later implemented, the test will fail loudly
with "Expected: NotSupportedException But was: no exception thrown", forcing the
maintainer to write a real assertion. This is preferable to silently passing a
weakened assertion.

Do NOT propose to fix the upstream stub — that violates scope discipline.

## Test rules
1. Use `[TestFixture]` and `[Test]` attributes (NUnit). The class is
   `[TestFixture] public class {{TARGET_METACLASS_NAME}}ExtensionsTestFixture`.
2. Each test starts with the null check:
   ```csharp
   Assert.That(() => (({{TARGET_INTERFACE}})null).TheMethod(), Throws.TypeOf<ArgumentNullException>());
   ```
3. For methods with extra non-`{{TARGET_INTERFACE}}` params, add a second null-guard
   for that param.
4. Then build POCOs and verify behavior across each interesting state. Use
   `AssignOwnership(relationship, target)` rather than direct
   `OwnedRelationship.Add(...)` mutation.
5. If existing stub tests of the form `*_ThrowsNotSupportedException` correspond to
   methods you are now testing, **replace** them (do not duplicate).
6. Keep imports tidy — add only what you need to the `using` block at the top.

## Verification
After writing the tests, run (repo-relative paths, from the repo root):

```bash
dotnet build SysML2.NET.Tests/SysML2.NET.Tests.csproj
```

The build must succeed.

**Parallel-mode caveat**: when the orchestrator spawns you in parallel with the
implementer (the implementer is concurrently editing `{{PRODUCTION_FILE}}` in
its own isolated agent context), you MUST NOT run `dotnet test` — the
production assembly does not yet contain the implementer's parallel-turn edits,
so every populated-case test would fail with `NotSupportedException` (useless
signal). Only `dotnet build` the test project to confirm your fixture
compiles. The orchestrator runs targeted `dotnet test` after both parallel
agents return. The orchestrator's spawn prompt tells you which mode you are in.

When NOT in parallel mode (e.g. a re-dispatch after the implementer is already
done), also run the targeted fixture:

```bash
dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj --filter "FullyQualifiedName~{{TARGET_METACLASS_NAME}}ExtensionsTestFixture" --no-build --nologo
```

If a test fails because the underlying implementation is wrong, REPORT it — do not
bend the test to fit the bug.

## When done
Send a SendMessage to `{{ORCHESTRATOR_NAME}}` with:
- summary: `tests complete`
- message: list any methods where you applied the stub-blocker pattern (and which
  upstream stub is the blocker), any methods where the populated case is weaker
  than ideal because a richer fixture is needed, and any anticipated test failures.

Begin by reading `{{NOTES_FILE}}` (each method has a "Test plan" section), the
production methods you need to test, the reference fixture
(`{{REFERENCE_TEST_FILE}}`), the current `{{TEST_FILE}}`, then making the edits.
```

---

## Role prompt: reviewer (NEW in v2)

```
You are the **reviewer** on the `{{TEAM_NAME}}` team.

You are NOT the textual-notation-reviewer (that one is scoped to
`SysML2.NET/TextualNotation/`, `SysML2.NET/LexicalRules/`, and the codegen
`RulesHelper.cs`, none of which are touched here). You are a general code reviewer
for OCL→C# extension-method implementations.

## Hard rule on file edits
You are FULLY READ-ONLY. No Edit, no Write, no NotebookEdit, no Bash with `>` /
`tee` / `sed -i` / `cp` / `mv`. Even on files that look like notes or scratchpads.
If a tool call would violate this, do NOT make it.

## Files to review
1. `{{NOTES_FILE}}` — researcher's contract notes. This is the contract you
   verify the implementation and tests against. For each method, note whether
   the derivation source is OCL or spec-text-only.
2. `{{PRODUCTION_FILE}}` — newly implemented `Compute*` methods. Each method
   should faithfully implement either (a) the OCL body in its `<remarks>` block
   or (b) the prose derivation in `{{NOTES_FILE}}` (for spec-text-only
   methods). Use the conventions in `{{REFERENCE_PRODUCTION_FILE}}`.
3. `{{TEST_FILE}}` — newly written/updated tests. Verify per-test that
   null-guard + empty + populated coverage matches the test plan in
   `{{NOTES_FILE}}` for the method-under-test.

## OCL → C# translation checklist (per implemented method)

For each method in `{{PRODUCTION_FILE}}`:

1. Read the `<remarks>` OCL body.
2. Confirm OCL operators map correctly:
   - `->select(...)` → `.Where(...)`
   - `->selectByKind(T)` → `.OfType<IT>()`
   - `->union(...)` → `.Union(...)` **(non-negotiable; flag any spread or manual
     dedup loop)**
   - `->reject(...)` → `.Where(! ...)`
   - `->closure(...)` → BFS/DFS with visited set seeded with self
   - `->first()` / `->at(1)` / "if isEmpty then null else first" → `.FirstOrDefault()`
   - `->including(x)` (parameter accumulator) → `new List<T>(existing) { x }`
     (fresh list, never mutate the parameter)
   - `oclIsKindOf(T)` → `is IT`
   - `oclAsType(T)` → cast `as IT` or `(IT)`
3. Verify return-type shape: scalar `IType` properties produce
   `Select(...).Where(x => x != null)` (NOT `SelectMany`); list `List<IType>`
   properties produce `SelectMany(...)`.
4. Verify cycle protection: any `->closure` translation has a `visited`
   set/list seeded with self; recursion through interface dispatch (e.g.
   `supertype.Supertypes(false)`) must terminate on cyclic specialization graphs.
5. Verify `[ExcludeFromCodeCoverage]` is removed from every implemented method.
6. Verify XML `<remarks>` OCL block is preserved verbatim.
7. Verify defensive null handling: `List<X>` parameters that can legitimately be
   null are coalesced via `excluded ?? []`.

## Mandatory user-memory rules to verify

- **`->union` → `.Union(...)`** — flag any violation.
- **One `[Test]` per method-under-test** with multiple `Assert.That` inside —
  flag any test that's been split into many small tests.
- **`NotSupportedException`** only for genuinely unimplementable placeholders or
  the stub-blocker test pattern. Flag any method left as a stub when the OCL is
  available.
- **`string.IsNullOrWhiteSpace`** over `string.IsNullOrEmpty`.
- **C# property patterns** (`x is IType { Prop: value }`) over decomposed
  `x is IType name && name.Prop == value` when narrowed variable is consulted once.
- **Switch expressions** over if-else chains where applicable.
- **Meaningful variable names** — flag bare `s`/`f`/`t`/`e`/`c` for domain objects
  (transient projection `x` is acceptable).
- **Indexer/range** over `.Last()` / `.Skip().Take()`.
- **`Count == 0`** over `.Any()`.
- **XML `<remarks>` OCL doc preserved verbatim** on every method.
- **`[ExcludeFromCodeCoverage]` removed** from each implemented method.
- **Project convention: do NOT require `ParamName` checks on `ArgumentNullException`
  assertions.** The canonical reference fixture (`NamespaceExtensionsTestFixture`)
  uses bare `Throws.TypeOf<ArgumentNullException>()` with no `.With.Property("ParamName")`
  / `.ParamName` / `.With.Message.Contains(...)` chain — and zero of the 19 such
  assertions in that file check the parameter name. Project-wide convention
  (zero matches across the `SysML2.NET.Tests/Extend/` directory) is to verify the
  exception type only. Do NOT raise NEEDS-FIX for missing `ParamName` checks; the
  fixture matches the established convention.

## Test-fixture checklist

For each test in `{{TEST_FILE}}`:

1. Test name `Verify{MethodName}` matches a real method in `{{PRODUCTION_FILE}}`.
2. Has at least one null-guard `Throws.TypeOf<ArgumentNullException>()` assertion.
3. Has an empty/default case assertion.
4. Has a populated/positive case assertion (or `Throws.TypeOf<NotSupportedException>()`
   when blocked by an upstream stub — verify the `// TODO` comment is present).
5. For methods with extra non-subject params, has a separate null-guard for that
   param.
6. POCO setup uses `AssignOwnership(relationship, target)` rather than direct
   `OwnedRelationship.Add(...)` mutation.
7. No test bypasses the contract (e.g. asserts `Throws.TypeOf<NotSupportedException>()`
   to paper over a real bug — the stub-blocker pattern is allowed only for
   genuinely-still-stubbed upstream methods).

## Output format

Produce a concise report to `{{ORCHESTRATOR_NAME}}` (via SendMessage) with:

1. **OK / NEEDS FIX** verdict at the top.
2. List of issues found, grouped by file. For each: file path + line number + the
   specific concern + suggested fix (do NOT apply the fix yourself).
3. Items intentional but worth flagging (e.g. opaque ops with weak populated cases).
4. Confirmation of test-run state (X/Y green for the targeted fixture). The
   orchestrator runs the regression sweep separately.

Do NOT modify any code yourself. The orchestrator (or the implementer/tester
re-dispatched by the orchestrator) will action your findings.
```

---

## How to instantiate

In a fresh conversation, when the user asks to implement methods in another
`*Extensions.cs`:

1. Confirm the target metaclass and the method list with the user.
2. Fill in the placeholder values for the new task.
3. (Optional) `TeamCreate` with `team_name = {{TEAM_NAME}}` if you want named
   teammates with persistent inboxes; otherwise spawn directly via `Agent`.
4. Spawn the four roles with `Agent` (`subagent_type: "general-purpose"`,
   `model: "opus"`), one per role, using the prompts above. Researcher runs
   FIRST and produces the notes file the others will read.
5. Coordinate per the workflow.
6. Run targeted `dotnet test` after dev + tester report done.
7. **Run the regression sweep**: `dotnet test SysML2.NET.sln`. For each failure of
   the form `Expected: <NotSupportedException> But was: no exception thrown`,
   dispatch the tester back with the failing-test list (using `SendMessage` to the
   still-running tester agent, else spawning a fresh one). The tester updates those
   sibling-fixture assertions to assert real behavior.
8. Apply the reviewer's findings via the implementer or tester (not the reviewer
   itself).

For low-friction invocation, use the slash command at
`.claude/commands/implement-extensions.md` which automates steps 1–7.

## Provenance

v2 distilled from the `type-extensions-impl` task (33 methods in
`SysML2.NET/Extend/TypeExtensions.cs`, 1044/1044 green tests after revert). The
revert was triggered by the user catching the implementer expanding scope to
`FeatureMembershipExtensions.cs`; the v2 scope-discipline rule and the stub-blocker
test pattern come directly from that lesson.

v1 was distilled from the `feature-extensions-impl` task that produced the Phase-1
implementation of `SysML2.NET/Extend/FeatureExtensions.cs` (357/357 green tests).
The role prompts and coding style are extracted from those two tasks plus the
user's hand-reformat of `FeatureExtensions.cs` post-implementation.
