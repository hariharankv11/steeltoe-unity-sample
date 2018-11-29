## Fortune-Teller-Service

This sample use Unity for IoC services. It uses **Unity.Microsoft.DependencyInjection nuget package** to enable the app to move registrations frpm Microsoft DI container to Unity container. This app uses below below cloudfoundry services  

* Config server
* MS SQL Connector to connect a MS SQL database on Azure 
* Discovery client
* Health management

*Note: This app is enabled to get SqlServer connection using DI, but data is not pulled from Sql Server db. You can extend this functionality to get data from your Sql db*.  


 ## Required nuget packages

Microsoft.Extensions.Hosting  
Microsoft.Extensions.Logging.Console  
Unity.Aspnet.Webapi   
Unity.Microsoft.DependencyInjection  

Pivotal.Extensions.Configuration.ConfigServercore  
Pivotal.Discovery.ClientCore  
Steeltoe.Management.CloudFoundryCore  
Steeltoe.CloudFoundry.Connectorcore  

System.Data.SqlClient  



## Endpoints to test 

<YOUR_PCF_APP_URL>/api/fortunes/random


## Building and Running the app

**Create and Bind these services:**  

| Bounded Service Name | PCF Service |
| --- | --- |
| coreConfigServer | config server service  
| sqlServerInstance | user provided service   
| eurekaServer | service registry service  
