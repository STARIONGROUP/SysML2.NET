# SysML2 Schema Atlas

An interactive visualization of the generated PostgreSQL persistence schema, published as a
Claude Code Artifact:

**https://claude.ai/code/artifact/02fcfe37-e5b2-473d-9716-679b2ac34b26**

The page shows all tables with every column, foreign key, CHECK constraint, index, and
trigger; an architecture diagram of the reference mechanism; a FK reference map grouped by
target; and the enum/function/view appendices. All table-level data is parsed directly from
`SysML2.NET.CodeGenerator/Sql/schema2.generated.sql`, so the page is only as fresh as its
last publish.

## Keeping it up to date (part of the `/sync-schema-guides` obligation)

Whenever `SysML2.NET.CodeGenerator/Sql/schema2.generated.sql` changes, the artifact must be
refreshed as part of the same task (the `/sync-schema-guides` skill carries this step):

1. Build the page (parses the schema, splices data + snapshot stamp into the template):

   ```powershell
   .claude/schema-atlas/build-schema-atlas.ps1            # writes %TEMP%\schema-atlas.html
   ```

2. Publish the output file with the Artifact tool, passing the URL above as `url` so the
   existing artifact is updated in place (favicon stays `🐘`).

3. Hard-coded prose numbers: the stats strip and catalog are fully data-driven, but the
   masthead lede, the layer blurbs, and the SVG diagram annotations carry a few literals
   (175 class kinds, 47 subtype / 7 link tables, 58 × 16 = 928 partitions, 2,629 cloned FK
   constraints, 167 views, 3 triggers). The build script prints the freshly parsed counts —
   if any differ from those literals, update `template.html` accordingly before publishing.

## Files

| File | Role |
|---|---|
| `build-schema-atlas.ps1` | Parses `schema2.generated.sql` → JSON, splices into the template, writes the final page to `%TEMP%` |
| `template.html` | The page (styles, diagram, rendering JS) with `__SCHEMA_JSON__` / `__SNAPSHOT__` placeholders |
