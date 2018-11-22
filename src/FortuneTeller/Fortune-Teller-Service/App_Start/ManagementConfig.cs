using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Diagnostics;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint;
using System.Collections.Generic;

namespace Fortune_Teller_Service
{
    // references
    // 1. big thanks to alfus. got this config from his springone demo
    // 2. tried digging into steeltoe libraries for ActuatorConfigurator and found this
    //    https://github.com/SteeltoeOSS/Management/blob/dev/src/Steeltoe.Management.EndpointWeb/ActuatorConfigurator.cs

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