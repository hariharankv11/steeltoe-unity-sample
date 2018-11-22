# steeltoe-unity-sample
.net full framework apps with unity and steeltoe libraries


# Load Microsoft DI registrations into Unity container

// references
// https://github.com/unitycontainer/microsoft-dependency-injection
// https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/ServiceProviderExtensions.cs


// add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
container.BuildServiceProvider(_services);


# Start Discovery client

// references
// https://github.com/SteeltoeOSS/Discovery/blob/dev/src/Steeltoe.Discovery.ClientAutofac/DiscoveryContainerBuilderExtensions.cs
    
public static void StartDiscoveryClient()
{
    UnityConfig.Container.Resolve<IDiscoveryClient>();
}


# Start Management endpoints

// references
// alfus's springone demo
// https://github.com/SteeltoeOSS/Samples/blob/dev/Management/src/AspDotNet4/CloudFoundryWeb/App_Start/ManagementConfig.cs
// https://github.com/SteeltoeOSS/Management/blob/dev/src/Steeltoe.Management.EndpointWeb/ActuatorConfigurator.cs

ActuatorConfigurator.UseCloudFoundryActuators(configuration, dynamicLoggerProvider,
                                                            healthContributors, null, loggerFactory);


 # required nuget packages

 for fortune service

Microsoft.Extensions.Hosting
Microsoft.Extensions.Logging.Console
Unity.Aspnet.Webapi 
Unity.Microsoft.DependencyInjection

Pivotal.Extensions.Configuration.ConfigServercore
Pivotal.Discovery.ClientCore
Steeltoe.Management.EndpointWeb
Steeltoe.CloudFoundry.Connectorcore
System.Data.SqlClient


for fortune UI

Microsoft.Extensions.Hosting
Microsoft.Extensions.Logging.Console
Unity.Mvc
Unity.Microsoft.DependencyInjection

Pivotal.Extensions.Configuration.ConfigServercore
Pivotal.Discovery.ClientCore
Steeltoe.Management.EndpointWeb
Steeltoe.CloudFoundry.Connectorcore

StackExchange.Redis
Steeltoe.CircuitBreaker.HystrixCore



# endpoints to test 

/home/random
/home/cached
