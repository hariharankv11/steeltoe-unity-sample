using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Diagnostics;
using System.Web.Mvc;

namespace SteeltoeUnityIoC.Controllers
{
    public class HomeController : Controller
    {

        //private IConfiguration _configuration = CoreServerConfig.GetService<IConfiguration>();
        //private IOptionsSnapshot<CloudFoundryServicesOptions> _cfServices = CoreServerConfig.GetService<IOptionsSnapshot<CloudFoundryServicesOptions>>();
        //private IConnectionMultiplexer _redisConnection = CoreServerConfig.GetService<IConnectionMultiplexer>();
        //private ILogger<HomeController> _logger = CoreServerConfig.GetService<ILogger<HomeController>>();

        private IConfiguration _configuration;
        private IOptionsSnapshot<CloudFoundryServicesOptions> _cfServices;
        private IConnectionMultiplexer _redisConnection;
        private ILogger<HomeController> _logger;
        private ITestClass _testClass;

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
            Debug.WriteLine($"spring app: {_configuration["spring:application:name"]}");
            Debug.WriteLine($"bounded services count: {_cfServices?.Value.ServicesList.Count}");
            Debug.WriteLine($"redis connection: {_redisConnection?.Configuration}");
            Debug.WriteLine((_redisConnection == null) ? "could not read connection using multiplexer" : "read connection using multiplexer");
            Debug.WriteLine($"testClass DoSomethingCrazy() method: {_testClass?.DoSomethingCrazy()}");
            
            
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            ViewBag.Message = $"spring app: {_configuration["spring:application:name"]}";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}