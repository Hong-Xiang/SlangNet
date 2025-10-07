# Local NuGet Testing Script
# Sets up a local NuGet feed and tests the package

param(
    [string]$PackageVersion = "2025.17.2"
)

$ErrorActionPreference = "Stop"

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "Testing SlangNet NuGet Package Locally" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan

# Setup local NuGet source
$localSource = Resolve-Path "$PSScriptRoot\artifacts"
Write-Host "`nLocal package source: $localSource" -ForegroundColor Gray

# Create test project
$testProjectPath = ".\test-nuget-package"
if (Test-Path $testProjectPath) {
    Remove-Item $testProjectPath -Recurse -Force
}

Write-Host "`nCreating test console application..." -ForegroundColor Yellow
dotnet new console -n test-nuget-package -o $testProjectPath -f net10.0

# Copy nuget.config to test project
Copy-Item "test-project-nuget.config" "$testProjectPath\nuget.config"

# Update test project to use win-x64
$csprojPath = "$testProjectPath\test-nuget-package.csproj"
$csprojContent = Get-Content $csprojPath -Raw
$csprojContent = $csprojContent -replace '<PropertyGroup>', "<PropertyGroup>`n    <RuntimeIdentifier>win-x64</RuntimeIdentifier>"
$csprojContent | Set-Content $csprojPath

# Add package reference
Write-Host "`nAdding SlangNet package reference..." -ForegroundColor Yellow
Set-Location $testProjectPath
dotnet add package SlangNet --version $PackageVersion

# Create test code
Write-Host "`nCreating test code..." -ForegroundColor Yellow
@'
using SlangNet;
using System;

Console.WriteLine("Testing SlangNet NuGet Package...");
Console.WriteLine();

try
{
    // Test 1: Create global session with default settings
    Console.WriteLine("Test 1: Creating global session with default settings...");
    var session = Slang.CreateGlobalSession();
    Console.WriteLine("✓ Success: Global session created");
    Console.WriteLine();

    // Test 2: Create global session with custom descriptor
    Console.WriteLine("Test 2: Creating global session with custom descriptor...");
    var desc = SlangGlobalSessionDesc.Default;
    desc.EnableGLSL = true;
    var sessionWithGLSL = Slang.CreateGlobalSession(desc);
    Console.WriteLine("✓ Success: Global session with GLSL support created");
    Console.WriteLine();

    // Test 3: Check for error messages
    Console.WriteLine("Test 3: Checking error message API...");
    var errorMsg = Slang.GetLastInternalErrorMessage();
    Console.WriteLine($"Last error message: {errorMsg ?? "(none)"}");
    Console.WriteLine("✓ Success: Error message API works");
    Console.WriteLine();

    Console.WriteLine("====================================");
    Console.WriteLine("All tests passed! ✓");
    Console.WriteLine("====================================");
}
catch (Exception ex)
{
    Console.WriteLine($"✗ Error: {ex.Message}");
    Console.WriteLine($"Type: {ex.GetType().Name}");
    return 1;
}

return 0;
'@ | Set-Content Program.cs

# Build and run test
Write-Host "`nBuilding test application..." -ForegroundColor Yellow
dotnet build

Write-Host "`nRunning test application..." -ForegroundColor Yellow
dotnet run

Set-Location ..

Write-Host "`n=====================================" -ForegroundColor Cyan
Write-Host "Local NuGet Test Complete!" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan

# Cleanup
Write-Host "`nCleaning up..." -ForegroundColor Yellow
Remove-Item $testProjectPath -Recurse -Force

Write-Host "Done!" -ForegroundColor Green
