# CLAUDE.md

Guidance for Claude Code when working in this repository.

## Project Overview

SysML2.NET is a .NET C# SDK implementing the OMG SysML v2 specification. It provides metaclass DTOs/POCOs, serializers (JSON, XMI, MessagePack, Textual Notation), a REST client, a DAL layer, and a Blazor WebAssembly viewer. Current version: 0.19.0.

## Project goal: conformance to the SPECIFICATIONS

**The target for EVERY layer is conformance to the OMG specifications — not parity with any reference implementation.** Where the two conflict, the specification wins.

| Specification | Governs |
|---|---|
| **KerML 1.0** | abstract syntax (metaclasses, properties, multiplicities, ordering, redefinitions), derived properties/operations (OCL), invariants, implied relationships (§8.4.2) |
| **SysML v2.0** | the systems layer on top of KerML — plus the textual concrete syntax |
| **Systems Modeling API & Services 1.0** | PIM types and services, REST/HTTP binding (`SysML2.NET.REST`, `PIM/`, `SysML2.NET.Serializer.Dictionary`) |
| **Model interchange** | the project/archive interchange format (`SysML2.NET.Kpar`) |

Rules that hold at every layer:

- **The specification decides.** Never implement a behaviour whose only justification is "the reference implementation does it". Record divergences in `GRAMMAR.md` ("Known KEBNF divergences"), the deviation ledger, or `GrammarErrata.cs`.
- **A reference implementation or its corpus is a test oracle, not the definition of correct.** A diff is a question, not a verdict. The pilot's serializer replays the author's original source text wherever the abstract syntax records no choice (optional keywords, name spellings, formatting) — on those points it is no authority at all.
- **Where the spec is genuinely ambiguous**, pick a reading, cite the clause, and note in one line that the clause admits more than one.
- **Where the spec offers several valid forms and ranks none**, the choice is ours: make it a setting (issue #359) or state it as a house convention — never as a requirement.
- **Do not claim conformance for any layer.** Confidence today rests on end-to-end comparison against sample data, which measures non-regression. Conformance needs round-trip tests (write → re-read → same model; exists for no serializer today) and per-invariant unit tests.

## Build & Test

```bash
dotnet restore SysML2.NET.sln && dotnet build SysML2.NET.sln
dotnet test SysML2.NET.sln
dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj --filter "FullyQualifiedName~AcceptActionUsageExtensionsTestFixture"
dotnet-coverage collect "dotnet test SysML2.NET.sln --no-build" -f xml -o coverage.xml   # as CI does
```

Test framework: **NUnit** (`[TestFixture]`, `[Test]`).

## Mandatory reading

**`DEVELOPMENT_STANDARDS.md`** (repo root) governs how code is written across the whole solution — production, tests, generator, scripts. Read it before authoring code in a session where you have not already done so; do not code from memory of "typical C#" or by copying a neighbouring file, since parts of this repo predate it.

**`TESTING.md`** (repo root) is the binding specification for every NUnit fixture. `Read` it in full, in the current session, before touching a single line in any `*.Tests/` project — including one-assertion changes. Several existing fixtures predate it and violate it; `TESTING.md` wins over any file you are looking at.

**Precedence:** `.editorconfig` / analyzer / linter configuration beats prose in either document. This `CLAUDE.md` beats `DEVELOPMENT_STANDARDS.md` where they overlap. `TESTING.md` is authoritative for fixtures.

`TESTING.md` rules most often got wrong (a checklist, not a substitute for reading it):

| Rule | § | Get-it-wrong symptom |
| --- | --- | --- |
| One `[Test]` per method-under-test, all scenarios packed inside | 2, 10 | a family of `…_WhenX_DoesY` tests sharing setup |
| Name it `Verify{MethodUnderTest}` — no scenario suffix | 6, 10 | `VerifyComputeFooWhenNull` |
| One fixture per production type, mirroring its namespace | 1 | a second `…AspectTestFixture` beside the real one |
| Every `[Test]` covers positive AND negative | 3, 10 | happy-path-only test |
| `Assert.That` only — never `Assert.Throws` / `IsTrue` / `AreEqual` | 4 | legacy NUnit API |
| `Has.Count.EqualTo(n)` | 8 | asserting on `.Count` directly |
| `Assert.EnterMultipleScope` only around 2+ consecutive asserts | 5, 10 | scope wrapping one fluent chain |
| Indexer/range over LINQ; `Is.SameAs` for POCO identity; `Is.EquivalentTo` when order is irrelevant | 8 | `.First()`, `Is.EqualTo` on a POCO |
| Assert an out-of-scope `NotSupportedException` stub — don't implement it | 9 | scope creep |

## Grounding SysML v2 / KerML work with the Hypha plugin

If the **Hypha** plugin is installed it is the **preferred grounding source for every SysML v2 / KerML semantic question**. Use it **before** implementing or reviewing anything depending on the metamodel — never a sibling analogue, the doc-comment OCL, or prior knowledge. Applies whenever you are about to: implement/modify a `Compute*` under `SysML2.NET/Extend/`; reason about features, multiplicities, ordering, redefinitions or constraints; implement/review a textual-notation or lexical rule; or claim what the specification requires.

- **`hypha:metamodel-lookup` — structure (always).** Type, multiplicity, **ordering**, redefinitions/subsettings, supertypes/subtypes, derivation and constraint OCL. Use the `hypha:metamodel-navigator` agent for cross-cutting fan-out across many metaclasses.
- **`hypha:spec-citation` — intent (whenever interpretation is involved).** The OCL is a formalization, not an explanation. Consult the spec when the OCL is terse, ambiguous, or leans on a defined term (`namingFeature`, `redefinedFeature`, connector `end`, typing/inheritance resolution). Skip only when the OCL is mechanical and unambiguous.
- **`hypha:sysml-validation`** — validate `.sysml` / `.kerml` textual notation.

Cite spec content by document name and clause ("OMG SysML v2 spec, Clause 8.2.2.1.1"), never by file path — this repo does not carry the specification texts.

Why both axes: `ActionDefinition::action` is declared `ordered`, a fact the OCL comment does not surface; and an OCL `->first()` picks one of many, but only the prose says on what basis (e.g. the most specific redefinition).

**If Hypha is not installed:** fall back to `Resources/KerML_only_xmi.uml` and `Resources/SysML_only_xmi.uml` (structure, OCL bodies, `ownedComment` prose). Mention once per session that installing Hypha is recommended, then proceed.

## Architecture

### Code generation pipeline

Files marked `THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!` must not be edited directly.

1. **Input**: `Resources/KerML_only_xmi.uml` and `Resources/SysML_only_xmi.uml` — the single source of truth for all generated code, including every OCL derivation, invariant, and operation body.
2. **Generator**: `SysML2.NET.CodeGenerator` reads them via `uml4net.xmi` and applies Handlebars templates (`Templates/Uml/*.hbs`).
3. **Output**: `AutoGen*` directories — DTOs/interfaces → `SysML2.NET/Core/AutoGenDto/`; POCOs → `Core/AutoGenPoco/`; enums → `Core/AutoGenEnum/`; JSON (de)serializers → `SysML2.NET.Serializer.Json/Core/AutoGen*/`; MessagePack formatters; extension methods → `SysML2.NET/Extend/`; DAL factories → `SysML2.NET.Dal/Core/`.

Favour duplicated, statically-defined generated methods over reflection-based code. To add a metaclass: update the XMI, then run the generators.

When working on the grammar/textual-notation generator (`SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs`), read `SysML2.NET.CodeGenerator/GRAMMAR.md` for the KEBNF model, cursor/builder conventions, and existing code-gen patterns.

### Textual notation reviewer is MANDATORY

**Every change touching these paths MUST be verified by the `textual-notation-reviewer` agent (`.claude/agents/textual-notation-reviewer.md`) before being reported complete or committed:**

- anything under `SysML2.NET.Serializer.TextualNotation/Writers/` (hand-coded partials, `AutoGenTextualNotationBuilder/*.cs`, `TextualNotationValidationExtensions.cs`, membership/string/cursor helpers)
- anything under `SysML2.NET/LexicalRules/` (including `AutoGenLexicalRules/*.cs`)
- `RulesHelper.cs` and any `Templates/Uml/*.hbs` emitting textual-notation or lexical-rules code

Author and reviewer both re-ground in `SysML2.NET.CodeGenerator/GRAMMAR.md`, `Resources/SysML-textual-bnf.kebnf` / `Resources/KerML-textual-bnf.kebnf`, and the rule's `<para>{…}</para>` XML doc on the generated sibling method. The agent enforces the `Move()` ↔ `+=` Golden Rule, EBNF quantifier semantics (`?` → single `if`; `*` → `while`; `+` → emit-once then loop), correct runtime type discriminators, absence of greedy-builder element drops, and consistency with the grammar rule. Reviewer passes have caught real bugs that no existing test would have failed on.

### Project dependency graph

```
SysML2.NET (core: netstandard2.1)
  ├── Core/AutoGenDto/     - DTO classes + interfaces (171 metaclasses × 2)
  ├── Core/AutoGenPoco/    - POCO classes + interfaces
  ├── Core/AutoGenEnum/    - Enums (FeatureDirectionKind, VisibilityKind, …)
  ├── Core/DTO/            - Hand-coded base: IElement : IData
  ├── Core/POCO/           - Hand-coded: IContainedElement, IContainedRelationship
  ├── Extend/              - Auto-generated extension methods per metaclass
  ├── Decorators/          - [Class], [Property], [Implements] attributes
  ├── PIM/                 - Platform-Independent Model DTOs (REST API types)
  ├── ModelInterchange/    - Archive/project interchange types (kpar)
  └── Common/IData.cs      - Base interface with Id property

SysML2.NET.Extensions        - Comparers, utilities across metaclasses
SysML2.NET.Serializer.Json / .Xmi / .MessagePack / .Dictionary (PIM)
SysML2.NET.Serializer.TextualNotation - Writers/, AutoGenTextualNotationBuilder/, validation extensions, cursor helpers
SysML2.NET.Dal               - Data Access Layer (Assembler, ElementFactory)
SysML2.NET.REST              - REST client + Session
SysML2.NET.Kpar              - Reader/Writer for .kpar archives
SysML2.NET.Viewer            - Blazor WebAssembly app (net9.0)
SysML2.NET.CodeGenerator     - Code generation tool (net10.0, not packaged)
```

Target frameworks: core `netstandard2.1`; tests and CodeGenerator `net10.0`; Viewer `net9.0`.

### DTO vs POCO

Each metaclass exists twice: **DTO** (lightweight, relationships by `Guid`, for serialization/transport) and **POCO** (resolved object references, `ContainerList<T>` for containment, for in-memory manipulation). Both share the `I{MetaclassName}` interface from `AutoGenDto/`; hand-coded `Core/DTO/IElement.cs` adds `IData` (`Guid Id`).

Generated namespaces mirror the KerML/SysML package hierarchy: `SysML2.NET.Core.DTO.Root.Elements`, `…Core.Types`, `…Systems.Actions`.

## Key conventions

- **Paths are ALWAYS repo-relative — NEVER absolute**, in every artifact you author: code comments, XML docs, error messages, commit/PR/issue bodies, plan files, agent briefs. Use forward slashes (`SysML2.NET/Extend/FooExtensions.cs`). The ONLY exception is the `Read`/`Edit`/`Write` `file_path` parameter, which the tools require to be absolute.
- Commit messages use `[Add]`, `[Update]`, `[Remove]`, `[Fix]` — except issue-fixing commits from `/implement-extensions[-batch]`, which use `Fix #<n>` / `Fix #<n1> #<n2> …` so GitHub auto-closes on merge.
- Branches: `master` is downstream only; **all feature work targets `development`** via PR.
- CI: GitHub Actions (`CodeQuality.yml`) — build, test, SonarQube.
- License: Apache 2.0 (code), LGPL v3.0 (metamodel files).

## Branch & PR workflow (MANDATORY)

Direct pushes to `development` or `master` are forbidden. All work lives on a feature branch. **Why: the user is the reviewer of record — the commit is the review and the push is the delivery.**

1. The agent **must NOT commit, EVER.** No exceptions, no asking, no "for convenience".
2. The agent **must NOT push commits, open PRs, or merge** unless the user explicitly asks in-conversation.
3. **When the agent creates a branch**: `git switch -c <branch> origin/development`, then immediately `git push -u origin <branch>` so the empty ref exists and the user's later push is a fast-forward. This is the only push the agent performs by default. If that push fails because the branch already exists on origin → abort and surface it; never force.
4. **At the end of any task that creates a branch**, stop with a summary: in-scope files, test counts, reviewer verdict, a pre-filled commit message (single line, no body, no trailers, no footer), and a handoff line — e.g. *"Review `git diff`, stage the in-scope files (`git add <path> …` — NEVER `-A` / `.`), commit with the message above, then `git push` (fast-forward, no `-u` needed). Open the PR yourself or via `gh pr create --base development`."* That is the end of the agent's involvement.

If the user does explicitly ask for push/PR: verify the branch is not `development`/`master`, `git log -1` matches the canonical form, and `git status --porcelain` is empty. Then `git push origin <branch>` — NEVER `--force`, `--force-with-lease`, or `--no-verify` — and `gh pr create --base development --head <branch> --title "Fix #<n>…" --body-file <tmp>` — NEVER `--base master`, never `--draft` unless asked.

Refuse: in-place feature work on `development`/`master`; pushing a commit the agent itself made (that is a policy violation needing human review).

## Comments: write as few as possible

Default to **none**, in production code, tests, and the generator alike. Prefer a better name or an extracted method over an explanation. Never narrate the what. No history, benchmarks, or "this used to…". No worked examples or case names — state the rule and, if it needs authority, cite the clause (`KerML §8.2.3.5.3`) and nothing more. Never reference the OMG pilot or any other tool as the reason for behaviour; grammar defects belong in `GrammarErrata.cs` with their rationale. No notes to future editors — encode the constraint in a guard or a test.

**The only test for keeping an inline comment:**

> A comment may ONLY state a constraint that would cause a reader to break something if they did not know it. A comment may NEVER explain why the change was made.

Ask: *if a reader deleted or rewrote this code without the comment, would they introduce a defect?* No → delete it. "It helps the reviewer" is not a yes; that is the commit message's job.

**Budget: at most 2 added comment lines per change.** A `PreToolUse` hook rejects comments containing `now`, `previously`, `rather than`, `instead of`, `used to`, `was `, `no longer`, `we `, `I ` — those signature words mean you are writing a commit message.

XML docs remain required on every type and member (`DEVELOPMENT_STANDARDS.md` §5.1), held to one sentence per tag, two as the ceiling.

## Quality rules

- **OCL is 1-based; never mix the two forms.** Metamodel positional *operations* (`IActionUsage.Argument(int)`, `InputParameter(int)`) are themselves 1-based — pass OCL's N through UNCHANGED. Raw `List<T>` indexing is 0-based — `->at(N)` becomes `[N-1]`, `->first()` becomes `[0]`/`FirstOrDefault()`, guarded on count to honour OCL's implicit null.
- **Call the POCO's instance member, not the static `Compute*` extension**, when invoking an operation or derived property from inside an extension method (`subject.IsDistinguishableFrom(other)`, `subject.qualifiedName`). Virtual dispatch honours redefinition in subclass POCOs. The static form is reserved exclusively for OCL `self.oclAsType(SuperType).method()`.
- Compare `Count` to 0 rather than calling `Any()`.
- `StringBuilder.Append(char)` over `Append(string)` for constant single-character input.
- `string.IsNullOrWhiteSpace` over `string.IsNullOrEmpty`.
- Switch expressions/statements over if-else chains.
- **LINQ by default** for projection/filter/aggregation instead of hand-rolled `foreach` + `if` + `.Add()`. The one exception is positional or range access on a concrete `List`/array (`list[^1]` over `list.Last()`, `array[1..^1]` over `.Skip(1).SkipLast(1)`).
- **Push a leading-`if` filter into the iterator**: `foreach (var x in xs.Where(…))`, not `foreach (var x in xs) { if (!p) continue; … }`; `.OfType<T>()` over an `is`-check plus cast. Exceptions: the predicate has observable side-effects and order matters, or it is too long to read inline (extract it and still call it from `.Where(...)`).
- Collection expressions (`[a, b, c]`, `[..xs]`, `[]`) over `new[] { … }` / `new List<T> { … }`, in production and tests alike.
- Meaningful variable names, never single letters (`charIndex`, not `i`).
- `NotSupportedException` (not `NotImplementedException`) for stubs.
- Property patterns (`x is IType { Prop: value }`) when the narrowed variable is consulted once.
- **Auto-properties always** — never a backing field plus a full-getter property when there is no non-trivial logic.
- **Method-group syntax over a lambda that merely invokes a no-arg method**: `Assert.That(subject.ComputeFoo, Throws.TypeOf<X>())`, `.Where(string.IsNullOrWhiteSpace)`. Fall back to a lambda only when the body does more than the bare call, the target is overloaded, or explicit type arguments are needed.
- Blank line on both sides of every braced block (`if`, `while`, `foreach`, `switch`, `using`, `try`/`catch`, `lock`, `do…while`) — except at the very start/end of a method body, and between a `}` and a continuation keyword of the same control flow.
- **One `[Test]` per class/method-under-test** packing every scenario into multiple `Assert.That` calls (`TESTING.md` §2). Split only when a scenario has a genuinely distinct, complex setup.

### Deriving `[0..1]` / `[1..1]` properties from `[0..*]` storage

`IRelationship.OwnedRelatedElement` and `IElement.OwnedRelationship` are **always `[0..*]` storage**. The `[1..1]` / `[0..1]` multiplicities in the metamodel belong to *derived*/*redefined* properties (`OwningMembership::ownedMemberElement`, `FeatureMembership::ownedMemberFeature`, …). **Project from the collection — never index positionally.** Use the shared helpers in `SysML2.NET/Extensions/ElementExtensions.cs` (all early-exit on the second match):

- **`SingleStrict<T>`** — `[1..1]`: empty → `IncompleteModelException`; one → return; 2+ → `MultiplicityViolationException`.
- **`SingleOrDefaultStrict<T>`** — `[0..1]`: empty → `null`; one → return; 2+ → `MultiplicityViolationException`.

Each has a homogeneous overload (`IEnumerable<T>`) and a heterogeneous one (`IEnumerable`, bundling an `OfType<TResult>()` filter).

```csharp
return subject.OwnedRelatedElement.SingleStrict<ITargetType>(nameof(subject));            // [1..1] type-narrowed
return subject.OwnedRelatedElement.SingleStrict<IElement>(nameof(subject));               // [1..1] non-narrowing
return subject.type.SingleOrDefaultStrict<IPredicate>(nameof(subject));                   // [0..1] type-narrowed
return subject.OwnedRelationship.OfType<ISomeRelationship>()
              .Select(r => r.something).SingleOrDefaultStrict(nameof(subject));           // [0..1] multi-hop
```

Pick the helper from the `[Property(lowerValue:…, upperValue:…)]` attribute on the generated POCO interface. Strictness applies **only to the final filter** of the chain — intermediate `OfType<…>()` hops may legitimately match many.

**OCL gate (overrides the `[0..1]` rule):** when the OCL body explicitly elects the first of many (`->first()`, `->at(1)`), the contract is "pick the first" — keep `FirstOrDefault`. Strict `[0..1]` applies only when the OCL has no first-picking call, or has no body at all.

`IncompleteModelException` (lower-bound: required element missing) and `MultiplicityViolationException` (upper-bound: too many) are the loud signals that a model is malformed. Do not swallow them as `null` when the multiplicity demands a throw, and do not raise them for the empty case of a legitimately-optional `[0..1]`.

Never use `.Count != 1 → throw` plus `OwnedRelatedElement[0] as ITargetType`: a Membership can carry annotation targets alongside the member element, so the correctly-typed element need not sit at index 0.
