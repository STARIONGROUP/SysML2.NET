---
description: Spawn ONE 4-role team across N SysML2.NET Extend files in one run — creates a batch branch, assigns the related GitHub issues to the user, and updates each issue's checklist on completion. One agent per role for the whole batch (NOT one team per file).
argument-hint: <file1.cs> <file2.cs> [<file3.cs> ...]   (2–6 Extension file names; each will be normalised to SysML2.NET/Extend/<Foo>Extensions.cs)
---

# /implement-extensions-batch

Apply the **`/implement-extensions` 4-role team workflow** across N files
(`$ARGUMENTS`) in one run — using ONE team for the entire batch. Per role:

| Role | Agent count (was → now) | Scope |
|---|---|---|
| Researcher | N → **1** | Writes ALL N notes files in one pass. |
| Implementer | N → **1** | Edits ALL N production files in one pass. |
| Tester | N → **1** | Edits ALL N test fixtures in one pass. |
| Reviewer | N → **1** | Reviews ALL N production + test pairs in one pass. |

The only parallelism left is **inside Phase IT**: the single implementer and the
single tester are spawned in parallel with each other (they read disjoint files,
so safe). Phase R, Phase RV, and the regression sweep each run a single agent.

The team template (role prompts) at `.claude/team-templates/extension-impl.md`
(v2, repo-tracked) still defines the role behaviour — but for batch use, the
orchestrator EXPANDS each prompt to cover the full file list rather than
substituting a single `{{PRODUCTION_FILE}}` / `{{TEST_FILE}}` / `{{NOTES_FILE}}`.
See "Prompt adaptation rules" below.

What this command adds on top of the single-file flow:

1. **Pre-flight validation** of every file + its GitHub issue, before any state
   change.
2. **Creates a new git branch** off `development` with a deterministic name
   derived from the batch's issue numbers.
3. **Assigns every related GitHub issue to the invoking user** (`@me`).
4. **Single consolidated regression sweep** instead of one per file.
5. **Loops the issue-checklist sync** per file at the end.

## Path conventions

Repo-relative with forward slashes throughout. Tools that require absolute paths
(`Read`, `Edit`, `Write`) get the repo root prepended at runtime.

## Hard scope rule

**Only modify**:
- Each named production file `SysML2.NET/Extend/<Foo>Extensions.cs` in the batch.
- Each file's corresponding test fixture
  `SysML2.NET.Tests/Extend/<Foo>ExtensionsTestFixture.cs`.
- The researcher notes file per batch member: `.team-notes/<foo>-extensions-spec.md`.
- Sibling test fixtures whose `Throws.TypeOf<NotSupportedException>()` assertions
  now fail because one of the batch's implementations unblocked them (consolidated
  regression sweep, see step 11).

**MUST NOT modify** the same things the single-file command refuses to touch:
other production files in `SysML2.NET/Extend/` or `SysML2.NET/Core/`,
auto-generated POCOs / interfaces, code-generator templates.

`feedback_scope_discipline.md` applies just as in `/implement-extensions`. Use the
stub-blocker test pattern (see template) when an in-scope test would otherwise
need to traverse a still-stubbed upstream method that is NOT part of the current
batch.

## Prompt adaptation rules (single-file template → batch role prompts)

The v2 template at `.claude/team-templates/extension-impl.md` is written for
ONE file per agent. For the batch command, the orchestrator adapts each role
prompt as follows BEFORE the `Agent(...)` call:

1. **File-list expansion**: replace each singular placeholder
   (`{{PRODUCTION_FILE}}`, `{{TEST_FILE}}`, `{{NOTES_FILE}}`,
   `{{TARGET_INTERFACE}}`, `{{TARGET_METACLASS_NAME}}`, `{{SUBJECT_PARAM}}`)
   with a numbered list `(file 1: …, file 2: …, file N: …)` and rewrite the
   "Goal" paragraph to iterate over all N files.
2. **Hard-rule-on-file-edits**: rewrite the "ONE file" language to "the
   following N file(s)" and enumerate the exact allowed paths. The agent must
   refuse Write/Edit on any path outside that explicit list.
3. **Methods to research / implement / test**: provide the full union of the
   N method lists, grouped under an `## File: <PRODUCTION_FILE>` heading per
   file so the agent can iterate file-by-file without losing the per-file
   contract.
4. **Verification step**: keep the in-prompt `dotnet build` invocation but
   target only the relevant project once (production for implementer, test for
   tester). The orchestrator runs the consolidated targeted `dotnet test` in
   Phase V.
5. **Parallel-mode caveat (tester only)**: unchanged. The tester is spawned
   in parallel with the implementer, so it MUST NOT run `dotnet test` — only
   `dotnet build` of the test project to confirm its fixture compiles.
6. **When-done message**: ask the agent to summarise per file, not in one
   blob, so the orchestrator can attribute findings back to the right file.

The team template itself does NOT need to change. The single-file
`/implement-extensions` command continues to use the unadapted prompts.

## Sub-agent spawn mode (applies to EVERY `Agent(...)` call below)

Every `Agent(...)` call in this command MUST pass `mode: "acceptEdits"`
explicitly. Same rule as the single-file command — see the "Sub-agent spawn
mode" section in `.claude/commands/implement-extensions.md` for the
rationale. Applies to: legacy researcher (8a), Hypha researcher (8b),
comparator (8.5), implementer (9), tester (9), regression-sweep tester (11),
reviewer (12).

## Pre-flight: Hypha plugin detection (runs before step 1)

Identical semantics to the single-file command's Hypha pre-flight (see
`.claude/commands/implement-extensions.md` → "Pre-flight: Hypha plugin
detection"). Summary:

- Record `hypha_available: bool` at the very start of the invocation.
- When `hypha_available == true`, both researchers run in parallel at Phase R
  (step 8), followed by the comparator (step 8.5), followed by the new
  Gate B-C (batch-scoped, one pick across all N files).
- When `hypha_available == false`, surface a one-line install recommendation
  once and continue with the legacy-only flow — steps 8, 9, ..., 14 as
  written. Skip step 8b, step 8.5, and Gate B-C entirely.
- On mid-run downgrade (Hypha errors between 8b and 8.5), stop dispatching
  Hypha work, skip 8.5 + Gate B-C, proceed with `active_notes_file` per
  file = its legacy `NOTES_FILE`. Surface the downgrade in step 14's final
  summary.

## Workflow

### 1. Parse `$ARGUMENTS` and validate the batch

- Split `$ARGUMENTS` by whitespace into a list of filenames.
- Accept each in the same forms as `/implement-extensions`:
  - repo-relative `SysML2.NET/Extend/<Foo>Extensions.cs`
  - with `@` prefix
  - absolute path
- For each token, extract the `<Foo>Extensions.cs` filename and reconstruct the
  canonical repo-relative path. Resolve duplicates (silently de-dup; warn the
  user if any were dropped).
- **Refuse the batch and route to the single-file command** if there is exactly
  1 file after de-dup. Print the equivalent `/implement-extensions` command
  and stop.
- **Refuse the batch and ask the user to split** if there are more than 6 files
  after de-dup (`AskUserQuestion`: "split into batches of 6?"). Cap exists to
  contain the single-context working set of each agent (~6 production files +
  ~6 spec files + ~6 test fixtures per agent's context).
- **Verify each file exists** from the repo root. Fail-fast with a per-file
  status line if any are missing — do NOT proceed to branch creation.

### 2. Per-file metadata discovery (sequential, ~1 minute total)

For each file in the batch, derive the same auto-paths as `/implement-extensions`
step 2:

| Slot | Rule |
|---|---|
| `PRODUCTION_FILE` | `SysML2.NET/Extend/<Foo>Extensions.cs` |
| `TEST_FILE` | `SysML2.NET.Tests/Extend/<Foo>ExtensionsTestFixture.cs` (must exist; otherwise surface to user) |
| `TARGET_INTERFACE` | `I<Foo>` — find via Glob `SysML2.NET/Core/AutoGenPoco/**/I<Foo>.cs` |
| `TARGET_METACLASS` | `<Foo>` |
| `SUBJECT_PARAM_NAME` | lowercase first char of `<Foo>` + `<Foo>[1..]` + `Subject` |
| `NOTES_FILE` | `.team-notes/<foo>-extensions-spec.md` (kebab-case) — legacy researcher output |
| `HYPHA_NOTES_FILE` | `.team-notes/<foo>-extensions-spec-hypha.md` — only when `hypha_available == true` |
| `COMPARISON_FILE` | `.team-notes/<foo>-extensions-comparison.md` — only when `hypha_available == true` |
| `ACTIVE_NOTES_FILE` | per-file, computed at Gate B-C (or defaulted to the legacy `NOTES_FILE` when Hypha is off). Substituted into implementer/tester/reviewer prompts. |
| `TEAM_NAME` | `batch-extensions-impl` (one team name for the whole batch) |
| `ISSUE_NUMBER` | from `gh issue list … --search "SysML2.NET/Extend/<Foo>Extensions.cs in:body"` |

`gh` discovery rule per file:
- 1 match → record `(file, issue_number)`.
- 0 or >1 matches → `AskUserQuestion` for an explicit issue number for that
  specific file before continuing. If the user can't provide one, drop that
  file from the batch with their consent.

### 3. Enumerate stubs + grade complexity per file

For each file in the (possibly reduced) batch:

- Grep the production file for `throw new NotSupportedException`. List the
  enclosing methods. If 0, drop the file from the batch and inform the user
  (already implemented).
- Apply the same complexity-grading rubric from `/implement-extensions` step 3.5
  to that file's method list. Record `(complexity, …)`.

Since the batch uses ONE agent per role, the model pick is BATCH-WIDE, not
per-file. Take the MAX complexity across all files and use that to pick the
model for each role.

If the batch becomes empty after pruning, abort cleanly.

### 4. Pre-flight git checks

- `git status --porcelain` must be empty. Refuse to proceed otherwise — the user
  has unstaged work that would be entangled with the batch.
- `git fetch origin development` to ensure the base branch is up-to-date locally.

### 5. Sanity check with the user

Use `AskUserQuestion` to present:

- The final batch composition (files + issues + per-file complexity + the
  batch-wide max-complexity grade).
- The proposed branch name (see step 6).
- Questions:
  1. **Proceed with this batch composition?** (Yes / No / drop specific files)
  2. **Model for the four batch agents?** (default by batch-wide max complexity:
     Haiku trivial / Sonnet standard / Opus complex; or override "all Sonnet"
     / "all Opus" / custom).

If user picks "drop specific files" or overrides models, apply and re-confirm.

### 6. Create the batch branch (after step 5 yes)

Branch name format:

```
batch-impl-extensions-<dashed-issue-numbers>
```

- Sort issue numbers ascending.
- If 4 or fewer issues: include all (e.g.
  `batch-impl-extensions-123-180-186-190`).
- If more than 4 issues: include the first 4 + `-plus<N-4>` suffix (e.g.
  `batch-impl-extensions-123-180-186-190-plus2` for N=6).

Create the local branch AND immediately push the empty ref to `origin`:

```bash
git switch -c <branch-name> origin/development
git push -u origin <branch-name>
```

The second push is safe: the branch tip equals `origin/development`'s tip, so no commits are pushed — just the ref is created on origin. This is the only push the orchestrator performs by default; it exists so the user's later `git push` of their own commit is a trivial fast-forward with no `-u` setup hassle.

Refuse if the branch already exists locally OR on origin (`git ls-remote --exit-code origin <branch>`) — ask the user to pick a different batch or delete the stale branch.

If `git push -u origin <branch-name>` fails (e.g. no GitHub auth, no write permission), surface the error and continue with the local branch only — the user will set up the remote ref themselves later.

### 7. Assign every issue to `@me`

```bash
for num in <each-issue-number>; do
    gh issue edit "$num" --repo STARIONGROUP/SysML2.NET --add-assignee @me
done
```

Idempotent — re-assigning is a no-op on `gh`. Report success/failure per issue;
on failure, log and continue (an unassignable issue is not a blocker for the
implementation itself).

### 8. Phase R — Spawn the batch researcher(s)

Split into 8a + 8b based on `hypha_available` from the pre-flight.

#### 8a. Legacy batch researcher (ALWAYS run)

**One `Agent(...)` call** for the entire batch:

- `subagent_type: "general-purpose"`
- `model: <researcher_model>` per the batch-wide step-5 grade.
- Foreground.
- Prompt: the v2 legacy researcher prompt from
  `.claude/team-templates/extension-impl.md`, adapted per "Prompt adaptation
  rules" above:
  - Allowed-write list: ALL N notes files
    (`.team-notes/<foo1>-extensions-spec.md`, …,
    `.team-notes/<fooN>-extensions-spec.md`).
  - "Methods to research" section: enumerate ALL methods across ALL N files,
    grouped under an `## File: <PRODUCTION_FILE>` heading per file.
  - "When done" SendMessage payload: a per-file summary (file → derivation
    source → transitive stub-blocker flags), not a single blob.

#### 8b. Hypha batch researcher (ONLY when `hypha_available == true`)

**Spawn 8a and 8b in the SAME orchestrator message** — one message, two
`Agent(...)` calls, both foreground. They run in parallel with completely
independent contexts.

- `subagent_type: "general-purpose"`
- `model: <researcher_model>` — same tier as legacy.
- Foreground.
- Prompt: the v2 `hypha-researcher` prompt, adapted per "Prompt adaptation
  rules":
  - Allowed-write list: ALL N Hypha notes files
    (`.team-notes/<foo1>-extensions-spec-hypha.md`, …,
    `.team-notes/<fooN>-extensions-spec-hypha.md`).
  - "Methods to research" enumeration: same as legacy (identical union of
    all N method lists, grouped per file).
  - The forbidden-reads list is UNCHANGED from the single-file prompt —
    the batch still MUST NOT touch `Resources/*.uml` or
    `Resources/specification/*.pdf.txt` regardless of file count.
  - Same per-file summary payload shape as 8a, plus explicit flags on
    methods where Hypha returned "not found" or had to fall back to a
    spec citation.

#### 8c. Post-researcher coverage check

After BOTH agents return (or just 8a in a legacy-only run), read each notes
file yourself to verify coverage. Re-dispatch the individual researcher whose
file(s) are empty / missing sections. Do NOT proceed until all files
(N legacy + N Hypha, or just N legacy) are complete.

If `hypha_available == false`, skip 8b + 8c-Hypha, skip step 8.5 + Gate B-C,
and proceed directly to step 9 (Phase IT) with each file's `ACTIVE_NOTES_FILE`
defaulted to its legacy `NOTES_FILE`.

### 8.5. Phase C — Spawn the batch comparator (ONLY when `hypha_available == true`)

**One `Agent(...)` call** for the whole batch:

- `subagent_type: "general-purpose"`
- `model: <researcher_model>` — same tier as the researchers.
- Foreground.
- Prompt: the v2 `comparator` prompt, adapted per "Prompt adaptation rules":
  - Allowed-write list: ALL N comparison files
    (`.team-notes/<foo1>-extensions-comparison.md`, …,
    `.team-notes/<fooN>-extensions-comparison.md`).
  - Input pairs: for each of the N files, the pair
    (`.team-notes/<foo>-extensions-spec.md`,
    `.team-notes/<foo>-extensions-spec-hypha.md`).
  - "When done" SendMessage payload: per-file overall verdict + counts,
    followed by a top-level batch verdict roll-up (majority-verdict wins,
    with the count of files in each bucket surfaced).

After it returns, read each `COMPARISON_FILE` yourself to build the batch-wide
inline preview for Gate B-C.

### 8.7. Gate B-C — Batch-Comparison gate (ONLY when `hypha_available == true`)

Before Phase IT, present the batch-wide comparison inline and ask for a
**single batch-wide pick** (Hypha vs. legacy) that applies to ALL N files.
This intentionally differs from the single-file Gate R-C: at N=6, per-file
picking is unusable — the gate would become 6 sequential
`AskUserQuestion` calls. A single batch-wide pick is the pragmatic tradeoff;
the per-file comparison files remain on disk regardless so the user can
audit each file's fit after the run.

1. **Render** an inline preview:
   - Batch-wide verdict roll-up (e.g. "Hypha stronger for 4/6 files, legacy
     stronger for 1/6, equivalent for 1/6").
   - Per-file table:

     | File | Verdict | Counts (agree/disagree/legacy-only/hypha-only) | Top concern |
     |---|---|---|---|
     | `<Foo1>Extensions.cs` | hypha stronger | 6/1/0/0 | — |
     | `<Foo2>Extensions.cs` | mixed | 3/2/1/0 | `ComputeX` — different OCL translation |
     | … | … | … | … |

   - Paths to all N `COMPARISON_FILE`s so the user can dive in per file.
   - Cap at ~120 lines (higher than single-file since the table scales with N).

2. **Ask** via `AskUserQuestion` (single question, 3 options):
   - **Use Hypha notes across the batch** — every file's
     `ACTIVE_NOTES_FILE` = its `HYPHA_NOTES_FILE`.
   - **Use legacy notes across the batch** — every file's
     `ACTIVE_NOTES_FILE` = its `NOTES_FILE`.
   - **Abort — re-research with feedback** — free-form `Other` text is
     forwarded to the still-addressable researcher(s) via `SendMessage`;
     after they return, re-run 8.5 and this gate.

3. **On pick**, record the per-file `ACTIVE_NOTES_FILE` map on orchestrator
   state (the pick is batch-wide but the map is per-file so downstream can
   substitute the correct path per file). Proceed to Phase IT (step 9).

4. **On abort without re-research**, stop the orchestration. All
   `.team-notes/*.md` files remain for the user's reference. The branch
   created in step 6 stays; no Phase IT edits happen.

Rationale: Gate B-C is the batch analogue of the single-file Gate R-C, with
a single-question shape because per-file picking at N=6 is impractical. The
per-file `COMPARISON_FILE`s stay on disk regardless of the pick so a mixed
verdict is still auditable file-by-file after the run.

### 9. Phase IT — Spawn the batch implementer + tester in parallel (TWO agents)

**One orchestrator message containing exactly 2 `Agent(...)` calls** — ONE
implementer and ONE tester. Both foreground. They run in parallel because the
implementer writes only to the N production files and the tester writes only
to the N test fixtures (disjoint sets).

- **Implementer prompt**: the v2 implementer prompt, adapted per "Prompt
  adaptation rules":
  - Allowed-write list: ALL N production files.
  - "Methods to implement" section: enumerate ALL methods across ALL N files,
    grouped under an `## File: <PRODUCTION_FILE>` heading per file. Order
    files by dependency tier (file A's stubs that file B depends on come first).
  - Reads ALL N `ACTIVE_NOTES_FILE`s before starting (per-file, from the
    Gate B-C pick — either legacy or Hypha; in a legacy-only run, all
    files' `ACTIVE_NOTES_FILE` = their legacy `NOTES_FILE`).
- **Tester prompt**: the v2 tester prompt, adapted similarly.
  - Allowed-write list: ALL N test fixtures.
  - "Methods to test" section: enumerate per file.
  - **Parallel-mode caveat still applies** — tester runs only `dotnet build` of
    the test project, NEVER `dotnet test` (production lacks the implementer's
    parallel-turn edits in the tester's disk view).
  - Reads ALL N `ACTIVE_NOTES_FILE`s before starting.

### 10. Phase V — Orchestrator verification (sequential)

After both agents return, run sequentially in the orchestrator turn:

1. **One build of production**:
   ```bash
   dotnet build SysML2.NET/SysML2.NET.csproj --nologo --verbosity quiet
   ```
   On failure, attribute the broken file(s) by reading the build diagnostics
   and re-dispatch THE implementer with a focused brief naming only the
   broken file(s) and the specific compile errors. Iterate.
2. **One consolidated targeted test run**, OR-joining every fixture in the
   batch:
   ```bash
   dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj \
       --filter "FullyQualifiedName~<Foo1>ExtensionsTestFixture|FullyQualifiedName~<Foo2>ExtensionsTestFixture|..." \
       --nologo --verbosity quiet
   ```
   For each failure, attribute it to the correct file and decide which role
   to re-dispatch:
   - OCL mistranslation in production → re-dispatch THE implementer with a
     focused brief naming the broken method(s) and the correct OCL→C# mapping.
   - Wrong test assertion → re-dispatch THE tester with a focused brief naming
     the broken `Verify*` method(s).
   Iterate until 0 failures across the batch.

### 11. Phase S — Consolidated regression sweep (ONE agent)

```bash
dotnet test SysML2.NET.sln --no-build --nologo --verbosity quiet
```

For each `Expected: <NotSupportedException> But was: no exception` failure,
identify which file in the batch unblocked it (grep the failing test for `For
Later: depends on …` references; or trace by the targeted stub's signature).

If any sibling fixtures need expansion, dispatch **ONE regression-sweep tester**
(not N — one agent gets the full list of touched siblings). Use the
**expand-don't-replace** brief from `/implement-extensions` step 8 (filter
discrimination + predicate completeness + owned vs inherited + null-projection
guard). The tester's allowed-write list is the set of touched sibling
fixture files.

Iterate until the full solution test run is 0 failures.

### 12. Phase RV — Spawn the batch reviewer (ONE agent)

**One `Agent(...)` call** for the entire batch — the v2 reviewer prompt adapted
per "Prompt adaptation rules":

- Read-only across the repo (unchanged from the template).
- Files to review: ALL N `ACTIVE_NOTES_FILE`s (per-file, from Gate B-C) +
  ALL N production files + ALL N test fixtures + any sibling fixtures
  touched in Phase S. The Hypha notes file / legacy notes file that was
  NOT picked at Gate B-C is not part of the reviewer's contract for that
  file, but is still preserved on disk for the A/B corpus.
- "Output format" SendMessage payload: per-file `OK / NEEDS FIX` verdicts, then
  a batch-wide summary line. The orchestrator needs to know which files need
  re-dispatch and which are clean.

For each per-file "NEEDS FIX" verdict, dispatch THE implementer or tester back
with a focused brief naming only the broken file(s) and the reviewer's findings.
Other files' results are still reported in the final summary.

### 13. Phase IS — Issue checklist sync (sequential, looped)

For each `(file, issue_number)` pair in the batch, run the **identical**
step-11 logic from `/implement-extensions`:

1. Fetch issue body (`gh issue view <num> --json body -q .body`).
2. Locate `### Checklist` section.
3. Enumerate `Compute*` methods in the production file. Tick the ones whose
   bodies no longer throw `NotSupportedException` AND whose `Verify{Method}`
   passed in step 10's last `dotnet test` run.
4. Append any new methods (signature not present in existing checklist) in
   declaration order.
5. Stitch new body (touch ONLY the Checklist section).
6. Push via `gh issue edit <num> --body-file <tmp-body-file>`.
7. Re-fetch + diff to verify only the Checklist section changed.

### 14. Final summary + commit-ready handoff (END OF RUN)

After Phase IS completes, the orchestrator stops. **This is the end of the batch run.** The orchestrator does NOT run `git add`, does NOT run `git commit`, does NOT push the user's commit, does NOT open the PR. Those are entirely the user's job. The agent's git involvement was bounded to step 6 (create branch locally + push empty ref).

Print to the user:

- **Branch**: name + base ref. Note that the empty branch was already pushed to `origin/<branch-name>` in step 6, so the user's `git push` of their own commit will be a trivial fast-forward.
- **Per-file table**:

  | File | Stubs impl. | Targeted tests | Reg. sweep impact | Reviewer | Issue |
  |---|---|---|---|---|---|
  | `<Foo1>Extensions.cs` | X/X | X/X green | N siblings expanded | OK | #<n> ticked X/X |
  | `<Foo2>Extensions.cs` | … | … | … | … | … |

- **Branch-wide totals**:
  - Files modified (sum of production + tests + sibling fixtures touched). `.team-notes/` are gitignored and stay local automatically.
  - Full solution test count (e.g. `1082/1082`).
  - Unresolved reviewer findings (if any).
  - Spec-text-only methods flagged separately (grounded in spec prose, not OCL).
  - Out-of-scope blockers surfaced.

- **Hypha comparison block** (ONLY when `hypha_available == true`):
  - Batch-wide verdict roll-up from the N `COMPARISON_FILE`s
    (e.g. "Hypha stronger for 4/6 files, legacy stronger for 1/6,
    equivalent for 1/6").
  - Which notes path drove implementation (batch-wide: Hypha or legacy,
    per Gate B-C).
  - Repo-relative paths to all N `.team-notes/<foo>-extensions-comparison.md`
    files for the user to inspect after the run — the A/B corpus lives on
    disk regardless of the Gate B-C pick.
  - If the run downgraded mid-flight (Hypha errored between 8b and 8.5),
    a short note saying "Hypha comparison unavailable for the batch —
    downgraded to legacy-only at step X" and skip the rest of the block.

- **Pre-filled commit message** (MANDATORY — append at the very end of the final-summary message in a fenced code block, ready to copy):

  ```
  Fix #<n1> #<n2> #<n3> …
  ```

  Single line. No body, no `Co-Authored-By` trailer, no "🤖 Generated with …" footer. The numbers are exactly the GitHub issue numbers handled by this batch, in the original `$ARGUMENTS` order (or, if the user expressed a preferred order in the invocation prompt, that order).

- **Explicit handoff line** — the orchestrator must include this verbatim at the bottom:

  > Review `git diff`, stage the in-scope files (`git add <path1> <path2> …` — NEVER `-A` / `.`), commit with the message above, then `git push` (the remote branch already exists from step 6, so this is a fast-forward — no `-u` needed). Open the PR yourself via the GitHub UI or `gh pr create --base development`.

After the handoff line, the orchestrator stops. The run is complete. The user may follow up with a separate request (e.g. "rerun the tests", "amend the fix") which the orchestrator serves as a new turn — but the agent does NOT proactively push, PR, or commit. If the user explicitly asks the agent to push or open the PR, the agent does so per the CLAUDE.md "Branch & PR workflow (MANDATORY)" → "If the user does explicitly ask the agent to push or open the PR" subsection.

## Failure handling

| Failure | When | Disposition |
|---|---|---|
| Missing input file | Step 1 | Abort, no state change. |
| File has 0 stubs | Step 3 | Drop from batch, inform user; abort if batch becomes empty. |
| Ambiguous issue | Step 2 | `AskUserQuestion` for an explicit issue number per file. |
| Dirty working tree | Step 4 | Abort, ask user to commit/stash. |
| Branch already exists | Step 6 | Abort; ask user to pick a different batch or delete the stale branch. |
| `gh issue edit --add-assignee` fails for one issue | Step 7 | Log + continue (non-blocking; implementation still proceeds). |
| Researcher's notes file missing/empty for one file | Step 8a / 8b | Re-dispatch THE affected researcher (legacy or Hypha) with a focused brief naming only that file. |
| Hypha plugin errors mid-run (between 8b and 8.5) | Step 8 / 8.5 | Skip step 8.5 + Gate B-C. Set every file's `ACTIVE_NOTES_FILE` = its legacy `NOTES_FILE`. Surface downgrade in step 14. |
| Comparator's report missing/empty for one file | Step 8.5 | Re-dispatch THE comparator with a focused brief naming only that file's pair. |
| Production build fails | Step 10.1 | Re-dispatch THE implementer with a focused brief naming the broken file(s) + compile errors. |
| Targeted test fails | Step 10.2 | Attribute (OCL vs test bug), re-dispatch THE implementer or THE tester with a focused brief. |
| Sibling test failure in regression sweep | Step 11 | Dispatch ONE regression-sweep tester with the full sibling list. |
| Reviewer NEEDS FIX for one or more files | Step 12 | Re-dispatch THE implementer or THE tester with a focused brief naming the broken file(s); other files' results still reported. |
| Implementer's context runs out mid-batch | Any IT/V step | Re-dispatch THE implementer with a focused brief covering only the unfinished file(s). Partial progress on disk is preserved. |
| Batch partially fails after branch + assignment | Any step ≥ 6 | Keep the branch; surface in final summary; user decides whether to retry via `/implement-extensions` for the still-broken single file or revert. |
| Empty-branch push to origin fails | Step 6 | Surface the error and continue with the local branch only. The user sets up the remote ref themselves later. |
| User explicitly asks the agent to push their commit and the commit doesn't exist on the current branch | User-initiated push request | Refuse, surface — agent only pushes commits that are already on the branch (which the user made themselves). |
| User explicitly asks the agent to push and the current branch is `development`/`master` | User-initiated push request | REFUSE — feature work must live on a feature branch first. Surface to the user. |

## Parallelism caps (orchestrator self-enforced)

- N ≤ 6 files per batch (single-context working set limit for each agent).
- Phase R:
  - Legacy-only run (`hypha_available == false`): **1** agent.
  - Hypha run (`hypha_available == true`): **2** agents in parallel
    (1 legacy researcher + 1 Hypha researcher). Same-message dual
    `Agent(...)` calls; disjoint output paths (legacy → `NOTES_FILE`,
    Hypha → `HYPHA_NOTES_FILE`).
- Phase C (comparator, only in Hypha runs): **1** agent.
- Phase IT: **2** agents in parallel (1 implementer + 1 tester).
- Phase S regression-sweep tester: **1** agent.
- Phase RV: **1** agent.

There is NO N-parallelism within any phase. The batch is sized for a single
context per role.

## Notes for the orchestrator (you, the main agent)

- The team-template role prompts at `.claude/team-templates/extension-impl.md`
  are the **source of truth** for per-role behaviour. The batch command
  ADAPTS those prompts per "Prompt adaptation rules" above (file-list
  expansion, multi-file allowed-write list, per-file method grouping).
  Do not invent new role prompts from scratch.
- All paths in agent prompts must be repo-relative with forward slashes.
- Legacy researcher is **mandatory** for the batch, even when one or more
  files have been seen before via `/implement-extensions`. It is cheap and
  produces the contract the implementer/tester/reviewer read (when Gate B-C
  picks legacy, or the run is legacy-only).
- **Hypha researcher + comparator + Gate B-C run when `hypha_available == true`.**
  Both researchers spawn in the same orchestrator message (parallel,
  disjoint outputs), the comparator diffs their N pairs, and Gate B-C
  presents a single batch-wide pick to the user. Neither researcher may
  cross-contaminate: legacy MUST NOT use Hypha, Hypha MUST NOT read raw
  `Resources/*.uml` or `Resources/specification/*.pdf.txt`. See the
  pre-flight section at the top of this file and the template's "How Hypha
  comparison plugs in" section.
- Reviewer is **mandatory** for the batch — cheap insurance against subtle OCL
  mistranslation.
- The branch and the assignments persist even on partial failure. Be explicit
  in the final summary about which files succeeded vs which need follow-up.
- **Commit is the user's job** — agent NEVER runs `git commit`, ever. Step 14 (final summary) ends with a pre-filled commit message + handoff line, then the run is over. See `feedback_pr_mandatory.md` and CLAUDE.md "Branch & PR workflow (MANDATORY)".
- **Push + PR are the user's job too** — the agent does NOT proactively push commits or open PRs. It only performs those if the user explicitly asks in a follow-up turn. The one push the agent does perform by default is the empty-branch push in step 6 (creating the remote ref at the same tip as `origin/development`, so the user's later push of their own commit is a trivial fast-forward).
- If the user supplies a single file, route them to `/implement-extensions`
  with the same filename rather than creating a degenerate 1-file "batch"
  branch.
- **Context budgeting**: each batch agent (researcher, implementer, tester,
  reviewer) handles up to N=6 files in a single context. If an agent's
  context fills before it finishes, re-dispatch it with a focused brief
  covering only the unfinished files — partial on-disk progress is
  preserved across dispatches.
