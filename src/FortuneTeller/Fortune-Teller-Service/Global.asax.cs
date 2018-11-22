using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using System;
using System.Web.Http;
using Unity;

namespace Fortune_Teller_Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IUnityContainer _container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            _container = UnityConfig.Container;

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
            var logger = _container.Resolve<ILogger<WebApiApplication>>();
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
