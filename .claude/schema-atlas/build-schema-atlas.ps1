# Builds the "SysML2 Schema Atlas" artifact page from the generated SQL schema.
#
# Parses SysML2.NET.CodeGenerator/Sql/schema2.generated.sql into JSON (tables, columns,
# FKs, checks, indexes, triggers, functions, views, enums, partition/seed stats), splices
# it into .claude/schema-atlas/template.html, and writes the finished page to the output
# path (default: %TEMP%\schema-atlas.html). The page is then published as an Artifact —
# see .claude/schema-atlas/README.md for the artifact URL and publish step.
#
# Windows PowerShell 5.1 compatible. No repo files are modified.

param(
    [string]$OutFile = (Join-Path $env:TEMP 'schema-atlas.html')
)

$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
$src = Join-Path $repoRoot 'SysML2.NET.CodeGenerator\Sql\schema2.generated.sql'
$templatePath = Join-Path $PSScriptRoot 'template.html'

$lines = Get-Content $src -Encoding UTF8

# --- section banners ---
$sections = @()
for ($i = 0; $i -lt $lines.Count; $i++) {
    if ($lines[$i] -match '^-- (\d+)\. ([A-Z].*?)\s*(\[GENERATED\]|\[HAND-WRITTEN\])?\s*$') {
        $sections += [pscustomobject]@{ num = [int]$Matches[1]; title = $Matches[2].Trim(); origin = $(if ($Matches[3]) { $Matches[3].Trim('[',']') } else { '' }); line = $i }
    }
}

function Get-Section($lineNo) {
    $best = $null
    foreach ($s in $sections) { if ($s.line -le $lineNo) { $best = $s } }
    return $best
}

# --- enums ---
$enums = @()
foreach ($l in $lines) {
    if ($l -match "^CREATE TYPE sysml2\.(\w+) AS ENUM \((.+)\);") {
        $vals = ($Matches[2] -split ',\s*') | ForEach-Object { $_.Trim(" '") }
        $enums += [pscustomobject]@{ name = $Matches[1]; values = $vals }
    }
}

# --- tables ---
$tables = @()
for ($i = 0; $i -lt $lines.Count; $i++) {
    if ($lines[$i] -notmatch '^CREATE TABLE sysml2\.(\w+) \($') { continue }
    $tname = $Matches[1]
    $startLine = $i

    # preceding contiguous comment block
    $cmt = @()
    $j = $i - 1
    while ($j -ge 0 -and $lines[$j] -match '^--( |$)') { $cmt = ,($lines[$j] -replace '^--\s?', '') + $cmt; $j-- }

    $cols = @(); $pk = ''; $fks = @(); $uniques = @(); $checks = @(); $partition = ''
    $pendingCmt = @()
    $i++
    while ($i -lt $lines.Count) {
        $l = $lines[$i]
        if ($l -match '^\)\s*(PARTITION BY .+?)?;') { if ($Matches[1]) { $partition = $Matches[1] }; break }
        $t = $l.Trim().TrimEnd(',')
        if ($t -eq '') { $i++; continue }
        if ($t -match '^--\s?(.*)$') { $pendingCmt += $Matches[1]; $i++; continue }

        $inline = ''
        if ($t -match '^(.*?)\s+--\s?(.*)$' -and $t -notmatch "'.*--.*'") { $t = $Matches[1].Trim(); $inline = $Matches[2] }

        if ($t -match '^FOREIGN KEY \((.+?)\)$') {
            # REFERENCES clause continues on the next line
            $i++
            $t = ($t + ' ' + $lines[$i].Trim().TrimEnd(','))
        }

        if ($t -match '^PRIMARY KEY \((.+)\)$') { $pk = $Matches[1] }
        elseif ($t -match '^FOREIGN KEY \((.+?)\) REFERENCES sysml2\.(\w+) \((.+?)\)\s*(ON DELETE \w+( \w+)?)?$') {
            $fks += [pscustomobject]@{ cols = $Matches[1]; refTable = $Matches[2]; refCols = $Matches[3]; onDelete = $(if ($Matches[4]) { $Matches[4] } else { '' }) }
        }
        elseif ($t -match '^UNIQUE \((.+)\)$') { $uniques += $Matches[1] }
        elseif ($t -match '^CONSTRAINT (\w+)$') {
            # multi-line named CHECK: accumulate following lines until parens balance
            $chkName = $Matches[1]; $body = ''
            do {
                $i++
                $body += ' ' + $lines[$i].Trim().TrimEnd(',')
                $open = ($body.ToCharArray() | Where-Object { $_ -eq '(' }).Count
                $close = ($body.ToCharArray() | Where-Object { $_ -eq ')' }).Count
            } while ($open -ne $close -and $i -lt $lines.Count)
            $checks += [pscustomobject]@{ name = $chkName; body = ($body.Trim() -replace '^CHECK\s*', '') }
        }
        elseif ($t -match '^(CONSTRAINT (\w+) )?CHECK\s*(.+)$') {
            $checks += [pscustomobject]@{ name = $(if ($Matches[2]) { $Matches[2] } else { '' }); body = $Matches[3] }
        }
        elseif ($t -match '^(\w+)\s+([\w\.]+)\s*(.*)$') {
            $cname = $Matches[1]; $ctype = $Matches[2]; $rest = $Matches[3]
            $notnull = $rest -match 'NOT NULL'
            $default = ''; if ($rest -match 'DEFAULT (.+?)( NOT NULL| NULL| REFERENCES|$)') { $default = $Matches[1].Trim() }
            $ref = $null
            if ($rest -match 'REFERENCES sysml2\.(\w+) \((\w+)\)\s*(ON DELETE \w+( \w+)?)?') {
                $ref = [pscustomobject]@{ table = $Matches[1]; col = $Matches[2]; onDelete = $(if ($Matches[3]) { $Matches[3] } else { '' }) }
            }
            $cols += [pscustomobject]@{
                name = $cname; type = $ctype; notnull = $notnull; default = $default; ref = $ref
                comment = (($pendingCmt + $inline) | Where-Object { $_ }) -join ' '
            }
            $pendingCmt = @()
        }
        $i++
    }
    $sec = Get-Section $startLine
    $tables += [pscustomobject]@{
        name = $tname; section = $sec.num; comment = ($cmt -join "`n")
        columns = $cols; pk = $pk; fks = $fks; uniques = $uniques; checks = $checks; partition = $partition
    }
}

# --- indexes (statements may span lines; read until ';') ---
$indexes = @()
for ($i = 0; $i -lt $lines.Count; $i++) {
    if ($lines[$i] -match '^CREATE (UNIQUE )?INDEX (\w+)') {
        $uniq = [bool]$Matches[1]; $iname = $Matches[2]
        $stmt = $lines[$i]
        while ($stmt -notmatch ';\s*$') { $i++; $stmt += ' ' + $lines[$i].Trim() }
        if ($stmt -match 'ON sysml2\.(\w+)\s*(.*);') {
            $indexes += [pscustomobject]@{ name = $iname; unique = $uniq; table = $Matches[1]; def = $Matches[2].Trim() }
        }
    }
}

# --- triggers ---
$triggers = @()
for ($i = 0; $i -lt $lines.Count; $i++) {
    if ($lines[$i] -match '^CREATE TRIGGER (\w+)$') {
        $trg = $Matches[1]; $timing = ''; $tbl = ''; $fn = ''
        for ($k = $i + 1; $k -lt $i + 5; $k++) {
            if ($lines[$k] -match '(BEFORE|AFTER) (INSERT|UPDATE|DELETE)( OR \w+)* ON sysml2\.(\w+)') { $timing = $Matches[0] -replace ' ON sysml2\.\w+', ''; $tbl = $Matches[4] }
            if ($lines[$k] -match 'EXECUTE FUNCTION sysml2\.(\w+)') { $fn = $Matches[1] }
        }
        $triggers += [pscustomobject]@{ name = $trg; table = $tbl; timing = $timing; fn = $fn }
    }
}

# --- functions ---
$functions = @()
for ($i = 0; $i -lt $lines.Count; $i++) {
    if ($lines[$i] -match '^CREATE OR REPLACE FUNCTION sysml2\.(\w+)\(') {
        $fname = $Matches[1]
        $ret = ''
        for ($k = $i; $k -lt $i + 15; $k++) { if ($lines[$k] -match '^\s*RETURNS (.+?)\s*$') { $ret = $Matches[1]; break } }
        $cmt = @(); $j = $i - 1
        while ($j -ge 0 -and $lines[$j] -match '^--( |$)') { $cmt = ,($lines[$j] -replace '^--\s?', '') + $cmt; $j-- }
        $functions += [pscustomobject]@{ name = $fname; returns = $ret; comment = (($cmt | Select-Object -First 4) -join ' ') }
    }
}

# --- views ---
$views = @()
foreach ($l in $lines) { if ($l -match '^CREATE VIEW sysml2\.(\w+) AS$') { $views += $Matches[1] } }

# --- partitions: modulus from the DO loop constant; partitioned tables carry a partition clause ---
$partModulus = 0
foreach ($l in $lines) { if ($l -match 'partition_count\s+constant int := (\d+)') { $partModulus = [int]$Matches[1]; break } }
$partitionedTables = @($tables | Where-Object { $_.partition }).Count

# --- class_kind seed rows ---
$ckText = [System.IO.File]::ReadAllText($src)
$ckBlock = [regex]::Match($ckText, "(?s)INSERT INTO sysml2\.class_kind.*?VALUES(.*?);")
$classKindRows = ([regex]::Matches($ckBlock.Groups[1].Value, "\(\d+, '")).Count
$abstractKinds = ([regex]::Matches($ckBlock.Groups[1].Value, "', true, ")).Count

$result = [pscustomobject]@{
    generatedAt = ''
    sections = $sections | Select-Object num, title, origin
    enums = $enums
    tables = $tables
    indexes = $indexes
    triggers = $triggers
    functions = $functions
    views = $views
    partitions = [pscustomobject]@{ modulus = $partModulus; partitionedTables = $partitionedTables }
    classKindRows = $classKindRows
    abstractKinds = $abstractKinds
}

$json = $result | ConvertTo-Json -Depth 8

# --- splice into template ---
$html = [System.IO.File]::ReadAllText($templatePath)

if (-not $html.Contains('__SCHEMA_JSON__')) { throw 'template.html is missing the __SCHEMA_JSON__ placeholder' }

$branch = (& git -C $repoRoot rev-parse --abbrev-ref HEAD 2>$null)

if (-not $branch) { $branch = 'unknown' }

$snapshot = "branch <code>$branch</code>, $(Get-Date -Format 'yyyy-MM-dd')"
$html = $html.Replace('__SCHEMA_JSON__', $json.Replace('</', '<\/'))
$html = $html.Replace('__SNAPSHOT__', $snapshot)

[System.IO.File]::WriteAllText($OutFile, $html, (New-Object System.Text.UTF8Encoding($false)))

Write-Host "tables=$($tables.Count) enums=$($enums.Count) indexes=$($indexes.Count) triggers=$($triggers.Count) functions=$($functions.Count) views=$($views.Count) partitionedTables=$partitionedTables modulus=$partModulus classKindRows=$classKindRows abstract=$abstractKinds"
Write-Host "wrote $OutFile ($([math]::Round((Get-Item $OutFile).Length / 1KB)) KB)"
