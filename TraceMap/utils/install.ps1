$Path64 = "C:\Program Files"
$Path32 = "C:\Program Files (x86)"
$InstallDir = ""
$ExeCurrentLocation = $PSScriptRoot + ".\*"

$is64os = (Test-Path $Path64 -PathType Any) | Out-String
if ($is64os) {
	$InstallDir = $Path64 + "\Tracemap"
}
else {
	$InstallDir = $Path64 + "\Tracemap"
}

New-Item -ItemType Directory -Force -Path $InstallDir 

Copy-Item -Path $ExeCurrentLocation -Destination $InstallDir

$env:Path += ";" + $InstallDir