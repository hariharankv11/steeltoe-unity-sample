using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using System;
using System.Web.Http;
using Unity;
using Unity.Microsoft.DependencyInjection;

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

            // add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
            _container.BuildServiceProvider(ApplicationConfig.Services);

            // start discovery client
            // https://github.com/SteeltoeOSS/Discovery/blob/dev/src/Steeltoe.Discovery.ClientAutofac/DiscoveryContainerBuilderExtensions.cs
            _container.Resolve<IDiscoveryClient>();

            // register and start management
            ManagementConfig.Register();
            ManagementConfig.Start();

        }


        protected void Application_End()
        {
            var client = _container.Resolve<IDiscoveryClient>();
            var logger = _container.Resolve<ILogger<WebApiApplication>>();

            logger.LogInformation("Shutting down!");

            // Unregister current app with Service Discovery server
            client.ShutdownAsync().Wait();

            // stop management
            ManagementConfig.Stop();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            Console.Error.WriteLine($"Application_Error: {exc}");

            Server.ClearError();
        }
    }
}
