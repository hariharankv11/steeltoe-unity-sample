# steeltoe-unity-sample
.net full framework apps with unity and steeltoe libraries


# code blocks

// references
// https://github.com/unitycontainer/microsoft-dependency-injection
// https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/ServiceProviderExtensions.cs


// add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
container.BuildServiceProvider(_services);