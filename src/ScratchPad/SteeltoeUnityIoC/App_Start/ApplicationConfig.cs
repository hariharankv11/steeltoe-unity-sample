using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector.Redis;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.Collections.Generic;
using Unity.Microsoft.DependencyInjection;

namespace SteeltoeUnityIoC
{
    public class ApplicationConfig
    {
        private static IHost _host;
        //private static IServiceCollection _services;

        public static T GetService<T>()
        {
           return _host.Services.GetService<T>();
        }

        //// return registrations
        //public static Dictionary<Type, object> Registrations()
        //{
        //    Dictionary<Type, object> services = new Dictionary<Type, object>();
        //    foreach (var service in _services)
        //    {
        //        services[service.ServiceType] = service.ImplementationInstance;
        //    }

        //    return services;
        //}

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

                        //// assign servicescollection
                        //_services = services;


                        // add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
                        // https://github.com/unitycontainer/microsoft-dependency-injection
                        // https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/ServiceProviderExtensions.cs

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
                    })
                    .Build();
        }
    }
}