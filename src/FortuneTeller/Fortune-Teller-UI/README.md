## Fortune-Teller-UI

This sample use Unity for IoC services. This app discovers Fortune-Teller-Service and consumes its endpoints. Cloufoundry services used in this app - 

* Config server
* Discovery client
* Redis Server
* Health management
* Hystrix (in-progress)


## Pre-requisites

1. Installed Pivotal CloudFoundry with Windows support
1. Installed Spring Cloud Services
1. Visual Studio 2017
1. ASP.NET app tragets .Net Framework 4.6.1 or above
1. ASP.NET MVC project use Unity.Mvc nuget


 ## Required nuget packages

Microsoft.Extensions.Hosting  
Microsoft.Extensions.Logging.Console  
Unity.Aspnet.Webapi   
Unity.Microsoft.DependencyInjection  

Pivotal.Extensions.Configuration.ConfigServercore  
Pivotal.Discovery.ClientCore  
Steeltoe.Management.CloudFoundryCore   
Steeltoe.CloudFoundry.Connectorcore  

StackExchange.Redis  
Steeltoe.CircuitBreaker.HystrixCore  


## Setup Cloudfoundry Services

You must first create instances of the Config Server service, Service Registry service, Redis service, Circuit Breaker dashboard in a org/space.

1. cf target -o YOUR_ORG -s YOUR_SPACE
1. cf create-service p-config-server standard coreConfigServer -c ./config-server.json
1. cf create-service p-service-registry standard eurekaServer
1. cf create-service p.redis cache-small redisInstance
1. cf create-service p-circuit-breaker-dashboard standard hystrixService



## Publish App & Push

1. Open SteeltoeUnitySampleApps.sln in Visual Studio.
1. Select src/FortuneTeller/Fortune-Teller-UI project in Solution Explorer.
1. Right-click and select Publish
1. Publish the App to a folder (bin/Debug/net472/win10-x64/publish)
1. cd src/FortuneTeller/Fortune-Teller-Service
1. cf push (publish folder path was set in manifest.yml)


## Endpoints to test 

<YOUR_PCF_APP_URL>/home/random  
<YOUR_PCF_APP_URL>/home/cached  


## Things in progress
Could not get the HystrixCommand to work. Commented out code blocks. I will address this later.
