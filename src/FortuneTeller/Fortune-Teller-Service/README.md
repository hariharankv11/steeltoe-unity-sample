## Fortune-Teller-Service

Features demonstarted 

* Config server
* MS SQL Connector to connect a MS SQL database on Azure
* Discovery client
* Health management



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



## endpoints to test 

<YOUR_PCF_APP_URL>/api/fortunes/random


## Building and Running the app

Conncted services:
coreConfigServer    -   config server service
sqlServerInstance   -   user provided service 
eurekaServer        -   service registry service