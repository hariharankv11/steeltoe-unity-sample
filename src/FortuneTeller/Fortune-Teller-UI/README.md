## Fortune-Teller-UI

This sample use Unity for IoC services. It uses **Unity.Microsoft.DependencyInjection nuget package** to enable the app to move registrations frpm Microsoft DI container to Unity container. This app uses below below cloudfoundry services  

* Config server
* Discovery client
* Redis Server
* Health management
* Hystrix (in-progress)


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



## Endpoints to test 

<YOUR_PCF_APP_URL>/home/random  
<YOUR_PCF_APP_URL>/home/cached  


## Building and Running the app

**Create and Bind these services:**  

| Bounded Service Name | PCF Service |
| --- | --- |
| coreConfigServer | config server service  
| redisInstance | redis service   
| eurekaServer | service registry service  
| hystrixService | circuit breaker service  

## Things in progress
Could not get the HystrixCommand to work. Commented out code blocks. I will address this later.
