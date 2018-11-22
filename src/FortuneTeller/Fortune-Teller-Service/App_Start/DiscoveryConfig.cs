using Steeltoe.Common.Discovery;
using Unity;

namespace Fortune_Teller_Service
{
    public class DiscoveryConfig
    {
        public static void StartDiscoveryClient()
        {
            // reference
            // https://github.com/SteeltoeOSS/Discovery/blob/dev/src/Steeltoe.Discovery.ClientAutofac/DiscoveryContainerBuilderExtensions.cs
            UnityConfig.Container.Resolve<IDiscoveryClient>();

        }

        public static void StopDiscoveryClient()
        {
            var client = UnityConfig.Container.Resolve<IDiscoveryClient>();

            // Unregister current app with Service Discovery server
            client.ShutdownAsync().Wait();
        }

    }
}