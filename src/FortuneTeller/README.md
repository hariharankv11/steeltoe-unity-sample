## ASP.NET 4.x Samples with Unity and Steeltoe

* **src/FortuneTeller/Fortune-Teller-Service** - provides endpoints to get fortune(s) from in-memory collection. It use config server, connect to a MS SQL database on Azure, use Discovery client, add health management.
* **src/FortuneTeller/Fortune-Teller-UI** - discovers Fortune-Teller-Service and consumes its endpoints. It use config server, connect to Fortune-Teller-Service using Discovery client, add health management, connect to Redis server on CloudFoundry.


