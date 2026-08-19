---
description: Evaluate whether a change touches the SQL-schema documentation set (SQLSCHEMA.md + both SQLSCHEMA-GUIDE docs + glossaries) and bring every affected document back in sync. Invoke after ANY change to the SQL schema artifacts, the SQL code generator, the PIM DTOs/serializers, or the Extend derived-property layer — or run it standalone as a consistency audit.
argument-hint: [optional: short description of what changed, or "audit" for a full consistency check]
---

# /sync-schema-guides

Keep the SQL-schema documentation set consistent with the code and with itself. The set:

| Document | Role |
|---|---|
| `SysML2.NET.CodeGenerator/SQLSCHEMA.md` | Compact reference: artifact table, audit findings, measured numbers, verification recipe, guide section map |
| `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md` | Long-form architectural guide (English) — 19 sections + glossary appendix (§19) |
| `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.nl.md` | Dutch translation of the guide — MUST stay structurally identical (same sections, same anchors pattern, same tables) |
| `SysML2.NET.CodeGenerator/IMPACT-RADIUS.md` | Design sketch for the impact-radius engine (guide obligation §15.1). English-only. Update when propagation kinds, the `derived_dependency` catalog concept, closure indexes, or the §15/§18 obligations it cites change. |

**Language policy for the Dutch guide:** Dutch prose, English conceptual terms (derived
properties, stored state, fold, checkpoint, overlay, impact radius, census, …). Never
translate the conceptual vocabulary — it must match the English guide, the schema comments,
and the code.

## 1. Determine what changed (skip if invoked with an explicit change description)

```
git status --porcelain
git diff --stat HEAD
```

The documentation set is AFFECTED when a change touches any of:

- `SysML2.NET.CodeGenerator/Sql/**` (golden schema, generated schema, smoke test)
- `SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs`
- `SysML2.NET.CodeGenerator/HandleBarHelpers/SqlSchemaHelpers.cs`
- `SysML2.NET.CodeGenerator/Extensions/SqlSchemaExtensions.cs`
- `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/SQLSchemaGenerator.cs`
- `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/ClassKindRegistry.cs` (and the
  `ClassKindRegistration` / `ModelVersionRegistration` records) — the append-only registry
  freezing class_kind ids and model_version ordinals; any append/close here changes the
  multi-version narrative (guide §6.4/§12.1) and the seed counts
- `SysML2.NET/PIM/**` and `SysML2.NET.Serializer.Json/PIM/**` (the PIM the schema §3 models)
- `SysML2.NET/Extend/**` (only when it changes WHICH derived properties exist or their
  semantics — the guides' census numbers and impact-radius narrative depend on them)
- `Resources/KerML_only_xmi.uml` / `Resources/SysML_only_xmi.uml` (metamodel change ⇒ census
  numbers, table counts, catalog counts all suspect)

If nothing relevant changed and the argument is not "audit": report "no sync needed" and stop.

## 2. Sync procedure (English guide is the master)

1. **Update the English guide first**, then mirror into the Dutch guide, then update
   SQLSCHEMA.md's compact statements and its guide section map. Never let the three diverge.
2. **Numbers are load-bearing — verify, don't trust.** These recur across all three documents
   and MUST match reality after any schema/generator change:
   - smoke-test assertion count (`grep -c "RAISE NOTICE 'PASS" SysML2.NET.CodeGenerator/Sql/schema.smoke.sql`)
   - subtype-table count (currently 47), link-table count (7), enum count (7), view count (167),
     class_kind rows (175, ids frozen by ClassKindRegistry), model_version rows (currently 1),
     reference-validation sources in the two-tier validate functions (currently 42),
     partitioned-table count × modulus
   - measured performance numbers: only replace with NEW measurements, never extrapolate
     silently — label extrapolations as such
3. **Section integrity:** the guides' section numbers are referenced from SQLSCHEMA.md's
   section map and from within the guides themselves. When adding a section, prefer appending
   subsections (x.y) over renumbering; if renumbering is unavoidable, grep all three documents
   for stale `section N` / `§N` / anchor references and fix the TOCs.
4. **Glossary appendix (§19 in both guides):** for every new term of art introduced by the
   change, add a row — shortest clear definition + guide-section reference — to BOTH
   glossaries, alphabetically placed. When a section is renumbered, re-verify every reference
   in the *See*/*Zie* columns.
5. **Schema-file § banners vs guide sections:** the schema files' `§N` banners are a separate
   numbering space. If a banner is added/renumbered in `schema.golden.sql`, update the
   template in lockstep (hand-written sections must stay byte-identical) and fix the `(§N)`
   suffixes in the guides' section headings.

## 3. Verification before reporting done

- The three documents agree on every shared number and section reference.
- Both guides have identical section structure (`grep -c "^## " both files` — counts match).
- If the schema itself changed: the generator fixture passes and the smoke test passes
  against both golden and generated schema (see SQLSCHEMA.md → Verification).
- Report a short diff summary per document: what was updated and why.

## Standing rule (also enforced via CLAUDE.md)

Any task that edits the affected paths of step 1 is NOT complete until this evaluation has
run. A change that alters behavior, counts, measured numbers, invariants, or terminology and
leaves the documentation set untouched is a defect, not a shortcut.
