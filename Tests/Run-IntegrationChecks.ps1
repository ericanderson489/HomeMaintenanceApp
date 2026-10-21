# Runs Windows Forms checks in a fresh folder, away from real account files.
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot
$testOutput = Join-Path $projectRoot ('TestResults/' + [guid]::NewGuid().ToString('N'))
dotnet build (Join-Path $PSScriptRoot 'IntegrationCheck.csproj') -o $testOutput --nologo
if ($LASTEXITCODE -ne 0) { throw 'Integration test build failed.' }
$run = Start-Process -FilePath (Join-Path $testOutput 'IntegrationCheck.exe') -PassThru -WindowStyle Hidden -RedirectStandardOutput (Join-Path $testOutput 'checks.log') -RedirectStandardError (Join-Path $testOutput 'errors.log')
if (-not $run.WaitForExit(120000)) {
    $run.Kill()
    throw "Integration checks timed out. Logs: $testOutput"
}
$run.WaitForExit()
Get-Content (Join-Path $testOutput 'checks.log')
Get-Content (Join-Path $testOutput 'errors.log')
if ($run.ExitCode -ne 0) { throw "Integration checks failed with exit code $($run.ExitCode). Logs: $testOutput" }
