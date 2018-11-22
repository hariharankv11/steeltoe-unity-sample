using Microsoft.Extensions.Logging;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace Fortune_Teller_UI
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
            ApplicationConfig.Register(Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT") ?? "Development");

            // build service provider for unity container
            ApplicationConfig.BuildServiceProvider(UnityConfig.Container);

            // start discovery client
            DiscoveryConfig.StartDiscoveryClient();

            // register and start management
            ManagementConfig.Register();
            ManagementConfig.Start();
        }

        protected void Application_End()
        {
            var logger = UnityConfig.Container.Resolve<ILogger<MvcApplication>>();
            logger.LogInformation("Shutting down!");

            // stop discovery client
            DiscoveryConfig.StopDiscoveryClient();

            // stop management
            ManagementConfig.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            Console.Error.WriteLine($"Application_Error: {exc}");

            Server.ClearError();
        }
    }
}
