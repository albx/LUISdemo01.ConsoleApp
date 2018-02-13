using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LUISdemo01.ConsoleApp
{
    public class LuisClient : IDisposable
    {
        private readonly string _appId;

        private readonly string _subscriptionKey;

        private readonly string _baseEndpoint;

        private bool _isStaging;

        private HttpClient _client;

        public LuisClient()
            : this(false)
        {
            
        }

        public LuisClient(bool isStaging)
        {
            var configuration = ConfigurationFactory.GetConfiguration();
            _appId = configuration["Luis:AppId"];
            _subscriptionKey = configuration["Luis:SubscriptionKey"];
            _baseEndpoint = configuration["Luis:BaseEndpoint"];

            _client = new HttpClient();
            _isStaging = isStaging;
        }

        public virtual async Task<string> SearchAsync(string query)
        {
            query = HttpUtility.UrlEncode(query);
            string endpoint = $"{_baseEndpoint}{_appId}?subscription-key={_subscriptionKey}&verbose=true&timezoneOffset=0&q={query}";
            if (_isStaging)
            {
                endpoint = $"{endpoint}&staging=true";
            }

            var response = await _client.GetAsync(endpoint);
            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
