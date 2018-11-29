## ASP.NET 4.x Samples

* src/FortuneTeller/Fortune-Teller-Service - use config server, connect to a MS SQL database on Azure, use Discovery client, add health management
* src/FortuneTeller/Fortune-Teller-UI - use config server, connect to Fortune-Teller-Service using Discovery client, add health management, connect to Redis server on CloudFoundry



## Load Microsoft DI registrations into Unity container

### References
https://github.com/unitycontainer/microsoft-dependency-injection
https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/ServiceProviderExtensions.cs

## Code blocks
`// add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
container.BuildServiceProvider(_services);`


## Start Discovery client

### reference
https://github.com/SteeltoeOSS/Discovery/blob/dev/src/Steeltoe.Discovery.ClientAutofac/DiscoveryContainerBuilderExtensions.cs
    
### Code blocks
`public static void StartDiscoveryClient()
{
    UnityConfig.Container.Resolve<IDiscoveryClient>();
}`


## Start Management endpoints

### References
Alfus's springone demo  
https://github.com/SteeltoeOSS/Samples/blob/dev/Management/src/AspDotNet4/CloudFoundryWeb/App_Start/ManagementConfig.cs  
https://github.com/SteeltoeOSS/Management/blob/dev/src/Steeltoe.Management.EndpointWeb/ActuatorConfigurator.cs  

### Code blocks
`ActuatorConfigurator.UseCloudFoundryActuators(configuration, dynamicLoggerProvider,
                                                            healthContributors, null, loggerFactory);`

