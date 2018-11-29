using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using SteeltoeUnityIoC.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace SteeltoeUnityIoC.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private IOptionsSnapshot<CloudFoundryServicesOptions> _cfServices;
        private IConnectionMultiplexer _redisConnection;
        private ILogger<HomeController> _logger;
        private ITestClass _testClass;

        //// these were added to test the app when unity extension broke mvc unity

        //public HomeController() { }

        //public HomeController(
        //    ITestClass testClass
        //    )
        //{
        //    _testClass = testClass;
        //}

        public HomeController(
            IConfiguration configuration,
            IOptionsSnapshot<CloudFoundryServicesOptions> cfServices,
            IConnectionMultiplexer redisConnection,
            ILogger<HomeController> logger,
            ITestClass testClass
            )
        {
            _configuration = configuration;
            _cfServices = cfServices;
            _redisConnection = redisConnection;
            _logger = logger;
            _testClass = testClass;
        }

        public ActionResult Index()
        {
            //Debug.WriteLine($"spring app: {_configuration["spring:application:name"]}");
            //Debug.WriteLine($"bounded services count: {_cfServices?.Value.ServicesList.Count}");
            //Debug.WriteLine($"redis connection: {_redisConnection?.Configuration}");
            //Debug.WriteLine((_redisConnection == null) ? "could not read connection using multiplexer" : "read connection using multiplexer");
            //Debug.WriteLine($"testClass DoSomethingCrazy() method: {_testClass?.DoSomethingCrazy()}");


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ResolveServices()
        {
            ViewBag.Message = "Use this page to display resovled services information";

            var services = new Dictionary<string, string>();

            services.Add("IConfiguration :: spring app - ", _configuration["spring:application:name"]);
            services.Add("IOptionsSnapshot<CloudFoundryServicesOptions> :: bounded services count - ", _cfServices?.Value.ServicesList.Count.ToString());
            services.Add("IConnectionMultiplexer :: redis connection - ", _redisConnection?.Configuration);
            services.Add("IConnectionMultiplexer :: could read connection using multiplexer- ", (_redisConnection == null) ? "no" : "yes");
            services.Add("ITestClass :: DoSomethingCrazy() method - ", _testClass?.DoSomethingCrazy());

            var model = new ServicesViewModel();
            model.Services = services;
            return View(model);
        }
    }
}