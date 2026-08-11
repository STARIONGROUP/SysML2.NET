# Grammar Code Generation Guide

This file provides essential context for working on the SysML2 textual notation code generator (`RulesHelper.cs` and related files). Read this when modifying grammar processing or the `TextualNotationBuilder` generation pipeline.

> **For the end-to-end pipeline narrative** (parser → grammar model → `RuleProcessor` dispatch → patterns A/B/C/D → three-tier guard resolution → no-target lifting), see the longer companion document `TEXTUAL_NOTATION_CODEGEN.md` in this same folder.

> **⚠ Reviewer agent is mandatory for every change.** Before committing any modification to `SysML2.NET.Serializer.TextualNotation/Writers/*.cs` or to `SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs`, invoke the `textual-notation-reviewer` agent (`.claude/agents/textual-notation-reviewer.md`) to verify grammar correctness. See CLAUDE.md "Textual notation reviewer is MANDATORY" for details.

## EBNF / KEBNF Notation Legend

SysML2 grammar rules (in `Grammar/Resources/*.kebnf` and the `<para>…</para>` XML docs of generated `Build{Rule}` methods) follow this notation:

| Construct | Notation | Meaning |
|---|---|---|
| Lexical element | `LEXICAL` (uppercase) | Lexer token |
| Terminal element | `'terminal'` (single-quoted) | Literal keyword or punctuation |
| Non-terminal element | `NonterminalElement` (PascalCase) | Reference to another rule |
| Sequential elements | `Element1 Element2` | Both appear in order |
| Alternative elements | `Element1 \| Element2` | Exactly one of them |
| Optional elements | `Element ?` | **Zero or one** occurrence |
| Repeated elements | `Element *` | **Zero or more** occurrences |
| Repeated elements | `Element +` | **One or more** occurrences (minimum 1, not zero) |
| Grouping | `( Elements... )` | Parentheses scope a quantifier or alternation |

**Quantifier pitfall when hand-coding:** `+` guarantees at least one occurrence. For `(A | B)+`, alternatives may interleave — a loop is required that re-tests the cursor after each iteration until neither alternative matches.

### KEBNF Extensions (SysML2-specific)

| Construct | Notation | Meaning |
|---|---|---|
| Scalar assignment | `prop = X` | Assign the parsed value of `X` to the property `prop` |
| Collection assignment | `prop += X` | Append one parsed `X` to the collection `prop` |
| Boolean assignment | `prop ?= 'keyword'` | Set `prop = true` when the terminal is present |
| Non-parsing assignment | `{ prop = 'val' }` | Implicit side-effect in parse direction; in unparse direction it emits no output, and it does NOT participate in dispatch-guard synthesis — only parsed assignments (`prop = X`, `prop += X`, `prop ?= X`) do |
| QualifiedName value literal | `prop = [QualifiedName]` | Cross-reference by qualified name |

## Pipeline Overview

```
KEBNF grammar files (Grammar/Resources/*.kebnf)
  parsed by Grammar/TextualNotationSpecificationVisitor
  into Grammar/Model/* (RuleElement hierarchy)
  processed by HandleBarHelpers/RulesHelper.cs
  via Handlebars template (Templates/Uml/textualNotationBuilder.hbs)
  emits SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/*.cs
```

**Hand-coded counterparts** live in `SysML2.NET.Serializer.TextualNotation/Writers/*.cs` (parent folder) as `partial` classes. When code-gen can't handle a rule, it emits `Build{RuleName}HandCoded(poco, cursorCache, stringBuilder)` which must be implemented in the hand-coded partial.

## Grammar Element Types (`Grammar/Model/`)

| Type | Grammar form | Key properties |
|------|--------------|----------------|
| `NonTerminalElement` | `RuleName`, `RuleName*`, `RuleName+` | `Name`, `IsCollection` |
| `AssignmentElement` | `prop=X`, `prop+=X`, `prop?=X` | `Property`, `Operator`, `Value: RuleElement` |
| `TerminalElement` | literal strings like keywords, `;`, `{` | `Value` |
| `GroupElement` | `(...)`, `(...)?`, `(...)*` | `Alternatives`, `IsOptional`, `IsCollection` |
| `ValueLiteralElement` | `[QualifiedName]`, `NAME` | `Value`, `QueryIsQualifiedName()` |
| `NonParsingAssignmentElement` | `{prop='val'}` | `PropertyName`, `Operator`, `Value` |

## Rule Structure

`RuleName:TargetElementName = alternative1 | alternative2 | ...`

- `TargetElementName` is the UML metaclass the rule targets (defaults to `RuleName` if omitted)
- Builder methods take `I{TargetElementName} poco` as parameter
- When a NonTerminal's target is the **declaring class** (same as calling context), it uses `poco`
- When a NonTerminal targets a different class, the cursor element is cast: `if (cursor.Current is ITargetType x) { ... }`

## Cursor Model (`ICursorCache`)

Cursors iterate over collection properties (typically `ownedRelationship`). Key mechanics:

- `cursorCache.GetOrCreateCursor(pocoId, propertyName, collection)` — same `(pocoId, propertyName)` returns the same cursor instance. Cursors are **shared** across builder methods.
- `cursor.Current` — current element (null when exhausted)
- `cursor.Move()` — advances to next element

### The Golden Rule: `Move()` ↔ `+=`

**`cursor.Move()` must be emitted exactly once per `+=` assignment processed, and nowhere else.**

The `+=` grammar operator means "consume one element from the collection" — so every `+=` processing advances the cursor by one. No other grammar construct advances it:

| Grammar construct | Advances cursor? |
|---|---|
| `prop+=X` (collection assignment) | **Yes — emit `Move()` after processing** |
| `prop=X` (scalar assignment) | No |
| `prop?='keyword'` (boolean assignment) | No |
| `'terminal'` | No |
| `RuleName` (plain NonTerminal reference) | No (the referenced rule may internally `+=`) |
| `RuleName*` / `RuleName+` (collection NonTerminal) | No (each iteration's inner `+=` advances) |
| `(...)` / `(...)?` / `(...)*` (groups) | No (inner `+=` advances) |
| `[QualifiedName]` / `NAME` (value literals) | No |

When a generated switch dispatches on `cursor.Current` for multiple `+=` alternatives, it **also** emits `default: cursor.Move(); break;` as a safety net — if an unexpected type appears in the cursor, the method still advances so callers in a `while` loop don't spin forever.

**Consequence:** `while (cursor.Current != null) { BuildDispatcher(poco); }` loops don't need an explicit outer `Move()` — the dispatcher's internal `+=` handling (or safety default) advances the cursor.

## Key Methods in `RulesHelper.cs`

| Method | Purpose |
|--------|---------|
| `ProcessAlternatives` | Entry point for processing a rule's alternatives. Dispatches to more specific handlers based on alternative structure |
| `ProcessUnitypedAlternativesWithOneElement` | Handles `A | B | C` where all alternatives have one element of the same type (NonTerminal, Terminal, or AssignmentElement) |
| `ProcessNonTerminalElement` | Processes a single NonTerminal reference. For collections, delegates to `EmitCollectionNonTerminalLoop` |
| `EmitCollectionNonTerminalLoop` | Generates `while (cursor.Current ...) { builderCall; cursor.Move(); }` |
| `ProcessAssignmentElement` | Handles `=`, `+=`, `?=` assignments. Emits property access, cursor advance, or boolean-triggered keyword |
| `OrderElementsByInheritance` | Sorts NonTerminals by UML class depth (most specific first) for switch case ordering |
| `ResolveBuilderCall` | Returns `XxxTextualNotationBuilder.BuildRuleName(var, cursorCache, stringBuilder);` or `null` if types incompatible |
| `ResolveCollectionWhileTypeCondition` | Builds while condition — positive `is Type` if collection has only `+=` assignments, negative `is not null and not NextType` as fallback |

## Guard Mechanisms for Ambiguous Dispatch

When multiple alternatives map to the same UML class (creating duplicate switch cases), these disambiguate:

1. **`?=` boolean guards** (primary) — e.g., `EndUsagePrefix` has `isEnd?='end'`, so it gets `when poco.IsEnd`
2. **`IsValidFor{RuleName}()` extension methods** (fallback) — hand-coded in `MembershipValidationExtensions.cs` or `TextualNotationValidationExtensions.cs`. Used when `?=` can't disambiguate
3. **Synthesised structural guards** (subtype-overlap defence) — when a duplicate group's target class has subtypes routed by a sibling alternative (i.e. another alternative targets a SUPERTYPE of the group's target), the would-be-default member is NOT left as a bare `case I{Target}:`. Instead, `RuleProcessor.PatternHandlers.cs#SynthesiseGuardFromRuleBody` walks the rule body and AND-combines one predicate per parsed `AssignmentElement`:
   - `prop = 'literal'` → `poco.{Prop} == "literal"`
   - `prop = [QualifiedName]` → `poco.{Prop} != null`
   - `prop = NonTerminal` → `poco.{Prop} is I{RHS-target}` (or `!= null` when the RHS target cannot be resolved)
   - first `ownedRelationship += NonTerminal` → `cursor.Current is I{RHS-target}`
   - non-cursor `prop += NonTerminal` → `poco.{Prop}.OfType<I{RHS-target}>().Any()`
   - `prop ?= 'kw'` → produced by step 1, not re-synthesised here
   - `{ prop = X }` non-parsing → ignored
4. **Type ordering** — more specific types (deeper inheritance) come first, fallback case (matching `NamedElementToGenerate`) goes last as `default:`

## Patterns Handled by Code-Gen

| Pattern | Example | Handler |
|---------|---------|---------|
| Body with collection items | `';' | '{' Items* '}'` | `ProcessAlternatives` body check with `IsCollection: true` NonTerminal |
| Body with single sub-rule | `';' | '{' SingleRule '}'` | `ProcessAlternatives` body check with `IsCollection: false` NonTerminal |
| QualifiedName or owned chain | `prop=[QualifiedName] | prop=OwnedChain{containment+=prop}` | `ProcessAlternatives` two-alternative check |
| Mixed NonTerminal + `+=` | `NonTerminal | prop+=X` | `if (cursor.Current is XType) { process + Move() } else { BuildNonTerminal(poco, ...) }` |
| Collection group | `(ownedRelationship+=A | ownedRelationship+=B)*` | `groupElement.IsCollection` handler: while loop + cursor-based switch |
| Pure dispatch | `NonFeatureMember | NamespaceFeatureMember` | `ProcessUnitypedAlternativesWithOneElement` NonTerminal case with `IsValidFor` guards |

## Switch Case Variable Scoping Gotcha

Pattern variables like `elementAsFeatureMembership` in `if (x is Type elementAsFeatureMembership)` have **block scope**, not just the `if` body — they leak into the enclosing scope. The `if (x != null) { }` wrapper around these serves as a **scoping boundary** to prevent name collisions when the same pattern appears multiple times in the same method. Don't remove outer null guards without understanding this.

## HandCoded Fallback Convention

When code-gen detects an unsupported pattern, it emits:
```csharp
Build{RuleName}HandCoded(poco, cursorCache, stringBuilder);
```

The hand-coded partial class file must:
1. Live in `SysML2.NET.Serializer.TextualNotation/Writers/{ClassName}TextualNotationBuilder.cs`
2. Declare `public static partial class {ClassName}TextualNotationBuilder`
3. Implement the method as `private static void Build{RuleName}HandCoded(...)` 
4. Use `NotSupportedException` (not `NotImplementedException`) for unimplemented stubs
5. Include the grammar rule as `<remarks>{rule}</remarks>` in XML doc

## Common Builder Conventions

- **Trailing space**: Most builders append a trailing space after their content (`stringBuilder.Append(' ')`). Chain builders already add this internally — don't double it.
- **Terminal formatting**: Special terminals like curly braces and semicolons use `AppendLine`; angle brackets and `~` have no trailing space (see `NewLineTerminals` / `NoTrailingSpaceTerminals` in `RulesHelper.cs`).
- **Owned vs referenced elements**: To distinguish `type=OwnedChain{ownedRelatedElement+=type}` from `type=[QualifiedName]`, check at runtime: `poco.OwnedRelatedElement.Contains(poco.Type)` owned (call chain builder), else cross-reference (emit `qualifiedName`).

## Testing Changes to the Generator

After modifying `RulesHelper.cs`:
```bash
dotnet build SysML2.NET.CodeGenerator/SysML2.NET.CodeGenerator.csproj
dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj --filter UmlCoreTextualNotationBuilderGeneratorTestFixture
# Generated files land in SysML2.NET.CodeGenerator.Tests/bin/Debug/net10.0/UML/_SysML2.NET.Core.UmlCoreTextualNotationBuilderGenerator/
cp SysML2.NET.CodeGenerator.Tests/bin/Debug/net10.0/UML/_SysML2.NET.Core.UmlCoreTextualNotationBuilderGenerator/*.cs SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/
dotnet build SysML2.NET.sln
dotnet test SysML2.NET.sln
```

**Count remaining HandCoded calls** to track progress:
```bash
grep -r "HandCoded" SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/*.cs | wc -l
```

## Known KEBNF / specification divergences

The `.kebnf` files are OMG-owned and **never edited**. Where a production cannot describe the
notation the pilot implementation actually reads and writes, the writer deviates and the deviation is
recorded here. All of these are reported upstream in
[SysML-v2-Release issue #124](https://github.com/Systems-Modeling/SysML-v2-Release/issues/124)
("Several textual KEBNF productions appear unreachable or inconsistent with release examples").

Do **not** "fix" the writer back to the literal production without checking this table first.

### #124 item 8 — `EntryTransitionMember` emits a duplicated `then` (deviation implemented)

```
EntryTransitionMember : FeatureMembership =
    MemberPrefix ( ownedRelatedElement += GuardedTargetSuccession
                 | 'then' ownedRelatedElement += TargetSuccession ) ';'

TargetSuccession : SuccessionAsUsage =
    ownedRelationship += SourceEndMember 'then' ownedRelationship += ConnectorEndMember
```

`TargetSuccession` supplies its own `'then'`, so the literal reading of the second alternative is
`then then S1;`. The corpus writes `entry; then off;`
(`Validation/05-State-based Behavior/5-State-based Behavior-2.sysml`), and the pilot's Xtext uses
`TransitionSuccession` (`EmptySourceEndMember ConnectorEndMember`, no `'then'`) at
`org.omg.sysml.xtext/src/org/omg/sysml/xtext/SysML.xtext:1798`.

Confirmed against both independent sources of the grammar — the `.kebnf` and the OMG specification
(SysML 2.0 §8.2.2.18.1 State Definitions, §8.2.2.17.8 Action Successions) — so this is a genuine
specification defect, not a transcription slip.

The model cannot discriminate the two productions: both are a `SuccessionAsUsage` with two
`EndFeatureMembership`s, and the multiplicity present on the source end is added by the pilot's
transform to *both* ends. **Deviation:** `BuildEntryTransitionMemberHandCoded` suppresses the rule's
own `'then'` and lets `TargetSuccession` supply it. The structure the KEBNF specifies is still
honoured; only the redundant keyword is dropped.

### #124 item 7 — `end` prefix on non-reference usages (deviation accepted, not implemented)

`DefaultReferenceUsage` has no `EndUsagePrefix`, so `end ref hitch` / `end port p1: P;` are
unreachable, yet appear in the corpus (`03-Function-based Behavior/3c-…-1`, `3c-…-2`). The writer
emits the pilot's form; the difference is recorded as an accepted deviation for those files.

### Items expected to affect folders not yet validated

Not yet investigated — listed so the cause is recognised on first encounter rather than
re-diagnosed:

| item | production | folder likely affected |
|---|---|---|
| #1 | `AllocationDefinition` missing from `DefinitionElement` | 12-Dependency Relationships |
| #3 | `MetadataUsage` not wired into any dispatch point | 14-Language Extensions |
| #9 | `SatisfyRequirementUsage` requires `assert` | 08-Requirements |
| #10 | `CaseBodyItem` admits no `ReturnParameterMember` | 10-Analysis and Trades |
| #11 | `EnumeratedValue` cannot carry prefix metadata (`#Security enum secret`) | 13-Model Containment, 14-Language Extensions |

Items #2, #4, #5, #6 concern productions with no corpus coverage.

## Model ↔ notation reconciliations (NOT divergences)

Cases where the grammar offers two conformant productions for one model, so the writer must choose.
Nothing here deviates from the specification — unlike the divergences above.

### `TargetTransitionUsage` — the implied transition source

```
StateBodyItem : Type = …
    | ( ownedRelationship += SourceSuccessionMember )?
      ownedRelationship += BehaviorUsageMember
      ( ownedRelationship += TargetTransitionUsageMember )*     ← shorthand
    | ownedRelationship += TransitionUsageMember                ← explicit
```

`state off; accept X then Y;` and `transition off accept X then Y;` are **both normative** and produce
the *same* model: the pilot resolves the shorthand at parse time and stores the source explicitly as a
`FeatureChainMember` (a non-owning `Membership` cross-referencing the state). The shorthand-ness is
therefore not recoverable, and `TargetTransitionUsage` has **no notation for the source at all**.

The writer prefers the shorthand (matching the corpus) only when all three hold, each required for
correctness rather than style:

- the transition is **anonymous** — `TargetTransitionUsage` has no `UsageDeclaration` slot, so a named
  transition would silently lose its name (this is what keeps `5-…-1` / `5-…-1a` on the explicit form);
- its source **is** the anchor feature of the preceding `BehaviorUsageMember` — otherwise the shorthand
  re-parses against that state and denotes a different element;
- it is positioned in the `( … )*` run following that member.

Consequence, in `TypeTextualNotationBuilder.EmitTargetTransitionRun`: the transition's own
`ownedRelationship` cursor is advanced once **past the source with no emission**. That is a deliberate
exception to the `Move()` ↔ `+=` Golden Rule, valid because the elected production has no notation for
that element. It is conditional — it only runs after `QueryImpliedSourceTransition` has confirmed
position 0 is the source membership — so it cannot consume a real element.
