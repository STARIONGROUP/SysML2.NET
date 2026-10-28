---
description: Bump every versioned csproj to a target version and rewrite each project's PackageReleaseNotes; creates branch release-<version>, never commits or pushes
argument-hint: <target-version, e.g. 0.25.0>
---

# /bump-version

Bump the solution to version **$ARGUMENTS** and rewrite release notes.

## Hard rules

- Never `git commit`, `git push`, open a PR, or tag. The user owns all of it.
- Only `*.csproj` files carrying a `<Version>` element are modified. Test projects, the Viewer and `Directory.*.props/targets` are untouched.
- All paths reported are repo-relative.

## Steps

1. **Validate the argument.** `$ARGUMENTS` must match `^\d+\.\d+\.\d+$`; otherwise stop and ask. Call it `NEW`.

2. **Find the previous release.** `git fetch origin --tags`, then `PREV` = the current `<Version>` of `SysML2.NET/SysML2.NET.csproj`. Verify `git rev-parse PREV` resolves to a tag; if not, use `git describe --tags --abbrev=0 origin/development` and say so. Stop if `NEW` is not greater than `PREV`.

3. **Create the branch.** Stop if `git status --porcelain` is non-empty. Then `git switch --no-track -c release-NEW origin/development`. If the branch already exists locally or on origin, stop and surface it — never reset or force. Do NOT push it.

4. **List the versioned projects.** `git grep -l "<Version>PREV</Version>" -- "*.csproj"`. Report any `*.csproj` with a `<Version>` different from `PREV` and stop.

5. **Collect changes per project.** For each project directory `D`:
   - `git log --no-merges --format="%h %s" PREV..HEAD -- D` and `git diff --stat PREV..HEAD -- D`.
   - Read PR/issue titles where the subject names `#<n>` (`gh issue view <n> --json title` / `gh pr view <n> --json title,body`) when the commit subject alone does not say what changed for consumers.
   - Inspect the diff (`git diff PREV..HEAD -- D ':!**/AutoGen*/**'` first, then generated folders only for a summary) to understand public-API impact.
   - Ignore changes with no consumer impact: license/file headers, whitespace, csproj housekeeping, tests, comments.

6. **Rewrite `<PackageReleaseNotes>`** in every project except `SysML2.NET.CodeGenerator` (not packaged; bump its version only). The previous content is **replaced**, not appended to:
   - One line per change, prefixed `[Add]`, `[Update]`, `[Remove]` or `[Fix]`, in that order. Describe the consumer-visible effect in one sentence; append the issue number(s) in parentheses when known, e.g. `(#389)`.
   - XML-escape `<`, `>`, `&` (`PagedResult&lt;T&gt;`).
   - A dependent project with no consumer-visible change of its own gets exactly `[Update] Aligned with SysML2.NET NEW`.
   - Keep the file's existing indentation for the element and its lines.

7. **Bump `<Version>PREV</Version>` → `<Version>NEW</Version>`** in every file from step 4. Do not touch any other element.

8. **Verify.** `git grep -n "<Version>PREV</Version>" -- "*.csproj"` returns nothing; `dotnet build SysML2.NET.sln` succeeds.

9. **Report and stop.** Show `git diff --stat`, each project's new release notes, and the handoff:

   > Review `git diff`, stage the csproj files (`git add <path> …` — never `-A` / `.`), commit with `[Update] Version bump to NEW`, then `git push -u origin release-NEW` and open the PR with `gh pr create --base development`.
