using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector.Redis;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Unity.Microsoft.DependencyInjection;

namespace SteeltoeUnityIoC
{
    public class CoreServerConfig
    {
        private static IHost _host;

        public static T GetService<T>()
        {

            return _host.Services.GetService<T>();
        }

        
        public static void Register(string environment)
        {
            _host =
                new HostBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        //Add your configurations as needed
                        config.AddJsonFile("appSettings.json", optional: true);
                        config.AddJsonFile($"appSettings.{environment}.json", optional: true);
                        config.AddEnvironmentVariables();
                        config.AddCloudFoundry();
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        //Add your services as needed including steeltoe extensions
                        services.AddOptions();
                        services.AddLogging();
                        services.ConfigureCloudFoundryOptions(hostContext.Configuration);
                        services.AddRedisConnectionMultiplexer(hostContext.Configuration);
                        //services.AddDistributedRedisCache(hostContext.Configuration);

                        // add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
                        // https://github.com/unitycontainer/microsoft-dependency-injection
                        // https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/ServiceProviderExtensions.cs

                        // Unity.Microsoft.DependencyInjection package is pulling Microsoft.NETCore.App and Microsoft.AspNetCore.Hosting.Abstractions. 
                        // Though we are not using any aspnet core hosting model, not sure if there will be any performance implications in terms of 
                        // cf push artifacts or full framework app execution. 

                        // see which one OF these notations make good sense
                        //services.BuildServiceProvider(UnityConfig.Container);
                        UnityConfig.Container.BuildServiceProvider(services);

                        // See if we can extract out AddServices method in below class. Note: TRIED DOING THIS. SOMETHING BROKE. LETS PULL THE PACKAGE. 
                        // https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/Configuration.cs
                        //UnityConfig.Container.AddExtension(new MdiExtension())
                        //        .AddServices(services);


                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {

                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        //logging.AddDynamicConsole(hostingContext.Configuration);
                    })
                    .Build();
        }
    }
}