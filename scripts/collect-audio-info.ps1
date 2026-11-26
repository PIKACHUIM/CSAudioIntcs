# Collects audio state on a Chromebook running Windows, for driver issue reports.
#
#   powershell -ExecutionPolicy Bypass -File scripts\collect-audio-info.ps1
#
# Writes ChromeBook-Audio-Report.txt (plus ChromeBook-Audio-Report-Amp.reg when
# amplifier devices exist) to the desktop. Nothing is uploaded and nothing is
# changed on the machine.
#
# Admin is not required. An elevated shell only fills in Secure Boot.

$ErrorActionPreference = 'SilentlyContinue'
$ProgressPreference = 'SilentlyContinue'

$lines = New-Object 'System.Collections.Generic.List[string]'
function W { param([string]$Text = '') $script:lines.Add($Text) }
function Sec { param([string]$Text) W ''; W ('=== ' + $Text + ' ===') }

W ('ChromeBook audio report -- ' + (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'))
W ('computer: ' + $env:COMPUTERNAME + '   user: ' + $env:USERNAME)
W ('elevated: ' + ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))

# ---------------------------------------------------------------- machine ---
Sec 'Machine'
$cs = Get-CimInstance Win32_ComputerSystem
W ('  manufacturer : ' + $cs.Manufacturer)
W ('  model        : ' + $cs.Model)
W ('  system sku   : ' + $cs.SystemSKU)
$board = Get-CimInstance Win32_BaseBoard
W ('  baseboard    : ' + $board.Manufacturer + ' / ' + $board.Product)
$bios = Get-CimInstance Win32_BIOS
W ('  bios         : ' + $bios.SMBIOSBIOSVersion + ' (' + $bios.ReleaseDate + ')')
$os = Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion'
W ('  windows      : ' + $os.ProductName + ' ' + $os.DisplayVersion + ' build ' + $os.CurrentBuild + '.' + $os.UBR)
$sb = try { (Confirm-SecureBootUEFI) } catch { 'unknown (' + $_.Exception.Message + ')' }
W ('  secure boot  : ' + $sb)

# ---------------------------------------------------------------- devices ---
Sec 'Media devices'
$media = @(Get-PnpDevice -Class Media | Sort-Object Status, FriendlyName)
if ($media.Count -eq 0) { W '  (none)' }
foreach ($d in $media) {
    W ('  [' + $d.Status + '] ' + $d.FriendlyName)
    W ('      ' + $d.InstanceId)
}

Sec 'Audio endpoints'
$eps = @(Get-PnpDevice -Class AudioEndpoint | Sort-Object Status, FriendlyName)
if ($eps.Count -eq 0) { W '  (none)' }
foreach ($d in $eps) {
    W ('  [' + $d.Status + '] ' + $d.FriendlyName)
    W ('      ' + $d.InstanceId)
}

Sec 'Amplifiers (ACPI\MX98390 / MX98357A / MX98360)'
$amps = @(Get-PnpDevice | Where-Object { $_.InstanceId -match '^ACPI\\(MX98390|MX98357A|MX98360)' })
W ('  count: ' + $amps.Count)
foreach ($d in $amps) {
    W ('  [' + $d.Status + '] ' + $d.InstanceId + '  ' + $d.FriendlyName)
}

# ---------------------------------------------------------------- drivers ---
Sec 'Audio driver versions'
$drv = Get-CimInstance Win32_PnPSignedDriver |
    Where-Object { $_.DeviceClass -eq 'MEDIA' -or $_.DeviceName -match 'CoolStar|Maxim|98390|98357|98360' } |
    Sort-Object DeviceName
if (-not $drv) { W '  (none)' }
foreach ($d in $drv) {
    W ('  ' + $d.DeviceName)
    W ('      version ' + $d.DriverVersion + '  provider ' + $d.DriverProviderName + '  inf ' + $d.InfName)
}

Sec 'Driver store (audio related)'
$pnputil = & pnputil.exe /enum-drivers 2>$null
$block = @()
$found = 0
foreach ($line in $pnputil) {
    if ($line -match '^\s*$') {
        if (($block -join ' ') -match 'csaudio|max98390|ssm4567|max98357|sklhdaudbus|inteltcss') {
            $block | ForEach-Object { W ('  ' + $_.Trim()) }
            W ''
            $found++
        }
        $block = @()
    } else {
        $block += $line
    }
}
if ($found -eq 0) { W '  (none)' }

# ------------------------------------------------------------- endpoints ---
# The stored device format is the 8-byte property header followed by a
# WAVEFORMATEX / WAVEFORMATEXTENSIBLE block: tag at +8, channels at +10,
# sample rate at +12.
Sec 'Render endpoints'
$render = 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\MMDevices\Audio\Render'
if (Test-Path $render) {
    foreach ($key in Get-ChildItem $render) {
        $props = Get-ItemProperty (Join-Path $key.PSPath 'Properties')
        $name = $props.'{a45c254e-df1c-4efd-8020-67d146a850e0},2'
        $fmt = $props.'{f19f064d-082c-4e27-bc73-6882a1bb8e4c},0'
        $state = (Get-ItemProperty $key.PSPath).DeviceState
        $tag = '?'
        $ch = '?'
        $rate = '?'
        if ($fmt -is [byte[]] -and $fmt.Length -ge 16) {
            $tag = '0x{0:X4}' -f [BitConverter]::ToUInt16($fmt, 8)
            $ch = [BitConverter]::ToUInt16($fmt, 10)
            $rate = [BitConverter]::ToUInt32($fmt, 12)
        }
        W ('  ' + $name + '   [state=' + $state + ' tag=' + $tag + ' channels=' + $ch + ' rate=' + $rate + ']')
        W ('      ' + $key.PSChildName)
    }
} else {
    W '  (no render endpoints)'
}

# ------------------------------------------------------------- problems ----
Sec 'Devices with a problem'
$bad = @(Get-PnpDevice | Where-Object { $_.Status -ne 'OK' -and $_.Status -ne 'Unknown' })
if ($bad.Count -eq 0) { W '  (none)' }
foreach ($d in $bad) {
    W ('  [' + $d.Status + '] ' + $d.Class + ' / ' + $d.FriendlyName)
    W ('      ' + $d.InstanceId + '  problem=' + $d.Problem)
}

Sec 'Audio services'
foreach ($s in 'Audiosrv', 'AudioEndpointBuilder') {
    $svc = Get-Service $s
    W ('  ' + $s + ' : ' + $svc.Status + ' / ' + $svc.StartType)
}

# ------------------------------------------------------------- amp .reg ----
$desktop = [Environment]::GetFolderPath('Desktop')
$report = Join-Path $desktop 'ChromeBook-Audio-Report.txt'
$regFile = Join-Path $desktop 'ChromeBook-Audio-Report-Amp.reg'
$ampKey = 'HKLM\SYSTEM\CurrentControlSet\Enum\ACPI\MX98390'
if (Test-Path 'HKLM:\SYSTEM\CurrentControlSet\Enum\ACPI\MX98390') {
    & reg.exe export $ampKey $regFile /y > $null 2>&1
    if (Test-Path $regFile) { W ''; W ('amp registry export: ' + $regFile) }
} else {
    W ''
    W 'no ACPI\MX98390 devices in the registry, nothing exported'
}

$lines -join "`r`n" | Set-Content -LiteralPath $report -Encoding UTF8
Write-Host ''
Write-Host ('report written to ' + $report)
if (Test-Path $regFile) { Write-Host ('registry dump    ' + $regFile) }
Write-Host 'Attach the file(s) to the issue.'
