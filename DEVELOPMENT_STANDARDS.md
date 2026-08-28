# Development Standards

Org-wide engineering conventions for C# / .NET projects at Starion Group.
Each repository should reference this file from its own `CLAUDE.md` /
`CONTRIBUTING.md` and add only the project-specific extensions on top.

> **Status:** draft. Source of record once approved: `starion-group/.github`.
> Project-specific overrides belong in the repo's own `CLAUDE.md`.

---

## 0. Precedence: automated tooling over prose

- **A configured `.editorconfig` entry, Roslyn analyzer (built-in, StyleCop,
  `Starion.Analyzers`, or otherwise), or linter rule always wins** over the
  written rules in this document whenever it covers the same concern. Where
  the tool's configured behavior and this document's wording disagree, follow
  the tool.
- **The rules written out below are the fallback** — they apply only where
  the repo has no analyzer, linter, or `.editorconfig` entry enforcing the
  equivalent behavior. As a repo adopts more analyzer coverage, more of this
  document becomes redundant with tooling rather than contradicted by it —
  that's the intended direction, not a problem to fix.
- This applies section-by-section, not document-wide: a repo may have
  `.editorconfig` coverage for §1 (naming, braces, `var` usage) while still
  relying on this document's prose for §3–§8 (test conventions, exceptions,
  docs, workflow, agent boundaries) which are largely judgment calls an
  analyzer cannot express.
- Before treating a rule below as binding, check the repo's `.editorconfig`,
  `Directory.Build.props` / `Directory.Build.targets` (analyzer package
  references and their configured severity), and any `.globalconfig`. If one
  of them already governs the point in question, defer to it — don't flag a
  diff as a violation of this document when it's actually compliant with the
  repo's own tool configuration.

---

## 1. C# code style

### 1.1 Naming & properties

- Use meaningful variable names. `charIndex` not `i`; `currentChar` not `c`;
  `element` not `e`. The exception is short-lived loop counters or trivially-scoped lambda parameters where domain context is already obvious.
- **Always use C# auto-properties**: `public T Foo { get; private set; }`,
  `public T Foo { get; init; }`, `public T Foo { get; }`. NEVER pair a private
  backing field with an expression-bodied or full-getter property when there is
  no non-trivial logic (validation, normalisation, lazy init, event firing).
  Mere storage is never a justification for a backing field — the compiler
  collapses auto-properties to the same IL.

### 1.2 Expressions & pattern matching

- Prefer switch expressions / statements over if-else chains when applicable.
- Prefer C# property patterns (`x is IFoo { Prop: value }`) over the
  declared-variable-plus-predicate form (`x is IFoo name && name.Prop == value`)
  when the narrowed variable is consulted only once. The property-pattern form
  is more concise and intent-revealing.
- Prefer C# collection expressions (`[a, b, c]`, `[..xs]`, `[]`) over
  `new[] { … }`, `new List<T> { … }`, `new T[] { … }` when constructing a
  collection. Applies to both production code AND tests
  (`Is.EqualTo([a, b])` not `Is.EqualTo(new[] { a, b })`; `return [];` not
  `return new List<T>();`). Fall back to explicit construction only when
  type inference cannot pick the right collection type.
- Use `string.IsNullOrWhiteSpace` (not `string.IsNullOrEmpty`) when checking
  the non-nullable value of a string.
- Use `StringBuilder.Append(char)` (not `StringBuilder.Append(string)`) when
  the input is a single-character constant.

### 1.3 Layout

- Surround every braced block (`if`, `else if`, `while`, `for`, `foreach`,
  `switch`, `using`, `try`/`catch`/`finally`, `lock`, `do…while`, anonymous
  `{ }`) with a blank line on both sides. The rule does NOT apply at the very
  start/end of a method body, nor between a `}` and a continuation keyword
  (`else`, `catch`, `finally`, `while` of `do…while`) that belongs to the
  same control flow.

### 1.4 Method-group preference

- **Prefer method-group syntax over a lambda that merely invokes a no-arg
  method.** Both in production code and in tests, write
  `Assert.That(subject.ComputeFoo, Throws.TypeOf<X>())` rather than
  `Assert.That(() => subject.ComputeFoo(), Throws.TypeOf<X>())`;
  pass `subject.Handle` rather than `x => subject.Handle(x)` when wiring up
  an event handler; pass `string.IsNullOrWhiteSpace` rather than
  `s => string.IsNullOrWhiteSpace(s)` to a LINQ predicate.
  The method group is more concise, allocates no closure, and reads as the
  action itself rather than a delegate that calls the action.
- Fall back to a lambda only when (a) the body does more than the bare call
  (transforms args, captures locals, adds null-handling), (b) the method is
  overloaded and the compiler can't infer which overload to bind, or (c) the
  call needs explicit type arguments the method-group form cannot supply.

### 1.5 Null guards in single-expression methods

- When a method's body is a single expression preceded by a null-guard on the
  subject, prefer the **ternary form** over a multi-statement `if`-throw
  block. SonarQube flags the multi-statement form as unnecessarily complex
  when the whole method is otherwise a one-liner.

  ```csharp
  // Preferred
  internal static IFoo ComputeFoo(this IBar subject)
  {
      return subject == null
          ? throw new ArgumentNullException(nameof(subject))
          : subject.definition.SingleOrDefaultStrict<IFoo>(nameof(subject));
  }

  // Avoid (when the body would otherwise be one return statement)
  internal static IFoo ComputeFoo(this IBar subject)
  {
      if (subject is null)
      {
          throw new ArgumentNullException(nameof(subject));
      }

      return subject.definition.SingleOrDefaultStrict<IFoo>(nameof(subject));
  }
  ```

- Keep the `if`-throw form when the method has additional setup (local
  variables, multiple statements, branching), where the ternary would degrade
  readability. The rule applies only when the entire body collapses to one
  return expression.

### 1.6 Expression-bodied members

- Use an expression body (`=>`) for any method or property that has a
  single-expression implementation.

  ```csharp
  public string Name => this.name;
  public bool IsValid => this.ValidationError is null;
  internal static IFoo ComputeFoo(this IBar subject) => subject?.Definition;
  ```

- Use a regular block body with braces for multi-statement implementations,
  and use the ternary form (§1.5) when the body is one expression preceded
  by a null guard.

### 1.7 `var` for local variables

- Use `var` for local variable declarations when the type is unambiguous from
  the right-hand side (constructor call, cast, or literal initialiser).

  ```csharp
  var person = new Person("Alice", 30);
  var names  = new List<string>();
  var typed  = (IFoo)source;
  ```

- Spell out the type explicitly when `var` would hide it — e.g. return
  values from opaque method calls, LINQ chains where the element type is
  not obvious to the reader, or local variables whose declared type matters
  for overload resolution downstream.

### 1.8 Member ordering

- Declare members in this order inside every class / struct:
    1. **Fields** — public → protected → private. No internal fields (use a
       property if cross-assembly access is genuinely required).
    2. **Constructors** — grouped together, in increasing parameter-count
       order.
    3. **Properties** — public → protected → internal → private.
    4. **Methods** — public → protected → internal → private. Within each
       visibility group, sort by name.
- Static members come before instance members within each visibility group.
- This ordering is mechanical, so it's enforceable by analyzer / formatter
  (StyleCop `SA1201`–`SA1206`).

### 1.9 One public type per file

- Each file contains exactly **one public type** (class, record, interface,
  struct, enum, or delegate). No nested public types — small DTOs and
  records each get their own file.
- The file name matches the type name (`Foo.cs` contains `Foo`).
- Private / file-scoped helper types may be co-located in the same file as
  the public type they serve, but only when they are tightly coupled and
  would not be useful in isolation.

### 1.10 Namespace declarations — block-scoped

- Use **block-scoped** namespace declarations. Do NOT use the file-scoped
  namespace syntax (`namespace Foo.Bar;`).

  ```csharp
  // Preferred
  namespace Foo.Bar
  {
      public class Baz
      {
          // ...
      }
  }

  // Avoid
  namespace Foo.Bar;  // file-scoped — do not use
  ```

- Reason: block-scoped namespaces make the type's scope visually explicit
  via indentation, keep imports and usings co-located with the type, and
  remain compatible with older code-style tooling.

### 1.11 Nullable reference types (`#nullable enable`)

- Each project adopts a **single, consistent stance** on nullable reference
  types. The two valid stances:
    1. **Disabled** (default for legacy / migrating codebases): do NOT add
       `#nullable enable` to any new or modified file. Where existing files
       already declare it, leave the directive in place when editing — do not
       remove it. Document optionality with XML doc prose only
       (`/// <returns>The X, or <c>null</c> when …</returns>`).
    2. **Enabled** (default for greenfield projects): turn on
       `<Nullable>enable</Nullable>` in `Directory.Build.props` or the project
       file, omit per-file `#nullable enable` directives, and use `?` and
       `!` annotations consistently across the project.
- Mixing the two stances within one project is forbidden — it confuses
  the analyzer and the reader. Pick one in the project's `CLAUDE.md` /
  `CONTRIBUTING.md` and stick with it.

---

## 2. LINQ

- **Prefer LINQ as much as possible** — including for projection / filter /
  aggregation over collections (`items.Where(…).Select(…).ToList()`,
  `result.AddRange(items.Select(…))`, `items.Any(predicate)`, etc.) instead
  of hand-rolled `foreach` + `if` + `.Add()` loops.
- The ONE exception is straightforward positional or range access on a
  concrete `List`/array: `list[^1]` beats `list.Last()`,
  `array[1..^1]` beats `array.Skip(1).SkipLast(1)` — indexer/range syntax
  is more performant there. Outside that narrow exception, LINQ wins for
  clarity AND maintainability.
- Prefer comparing `Count` to `0` over `Any()` for clarity and performance
  (avoids allocating an enumerator when the collection exposes a `Count`).
- **Flatten a `foreach` with a leading-`if` filter** by pushing the predicate
  into a `.Where(…)` clause on the iterated source. Write
  `foreach (var x in xs.Where(x => predicate))` instead of
  `foreach (var x in xs) { if (!predicate) { continue; } … }`.
  Same for `.OfType<T>()` instead of a runtime `is`-check + cast.
  Applies to nested loops too — push each level's filter onto its own iterator.
  The body should be the action, not the guard.
  Narrow exceptions: (a) the predicate has observable side-effects
  (e.g. `visited.Add(x)`) and the iteration order must be preserved, where the
  LINQ form changes timing; (b) the predicate is too long to read inline —
  extract it to a named local function or method and still call it from the
  `.Where(…)`.

---

## 3. Test conventions

### 3.1 One `[Test]` per method-under-test

- **Default to ONE `[Test]` method per class / method-under-test**, packing
  every scenario (happy path, edge cases, null guards, alternate inputs)
  into multiple `Assert.That` calls inside that one test. Do NOT write one
  `[Test]` per scenario when the setup is shared; that produces a bloated
  test list and duplicated arrange boilerplate.
- Split into separate `[Test]` methods only when each scenario has a
  genuinely distinct, complex setup.
- Test method name: `Verify{MethodUnderTest}` (e.g. `VerifyComputeFoo`).
- No XML docs on test code — see §5.1.

### 3.2 Assertions

- Use `Assert.That(…)` exclusively (the constraint-model API), not the legacy
  `Assert.AreEqual` / `Assert.IsTrue` style.
- Use `Assert.EnterMultipleScope()` ONLY for consecutive related asserts
  whose failures should all be reported in a single run. Do not abuse it as
  a global wrapper.
- For `ArgumentNullException` assertions, use
  `Throws.TypeOf<ArgumentNullException>()` only — do NOT chain
  `.With.Property("ParamName").EqualTo(…)`. The exception type is the
  contract; the param-name is implementation detail.
- Cover both positive AND negative scenarios for every method — at minimum:
  null-guard, empty-input, happy-path, edge-case.

### 3.3 Test data

- **No PII**, no customer/proper-noun names, no real production data in
  fixtures. Use synthesized placeholders (`"the requirement text"`,
  `new Feature { DeclaredName = "Foo" }`).
- Use the method-group form in `Assert.That` whenever the lambda would just
  invoke a no-arg method (see §1.4).

### 3.4 Hoist repeated literal arrays to `static readonly` fields

- When the same constant array literal is passed to a method more than once
  in a fixture (e.g. across multiple `Assert.That` calls inside one `[Test]`
  method, or across `[TestCase]` rows), extract it to a `private static
  readonly` field on the fixture class. SonarQube flags repeated inline
  array literals as `S3878` ("Prefer 'static readonly' fields over constant
  array arguments if the called method is called repeatedly and is not
  mutating the passed array") because each call site allocates a new array.

  ```csharp
  // Preferred
  [TestFixture]
  public class FooTestFixture
  {
      private static readonly string[] TwoElementSequence = ["first", "second"];

      [Test]
      public void VerifyBar()
      {
          Assert.That(() => TwoElementSequence.Bar(),  Throws.TypeOf<X>());
          Assert.That(() => TwoElementSequence.Baz(),  Is.EqualTo(2));
      }
  }

  // Avoid: each line allocates a fresh array
  Assert.That(() => new[] { "first", "second" }.Bar(), Throws.TypeOf<X>());
  Assert.That(() => new[] { "first", "second" }.Baz(), Is.EqualTo(2));
  ```

- The field name should describe the sequence's semantic role
  (`TwoElementSequence`, `ExpectedNamesAfterSort`), not its literal value.
- This rule does NOT apply when the array is used exactly once — leave the
  inline literal in place to keep the data next to the assertion.

### 3.5 Every change ships with a test

- **No production change merges without a corresponding test.**
- **New code** — add a new fixture in the matching test project.
- **Modified code** — extend the existing fixture with the affected
  scenarios (per §3.1, packed into the same `[Test]` method when possible).
- **Bug fix** — add the regression test FIRST (red), then the fix
  (green). The test commit and the fix commit may be squashed, but the
  regression scenario must be visible in the final diff.
- Tests that are temporarily skipped via `[Ignore]` / `[Test(Skip = …)]`
  must reference a tracking issue and a target date for re-enablement.

### 3.6 Verification gate after every fix

- Run a **focused** verification gate after every fix — build, then the
  test fixtures relevant to the modified files. Do NOT run the full
  solution test suite locally; that's CI's job.
- The shape of the gate is project-specific (different solutions name
  things differently), but every project's `CLAUDE.md` / `CONTRIBUTING.md`
  must publish the canonical commands. As a template:

  ```bash
  # Build
  dotnet build <solution-file> --no-restore --configuration Release \
      -p:ContinuousIntegrationBuild=true

  # Targeted tests (filter by fixture name)
  dotnet test <touched-test-project> --configuration Release \
      --no-build --no-restore \
      --filter "FullyQualifiedName~<NewTestClass>|FullyQualifiedName~<TouchedFixture>" \
      --verbosity minimal
  ```

- A build error or a failing focused test **blocks the work from being
  marked complete**. Fix forward — do NOT skip the test, mark it
  inconclusive, comment it out, or weaken the assertion to make the gate
  pass. If a test is genuinely stale, delete it in a separate commit with
  the rationale in the message; don't disable it.

---

## 4. Exceptions & error handling

### 4.1 Exception types

- Use `NotSupportedException` (NOT `NotImplementedException`) for placeholder
  / stub methods that require manual implementation. `NotImplementedException`
  is reserved for code that intentionally has no implementation in some
  configurations.
- **Split distinct failure modes into distinct exception types.** When a
  helper can fail for semantically different reasons (e.g. "required input
  missing" vs "input over-bounded"), each mode gets its own exception type.
  This lets callers `catch` precisely and lets diagnostics communicate the
  actual defect. A single `WrongStateException` that means six things is an
  anti-pattern.
- Document every exception the method throws with a `<exception>` XML doc tag
  per type, with a one-line description of the trigger.

### 4.2 Validation boundaries

- Validate at the system boundary: user input, external API responses,
  deserialised payloads, file contents. Trust internal code and framework
  guarantees — don't add defensive null-checks or argument validation for
  scenarios that can't happen given the type system and the call graph.
- Never silently swallow an error. If a state is unexpected, throw. If a
  state is expected, model it (`null`, `Option<T>`, a `Result` type, an
  enum-discriminated record).

---

## 5. Documentation & comments

### 5.1 XML docs

- **Every type and every one of its members carries XML docs** — not just
  methods. That means classes, interfaces, structs, records, enums and
  delegates, and within them: constructors, methods, properties, indexers,
  events, fields, enum literals and generic type parameters. This holds
  regardless of visibility (`public`, `internal`, `protected`, `private`).
- Apply each tag on the cases where it applies, with none omitted:

  | Tag | When |
  |---|---|
  | `<summary>` | every type and every member, always |
  | `<param>` | one per parameter |
  | `<typeparam>` | one per generic type parameter |
  | `<returns>` | any non-`void` member |
  | `<exception>` | every exception thrown directly (see §4.1) |

- **Test projects are exempt.** Test fixtures, their `[Test]` methods, their
  setup/teardown and their helper members need no XML docs. Reason: fixtures
  are already large, the `Verify{MethodUnderTest}` naming (§3.1) plus the
  assertions state the intent, and doc comments would only add bulk to a file
  nobody consumes as an API. Production code carries the full rule above.
- Avoid placeholder text (`"Computes the derived property"`, `"the computed
  result"`). The summary should describe what the method does and why.
- **Do NOT use `<inheritdoc/>`** as a shortcut for inherited / interface
  members. Write an explicit `<summary>` on every member. If the member
  differs from its base or interface contract, add a `<remarks>` section
  describing the difference. Reason: `<inheritdoc/>` makes the contract
  invisible at the point of definition and forces readers to navigate to
  the base/interface to understand what the override does (or fails to do
  differently).
- **Multi-line summary format**, not one-liners on the same line as the tag.
  Each `<summary>` opens on its own line, the prose follows on the next
  line, and `</summary>` closes on its own line. Same for `<remarks>`.

  ```csharp
  // Preferred
  /// <summary>
  /// Returns the name of the person.
  /// </summary>
  /// <param name="person">The person whose name to return.</param>
  /// <returns>The person's name; never <c>null</c>.</returns>
  public string GetName(Person person) => person.Name;

  // Avoid
  /// <summary>Returns the name of the person.</summary>
  public string GetName(Person person) => person.Name;

  // Avoid
  /// <inheritdoc/>
  public string GetName(Person person) => person.Name;
  ```

- **Concise beats exhaustive.** A doc comment is a contract, not an essay:
  state what the member does, what each argument means, what comes back and
  when it throws — then stop. One sentence per tag is the target, two is the
  ceiling. Documentation that reads TL;DR gets skipped, which is worse than
  terse documentation that gets read.
    - Don't restate the signature in prose ("Gets or sets the Name property
      of type string").
    - Don't narrate the implementation, its history, or the alternatives
      considered.
    - Don't repeat on an override what the base already says, unless the
      behaviour genuinely differs — and then document only the difference
      (in `<remarks>`, per the `<inheritdoc/>` rule above).
- Write the docs AFTER the implementation is complete so that the prose
  accurately reflects the final behaviour and edge cases — not the
  pre-implementation intent.

### 5.2 Inline comments

- Only add comments where the logic is not self-evident. Don't narrate
  what the code does line-by-line — let the code speak.
- **Comments are the exception, not the norm, and each one is short.**
  Readable code needs few of them; a method dense with comments is usually a
  method that wants better names or an extracted helper. Comment the *why*,
  never the *what* — if a comment paraphrases the line below it, delete it.
  Keep each to a single line where possible.
- Legitimate uses: a non-obvious spec / standard constraint being honoured, a
  deliberate deviation from the obvious implementation, a workaround together
  with its cause, or a known-tricky edge case.
- No section-banner comments (`// ---- helpers ----`) and no commented-out
  code.
- Avoid `// TODO: ask <name>` — names rot, leak org structure, and create
  silent dependencies on individuals. Use `// TODO(<repo>#<issue>): …`
  pointing at a tracked issue, or fix it now.
- Don't leave dead `// removed XYZ` or `// was: …` comments. Source control
  is the history; comments are for present-tense intent.

---

## 6. Confidentiality & repo hygiene

### 6.1 Paths

- **Paths are ALWAYS repo-relative — NEVER absolute.** This applies to every
  path you write anywhere: code comments, XML doc `<see cref="…"/>` and prose,
  source-string citations, error/log messages, commit messages, PR bodies,
  GitHub issue bodies, team-notes / spec files, plan files, prompts and
  agent briefs (e.g. say `SubProject/Foo/BarExtensions.cs`, NOT
  `C:\CODE\Repo\SubProject\Foo\BarExtensions.cs` and NOT
  `/c/CODE/Repo/...`). Use forward slashes.
- Reason: absolute paths are user-/machine-specific and leak the local
  filesystem into the repo and into communication with other contributors —
  they break for anyone else, get stale on rename/move, and are noisy.
- The ONLY exception is tool / IDE arguments that require an absolute path
  (e.g. an LLM agent's `Read`/`Edit`/`Write` `file_path` parameter, or a
  `cd` target in a script). Those are not user-visible artifacts.

### 6.2 No leakage of internal-only data

- No customer / project / proper-noun names in code, tests, commits, or
  documentation. Use generic placeholders.
- No machine-specific paths, user names, hostnames, IP addresses, internal
  URLs, or environment-dependent values hardcoded in source. Push them to
  configuration.
- No secrets, API keys, tokens, certificates, or credentials in source —
  ever. Use a secrets manager (Azure Key Vault, AWS Secrets Manager,
  GitHub Actions secrets, etc.) and reference by name.
- No PII in tests or fixtures (real e-mails, names, phone numbers).
  Use synthesized data.
- No timestamps, GUIDs, or local file paths in generated code that vary per
  build — output must be byte-deterministic.

### 6.3 Encoding & formatting

- Files end with `LF` (newline) — configure `.editorconfig` and
  `.gitattributes` accordingly.
- UTF-8 encoding, no BOM unless the consumer requires it (some legacy
  Windows tools do; default is no BOM).
- Trim trailing whitespace on every line. Trim trailing blank lines at
  end-of-file.

---

## 7. Branch / commit / PR workflow

- **Direct pushes to `master` (or `main`) and to the integration branch
  (`development` / `develop`) are forbidden.** All work lives on a feature
  branch.
- Feature branches target the integration branch via PR; the release branch
  (`master`/`main`) is downstream-only.
- Branch naming: `<issue-number>-<short-kebab-description>` (e.g.
  `312-multiplicity-violation`).
- Commit messages: short, imperative, prefixed with one of `[Add]`,
  `[Update]`, `[Remove]`, `[Fix]` — or the canonical `Fix #<n>` /
  `Fix #<n1> #<n2> …` form for issue-closing commits (so the merge auto-closes
  the issues on GitHub).
- One commit = one logical change. Squash trivia (formatting, typo fixes)
  before opening the PR.
- Never use `git push --force` or `--force-with-lease` against the
  integration or release branch. On feature branches, force-with-lease is
  permitted but should be communicated to anyone else collaborating.
- Never bypass hooks (`--no-verify`, `--no-gpg-sign`, `commit.gpgsign=false`)
  unless a teammate has explicitly approved a one-off.

### 7.1 PR hygiene

- PR title mirrors the commit message (under 70 chars).
- PR body: a short Summary section, a Test Plan section, and a link to the
  tracking issue.
- Self-review the PR diff before requesting review from a teammate.
- CI must be green before merge. Don't disable a failing check — fix the
  underlying issue.

---

## 8. AI / agent collaboration

Applies when working with LLM coding assistants (Claude Code, Copilot Chat,
Cursor, etc.).

### 8.1 Agent boundaries

- **Agents must NOT auto-commit.** `git commit` is the human's
  responsibility — no exceptions, no asking, no "for convenience". The human
  reviews `git diff` and commits manually.
- **Agents must NOT push commits, open PRs, or merge by default.**
  Push / PR / merge are the human's job. The agent only performs these if
  the human explicitly asks in-conversation; otherwise it stays out of git
  remote operations entirely.
- When the agent creates a branch, it must (a) create it locally with
  `git switch -c <branch> origin/<integration-branch>`, and
  (b) immediately push the empty branch with
  `git push -u origin <branch>`. That's the only push the agent performs by
  default — it's safe because the branch tip equals the integration branch's
  tip.

### 8.2 Agent output discipline

- All paths in agent-authored artifacts are repo-relative (§6.1).
- Agent commit messages have NO `Co-Authored-By` trailer and NO
  "🤖 Generated with …" footer unless the human asked for them. The human
  is the author of record.
- Agent never references customer-specific data, machine paths, or
  user names in PR bodies, issue comments, or code comments.
- If a tool action is blocked (hook denial, permission prompt), the agent
  surfaces the block to the human rather than retrying with workarounds.

### 8.3 Failure modes & escalation

- If the agent encounters a stub (`throw new NotSupportedException(…)`) on a
  dependency, it does NOT implement it as a side effect of the current task.
  Implementing stubs is a separately scoped change. The agent should surface
  the blocker and leave the stub in place.
- If the agent's diff grows beyond the scope the human approved, the agent
  stops and asks before continuing.
- "Scope discipline": when a task names specific file(s), don't modify other
  production files even if a stub or unrelated code looks suspicious. Leave
  the side-issue for a separate PR.

---

## 9. Generated code is read-only

- **Never edit generated files directly.** Every project that produces code
  via a generator (T4, Roslyn source generators, custom code-gen pipelines,
  Handlebars templates, OpenAPI / gRPC / Protobuf tooling, etc.) marks the
  generated output as read-only. Edit the generator's input — the template,
  the schema, the model — and re-run the generator.
- Recognise generated files by:
    - a folder convention (`AutoGen*`, `Generated*`, `obj/`, etc.),
    - a marker comment in the file header
      (`// THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!`),
    - a `.g.cs` / `.generated.cs` filename suffix,
    - or an `[GeneratedCode]` attribute on the type.
- If a generated file has a bug or needs a new feature, locate the
  generator (template, processor, schema) and change THAT — then re-run
  the generator. Hand-editing the output is treated as a policy violation
  in code review; the diff will be reverted on the next generator run.
- Hand-authored extensions to generated types belong in a separate
  `partial class` (or extension method class) in a non-generated file. The
  generated half stays untouched; the hand-coded half evolves freely.

---

## Enforcement

- **Mechanical rules** (auto-properties, blank-line-around-braces, LINQ
  preference, naming, collection expressions): enforced by
  `.editorconfig` + Roslyn analyzers where a repo has them configured
  (planned org-wide: `Starion.Analyzers` NuGet) — see §0. This document's
  wording is the fallback for repos without that coverage yet, not a
  second, competing source of truth.
- **Judgment rules** (test consolidation, exception design, comment style,
  branch workflow, agent boundaries): enforced by code review and by
  reference from each repo's `CLAUDE.md` / `CONTRIBUTING.md`.
- **Static analysis (SonarQube / SonarCloud)** runs on every PR via CI. The
  rules in this document are deliberately aligned with the SonarQube rule
  set — fixing an SQ finding should usually be straightforward by applying
  the corresponding rule below.

## SonarQube rule cross-reference

The following SQ rules map directly to sections of this document. When the
SQ analyzer flags an issue, locate the rule in this table and apply the
remediation cited:

| Rule | Title (excerpt) | This-doc section |
|---|---|---|
| `S1125` | "Boolean literals should not be redundant" | §1.2 (switch / patterns) |
| `S1135` | "Track uses of 'TODO' tags" | §5.2 (no `// TODO: ask <name>`) |
| `S1854` | "Unused assignments should be removed" | §5.2 (no dead code in comments either) |
| `S2178` | "Short-circuit logic should be used in boolean contexts" | §1.2 |
| `S2933` | "Fields that are only assigned in the constructor should be 'readonly'" | §1.1 (auto-properties / readonly) |
| `S2955` | "Generic parameters not constrained to reference types should not be compared to null" | §4.2 |
| `S3358` | "Ternary operators should not be nested" | §1.5 (use the simple ternary; nest only when readable) |
| `S3878` | "Arrays should not be created for params parameters" / "Prefer 'static readonly' fields over constant array arguments" | §3.4 (hoist repeated literal arrays) |
| `S3963` | "'static' fields should be initialized inline" | §3.4 |
| `S4136` | "Method overloads should be grouped together" | §1.3 (layout / readability) |
| `S6602` | "'Find' method should be used instead of the 'FirstOrDefault' extension" | §2 (LINQ; a project-specific override may apply where `FirstOrDefault` deliberately honours a specification contract — document the override in the repo's own `CLAUDE.md`) |
| `S6604` | "Collection-specific method should be used instead of the 'Any' extension method" | §2 (Count == 0 over Any()) |
| `S6605` | "Collection-specific 'Exists' method should be used instead of the 'Any' extension" | §2 |
| `S6610` | "'StringComparison' should be used explicitly" | §1.2 |
| `S6618` | "'string.Create' should be used instead of 'FormattableString'" | §1.2 |
| `CA1825` | "Avoid zero-length array allocations" | §1.2 (collection expressions `[]`) |
| `CA1829` | "Use 'Length' / 'Count' property instead of 'Enumerable.Count' method" | §2 |

This table is not exhaustive — when an SQ rule is hit that is not listed,
either (a) the fix is obvious from the rule's own description and the spirit
of these standards, or (b) it surfaces a gap in this document that should
be amended via PR.

### Most frequently recurring findings

Two SQ findings dominate the backlog across projects — in static helper /
extension classes and their fixtures in particular. Their canonical fixes:

1. **Static readonly arrays in test fixtures** — see §3.4. Whenever a
   constant array literal is the argument to a tested method invoked more
   than once across the fixture, hoist it.
2. **Ternary null-guard in single-expression methods** — see §1.5. When the
   method body collapses to one return after the null check, use the
   ternary form.

## Project-specific overrides

A project's own `CLAUDE.md` or `CONTRIBUTING.md` can override any rule here
with a documented justification, but should NEVER silently diverge. If a
rule no longer fits, propose the change against this document — don't
fork the conventions per repo.
