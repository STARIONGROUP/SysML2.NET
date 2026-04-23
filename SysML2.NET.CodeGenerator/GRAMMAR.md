# Grammar Code Generation Guide

This file provides essential context for working on the SysML2 textual notation code generator (`RulesHelper.cs` and related files). Read this when modifying grammar processing or the `TextualNotationBuilder` generation pipeline.

## Pipeline Overview

```
KEBNF grammar files (Grammar/Resources/*.kebnf)
  parsed by Grammar/TextualNotationSpecificationVisitor
  into Grammar/Model/* (RuleElement hierarchy)
  processed by HandleBarHelpers/RulesHelper.cs
  via Handlebars template (Templates/Uml/textualNotationBuilder.hbs)
  emits SysML2.NET/TextualNotation/AutoGenTextualNotationBuilder/*.cs
```

**Hand-coded counterparts** live in `SysML2.NET/TextualNotation/*.cs` (parent folder) as `partial` classes. When code-gen can't handle a rule, it emits `Build{RuleName}HandCoded(poco, cursorCache, stringBuilder)` which must be implemented in the hand-coded partial.

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
3. **Type ordering** — more specific types (deeper inheritance) come first, fallback case (matching `NamedElementToGenerate`) goes last as `default:`

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
1. Live in `SysML2.NET/TextualNotation/{ClassName}TextualNotationBuilder.cs`
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
cp SysML2.NET.CodeGenerator.Tests/bin/Debug/net10.0/UML/_SysML2.NET.Core.UmlCoreTextualNotationBuilderGenerator/*.cs SysML2.NET/TextualNotation/AutoGenTextualNotationBuilder/
dotnet build SysML2.NET.sln
dotnet test SysML2.NET.sln
```

**Count remaining HandCoded calls** to track progress:
```bash
grep -r "HandCoded" SysML2.NET/TextualNotation/AutoGenTextualNotationBuilder/*.cs | wc -l
```
