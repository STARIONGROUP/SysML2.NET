# Textual Notation Code Generation — Technical Reference

This document describes, end-to-end and in technical detail, how the **textual notation builders** (`Build{Rule}` methods that turn a POCO into KerML/SysML v2 source text) are generated from the **UML metamodel** and the **KEBNF grammar files**. It is intended for engineers extending or debugging the generator, hand-coding `Build{Rule}HandCoded` partials, or reviewing emitted output.

It does **not** teach KEBNF or SysML v2; for those see `Resources/SysML-textual-bnf.kebnf`, `Resources/KerML-textual-bnf.kebnf`, and the OMG SysML v2 Textual Notation tutorial in `Resources/specification/Intro to the SysML v2 Language-Textual Notation.pdf.txt`.

## 1. Inputs

The generator combines **two sources of truth**. Neither is sufficient on its own.

| Source | Files | Role |
|---|---|---|
| UML metamodel | `Resources/KerML_only_xmi.uml`, `Resources/SysML_only_xmi.uml` | Defines every metaclass, its properties (name, type, multiplicity, owned/derived), and inheritance. Read via `uml4net.xmi`. |
| KEBNF grammar | `Resources/KerML-textual-bnf.kebnf`, `Resources/SysML-textual-bnf.kebnf` | Defines every textual rule, its target metaclass, alternatives, terminals, and property assignments. Loaded via an ANTLR-generated parser. |

A KEBNF rule header has the form

```
RuleName : TargetElementName = alternative1 | alternative2 | ...
```

`TargetElementName` (the UML metaclass the rule applies to) defaults to `RuleName` when omitted. The builder method for the rule will take `I{TargetElementName} poco` as its first argument. Example from `Resources/SysML-textual-bnf.kebnf:42-44`:

```
Identification : Element =
    ( '<' declaredShortName = NAME '>' )?
    ( declaredName = NAME )?
```

For the notation legend (terminals, non-terminals, quantifiers, assignment operators, KEBNF extensions) the authoritative source is **Clause 8.2.2.1.1 — EBNF Conventions** of the OMG SysML v2 specification (`Resources/specification/2a-OMG_Systems_Modeling_Language.pdf.txt`).

---

## 2. Outputs

| Output directory / file | Generator | Templates |
|---|---|---|
| `SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/*.cs` (one per UML class that has rules targeting it) | `UmlCoreTextualNotationBuilderGenerator` | `Templates/Uml/core-textual-notation-builder-template.hbs`, `…core-textual-notation-shared-builder-template.hbs`, `…core-textual-notation-builder-facade-template.hbs` |
| `SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/SharedTextualNotationBuilder.cs` (rules whose target class cannot be matched to any UML metaclass, e.g. `FeaturePrefix`) | same | shared-builder template |
| `SysML2.NET/LexicalRules/AutoGenLexicalRules/Keywords.cs`, `SymbolicKeywordKind.cs`, `SymbolicKeywordKindExtensions.cs` | `UmlCoreLexicalRulesGenerator` | `core-lexical-keywords-template.hbs`, `core-lexical-symbolic-keyword-kind-template.hbs`, `core-lexical-symbolic-keyword-kind-extensions-template.hbs` |

**Not generated** (despite living next to the AutoGen folder):
- `SysML2.NET.Serializer.TextualNotation/Writers/TextualNotationValidationExtensions.cs` — hand-coded `IsValidFor*` guards.
- `SysML2.NET.Serializer.TextualNotation/Writers/*TextualNotationBuilder.cs` (root of `Writers/`, NOT under `AutoGenTextualNotationBuilder/`) — hand-coded partial-class companions containing `Build{Rule}HandCoded` methods.
- `SysML2.NET.Serializer.TextualNotation/Writers/SharedTextualNotationBuilder.cs` (root) — hand-coded helpers like `QueryShortestResolvableName`, `AppendRegularComment`.

---

## 3. End-to-End Pipeline

```
Resources/*.kebnf  ──ANTLR──►  kebnfLexer + kebnfParser
                                  │
                                  ▼
                          TextualNotationSpecificationVisitor
                                  │
                                  ▼
                          TextualNotationSpecification
                          (List<TextualNotationRule>, hierarchy of RuleElement)
                                  │
Resources/*.uml  ──uml4net──► XmiReaderResult ──────┐
                                                     │
                              ┌──────────────────────┘
                              ▼
                  UmlCoreTextualNotationBuilderGenerator.GenerateAsync
                  (groups rules by target UML class)
                              │
                              ▼ (per UML class)
                  Handlebars renders core-textual-notation-builder-template.hbs
                              │
                              │ template invokes Handlebars helper
                              │ "RulesHelper.WriteRule" for each rule
                              ▼
                  RulesHelper.WriteRule (RulesHelper.cs:65)
                              │
                              ▼
                  RuleProcessor.ProcessAlternatives (RuleProcessor.cs:50)
                              │
                              ▼ emit
                  Build{Rule}() body — either inline C# or a delegating
                  call to Build{Rule}HandCoded.
                              │
                              ▼
                  AutoGenTextualNotationBuilder/{Class}TextualNotationBuilder.cs
```

**Loader entry point**: `GrammarLoader.LoadTextualNotationSpecification` (`SysML2.NET.CodeGenerator/Grammar/GrammarLoader.cs`) — runs the ANTLR-generated `kebnfLexer`/`kebnfParser` (under `Grammar/AutoGenGrammar/`) and walks the parse tree with `TextualNotationSpecificationVisitor`.

**Top-level orchestrator**: `UmlCoreTextualNotationBuilderGenerator.GenerateAsync(XmiReaderResult, TextualNotationSpecification, DirectoryInfo)` (`SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/UmlCoreTextualNotationBuilderGenerator.cs:113`).

---

## 4. KEBNF Grammar Model (`SysML2.NET.CodeGenerator/Grammar/Model/`)

The parser produces an immutable AST whose node types each map to one grammar construct.

| C# type | KEBNF construct | Key properties |
|---|---|---|
| `TextualNotationRule` | `Name : Target = …` | `RuleName`, `TargetElementName`, `EffectiveTarget`, `Alternatives`, `IsDispatcherRule`, `IsMultiCollectionAssignment` |
| `Alternatives` | one branch separated by `\|` | `Elements : List<RuleElement>` |
| `NonTerminalElement` | `OtherRule`, `OtherRule*`, `OtherRule+` | `Name`; inherits `Suffix` and `IsCollection` from `RuleElement` |
| `AssignmentElement` | `prop = X`, `prop += X`, `prop ?= X` | `Property`, `Operator`, `Value : RuleElement` |
| `TerminalElement` | `'keyword'`, `';'`, `'{'` | `Value` |
| `GroupElement` | `( … )`, `( … )?`, `( … )*` | `Alternatives`, `IsOptional`, `IsCollection` |
| `ValueLiteralElement` | `[QualifiedName]`, `NAME` | `Value`, `QueryIsQualifiedName()` |
| `NonParsingAssignmentElement` | `{ prop = 'val' }` | `PropertyName`, `Operator`, `Value` — emits *no* output in the unparse direction |

All elements share `RuleElement.Suffix ∈ { "", "?", "*", "+" }`, `RuleElement.IsOptional`, `RuleElement.IsCollection`.

---

## 5. The Handlebars Facade — `RulesHelper.cs`

`RulesHelper` is a **thin facade** required by HandlebarsDotNet, which can only call static helpers. All real work lives in `RuleProcessor` (a partial class spanning four files). `RulesHelper`'s job is to register helpers and forward to a `RuleProcessor` instance captured in a closure.

Public surface (`SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs`):

| Member | Lines | Purpose |
|---|---|---|
| `SharedBuilderClassName = "SharedTextualNotationBuilder"` | 43 | Class name used for no-target / unmatched-class rules. |
| `RegisterRulesHelper(this IHandlebars)` | 49–98 | Registers two Handlebars helpers and instantiates the singleton `RuleProcessor`. |
| `"RulesHelper.ContainsAnyDispatcherRules"` helper | 53–63 | Template-side predicate over a `List<TextualNotationRule>`. |
| `"RulesHelper.WriteRule"` helper | 65–97 | **Primary entry**: constructs `RuleGenerationContext` with `CurrentVariableName = "poco"` and delegates to `RuleProcessor.ProcessAlternatives`. |
| `ResolveNoTargetRuleEffectiveTarget(rule, allRules, cacheSource)` | 107–110 | Static utility — delegates to `NoTargetRuleResolver.ResolveEffectiveTarget`. |
| `IsSharedNoTargetRule(rule, cacheSource)` | 118–121 | Static utility — delegates to `NoTargetRuleResolver.IsSharedRule`. |

The template calls `RulesHelper.WriteRule` once per `Build{Rule}` method body; everything that follows is driven by `RuleProcessor`.

---

## 6. The `RuleProcessor` Engine

`RuleProcessor` is `internal sealed partial class` (`RuleProcessor.cs:39`), split across four files for readability.

| Partial file | Responsibility |
|---|---|
| `RuleProcessor.cs` | Top-level dispatch (`ProcessAlternatives` at line 50) and a small set of cross-cutting helpers (single-alternative emission, body emission, cursor declarations). |
| `RuleProcessor.ElementProcessing.cs` | Per-`RuleElement` emission: `ProcessRuleElement` (line 48), `ProcessAssignmentElement` (line 259), `ProcessNonTerminalElement` (line 435), `DeclareCursorIfRequired` (line 578), `EmitSharedNoTargetRuleCall` (line 627). |
| `RuleProcessor.CollectionProcessing.cs` | `while`-loop construction for collection NonTerminals: `EmitCollectionNonTerminalLoop` (line 44), `ResolveCollectionWhileTypeCondition` (line 174), `ResolveContentTypeGuard` (line 264), `ResolveBuilderCall` (line 349), `GenerateInlineOptionalCondition` (line 374), `TryEmitOptionalCondition` (line 413). |
| `RuleProcessor.PatternHandlers.cs` | The four pattern families and unityped/multi-collection dispatch: `TryHandleOperatorLiteralAlternation` (line 44), `TryHandleEmptyVsNonEmptyMembership` (line 118), `TryHandlePocoTypeDispatchWithCompoundAlternatives` (line 238), `TryHandleReferenceOrInline` (line 325), `ProcessMultiCollectionAssignment` (line 500), `ProcessUnitypedAlternativesWithOneElement` (line 509), `EmitCompoundPocoTypeBranch` (line 917). |

### Top-level dispatch (`RuleProcessor.cs:50–66`)

```csharp
internal void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass,
    IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext,
    bool isPartOfMultipleAlternative = false)
{
    ruleGenerationContext.DefinedCursors ??= [];

    if (alternatives.Count == 1)
    {
        this.ProcessSingleAlternative(writer, umlClass, alternatives.ElementAt(0), …);
    }
    else if (alternatives.All(x => x.Elements.Count == 1))
    {
        this.ProcessSingleElementAlternatives(writer, umlClass, alternatives, …);
    }
    else
    {
        this.ProcessMultiElementAlternatives(writer, umlClass, alternatives, …);
    }
}
```

The three branches correspond to dramatically different emission strategies:

- **One alternative** → linear element-by-element emission.
- **Many alternatives, each one element** → either a `switch` over POCO runtime type / a single `?=` keyword guard / a `Sub1 | Sub2` dispatcher (handled in `ProcessUnitypedAlternativesWithOneElement` at `PatternHandlers.cs:509`).
- **Many alternatives, multi-element** → try patterns A–D in order; on failure, emit `Build{Rule}HandCoded` stub call.

---

## 7. **[HARD]** The Cursor Model and the Golden Rule

The single greatest source of subtle bugs in this subsystem — and the dominant historical technical debt the codebase still carries from earlier iterations of the generator — is **incorrect cursor advancement**. Every production regression encountered in the `FeatureSpecialization*` rule family during development traced back to a single broken invariant: the cursor moved either zero times or twice when it should have moved exactly once. The existing unit-test suite did not always catch these because skipped elements were often optional in the test fixtures. Read this section twice.

### 7.1 What a cursor is

A cursor — `CollectionCursor<T>` at `SysML2.NET.Serializer.TextualNotation/Writers/CollectionCursor.cs` — iterates **one** property of a POCO, typically `ownedRelationship`. Cursors are obtained from a per-POCO cache:

```csharp
var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(
    poco.Id, "ownedRelationship", poco.OwnedRelationship);
```

The cache is keyed on `(pocoId, propertyName)` so **the same cursor instance is shared across every builder method that touches the same property of the same POCO during a single serialization**. This sharing is what allows sibling `Build{X}` methods to consume their respective slices of `ownedRelationship` in order.

The cursor only has three operations relevant to the generator:

- `cursor.Current` — element at the current position (`null` once exhausted).
- `cursor.Move(int amount = 1)` (`CollectionCursor.cs:97`) — advances by `amount`, clamped to the end. **Cannot move backwards.**
- `cursor.GetNext(int amount)` — peek without advancing (used by lookahead, §8).

### 7.2 The Golden Rule

> **`cursor.Move()` is emitted exactly once per `+=` assignment processed, and nowhere else.**

`+=` is the only KEBNF operator whose semantics include "consume one element from the collection". Every other construct delegates cursor advance to whatever internal `+=` it ultimately reaches. The full truth table (canonical, quoted from `GRAMMAR.md:82–91`):

| Grammar construct | Advances cursor? |
|---|---|
| `prop += X` (collection assignment) | **Yes — emit `Move()` after processing** |
| `prop = X` (scalar assignment) | No |
| `prop ?= 'keyword'` (boolean assignment) | No |
| `'terminal'` | No |
| `RuleName` (plain NonTerminal reference) | No (the referenced rule may internally `+=`) |
| `RuleName*` / `RuleName+` (collection NonTerminal) | No (each iteration's inner `+=` advances) |
| `(...)` / `(...)?` / `(...)*` (groups) | No (inner `+=` advances) |
| `[QualifiedName]` / `NAME` (value literals) | No |

The pitfall this prevents: a generated `while` loop that calls a sibling builder which *also* `Move()`s, plus its own `Move()`, silently skips every other element. Existing tests don't always catch this because skipped elements are often optional in well-formed input.

### 7.3 Generator-side enforcement

- `DeclareCursorIfRequired` (`RuleProcessor.ElementProcessing.cs:578`) — declares a cursor variable in the emitted method only when (a) a `+=` assignment is reached, (b) the cursor for that property has not already been declared in this method. `RuleGenerationContext.DefinedCursors` is the per-method cache.
- `CursorDefinition.ApplicableRuleElements` — every `+=` assignment that references the same cursor is recorded so that lookahead (§8) and duplicate-detection know which assignments share state.
- For multi-`+=` switches, the generator **always** emits `default: cursor.Move(); break;` so an unexpected element type cannot stall a `while` loop. See `GRAMMAR.md:93`.

### 7.4 The deferred-move trick

When a `+=` lives inside an *optional* `GroupElement`, calling `Move()` unconditionally would advance the cursor even when the optional path is not taken. The generator therefore records a `PendingCursorMove` and only emits the `Move()` inside the optional path's body. See the optional-group handling in `RuleProcessor.ElementProcessing.cs` (around the `AssignmentElement` branch inside `ProcessRuleElement`, roughly line 278–283; the exact line drifts as the file evolves).

### 7.5 Canonical loop — annotated

`SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/FeatureTextualNotationBuilder.cs:110–131` implements `Typings : Feature = TypedBy ( ',' ownedRelationship += FeatureTyping )*`:

```csharp
public static void BuildTypings(IFeature poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
{
    // 7.1: cursor obtained from cache, shared with sibling Build* methods
    var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(
        poco.Id, "ownedRelationship", poco.OwnedRelationship);

    // First, the leading non-terminal — BuildTypedBy consumes its own += internally.
    // No Move() here (Golden Rule: plain NonTerminal does not advance).
    BuildTypedBy(poco, writerContext, stringBuilder);

    // 7.2: 'while' guard from §8 — typed lookahead on cursor.Current.
    while (ownedRelationshipCursor.Current != null
        && ownedRelationshipCursor.Current is IFeatureTyping)
    {
        stringBuilder.Append(", ");

        // 7.3: outer null guard is the "scoping fence" (§9.3) — keeps the
        // pattern-bound variable 'elementAsFeatureTyping' from leaking.
        if (ownedRelationshipCursor.Current != null)
        {
            if (ownedRelationshipCursor.Current is IFeatureTyping elementAsFeatureTyping)
            {
                // Plain delegation. The callee does NOT advance.
                FeatureTypingTextualNotationBuilder.BuildFeatureTyping(
                    elementAsFeatureTyping, writerContext, stringBuilder);
            }
        }

        // 7.4: exactly one Move() per +=, after the assignment is processed.
        ownedRelationshipCursor.Move();
    }
}
```

---

## 8. **[HARD]** Collection `while`-Loop Type Conditions / Lookahead

For `X*` or `X+` over a collection property, a naive `while (cursor.Current != null)` would over-consume — the cursor's collection (`ownedRelationship`) usually holds elements for **several** sibling rules. The generator therefore synthesises a discriminating predicate.

### 8.1 `ResolveCollectionWhileTypeCondition` (`RuleProcessor.CollectionProcessing.cs:174`)

Produces the `while (…)` predicate. Two output modes:

- **Positive form** — `while (cursor.Current is TargetType)` — used when the collection's `+=` targets a single homogeneous type and no sibling rule competes for the same cursor in the same alternative. Demonstrated above in `BuildTypings`.
- **Negative-with-lookahead** — `while (cursor.Current is not null and not NextSiblingType)` — used when the next sibling element in the alternative would consume a different type from the same cursor. The lookahead determines **which** type to exclude.

### 8.2 `ResolveContentTypeGuard` (`RuleProcessor.CollectionProcessing.cs:264`)

Where the referenced rule itself dispatches on a nested property, this method recursively descends into the rule's definition to find the **complementary property pattern** that disambiguates it. For example, when the cursor walks `ownedRelationship` and two sibling rules both wrap their target in `Membership`, this method may emit:

```csharp
cursor.Current is IMembership { OwnedAssignmentTarget.OfType<IExpression>().Any() }
```

The recursion is bounded by the rule graph and guarded against cycles via the existing `RuleGenerationContext` traversal state.

### 8.3 Failure mode this prevents

A representative historical defect: a `while (cursor.Current != null) { BuildFeatureMembership(...); cursor.Move(); }` loop over `ownedRelationship` consumed *all* subsequent elements — including elements that were meant to feed the next sibling rule (e.g., a `Specialization` block). The symptom was silent loss of output for valid inputs, with no exception and no failing assertion in the existing unit tests. Adding the lookahead type guard restored the slice boundaries. This pattern of "silent over-consumption" is the principal reason `ResolveCollectionWhileTypeCondition` was introduced; before it existed, every multi-`+=` rule had to be hand-coded with the lookahead spelled out by the author.

---

## 9. **[HARD]** Type-Discriminator Dispatch (switch + `when` guards)

When one rule has multiple alternatives keyed on the **POCO's runtime metaclass**, the generator emits a C# `switch (poco)` with type patterns, optionally constrained by `when` guards.

### 9.1 Canonical shape

`TypeTextualNotationBuilder.BuildTypeRelationshipPart` at `AutoGenTextualNotationBuilder/TypeTextualNotationBuilder.cs:596–614`:

```csharp
public static void BuildTypeRelationshipPart(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
{
    switch (poco)
    {
        case IType pocoTypeDisjoiningPart when pocoTypeDisjoiningPart.IsValidForDisjoiningPart(writerContext):
            BuildDisjoiningPart(pocoTypeDisjoiningPart, writerContext, stringBuilder);
            break;
        case IType pocoTypeUnioningPart when pocoTypeUnioningPart.IsValidForUnioningPart(writerContext):
            BuildUnioningPart(pocoTypeUnioningPart, writerContext, stringBuilder);
            break;
        case IType pocoTypeIntersectingPart when pocoTypeIntersectingPart.IsValidForIntersectingPart(writerContext):
            BuildIntersectingPart(pocoTypeIntersectingPart, writerContext, stringBuilder);
            break;
        default:
            BuildDifferencingPart(poco, writerContext, stringBuilder);
            break;
    }
}
```

### 9.2 Ordering — `OrderElementsByInheritance`

Switch cases must be emitted **most-derived first**, otherwise a parent type pattern shadows children. The generator sorts NonTerminals by UML inheritance depth (see `GRAMMAR.md:106` and the implementation inside `RuleProcessor`); the *least*-derived case (typically the metaclass that *declared* the rule, i.e. the `NamedElementToGenerate`) becomes the `default:`.

### 9.3 The switch-variable scoping fence

C# pattern variables (`elementAsFeatureMembership` in `if (x is Type elementAsFeatureMembership)`) have **block scope** — they leak into the enclosing block, not just the `if` body. If the same pattern recurs in the same method (common in generated code), name collisions are a compile error. The wrapping `if (cursor.Current != null) { … }` block is a deliberate **scoping fence**. Do not remove these outer null guards without understanding this — see `GRAMMAR.md:129–131`.

---

## 10. **[HARD]** The Four Pattern Families (A / B / C / D)

`ProcessMultiElementAlternatives` tries four specialised handlers before falling back to `Build{Rule}HandCoded`. Each handler is a *structural recognition* of a recurring grammar shape, with an emission strategy that beats the generic fallback. All four live in `RuleProcessor.PatternHandlers.cs`.

> **Failing to match any pattern is not a bug.** It is the design-intended signal that the rule warrants a hand-coded partial. The fallback emits `Build{Rule}HandCoded(poco, writerContext, stringBuilder);` and stops; see §13.

### 10.1 Pattern A — Empty vs Non-Empty Membership

| Aspect | Detail |
|---|---|
| Handler | `TryHandleEmptyVsNonEmptyMembership` (`PatternHandlers.cs:118`) |
| Trigger | Two alternatives, both `+=` to the same property; one uses an `EmptyX` rule, the other `NonEmptyX`. |
| Emitted skeleton | `if (cursor.Current is IT { property.Count: 0 }) BuildEmpty…(...); else BuildNonEmpty…(...);` |
| Example grammar | `ownedRelationship += EmptyMembership \| ownedRelationship += NonEmptyMembership` |
| What it avoids | Falling into the generic `switch (poco)` dispatch which would lack a `Count == 0` discriminator and produce duplicate cases. |

### 10.2 Pattern B — Operator-Literal Alternation

| Aspect | Detail |
|---|---|
| Handler | `TryHandleOperatorLiteralAlternation` (`PatternHandlers.cs:44`) |
| Trigger | Every alternative starts with an `AssignmentElement` whose `Property == "operator"` and `Operator == "="`, differing only in the literal value. (See the exact predicate at `PatternHandlers.cs:55–58`.) |
| Emitted skeleton | `switch (poco.Operator) { case "+": …; case "-": …; … }` |
| Example grammar | Arithmetic/relational operator rules with `'+' \| '-' \| '*' \| '/'`. |
| What it avoids | A naive type-pattern switch (all branches share the POCO type), and a missed opportunity to compile to a fast string switch. |

### 10.3 Pattern C — Runtime-Type Dispatch with Compound Alternatives

| Aspect | Detail |
|---|---|
| Handler | `TryHandlePocoTypeDispatchWithCompoundAlternatives` (`PatternHandlers.cs:238`) |
| Trigger | Two or more alternatives, each beginning with a **NonTerminal whose target metaclass differs** from the others. The remaining elements of each alternative must be processable as a trailing fixed sequence (validated by `AreAlternativeTailElementsProcessable`, `PatternHandlers.cs:429`). |
| Emitted skeleton | `switch (poco) { case Sub1 s1: BuildSub1Rule(s1, …); /* trailing terminals */; break; default: BuildOtherRule(...); /* trailing terminals */; break; }` — each branch emitted by `EmitCompoundPocoTypeBranch` (`PatternHandlers.cs:917`). |
| What it avoids | Hand-coding the type switch and risking forgetting the trailing terminals or the safety-default `Move()`. |

### 10.4 Pattern D — Reference-or-Inline Two-Alternative

| Aspect | Detail |
|---|---|
| Handler | `TryHandleReferenceOrInline` (`PatternHandlers.cs:325`) |
| Trigger | Exactly two alternatives where one form is a cross-reference (`prop += ReferencedRule …`) and the other is a structural inline (`'keyword' '{' ownedContent += … '}'`). The choice is made at runtime from the POCO state. |
| Emitted skeleton | `if (poco.prop.OfType<ReferenceType>().Any()) { /* reference form */ } else { /* inline form */ }` |
| What it avoids | A switch on poco type that would not distinguish these two cases. |

### 10.5 Related: QualifiedName-or-Owned-Chain

This is the same idea but for a *scalar* property: at runtime, choose between emitting a qualified name and recursing into a chain builder. The runtime test is `poco.OwnedRelatedElement.Contains(poco.Type)` (owned → chain builder) or fall through to a `SharedTextualNotationBuilder.AppendQualifiedName` call. See `GRAMMAR.md:151` and the implementation in `RuleProcessor.cs` near the two-alternative checks.

### 10.6 Dispatch order

`ProcessMultiElementAlternatives` tries the four `TryHandle*` methods in the order **A → B → C → D**, then falls through to `EmitHandCodedFallback` if none returns `true`. Each `TryHandle*` is structured to validate **all** preconditions before emitting anything, so order-dependence is local and a non-match is cheap.

---

## 11. **[HARD]** Three-Tier Guard Hierarchy for Ambiguous Dispatch

When `OrderElementsByInheritance` leaves duplicate switch cases (e.g., two rules both target `IFeature`), the generator must disambiguate them. The three tiers, in order of preference:

### 11.1 Tier 1 — Boolean `?=` guards (primary)

If exactly one rule has a unique `?=` boolean assignment that the others lack, the generator emits `when poco.IsXxx` on its case. Code: `PatternHandlers.cs:561–579` (inside `ProcessUnitypedAlternativesWithOneElement`).

Example: `EndUsagePrefix` has `isEnd ?= 'end'`; it gets `case IUsage u when u.IsEnd:`.

### 11.2 Tier 2 — `IsValidFor{RuleName}` extension methods (fallback)

If Boolean guards are insufficient, the generator emits `when poco.IsValidFor{RuleName}(writerContext)` — a call to a **hand-coded** extension method in `SysML2.NET.Serializer.TextualNotation/Writers/TextualNotationValidationExtensions.cs`. These methods consult the cursor's current `ownedRelationship` via the helper `QueryCurrentOwnedRelationship` (`TextualNotationValidationExtensions.cs:193`).

Canonical examples (`TextualNotationValidationExtensions.cs:67–84`):

```csharp
internal static bool IsValidForTypings(this IFeature feature, TextualNotationWriterContext writerContext)
{
    return QueryCurrentOwnedRelationship(feature, writerContext) is IFeatureTyping;
}

internal static bool IsValidForSubsettings(this IFeature feature, TextualNotationWriterContext writerContext)
{
    return QueryCurrentOwnedRelationship(feature, writerContext) is ISubsetting
        and not IRedefinition
        and not IReferenceSubsetting
        and not ICrossSubsetting;
}
```

The generator emits these guards at `PatternHandlers.cs:629` and `:804`.

> **Extension point.** When a new rule introduces a duplicate switch case that Boolean guards cannot resolve (the compiler will refuse to build, or — worse — silently shadow one branch with another), the correct fix is to add a new `IsValidFor{RuleName}` method in `TextualNotationValidationExtensions.cs` and re-run the generator. **Do not** try to patch the generator's pattern-recognition to handle the case structurally unless multiple rules would benefit; doing so was tried earlier and produced fragile, rule-specific branches inside `RuleProcessor` that became a maintenance burden.

### 11.3 Tier 3 — Type ordering (last resort)

When neither boolean guards nor `IsValidFor` methods exist, `OrderElementsByInheritance` orders cases most-derived first; the rule that targets the rule's declared `NamedElementToGenerate` (the least-derived) becomes `default:`. This tier is **structural** rather than semantic; it works only when the runtime type alone uniquely identifies the alternative.

---

## 12. Quantifier & Group Emission Rules

A practical summary — the `RuleElement.Suffix` drives this directly:

| Suffix | Emitted form |
|---|---|
| `?` (zero-or-one) | Single `if (predicate) { body }` — no loop. |
| `*` (zero-or-more) | `while (predicate) { body }` — body may execute zero times. |
| `+` (one-or-more) | Body emitted once unconditionally, **then** the same `while` loop as `*`. |

**KEBNF pitfall** (carried over from `GRAMMAR.md:23`): for `(A | B)+`, the loop must re-test both `A` and `B` each iteration; never collapse to `while (cursor is A)`. The two alternatives may legally interleave.

For `GroupElement` with `IsCollection = true`, the generator emits a single `while` loop whose body is a `switch` over the cursor element. This is one of the cases handled by `ProcessUnitypedAlternativesWithOneElement` (`PatternHandlers.cs:509`).

---

## 13. Hand-Coded Fallback Pattern

When the generator detects an unsupported rule shape it emits a delegating call:

```csharp
Build{RuleName}HandCoded(poco, writerContext, stringBuilder);
```

The hand-coded partial must:

1. Live in `SysML2.NET.Serializer.TextualNotation/Writers/{ClassName}TextualNotationBuilder.cs` — the file **next to** (not inside) `AutoGenTextualNotationBuilder/`.
2. Declare the class as `public static partial class {ClassName}TextualNotationBuilder`.
3. Implement the method as `private static void Build{RuleName}HandCoded(I{Class} poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)`.
4. Reproduce the grammar rule verbatim in `<remarks>` XML doc.
5. Throw `NotSupportedException` for any not-yet-implemented stub.

Every hand-coded implementation must be reviewed against the same four invariants that defeated earlier iterations of the codebase:

1. **Cursor advancement (§7)** — exactly one `Move()` per `+=` processed; never a `Move()` after a callee that already advanced the cursor.
2. **Quantifier semantics (§12)** — `?` is a single `if`, `*` is a `while` that may execute zero times, `+` is one unconditional emission followed by the same `while`.
3. **Type discriminator correctness (§9)** — the value pattern-matched in a `switch` must be the *actual* element on the cursor (e.g. an `ISpecialization` directly, not the `IOwningMembership` wrapping it).
4. **Grammar fidelity** — the element order, alternatives, and target metaclass of the hand-coded method must match the KEBNF rule verbatim.

Bugs in any of these four areas were the dominant source of regressions during the migration from purely hand-coded builders to the generator-first approach, and they remain the costliest to diagnose because they fail silently rather than throwing.

---

## 14. No-Target Rule Resolution & Shared-Builder Lifting

A rule may omit the `: Target` annotation. Such "no-target" rules are placed into one of two homes by `NoTargetRuleResolver` (`SysML2.NET.CodeGenerator/HandleBarHelpers/NoTargetRuleResolver.cs`):

- **`ResolveEffectiveTarget`** (`:46`) — walks the rule's assignments (transitively, following `CollectPropertiesInNoTargetScope` at `:119`), collects the set of *property names* and any *required supertypes*, then asks the UML model for the **minimal** metaclass that owns every property and satisfies every supertype. Falls back to `Element` if the rule has no property assignments.
- **`IsSharedRule`** (`:86`) — returns `true` when the rule's name cannot be matched to any UML metaclass but the rule still has assignments (e.g., `FeaturePrefix`). Such rules are lifted into `SharedTextualNotationBuilder` (the constant `RulesHelper.SharedBuilderClassName` at `:43`).

The shared-builder generation is driven by `UmlCoreTextualNotationBuilderGenerator.GenerateSharedBuilderInternal` (around line 225 of that file).

The remaining helpers — `CollectPropertiesInNoTargetScopeFromElements` (`:140`), `RuleHasAnyAssignment` (`:186`), `ContainsAnyAssignment` (`:204`) — are internal traversal utilities.

---

## 15. Templates & Generator Wiring

### 15.1 Handlebars templates (`Templates/Uml/`)

| Template | Generates |
|---|---|
| `core-textual-notation-builder-template.hbs` | One `{Class}TextualNotationBuilder.cs` (under `AutoGenTextualNotationBuilder/`) per UML class with rules. |
| `core-textual-notation-shared-builder-template.hbs` | `SharedTextualNotationBuilder.cs` (the AutoGen one — distinct from the hand-coded sibling in the parent folder). |
| `core-textual-notation-builder-facade-template.hbs` | Top-level façade entry points used to start writing a POCO. |
| `core-lexical-keywords-template.hbs` | `Keywords.cs` (FrozenSet of reserved keywords). |
| `core-lexical-symbolic-keyword-kind-template.hbs` | `SymbolicKeywordKind.cs` enum. |
| `core-lexical-symbolic-keyword-kind-extensions-template.hbs` | `SymbolicKeywordKindExtensions.cs` (`GetKeyword()`/`GetSymbol()` switch expressions). |

All textual-notation templates call the Handlebars helper `RulesHelper.WriteRule` once per `Build{Rule}` method body.

### 15.2 Generator classes

| Class | Entry point | Output |
|---|---|---|
| `UmlCoreTextualNotationBuilderGenerator` | `GenerateAsync(XmiReaderResult, TextualNotationSpecification, DirectoryInfo)` at `UmlCoreTextualNotationBuilderGenerator.cs:113` | Builder + shared-builder + façade files |
| `UmlCoreLexicalRulesGenerator` | `GenerateAsync(XmiReaderResult, TextualNotationSpecification, DirectoryInfo)` at `UmlCoreLexicalRulesGenerator.cs:95` | `Keywords.cs`, `SymbolicKeywordKind.cs`, `SymbolicKeywordKindExtensions.cs` |
| `GrammarLoader` | `LoadTextualNotationSpecification(...)` | `TextualNotationSpecification` |

Each generator registers the standard helper bundle on its Handlebars instance: `RegisterNamedElementHelper`, `RegisterStringHelper`, `RegisterRulesHelper` (from `RulesHelper.cs:49`). The KerML and SysML KEBNF specifications are merged before generation so that one rule may override the other by name.

---

## 16. Lexical-Rules Generation

`UmlCoreLexicalRulesGenerator` is smaller and simpler than the builder generator. Its three responsibilities:

- **`GenerateKeywordsAsync`** (`UmlCoreLexicalRulesGenerator.cs:110`) — locates the `RESERVED_KEYWORD` rule in the KEBNF (`Resources/SysML-textual-bnf.kebnf:13–26`) and emits its list of single-quoted terminals as a `FrozenSet<string> Keywords.ReservedKeywords`.
- **`GenerateSymbolicKeywordKindAsync`** (`:146`) — emits the `SymbolicKeywordKind` enum and its `GetKeyword()`/`GetSymbol()` extension methods.
- **`TryResolveSymbolicKeywordRule`** (`:205`) — recognises the shape `RULE = '<symbol>' | '<keyword>'` (one alternative non-alphabetic, the other alphabetic — e.g., `DEFINED_BY = ':' | 'defined' 'by'`). Each matching rule contributes one `SymbolicKeywordKind` enum case (`DefinedBy`, `Specializes`, `Subsets`, `References`, `Crosses`, `Redefines`).

Lexical rules feed two consumers: the textual notation builder (which references the keyword set to escape colliding identifiers) and downstream tooling for parsing.

---

## 17. Regeneration & Test Workflow

After any change in `SysML2.NET.CodeGenerator/HandleBarHelpers/` or under `SysML2.NET.CodeGenerator/Templates/Uml/`, the generated output must be regenerated and re-committed. The canonical loop (mirroring `GRAMMAR.md:153–168`):

```bash
dotnet build SysML2.NET.CodeGenerator/SysML2.NET.CodeGenerator.csproj

dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj \
    --filter UmlCoreTextualNotationBuilderGeneratorTestFixture

# Generated files land here:
#   SysML2.NET.CodeGenerator.Tests/bin/Debug/net10.0/UML/_SysML2.NET.Core.UmlCoreTextualNotationBuilderGenerator/

cp SysML2.NET.CodeGenerator.Tests/bin/Debug/net10.0/UML/_SysML2.NET.Core.UmlCoreTextualNotationBuilderGenerator/*.cs \
   SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/

dotnet build SysML2.NET.sln
dotnet test SysML2.NET.sln
```

To gauge how many rules still rely on hand-coded fallback:

```bash
grep -r "HandCoded" SysML2.NET.Serializer.TextualNotation/Writers/AutoGenTextualNotationBuilder/*.cs | wc -l
```

The count should trend down as patterns expand.

---

## 18. Cross-References

**Project-internal**

- `SysML2.NET.CodeGenerator/GRAMMAR.md` — quick reference for `RulesHelper.cs` editors (notation legend, Golden Rule truth table, key-methods table).
- `SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs`, `RuleProcessor*.cs`, `NoTargetRuleResolver.cs` — the implementation this document describes.
- `SysML2.NET.Serializer.TextualNotation/Writers/CollectionCursor.cs`, `CursorCache.cs`, `TextualNotationWriterContext.cs` — the runtime cursor infrastructure that the generator targets.
- `SysML2.NET.Serializer.TextualNotation/Writers/TextualNotationValidationExtensions.cs` — the canonical home for hand-coded `IsValidFor*` guards (Tier 2 of §11).

**Formal specifications**

- `Resources/SysML-textual-bnf.kebnf`, `Resources/KerML-textual-bnf.kebnf` — KEBNF grammar source of truth.
- `Resources/KerML_only_xmi.uml`, `Resources/SysML_only_xmi.uml` — UML metamodel source of truth (metaclasses, properties, inheritance, OCL constraints).
- `Resources/specification/2a-OMG_Systems_Modeling_Language.pdf.txt` — *OMG SysML v2, Part 1: Language Specification*. **Clause 8.2.2.1.1 (EBNF Conventions)** is the authoritative notation legend.
- `Resources/specification/1-Kernel_Modeling_Language.pdf.txt` — *OMG KerML 1.0*. Semantics of metaclasses referenced by KerML rules.
- `Resources/specification/Intro to the SysML v2 Language-Textual Notation.pdf.txt` — SST tutorial; canonical worked examples (informative).

## 19. Historical & Open Technical Debt

This section is deliberately preserved as the institutional memory of the trade-offs that shape the current design. New contributors should expect to encounter the following.

- **Hand-coded fallback surface.** A non-trivial number of rules still emit `Build{Rule}HandCoded` calls because no pattern (A–D) matches their shape. Each is a candidate for a future generator extension *if* the same shape appears in two or more rules; one-off patterns should stay hand-coded. Use the `HandCoded` grep one-liner in §17 to measure progress.
- **Pattern-handler order dependency.** Patterns A–D are tried in order. Today their preconditions are disjoint, but adding a fifth pattern requires re-validating that earlier patterns do not greedily claim its shape. There is no test that asserts pattern-handler exclusivity; reviewers must check by inspection.
- **`IsValidFor*` proliferation.** The Tier-2 fallback (§11.2) is necessary precisely because the generator cannot infer disambiguation from the UML model alone (the metamodel does not encode "this membership is only valid in *this* syntactic context"). Each new guard is small but adds a place where the grammar's behaviour is split between code and metamodel. There is no automated check that every duplicate-case-causing rule has a matching guard.
- **Silent over-consumption failure mode.** As described in §8.3, an incorrect `while` predicate produces no exception, only missing output. This is by far the costliest class of bug to diagnose. The lookahead heuristic in `ResolveContentTypeGuard` works for the rules that exist today but is not a complete decision procedure — a sufficiently exotic future rule may need a fresh case added to the lookahead logic.
- **Cursor sharing across builder methods.** The `(pocoId, propertyName)` cache key is convenient but couples sibling rules: changing the order in which two builders are called can change the output. The contract is implicit — there is no compile-time check that callers visit cursors in a consistent order. Refactors that re-order method calls inside a `Build{Rule}` need careful review.
- **Generator/output round-trip.** Regeneration is a manual `cp` step (§17), not a build target. Forgetting it is a recurrent source of "the generator says X but the file on disk says Y" confusion. Promoting regeneration to an MSBuild target is an open opportunity, blocked only by the cost of running the generator on every build.
