using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fortune_Teller_Service.Controllers
{
    public class ContainerController : ApiController
    {
        IConfiguration _configuration;
        IOptionsSnapshot<CloudFoundryApplicationOptions> _cloudFoundryApplicationOptions;
        IDbConnection _dbConnection;
        ILogger<ContainerController> _logger;

        public ContainerController(
            IConfiguration configuration,
            IOptionsSnapshot<CloudFoundryApplicationOptions> cloudFoundryApplicationOptions,
            IDbConnection dbConnection,
            ILogger<ContainerController> logger
            )
        {
            _configuration = configuration;
            _cloudFoundryApplicationOptions = cloudFoundryApplicationOptions;
            _dbConnection = dbConnection;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/container")]
        public IEnumerable<Service> GetServices()
        {
            List<Service> services = new List<Service>();

            services.Add(new Service() { Name = "IConfiguration", Value = _configuration["REGISTRATION_SERVER_ENDPOINT"]});
            services.Add(new Service() { Name = "IOptionsSnapshot<CloudFoundryApplicationOptions>", Value = _cloudFoundryApplicationOptions.Value.Name });
            services.Add(new Service() { Name = "IDbConnection", Value = _dbConnection.ConnectionString });

            return services;
        }

        public class Service
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
