using Fortune_Teller_UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fortune_Teller_UI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        IFortuneService _fortunes;
        ILogger<HomeController> _logger;

        public HomeController(IFortuneService fortuneService, ILogger<HomeController> logger)
        {
            _fortunes = fortuneService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger?.LogInformation("Index");
            return View();
        }

        [HttpGet]
        [Route("random")]
        public async Task<string> Random()
        {
            _logger?.LogInformation("Random");
            return  await _fortunes.RandomFortuneAsync();
        }

        [HttpGet]
        [Route("cached")]
        public async Task<string> Cached()
        {
            _logger?.LogInformation("Cached");
            return await _fortunes.CachedRandomFortuneAsync();
        }
    }
}