using Fortune_Teller_UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fortune_Teller_UI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        //FortuneServiceCommand _fortuneServiceCommand;
        IFortuneService _fortunes;
        ILogger<HomeController> _logger;

        public HomeController(
            //FortuneServiceCommand fortuneServiceCommand,
            IFortuneService fortuneService,
            ILogger<HomeController> logger)
        {
            //_fortuneServiceCommand = fortuneServiceCommand;
            _fortunes = fortuneService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger?.LogInformation("Index");
            return View();
        }

        //// this uses hystrix command, so we can provide fallback logic
        //[HttpGet]
        //[Route("random")]
        //public async Task<JsonResult> Random()
        //{
        //    var fortune = await _fortuneServiceCommand.RandomFortune();
        //    return Json(fortune, JsonRequestBehavior.AllowGet);
        //}


        // this is using service directly
        [HttpGet]
        [Route("random")]
        public async Task<JsonResult> Random()
        {
            var fortune = await _fortunes.RandomFortuneAsync();
            return Json(fortune, JsonRequestBehavior.AllowGet);
        }

        // this is using service directly
        [HttpGet]
        [Route("cached")]
        public async Task<JsonResult> Cached()
        {
            _logger?.LogInformation("Cached");

            await _fortunes.SetFortuneInCacheAsync();

            var fortune = await _fortunes.GetFortuneInCacheAsync();
            return Json(fortune, JsonRequestBehavior.AllowGet);
        }
    }
}