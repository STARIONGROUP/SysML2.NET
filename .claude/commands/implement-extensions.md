---
description: Spawn researcher/implementer/tester/reviewer team to fill in stub Compute* methods in a SysML2.NET Extend file
argument-hint: <absolute-path-to-Extensions-file.cs>
---

# /implement-extensions

Spawn the 4-role agent team (researcher → implementer → tester → reviewer) for
**$ARGUMENTS** — a `*Extensions.cs` file under `SysML2.NET/Extend/` whose
`Compute*` methods are still stubs throwing `NotSupportedException`. Per-role
models are picked dynamically based on stub complexity (see step 3.5).

The team template is at `C:\Users\atheate\.claude\team-templates\extension-impl.md`
(v2). Read it first — its role prompts are the source of truth; this command body
is the orchestration glue.

## Hard scope rule

**Only modify**:
- The named production file `$ARGUMENTS`
- Its corresponding test fixture (auto-derived in step 2)
- The researcher's notes file (auto-derived in step 2)
- Sibling test fixtures whose `Throws.TypeOf<NotSupportedException>()` assertions
  now fail because of regressions caused by the new implementations (regression
  sweep in step 8)

**MUST NOT modify**:
- Any other production file in `SysML2.NET/Extend/`, `SysML2.NET/Core/`, etc.
- Auto-generated POCOs and interfaces.
- Code-generator templates.

This is the user-memory `feedback_scope_discipline.md` rule. Even when an adjacent
stub blocks dependent test coverage, surface the blocker; do not silently expand
scope. Use the stub-blocker test pattern (see template).

## Workflow

### 1. Validate input

Confirm `$ARGUMENTS` is:
- An absolute Windows path with backslashes.
- A file matching `C:\CODE\SysML2.NET\SysML2.NET\Extend\*Extensions.cs`.
- The file exists.

If validation fails, stop and ask the user to re-invoke with a corrected path.

### 2. Auto-derive paths

From `$ARGUMENTS = C:\CODE\SysML2.NET\SysML2.NET\Extend\<FOO>Extensions.cs`:

- **Production file**: `$ARGUMENTS` itself.
- **Test fixture**: `C:\CODE\SysML2.NET\SysML2.NET.Tests\Extend\<FOO>ExtensionsTestFixture.cs`.
  If it does not exist, surface that to the user — likely scope mismatch.
- **Reference production file**: `C:\CODE\SysML2.NET\SysML2.NET\Extend\NamespaceExtensions.cs`.
- **Reference test file**: `C:\CODE\SysML2.NET\SysML2.NET.Tests\Extend\NamespaceExtensionsTestFixture.cs`.
- **Target interface**: `I<FOO>` — find via Glob `SysML2.NET\Core\AutoGenPoco\**\I<FOO>.cs`.
- **Target metaclass name**: `<FOO>`.
- **Subject param name**: lowercase first char of `<FOO>` + `<FOO>[1..]` + `Subject` (e.g. `Type` → `typeSubject`, `Feature` → `featureSubject`).
- **Notes file**: `C:\CODE\SysML2.NET\.team-notes\<foo>-extensions-spec.md` (kebab-case `<foo>`). Create the `.team-notes\` directory if it doesn't exist (`mkdir -p`).
- **Team name**: `<foo>-extensions-impl`.
- **GitHub issue number**: discover via
  ```bash
  gh issue list --repo STARIONGROUP/SysML2.NET --state all \
      --search "SysML2.NET/Extend/<FOO>Extensions.cs in:body" \
      --json number,title,state --limit 5
  ```
  Pick the single match. If 0 results or >1 results, surface to the user and
  ask for an explicit issue number before proceeding (do not guess).
  Search-by-body is preferred over search-by-title because the body always
  contains the canonical source-path string and is therefore unambiguous.
- **Issue URL**: `https://github.com/STARIONGROUP/SysML2.NET/issues/<num>`.

Print the derived paths (including the issue URL) back to the user as a sanity check.

### 3. Enumerate stub methods

Grep the production file for `throw new NotSupportedException`. List the enclosing
methods. These are the stubs to implement.

If the count is 0, stop — the file has no stubs left.

### 3.5. Grade complexity, pick models

For each stub method, look at its `<remarks>` OCL block (or note its absence) and
tally these signals:

- **trivial signals**
  - No OCL block — spec-text-only redefinition with sibling precedent (e.g.
    `FeatureMembership::ownedMemberFeature` mirrors
    `OwningMembershipExtensions.ComputeOwnedMemberElement`)
  - OCL is a single `OfType<T>` / `selectByKind` filter on `OwnedRelationship`
    or `ownedMember`
- **standard signals**
  - OCL has `->select` / `->reject` / scalar chain navigation
  - OCL has `->union` of single-step paths
  - OCL has a single `oclAsType` cast or a single `oclIsKindOf` test
- **complex signals**
  - OCL has `->closure(...)` (cycle protection needed: BFS/DFS with `visited` set)
  - OCL has nested `let` / `if-then-else` / multiple `oclAsType`
  - OCL has multi-step `->union` (e.g.
    `ownedMembership.OfType<X>().Union(otherChain.OfType<Y>())`)
  - Cross-interface recursion (e.g. `Supertypes(false)`, recursive
    `ImportedMemberships(excluded)`)
- **bump-up signal**
  - Total method count > 15 promotes the whole task one tier
  - Even one complex signal anywhere → task is complex

Grade the task overall as **trivial / standard / complex** using the worst signal
observed. Then pick a model per role from this default table:

| Role | Drives the choice | trivial | standard | complex |
|---|---|---|---|---|
| Researcher | OCL density × method count | Haiku | Sonnet | Opus |
| Implementer | OCL operator complexity | Sonnet | Sonnet | Opus |
| Tester (targeted fixture) | populated-case fixture wiring complexity | Sonnet | Sonnet | Opus |
| Tester (regression sweep, if any) | OCL semantics needed to assert real behavior in sibling fixtures | Sonnet | Sonnet | Opus |
| Reviewer | diff size × OCL density | Sonnet | Sonnet | Opus |

Per-role asymmetry is encouraged. Examples:
- Trivial impl + a regression sweep that touches 8 sibling tests asserting
  moderate OCL → Sonnet implementer + Sonnet regression-sweep tester (still
  Sonnet because the OCL is moderate, not complex).
- Standard impl with a single `->closure` method buried in the list → bump
  the implementer to Opus only, keep the rest at Sonnet.
- Trivial 2-method spec-text-only file → Haiku researcher, Sonnet for the
  rest.

Record the per-role selection. It will be presented to the user in step 4 and
applied at every `Agent(...)` spawn in steps 5–9.

### 4. Sanity check with the user

Use `AskUserQuestion` to present:
- The auto-derived paths (test fixture, interface, reference template, notes file).
- The list of stub methods (or a count if there are many).
- The complexity grade and the per-role model selection from step 3.5.
- Two questions:
  1. Scope: "Implement all" or "Implement a subset" (let the user paste a method
     list as a custom answer).
  2. Models: "Use the dynamic per-role selection above" or override with
     "All Opus" / "All Sonnet" / "Custom" (let the user paste a per-role
     mapping).

If they pick subset, narrow the method list. If they override the model
selection, apply that override at every `Agent(...)` spawn below. Otherwise
proceed with the dynamic defaults.

### 5. Spawn the researcher (FIRST role — produces the notes file the others read)

Read the v2 team template at `C:\Users\atheate\.claude\team-templates\extension-impl.md`
to refresh the role prompts. Substitute the placeholders from step 2 + the method
list from step 4.

Spawn the **researcher** as
`Agent({subagent_type: "general-purpose", model: <researcher_model>})`
with the v2 researcher prompt, where `<researcher_model>` is the model picked
in step 3.5 (Haiku for trivial, Sonnet for standard, Opus for complex — or the
user's step-4 override). Foreground (not `run_in_background`) — the next roles
depend on the notes file.

The researcher MUST:
- Treat the OCL `<defaultValue>`/`<ownedRule>` body in the XMI as the canonical
  source of truth.
- Fall back to the OCL block in the production file's `<remarks>` (mirrored from
  XMI by codegen).
- For methods with NO OCL body (e.g. `Type::isConjugated`), record a short prose
  derivation rule + spec citation, and EXPLICITLY FLAG the spec-text-only origin
  in the notes.
- Flag any method whose OCL transitively reads a still-stubbed sibling
  `Compute*` so the tester knows to use the stub-blocker pattern.

After the researcher finishes, read `{{NOTES_FILE}}` yourself to verify it
covers all methods + flags spec-text-only and stub-blocker cases.

### 6. Spawn the implementer and tester in parallel

**Spawn both roles in a single orchestrator message** containing TWO `Agent(...)`
tool calls — one for the implementer, one for the targeted-fixture tester. Both
foreground (do not set `run_in_background`). Parallel `Agent` tool calls in the
same assistant message execute concurrently; each agent runs in its own
isolated context. The only thing they share is the researcher's notes file on
disk.

Spawn 1 — **implementer**:
`Agent({subagent_type: "general-purpose", model: <implementer_model>})` with the
v2 implementer prompt, where `<implementer_model>` is the model picked in
step 3.5 (Sonnet for trivial/standard, Opus for complex — or the user's step-4
override). The prompt MUST instruct the implementer to read `{{NOTES_FILE}}`
first.

Spawn 2 — **tester (targeted fixture)**:
`Agent({subagent_type: "general-purpose", model: <tester_model>})` with the v2
tester prompt, where `<tester_model>` is the **targeted-fixture** tester model
picked in step 3.5 (Sonnet for trivial/standard, Opus for complex — or the
user's step-4 override). The prompt MUST instruct the tester to read
`{{NOTES_FILE}}` first (each method has a "Test plan" section there).

**Parallel-mode caveat for the tester**: when spawned in parallel with the
implementer, the tester runs ONLY `dotnet build` on the test project (confirms
the fixture compiles against the pre-existing interfaces in `Core/AutoGenPoco/`).
It MUST NOT run `dotnet test` — production does not yet contain the
implementer's parallel-turn edits, so every populated-case test would fail with
`NotSupportedException` (useless signal). The orchestrator runs targeted
`dotnet test` in step 7. State this explicitly in the tester's spawn prompt.

### 7. Orchestrator verification (post-parallel)

After both step-6 agents return, run sequentially in the orchestrator's own
turn:

1. Build production:
   ```bash
   dotnet build C:\CODE\SysML2.NET\SysML2.NET\SysML2.NET.csproj --nologo --verbosity quiet
   ```
   On failure, dispatch the implementer back to fix its own bugs (do not
   delegate to a fresh agent unless the original is non-responsive).

2. Run targeted fixture:
   ```bash
   dotnet test C:\CODE\SysML2.NET\SysML2.NET.Tests\SysML2.NET.Tests.csproj --filter "FullyQualifiedName~<FOO>ExtensionsTestFixture" --nologo --verbosity quiet
   ```
   Analyze each failure and route the fix:
   - **OCL mistranslation in production** → re-dispatch the implementer.
   - **Wrong test assertion** (e.g. assertion built against the original
     contract that the implementer's deviation report invalidated) →
     re-dispatch the tester.

   Iterate until the targeted fixture has 0 failures.

### 8. Regression sweep (mandatory)

Run the full solution test suite:

```bash
dotnet test C:\CODE\SysML2.NET\SysML2.NET.sln --no-build --nologo --verbosity quiet
```

If failures exist, identify those of the form:

> Expected: `<NotSupportedException>` But was: `no exception thrown`

These are pre-existing tests in sibling fixtures that asserted the stubs throw —
they now fail because our new implementations make those paths succeed. Dispatch
the tester back (via `SendMessage` to the still-running tester if available, else
a fresh `Agent` call with `model: <regression_sweep_tester_model>` from
step 3.5) with the failing-test list and instructions to update those
assertions to assert real behavior. The regression sweep is in-scope per the
template.

**Critical**: do NOT brief the tester as "replace the stale `Throws` assertion".
Brief it as "**expand each touched test to cover every distinct branch implied
by the production OCL**". This means, for each touched sibling test:
- **Filter discrimination** — for every `OfType<X>()` / `selectByKind(X)`,
  add a sibling element of a non-X kind to the fixture and assert it is
  excluded.
- **Predicate completeness** — for every `Where(...)` predicate composed of
  `or` / `and` / equality clauses, add fixtures that exercise each clause both
  true and false (e.g. for `direction = In or Inout`, add an `In` feature, an
  `Inout` feature, an `Out` feature, and an undirected feature; assert the
  first two are included and the last two excluded).
- **Owned vs. inherited** — when the OCL unions an owned collection with an
  inherited one (`X.union(inheritedMembership.selectByKind(...))`), wire a
  Specialization in the fixture and confirm the inherited branch surfaces.
  When the OCL is inheritance-only (`inheritedMemberships.selectByKind(...)`),
  also wire a sibling owned member and confirm it does NOT surface.
- **Null-projection guard** — when the LINQ chain ends with
  `.Where(x => x != null)` (defending against a Select that may yield null),
  construct a case where the projection yields null and assert it is filtered
  out.

A "single happy-path positive case + null + empty" pattern is **insufficient**
for the regression sweep — it leaves filter, predicate, and inheritance branches
untested. The original stub-blocker test only asserted one positive case because
that's all that *could* be asserted while the upstream was stubbed; once the
stub is gone, the full OCL surface is in scope.

**Parallel-spawn opportunity**: if step 7's verification surfaced targeted-fixture
test-assertion fixes that were deferred to this step (i.e. there is BOTH (a)
work for the targeted-fixture tester re-dispatch on `{{TEST_FILE}}` AND (b)
work for the regression-sweep tester on sibling `*ExtensionsTestFixture.cs`
files), spawn the two roles in a single orchestrator message with TWO
`Agent(...)` tool calls, both foreground. Use the targeted-fixture
`<tester_model>` for (a) and the `<regression_sweep_tester_model>` for (b)
(both from step 3.5, or the user's step-4 override). They edit disjoint files
so this is safe. If only one of (a) or (b) has work, spawn only that one.

Iterate until 100% green or the user opts out.

### 9. Spawn the reviewer (LAST role — verdict only)

`Agent({subagent_type: "general-purpose", model: <reviewer_model>})` with the v2
reviewer prompt, where `<reviewer_model>` is the model picked in step 3.5
(Sonnet for trivial/standard, Opus for complex — or the user's step-4
override). Foreground. The reviewer cross-checks `{{PRODUCTION_FILE}}` and
`{{TEST_FILE}}` against `{{NOTES_FILE}}` and produces an "OK / NEEDS FIX"
verdict.

If the verdict is "NEEDS FIX", dispatch the implementer or tester back to
action the findings (the reviewer never edits).

### 10. Final summary

Report to the user:
- Modified files (production + test fixture + notes + any regression-sweep test fixtures).
- Test counts (X/Y green for the targeted fixture; A/B green for the full solution).
- Reviewer verdict + any unresolved findings.
- Out-of-scope blockers surfaced (e.g. "5 populated cases use the stub-blocker
  pattern because `<UpstreamMethod>` in `<UpstreamFile>.cs` is still a stub —
  consider a follow-up issue").
- Spec-text-only methods (e.g. `IsConjugated`) — flag separately so the user
  knows the implementation is grounded in spec prose rather than OCL.
- **Issue checklist sync**: `<issue-url>` — `<newly-ticked>` newly ticked,
  `<newly-added>` newly added, `<ticked>/<total>` total (filled in after step 11).

Do NOT auto-commit. The user reviews and commits.

### 11. Sync GitHub issue checklist

Runs **unconditionally** after step 10, even on reviewer NEEDS FIX. Rationale:
the issue should always reflect the current implementation state of the file;
unresolved findings are separately surfaced in the final-summary report. The
`gh issue edit` push must touch ONLY the `### Checklist` section.

1. **Fetch current issue body**:
   ```bash
   gh issue view <issue-number> --repo STARIONGROUP/SysML2.NET --json body -q .body > <tmp-old-body>
   ```
   Read the file with the Read tool. Locate the `### Checklist` section
   (everything between that header and the next `### ` header or EOF).

2. **Enumerate implementation status from the production file**. For each
   `Compute*` method declared in `{{PRODUCTION_FILE}}`:
   - Extract the full extension-method signature in the same format the
     existing checklist uses (return type + method name + `(this <Iface>[, …])`),
     e.g. `List<IClassifier> ComputeDefinition(this IUsage)`.
   - Mark **implemented** when BOTH of the following hold:
     - The method body does not contain `throw new NotSupportedException`.
     - The targeted-fixture test for it (`Verify{MethodName}` in
       `{{TEST_FILE}}`) passed in step 7's last `dotnet test` run. Use the
       output already captured; do NOT re-run tests.

3. **Compute the new Checklist section**:
   - For each existing checklist item: tick it (`- [x]`) if the corresponding
     method is implemented per (2); otherwise leave its current state.
   - For each `Compute*` method in `{{PRODUCTION_FILE}}` whose signature is
     not present in the checklist (use exact-string match on the back-tick
     content), append it as a new line. Tick it iff implemented.
   - Preserve the relative order of pre-existing items. Append new items
     after the last existing item, in declaration order from the production
     file.
   - Do NOT remove any existing item, even if its signature no longer matches
     a method in the production file (signature drift is the user's call).

4. **Stitch the new body**: replace ONLY the lines under `### Checklist`
   (up to the next `### ` header or EOF) with the recomputed block.
   Everything else — `### Prerequisites`, `### Description`,
   `### System Configuration`, blank lines, trailing whitespace — is
   preserved verbatim. Use a small string-slice (not a regex rewrite of the
   whole body) to minimize the diff.

5. **Push the update**:
   ```bash
   gh issue edit <issue-number> --repo STARIONGROUP/SysML2.NET --body-file <tmp-new-body>
   ```
   Use `--body-file` (not `--body "..."`) so multi-line content survives the
   shell unchanged.

6. **Verify**: re-fetch with `gh issue view` and diff against the version you
   pushed. If the diff is non-empty outside the Checklist section, abort and
   surface to the user — do NOT push a second time.

7. **Report** back into the step-10 final-summary line: issue URL, count of
   newly ticked items, count of newly added items, and the resulting
   `<ticked>/<total>` ratio.

## Notes for the orchestrator (you, the main agent)

- Pick the model per role using the complexity-grading rubric in step 3.5.
  Default tiers are Haiku (researcher only, trivial task), Sonnet (most cases),
  Opus (only when OCL has `->closure` / multi-step `->union` / cross-interface
  recursion, or method count > 15). The user can override "all Opus" /
  "all Sonnet" / "Custom" at the step-4 sanity check. Per-role asymmetry is
  encouraged (e.g. trivial impl + Opus regression-sweep tester).
- Spawn each role **foreground** (not `run_in_background`). The implementer and
  tester in step 6 are spawned in parallel by issuing TWO `Agent(...)` tool
  calls in a single orchestrator message — both still foreground, just
  concurrent. The same single-message-two-Agent-calls pattern applies to the
  Level-2 parallel spawn in step 8 (targeted-fixture re-dispatch ∥
  regression-sweep tester) when both have work. All other roles run
  sequentially because they depend on the previous step's output.
- The researcher runs **FIRST and is mandatory** — even when the production file's
  `<remarks>` already carries OCL, the researcher's notes file gives the
  implementer/tester/reviewer a single shared contract document, AND it's the only
  role that handles spec-text-only methods cleanly.
- The reviewer is **mandatory** even when in past runs it caught no bugs the
  tester missed — the user explicitly wants it as cheap insurance against subtle
  OCL mistranslation.
- If a build error involves an explicit-interface-impl loop (e.g. `(INamespace)x`
  cast not bypassing virtual dispatch), call the static extension method directly
  rather than via interface dispatch — pattern from the TypeExtensions task fix.
- The step-11 GitHub-issue sync runs **unconditionally** after step 10, even on
  reviewer NEEDS FIX. Rationale: the issue should always reflect the current
  implementation state of the file; unresolved findings are separately surfaced
  in the final-summary report. The `gh issue edit` push must touch ONLY the
  `### Checklist` section — verify with a re-fetch + diff before reporting "done".
