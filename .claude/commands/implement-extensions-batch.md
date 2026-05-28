---
description: Spawn the 4-role team across N SysML2.NET Extend files in one run — creates and pushes a batch branch, assigns the related GitHub issues to the user, and updates each issue's checklist on completion
argument-hint: <file1.cs> <file2.cs> [<file3.cs> ...]   (2–6 Extension file names; each will be normalised to SysML2.NET/Extend/<Foo>Extensions.cs)
---

# /implement-extensions-batch

Run **ONE** 4-role team (researcher, implementer, tester, reviewer) over N
files (`$ARGUMENTS`) in a single batch. Each agent handles **all N files** in
its own context — not one team per file. Role boundaries are unchanged: the
implementer still cannot touch test fixtures, the tester still cannot touch
production files, the reviewer is still read-only. What changes is the ACL
size: each role's allowed-write set is now the N files of its kind in the
batch instead of one.

What this command does on top of the single-file flow:

1. **Pre-flight validation** of every file + its GitHub issue, before any state
   change.
2. **Creates a new git branch** off `development` with a deterministic name
   derived from the batch's issue numbers, AND pushes it to `origin` with
   upstream tracking set so the user can immediately open a pull request after
   committing.
3. **Assigns every related GitHub issue to the invoking user** (`@me`).
4. **Spawns one team via `TeamCreate` + 4 named agents.** Researcher first,
   then implementer + tester in parallel, then reviewer. Iterative fixes route
   via `SendMessage` to the same named agent — no fresh spawns.
5. **Single consolidated regression sweep** dispatched to the still-running
   tester.
6. **Loops the issue-checklist sync** per file at the end.

The team template (role prompts) at `.claude/team-templates/extension-impl.md`
is the source of truth for both the single-file role prompts AND the
batch-mode addenda. This command body is the batch orchestration glue.

**Total live-agent count: 4** (vs. 4N in the previous per-file design).

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
  regression sweep, see step 12).

**MUST NOT modify** the same things the single-file command refuses to touch:
other production files in `SysML2.NET/Extend/` or `SysML2.NET/Core/`,
auto-generated POCOs / interfaces, code-generator templates.

`feedback_scope_discipline.md` applies just as in `/implement-extensions`. Use the
stub-blocker test pattern (see template) when an in-scope test would otherwise
need to traverse a still-stubbed upstream method that is NOT part of the current
batch.

## Pre-flight: plan mode IS the pre-execution approval gate (Gate 0)

If the orchestrator session is in **plan mode** when `/implement-extensions-batch` is invoked, use plan mode as the natural pre-execution approval gate. Do NOT spawn any sub-agent, do NOT create any branch, do NOT assign any issue. Instead:

1. **Stay in plan mode.** Do all the READ-ONLY pre-flight work that steps 1 → 4 of this command require:
   - Validate every input file exists; refuse missing files.
   - For each file, look up the GitHub issue via `gh issue list … --search "<path> in:body"`. Surface ambiguities (0 or >1 match) via `AskUserQuestion`.
   - Enumerate stub `Compute*` methods per file (`Grep "throw new NotSupportedException"`); drop files with zero stubs.
   - Grade complexity per file (step 3.5 rubric); roll up per-role models (step 3.5 rollup logic).
   - Read `git status --porcelain` (refuse on dirty tree).
   - `git fetch origin development` (read-only).
   - Run `git ls-remote --exit-code origin <proposed-branch>` and `git branch --list <proposed-branch>` to check the branch isn't already taken. If it is, surface via `AskUserQuestion` (different branch / abort).

2. **Write the plan file** (at the path Claude Code provides in the plan-mode system reminder) with the standard structure (Context, Recommended approach, Critical files, Verification). The "Recommended approach" section is a concise description of the proposed execution:
   - Batch composition (files + GitHub issue numbers + per-file complexity grade + stub counts).
   - Proposed branch name + base ref + remote-tracking intent.
   - Team name (`TeamCreate({team_name: "..."})`).
   - Per-role model picks (rolled up from step 3.5). Note that `ExitPlanMode` is binary (approve / reject), so the user cannot override models from inside the plan-approval UI. Two override paths are supported: (a) reject the plan and re-invoke with an explicit model preference in the prompt, OR (b) accept the plan and the orchestrator fires a follow-up `AskUserQuestion` (Yes / "all Sonnet" / "all Opus" / Custom) BEFORE step 6's branch creation.
   - Workflow shape: Phase R → Phase R-A (Gate R-A approval) → Phase IT → V → S → RV → IS.
   - Explicit mention that Phase R-A will pause for `AskUserQuestion` after the researcher returns its `spec ready` (so the user knows there is exactly ONE more structural checkpoint after this one).

3. **Call `ExitPlanMode`.** The user reviews the plan in the standard plan-approval UI:
   - **Approve (unchanged)** → plan mode exits, orchestrator proceeds (model-override `AskUserQuestion` per step 2 above, then step 6 → 7 → 8 → Phase R → Phase R-A → Phase IT → V → S → RV → IS).
   - **Approve (edited)** → the tool result includes a `## Approved Plan (edited by user):` block with the user's modifications. The orchestrator MUST treat the EDITED plan content as the source of truth — re-parse composition / branch / models from it, NOT from the originally-proposed plan. If the user dropped a file by removing it from the composition list, the orchestrator continues with the reduced set. If the user changed the branch name or model picks inline, the orchestrator honors that. (Standard Claude Code plan-edit-on-approval pattern; see the `<system-reminder>` after every `ExitPlanMode` approval.)
   - **Reject** → nothing executes. No branch, no team, no issue assignments. The plan file remains for the user's reference.

If the orchestrator session is **NOT in plan mode** when the command is invoked, continue with the existing step 5 sanity-check (`AskUserQuestion` for composition + models + branch name). Gate 0 only fires in plan mode; otherwise the existing step 5 plays the same role.

**Important — tool-level prompts are NOT auto-allowed.** Gate 0 (and Gate R-A in step 9.5) govern STRUCTURAL approval only. Individual tool calls — `Bash(dotnet build *)`, `Bash(git push *)`, `TeamCreate`, `Agent(...)`, etc. — continue to surface per the user's `settings.json` and harness defaults. The user has chosen to keep handling those prompts manually; the gates do not bypass them.

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
  contain agent-spawn count in the IT phase (~24 agents at N=6).
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
| `NOTES_FILE` | `.team-notes/<foo>-extensions-spec.md` (kebab-case) |
| `TEAM_NAME` | `<foo>-extensions-impl` |
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
  to that file's method list. Record `(complexity, per_file_per_role_model_picks)`.

If the batch becomes empty after pruning, abort cleanly.

### 3.5. Batch-wide model rollup (one model per role for the whole team)

Each role is spawned ONCE for the whole batch, so the orchestrator picks ONE
model per role using the **worst-case complexity across the batch**:

- If any file in the batch is graded `complex` → that role's complex-tier model.
- Else if any file is graded `standard` → that role's standard-tier model.
- Else (all `trivial`) → that role's trivial-tier model.

Rationale: the per-role model is fixed at spawn time and applies uniformly to
every file the agent edits in that run. Picking a tier weaker than the worst
file would under-equip the agent on that file. The user can still override
"all Sonnet" / "all Opus" / "Custom" at the step-5 sanity check.

Per-role asymmetry across roles is still encouraged (e.g. trivial impl across
all files but a heavy regression-sweep load → Opus tester).

Record the rolled-up picks as
`(batch_researcher_model, batch_implementer_model, batch_tester_model, batch_reviewer_model)`.

### 4. Pre-flight git checks

- `git status --porcelain` must be empty. Refuse to proceed otherwise — the user
  has unstaged work that would be entangled with the batch.
- `git fetch origin development` to ensure the base branch is up-to-date locally.

### 5. Sanity check with the user

Use `AskUserQuestion` to present:

- The final batch composition (files + issues + per-file complexity grade for
  transparency).
- The **batch-wide rolled-up picks** from step 3.5:
  `researcher=<X>, implementer=<X>, tester=<X>, reviewer=<X>`.
- The proposed branch name (see step 6).
- Questions:
  1. **Proceed with this batch composition?** (Yes / No / drop specific files)
  2. **Use the rolled-up batch-wide model selection?** (Yes / override "all
     Sonnet" / "all Opus" / custom — Custom lets the user override individual
     roles)

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

Create locally **and immediately publish to `origin` with upstream tracking**:
```bash
git switch -c <branch-name> origin/development
git push -u origin <branch-name>
```

The push lifts the branch onto the remote at the same commit as
`origin/development` (no diff yet — that comes after the batch's edits + the
user's commit). Setting upstream now means:
- The user's eventual `git push` after committing needs no flags.
- A pull request can be opened via the GitHub UI or `gh pr create` as soon as
  the user pushes their first commit, without an additional `git push -u`
  step.

If the `git push -u` fails (network, auth, branch-protection refusing empty
pushes), log the failure but **continue with the batch**. The implementation
work is the main goal; the branch will still exist locally and the user can
re-push manually at the end. Surface the failure clearly in the final summary.

Refuse if the branch already exists locally OR on origin (`git ls-remote
--exit-code origin <branch>`) — ask the user to pick a different batch or delete
the stale branch.

### 7. Assign every issue to `@me`

```bash
for num in <each-issue-number>; do
    gh issue edit "$num" --repo STARIONGROUP/SysML2.NET --add-assignee @me
done
```

Idempotent — re-assigning is a no-op on `gh`. Report success/failure per issue;
on failure, log and continue (an unassignable issue is not a blocker for the
implementation itself).

### 8. Create the named team (one-shot, before any Phase)

```
TeamCreate({team_name: "batch-extensions-impl-<branch-suffix>"})
```

where `<branch-suffix>` is the same dashed-issue-number string used in the
branch name from step 6 (deduplicates across concurrent batches). The team
hosts the four named teammates spawned in the following phases.

Set `{{ORCHESTRATOR_NAME}}` = your own orchestrator identifier (the value
named teammates SendMessage back to). The named teammates remain addressable
for the rest of the run; iterative fixes route via `SendMessage to: "<name>"`,
not via fresh `Agent` spawns.

### 9. Phase R — Spawn the single researcher

ONE `Agent(...)` call:

- `name: "researcher"`
- `subagent_type: "general-purpose"`
- `team_name`: the team from step 8.
- `model`: `<batch_researcher_model>` from step 3.5 (or user override from step 5).
- Foreground (no `run_in_background`).
- Prompt: the **batch-mode researcher prompt** from
  `.claude/team-templates/extension-impl.md` — i.e. the single-file researcher
  body PLUS the "Batch-mode operation" addendum, with `{{BATCH_FILES}}`
  expanded to the numbered list of (interface, paths, method-list) tuples,
  one per file in the batch.

Wait for the researcher's `spec ready` SendMessage. Then **read each `.team-notes/<foo>-extensions-spec.md`** yourself to verify coverage, spec-text-only flags, and stub-blocker flags. Proceed to step 9.5 (Phase R-A approval gate) — DO NOT spawn implementer + tester directly.

The researcher agent **stays addressable** for the rest of the run — the implementer / tester / reviewer may need a clarification on one file's OCL later, which the orchestrator can route via `SendMessage to: "researcher"`. The researcher is also the recipient of step 9.5's "Abort — research again with feedback" branch.

### 9.5. Phase R-A — Researcher-plan approval gate (MANDATORY, every run)

Before spawning implementer + tester, the orchestrator MUST render an inline spec preview and ask for explicit approval. This is non-skippable, even when the researcher reports zero ambiguities / zero spec-text-only flags / zero stub-blockers. It is the only chance the user gets to inspect the per-method derivation plan before any code is written to disk.

1. **Render** an inline preview in the chat response, one block per file. For each file, pull from its `.team-notes/<foo>-extensions-spec.md`:
   - File path + GitHub issue number (link to issue if practical).
   - Per method:
     - Signature line (`internal static <ReturnType> Compute<Name>(this I<Foo> <subjectParam>)`).
     - Derivation source tag: `OCL in XMI`, `OCL in <remarks>`, or `spec-text only`.
     - The suggested C# code block from the notes — single fenced block, cap ≤ 8 lines.
     - Dependencies summary (sibling derived properties used; upstream stubs hit).
     - Stub-blocker flag (if any) — signals that the populated case will need the stub-blocker test pattern.
   - Use a compact Markdown format. Cap total preview at ~80 lines per file to stay scannable. If a notes file's suggested code is unusually long, link the notes-file path and quote only the first ~5 lines.

2. **Ask** via `AskUserQuestion` (single question, 3 options):
   - **Approve — spawn implementer + tester now** *(Recommended)*. Proceeds to step 10.
   - **Drop specific files from the batch**. User picks which file(s) to exclude; orchestrator regenerates the preview against the remaining files and re-asks. Drop-and-continue does NOT recreate the branch (already pushed) and does NOT unassign the dropped issues (idempotent enough; surface this in the final summary).
   - **Abort — research again with feedback**. Orchestrator forwards the user's free-form `Other` text to the still-addressable `researcher` via `SendMessage`, waits for a fresh `spec ready`, then re-runs step 9.5.

3. **On Approve**, proceed to step 10. Do NOT re-ask until the run completes.

4. **On Abort (user rejects without requesting re-research)**, stop the orchestration. Branch + issue assignments remain. Notes files remain. The user can revert manually via git if desired.

Rationale: the previous "surface ambiguities" instruction was conditional and got skipped on clean researcher returns. This gate runs *every time*, so the user always sees the researcher's contract before any code is committed to disk. The gate governs STRUCTURAL approval only — individual tool-permission prompts that the user's `settings.json` requires (e.g. `Bash(dotnet build *)`) continue to surface during Phase IT / V / S / RV / IS.

### 10. Phase IT — Spawn the single implementer + single tester in parallel

**One orchestrator message containing TWO `Agent(...)` calls**, both
foreground:

1. Implementer:
   - `name: "implementer"`, `team_name`: same team.
   - `model`: `<batch_implementer_model>`.
   - Prompt: batch-mode implementer prompt with `{{BATCH_FILES}}` expanded.
2. Tester:
   - `name: "tester"`, `team_name`: same team.
   - `model`: `<batch_tester_model>`.
   - Prompt: batch-mode tester prompt with `{{BATCH_FILES}}` expanded AND
     the **parallel-mode caveat** clearly stated (`dotnet build` only;
     MUST NOT run `dotnet test`).

The two agents edit disjoint files (N production files vs. N test fixtures),
so concurrent edits are safe.

Wait for both `dev complete` and `tests complete` SendMessages.

### 11. Phase V — Orchestrator verification (sequential)

Run sequentially in the orchestrator turn:

1. **One build of production**:
   ```bash
   dotnet build SysML2.NET/SysML2.NET.csproj --nologo --verbosity quiet
   ```
   On failure, attribute the error to its source file(s) by reading the
   compiler output, then:
   ```
   SendMessage({to: "implementer", message: "Build failed; fix in place.
   Files + errors: …"})
   ```
   The same implementer agent retains its context and patches the diffs.
   Iterate until the build is green.
2. **One consolidated targeted test run**, OR-joining every fixture in the
   batch:
   ```bash
   dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj \
       --filter "FullyQualifiedName~<Foo1>ExtensionsTestFixture|FullyQualifiedName~<Foo2>ExtensionsTestFixture|..." \
       --nologo --verbosity quiet
   ```
   For each failure, attribute it and route via `SendMessage`:
   - **OCL mistranslation in production** →
     `SendMessage({to: "implementer", message: "(file, method,
     observed-vs-expected). Fix in place."})`
   - **Wrong test assertion** →
     `SendMessage({to: "tester", message: "(file, method,
     observed-vs-expected). Fix in place."})`
   Both agents keep their context; do NOT spawn fresh per-fix Agent calls.
   Iterate until 0 failures across the batch.

### 12. Phase S — Consolidated regression sweep

```bash
dotnet test SysML2.NET.sln --no-build --nologo --verbosity quiet
```

For each `Expected: <NotSupportedException> But was: no exception` failure,
identify which file in the batch unblocked it (grep the failing test for `For
Later: depends on …` references; or trace by the targeted stub's signature).

Send the consolidated brief to the still-running tester:

```
SendMessage({to: "tester", message: "Regression sweep brief. For each
sibling fixture below, expand-don't-replace per the four-axis checklist
(filter discrimination + predicate completeness + owned vs inherited +
null-projection guard). Sibling fixtures and their failing tests:
- SysML2.NET.Tests/Extend/<Sibling1>ExtensionsTestFixture.cs:
    failing tests: [...], exercising production OCL: ..."})
```

The tester's ACL extends to those sibling fixtures for this dispatch only.

Iterate until the full solution test run is 0 failures.

### 13. Phase RV — Spawn the single reviewer

ONE `Agent(...)` call:

- `name: "reviewer"`, `team_name`: same team, `model: <batch_reviewer_model>`.
- Prompt: batch-mode reviewer prompt with `{{BATCH_FILES}}` expanded.

The reviewer walks each file's `(notes, production, tests + any
regression-swept sibling fixtures touched by this batch)` triple and returns
one `OK` or `NEEDS FIX` verdict with per-file findings grouped.

For each NEEDS-FIX finding:
- Production-code finding →
  `SendMessage({to: "implementer", message: "(file, line, concern). Fix."})`
- Test finding →
  `SendMessage({to: "tester", message: "(file, line, concern). Fix."})`

Re-run Phase V's build + consolidated targeted test after fixes, then
re-dispatch the reviewer if its prior verdict was NEEDS FIX:
`SendMessage({to: "reviewer", message: "Findings actioned; please
re-verify."})`. The reviewer keeps its context.

### 14. Phase IS — Issue checklist sync (sequential, looped)

For each `(file, issue_number)` pair in the batch, run the **identical**
step-11 logic from `/implement-extensions`:

1. Fetch issue body (`gh issue view <num> --json body -q .body`).
2. Locate `### Checklist` section.
3. Enumerate `Compute*` methods in the production file. Tick the ones whose
   bodies no longer throw `NotSupportedException` AND whose `Verify{Method}`
   passed in step 11's last `dotnet test` run.
4. Append any new methods (signature not present in existing checklist) in
   declaration order.
5. Stitch new body (touch ONLY the Checklist section).
6. Push via `gh issue edit <num> --body-file <tmp-body-file>`.
7. Re-fetch + diff to verify only the Checklist section changed.

### 15. Final summary

Print to the user:

- **Branch**: name + base ref + remote-tracking state (`pushed to origin` /
  `local only — push failed at step 6, push manually with: git push -u origin <branch>`)
  + how to delete-if-aborting (locally: `git branch -D <branch>`; remotely if
  pushed: `git push origin --delete <branch>`).
- **Per-file table**:

  | File | Stubs impl. | Targeted tests | Reg. sweep impact | Reviewer | Issue |
  |---|---|---|---|---|---|
  | `<Foo1>Extensions.cs` | X/X | X/X green | N siblings expanded | OK | #<n> ticked X/X |
  | `<Foo2>Extensions.cs` | … | … | … | … | … |

- **Branch-wide totals**:
  - Files modified (sum of production + tests + notes).
  - Full solution test count (e.g. `1082/1082`).
  - Unresolved reviewer findings (if any).
  - Spec-text-only methods flagged separately (grounded in spec prose, not OCL).
  - Out-of-scope blockers surfaced (e.g. "VerifyComputeX in <Sibling>TestFixture
    is still stub-blocked on `<UpstreamMethod>` — consider a follow-up issue").

- **Reminder**: nothing is auto-committed. The branch exists locally (and on
  `origin` with upstream tracking, when step 6's push succeeded). User reviews
  `git diff`, commits, then `git push` (no flags needed — tracking is already
  set) and opens the PR via `gh pr create --base development --head <branch>`
  or the GitHub UI.

## Failure handling

| Failure | When | Disposition |
|---|---|---|
| Missing input file | Step 1 | Abort, no state change. |
| File has 0 stubs | Step 3 | Drop from batch, inform user; abort if batch becomes empty. |
| Ambiguous issue | Step 2 | `AskUserQuestion` for an explicit issue number per file. |
| Dirty working tree | Step 4 | Abort, ask user to commit/stash. |
| Branch already exists | Step 6 | Abort; ask user to pick a different batch or delete the stale branch. |
| `git push -u origin <branch>` fails (network, auth, branch protection) | Step 6 | Log + continue (non-blocking; surface clearly in step 15 final summary with a manual re-push command). |
| `gh issue edit --add-assignee` fails for one issue | Step 7 | Log + continue (non-blocking; implementation still proceeds). |
| `TeamCreate` fails | Step 8 | Abort with a clear error; the named-teammate flow requires a live team. Do NOT silently fall back to per-file `Agent` calls — that defeats the cost-reduction goal of this command. |
| Production build fails after implementer | Step 11.1 | Attribute to the source file(s); `SendMessage to: "implementer"` with the failing file + error. Same agent, no re-spawn. |
| Targeted test fails | Step 11.2 | Attribute (OCL vs test bug); `SendMessage to: "implementer"` or `to: "tester"` with the `(file, method, observed-vs-expected)` triple. Same agent, no re-spawn. |
| Sibling test failure in regression sweep | Step 12 | `SendMessage to: "tester"` with the consolidated regression brief (all touched sibling fixtures in one dispatch). Tester's ACL extends to those fixtures for this dispatch only. |
| Reviewer NEEDS FIX | Step 13 | `SendMessage to: "implementer"` or `to: "tester"` with the per-file finding; then `SendMessage to: "reviewer"` to re-verify. Same agents throughout. |
| Named teammate becomes unresponsive (timed-out SendMessage) | Phases IT / V / S / RV | Fall back to a fresh `Agent(...)` spawn for that role only, replaying the spawn prompt PLUS the most recent context the orchestrator has (notes file paths, prior deviation reports). The team-name stays alive for the other roles. |
| Sub-agent deadlocked on harness UI permission prompt the user cannot action | Phases IT / V / S / RV | Orchestrator can take over the deadlocked sub-agent's remaining work directly (build / test / gh issue edit run from the orchestrator's own permission scope). Send `shutdown_request` to the parked sub-agent. Surface to the user clearly in the final summary. (Precedent: 2026-05-28 batch run for issues #111/#116/#118/#119/#130/#132 — implementer + tester parked on `dotnet build` prompts; orchestrator completed Phases V → S → RV → IS itself.) |
| One file's implementation fails after branch + assignment | Any step ≥ 6 | Keep the branch; surface in final summary; user decides whether to retry via `/implement-extensions` for that single file or revert. |
| User rejects plan at Gate 0 (Pre-flight, plan-mode invocation) | Pre-flight | Stop. No branch, no team, no issue assignments. Plan file remains for reference. |
| User picks "Abort — research again with feedback" at Gate R-A (Phase R-A) | Step 9.5 | Forward the user's free-form text to the still-addressable `researcher` via `SendMessage`. Wait for a fresh `spec ready`. Re-run step 9.5. |
| User picks "Abort" outright at Gate R-A | Step 9.5 | Stop orchestration. Branch + issue assignments remain (user reverts via git / `gh issue edit --remove-assignee` if desired). Researcher's `.team-notes/*.md` files remain for later reference. |
| User picks "Drop specific files" at Gate R-A | Step 9.5 | Recompute the batch composition against the reduced set. Regenerate the inline preview. Re-ask via `AskUserQuestion`. Branch name + issue assignments are NOT recomputed (already pushed / assigned); surface the mismatch in the final summary. |

## Parallelism caps (orchestrator self-enforced)

- N ≤ 6 files per batch (unchanged — bounds the per-agent context size, not
  the live-agent count).
- Phase R: **1** agent (the researcher).
- **Phase R-A (step 9.5): no agents in flight.** The user-facing AskUserQuestion gate blocks all sub-agent spawning until the user approves. The researcher is idle but addressable for re-research dispatches.
- Phase IT: **2** agents in parallel (implementer ∥ tester), via one
  orchestrator message containing two `Agent(...)` calls.
- Phase RV: **1** agent (the reviewer).
- Regression sweep dispatch (step 12): handled inside the still-running
  tester via `SendMessage`. No extra spawn.
- **Total live-agent count: 4**, regardless of batch size.

## Notes for the orchestrator (you, the main agent)

- The team-template role prompts at `.claude/team-templates/extension-impl.md`
  are the **source of truth**. The batch-mode addendum inside each role's
  prompt block is what activates one-team-for-all-files behaviour — it is
  the single-file prompt PLUS the "Batch-mode operation" section, with
  `{{BATCH_FILES}}` expanded.
- All paths in agent prompts must be repo-relative with forward slashes.
- Researcher is **mandatory** for the batch, even when individual files have
  been seen before via `/implement-extensions`. One researcher produces all
  N notes files; the implementer/tester/reviewer read them.
- Reviewer is **mandatory** for the batch — cheap insurance against subtle
  OCL mistranslation across all N files.
- The branch and the assignments persist even on partial failure. Be explicit
  in the final summary about which files succeeded vs which need follow-up.
- Do NOT auto-commit. The user reviews `git diff` and commits manually.
- If the user supplies a single file, route them to `/implement-extensions`
  with the same filename rather than creating a degenerate 1-file "batch"
  team.
- **Prefer SendMessage over fresh Agent spawns inside the iteration loop.**
  The named teammates retain their context (notes already read, prior
  decisions, OCL chains they've already traced). A fresh `Agent(...)` call
  for a single-file fix throws that context away and re-pays the prompt
  cost. The only reason to spawn fresh is the "named teammate becomes
  unresponsive" row in the failure-handling table.
- **Plan mode is handled by Gate 0 at the top of this file** — the orchestrator writes the proposed-execution plan to the plan file, calls `ExitPlanMode`, and proceeds on approval. The orchestrator never spawns sub-agents while plan mode is active, so the previous "degraded mode" workaround is no longer needed and has been removed. If for some reason plan mode re-engages mid-run (e.g. the user toggles it back on after Gate 0 approval), the orchestrator pauses any pending sub-agent spawns and surfaces the state to the user.
- **Gate R-A (step 9.5) is mandatory every run.** It is the only structural checkpoint between the researcher returning `spec ready` and the implementer + tester being spawned. Do not skip it even when the researcher reports zero ambiguities — the user explicitly asked for an unconditional gate so they can review per-method derivations before code is written.
- **Tool-level prompts (`Bash(...)`, `Edit(...)`, `Write(...)`, `TeamCreate`, `Agent(...)`) still surface per the user's `settings.json`.** Gate 0 and Gate R-A govern STRUCTURAL approval only. The user has explicitly chosen to keep handling tool-level prompts manually rather than auto-allowing them via permission rules or hooks.
- **Sub-agent-side deadlocks**: if an implementer / tester gets parked on a harness UI permission prompt the user cannot action (precedent: 2026-05-28 batch run for issues #111/#116/#118/#119/#130/#132), the orchestrator should take over the deadlocked work directly from its own permission scope (running `dotnet build` / `dotnet test` / `gh issue edit` itself). Send `shutdown_request` to the parked sub-agent. Surface clearly in the final summary.
