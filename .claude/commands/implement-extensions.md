---
description: Spawn researcher/implementer/tester/reviewer team to fill in stub Compute* methods in a SysML2.NET Extend file
argument-hint: <path-to-Extensions-file.cs (relative to repo root or absolute)>
---

# /implement-extensions

Spawn the 4-role agent team (researcher → implementer → tester → reviewer) for
**$ARGUMENTS** — a `*Extensions.cs` file under `SysML2.NET/Extend/` whose
`Compute*` methods are still stubs throwing `NotSupportedException`. Per-role
models are picked dynamically based on stub complexity (see step 3.5).

The team template is at `.claude/team-templates/extension-impl.md` (v2, repo-tracked).
Read it first — its role prompts are the source of truth; this command body is the
orchestration glue.

## Path conventions (portable across contributors)

All paths in this command are **repo-relative with forward slashes**, so the workflow
works for any contributor regardless of where they cloned the repo (e.g.
`C:\code\SysML2.NET`, `~/projects/SysML2.NET`, `D:\dev\SysML2.NET`). Forward slashes
are accepted by both `dotnet` CLI and `bash` (incl. Git Bash on Windows, WSL,
macOS, Linux).

When invoking tools that REQUIRE absolute paths (e.g. `Read`, `Edit`, `Write`), the
orchestrator resolves repo-relative paths at runtime by prepending the working
directory (the repo root, where `/implement-extensions` was invoked).

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

## Sub-agent spawn mode (applies to EVERY `Agent(...)` call below)

Every `Agent(...)` call in this command MUST pass `mode: "acceptEdits"`
explicitly:

```
Agent({
    subagent_type: "general-purpose",
    model: "<haiku|sonnet|opus>",
    mode: "acceptEdits",   // MANDATORY — see rationale below
    prompt: "<role prompt>"
})
```

**Rationale**: sub-agents inherit the parent orchestrator's permission mode
at spawn time. If the parent is in (or has been in) plan mode, the sub-agent
inherits plan-mode enforcement, and plan mode's built-in pre-tool-use hook
blocks `Skill(...)` calls. This makes the Hypha researcher's grounding path
(`Skill(hypha:metamodel-lookup)`, `Skill(hypha:spec-citation)`) completely
unreachable from the sub-agent, and the researcher also cannot `Write` its
notes file. `mode: "acceptEdits"` overrides the inheritance so the sub-agent
can invoke both Skills and Writes as its role requires. Discovered during a
dry-run against `SysML2.NET/Extend/StepExtensions.cs` (2026-07-01).

Applies to: legacy researcher (5a), Hypha researcher (5b), comparator (5.4),
implementer (6), targeted-fixture tester (6), regression-sweep tester (8),
reviewer (9). If any spawn omits `mode: "acceptEdits"`, the run may silently
degrade to the deferred-write workaround (see the team template's
"Plan-mode-aware prompting" section), which is much slower and requires the
orchestrator to apply on-behalf writes.

## Pre-flight: Hypha plugin detection (runs before Gate 0)

The Hypha plugin is a WIP grounding source for SysML v2 / KerML lookups
(`hypha:metamodel-lookup` for structure, `hypha:spec-citation` for intent).
This command uses it to A/B-validate a Hypha-grounded researcher against the
legacy XMI-grounded researcher on every invocation where the plugin is
installed. See `.claude/team-templates/extension-impl.md` → "How Hypha
comparison plugs in" for the design.

At the very start of the invocation, BEFORE Gate 0 or any other step:

1. **Detect availability**. Record `hypha_available: bool` on the
   orchestrator's per-run state. The signal: is the `hypha:metamodel-lookup`
   skill / `hypha:metamodel-navigator` agent listed in the current session's
   toolset (the `<system-reminder>` block at session start enumerates
   available skills and agents; the deferred-tool list plus that reminder
   are the authoritative check).

2. **When `hypha_available == true`** — no user interaction needed. The
   Hypha researcher will run in parallel with the legacy researcher at
   step 5, followed by the comparator at step 5.4, followed by Gate R-C.

3. **When `hypha_available == false`** — surface a one-line recommendation
   to the user (once per invocation, in the same orchestrator turn as the
   Gate 0 plan / step 4 sanity check, whichever comes first):
   > The Hypha plugin isn't installed in this session. It grounds
   > SysML v2 / KerML lookups in the metamodel + normative spec text and is
   > this command's A/B counterpart to the legacy XMI researcher.
   > Installing it is optional — the legacy-only flow works fine — but the
   > per-run comparison corpus won't grow while it's off.

   Do NOT block the run. Do NOT loop the recommendation. If the user
   ignores it, silently accept refusal and continue.

4. **On refusal (or `hypha_available == false` after the recommendation)**:
   the run proceeds with the legacy-only flow — steps 5, 5.5, 6, ..., 11 as
   written. Skip steps 5b, 5.4, and Gate R-C entirely. The single-file
   downstream (implementer, tester, reviewer) reads `{{NOTES_FILE}}` in the
   normal way.

5. **Mid-run downgrade**: if a Hypha tool call errors between steps 5 and
   5.4 (plugin removed / knowledge base not built / auth failure), stop
   dispatching Hypha work. Skip the comparator + Gate R-C. Proceed with the
   legacy researcher's notes as `active_notes_file`. Surface the downgrade
   explicitly in step 10's final summary so the user knows the run didn't
   exercise Hypha.

The rest of this command's pre-flight (plan mode Gate 0) is unaffected.

## Pre-flight: plan mode IS the pre-execution approval gate (Gate 0)

If the orchestrator session is in **plan mode** when `/implement-extensions` is invoked, use plan mode as the natural pre-execution approval gate. Do NOT spawn any sub-agent. Instead:

1. **Stay in plan mode.** Do all the READ-ONLY pre-flight work that steps 1 → 3.5 of this command require:
   - Validate the input file exists (per step 1's normalization rule); refuse if missing.
   - Auto-derive the per-file paths (per step 2): production, test fixture, target interface, subject param, notes file, team name.
   - Look up the GitHub issue via `gh issue list --search "<path> in:body"`. Surface ambiguity (0 or >1 match) via `AskUserQuestion`.
   - Enumerate stub `Compute*` methods (per step 3); abort if 0 stubs.
   - Grade complexity, pick per-role models (per step 3.5).
   - Read `git status --porcelain` (read-only; refuse on dirty tree if you intend the run to be clean).

2. **Write the plan file** (at the path Claude Code provides in the plan-mode system reminder) with the standard structure (Context, Recommended approach, Critical files, Verification). The "Recommended approach" section is a concise description of the proposed execution:
   - The one production file + its derived test fixture + notes file + GitHub issue number.
   - Per-role model picks (from step 3.5). Note that `ExitPlanMode` is binary (approve / reject), so the user cannot override models from inside the plan-approval UI. Two override paths are supported: (a) reject the plan and re-invoke with an explicit model preference in the prompt, OR (b) accept the plan and the orchestrator fires a follow-up `AskUserQuestion` BEFORE step 5's researcher spawn.
   - Stub method count + complexity grade.
   - Workflow shape: step 5 (researcher spawn — legacy + Hypha in parallel when `hypha_available`, else legacy only) → step 5.4 (comparator, only when `hypha_available`) → new Gate R-C (only when `hypha_available`; user picks Hypha vs. legacy notes) → step 5.5 (Gate R-A approval on the picked notes file) → step 6 (implementer + tester) → step 7 (verification) → step 8 (regression sweep) → step 9 (reviewer) → step 10 (final summary) → step 11 (issue checklist sync).
   - `hypha_available` value from the pre-flight above, so the user sees the workflow shape they'll actually get.
   - Explicit mention of the structural checkpoints after this one: **when `hypha_available`**, Gate R-C fires after the comparator returns (user picks Hypha vs. legacy notes), then Gate R-A fires after the picked notes are inline-previewed. **When Hypha is off**, only Gate R-A fires (one more checkpoint after this one).

3. **Call `ExitPlanMode`.** The user reviews the plan in the standard plan-approval UI:
   - **Approve (unchanged)** → plan mode exits, orchestrator proceeds (model-override `AskUserQuestion` per step 2 above, then step 5 → 5.5 → 6 → 7 → 8 → 9 → 10 → 11).
   - **Approve (edited)** → the tool result includes a `## Approved Plan (edited by user):` block with the user's modifications. The orchestrator MUST treat the EDITED plan content as the source of truth — re-parse the (single) file path + model picks from it, NOT from the originally-proposed plan. (Standard Claude Code plan-edit-on-approval pattern; see the `<system-reminder>` after every `ExitPlanMode` approval.)
   - **Reject** → nothing executes. The plan file remains for the user's reference.

If the orchestrator session is **NOT in plan mode** when the command is invoked, continue with the existing step 4 sanity-check (`AskUserQuestion` for scope + models). Gate 0 only fires in plan mode; otherwise the existing step 4 plays the same role.

**Important — tool-level prompts are NOT auto-allowed.** Gate 0 (and Gate R-A in step 5.5) govern STRUCTURAL approval only. Individual tool calls — `Bash(dotnet build *)`, `Bash(git push *)`, `Agent(...)`, etc. — continue to surface per the user's `settings.json` and harness defaults. The user has chosen to keep handling those prompts manually; the gates do not bypass them.

## Workflow

### 1. Validate input

Accept `$ARGUMENTS` in any of these forms:
- Repo-relative path: `SysML2.NET/Extend/<FOO>Extensions.cs`
- Repo-relative with `@` prefix (Claude Code file-reference): `@SysML2.NET/Extend/<FOO>Extensions.cs`
- Absolute path on the contributor's machine (Windows or POSIX), with `@` prefix or without

**Normalization rule**: extract the `<FOO>Extensions.cs` filename from the input and
reconstruct the canonical repo-relative path `SysML2.NET/Extend/<FOO>Extensions.cs`.
Then verify this file exists from the repo root (the working directory). If it
doesn't, stop and surface to the user.

### 2. Auto-derive paths

From the normalized canonical path `SysML2.NET/Extend/<FOO>Extensions.cs`:

- **Production file**: `SysML2.NET/Extend/<FOO>Extensions.cs`.
- **Test fixture**: `SysML2.NET.Tests/Extend/<FOO>ExtensionsTestFixture.cs`.
  If it does not exist, surface that to the user — likely scope mismatch.
- **Reference production file**: `SysML2.NET/Extend/NamespaceExtensions.cs`.
- **Reference test file**: `SysML2.NET.Tests/Extend/NamespaceExtensionsTestFixture.cs`.
- **Target interface**: `I<FOO>` — find via Glob `SysML2.NET/Core/AutoGenPoco/**/I<FOO>.cs`.
- **Target metaclass name**: `<FOO>`.
- **Subject param name**: lowercase first char of `<FOO>` + `<FOO>[1..]` + `Subject` (e.g. `Type` → `typeSubject`, `Feature` → `featureSubject`).
- **Notes file (legacy)**: `.team-notes/<foo>-extensions-spec.md` (kebab-case
  `<foo>`). The `.team-notes/` directory is gitignored at `.gitignore` line
  `/.team-notes/*`, so all three per-run files are per-contributor scratch.
  Create the directory if it doesn't exist (`mkdir -p .team-notes`).
- **Notes file (Hypha)** — only when `hypha_available == true`:
  `.team-notes/<foo>-extensions-spec-hypha.md`.
- **Comparison file** — only when `hypha_available == true`:
  `.team-notes/<foo>-extensions-comparison.md`.
- **Active notes file** — computed at Gate R-C (or defaulted to the legacy
  notes file when Hypha is off). Held on orchestrator state as
  `active_notes_file` and substituted for `{{ACTIVE_NOTES_FILE}}` in the
  implementer / tester / reviewer prompts.
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
- The auto-derived paths (test fixture, interface, reference template, notes
  files: legacy `{{NOTES_FILE}}`, and — when `hypha_available` — Hypha
  `{{HYPHA_NOTES_FILE}}` and comparison `{{COMPARISON_FILE}}`).
- `hypha_available` (`true` / `false`) and the resulting workflow shape
  (either legacy+Hypha+comparator+Gate R-C or legacy-only). When
  `hypha_available == false` and this session has not been recommended the
  plugin yet, include the one-line install recommendation from the pre-flight
  in the sanity-check text — do NOT add it as a question option (it's
  informational; the user acts on it out-of-band).
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

### 5. Spawn the researcher(s) (FIRST role — produces the notes file(s) the others read)

Read the v2 team template at `.claude/team-templates/extension-impl.md`
to refresh the role prompts. Substitute the placeholders from step 2 + the method
list from step 4.

Split into 5a + 5b based on `hypha_available` from the pre-flight:

#### 5a. Legacy researcher (ALWAYS run)

Spawn the **legacy researcher** as
`Agent({subagent_type: "general-purpose", model: <researcher_model>})`
with the v2 legacy researcher prompt, where `<researcher_model>` is the model
picked in step 3.5 (Haiku for trivial, Sonnet for standard, Opus for complex
— or the user's step-4 override). Foreground.

The legacy researcher MUST:
- Treat the OCL `<defaultValue>`/`<ownedRule>` body in the XMI as the canonical
  source of truth.
- Fall back to the OCL block in the production file's `<remarks>` (mirrored from
  XMI by codegen).
- For methods with NO OCL body (e.g. `Type::isConjugated`), record a short prose
  derivation rule + spec citation to `Resources/specification/*.pdf.txt`, and
  EXPLICITLY FLAG the spec-text-only origin in the notes.
- Flag any method whose OCL transitively reads a still-stubbed sibling
  `Compute*` so the tester knows to use the stub-blocker pattern.

#### 5b. Hypha researcher (ONLY when `hypha_available == true`)

**Spawn 5a and 5b in the same orchestrator message** — one message, two
`Agent(...)` calls, both foreground. They run in parallel with completely
independent contexts: the legacy researcher never sees Hypha's output and
the Hypha researcher never sees `{{NOTES_FILE}}`. This is the whole point
of the A/B setup.

Spawn the **Hypha researcher** as
`Agent({subagent_type: "general-purpose", model: <researcher_model>})` with
the v2 `hypha-researcher` prompt (same model tier as legacy — OCL complexity
is the same input for both). The prompt MUST enforce:
- Only Hypha skills / agents (`hypha:metamodel-lookup`, `hypha:spec-citation`,
  `hypha:metamodel-navigator`) as grounding sources.
- **Forbidden reads**: `Resources/*.uml` and `Resources/specification/*.pdf.txt`
  — those are the legacy researcher's grounding sources. Cross-contamination
  defeats the comparison.
- Same output section layout as the legacy researcher so the comparator can
  align entries structurally.
- Notes-file target: `{{HYPHA_NOTES_FILE}}` (never `{{NOTES_FILE}}`).

#### 5c. Post-researcher coverage check

After both agents return (or just 5a in a legacy-only run), read each notes
file yourself to verify coverage. Re-dispatch the individual researcher whose
file is empty / missing sections / didn't flag spec-text-only cases where
applicable. Do NOT proceed until both files (or the legacy file, in a
legacy-only run) are complete.

If `hypha_available == false`: skip 5b and 5c-Hypha, proceed directly to
step 5.5 (Gate R-A) with `active_notes_file = {{NOTES_FILE}}`. Otherwise
proceed to step 5.4 (comparator) before Gate R-C.

### 5.4. Spawn the comparator (ONLY when `hypha_available == true`)

Single `Agent({subagent_type: "general-purpose", model: <researcher_model>})`
call for the `comparator` role (same model tier as the researchers —
comprehension needs are similar). Foreground.

The comparator MUST:
- Read ONLY `{{NOTES_FILE}}` and `{{HYPHA_NOTES_FILE}}`. No other file is
  necessary.
- Emit `agree` / `disagree` / `legacy-only` / `hypha-only` per method + an
  overall verdict (`hypha stronger` / `legacy stronger` / `equivalent` /
  `mixed`) backed by counts.
- Write the report to `{{COMPARISON_FILE}}` (the ONLY file it may Write) and
  send a compact summary back to the orchestrator with summary
  `comparison ready`.
- NOT recommend a winner — the user picks at Gate R-C.

After it returns, read `{{COMPARISON_FILE}}` yourself to build the inline
preview for Gate R-C.

### 5.45. Gate R-C — Researcher-Comparison gate (ONLY when `hypha_available == true`)

Before Gate R-A, present the comparator's report inline and let the user pick
which researcher's notes drive the downstream implementer / tester / reviewer.

1. **Render** an inline preview from `{{COMPARISON_FILE}}` in the chat response:
   - Overall verdict + counts (`agree: N, disagree: N, legacy-only: N,
     hypha-only: N`).
   - Per-method verdict table (compact — one row per method, `agree`
     collapsed to a single count line at the top if the majority agree).
   - Top 3 disagreements by materiality with one-line concerns.
   - Paths to `{{NOTES_FILE}}`, `{{HYPHA_NOTES_FILE}}`, `{{COMPARISON_FILE}}`
     so the user can dive in.
   - Cap the preview at ~80 lines.

2. **Ask** via `AskUserQuestion` (single question, 3 options):
   - **Use Hypha notes** — Hypha drives implementer/tester/reviewer.
     Sets `active_notes_file = {{HYPHA_NOTES_FILE}}`.
   - **Use legacy notes** — legacy drives.
     Sets `active_notes_file = {{NOTES_FILE}}`.
   - **Abort — re-research with feedback** — the free-form `Other` text is
     forwarded to whichever researcher(s) are still addressable via
     `SendMessage`. After they return fresh `spec ready` /
     `hypha spec ready` payloads, re-run step 5.4 (comparator) and this gate.

3. **On pick**, record `active_notes_file` on orchestrator state and proceed
   to Gate R-A (step 5.5). The comparator report is preserved on disk
   regardless of the pick — it feeds the accumulating A/B corpus.

4. **On abort without re-research** (user rejects both notes files and does
   not request re-research), stop the orchestration. All three
   `.team-notes/` files remain for the user's reference.

Rationale: Gate R-C exists ONLY in Hypha-available runs. It is the user's
one and only choice-point between the two grounding paths — after this, the
downstream flow is grounded solely in the picked notes file. The comparator
report itself is a durable artifact; Gate R-C's decision does not delete
either notes file.

### 5.5. Phase R-A — Researcher-plan approval gate (MANDATORY, every run)

Before spawning implementer + tester, the orchestrator MUST render an inline spec preview and ask for explicit approval. This is non-skippable, even when the researcher reports zero ambiguities. It is the only chance the user gets to inspect the per-method derivation plan before any code is written to disk.

1. **Render** an inline preview in the chat response. Pull from
   `active_notes_file` (either `{{NOTES_FILE}}` or `{{HYPHA_NOTES_FILE}}`,
   per Gate R-C — or `{{NOTES_FILE}}` in a legacy-only run):
   - File path + GitHub issue number (link to issue if practical).
   - Per method:
     - Signature line (`internal static <ReturnType> Compute<Name>(this {{TARGET_INTERFACE}} {{SUBJECT_PARAM}})`).
     - Derivation source tag: `OCL in XMI`, `OCL in <remarks>`, or `spec-text only`.
     - The suggested C# code block from the notes — single fenced block, cap ≤ 8 lines.
     - Dependencies summary (sibling derived properties used; upstream stubs hit).
     - Stub-blocker flag (if any) — signals that the populated case will need the stub-blocker test pattern.
   - Use a compact Markdown format. Cap total preview at ~80 lines.

2. **Ask** via `AskUserQuestion` (single question, 2 options — "drop files" collapses out in single-file mode):
   - **Approve — spawn implementer + tester now** *(Recommended)*. Proceeds to step 6.
   - **Abort — research again with feedback**. Orchestrator forwards the user's free-form `Other` text to the researcher whose notes are `active_notes_file` (either legacy or Hypha) via `SendMessage`, waits for a fresh `spec ready` / `hypha spec ready`, then re-runs step 5.5. When Hypha is available, ALSO re-run step 5.4 (comparator) if the re-research changed the active notes file — the disk-side artifact needs to stay in sync.

3. **On Approve**, proceed to step 6. Do NOT re-ask until the run completes.

4. **On Abort (user rejects without requesting re-research)**, stop the orchestration. The notes file remains for the user's reference.

Rationale: the previous "After the researcher finishes, read `{{NOTES_FILE}}` yourself to verify" instruction was an internal check by the orchestrator, with no user-visible gate. This gate runs *every time*, so the user always sees the researcher's contract before any code is committed to disk. When Hypha is available, Gate R-C ran BEFORE this gate to let the user pick which grounding path drives the implementation; this gate then displays that picked notes file's per-method contract. The gate governs STRUCTURAL approval only — individual tool-permission prompts that the user's `settings.json` requires (e.g. `Bash(dotnet build *)`) continue to surface during steps 6 / 7 / 8 / 9 / 11.

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
override). The prompt MUST instruct the implementer to read `active_notes_file`
first (either `{{NOTES_FILE}}` or `{{HYPHA_NOTES_FILE}}`, per Gate R-C —
substitute the concrete path before spawning).

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

1. Build production (paths are repo-relative, executed from the repo root):
   ```bash
   dotnet build SysML2.NET/SysML2.NET.csproj --nologo --verbosity quiet
   ```
   On failure, dispatch the implementer back to fix its own bugs (do not
   delegate to a fresh agent unless the original is non-responsive).

2. Run targeted fixture:
   ```bash
   dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj --filter "FullyQualifiedName~<FOO>ExtensionsTestFixture" --nologo --verbosity quiet
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
dotnet test SysML2.NET.sln --no-build --nologo --verbosity quiet
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
`{{TEST_FILE}}` against `active_notes_file` (either `{{NOTES_FILE}}` or
`{{HYPHA_NOTES_FILE}}`, per Gate R-C) and produces an "OK / NEEDS FIX" verdict.

If the verdict is "NEEDS FIX", dispatch the implementer or tester back to
action the findings (the reviewer never edits).

### 10. Final summary + commit-ready handoff (END OF RUN)

After step 11 (issue checklist sync) completes, the orchestrator stops. **This is the end of the run.** The orchestrator does NOT run `git add`, does NOT run `git commit`, does NOT push the user's commit, does NOT open the PR. Those are entirely the user's job.

Report to the user:
- Modified files (production + test fixture + notes + any regression-sweep test fixtures).
- Test counts (X/Y green for the targeted fixture; A/B green for the full solution).
- Reviewer verdict + any unresolved findings.
- **Hypha comparison block** (ONLY when `hypha_available == true`):
  - Overall verdict from `{{COMPARISON_FILE}}` (`hypha stronger` /
    `legacy stronger` / `equivalent` / `mixed`) + counts
    (`agree / disagree / legacy-only / hypha-only`).
  - Which notes file drove implementation (`active_notes_file`).
  - Repo-relative path to `{{COMPARISON_FILE}}` for the user to inspect
    after the run — the A/B corpus lives on disk regardless of the Gate R-C
    pick.
  - If the run downgraded mid-flight (Hypha errored after step 5b), a short
    note saying "Hypha comparison unavailable — downgraded to legacy-only
    at step X" and skip the rest of the block.
- Out-of-scope blockers surfaced (e.g. "5 populated cases use the stub-blocker
  pattern because `<UpstreamMethod>` in `<UpstreamFile>.cs` is still a stub —
  consider a follow-up issue").
- Spec-text-only methods (e.g. `IsConjugated`) — flag separately so the user
  knows the implementation is grounded in spec prose rather than OCL.
- **Issue checklist sync**: `<issue-url>` — `<newly-ticked>` newly ticked,
  `<newly-added>` newly added, `<ticked>/<total>` total (filled in after step 11).
- **Pre-filled commit message** (MANDATORY — append at the very end of the
  final-summary message in a fenced code block, ready to copy):

  ```
  Fix #<n>
  ```

  Where `<n>` is the GitHub issue number handled by this run. Nothing else —
  no body paragraphs, no per-method bullet list, no `Co-Authored-By` trailer,
  no "🤖 Generated with …" footer. The single line is the entire message.

- **Explicit handoff line** — the orchestrator must include this verbatim at the bottom:

  > Review `git diff`, stage the in-scope files (`git add <path> …` — NEVER `-A` / `.`), commit with the message above, then `git push` (if the remote branch ref does not yet exist, use `git push -u origin <branch>` once). Open the PR yourself via the GitHub UI or `gh pr create --base development`.

After the handoff line, the orchestrator stops. The run is complete. If the user explicitly asks in a follow-up turn for the agent to push their commit or open the PR, the agent does so per the CLAUDE.md "Branch & PR workflow (MANDATORY)" → "If the user does explicitly ask the agent to push or open the PR" subsection. Otherwise the user handles those steps themselves. See `feedback_pr_mandatory.md`.

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
  role that handles spec-text-only methods cleanly. **When the Hypha plugin is
  installed**, an additional `hypha-researcher` role runs IN PARALLEL with the
  legacy researcher, followed by a `comparator` agent and Gate R-C where the
  user picks which notes drive downstream. See the pre-flight section at the
  top of this file and steps 5b / 5.4 / 5.45.
- **No cross-contamination**: legacy researcher MUST NOT use Hypha; Hypha
  researcher MUST NOT read `Resources/*.uml` or
  `Resources/specification/*.pdf.txt`. Both may read the production file's
  C# signature. This is enforced in the team-template role prompts, not by
  the tool sandbox — respect it when substituting placeholders.
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
- **Commit is the user's job** — the agent NEVER runs `git commit`, ever. Step 10 (final summary) ends with a pre-filled commit message + handoff line, then the run is over. See `feedback_pr_mandatory.md` and CLAUDE.md "Branch & PR workflow (MANDATORY)".
- **Push + PR are the user's job too** — the agent does NOT proactively push commits or open PRs. It only performs those if the user explicitly asks in a follow-up turn (rare; user-initiated only). Unlike `/implement-extensions-batch`, this command does NOT create a branch, so there is no empty-branch push to perform — the user owns branch creation and remote setup.
- **Plan mode is handled by Gate 0 at the top of this file** — the orchestrator writes the proposed-execution plan to the plan file, calls `ExitPlanMode`, and proceeds on approval. The orchestrator never spawns sub-agents while plan mode is active, so the previous "degraded mode" workaround is no longer needed and has been removed.
- **Gate R-A (step 5.5) is mandatory every run.** It is the only structural checkpoint between the researcher returning `spec ready` and the implementer + tester being spawned. Do not skip it even when the researcher reports zero ambiguities — the user explicitly asked for an unconditional gate so they can review per-method derivations before code is written.
- **Gate R-C (step 5.45) is mandatory ONLY when `hypha_available == true`.** It runs BEFORE Gate R-A and is the user's one choice-point between the two grounding paths (Hypha vs. legacy). Do not skip it when Hypha is on. Do not add it when Hypha is off (there's nothing to compare). The pre-flight `hypha_available` flag is the single source of truth for whether Gate R-C fires.
- **Tool-level prompts (`Bash(...)`, `Edit(...)`, `Write(...)`, `Agent(...)`) still surface per the user's `settings.json`.** Gate 0 and Gate R-A govern STRUCTURAL approval only. The user has explicitly chosen to keep handling tool-level prompts manually rather than auto-allowing them via permission rules or hooks.
- **Sub-agent-side deadlocks**: if an implementer or tester gets parked on a harness UI permission prompt the user cannot action, the orchestrator should take over the deadlocked work directly from its own permission scope (running `dotnet build` / `dotnet test` / `gh issue edit` itself). Send `shutdown_request` to the parked sub-agent. Surface clearly in the final summary.
