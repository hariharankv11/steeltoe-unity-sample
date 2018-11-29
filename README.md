# ASP.NET 4.x Sample Applications with Unity and Steeltoe 

This repo contains sample apps illustrating how to use the Steeltoe libraries for connecting to CloudFoundry services in your ASP.NET 4.x application using Unity IoC. It takes quite an effort to instrument full framework app to use some of the Steeltoe features. Autofac extensions are available for full framework to abstarct away that effort. But there are no Unity extensions availble. These sample apps illustarte how to use Steeltoe features with less effort. 

The concept is pretty simple - configure and add services to Micorsoft DI container and load them into Unity container. So you can use Unity for your apps the same way you do and can inject Steeltoe interfaces (and Unity IoC can resolve them). 


## ASP.NET 4.x Samples

* src/FortuneTeller/Fortune-Teller-Service - use config server, connect to a MS SQL database on Azure, use Discovery client, add health management
* src/FortuneTeller/Fortune-Teller-UI - use config server, connect to Fortune-Teller-Service using Discovery client, add health management, connect to Redis server on CloudFoundry
* src/ScratchPad/SteeltoeUnityIoC - sample application to demonstrate loading service registrations from Microsoft DI container to Unity container

**Target .Net Framework 4.6.1 or above, since we are making use of .Net Standard libraries**
