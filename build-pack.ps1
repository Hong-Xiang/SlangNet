# Build and Pack Script for SlangNet
# Follows NuGet packaging best practices

param(
    [string]$Configuration = "Release",
    [string]$OutputPath = ".\artifacts"
)

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "Building SlangNet NuGet Package" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
if (Test-Path $OutputPath) {
    Remove-Item $OutputPath -Recurse -Force
}
dotnet clean SlangNet\SlangNet.csproj -c $Configuration

# Restore dependencies
Write-Host "`nRestoring dependencies..." -ForegroundColor Yellow
dotnet restore SlangNet\SlangNet.csproj

# Build for win-x64
Write-Host "`nBuilding for win-x64..." -ForegroundColor Yellow
dotnet build SlangNet\SlangNet.csproj `
    -c $Configuration `
    --no-restore

# Run tests
Write-Host "`nRunning tests..." -ForegroundColor Yellow
dotnet test SlangNet.Test\SlangNet.Test.csproj `
    -c $Configuration

if ($LASTEXITCODE -ne 0) {
    Write-Host "`nTests failed! Aborting pack." -ForegroundColor Red
    exit $LASTEXITCODE
}

# Create NuGet package
Write-Host "`nCreating NuGet package..." -ForegroundColor Yellow
dotnet pack SlangNet\SlangNet.csproj `
    -c $Configuration `
    --no-build `
    -o $OutputPath

# Verify package contents
Write-Host "`nPackage created successfully!" -ForegroundColor Green
$packagePath = Get-ChildItem $OutputPath -Filter "*.nupkg" | Select-Object -First 1
Write-Host "Package: $($packagePath.FullName)" -ForegroundColor Green
Write-Host "Size: $([math]::Round($packagePath.Length / 1MB, 2)) MB" -ForegroundColor Green

Write-Host "`nPackage Contents:" -ForegroundColor Cyan
Add-Type -AssemblyName System.IO.Compression.FileSystem
$zip = [System.IO.Compression.ZipFile]::OpenRead($packagePath.FullName)
$zip.Entries | Where-Object { 
    $_.FullName -like "lib/*" -or 
    $_.FullName -like "runtimes/*" -or
    $_.FullName -eq "README.md"
} | Select-Object FullName, @{Name="Size";Expression={[math]::Round($_.Length / 1KB, 2)}} | Format-Table -AutoSize
$zip.Dispose()

Write-Host "`n=====================================" -ForegroundColor Cyan
Write-Host "Build Complete!" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
