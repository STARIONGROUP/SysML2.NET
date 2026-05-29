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

- Commit messages use prefix tags: `[Add]`, `[Update]`, `[Remove]`, `[Fix]` — except for issue-fixing commits produced by `/implement-extensions` and `/implement-extensions-batch`, which use the canonical short form `Fix #<n>` (single issue) or `Fix #<n1> #<n2> …` (batch) so GitHub auto-closes the issues on merge.
- Main branch: `master`. Development branch: `development`. **All feature work targets `development`** via PR; `master` is downstream only.
- CI: GitHub Actions (`CodeQuality.yml`) — builds, tests, and runs SonarQube analysis
- License: Apache 2.0 (code), LGPL v3.0 (metamodel files)
- To add a new metaclass: update the UML XMI source files, then run the code generators — do not manually create AutoGen files

## Branch & PR workflow (MANDATORY)

Every branch must be pushed and have an open PR against `development` before any task is reported "done". The PR is the user's review surface — the agent does NOT ask the user to review `git diff` manually before committing. Direct pushes to `development` or `master` are forbidden.

Operationally, at the end of any task that creates a branch (most commonly `/implement-extensions` and `/implement-extensions-batch`):

1. **Stage in-scope files explicitly** — `git add <path1> <path2> …`. NEVER `git add -A` or `git add .` (sensitive files / drift can leak).
2. **Commit** with the canonical short message: `Fix #<n>` for single-issue runs, `Fix #<n1> #<n2> …` for batches. Single line. No body, no `Co-Authored-By` trailer, no "🤖 Generated with …" footer, no `--no-verify`.
3. **Push**: `git push -u origin <branch>`. NEVER `--force`, NEVER `--force-with-lease`.
4. **Open PR**: `gh pr create --base development --head <branch> --title "Fix #<n>…" --body-file <pr-body-tmp>`. Capture the PR URL and surface it in the final summary.
5. **If the current branch is `development` or `master`**: REFUSE the workflow. Surface to the user — feature work must live on a feature branch first.

Failure modes:
- `git push` rejected (branch diverged from origin) → abort, surface, do NOT force-push.
- `gh pr create` fails (no auth, no write permission) → leave the branch pushed, advise the user to open the PR manually.
- Mid-task non-payload edits (e.g. instruction-file updates surfaced during a batch) → split into separate commits on the same branch so the `Fix #…` commit carries only the issue-resolving payload.

This policy reverses any older slash-command snippet that says "Do NOT auto-commit; the user reviews `git diff` and commits manually" — those snippets have been superseded by Phase PR.

## Quality rules

- Prefer comparing 'Count' to 0 rather than using 'Any()', both for clarity and for performance
- Use 'StringBuilder.Append(char)' instead of 'StringBuilder.Append(string)' when the input is a constant unit string
- Prefer 'string.IsNullOrWhiteSpace' over 'string.IsNullOrEmpty' when checking the non-nullable value of a string
- Prefer switch expressions/statements over if-else chains when applicable
- Prefer LINQ as much as possible — including for projection / filter / aggregation over collections (`items.Where(...).Select(...).ToList()`, `result.AddRange(items.Select(...))`, `items.Any(predicate)`, etc.) instead of hand-rolled `foreach` + `if` + `.Add()` loops. The ONE exception is straightforward positional or range access on a concrete `List`/array: `list[^1]` beats `list.Last()`, `array[1..^1]` beats `array.Skip(1).SkipLast(1)` — indexer/range syntax is more performant there. Outside that narrow exception, LINQ wins for clarity AND maintainability.
- Prefer C# collection expressions (`[a, b, c]`, `[..xs]`, `[]`) over `new[] { ... }`, `new List<T> { ... }`, `new T[] { ... }` when constructing a collection. Applies to both production code AND tests (e.g. `Is.EqualTo([classifier1, classifier2])` not `Is.EqualTo(new[] { classifier1, classifier2 })`, `return [];` not `return new List<T>();`). Fall back to explicit construction only when type inference cannot pick the right collection type.
- Use meaningful variable names instead of single-letter names in any context (e.g., 'charIndex' instead of 'i', 'currentChar' instead of 'c', 'element' instead of 'e')
- Use 'NotSupportedException' (not 'NotImplementedException') for placeholder/stub methods that require manual implementation
- Prefer C# property patterns ('x is IType { Prop: value }') over declared-variable-plus-predicate form ('x is IType name && name.Prop == value') when the narrowed variable is only consulted once; the property-pattern form is more concise and intent-revealing
- Surround every braced block (`if`, `else if`, `while`, `for`, `foreach`, `switch`, `using`, `try`/`catch`/`finally`, `lock`, `do…while`, anonymous `{ }`) with a blank line on both sides — the rule does NOT apply at the very start/end of a method body, nor between a `}` and a continuation keyword (`else`, `catch`, `finally`, `while` of `do…while`) that belongs to the same control flow
- When invoking an operation or derived property on a POCO from inside an extension method, call the POCO's instance member (e.g. `subject.IsDistinguishableFrom(other)`, `subject.qualifiedName`), NOT the static `ComputeXxxOperation` / `ComputeXxx` extension method. Virtual dispatch on the POCO honors operation/property REDEFINITION in subclass POCOs; calling the static extension directly bypasses dispatch and silently skips overrides. The static-extension form is reserved EXCLUSIVELY for the C# translation of OCL `self.oclAsType(SuperType).method()` — an explicit upcast that mandates targeting the SuperType's body (e.g. `Usage::namingFeature()` → `FeatureExtensions.ComputeNamingFeatureOperation(usage)`; `OwningMembership::path()` → `RelationshipExtensions.ComputeRedefinedPathOperation(owningMembership)`)
- **`IRelationship.OwnedRelatedElement` and `IElement.OwnedRelationship` storage collections are `[0..*]` — NEVER cardinality-limited.** The [1..1] / [0..1] multiplicities that appear in the metamodel apply to *derived* / *redefined* properties (e.g. `OwningMembership::ownedMemberElement`, `FeatureMembership::ownedMemberFeature`, `SubjectMembership::ownedSubjectParameter`), NOT to the underlying storage. When implementing such a derivation, **project from the collection — do not assume positional indexing**. For the common case of a `[1..1]` derived property, use the canonical shared helper `ElementExtensions.RequireSingleOfType<T>(this IReadOnlyList<IElement>, string)` from `SysML2.NET/Extensions/ElementExtensions.cs` — it does a zero-allocation index-based scan, early-exits on the second match, and throws `IncompleteModelException` with distinct "missing" vs "more than one" diagnostics:

  ```csharp
  // [1..1] type-narrowed redefinition (e.g. SubjectMembership::ownedSubjectParameter : IUsage)
  return subject.OwnedRelatedElement.RequireSingleOfType<ITargetType>(nameof(subject));

  // [1..1] non-narrowing redefinition (e.g. OwningMembership::ownedMemberElement : IElement)
  return subject.OwnedRelatedElement.RequireSingleOfType<IElement>(nameof(subject));
  ```

  The helper signature is `internal static T RequireSingleOfType<T>(this IReadOnlyList<IElement> elements, string subjectName) where T : class, IElement`. Because `IReadOnlyList<T>` is covariant, the same helper works on `IElement.OwnedRelationship` (whose element type is `IRelationship : IElement`) without an additional overload.

  The failure mode the helper produces matches the **derived property's declared multiplicity** as recorded in the `[Property(lowerValue:…, upperValue:…)]` attribute on the generated POCO interface (or in the UML XMI):

  | Multiplicity | Empty projection | Single-match projection | 2+ match projection |
  |---|---|---|---|
  | `[1..1]` (lowerValue=1, upperValue=1) | `throw IncompleteModelException` | return the match | `throw IncompleteModelException` |
  | `[0..1]` (lowerValue=0, upperValue=1) | `return null` (do NOT use the helper — write inline) | return the match | `throw IncompleteModelException` (inline) |
  | `[0..*]` / `[1..*]` | (use `List<T>` projection; not this pattern) | n/a | n/a |

  `IncompleteModelException` is the loud signal to SDK users that the model is malformed — DO NOT swallow it as `null` when the multiplicity is `[1..1]`, and DO NOT raise it for the empty case when the multiplicity is `[0..1]` (a legitimately-optional property).

  Do NOT use `.Count != 1 → throw` followed by `OwnedRelatedElement[0] as ITargetType` — that pattern (a) silently drops the correctly-typed element when it does not sit at index 0 (`AssignOwnership` allows owned related elements for both `IOwningMembership` AND `IAnnotation`, so a Membership can carry annotation targets alongside the member element), and (b) always allocates a `List<T>` via `OfType<T>().ToList()` even when the answer is decidable after the first two elements.
