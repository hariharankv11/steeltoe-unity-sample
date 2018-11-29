using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Diagnostics;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint;
using System.Collections.Generic;
using System.Web.Http;

namespace Fortune_Teller_Service
{   
    public class ManagementConfig
    {
        public static void Register()
        {
            var configuration = ApplicationConfig.GetService<IConfiguration>();
            var dynamicLoggerProvider = ApplicationConfig.GetService<ILoggerProvider>();
            var healthContributors = ApplicationConfig.GetService<IEnumerable<IHealthContributor>>();
            var loggerFactory = ApplicationConfig.GetService<ILoggerFactory>();
            var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();

            ActuatorConfigurator.UseCloudFoundryActuators(configuration, dynamicLoggerProvider,
                                                            healthContributors, apiExplorer, loggerFactory);
        }

        public static void Start()
        {
            DiagnosticsManager.Instance.Start();
        }

        public static void Stop()
        {
            DiagnosticsManager.Instance.Stop();
        }

    }
}