﻿#$websiteRoot = "C:\inetpub\wwwroot\LearnEXMxconnect.dev.local"

#$scriptPath = (Get-Variable MyInvocation -Scope Script).Value.MyCommand.Path
#$currentFolder = Split-Path $scriptPath
#Push-Location $currentFolder

#$src = "..\..\path"

$xConnectWebRoot = "C:\inetpub\wwwroot\LearnEXMxconnect.dev.local"

$solutionSourceRoot = "C:\projects\25DaysOfSitecoreEXM\src"

$jsonModelsAr = @("$solutionSourceRoot\Feature\JSONModelGenerator\code\AutoGenerated\*.json") # add additional filters to the array as needed

$ModeDllsAr = @("$solutionSourceRoot\Foundation\Marketing\code\bin\debug\LearnEXM.Foundationation.Marketing.dll",
                "$solutionSourceRoot\Feature\Marketing\code\bin\debug\LearnEXM.Feature.Marketing.dll"
                )

$xmlPredicateDescriptors = @("$solutionSourceRoot\Feature\Marketing\code\PredicateDescriptors\*.xml")

$xmlConfigurationFiles = @("$solutionSourceRoot\Feature\JSONModelGenerator\Code\{xConnect}\App_Data\jobs\continuous\AutomationEngine\App_Data\config\sitecore\sc.Marketing.CustomModel.xml")

$ConfigurationPatches = @("$solutionSourceRoot\Feature\JSONModelGenerator\Code\{xConnect}\App_Data\jobs\continuous\AutomationEngine\App_Data\config\sitecore\sc.Marketing.CustomModel.xml")

$serviceAutomationEngine = "LearnEXMxconnect.dev.local-MarketingAutomationService"
$serviceProcessingEngine = "LearnEXMxconnect.dev.local-ProcessingEngineService"
$serviceIndexer = "LearnEXMxconnect.dev.local-IndexWorker"

# -------------- Targets - you shouldn't need to change these

$xConnectAppDataModels = "$xConnectWebRoot\App_Data\Models"
$xConnectAppDataIndexeWorkerModels = "$xConnectWebRoot\App_data\jobs\continuous\IndexWorker\App_data\Models"
$xConnectApp_dataAutomationEngine = "$xConnectWebRoot\App_data\jobs\continuous\AutomationEngine"
$xConnectBin  = "$xConnectWebRoot\bin"
$xConnectApp_dataSegmentation = "$xConnectWebRoot\App_Data\Config\sitecore\Segmentation"




function CopyFiles ($SourceArray, $destinationFolder) {
    Write-Host("")
    Write-Host("CopyFiles:")
    # Write-Host("SourceArray : $SourceArray")
    Write-Host("`tdestinationFolder : $destinationFolder")

     foreach ($source in $SourceArray){
        Write-Host("`t`tsource : $source")
        
        $filteredChidlren = Get-ChildItem -Path $source -Recurse



        if($filteredChidlren -ne $null){
            foreach ($filteredChild in $filteredChidlren){
                Write-Host "`t`t`tfilteredChild : $filteredChild.Name" 
                Copy-Item $filteredChild -Destination $destinationFolder
            }

        }
        else{
            write-host ("no filtered children")
        }
     }
}




# --------------- Tasks

CopyFiles $jsonModelsAr $xConnectAppDataModels
CopyFiles $jsonModelsAr $xConnectAppDataIndexeWorkerModels
CopyFiles $ModeDllsAr $xConnectApp_dataAutomationEngine
CopyFiles $ModeDllsAr $xConnectApp_dataAutomationEngine
CopyFiles $xmlPredicateDescriptors $xConnectApp_dataSegmentation
CopyFiles $xmlPredicateDescriptors $xConnectApp_dataAutomationEngine
CopyFiles $xmlConfigurationFiles $xConnectApp_dataAutomationEngine

iisreset
iisreset

Restart-Service -Name $serviceAutomationEngine
Restart-Service -Name $serviceProcessingEngine
Restart-Service -Name $serviceIndexer
