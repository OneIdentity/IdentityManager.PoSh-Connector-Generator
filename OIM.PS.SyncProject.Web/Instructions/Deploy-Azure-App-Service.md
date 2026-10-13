# Deploy the Existing Azure App Service

This procedure updates application code on the existing Windows App Service. It does not create resources, change the hosting plan, configure authentication, or change the runtime.

## Existing Target

| Setting | Value |
| --- | --- |
| Subscription | Visual Studio Enterprise |
| Subscription ID | `3a2b24af-ec2b-46f1-98bb-f930555558d3` |
| App resource group | `rg-oneim` |
| Web app | `OIMPSSyncProjectGeneratorWeb` |
| App Service Plan | `ServicePlanf2952bc6-a341` |
| Plan resource group | `oiservice` |
| Region | Central US (`centralus`) |
| Platform | Windows, .NET 10, 32-bit worker |
| Existing plan tier | Free F1; retained, not upgraded |
| Instances | One |

Live URL: https://oimpssyncprojectgeneratorweb-efgfdzbaekdtb6gy.centralus-01.azurewebsites.net

Always query `defaultHostName` rather than constructing a hostname from the app name. This app uses Azure's generated hostname suffix.

## Prerequisites

- Windows PowerShell 5.1 or later, Azure CLI, and the .NET 10 SDK.
- Permission to publish to this existing App Service.
- Run the commands from the repository root, not the web project's directory.
- Confirm the web project still targets `net10.0`.
- Retain a known-good deployment ZIP for rollback. This procedure does not back up the previous server files.

```powershell
dotnet --list-sdks
az version
az login
```

If Azure CLI is already authenticated to the correct tenant, another `az login` is unnecessary. Complete any authentication prompt directly; never place passwords or tokens in this document or source control.

## 1. Set Variables and Inspect the Target

```powershell
$ErrorActionPreference = 'Stop'
$subscriptionId = '3a2b24af-ec2b-46f1-98bb-f930555558d3'
$resourceGroup = 'rg-oneim'
$webAppName = 'OIMPSSyncProjectGeneratorWeb'
$appServicePlan = 'ServicePlanf2952bc6-a341'
$planResourceGroup = 'oiservice'
$project = '.\OIM.PS.SyncProject.Web\OIM.PS.SyncProject.Web.csproj'
$deploymentRoot = Join-Path $PWD ('artifacts/appservice/' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss'))
$publishDir = Join-Path $deploymentRoot 'publish'
$zipPath = Join-Path $deploymentRoot 'app.zip'

az webapp show --subscription $subscriptionId --resource-group $resourceGroup --name $webAppName --query '{name:name,location:location,state:state,plan:serverFarmId,host:defaultHostName,httpsOnly:httpsOnly}' -o json
if ($LASTEXITCODE -ne 0) { throw 'Target inspection failed.' }

az webapp config show --subscription $subscriptionId --resource-group $resourceGroup --name $webAppName --query '{runtime:netFrameworkVersion,is32Bit:use32BitWorkerProcess,workers:numberOfWorkers}' -o json
if ($LASTEXITCODE -ne 0) { throw 'Runtime inspection failed.' }

az webapp list-runtimes --os-type windows -o json
if ($LASTEXITCODE -ne 0) { throw 'Runtime discovery failed.' }

az appservice plan show --subscription $subscriptionId --resource-group $planResourceGroup --name $appServicePlan --query '{name:name,sku:sku,location:location}' -o json
if ($LASTEXITCODE -ne 0) { throw 'Plan inspection failed.' }
```

The verified target was already configured as `v10.0`. Do not create a new plan or run `az webapp create` for this update. The plan is shared infrastructure in another resource group; do not upgrade, resize, or delete it as part of publishing code.

## 2. Publish the Web Project

```powershell
dotnet publish $project --configuration Release --framework net10.0 --no-self-contained -p:UseAppHost=false --output $publishDir
if ($LASTEXITCODE -ne 0) { throw 'Release publish failed.' }
Get-Content (Join-Path $publishDir 'web.config')
Get-Content (Join-Path $publishDir 'OIM.PS.SyncProject.Web.runtimeconfig.json')
```

Publishing compiles the app and its referenced Common and Generator libraries. Do not publish the whole solution: the Windows Forms desktop application is not part of the web deployment.

`UseAppHost=false` produces a framework-dependent DLL. The generated IIS configuration should use `processPath="dotnet"` with `OIM.PS.SyncProject.Web.dll` as its argument. This avoids deploying a 64-bit apphost to the existing 32-bit worker.

Use a fresh output directory each time so files from an earlier build cannot enter the new package.

If a future target genuinely lacks the required runtime, stop and select a compatible deployment approach. For this existing 32-bit Windows worker, a self-contained executable would need `win-x86`, not `win-x64`; publish with `--self-contained true` and `UseAppHost=true`, then inspect the generated IIS configuration again. No such fallback was needed for the verified deployment.

## 3. Check the Published Application Locally

In a separate terminal, run the published package in Production mode:

```powershell
$publishRoot = (Resolve-Path $publishDir).Path
dotnet (Join-Path $publishRoot 'OIM.PS.SyncProject.Web.dll') --urls http://127.0.0.1:5189 --environment Production --contentRoot $publishRoot
```

Open `http://127.0.0.1:5189/Generator`. Check the page, images, and styling. Add a class containing a string primary-key property and a string display property, then generate both PowerShell and .NET ZIP downloads. Stop the local server with Ctrl+C afterward.

An HTTPS-redirection warning is expected for this HTTP-only local smoke check because no HTTPS port is configured. It is not a startup failure. The live app must continue to use HTTPS.

## 4. Create a Deployment ZIP

Windows ZIP APIs can emit backslashes in entry names. The following uses the .NET ZIP API and explicitly normalizes all nested entry paths to forward slashes. Published files must be at the archive root, not inside a `publish` directory.

```powershell
Add-Type -AssemblyName System.IO.Compression.FileSystem
$publishRoot = (Resolve-Path $publishDir).Path.TrimEnd('\') + '\'
$archive = [System.IO.Compression.ZipFile]::Open($zipPath, [System.IO.Compression.ZipArchiveMode]::Create)
try {
    foreach ($file in Get-ChildItem -LiteralPath $publishRoot -Recurse -File -Force) {
        $entryName = $file.FullName.Substring($publishRoot.Length).Replace('\', '/')
        $entry = $archive.CreateEntry($entryName, [System.IO.Compression.CompressionLevel]::Optimal)
        $inputStream = [System.IO.File]::OpenRead($file.FullName)
        $outputStream = $entry.Open()
        try {
            $inputStream.CopyTo($outputStream)
        } finally {
            $inputStream.Dispose()
            $outputStream.Dispose()
        }
    }
} finally {
    $archive.Dispose()
}

$archive = [System.IO.Compression.ZipFile]::OpenRead($zipPath)
try {
    $entries = @($archive.Entries.FullName)
    foreach ($required in @('web.config', 'OIM.PS.SyncProject.Web.dll', 'OIM.PS.SyncProject.Common.dll', 'OIM.PS.SyncProject.Generator.dll', 'wwwroot/css/site.css')) {
        if ($entries -notcontains $required) { throw "ZIP missing $required" }
    }
    if ($entries | Where-Object { $_ -match '\\' }) { throw 'ZIP contains backslash paths.' }
} finally {
    $archive.Dispose()
}
Get-FileHash $zipPath -Algorithm SHA256
```

Keep packages outside source control. The repository ignores `artifacts/`. Do not include credentials, publish profiles, or local session audit files in the ZIP.

## 5. Replace the Existing Application Code

**This replaces the deployed files and restarts the site. Expect brief downtime and loss of in-memory session metadata.** `--clean true` removes files left by previous deployments; do not use it if server-side files need to be retained without first arranging a backup.

```powershell
az webapp deploy --subscription $subscriptionId --resource-group $resourceGroup --name $webAppName --src-path $zipPath --type zip --clean true --restart true --timeout 600000
if ($LASTEXITCODE -ne 0) { throw 'Azure deployment failed; inspect deployment logs before retrying.' }
```

Use the existing CLI sign-in. Do not enable basic publishing authentication or weaken network/authentication controls just to bypass a failed deployment. Do not print or store access tokens.

No new resources, plan changes, or application configuration updates are required for this code-only procedure. Existing plan limits and cold starts remain.

## 6. Verify the Live Application

```powershell
$hostName = az webapp show --subscription $subscriptionId --resource-group $resourceGroup --name $webAppName --query defaultHostName -o tsv
if ($LASTEXITCODE -ne 0) { throw 'Could not resolve the application hostname.' }
$baseUrl = 'https://' + $hostName.Trim()
$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession

foreach ($path in @('/', '/Generator', '/css/site.css', '/images/Logo.png')) {
    $response = Invoke-WebRequest -Uri ($baseUrl + $path) -WebSession $session -UseBasicParsing -TimeoutSec 120
    if ($response.StatusCode -ne 200) { throw "Live check failed: $path" }
    Write-Output "$path -> HTTP $($response.StatusCode)"
}

$classForm = @{
    ClassName = 'SmokeRecord'
    'Properties[0].PropertyName' = 'Id'
    'Properties[0].DataType' = 'String'
    'Properties[0].IsPrimaryKey' = 'true'
    'Properties[1].PropertyName' = 'DisplayName'
    'Properties[1].DataType' = 'String'
    'Properties[1].IsDisplay' = 'true'
}
[void](Invoke-WebRequest -Uri "$baseUrl/Generator/AddClass" -Method Post -Body $classForm -WebSession $session -UseBasicParsing -TimeoutSec 120)

foreach ($action in @('GeneratePowerShell', 'GenerateNet')) {
    $response = Invoke-WebRequest -Uri "$baseUrl/Generator/$action" -Method Post -Body @{namespace='DeploymentSmoke';className='SmokeConnector'} -WebSession $session -UseBasicParsing -TimeoutSec 120
    $stream = $response.RawContentStream
    $stream.Position = 0
    $signature = New-Object byte[] 4
    [void]$stream.Read($signature, 0, 4)
    if ($response.StatusCode -ne 200 -or $signature[0] -ne 80 -or $signature[1] -ne 75) {
        throw "$action did not return a ZIP archive."
    }
    Write-Output "$action -> HTTP 200, ZIP signature verified"
}
```

The test metadata belongs to a fresh browser/session cookie, not another user's project. There is no application database to migrate or seed. Authentication testing is not applicable to the current anonymous demo.

Confirm deployment success also requested a site restart, then verify the site afterward. A separate manual restart can be tested when needed with `az webapp restart`, but it disrupts active sessions.

## Diagnostics and Rollback

```powershell
az webapp log deployment list --subscription $subscriptionId --resource-group $resourceGroup --name $webAppName -o table
az webapp log deployment show --subscription $subscriptionId --resource-group $resourceGroup --name $webAppName -o json
```

Application filesystem logging was **Off** during verification; deployment logs were inspected, not a retained live application log stream. To investigate startup failures, deliberately enable application logging in Azure App Service or use the portal's diagnostic tools. Changing logging is an operational configuration change, not part of this code-only deployment.

For a runtime error, check the worker architecture, configured runtime, generated `web.config`, and publish output. Do not deploy a Windows x64 executable to a 32-bit worker. For broken assets, check root ZIP layout and forward-slash entry names.

Rollback consists of deploying a previously retained, known-good ZIP to the same app using the command in step 5. Do not delete the resource group or shared plan. No pre-update backup was captured automatically in the deployment recorded below.

## Verified Deployment Record

- Date: 2026-10-13, deployment completed at `2026-10-13T21:49:05.0017692Z`.
- OneDeploy ID: `6413014e4843453a80104d85bd9083f5`.
- Package SHA256: `D15D2AC148F8353119ED73863D1E6BC13F3168F1A083F8E4843FF080C8F4B8F0`.
- Release publish and Production-mode startup passed.
- Live HTTPS root, generator, stylesheet, and logo returned HTTP 200 after the deployment-requested restart.
- Both PowerShell and .NET generation returned ZIP archives with valid metadata.
- Deployment logs contained successful completion and restart; runtime remained .NET 10 with one 32-bit worker.
- Resource inventory reported no new Azure resources.

Known caveats: session metadata expires after 30 minutes idle and is lost on restart; export work before redeployment. The existing .NET generator returns HTTP 500 when called with an empty project containing no classes. A configured class with a primary key and display property was used for the successful workflow checks; this deployment did not repair the empty-project behavior.