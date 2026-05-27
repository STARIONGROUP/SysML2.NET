---
description: Spawn the 4-role team across N SysML2.NET Extend files in one run — creates and pushes a batch branch, assigns the related GitHub issues to the user, and updates each issue's checklist on completion
argument-hint: <file1.cs> <file2.cs> [<file3.cs> ...]   (2–6 Extension file names; each will be normalised to SysML2.NET/Extend/<Foo>Extensions.cs)
---

# /implement-extensions-batch

Apply the **existing `/implement-extensions` 4-role team workflow** across N
files (`$ARGUMENTS`) in one run. The team itself is unchanged — researcher,
implementer, tester, reviewer per file. What this command adds on top of the
single-file flow:

1. **Pre-flight validation** of every file + its GitHub issue, before any state
   change.
2. **Creates a new git branch** off `development` with a deterministic name
   derived from the batch's issue numbers, AND pushes it to `origin` with
   upstream tracking set so the user can immediately open a pull request after
   committing.
3. **Assigns every related GitHub issue to the invoking user** (`@me`).
4. **Parallelises agent spawns across files** wherever their target files are
   disjoint.
5. **Single consolidated regression sweep** instead of one per file.
6. **Loops the issue-checklist sync** per file at the end.

The team template (role prompts) at `.claude/team-templates/extension-impl.md`
(v2, repo-tracked) is the source of truth for the per-file behaviour. This
command body is the batch orchestration glue.

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
  regression sweep, see step 10).

**MUST NOT modify** the same things the single-file command refuses to touch:
other production files in `SysML2.NET/Extend/` or `SysML2.NET/Core/`,
auto-generated POCOs / interfaces, code-generator templates.

`feedback_scope_discipline.md` applies just as in `/implement-extensions`. Use the
stub-blocker test pattern (see template) when an in-scope test would otherwise
need to traverse a still-stubbed upstream method that is NOT part of the current
batch.

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
  to that file's method list. Record `(complexity, per_role_model_picks)`.

If the batch becomes empty after pruning, abort cleanly.

### 4. Pre-flight git checks

- `git status --porcelain` must be empty. Refuse to proceed otherwise — the user
  has unstaged work that would be entangled with the batch.
- `git fetch origin development` to ensure the base branch is up-to-date locally.

### 5. Sanity check with the user

Use `AskUserQuestion` to present:

- The final batch composition (files + issues + complexity + per-role model
  picks per file).
- The proposed branch name (see step 6).
- Questions:
  1. **Proceed with this batch composition?** (Yes / No / drop specific files)
  2. **Use the per-file dynamic model selection?** (Yes / override "all Sonnet"
     / "all Opus" / custom)

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

### 8. Phase R — Spawn researchers in parallel

**One orchestrator message containing N `Agent(...)` calls** (one per file). Each:

- `subagent_type: "general-purpose"`
- `model: <researcher_model>` per the per-file step-3 grade (Haiku trivial /
  Sonnet standard / Opus complex, or user override from step 5).
- Foreground (no `run_in_background`).
- Prompt: the v2 researcher prompt from `.claude/team-templates/extension-impl.md`
  with that file's `{{PLACEHOLDERS}}` substituted + the file's method list.

After all N return, **read each notes file** yourself to verify coverage +
spec-text-only flags + stub-blocker flags.

### 9. Phase IT — Spawn implementers + testers in parallel

**One orchestrator message containing 2N `Agent(...)` calls** — one implementer
+ one tester per file. All foreground.

Each implementer prompt is the v2 implementer prompt with the file's placeholders
+ the **parallel-mode caveat** clearly stated (see template). Each tester prompt
is the v2 tester prompt with the same caveat — they MUST run `dotnet build` only
and MUST NOT run `dotnet test` (production lacks parallel-turn edits in their
disk view).

### 10. Phase V — Orchestrator verification (sequential)

After all 2N agents return, run sequentially in the orchestrator turn:

1. **One build of production**:
   ```bash
   dotnet build SysML2.NET/SysML2.NET.csproj --nologo --verbosity quiet
   ```
   On failure, identify which file's production diff caused it, re-dispatch
   that file's implementer. Iterate.
2. **One consolidated targeted test run**, OR-joining every fixture in the
   batch:
   ```bash
   dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj \
       --filter "FullyQualifiedName~<Foo1>ExtensionsTestFixture|FullyQualifiedName~<Foo2>ExtensionsTestFixture|..." \
       --nologo --verbosity quiet
   ```
   For each failure, attribute it to the correct file:
   - OCL mistranslation in production → re-dispatch THAT file's implementer.
   - Wrong test assertion → re-dispatch THAT file's tester.
   Iterate until 0 failures across the batch.

### 11. Phase S — Consolidated regression sweep

```bash
dotnet test SysML2.NET.sln --no-build --nologo --verbosity quiet
```

For each `Expected: <NotSupportedException> But was: no exception` failure,
identify which file in the batch unblocked it (grep the failing test for `For
Later: depends on …` references; or trace by the targeted stub's signature).

Dispatch regression-sweep testers (Sonnet by default) per touched sibling
fixture, in parallel when the sibling fixtures are disjoint. Use the
**expand-don't-replace** brief from `/implement-extensions` step 8 (filter
discrimination + predicate completeness + owned vs inherited + null-projection
guard).

Iterate until the full solution test run is 0 failures.

### 12. Phase RV — Reviewers in parallel

**One orchestrator message containing N `Agent(...)` calls** — one reviewer per
file. Each scoped to ONE file's `(notes file, production file, test fixture,
regression-swept sibling tests that this file's implementation touched)`.

For each "NEEDS FIX" verdict, dispatch the implementer or tester for that file
back. Other files' results are still reported in the final summary.

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

### 14. Final summary

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
| `git push -u origin <branch>` fails (network, auth, branch protection) | Step 6 | Log + continue (non-blocking; surface clearly in step 14 final summary with a manual re-push command). |
| `gh issue edit --add-assignee` fails for one issue | Step 7 | Log + continue (non-blocking; implementation still proceeds). |
| Production build fails after implementer | Step 10.1 | Attribute to the file, re-dispatch that implementer. |
| Targeted test fails | Step 10.2 | Attribute (OCL vs test bug), re-dispatch correct role. |
| Sibling test failure in regression sweep | Step 11 | Dispatch a regression-sweep tester per touched fixture; in scope. |
| Reviewer NEEDS FIX | Step 12 | Re-dispatch implementer or tester for that file only; other files' results still reported. |
| One file's implementation fails after branch + assignment | Any step ≥ 6 | Keep the branch; surface in final summary; user decides whether to retry via `/implement-extensions` for that single file or revert. |

## Parallelism caps (orchestrator self-enforced)

- N ≤ 6 files per batch.
- Phase R: N parallel agents.
- Phase IT: 2N parallel agents (max 12 at N=6).
- Phase RV: N parallel agents.
- Regression sweep dispatch (step 11): batch parallel by touched fixture
  filename; if more than 6 fixtures need expansion, serialise above 6.

## Notes for the orchestrator (you, the main agent)

- The team-template role prompts at `.claude/team-templates/extension-impl.md`
  are the **source of truth** for per-file behaviour. Substitute the
  file-specific placeholders fresh for each agent spawn; do not let prompts
  leak across files.
- All paths in agent prompts must be repo-relative with forward slashes (per
  the convention used throughout the existing single-file command).
- Researcher is **mandatory** per file, even when the file has been seen before
  via `/implement-extensions`. Researchers are cheap and produce the contract
  the implementer/tester/reviewer read.
- Reviewers are **mandatory** per file — cheap insurance against subtle OCL
  mistranslation. Even when the file is trivial spec-text-only.
- The branch and the assignments persist even on partial failure. Be explicit
  in the final summary about which files succeeded vs which need follow-up.
- Do NOT auto-commit. The user reviews `git diff` and commits manually.
- If the user supplies a single file, route them to `/implement-extensions`
  with the same filename rather than creating a degenerate 1-file "batch"
  branch.
