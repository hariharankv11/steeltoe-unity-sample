using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using Steeltoe.Common.Discovery;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fortune_Teller_UI.Services
{
    public class FortuneService : IFortuneService
    {
        DiscoveryHttpClientHandler _handler;

        private string RANDOM_FORTUNE_URL = "http://fortuneServiceUnity/api/fortunes/random";
        private const string CACHED_ITEM_KEY = "CACHED_FORTUNE";
        private ILogger<FortuneService> _logger;
        private IConnectionMultiplexer _cache;

        public FortuneService(
            IConfiguration config,
            IDiscoveryClient client, 
            IConnectionMultiplexer cache,
            ILogger<FortuneService> logger)
        {
            RANDOM_FORTUNE_URL = config["RANDOM_FORTUNE_URL"];
            _handler = new DiscoveryHttpClientHandler(client);
            _logger = logger;
            _cache = cache;
        }

        public async Task<Fortune> RandomFortuneAsync()
        {
            _logger?.LogInformation("RandomFortuneAsync");
            var client = GetClient();

            var result = await client.GetStringAsync(RANDOM_FORTUNE_URL);
            _logger.LogInformation("RandomFortuneAsync: {0}", result);
           
            return JsonConvert.DeserializeObject<Fortune>(result);
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient(_handler, false);
            return client;
        }


        #region redis cache sample

        public async Task<Fortune> GetFortuneInCacheAsync()
        {
            var result = await GetCacheItem(CACHED_ITEM_KEY);
            _logger.LogInformation("CachedRandomFortuneAsync: {0}", result);


            if (result == null) return null;
            return JsonConvert.DeserializeObject<Fortune>(result);
        }


        public async Task SetFortuneInCacheAsync()
        {
            _logger?.LogInformation("RandomFortuneAsync");

            if (!await IsItemCached(CACHED_ITEM_KEY))
            {
                var fortune = await RandomFortuneAsync();
                var result = JsonConvert.SerializeObject(fortune);
                await SetCacheItem(CACHED_ITEM_KEY, result);
            }
        }

        private async Task SetCacheItem(string key, string value)
        {
            _logger.LogInformation("Writing result to cache");
            await _cache.GetDatabase().StringSetAsync(key, value);
        }

        private async Task<string> GetCacheItem(string key)
        {
            _logger.LogInformation("Reading result from cache");
            return await _cache.GetDatabase().StringGetAsync(key);
        }

        private async Task<bool> IsItemCached(string key)
        {
            var result = await GetCacheItem(CACHED_ITEM_KEY);
            return !string.IsNullOrEmpty(result);
        }


        #endregion
    }
}
