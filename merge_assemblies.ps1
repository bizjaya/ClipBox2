# PowerShell script to merge assemblies using ILMerge
# First, download ILMerge if it doesn't exist
$ilmergePath = "$PSScriptRoot\ILMerge.exe"
if (-not (Test-Path $ilmergePath)) {
    Write-Host "Downloading ILMerge..."
    $ilmergeUrl = "https://github.com/dotnet/ILMerge/releases/download/v3.0.41/ilmerge-3.0.41.nupkg"
    $tempZip = "$PSScriptRoot\ilmerge.nupkg"
    Invoke-WebRequest -Uri $ilmergeUrl -OutFile $tempZip
    
    # Extract the nupkg (it's just a zip file)
    $tempFolder = "$PSScriptRoot\ilmerge_temp"
    if (Test-Path $tempFolder) { Remove-Item -Path $tempFolder -Recurse -Force }
    New-Item -Path $tempFolder -ItemType Directory | Out-Null
    Expand-Archive -Path $tempZip -DestinationPath $tempFolder -Force
    
    # Copy ILMerge.exe to our directory
    Copy-Item -Path "$tempFolder\tools\net452\ILMerge.exe" -Destination $ilmergePath
    
    # Clean up
    Remove-Item -Path $tempZip -Force
    Remove-Item -Path $tempFolder -Recurse -Force
}

# Set paths
$binDir = "$PSScriptRoot\bin\Release"
$outputDir = "$PSScriptRoot\SingleExe"
$primaryAssembly = "$binDir\ClipBox2.exe"
$outputAssembly = "$outputDir\ClipBox2_Merged.exe"

# Create output directory if it doesn't exist
if (-not (Test-Path $outputDir)) {
    New-Item -Path $outputDir -ItemType Directory | Out-Null
}

# Get all DLLs in the bin directory
$dlls = Get-ChildItem -Path $binDir -Filter "*.dll" | Select-Object -ExpandProperty FullName

# Build the ILMerge command
$ilmergeArgs = @(
    "/target:winexe",
    "/targetplatform:v4",
    "/out:$outputAssembly",
    "`"$primaryAssembly`""
)

foreach ($dll in $dlls) {
    $ilmergeArgs += "`"$dll`""
}

# Execute ILMerge
Write-Host "Merging assemblies..."
$ilmergeCommand = "& `"$ilmergePath`" $($ilmergeArgs -join ' ')"
Invoke-Expression $ilmergeCommand

if (Test-Path $outputAssembly) {
    Write-Host "Successfully created merged assembly at: $outputAssembly"
    
    # Copy any non-DLL files that might be needed (config files, resources, etc.)
    Get-ChildItem -Path $binDir -Exclude "*.dll", "*.pdb", "*.xml", $primaryAssembly | 
        ForEach-Object {
            Copy-Item -Path $_.FullName -Destination $outputDir
            Write-Host "Copied: $($_.Name)"
        }
} else {
    Write-Host "Failed to create merged assembly!" -ForegroundColor Red
}
