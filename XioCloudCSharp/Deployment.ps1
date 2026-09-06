<#
.SYNOPSIS
    This script is used to deploy compiled code to Crestron Processor or Server
    It requires PowerShell 7 or above to run. 
.DESCRIPTION
    Depending on the compile configuration the script will deploy the compiled code to the Crestron Processor or Server
    When deploying to processor as debug, it will load the program, and enable debuging in the processor.
    While on debug alway Kill the program first before loading a new version.  
    Make sure that the processor is not on breakpoint mode when killing the program or the processor will lock up
    When deploying to processor as release, it will only load the program.
    When deploying to server, it will upload the program to the server and start the program.
    It will make a copy of the compiled and packaged .CLZ found in the output directory to the project directory.
    
    It will read credentials from Windows Enviromental Variables first or from a .ENV file in the project directory.
.PARAMETER ProjectDir
    The directory of the project
.PARAMETER OutDir
    The output directory of the project
.PARAMETER ProjectName
    The name of the project
.PARAMETER Configuration
    The configuration used to compile the project can be one of the following:
    DebugAppliance, ReleaseAppliance, ReleaseServer, ReleaseCopyOnly    
#>
param (
    [string] $ProjectDir,
    [string] $OutDir,
    [string] $ProjectName,
    [string] $Configuration
)
# Exits if Powershell version is not 7 or above
$version = (Get-Host).version
if ($version.Major -lt 7) {
    Write-Output "This script only works on Powershell 7 or above"
    Write-Output "Install Powershell 7 by typing the following on terminal"
    Write-Output "winget install Microsoft.PowerShell"
    return;
}

function SendToAppliance {
    Invoke-Expression "echo y | pscp -l $( $Env.Appliance.UserName ) -pw $( $Env.Appliance.Password ) -sftp ""$ProjectDir$OutDir$ProjectName.cpz"" $( $Env.Appliance.Address ):program$( $Env.Appliance.SlotNo )"
}

function SendToServer {
    $token = $Env.Server.Token
    $AuthHeader = @{ "Authorization" = $token }
    $result = Invoke-WebRequest -Uri "https://$( $Env.Server.Address )/VirtualControl/config/api/ProgramLibrary" -Headers $AuthHeader  -SkipCertificateCheck
    $content = ConvertFrom-Json $result.Content -AsHashTable
    $programLibrary = $content.Device.Programs.ProgramLibrary
    foreach ($key in $programLibrary.Keys) {
        if ($programLibrary.$key.AppFile -eq "$ProjectName.cpz") {
            Write-Output "Program Exists already, Overwriting"
            Invoke-Expression "curl.exe -X PUT https://$( $Env.Server.Address )/VirtualControl/config/api/ProgramLibrary -H Authorization:$( $Env.Server.Token ) -H Content-Type:multipart/form-data -F ""ProgramId=$( $programLibrary.$key.ProgramId )"" -F ""AppFile=@$ProjectDir$OutDir$ProjectName.cpz"" -F ""StartNow=true"" -k"
            return;
        }
    }
    if ($null -eq $programLibrary.Keys) {
        return;
    }
    Write-Output "Program Doesn't Exists, Creating"
    Invoke-Expression "curl.exe -X POST https://$( $Env.Server.Address )/VirtualControl/config/api/ProgramLibrary -H Authorization:$( $Env.Server.Token ) -H Content-Type:multipart/form-data -F ""FriendlyName=$ProjectName"" -F ""AppFile=@$ProjectDir$OutDir$ProjectName.cpz"" -k"
}
$Env = [PSCustomObject]@{
    Appliance = [PSCustomObject]@{
        UserName = ""
        Password = ""
        Address  = ""
        SlotNo   = ""
    }
    Server    = [PSCustomObject]@{
        Token   = ""
        Address = ""
    }
}
If (Test-Path ".ENV") {
    $LoadedEnv = Get-Content -Raw ".ENV" | ConvertFrom-JSON;
}

function CopyCompiledCode() {
    $copyPath = Resolve-Path ../
    if (Test-Path "$ProjectDir$OutDir$ProjectName.cpz" -PathType Leaf) {
        Copy-Item -Path "$ProjectDir$OutDir$ProjectName.cpz" -Destination $copyPath
        Write-Output "CPZ package has been copied to: to $copyPath For $Configuration"
    }
    else {
        Write-Output "No CPZ file found"
    }
}

$Env.Appliance.UserName = ([String]::IsNullOrEmpty($LoadedEnv.Appliance.UserName)) ? $Env:ApplianceUserName : $LoadedEnv.Appliance.UserName;
$Env.Appliance.Password = ([String]::IsNullOrEmpty($LoadedEnv.Appliance.Password)) ? $Env:AppliancePassword : $LoadedEnv.Appliance.Password;
$Env.Appliance.Address = ([String]::IsNullOrEmpty($LoadedEnv.Appliance.Address)) ? $Env:ApplianceAddress : $LoadedEnv.Appliance.Address;
$Env.Appliance.SlotNo = ([String]::IsNullOrEmpty($LoadedEnv.Appliance.SlotNo)) ? $Env:ApplianceSlotNo : $LoadedEnv.Appliance.SlotNo;
$Env.Server.Token = ([String]::IsNullOrEmpty($LoadedEnv.Server.Token)) ? $Env:ServerToken : $LoadedEnv.Server.Token;
$Env.Server.Address = ([String]::IsNullOrEmpty($LoadedEnv.Server.Address)) ? $Env:ServerAddress : $LoadedEnv.Server.Address;

CopyCompiledCode

switch ($Configuration) {
    "DebugAppliance" {
        If ($Env.Appliance.Address -eq "") {
            return
        };
        Write-Output "Sending to $( $Env.Appliance.Address ) For $Configuration"
        Write-Output "Do not stop debugging from IDE. Instead Kill the program at the processor using KILLPROG -P:{Slot}."
        Write-Output "Make sure that the processor is not on breakpoint mode when killing the program or the processor will lock up"
        Write-Output "Setting Debug Mode on processor for Port 50000 on all IP addresses"
        SendToAppliance
        Invoke-Expression "plink -pw $( $Env.Appliance.Password ) $( $Env.Appliance.UserName )@$( $Env.Appliance.Address ) -batch ""PROGLOAD -P:$( $Env.Appliance.SlotNo ) -D"""
        Invoke-Expression "plink -pw $( $Env.Appliance.Password ) $( $Env.Appliance.UserName )@$( $Env.Appliance.Address ) -batch ""DEBUGPROGRAM -P:$( $Env.Appliance.SlotNo ) -Port:50000 -IP:0.0.0.0 -S"""
        Invoke-Expression "plink -pw $( $Env.Appliance.Password ) $( $Env.Appliance.UserName )@$( $Env.Appliance.Address ) -batch ""PROGRESET -P:$( $Env.Appliance.SlotNo )"""
    }
    "ReleaseAppliance" {
        If ($Env.Appliance.Address -eq "") {
            return
        };
        Write-Output "Sending to $( $Env.Appliance.Address ) For $Configuration"
        SendToAppliance
        Invoke-Expression "plink -pw $( $Env.Appliance.Password ) $( $Env.Appliance.UserName )@$( $Env.Appliance.Address ) -batch ""PROGLOAD -P:$( $Env.Appliance.SlotNo )"""
    }
    { $_ -in "ReleaseServer", "DebugServer" } {
        If ($Env.Server.Address -eq "") {
            return
        };
        Write-Output "Sending to $( $Env.Server.Address ) For $Configuration"
        SendToServer
    }
    Default {
    }
}






<#  NOTE:
This script only works on PowerShell 7 or above. TO Install run:
    winget install Microsoft.PowerShell
Also make sure to enable scripts by typing
    Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope CurrentUser
#>

<#  environmental file .ENV Properties
-------------------------------------------
 {
    "Appliance" : 
    {
        "UserName":"",
        "Password":"",
        "Address":"",
        "SlotNo":"01"
    },
    "Server":
    {
        "Token":"",
        "Address":""
    }
 }
------------------------------------------ 
#>

<#  Windows environmental  variables (Replace **** with your info and then paste in PowerShell Terminal)
This only needs to be done once in your machine
-------------------------------------------
[System.Environment]::SetEnvironmentVariable('ApplianceUserName','****',"user")
[System.Environment]::SetEnvironmentVariable('AppliancePassword','****',"user")
[System.Environment]::SetEnvironmentVariable('ApplianceAddress','****',"user")
[System.Environment]::SetEnvironmentVariable('ApplianceSlotNo','01',"user")
[System.Environment]::SetEnvironmentVariable('ServerToken','****',"user")
[System.Environment]::SetEnvironmentVariable('ServerAddress','****',"user")
------------------------------------------ 
#>

<# MSBUILD Addition: Add at the end inside the Project Tag
------------------------------------------ 
    <Target Name="CLZPostBuildActions" AfterTargets="SimplSharpPostProcess">
        <Exec Command="&quot;C:\Program Files\PowerShell\7\pwsh.exe&quot; -command &quot;&amp; { .\Deployment.ps1 '$(ProjectDir)' '$(OutDir)' '$(ProjectName)' '$(Configuration)' }&quot;" />
    </Target>
------------------------------------------ 
#>