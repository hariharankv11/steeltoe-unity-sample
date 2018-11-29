using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Fortune_Teller_UI.Services
{
    public class FortuneServiceCommand : HystrixCommand<Fortune>
    {
        IFortuneService _fortuneService;
        ILogger<FortuneServiceCommand> _logger;

        public FortuneServiceCommand(
            IHystrixCommandOptions options, 
            IFortuneService fortuneService, 
            ILogger<FortuneServiceCommand> logger) : base(options)
        {
            _fortuneService = fortuneService;
            _logger = logger;
            IsFallbackUserDefined = true;
        }
        public async Task<Fortune> RandomFortune()
        {
            return await ExecuteAsync();
        }
        protected override async Task<Fortune> RunAsync()
        {
            var result = await _fortuneService.RandomFortuneAsync();
            _logger.LogInformation("Run: {0}", result);
            return result;
        }

        protected override async Task<Fortune> RunFallbackAsync()
        {
            _logger.LogInformation("RunFallback");
            //TODO: return cached data when the request was made to get by a specific fortuneId
            //      refer to firtune service setting and getting cache data
            return await Task.FromResult<Fortune>(new Fortune() { Id = 9999, Text = "You will have a happy day!" });
        }
    }
}
