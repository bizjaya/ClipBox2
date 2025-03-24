# Version number
$version = "2.4.0"

# Paths
$binDir = ".\bin\Release"
$outputDir = ".\installer"

# Create output directory if it doesn't exist
if (!(Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir
}

# Build WiX project
& "C:\Program Files (x86)\WiX Toolset v3.11\bin\candle.exe" `
    -dVersion="$version" `
    -dBinDir="$binDir" `
    ClipBox2.wxs `
    -out "$outputDir\ClipBox2.wixobj"

& "C:\Program Files (x86)\WiX Toolset v3.11\bin\light.exe" `
    -ext WixUIExtension `
    "$outputDir\ClipBox2.wixobj" `
    -out "$outputDir\ClipBox2-$version.msi"

Write-Host "Installer created at $outputDir\ClipBox2-$version.msi"
