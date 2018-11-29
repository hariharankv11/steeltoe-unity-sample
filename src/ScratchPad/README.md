## Scratchpad application to test Unity integration

This is just a scratchpad application where I tried different solutions to load registrations from Microsoft DI container into Unity container. Solutions I tried
* Manually dump services by iterating through IServiceCollection **(did not work)**
* Developed Unity extension to intercept resolving action. This extension uses refelction to retrieve services from my custom static method, which in turn gets that service from Microsoft DI container **(worked as expected, but it broke actual MVC Unity pipeline. not sure why.)**
* Pull Unity.Microsoft.DependencyInjection package and piggy back on its extension method to load registrations **(worked like a charm)**
* Tried to extract code blocks of Unity.Microsoft.DependencyInjection extension, thinking of avoiding dependency on Unity.Microsoft.DependencyInjection. But code started becoming wonky since I had to pull multiple dependent classes. **(did not work)**


## References
https://github.com/unitycontainer/microsoft-dependency-injection  
https://github.com/unitycontainer/microsoft-dependency-injection/blob/master/src/ServiceProviderExtensions.cs


## Code blocks
`// add services to unity container. piggy backing on Unity.Microsoft.DependencyInjection extensions
container.BuildServiceProvider(_services);`
