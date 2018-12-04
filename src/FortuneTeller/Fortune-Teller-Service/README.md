## Fortune-Teller-Service

This sample use Unity for IoC services. This app provides endpoints to get fortune(s) from in-memory collection. Cloufoundry services used in this app - 

* Config server
* MS SQL Connector to connect a MS SQL database on Azure
* Discovery client
* Health management

*Note: This app is enabled to get SqlServer connection using DI, but data is not pulled from Sql Server db. You can extend this functionality to get data from your Sql db*.  

## Pre-requisites

1. Installed Pivotal CloudFoundry with Windows support
1. Installed Spring Cloud Services
1. Visual Studio 2017
1. ASP.NET app tragets .Net Framework 4.6.1 or above
1. ASP.NET WEB API project use Unity.Aspnet.Webapi nuget


## Required nuget packages

Microsoft.Extensions.Hosting  
Microsoft.Extensions.Logging.Console  
Unity.Aspnet.Webapi   
Unity.Microsoft.DependencyInjection  

Pivotal.Extensions.Configuration.ConfigServercore  
Pivotal.Discovery.ClientCore  
Steeltoe.Management.EndpointWeb    
Steeltoe.CloudFoundry.Connectorcore  

System.Data.SqlClient  

## Setup Cloudfoundry Services

You must first create instances of the Config Server service, Service Registry service, User Provided service in a org/space.

1. cf target -o YOUR_ORG -s YOUR_SPACE
1. cf create-service p-config-server standard coreConfigServer -c ./config-server.json
1. cf create-service p-service-registry standard  eurekaServer
1. cf cups sqlServerInstance -p '{\"pw\": \"|YOUR_SQL_PWD|\",\"uid\": \"|YOUR_SQL_UID|\",\"uri\": \"jdbc:sqlserver://|YOUR_SQLSERVER_INSTANCE|:|1433|;databaseName=|YOUR_SQL_DB|\"}'



## Publish App & Push

1. Open SteeltoeUnitySampleApps.sln in Visual Studio.
1. Select src/FortuneTeller/Fortune-Teller-Service project in Solution Explorer.
1. Right-click and select Publish
1. Publish the App to a folder (bin/Debug/net472/win10-x64/publish)
1. cd src/FortuneTeller/Fortune-Teller-Service
1. cf push (publish folder path was set in manifest.yml)


## Endpoints to test 

<YOUR_PCF_APP_URL>/api/fortunes/random

 
