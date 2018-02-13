using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LUISdemo01.ConsoleApp
{
    public class LuisClient : IDisposable
    {
        private readonly string _appId = "762ba9dc-63e2-477a-8b6a-363daadb2c73";

        private readonly string _subscriptionKey = "f0c0d8fd970b4dec88031b46da569c21";

        private readonly string _baseEndpoint = "https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/";

        private bool _isStaging;

        private HttpClient _client;

        public LuisClient()
            : this(false)
        {
            
        }

        public LuisClient(bool isStaging)
        {
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
