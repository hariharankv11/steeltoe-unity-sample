using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Pivotal.Discovery.Client;
using Pivotal.Extensions.Configuration.ConfigServer;
using Steeltoe.CloudFoundry.Connector.SqlServer;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Logging;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace Fortune_Teller_Service
{
    public class ApplicationConfig
    {
        private static IHost _host;
        private static IServiceCollection _services;
        
        public static void Register(string environment)
        {
            ILoggerFactory factory = new LoggerFactory();
            factory.AddProvider(new ConsoleLoggerProvider((category, logLevel) => logLevel >= LogLevel.Debug, false));

            _host =
                new HostBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        //Add your configurations as needed
                        config.AddJsonFile("appSettings.json", optional: true);
                        config.AddJsonFile($"appSettings.{environment}.json", optional: true);
                        config.AddEnvironmentVariables();
                        config.AddConfigServer(environment, factory);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        //Add your services as needed including steeltoe extensions
                        services.AddOptions();
                        services.AddLogging();
                        services.ConfigureCloudFoundryOptions(hostContext.Configuration);
                        services.AddSqlServerConnection(hostContext.Configuration, logFactory: factory);
                        services.AddDiscoveryClient(hostContext.Configuration);

                        // assign service collection to local place holder
                        _services = services;
                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddDynamicConsole(hostingContext.Configuration);
                    })
                    .Build();                    
        }

        public static T GetService<T>()
        {
            return _host.Services.GetService<T>();
        }

        public static void BuildServiceProvider(IUnityContainer container)
        {
            // add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
            container.BuildServiceProvider(_services);
        }
    }
}