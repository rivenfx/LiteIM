# 执行公用脚本
. ".\basic_funcs.ps1"

# 基本信息
# -------------- nuget.config --------------- 
$NugetConfig = $env:nexus_nuget_config  # nuget配置

# 路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName   # 当前路径
$rootFolder = Join-Path $packFolder "../"               # 项目根目录
$slnFolder = $rootFolder # sln所在目录

# 切换到源码目录
Set-Location $rootFolder

# 修改NuGet.config
$nugetConfigPath = Join-Path $rootFolder "./NuGet.Config"
if(![String]::IsNullOrWhiteSpace($NugetConfig)){
    WriteFile -Path $nugetConfigPath -Content $NugetConfig
}

# 切换到项目目录
Set-Location $slnFolder


# 切换到脚本启动目录
Set-Location $packFolder


# 执行错误判断
if($Error.Count -eq 0){
    exit 0
}else {
    exit 1
}
