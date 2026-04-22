# 获取env
function GetEnv {
    param (
        [string]$Name
    )
    Write-Host "GetEnv: ${Name}" 
    return [Environment]::GetEnvironmentVariable("$Name")
}

# 读取文件 utf8
function ReadFile {
    param (
        [string]$Path
    )
    Write-Host "ReadFile: ${Path}" 
    return Get-Content -Path $Path -Encoding UTF8
}

# 写入文件 utf8
function WriteFile {
    param (
        $Path,
        $Content
    )
    Write-Host "WriteFile: ${Path}" 
    Set-Content -Path $Path  -Value $Content -Encoding UTF8 -Force
}


# 替换文本内容
function ConentReplace {
    param (
        [string]$Path,
        [string]$OldVal,
        [string]$NewVal
    )
    if (Test-Path $Path) {   
        Write-Host "ConentReplace: ${Path}" 
        (Get-Content $Path) -Replace $OldVal, $NewVal | Set-Content $Path
    }
}

# 更新xml文件的选择路径的InnerText
function UpdateXmlInnerText {
    param (
        [string]$Path,
        [string]$XPath,
        [string]$InnerText
    )
    if (Test-Path $Path) {
        Write-Host "UpdateXmlInnerText: ${Path}"
        [xml]$content = Get-Content $Path
        $content.SelectNodes($XPath)[0].set_InnerText($InnerText)
        $content.Save($Path)
    }
}

# 执行脚本
function CmdExec {
    param (
        [string]$CmdStr
    )
    $onlyPrint = $env:onlyPrint
    Write-Host "CmdExec: ${CmdStr}"
    if ($onlyPrint) {
        return
    }
    # & $CmdStr
    Invoke-Expression $CmdStr
}

# nuget登录
function NugetLogin {
    param (
        [string]$source,
        [string]$user,
        [string]$apikey
    )
    dotnet nuget add source  -n "$user" `
        -u "$user" `
        -p "$apikey" `
        --store-password-in-clear-text `
        "$source"
}