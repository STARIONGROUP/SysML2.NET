---
description: Spawn researcher/implementer/tester/reviewer team to fill in stub Compute* methods in a SysML2.NET Extend file
argument-hint: <absolute-path-to-Extensions-file.cs>
---

# /implement-extensions

Spawn the 4-role agent team (researcher → implementer → tester → reviewer, all
Opus) for **$ARGUMENTS** — a `*Extensions.cs` file under `SysML2.NET/Extend/`
whose `Compute*` methods are still stubs throwing `NotSupportedException`.

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

Print the derived paths back to the user as a sanity check.

### 3. Enumerate stub methods

Grep the production file for `throw new NotSupportedException`. List the enclosing
methods. These are the stubs to implement.

If the count is 0, stop — the file has no stubs left.

### 4. Sanity check with the user

Use `AskUserQuestion` to present:
- The auto-derived paths (test fixture, interface, reference template, notes file).
- The list of stub methods (or a count if there are many).
- Two options: "Implement all" or "Implement a subset" (let the user paste a method
  list as a custom answer).

If they pick subset, narrow the method list. Otherwise proceed with all.

### 5. Spawn the researcher (FIRST role — produces the notes file the others read)

Read the v2 team template at `C:\Users\atheate\.claude\team-templates\extension-impl.md`
to refresh the role prompts. Substitute the placeholders from step 2 + the method
list from step 4.

Spawn the **researcher** as `Agent({subagent_type: "general-purpose", model: "opus"})`
with the v2 researcher prompt. Foreground (not `run_in_background`) — the next
roles depend on the notes file.

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
`Agent({subagent_type: "general-purpose", model: "opus"})` with the v2
implementer prompt. The prompt MUST instruct the implementer to read
`{{NOTES_FILE}}` first.

Spawn 2 — **tester**:
`Agent({subagent_type: "general-purpose", model: "opus"})` with the v2 tester
prompt. The prompt MUST instruct the tester to read `{{NOTES_FILE}}` first
(each method has a "Test plan" section there).

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
a fresh `Agent` call) with the failing-test list and instructions to update those
assertions to assert real behavior. The regression sweep is in-scope per the
template.

**Parallel-spawn opportunity**: if step 7's verification surfaced targeted-fixture
test-assertion fixes that were deferred to this step (i.e. there is BOTH (a)
work for the targeted-fixture tester re-dispatch on `{{TEST_FILE}}` AND (b)
work for the regression-sweep tester on sibling `*ExtensionsTestFixture.cs`
files), spawn the two roles in a single orchestrator message with TWO
`Agent(...)` tool calls, both foreground. They edit disjoint files so this is
safe. If only one of (a) or (b) has work, spawn only that one.

Iterate until 100% green or the user opts out.

### 9. Spawn the reviewer (LAST role — verdict only)

`Agent({subagent_type: "general-purpose", model: "opus"})` with the v2 reviewer
prompt. Foreground. The reviewer cross-checks `{{PRODUCTION_FILE}}` and
`{{TEST_FILE}}` against `{{NOTES_FILE}}` and produces an "OK / NEEDS FIX" verdict.

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

Do NOT auto-commit. The user reviews and commits.

## Notes for the orchestrator (you, the main agent)

- Use the **Opus** model for all four roles — they handle OCL→C# translation
  best and the user has explicitly preferred Opus for this workflow.
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
