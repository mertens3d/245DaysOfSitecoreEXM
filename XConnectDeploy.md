### Model Json Files**
  
Copy all custom model json files to

re: [Deploy JSON models to XConnect and Marketing Automation Operations ](https://doc.sitecore.com/developers/90/sitecore-experience-platform/en/deploy-a-custom-model.html#idp16829_body)

1. `C:\<Path to xConnect>\App_Data\Models` 
    
    e.g. C:\inetpub\wwwroot\LearnEXMxconnect.dev.local\App_Data\Models

2. `C:\<Path to xConnect>\App_data\jobs\continuous\IndexWorker\App_data\Models`

    e.g. C:\inetpub\wwwroot\LearnEXMxconnect.dev.local\App_Data\jobs\continuous\IndexWorker\App_Data\Models


3. `C:\<Path to marketing automation operations>\root\App_Data\Models`


### Model Dll's

re: [Deploy the model to the Marketing Automation Engine](https://doc.sitecore.com/developers/90/sitecore-experience-platform/en/deploy-a-custom-model.html#idp16850_body)

Copy all model DLL's to the root of every instance of the Marketing Automation Engine
 
e.g. C:\projects\25DaysOfSitecoreEXM\src\Foundation\CollectionModel.Builder\code\bin\Debug\LearnEXM.Foundation.CollectionModel.Builder.dll

to: 
1. `C:\<Path to xConnect>\App_data\jobs\continuous\AutomationEngine` 
    e.g.  C:\inetpub\wwwroot\LearnEXMxconnect.dev.local\App_Data\jobs\continuous\AutomationEngine
2. `C:\<Path to Sitecore>\bin\`

    
 Deploy your custom model to all core Sitecore instances. This includes the following roles:
 1. Content Delivery
 2. Content Management
 3. xDB Processing



### XML configuration files

re: [Deploy the model to the Marketing Automation Engine](https://doc.sitecore.com/developers/90/sitecore-experience-platform/en/deploy-a-custom-model.html#idp16850_body)

e.g. 

[C:\projects\25DaysOfSitecoreEXM\src\Foundation\CollectionModel.Builder\code\{wwwroot}\{XConnect Instance}\App_data\jobs\continuous\AutomationEngine\AppData\config\sitecore\sc.CinemaDetailsCollection.CustomModel.xml](C:\projects\25DaysOfSitecoreEXM\src\Foundation\CollectionModel.Builder\code\{wwwroot}\XConnectInstance\App_data\jobs\continuous\AutomationEngine\AppData\config\sitecore\sc.CinemaDetailsCollection.CustomModel.xml)


Copy to `C:\<Path to xConnect>\App_data\jobs\continuous\AutomationEngine\AppData\config\sitecore` 

e.g. C:\inetpub\wwwroot\LearnEXMxconnect.dev.local\App_Data\jobs\continuous\AutomationEngine\App_Data\Config\sitecore


### Config Patches
e.g. 

These should deploy as part of the website project publishing 

1. [C:\projects\25DaysOfSitecoreEXM\src\Project\Website\code\App_Config\Sitecore\XConnect.Client.Configuration\Sitecore.XConnect.Client.CinemaDetailsCollection.config](C:\projects\25DaysOfSitecoreEXM\src\Project\Website\code\App_Config\Sitecore\XConnect.Client.Configuration\Sitecore.XConnect.Client.CinemaDetailsCollection.config)
2. [C:\projects\25DaysOfSitecoreEXM\src\Project\Website\code\App_Config\Sitecore\XConnect.Client.Configuration\Sitecore.XConnect.Client.SitecoreCinemaModel.config](C:\projects\25DaysOfSitecoreEXM\src\Project\Website\code\App_Config\Sitecore\XConnect.Client.Configuration\Sitecore.XConnect.Client.SitecoreCinemaModel.config)