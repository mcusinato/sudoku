param(
    [Parameter(Mandatory = $true)]
    [ValidatePattern('^\d+(\.\d+)*(-[a-zA-Z0-9][a-zA-Z0-9.-]*)?$')]
    [string]$Version,

    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[a-zA-Z0-9][a-zA-Z0-9.-]*$')]
    [string]$Server,

    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[a-z_][a-z0-9_-]*$')]
    [string]$User,

    [Parameter(Mandatory = $true)]
    [string]$KeyPath,

    [Parameter(Mandatory = $true)]
    [ValidatePattern('^SHA256:[a-zA-Z0-9+/]{43}=?$')]
    [string]$HostKey
)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$dotnet = Join-Path $env:USERPROFILE '.dotnet\dotnet.exe'
if (-not (Test-Path -LiteralPath $dotnet -PathType Leaf)) {
    $dotnet = (Get-Command dotnet.exe -ErrorAction Stop).Source
}
$plink = Join-Path $env:ProgramFiles 'PuTTY\plink.exe'
$pscp = Join-Path $env:ProgramFiles 'PuTTY\pscp.exe'
$index = Join-Path $root 'index.html'
foreach ($file in @($plink, $pscp, $KeyPath, $index)) {
    if (-not (Test-Path -LiteralPath $file -PathType Leaf)) {
        throw "File not found: $file"
    }
}

$projects = @(
    @{ Name = '100'; Project = '100\100_gui.csproj' }
    @{ Name = 'sudoku'; Project = 'sudoku\sudoku_gui.csproj' }
    @{ Name = 'icosudoku'; Project = 'icosudoku\icosudoku_gui.csproj' }
)
$staging = Join-Path ([System.IO.Path]::GetTempPath()) ("sudoku-publish-" + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $staging | Out-Null
try {
    foreach ($project in $projects) {
        $output = Join-Path $staging $project.Name
        & $dotnet publish (Join-Path $root $project.Project) --configuration Release --runtime linux-x64 --self-contained false --output $output
        if ($LASTEXITCODE -ne 0) {
            throw "Publishing $($project.Name) failed; nothing was copied to the server."
        }
    }

    $target = "${User}@${Server}"
    $remoteCommand = 'mkdir -p "$HOME/publish/sudoku" && mkdir -- "$HOME/publish/sudoku/{0}"' -f $Version
    & $plink -batch -i $KeyPath -hostkey $HostKey -ssh $target $remoteCommand
    if ($LASTEXITCODE -ne 0) {
        throw "Could not create the version directory. It may already exist; no existing version will be overwritten."
    }

    foreach ($project in $projects) {
        Write-Host "Copying $($project.Name)..."
        & $pscp -q -batch -r -i $KeyPath -hostkey $HostKey (Join-Path $staging $project.Name) "${target}:publish/sudoku/$Version/"
        if ($LASTEXITCODE -ne 0) {
            throw "Copy failed. The server directory ~/publish/sudoku/$Version may be incomplete; inspect it before retrying with a new version."
        }
    }
    Write-Host 'Copying index.html...'
    & $pscp -q -batch -i $KeyPath -hostkey $HostKey $index "${target}:publish/sudoku/$Version/index.html"
    if ($LASTEXITCODE -ne 0) {
        throw "Copy of index.html failed. The server directory ~/publish/sudoku/$Version is incomplete; inspect it before retrying with a new version."
    }
    Write-Host "Release prepared at ~/publish/sudoku/$Version. Running applications were not changed."
}
finally {
    Remove-Item -LiteralPath $staging -Recurse -Force
}
