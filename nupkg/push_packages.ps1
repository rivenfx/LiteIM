. ".\common.ps1"

$apiKey = $args[0]
if ([System.String]::IsNullOrWhiteSpace($apiKey)) 
{
    $apiKey = $env:NUGET_KEY
}

# 获取版本
[xml]$versionPropsXml = Get-Content (Join-Path $rootFolder "version.props")
$version = $versionPropsXml.Project.PropertyGroup.Version
$versionStr = $version.Trim()

# 发布所有包
foreach($project in $projects) {
    $packageId = $project

    # 获取packageid
    Try {
        $csprojPath = Join-Path $srcPath $project ("./"+$project+".csproj")
        [xml]$csprojXml = Get-Content $csprojPath
        $packageId = $csprojXml.Project.PropertyGroup.PackageId
    } Catch {
        Write-Host ('get package id error in ' + $project + '.csproj, So use package id: '+ $packageId)
    }

    # 包全路径
    $packageFullPath = Join-Path $packOutputFolder ($packageId + "." + $versionStr + ".nupkg")

    $packageFullPath

    # 发布包
    & dotnet nuget push $packageFullPath -s https://api.nuget.org/v3/index.json --api-key "$apiKey" --skip-duplicate
}

# 返回脚本执行目录
Set-Location $packFolder
