﻿using Steeltoe.CircuitBreaker.Hystrix.MetricsStream;
using Steeltoe.Common.Discovery;
using Unity;

namespace Fortune_Teller_UI
{
    public class DiscoveryConfig
    {
        public static void StartDiscoveryClient()
        {
            // start discovery client
            UnityConfig.Container.Resolve<IDiscoveryClient>();

        }

        public static void StopDiscoveryClient()
        {
            var client = UnityConfig.Container.Resolve<IDiscoveryClient>();

            // Unregister current app with Service Discovery server
            client.ShutdownAsync().Wait();
        }

        public static void StartHystrixMetricsStream()
        {
            // Start the Hystrix Metrics stream 
            UnityConfig.Container.Resolve<RabbitMetricsStreamPublisher>();
        }

    }
}