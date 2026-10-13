param([string]$BaseUrl = 'http://localhost:5187')

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Net.Http
Add-Type -AssemblyName System.IO.Compression.FileSystem
$handler = New-Object System.Net.Http.HttpClientHandler
$client = New-Object System.Net.Http.HttpClient($handler)
$client.BaseAddress = [Uri]$BaseUrl
$workFolder = Join-Path ([IO.Path]::GetTempPath()) ('ConnectorGeneration-' + [Guid]::NewGuid().ToString('N'))
$null = [IO.Directory]::CreateDirectory($workFolder)

function Assert-True($Condition, [string]$Message) {
    if (-not $Condition) { throw $Message }
}

function Send-Form([string]$Action, [hashtable]$Values) {
    $fields = New-Object 'System.Collections.Generic.Dictionary[string,string]'
    foreach ($key in $Values.Keys) { $fields.Add($key, [string]$Values[$key]) }
    $content = New-Object System.Net.Http.FormUrlEncodedContent($fields)
    try {
        $response = $client.PostAsync("/Generator/$Action", $content).GetAwaiter().GetResult()
        $null = $response.EnsureSuccessStatusCode()
        return $response
    } finally {
        $content.Dispose()
    }
}

function Import-Metadata($Metadata) {
    $content = New-Object System.Net.Http.MultipartFormDataContent
    $json = ConvertTo-Json -InputObject $Metadata -Depth 20
    $content.Add((New-Object System.Net.Http.StringContent($json)), 'file', 'metadata.json')
    try {
        $response = $client.PostAsync('/Generator/ImportMetadata', $content).GetAwaiter().GetResult()
        $null = $response.EnsureSuccessStatusCode()
        $response.Dispose()
    } finally {
        $content.Dispose()
    }
}

function Export-Metadata([hashtable]$Extra = @{}) {
    $values = @{ namespace = 'GenerationCheck'; className = 'Connector' }
    foreach ($key in $Extra.Keys) { $values[$key] = $Extra[$key] }
    $response = Send-Form 'SaveMetadata' $values
    try {
        return $response.Content.ReadAsStringAsync().GetAwaiter().GetResult() | ConvertFrom-Json
    } finally {
        $response.Dispose()
    }
}

function Get-GeneratedFiles([string]$Action, [bool]$TestCode, [bool]$Solution, [string]$Name) {
    $response = Send-Form $Action @{
        namespace = 'GenerationCheck'; className = 'Connector'
        generateTestCode = $TestCode.ToString(); generateSolution = $Solution.ToString()
    }
    $zipPath = Join-Path $workFolder "$Name.zip"
    $destination = Join-Path $workFolder $Name
    try {
        [IO.File]::WriteAllBytes($zipPath, $response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult())
        [IO.Compression.ZipFile]::ExtractToDirectory($zipPath, $destination)
        return $destination
    } finally {
        $response.Dispose()
    }
}

function Assert-PowerShellParses([string]$Path) {
    $tokens = $null
    $parseErrors = $null
    $null = [System.Management.Automation.Language.Parser]::ParseFile($Path, [ref]$tokens, [ref]$parseErrors)
    Assert-True ($parseErrors.Count -eq 0) "PowerShell parsing failed for ${Path}: $parseErrors"
}

function Get-DeclarationOrder([string]$Path) {
    $tokens = $null
    $parseErrors = $null
    $syntax = [System.Management.Automation.Language.Parser]::ParseFile($Path, [ref]$tokens, [ref]$parseErrors)
    Assert-True ($parseErrors.Count -eq 0) "PowerShell parsing failed for ${Path}: $parseErrors"
    foreach ($statement in $syntax.EndBlock.Statements) {
        if ($statement -is [System.Management.Automation.Language.TypeDefinitionAst]) {
            "class $($statement.Name)"
        } elseif ($statement -is [System.Management.Automation.Language.FunctionDefinitionAst] -and $statement.Name -notlike 'SafeWrite*ToJson') {
            "function $($statement.Name)"
        }
    }
}

try {
    Assert-True ((Export-Metadata).GenerateTestCode -eq $true) 'New projects must default to checked.'
    $metadata = @{
        Namespace = 'GenerationCheck'; ClassName = 'Connector'; Parameters = @(
            @{ ParamName = 'Endpoint'; Description = 'Target endpoint'; DataType = 'String' }
        )
        SyncClasses = @(@{
            ClassName = 'Person'; Properties = @(
                @{ PropertyName = 'Id'; DataType = 'String'; IsPrimaryKey = $true }
                @{ PropertyName = 'DisplayName'; DataType = 'String'; IsDisplay = $true }
            )
        })
    }
    Import-Metadata $metadata
    Assert-True ((Export-Metadata).GenerateTestCode -eq $false) 'Legacy imports must default to unchecked.'
    $exported = Export-Metadata @{ generateTestCode = 'true' }
    Assert-True ($exported.GenerateTestCode -eq $true) 'Export must include the posted setting.'
    Import-Metadata $exported
    Assert-True ((Export-Metadata).GenerateTestCode -eq $true) 'Import must retain an explicit true setting.'

    $checkedPS = Get-GeneratedFiles 'GeneratePowerShell' $true $false 'checked-ps'
    $checkedNet = Get-GeneratedFiles 'GenerateNet' $true $false 'checked-net'
    foreach ($folder in @($checkedPS, $checkedNet)) {
        Assert-True (Test-Path (Join-Path $folder 'person.json')) 'Checked output must retain object JSON.'
    }
    $checkedModule = Get-Content (Join-Path $checkedPS 'GenerationCheckModule.psm1') -Raw
    Assert-True ($checkedModule.Contains('SafeWritePersonToJson')) 'Checked output must retain demo implementations.'
    Assert-True (Test-Path (Join-Path $checkedPS 'GenerationCheck_TEST.ps1')) 'Checked output must retain the PowerShell test.'
    Assert-True (Test-Path (Join-Path $checkedNet 'GenerationCheck_TEST.cs')) 'Checked output must retain the C# test.'

    $metadata.SyncClasses += @{
        ClassName = 'Account'; Properties = @(
            @{ PropertyName = 'Id'; DataType = 'String'; IsPrimaryKey = $true }
            @{ PropertyName = 'Name'; DataType = 'String'; IsDisplay = $true }
        )
    }
    Import-Metadata $metadata
    foreach ($reordered in @($false, $true)) {
        if ($reordered) {
            $response = Send-Form 'MoveClass' @{ index = '1'; direction = 'up' }
            $response.Dispose()
        }
        $orderedChecked = Get-GeneratedFiles 'GeneratePowerShell' $true $false "order-checked-$reordered"
        $orderedUnchecked = Get-GeneratedFiles 'GeneratePowerShell' $false $false "order-unchecked-$reordered"
        $checkedOrder = @(Get-DeclarationOrder (Join-Path $orderedChecked 'GenerationCheckModule.psm1'))
        $uncheckedOrder = @(Get-DeclarationOrder (Join-Path $orderedUnchecked 'GenerationCheckModule.psm1'))
        Assert-True (($checkedOrder -join '|') -ceq ($uncheckedOrder -join '|')) "Unchecked declarations must match checked order: $checkedOrder; actual: $uncheckedOrder"
        $classOrder = if ($reordered) { 'class Account|class Person' } else { 'class Person|class Account' }
        Assert-True (($uncheckedOrder[0..1] -join '|') -ceq $classOrder) 'Class definitions must retain UI order.'
    }

    foreach ($type in @('Int', 'Bool', 'DateTime', 'String')) {
        foreach ($multiple in @($false, $true)) {
            $metadata.SyncClasses += @{
                ClassName = "$type$multiple"; Properties = @(
                    @{ PropertyName = 'Id'; DataType = 'Int'; IsPrimaryKey = $true }
                    @{ PropertyName = 'Value'; DataType = $type; IsMultivalue = $multiple }
                )
            }
        }
    }
    $metadata.SyncClasses += @{
        ClassName = 'Membership'; IsManyToMany = $true; Properties = @(
            @{ PropertyName = 'Id'; DataType = 'String'; IsCombinedPrimaryKey = $true }
            @{ PropertyName = 'PersonId'; DataType = 'String'; IncludeInCombinedPrimaryKey = $true }
            @{ PropertyName = 'GroupId'; DataType = 'String'; IncludeInCombinedPrimaryKey = $true }
        )
    }
    $metadata.SyncClasses += @{
        ClassName = 'MultiKey'; Properties = @(
            @{ PropertyName = 'Id'; DataType = 'Int'; IsPrimaryKey = $true }
            @{ PropertyName = 'Tenant'; DataType = 'String'; IsPrimaryKey = $true }
            @{ PropertyName = 'DisplayName'; DataType = 'String'; IsDisplay = $true }
        )
    }
    $metadata.GenerateTestCode = $false
    Import-Metadata $metadata
    $exported = Export-Metadata
    Assert-True ($exported.GenerateTestCode -eq $false) 'Import must retain an explicit false setting.'
    Import-Metadata $exported
    $response = Send-Form 'UpdateBasic' @{ namespace = 'GenerationCheck'; className = 'Connector' }
    $response.Dispose()
    Assert-True ((Export-Metadata).GenerateTestCode -eq $false) 'Ordinary edits must retain the setting.'

    $plainPS = Get-GeneratedFiles 'GeneratePowerShell' $false $false 'plain-ps'
    $plainNet = Get-GeneratedFiles 'GenerateNet' $false $false 'plain-net'
    $solution = Get-GeneratedFiles 'GenerateNet' $false $true 'solution'
    foreach ($folder in @($plainPS, $plainNet, $solution)) {
        $jsonFiles = @(Get-ChildItem $folder -Recurse -Filter '*.json')
        Assert-True ($jsonFiles.Count -eq 1 -and $jsonFiles[0].Name -eq 'GenerationCheckMetadata.json') 'Unchecked output must contain only metadata JSON.'
        $archivedMetadata = Get-Content $jsonFiles[0].FullName -Raw | ConvertFrom-Json
        Assert-True ($archivedMetadata.GenerateTestCode -eq $false) 'ZIP metadata must retain the setting.'
        Assert-True (@(Get-ChildItem $folder -Recurse -Filter '*.xml').Count -eq 1) 'Connector XML must remain in each ZIP.'
    }
    $modulePath = Join-Path $plainPS 'GenerationCheckModule.psm1'
    $module = Get-Content $modulePath -Raw
    Assert-True ($module -notmatch 'SafeWrite|FilePath|ConvertFrom-Json|ConvertTo-Json') 'Unchecked module must not contain JSON storage.'
    Assert-True ($module.Contains('function SetParameters') -and $module.Contains('Function Write-Log')) 'Connection setup and logging must remain.'
    $implementation = Get-Content (Join-Path $plainNet 'GenerationCheckImplement.cs') -Raw
    Assert-True ($implementation -notmatch 'JsonSerializer|FilePath|ReadAllText|WriteAllText') 'Unchecked C# must not contain JSON storage.'
    foreach ($source in @($module, $implementation)) {
        Assert-True ($source.Contains('TODO - Add code for interacting with target system here')) 'Stubs must contain the requested TODO.'
        Assert-True ($source.Contains('Not Implemented')) 'Stubs must contain the requested error.'
    }
    $psExample = Join-Path $plainPS 'GenerationCheck_TEST.ps1'
    Assert-PowerShellParses $modulePath
    Assert-PowerShellParses $psExample
    foreach ($examplePath in @($psExample, (Join-Path $plainNet 'GenerationCheck_TEST.cs'))) {
        $example = Get-Content $examplePath -Raw
        foreach ($operation in @('PersonGetAll', 'PersonGet', 'PersonUpdate')) {
            Assert-True ($example.Contains($operation)) "Example must call $operation."
        }
        Assert-True ($example -notmatch 'PersonCreate|PersonDelete') 'Minimal examples must not create or delete objects.'
    }

    Import-Module $modulePath -Force -DisableNameChecking
    try {
        foreach ($syncClass in $metadata.SyncClasses) {
            foreach ($operation in @('GetAll', 'Get', 'Create', 'Update', 'Delete')) {
                $command = $syncClass.ClassName + $operation
                $stubThrew = $false
                try { & $command | Out-Null } catch { $stubThrew = $_.Exception.Message -eq 'Not Implemented' }
                Assert-True $stubThrew "$command must throw Not Implemented."
            }
        }
        Assert-True (@(Get-ChildItem $plainPS -Filter '*.json').Count -eq 1) 'Loading/calling the module must not create JSON data.'
    } finally {
        Remove-Module GenerationCheckModule
    }

    $buildOutput = & dotnet build (Join-Path $solution 'GenerationCheck.sln') --verbosity quiet 2>&1
    Assert-True ($LASTEXITCODE -eq 0) "Generated solution failed to build: $buildOutput"
    $startInfo = New-Object System.Diagnostics.ProcessStartInfo
    $startInfo.FileName = 'dotnet'
    $startInfo.Arguments = 'run --project "' + (Join-Path $solution 'GenerationCheck.Test/GenerationCheck.Test.csproj') + '" --no-build'
    $startInfo.UseShellExecute = $false
    $startInfo.RedirectStandardOutput = $true
    $startInfo.RedirectStandardError = $true
    $process = [System.Diagnostics.Process]::Start($startInfo)
    try {
        $standardOutput = $process.StandardOutput.ReadToEndAsync()
        $standardError = $process.StandardError.ReadToEndAsync()
        $process.WaitForExit()
        $runOutput = $standardOutput.GetAwaiter().GetResult() + $standardError.GetAwaiter().GetResult()
        Assert-True ($process.ExitCode -ne 0 -and $runOutput -match 'NotImplementedException: Not Implemented') 'C# example must call the actual stub.'
    } finally {
        $process.Dispose()
    }
    Write-Output 'PASS: defaults, metadata round trips, checked/unchecked ZIPs, all scalar/array types, combined/multiple keys, PowerShell parsing and stubs, generated C# build and stub invocation.'
    Write-Output "Generated validation artifacts: $workFolder"
} finally {
    $client.Dispose()
    $handler.Dispose()
}