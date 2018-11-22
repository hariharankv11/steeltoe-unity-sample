using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Diagnostics;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint;
using System.Collections.Generic;

namespace Fortune_Teller_UI
{
    public class ManagementConfig
    {
        public static void Register()
        {
            var configuration = ApplicationConfig.GetService<IConfiguration>();
            var dynamicLoggerProvider = ApplicationConfig.GetService<ILoggerProvider>();
            var healthContributors = ApplicationConfig.GetService<IEnumerable<IHealthContributor>>();
            var loggerFactory = ApplicationConfig.GetService<ILoggerFactory>();

            ActuatorConfigurator.UseCloudFoundryActuators(configuration, dynamicLoggerProvider,
                                                            healthContributors, null, loggerFactory);
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