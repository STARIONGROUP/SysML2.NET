# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SysML2.NET is a .NET C# SDK implementing the OMG SysML v2 specification (based on Beta 4 pilot implementation). It provides metaclass DTOs/POCOs, serializers (JSON, XMI, MessagePack), a REST client, a DAL layer, and a Blazor WebAssembly viewer application. Current version: 0.19.0.

## Build & Test Commands

```bash
# Restore and build entire solution
dotnet restore SysML2.NET.sln
dotnet build SysML2.NET.sln

# Run all tests
dotnet test SysML2.NET.sln

# Run tests for a specific project
dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj
dotnet test SysML2.NET.Serializer.Json.Tests/SysML2.NET.Serializer.Json.Tests.csproj

# Run a single test by name
dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj --filter "FullyQualifiedName~AcceptActionUsageExtensionsTestFixture"

# Run with coverage (as CI does)
dotnet-coverage collect "dotnet test SysML2.NET.sln --no-build" -f xml -o coverage.xml
```

Test framework: **NUnit**. Test classes use `[TestFixture]` and `[Test]` attributes.

**When writing or modifying unit tests** in any `*.Tests/` project: read `TESTING.md` at the repo root for the NUnit conventions (one `[Test]` per method-under-test, `Assert.That` everywhere, `Assert.EnterMultipleScope` only for consecutive asserts, mandatory positive + negative coverage, assertion idiom preferences, `Verify{MethodUnderTest}` naming).

## Architecture

### Code Generation

- favour duplicated code in codegeneration to have staticaly defined methods that provide performance over reflection based code.
- code generation is done by processing the UML model and creating handlebars templates
- **When working on the grammar/textual notation code generator** (`SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs` and related grammar processing): read `SysML2.NET.CodeGenerator/GRAMMAR.md` for the KEBNF grammar model, cursor/builder conventions, and code-gen patterns already handled.

### Textual notation reviewer is MANDATORY

**Every code change touching any of the following paths MUST be verified by the `textual-notation-reviewer` agent before reporting the change as complete or committing:**

- Every file under `SysML2.NET.Serializer.TextualNotation/Writers/` — both hand-coded partials (`*.cs`), the generated `AutoGenTextualNotationBuilder/*.cs`, `IsValidFor` guard extensions (`TextualNotationValidationExtensions.cs`), and any membership / string / cursor helpers that sit beside them.
- Every file under `SysML2.NET/LexicalRules/` — both hand-coded members and the generated `AutoGenLexicalRules/*.cs` (`Keywords`, `SymbolicKeywordKind`, `SymbolicKeywordKindExtensions`).
- `SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs` and any Handlebars template under `SysML2.NET.CodeGenerator/Templates/Uml/` that emits textual-notation or lexical-rules code.

**The KEBNF grammar context applies to ALL of these locations** — not just the generator. When implementing or reviewing hand-coded methods in `SysML2.NET.Serializer.TextualNotation/Writers/`, the author and the reviewer must re-ground in:
- `SysML2.NET.CodeGenerator/GRAMMAR.md` — the cursor / builder conventions and patterns
- `Resources/SysML-textual-bnf.kebnf` and `Resources/KerML-textual-bnf.kebnf` — the grammar source of truth
- The rule's `<para>{…}</para>` XML doc on the generated sibling method (if the method is a HandCoded companion)

The agent is defined at `.claude/agents/textual-notation-reviewer.md`. Invoke it with the rule(s) being implemented, the KEBNF text, and the file paths to review. It enforces:
- the `Move()` ↔ `+=` Golden Rule (cursor advances only on `+=` consumption; direct `cursor.Move()` calls are forbidden after any callee that already advances the cursor internally)
- EBNF quantifier semantics (`?` = 0..1 → single `if`; `*` = 0+ → `while` loop; `+` = 1+ → emit-once then loop)
- correct runtime type discriminators (e.g. `ISpecialization` IS the cursor element, not wrapped in `IOwningMembership`)
- absence of greedy-builder pitfalls that silently drop interleaved elements
- consistency between the hand-coded method and the grammar rule it implements (name, target type, element order, alternatives)

Reason this is mandatory: reviewer passes have caught real grammar-correctness bugs (wrong discriminator, silent element drop, missing `*` loop, spurious double-`Move()` in `FeatureSpecialization*` loops) that would have shipped broken textual notation without failing any existing test.

### Code Generation Pipeline

Most code in this repo is **auto-generated** — files marked `THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!` must not be edited directly.

The pipeline:
1. **Input**: `Resources/KerML_only_xmi.uml` and `Resources/SysML_only_xmi.uml` — these two UML-based XMI files define the KerML and SysML v2 specification respectively. They are the **single source of truth** for all generated DTOs, POCOs, serializers, extension methods, and other auto-generated code. All OCL constraints (derivation rules, validation invariants, and operation body conditions) for each metaclass are also defined within these XMI files.
2. **Generator**: `SysML2.NET.CodeGenerator` reads these via `uml4net.xmi`, uses Handlebars templates (`Templates/Uml/*.hbs`) to generate code
3. **Output**: `AutoGen*` directories across multiple projects

Generator classes in `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/` produce:
- DTOs and interfaces → `SysML2.NET/Core/AutoGenDto/`
- POCOs → `SysML2.NET/Core/AutoGenPoco/`
- Enums → `SysML2.NET/Core/AutoGenEnum/`
- JSON serializers/deserializers → `SysML2.NET.Serializer.Json/Core/AutoGenSerializer/` and `AutoGenDeSerializer/`
- MessagePack formatters → `SysML2.NET.Serializer.MessagePack/`
- Extension methods (Extend) → `SysML2.NET/Extend/`
- DAL factories → `SysML2.NET.Dal/Core/`

### Formal specification references

The XMI files (`Resources/KerML_only_xmi.uml`, `Resources/SysML_only_xmi.uml`) define the **structure** of the metamodel and the OCL constraints. The KEBNF files (`Resources/SysML-textual-bnf.kebnf`, `Resources/KerML-textual-bnf.kebnf`) define the **concrete textual syntax**. Neither narrates the *semantics* or *intent* of a metaclass, the rationale behind an OCL constraint, the contract of the REST API, or the idiomatic use of a notation construct. For that, this repo carries the formal OMG specification texts (PDF→text) under `Resources/specification/`. Treat them as cross-references — not as a generation input.

- `Resources/specification/1-Kernel_Modeling_Language.pdf.txt` — *Kernel Modeling Language (KerML) Version 1.0* (OMG formal/2026-03-01). Consult when working with metaclasses in the `Root.*`, `Core.*`, and `Kernel.*` namespaces (under `SysML2.NET/Core/AutoGenDto/` and `AutoGenPoco/`), when an OCL constraint is unclear, or when reasoning about element/relationship/feature/classification semantics that the XMI does not spell out.
- `Resources/specification/2a-OMG_Systems_Modeling_Language.pdf.txt` — *OMG Systems Modeling Language (SysML) Version 2.0, Part 1: Language Specification* (OMG formal/2026-03-02). Consult when working with the systems-engineering-specific metaclasses in `Systems.*` namespaces — Parts, Ports, Connections, Interfaces, Actions, States, Interactions, Requirements, Constraints, Use Cases, Analysis/Verification Cases, Views, Metadata — and to ground the Definition/Usage pattern.
- `Resources/specification/3-Systems_Modeling_API_and_Services.pdf.txt` — *Systems Modeling API and Services Version 1.0* (OMG formal/2026-03-04). Consult when working in `SysML2.NET.REST/`, `SysML2.NET/PIM/`, `SysML2.NET.Serializer.Dictionary/`, or `SysML2.NET/ModelInterchange/`. Defines the Platform-Independent Model (ProjectService, ElementNavigationService, ProjectDataVersioningService, QueryService, ExternalRelationshipService, ProjectUsageService) and the REST/HTTP and OSLC PSMs.
- `Resources/specification/Intro to the SysML v2 Language-Textual Notation.pdf.txt` — SST tutorial, Release 2026-03. Informative companion to the KEBNF grammar; consult for canonical examples and idioms when implementing or reviewing rules under `SysML2.NET.Serializer.TextualNotation/Writers/` and `SysML2.NET/LexicalRules/`.
- `Resources/specification/Intro to the SysML v2 Language-Graphical Notation.pdf.txt` — SST tutorial, Release 2026-03. Consult when working on `SysML2.NET.Viewer/` (Blazor) for the visual-rendering conventions of each metaclass family.

These text files are large (PDF-converted, up to 1.3 MB) and the conversion is not always clean. Read them with `Read` `offset`/`limit` and use `Grep` to jump to chapter/section anchors (e.g. `^7\.\d+`, `Clause 8\.`, or a metaclass name) rather than loading whole files into context.

### Grounding SysML v2 / KerML work with the Hypha plugin

If the **Hypha** plugin is installed, treat it as the grounding step **before** implementing or reviewing anything that depends on the SysML v2 / KerML metamodel — do not rely on a sibling analogue, the doc-comment OCL, or prior knowledge as the source of truth. The metamodel is large and precise; a plausible prior is exactly what produces confident-but-wrong derivations.

**If the Hypha plugin is *not* installed:** the first time a task in this session would benefit from this grounding (any of the situations below), inform the user once that the Hypha plugin exists and that installing it is recommended for accurate SysML v2 / KerML work — it surfaces the metamodel structure and normative spec text on demand. Then proceed using the checked-in sources as the fallback source of truth: the XMI metamodel (`Resources/KerML_only_xmi.uml`, `Resources/SysML_only_xmi.uml`) for structure and OCL, and `Resources/specification/` for intent. Do not repeat the recommendation on every subsequent task.

This applies whenever you are about to:
- implement or modify a `Compute*` derived-property / OCL computation under `SysML2.NET/Extend/`,
- reason about a metaclass's features, multiplicities, ordering, redefinitions/subsettings, or constraints,
- implement or review a textual-notation / lexical rule, or
- make a claim about what the SysML v2 / KerML specification requires.

Ground on **two axes** — structure *and* intent — because the metamodel gives you the *what* but not the *why*:

- **`hypha:metamodel-lookup` — structure (always).** A metaclass's type, multiplicity, **ordering**, redefinitions/subsettings, supertypes/subtypes, and the derivation/constraint OCL. This is the default, always-on step. (For cross-cutting fan-out questions spanning many metaclasses, the `hypha:metamodel-navigator` agent.)
- **`hypha:spec-citation` — intent (when the derivation involves interpretation).** The OCL is a *formalization, not an explanation*: it says what to compute, not why the concept exists, what a defined term means, or how an underspecified edge case should behave. Consult the specification for the rationale and semantics whenever the OCL is terse, ambiguous, leans on a defined term (e.g. `namingFeature`, `redefinedFeature`, connector `end`, feature typing/inheritance resolution), or otherwise needs interpretation beyond a mechanical filter — so the C# translation is not merely syntactically faithful but semantically correct. Skip it only when the OCL is genuinely mechanical (e.g. a plain `selectByKind`) and unambiguous.
- **`hypha:sysml-validation`** — validate `.sysml` / `.kerml` textual notation against the grammar and metamodel.

Ground first, then implement against the verified contract. Two concrete examples of why:
- **Structure the OCL comment hides:** `ActionDefinition::action` is declared `ordered` in the metamodel — a fact the OCL comment alone does not surface and a sibling analogue may satisfy only by accident. Confirm via `hypha:metamodel-lookup`.
- **Intent the OCL comment cannot express:** an OCL body that reads `->first()` or `->at(1)` is picking *one* of many, but only the spec prose says *on what basis* (e.g. the most specific redefinition) — translate it faithfully to that intent, not as an arbitrary first-element grab. Confirm via `hypha:spec-citation`.

### Project Dependency Graph

```
SysML2.NET (core: netstandard2.1)
  ├── Core/AutoGenDto/     - 342 files: DTO classes + interfaces (171 metaclasses × 2)
  ├── Core/AutoGenPoco/    - POCO classes + interfaces
  ├── Core/AutoGenEnum/    - Enums (FeatureDirectionKind, VisibilityKind, etc.)
  ├── Core/DTO/            - Hand-coded base: IElement : IData
  ├── Core/POCO/           - Hand-coded: IContainedElement, IContainedRelationship
  ├── Extend/              - Auto-generated extension methods per metaclass
  ├── Decorators/          - [Class], [Property], [Implements] attributes from UML
  ├── PIM/                 - Platform-Independent Model DTOs (REST API types)
  ├── ModelInterchange/    - Archive/project interchange types (kpar support)
  └── Common/IData.cs      - Base interface with Id property

SysML2.NET.Extensions        - Comparers, utilities across metaclasses
SysML2.NET.Serializer.Json   - JSON (de)serialization via System.Text.Json
SysML2.NET.Serializer.Xmi    - XMI (de)serialization
SysML2.NET.Serializer.MessagePack - MessagePack binary serialization
SysML2.NET.Serializer.Dictionary  - Dictionary-based serialization (PIM)
SysML2.NET.Serializer.TextualNotation - Writers/, Writers/AutoGenTextualNotationBuilder/, validation extensions, cursor helpers
SysML2.NET.Dal               - Data Access Layer (Assembler, ElementFactory)
SysML2.NET.REST              - REST client + Session for SysML2 API servers
SysML2.NET.Kpar              - Reader/Writer for .kpar archive format
SysML2.NET.Viewer            - Blazor WebAssembly app (net9.0)
SysML2.NET.CodeGenerator     - Code generation tool (net10.0, not packaged)
```

### DTO vs POCO Pattern

Each metaclass exists in two forms:
- **DTO** (Data Transfer Object): Lightweight, uses `Guid` references for relationships. Used for serialization/transport. Properties reference other elements by `Guid` ID.
- **POCO** (Plain Old CLR Object): Rich object model with resolved object references. Used for in-memory manipulation. Uses `ContainerList<T>` for containment relationships.

Both share the same `I{MetaclassName}` interface from `AutoGenDto/`. The hand-coded `Core/DTO/IElement.cs` adds `IData` (which provides `Guid Id`) to the root interface.

### Namespace Convention

Auto-generated DTOs use structured namespaces reflecting the KerML/SysML package hierarchy:
- `SysML2.NET.Core.DTO.Root.Elements` (Element, Annotation, etc.)
- `SysML2.NET.Core.DTO.Core.Types` (Type, Feature, Classifier, etc.)
- `SysML2.NET.Core.DTO.Systems.Actions` (ActionUsage, etc.)

### Target Frameworks

- Core library (`SysML2.NET`): `netstandard2.1`
- Test projects and CodeGenerator: `net10.0`
- Viewer: `net9.0` (Blazor WebAssembly)

## Key Conventions

- **Paths are ALWAYS repo-relative — NEVER absolute.** This rule applies to every path the agent writes anywhere: code comments, XML doc `<see cref="…"/>` and prose, source-string citations, error/log messages, commit messages, PR bodies, GitHub issue bodies, `.team-notes/` spec files, plan files, skill prompts and agent briefs (e.g. say `SysML2.NET/Extend/FooExtensions.cs`, NOT `C:\CODE\SysML2.NET\SysML2.NET\Extend\FooExtensions.cs` and NOT `/c/CODE/SysML2.NET/...`). Use forward slashes. Reason: absolute paths are user-/machine-specific and leak the local filesystem into the repo and into communication with other contributors — they break for anyone else, get stale on rename/move, and are noisy. The ONLY exception is the `Read` / `Edit` / `Write` tool `file_path` parameter, which the tool implementation requires to be absolute — those tool arguments are not user-visible artifacts. Everything you author as content must be repo-relative.
- Commit messages use prefix tags: `[Add]`, `[Update]`, `[Remove]`, `[Fix]` — except for issue-fixing commits produced by `/implement-extensions` and `/implement-extensions-batch`, which use the canonical short form `Fix #<n>` (single issue) or `Fix #<n1> #<n2> …` (batch) so GitHub auto-closes the issues on merge.
- Main branch: `master`. Development branch: `development`. **All feature work targets `development`** via PR; `master` is downstream only.
- CI: GitHub Actions (`CodeQuality.yml`) — builds, tests, and runs SonarQube analysis
- License: Apache 2.0 (code), LGPL v3.0 (metamodel files)
- To add a new metaclass: update the UML XMI source files, then run the code generators — do not manually create AutoGen files

## Branch & PR workflow (MANDATORY)

Direct pushes to `development` or `master` are forbidden. All work lives on a feature branch.

**Agent boundaries are strict and minimal**:

1. The agent **must NOT auto-commit, EVER.** `git commit` is the user's responsibility — no exceptions, no asking, no "for convenience". The user reviews `git diff` and commits manually.
2. The agent **must NOT push commits, open PRs, or merge by default.** Push + PR + merge are the user's job too. The agent only performs push/PR if the user explicitly asks for them in-conversation; otherwise it stays out of git remote operations entirely.
3. **When the agent creates a branch** (typically inside `/implement-extensions-batch` step 6), it must:
   - create it locally with `git switch -c <branch> origin/development`, AND
   - **immediately push the empty branch to `origin`** with `git push -u origin <branch>`, so the remote ref exists at the same commit as `origin/development` and the user's later push of the actual commit becomes a trivial fast-forward.
   This is the only push the agent performs by default. It is safe because the branch tip equals `origin/development`'s tip — no new commits, no force flags, no risk of overwriting.
4. **At the end of any task that creates a branch**, the agent stops with a final summary that includes:
   - the in-scope files modified, the test counts, the reviewer verdict, etc.,
   - a **pre-filled commit message** (`Fix #<n>` for single-issue runs, `Fix #<n1> #<n2> …` for batches — single line, no body, no `Co-Authored-By` trailer, no "🤖 Generated with …" footer),
   - a handoff line telling the user how to stage + commit + push the resulting commit themselves. Example:
     > Review `git diff`, stage the in-scope files (`git add <path> …` — NEVER `-A` / `.`), commit with the message above, then `git push` (the remote branch already exists, so this is a fast-forward — no `-u` needed). Open the PR yourself via the GitHub UI or `gh pr create --base development`.
   - This is the end of the agent's involvement. **The agent does NOT proceed to push the commit, does NOT open the PR**, unless the user explicitly asks. Typical case: the user handles both.

**If the user does explicitly ask the agent to push or open the PR** (rare; user-initiated only):
- The agent verifies: current branch is not `development`/`master`, `git log -1` matches the canonical `Fix #<n>…` form, `git status --porcelain` is empty.
- Then `git push origin <branch>` — NEVER `--force`, NEVER `--force-with-lease`, NEVER `--no-verify`.
- Then `gh pr create --base development --head <branch> --title "Fix #<n>…" --body-file <pr-body-tmp>` — NEVER `--base master`, NEVER `--draft` unless the user asked.

**Failure modes**:
- `git push -u origin <branch>` (step 3) fails because the branch already exists on origin → abort, surface to user, do not force.
- Branch creation requested but the current branch is `development` or `master` AND the user asked for in-place work → REFUSE. Feature work must live on a feature branch first.
- If the user asks the agent to push a commit and that commit was made by the agent (somehow), refuse and surface the policy violation. The agent's commits are forbidden by construction; if one exists, it is a bug that needs human review.

**Why this split**: the user is the reviewer of record. The commit is the review and the push is the delivery — both are the user's calls. The agent's git involvement is bounded to: (a) create the branch locally + push the empty ref (so the user's push later is frictionless), and (b) leave the rest alone. This was tightened after two failures: first the agent auto-pushed branches to `development` directly, then over-corrected by auto-committing on the user's behalf.

## Quality rules

- Prefer comparing 'Count' to 0 rather than using 'Any()', both for clarity and for performance
- Use 'StringBuilder.Append(char)' instead of 'StringBuilder.Append(string)' when the input is a constant unit string
- Prefer 'string.IsNullOrWhiteSpace' over 'string.IsNullOrEmpty' when checking the non-nullable value of a string
- Prefer switch expressions/statements over if-else chains when applicable
- Prefer LINQ as much as possible — including for projection / filter / aggregation over collections (`items.Where(...).Select(...).ToList()`, `result.AddRange(items.Select(...))`, `items.Any(predicate)`, etc.) instead of hand-rolled `foreach` + `if` + `.Add()` loops. The ONE exception is straightforward positional or range access on a concrete `List`/array: `list[^1]` beats `list.Last()`, `array[1..^1]` beats `array.Skip(1).SkipLast(1)` — indexer/range syntax is more performant there. Outside that narrow exception, LINQ wins for clarity AND maintainability.
- **Flatten a `foreach` with a leading-`if` filter by pushing the predicate into a `.Where(...)` clause on the iterated source.** When a `foreach` body opens with `if (predicate) { … }` or `if (!predicate) { continue; }` and that's the only thing gating the body, move the predicate into a `.Where(...)` on the foreach source so the loop body is the unguarded action: write `foreach (var x in xs.Where(x => predicate))` instead of `foreach (var x in xs) { if (!predicate) { continue; } … }`. Same for `.OfType<T>()` instead of a runtime `is`-check + cast. Applies to nested loops too — push each level's filter onto its own iterator. The body should be the action, not the guard. The narrow exceptions are: (a) the predicate has observable side-effects (e.g. `visited.Add(x)`) and the iteration order must be preserved, where the LINQ form changes timing; (b) the predicate is too long to read inline — extract it to a named local function or method and still call it from the `.Where(...)`.
- Prefer C# collection expressions (`[a, b, c]`, `[..xs]`, `[]`) over `new[] { ... }`, `new List<T> { ... }`, `new T[] { ... }` when constructing a collection. Applies to both production code AND tests (e.g. `Is.EqualTo([classifier1, classifier2])` not `Is.EqualTo(new[] { classifier1, classifier2 })`, `return [];` not `return new List<T>();`). Fall back to explicit construction only when type inference cannot pick the right collection type.
- Use meaningful variable names instead of single-letter names in any context (e.g., 'charIndex' instead of 'i', 'currentChar' instead of 'c', 'element' instead of 'e')
- Use 'NotSupportedException' (not 'NotImplementedException') for placeholder/stub methods that require manual implementation
- Prefer C# property patterns ('x is IType { Prop: value }') over declared-variable-plus-predicate form ('x is IType name && name.Prop == value') when the narrowed variable is only consulted once; the property-pattern form is more concise and intent-revealing
- **Always use C# auto-properties** (`public T Foo { get; private set; }`, `public T Foo { get; init; }`, `public T Foo { get; }`) — NEVER pair a private backing field with an expression-bodied or full-getter property when there is no non-trivial logic (validation, normalisation, lazy init, event firing). Mere storage is never a justification for a backing field; the compiler collapses auto-properties to the same IL.
- **For test fixtures: default to ONE `[Test]` method per class / method-under-test** packing every scenario (happy path, edge cases, null guards, alternate inputs) into multiple `Assert.That` calls inside that one test — per `TESTING.md` §2. Do NOT write one `[Test]` per scenario when the setup is shared; that produces a bloated test list and duplicated arrange boilerplate. Split into separate `[Test]` methods only when each scenario has a genuinely distinct, complex setup.
- **Prefer method-group syntax over lambda when the lambda merely invokes a no-arg method.** Both in production code and in tests, write `Assert.That(subject.ComputeFoo, Throws.TypeOf<X>())` rather than `Assert.That(() => subject.ComputeFoo(), Throws.TypeOf<X>())`; pass `subject.Handle` rather than `x => subject.Handle(x)` when wiring up an event handler; pass `string.IsNullOrWhiteSpace` rather than `s => string.IsNullOrWhiteSpace(s)` to a LINQ predicate. The method group is more concise, allocates no closure, and reads as the action itself rather than as a delegate that calls the action. Fall back to a lambda only when (a) the lambda's body does more than the bare call (transforms args, captures locals, adds null-handling), (b) the target method is overloaded and the compiler can't infer which overload to bind, or (c) the call needs explicit type arguments the method-group form cannot supply.
- Surround every braced block (`if`, `else if`, `while`, `for`, `foreach`, `switch`, `using`, `try`/`catch`/`finally`, `lock`, `do…while`, anonymous `{ }`) with a blank line on both sides — the rule does NOT apply at the very start/end of a method body, nor between a `}` and a continuation keyword (`else`, `catch`, `finally`, `while` of `do…while`) that belongs to the same control flow
- When invoking an operation or derived property on a POCO from inside an extension method, call the POCO's instance member (e.g. `subject.IsDistinguishableFrom(other)`, `subject.qualifiedName`), NOT the static `ComputeXxxOperation` / `ComputeXxx` extension method. Virtual dispatch on the POCO honors operation/property REDEFINITION in subclass POCOs; calling the static extension directly bypasses dispatch and silently skips overrides. The static-extension form is reserved EXCLUSIVELY for the C# translation of OCL `self.oclAsType(SuperType).method()` — an explicit upcast that mandates targeting the SuperType's body (e.g. `Usage::namingFeature()` → `FeatureExtensions.ComputeNamingFeatureOperation(usage)`; `OwningMembership::path()` → `RelationshipExtensions.ComputeRedefinedPathOperation(owningMembership)`)
- **`IRelationship.OwnedRelatedElement` and `IElement.OwnedRelationship` storage collections are `[0..*]` — NEVER cardinality-limited.** The [1..1] / [0..1] multiplicities that appear in the metamodel apply to *derived* / *redefined* properties (e.g. `OwningMembership::ownedMemberElement`, `FeatureMembership::ownedMemberFeature`, `SubjectMembership::ownedSubjectParameter`), NOT to the underlying storage. When implementing such a derivation, **project from the collection — do not assume positional indexing**. Two canonical shared helpers in `SysML2.NET/Extensions/ElementExtensions.cs` cover the common cases (all early-exit on the second match, no full materialisation), each with two overloads:

  - **`SingleStrict<T>`** — `[1..1]` semantics. Empty → `throw IncompleteModelException` (lower-bound violation, missing required). Single → return. 2+ → `throw MultiplicityViolationException` (upper-bound violation).
    - `SingleStrict<T>(this IEnumerable<T>, string)` — homogeneous: source is already typed `T`.
    - `SingleStrict<TResult>(this IEnumerable, string)` — heterogeneous: source is wider; bundles a `OfType<TResult>()` filter before the strict-single check.
  - **`SingleOrDefaultStrict<T>`** — `[0..1]` semantics. Empty → `return null`. Single → return. 2+ → `throw MultiplicityViolationException`.
    - `SingleOrDefaultStrict<T>(this IEnumerable<T>, string)` — homogeneous.
    - `SingleOrDefaultStrict<TResult>(this IEnumerable, string)` — heterogeneous with implicit `OfType<TResult>()`.

  ```csharp
  // [1..1] type-narrowed redefinition (e.g. SubjectMembership::ownedSubjectParameter : IUsage)
  return subject.OwnedRelatedElement.SingleStrict<ITargetType>(nameof(subject));

  // [1..1] non-narrowing redefinition (e.g. OwningMembership::ownedMemberElement : IElement)
  return subject.OwnedRelatedElement.SingleStrict<IElement>(nameof(subject));

  // [0..1] type-narrowed projection over a storage collection (e.g. ConstraintUsage::constraintDefinition : IPredicate)
  return subject.type.SingleOrDefaultStrict<IPredicate>(nameof(subject));

  // [0..1] over an already-projected stream (multi-hop chain ending in a Select/predicate)
  return subject.OwnedRelationship.OfType<ISomeRelationship>().Select(r => r.something).SingleOrDefaultStrict(nameof(subject));
  ```

  All four signatures accept `IEnumerable` / `IEnumerable<T>`; storage collections (`IElement.OwnedRelationship`, `IRelationship.OwnedRelatedElement`) and derived enumerables (e.g. `Feature::type`) bind directly — no `IReadOnlyList` overload needed.

  The failure mode each helper produces matches the **derived property's declared multiplicity** as recorded in the `[Property(lowerValue:…, upperValue:…)]` attribute on the generated POCO interface (or in the UML XMI):

  | Multiplicity | Empty projection | Single-match projection | 2+ match projection |
  |---|---|---|---|
  | `[1..1]` (lowerValue=1, upperValue=1) | `throw IncompleteModelException` (lower-bound violation, missing required) | return the match | `throw MultiplicityViolationException` (upper-bound violation) |
  | `[0..1]` (lowerValue=0, upperValue=1) | `return null` (use `SingleOrDefaultStrict<TResult>` for direct `OfType<TResult>` over a storage collection or derived enumerable; chain explicit projections then `SingleOrDefaultStrict()` for multi-hop / predicate-filtered) | return the match | `throw MultiplicityViolationException` |
  | `[0..*]` / `[1..*]` | (use `List<T>` projection; not this pattern) | n/a | n/a |

  Strictness applies **only to the final filter** of the projection chain. Intermediate filters (e.g. `OwnedRelationship.OfType<IFeatureTyping>()` in a `FeatureTyping → Type → IXxxDefinition` chain) may legitimately match many elements; the upper-bound check applies to the last `.OfType<T>()` whose result is the `[0..1]` / `[1..1]` derived value.

  **OCL gate (overrides the table for `[0..1]`):** when the OCL derivation body in the property's `<remarks><code>` block explicitly elects the first of many (`->first()`, `->at(1)`), the spec contract is "pick the first if multiple" — keep `FirstOrDefault`. The strict `[0..1]` rule applies only when the OCL has no first-picking call (or no OCL body at all), in which case the multiplicity `[0..1]` is the only contract and 2+ must surface as `MultiplicityViolationException`.

  `IncompleteModelException` and `MultiplicityViolationException` are the loud signals to SDK users that the model is malformed:
  - `IncompleteModelException` — lower-bound violation: the model is missing a required element (0 matches against a `[1..1]` property).
  - `MultiplicityViolationException` — upper-bound violation: the model carries more elements than the upper bound allows (2+ matches against a `[0..1]` or `[1..1]` property).

  DO NOT swallow them as `null` when the multiplicity demands a throw, and DO NOT raise them for the empty case when the multiplicity is `[0..1]` (a legitimately-optional property).

  Do NOT use `.Count != 1 → throw` followed by `OwnedRelatedElement[0] as ITargetType` — that pattern (a) silently drops the correctly-typed element when it does not sit at index 0 (`AssignOwnership` allows owned related elements for both `IOwningMembership` AND `IAnnotation`, so a Membership can carry annotation targets alongside the member element), and (b) always allocates a `List<T>` via `OfType<T>().ToList()` even when the answer is decidable after the first two elements.
