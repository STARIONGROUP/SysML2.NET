# TESTING.md — Unit-test conventions for SysML2.NET

> **When to read this file:** load it whenever you are writing or modifying unit tests in any `*.Tests/` project of this solution. It is the authoritative companion to `CLAUDE.md` for everything test-related. Outside of test authoring, you do not need it.

This document captures the conventions enforced for the NUnit test fixtures across `SysML2.NET.Tests/`, `SysML2.NET.Serializer.Json.Tests/`, `SysML2.NET.Serializer.Xmi.Tests/`, `SysML2.NET.Extensions.Tests/`, `SysML2.NET.CodeGenerator.Tests/`, and every other `*.Tests` project. The rules below have been distilled from explicit author-review feedback — diverging from them produces churn and PR back-and-forth, so treat them as binding.

---

## 1. Test framework

- **Framework:** NUnit
- **Fixture attribute:** `[TestFixture]`
- **Test attribute:** `[Test]`
- **Project layout:** mirror the production namespace under the test project (e.g. `SysML2.NET/Extend/FeatureExtensions.cs` → `SysML2.NET.Tests/Extend/FeatureExtensionsTestFixture.cs`).

---

## 2. One `[Test]` per method-under-test (default)

**Default to a single `[Test]` method per `Compute*` / method-under-test** and pack every scenario — happy path, edge cases, null guards, alternate inputs — into multiple `Assert.That` calls inside that one test.

**Why this is the default:** keeps the test list compact, removes duplicated arrange boilerplate, and makes the intent obvious — one `Compute*` operation has one combined coverage test. It works particularly well for the `Compute*` derivation tests where scenarios build on one shared subject incrementally (null guard → empty subject → wrong-target negative → positive populated case → multi-element ordered case).

### When separated `[Test]` methods ARE appropriate

The combined-form is the default, **not absolute**. Split into separate `[Test]` methods when **each scenario has a genuinely distinct, complex setup** that would tangle if packed into one test:

- different bridge / relationship metaclass per scenario,
- a multi-step containment chain that exists only for one scenario,
- an incompatible-source-or-target shape that requires its own local POCO fixtures.

In those cases, separate `[Test]` methods with descriptive scenario suffixes are clearer than one mega-test with branching arrange blocks. The canonical *separated* example is `SysML2.NET.Tests/Extensions/ElementExtensionsTestFixture.cs` — each `AssignOwnership_*` method exercises a distinct scenario (cycle, incompatible owner type, incompatible target type, valid parameters, …) with its own local construction.

Rule of thumb: if you find yourself naming locals `feature1` / `feature2` / `bridge1` / `bridge2` or writing more than ~3 lines of arrange between asserts, that scenario probably wants its own `[Test]`.

### Canonical example

`SysML2.NET.Tests/Extend/FeatureExtensionsTestFixture.cs`:

```csharp
[Test]
public void VerifyComputeOwnedFeatureChaining()
{
    Assert.That(() => ((IFeature)null).ComputeOwnedFeatureChaining(), Throws.TypeOf<ArgumentNullException>());

    var feature = new Feature();

    Assert.That(feature.ComputeOwnedFeatureChaining(), Has.Count.EqualTo(0));

    var subsetting = new Subsetting();
    feature.AssignOwnership(subsetting);

    Assert.That(feature.ComputeOwnedFeatureChaining(), Has.Count.EqualTo(0));

    var chainingTarget1 = new Feature();
    var chaining1 = new FeatureChaining { ChainingFeature = chainingTarget1 };
    feature.AssignOwnership(chaining1);

    var chainingTarget2 = new Feature();
    var chaining2 = new FeatureChaining { ChainingFeature = chainingTarget2 };
    feature.AssignOwnership(chaining2);

    var result = feature.ComputeOwnedFeatureChaining();

    using (Assert.EnterMultipleScope())
    {
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0], Is.SameAs(chaining1));
        Assert.That(result[1], Is.SameAs(chaining2));
    }
}
```

One test, five logical scenarios: null subject → empty subject → unrelated-ownership subject → first populated case → multiple-element ordered case.

---

## 3. Cover positive AND negative cases in the same test

Each `[Test]` MUST exercise both directions of the method-under-test. Build up state incrementally and assert the delta after each arrange step.

**Negative cases (always include where applicable):**

| Negative scenario          | Assertion                                                                |
| -------------------------- | ------------------------------------------------------------------------ |
| Null subject               | `Assert.That(() => …, Throws.TypeOf<ArgumentNullException>())`           |
| Empty / unpopulated subject | `Assert.That(result, Has.Count.EqualTo(0))`                              |
| Wrong-target scenario       | populated subject whose owned elements don't match the rule's filter — assert `Has.Count.EqualTo(0)` |
| Out-of-scope stub           | `Assert.That(() => …, Throws.TypeOf<NotSupportedException>())` — see §9 |

**Positive cases:** populated subject, multiple elements with verified ordering, dedup semantics where the OCL says so.

**Pre/post-arrange assert ladder** — see `SysML2.NET.Tests/Extend/NamespaceExtensionsTestFixture.cs`:

```csharp
[Test]
public void VerifyComputeImportedMembership()
{
    Assert.That(() => ((INamespace)null).ComputeImportedMembership(), Throws.TypeOf<ArgumentNullException>());

    var namespaceElement = new Namespace();

    Assert.That(namespaceElement.ComputeImportedMembership(), Has.Count.EqualTo(0));

    var importedNamespace = new Namespace();
    var importedElement = new Definition { DeclaredName = "imported" };
    var importedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
    importedNamespace.AssignOwnership(importedMembership, importedElement);

    var namespaceImport = new NamespaceImport { ImportedNamespace = importedNamespace };
    namespaceElement.AssignOwnership(namespaceImport);

    Assert.That(namespaceElement.ComputeImportedMembership(), Is.EquivalentTo([importedMembership]));
}
```

The wrong-target negative case is illustrated in `FeatureExtensionsTestFixture.VerifyComputeOwnedFeatureInverting` — a `FeatureInverting` pointing at *another* feature is added first (asserted to produce count 0) **before** the positive-case `FeatureInverting` pointing at the subject is added.

---

## 4. Always use `Assert.That` — no legacy forms

Every assertion (including exception assertions) MUST use the fluent `Assert.That(...)` form.

**Forbidden legacy forms:**

| Don't write              | Write instead                                                            |
| ------------------------ | ------------------------------------------------------------------------ |
| `Assert.Throws<T>(…)`    | `Assert.That(() => …, Throws.TypeOf<T>())`                               |
| `Assert.IsTrue(x)`       | `Assert.That(x, Is.True)`                                                |
| `Assert.IsFalse(x)`      | `Assert.That(x, Is.False)`                                               |
| `Assert.AreEqual(a, b)`  | `Assert.That(b, Is.EqualTo(a))`                                          |
| `Assert.IsNull(x)`       | `Assert.That(x, Is.Null)`                                                |
| `Assert.IsNotNull(x)`    | `Assert.That(x, Is.Not.Null)`                                            |

**Exception-message assertions** are still a single fluent chain — no scope wrapper:

```csharp
Assert.That(() => parser.Parse(input),
    Throws.TypeOf<FormatException>().With.Message.Contains("unexpected token"));
```

**Why:** consistency and readability. The mixed-style `Assert.Throws` / `Assert.IsTrue` family was explicitly called out as non-idiomatic.

---

## 5. `Assert.EnterMultipleScope` — use only for consecutive asserts

Use `using (Assert.EnterMultipleScope()) { … }` **only** when **two or more consecutive** `Assert.That` calls follow each other and you want all of them to be evaluated even if earlier ones fail.

**Do:**

```csharp
using (Assert.EnterMultipleScope())
{
    Assert.That(result, Has.Count.EqualTo(2));
    Assert.That(result[0], Is.SameAs(chaining1));
    Assert.That(result[1], Is.SameAs(chaining2));
}
```

**Don't** wrap a single fluent assertion in a scope — even when it is a long chain like `Throws.TypeOf<T>().With.Message.Contains("…")`. A single assertion does not need it.

**Don't** put the early null-guard `Assert.That` inside a scope that ends before the next `Assert.That` — those are standalone and stay outside any scope.

---

## 6. Test naming

**Combined-form tests (the default — §2):**

- One test method = `public void Verify{MethodUnderTest}()` (e.g. `VerifyComputeOwnedFeatureChaining`, `VerifyComputeImportedMembership`).
- Matches the existing convention across `SysML2.NET.Tests/Extend/*` and is what reviewers will look for.
- Do **not** suffix with scenario names — scenarios live inside the body.

**Separated-form tests (the §2 exception):**

- Use `{MethodUnderTest}_{ScenarioDescription}_{ExpectedOutcome}` (e.g. `AssignOwnership_WithIncompatibleOwnerType_ThrowsInvalidOperationException`, `AssignOwnership_WithContainmentCycle_ThrowsInvalidOperationException`).
- Each method should be self-explanatory from its name alone — the scenario is in the title because it's distinct enough to deserve its own test.

---

## 7. Arrange / Act / Assert inside a combined test

- Separate arrange / act / assert blocks with **blank lines**.
- Add short inline comments only when the delta from the previous step is non-obvious.
- Re-call the method-under-test after each new arrange step so each `Assert.That` describes one increment of state.
- Reuse the same locals (`feature`, `namespaceElement`, `result`) across the test — don't fork into `feature1` / `feature2` unless you really need two independent subjects.

---

## 8. Assertion idiom preferences

| Concern                 | Prefer                                                       | Avoid                                             |
| ----------------------- | ------------------------------------------------------------ | ------------------------------------------------- |
| Collection count        | `Has.Count.EqualTo(n)`                                       | `result.Count, Is.EqualTo(n)`                     |
| Reference equality (POCO) | `Is.SameAs(expected)`                                      | `Is.EqualTo(expected)` (relies on `Equals`)       |
| Order-irrelevant collection equality | `Is.EquivalentTo([…])`                          | `Is.EqualTo([…])` (forces order)                  |
| Order-relevant collection equality   | `Is.EqualTo([…])`                              | hand-rolled `for (int i …)` loops                 |
| First / last element    | `result[0]` / `result[^1]` (indexer)                         | `result.First()` / `result.Last()` (LINQ)         |
| Range / slice           | `result[1..^1]`                                              | `result.Skip(1).Take(n)`                          |
| String empty/null       | `Is.Null.Or.Empty`                                           | `string.IsNullOrEmpty(x), Is.True`                |
| Substring               | `Does.Contain("…")` or `Contains.Substring("…")`             | `x.Contains("…"), Is.True`                        |

The LINQ/indexer preference aligns with the project-wide quality rule documented in `CLAUDE.md` ("Prefer indexer syntax and range syntax over LINQ methods when applicable").

---

## 9. Scope discipline — assert, don't fix

If a test you are writing crosses into a method that currently throws `NotSupportedException` because it's an out-of-scope stub, **assert that exception** — do **not** implement the stub to make the test pass:

```csharp
Assert.That(() => member.ComputeOwnedMemberFeature(),
    Throws.TypeOf<NotSupportedException>());
```

When the stub is implemented later (in its own scoped change), the assertion will be replaced with the real positive case in the same `[Test]`.

This is the testing-side companion to the broader scope-discipline feedback: a task scoped to file X must not silently grow to also modify file Y, even if file Y is "one line away from working".

---

## 10. Anti-pattern checklist (what NOT to do)

- ❌ Splitting one method-under-test into many `…_WhenX_DoesY` tests **when the scenarios share setup and could trivially combine** (combined-form is the default — see §2). Splitting is acceptable when each scenario has a genuinely distinct, complex setup.
- ❌ `Assert.Throws<T>` / `Assert.IsTrue` / `Assert.AreEqual` / `Assert.IsNull` (any legacy NUnit API).
- ❌ Wrapping a single `Assert.That` inside `Assert.EnterMultipleScope`.
- ❌ Putting standalone, non-adjacent asserts inside a scope that's already closed.
- ❌ Suffixing the combined-form `Verify{MethodUnderTest}` test name with a scenario (`VerifyComputeFooWhenNull`) — scenarios live in the body. (The separated-form `{Method}_{Scenario}_{Outcome}` style is the §6 exception and is fine when §2's separated-form criteria apply.)
- ❌ Implementing an out-of-scope stub from within a test fixture change.
- ❌ Asserting only the positive case (or only the negative case) — every test covers both.
- ❌ `result.First()` / `result.Last()` / `result.Count() == 0` where indexer / `Has.Count.EqualTo` works.

---

## 11. Reference fixtures

When in doubt, model new fixtures on these (they reflect the current canonical styles):

**Combined-form (the default — §2):**

- `SysML2.NET.Tests/Extend/FeatureExtensionsTestFixture.cs` — null guard → empty subject → wrong-target negative → populated positive → multi-assert scope.
- `SysML2.NET.Tests/Extend/NamespaceExtensionsTestFixture.cs` — pre/post-arrange assert ladder, `Is.EquivalentTo([…])` collection idiom, no `EnterMultipleScope` when assertions are non-consecutive.

**Separated-form (the §2 exception):**

- `SysML2.NET.Tests/Extensions/ElementExtensionsTestFixture.cs` — each `AssignOwnership_*` `[Test]` exercises a distinct scenario (cycle, incompatible owner type, incompatible target type, null guards, valid parameters, reference-only bridge, …) with its own local POCO construction. Scenario-suffixed names are appropriate here because each scenario is too distinct to combine cleanly.
