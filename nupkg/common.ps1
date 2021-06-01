# 路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName   # 当前路径
$rootFolder = Join-Path $packFolder "../"               # 项目根目录
$packOutputFolder = Join-Path $packFolder "dist"   # 输出nuget package 目录
# 解决方案路径
$slnPath = $rootFolder
$srcPath = Join-Path $slnPath "src"


# 所有的项目名称
$projects = (
    "LiteIM.Core",
    "LiteIM.CSRedisCore"
)
