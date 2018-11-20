using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace SteeltoeUnityIoC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // register microsoft & steeltoe services
            CoreServerConfig.Register(Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT") ?? "Development");

            // test microsoft di container
            Debug.WriteLine("getting service from CoreServerConfig");

            IConfiguration _configuration = CoreServerConfig.GetService<IConfiguration>();
            IOptionsSnapshot<CloudFoundryServicesOptions> _cfServices = CoreServerConfig.GetService<IOptionsSnapshot<CloudFoundryServicesOptions>>();
            IConnectionMultiplexer redisConnection = CoreServerConfig.GetService<IConnectionMultiplexer>();
            ILogger<MvcApplication> _logger = CoreServerConfig.GetService<ILogger<MvcApplication>>();

            Debug.WriteLine($"spring app: {_configuration["spring:application:name"]}");
            Debug.WriteLine($"bounded services count: {_cfServices?.Value.ServicesList.Count}");
            Debug.WriteLine($"redis connection: {redisConnection?.Configuration}");
            Debug.WriteLine((redisConnection == null) ? "could not read connection using multiplexer" : "read connection using multiplexer");


            //// test unity extension 
            //Debug.WriteLine("getting service from UnityConfig.Container");

            //_configuration = UnityConfig.Container.Resolve<IConfiguration>();
            //Debug.WriteLine($"spring app: {_configuration["spring:application:name"]}");

            //redisConnection = UnityConfig.Container.Resolve<IConnectionMultiplexer>();
            //Debug.WriteLine($"redis connection: {redisConnection?.Configuration}");
            //Debug.WriteLine((redisConnection == null) ? "could not read connection using multiplexer" : "read connection using multiplexer");

            //_logger = UnityConfig.Container.Resolve<ILogger<MvcApplication>>();

            //_cfServices = UnityConfig.Container.Resolve<IOptionsSnapshot<CloudFoundryServicesOptions>>();
            //Debug.WriteLine($"bounded services count: {_cfServices?.Value.ServicesList.Count}");
        }
    }
}
