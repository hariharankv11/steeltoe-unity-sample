
using Fortune_Teller_Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Web.Http;

namespace Fortune_Teller_Service.Controllers
{
    public class FortunesController : ApiController
    {
        private IFortuneRepository _fortunes;
        private ILogger<FortunesController> _logger;

        public FortunesController(
            IFortuneRepository fortunes, 
            ILogger<FortunesController> logger)
        {
            //_fortunes = new FortuneRepository(SampleData.FortuneContext);
            _fortunes = fortunes;
            _logger = logger;
        }

        // GET: api/fortunes
        [HttpGet]
        [Route("api/fortunes")]
        public IEnumerable<Fortune> Get()
        {
            _logger?.LogInformation("api/fortunes");
            return _fortunes.GetAll();
        }

        // GET api/fortunes/random
        [HttpGet]
        [Route("api/fortunes/random")]
        public IHttpActionResult Random()
        {
            _logger?.LogInformation("api/fortunes/random");
            return Ok(_fortunes.RandomFortune());
        }
    }
}
