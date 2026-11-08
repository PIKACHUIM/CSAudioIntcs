# Verifies that the payload referenced by installer\payload.list is complete
# before Inno Setup packages it. Fails the build on anything missing.

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$list = Join-Path $root 'installer\payload.list'
$dp   = Join-Path $root 'tools\dpinst\dpinst.exe'

if (-not (Test-Path $list)) { throw "payload.list not found: $list" }

$errors = New-Object System.Collections.Generic.List[string]
$warns  = New-Object System.Collections.Generic.List[string]
$count  = 0

foreach ($line in Get-Content -LiteralPath $list) {
    $entry = $line.Trim()
    if ($entry -eq '' -or $entry.StartsWith('#')) { continue }

    $dir = Join-Path $root ('drivers\' + $entry)
    if (-not (Test-Path -LiteralPath $dir)) {
        $errors.Add("missing folder: drivers\$entry")
        continue
    }

    $infs = @(Get-ChildItem -LiteralPath $dir -Filter '*.inf' -File)
    if ($infs.Count -eq 0) { $errors.Add("no .inf in drivers\$entry") }

    $sys = @(Get-ChildItem -LiteralPath $dir -Filter '*.sys' -File)
    $cat = @(Get-ChildItem -LiteralPath $dir -Filter '*.cat' -File)
    if ($sys.Count -eq 0) { $warns.Add("no .sys in drivers\$entry (placeholder only?)") }
    if ($cat.Count -eq 0) { $warns.Add("no .cat in drivers\$entry (unsigned package)") }

    foreach ($i in $infs) {
        $ref = Select-String -LiteralPath $i.FullName -Pattern '^\s*CatalogFile\s*=' -ErrorAction SilentlyContinue
        if ($ref) {
            $catName = ($ref.Line -split '=', 2)[1].Trim()
            if (-not (Test-Path -LiteralPath (Join-Path $dir $catName))) {
                $errors.Add("$entry\$($i.Name) references $catName but it is missing")
            }
        }
    }

    $count++
}

if (-not (Test-Path -LiteralPath $dp)) { $errors.Add('tools\dpinst\dpinst.exe is missing') }

$ca = Join-Path $root 'certs\pikachu-certificate-install.exe'
if (-not (Test-Path -LiteralPath $ca)) {
    $errors.Add('certs\pikachu-certificate-install.exe is missing (the catalogs cannot be trusted without it)')
}

Write-Host ("payload entries : {0}" -f $count)
Write-Host ("dpinst.exe      : {0}" -f (Test-Path -LiteralPath $dp))
Write-Host ("ca installer    : {0}" -f (Test-Path -LiteralPath $ca))

foreach ($w in $warns)  { Write-Host ("WARN  " + $w) -ForegroundColor Yellow }
foreach ($e in $errors) { Write-Host ("ERROR " + $e) -ForegroundColor Red }

if ($errors.Count -gt 0) { throw ("preflight failed with {0} error(s)" -f $errors.Count) }
Write-Host 'preflight OK' -ForegroundColor Green
