# 全局通用的配置变量信息
$version = "1.0.0"
$SLN_PATH = 'LiteIM.sln'

# 是否为发布
$isProduction = $env:IS_PRODUCTION

# 发布模式，从环境变量读取
if ($isProduction -eq $True) {
  $version = $env:TAG
}

# 源码根路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName # 运行目录
$slnPath = Join-Path $packFolder "../" # sln所在目录
$distPath = Join-Path $packFolder 'dist' # 输出目录

# 打印构建信息
Write-Host '项目当前所在路径信息：' $packFolder
Write-Host "即将生成的新版本号为" $version
Write-Host "输出目录" $distPath
 
# 转换相对路径为绝对路径
Set-Location  $slnPath


# 还原nuget包
Write-Host "开始还原"
dotnet restore $SLN_PATH --ignore-failed-sources
# 编译
Write-Host "开始编译打包"
dotnet publish $SLN_PATH -c Release
# 打包
dotnet pack $SLN_PATH --no-restore `
  -c Release `
  -o ${distPath} `
  -p:IncludeSymbols=true `
  -p:SymbolPackageFormat=snupkg `
  -p:Version=${version} `
  -p:Version=${version}

# 推送
Write-Host "打包完成清单"
$packageCounter = 0
Get-ChildItem ($distPath + '/*.nupkg') | ForEach-Object -Process {
  if ($_ -is [System.IO.FileInfo]) { 
    $packageCounter += 1    
    Write-Host ($_.FullName)
  }
}
Write-Host ('打包完成，包数量： ' + $packageCounter )

# 切换到当前目录
Set-Location $packFolder
# 执行错误判断
if($Error.Count -eq 0){
  exit 0
}else {
  exit 1
}
