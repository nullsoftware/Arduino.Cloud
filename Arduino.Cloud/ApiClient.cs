using Arduino.Cloud.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using Arduino.Cloud.Exceptions;
using System.Text;

namespace Arduino.Cloud
{
    public class ApiClient
    {
        private readonly HttpClient _client;
        private readonly IApiClientSettings _settings;
        private readonly ICredentialsProvider _credentialsProvider;

        private TokenInfo? _token;

        public ApiClient(ICredentialsProvider credentialsProvider, IApiClientSettings? settings = null, HttpClient? client = null) 
        {
            _credentialsProvider = credentialsProvider;
            _settings = settings ?? ApiClientSettings.Default;
            _client = client ?? new HttpClient();
        }

        #region Api Methods

        public async Task<List<Dashboard>> GetDashboardList(string? name = null, Guid? userId = null)
        {
            await VerifyAccessToken();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, CreateFullUrl("/v2/dashboards"));
            Dictionary<string, string> content = new Dictionary<string, string>();

            if (name != null)
            {
                content.Add("name", name);
            }
            if (userId != null)
            {
                content.Add("user_id", userId.Value.ToString());
            }

            request.Content = new FormUrlEncodedContent(content);

            using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Dashboard>>(json)!;
            }
        }

        public async Task<List<DeviceInfo>> GetDeviceList(bool acrossUserIds, string? serial = null, string? tags = null)
        {
            await VerifyAccessToken();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, CreateFullUrl("/v2/devices"));
            Dictionary<string, string> content = new Dictionary<string, string>()
            {
                ["across_user_ids"] = acrossUserIds.ToString().ToLower()
            };

            if (serial != null)
            {
                content.Add("serial", serial);
            }
            if (tags != null)
            {
                content.Add("tags", tags);
            }

            request.Content = new FormUrlEncodedContent(content);

            using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DeviceInfo>>(json)!;
            }
        }

        public async Task<DeviceInfo> GetDevice(Guid deviceId)
        {
            await VerifyAccessToken();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, CreateFullUrl($"/v2/devices/{deviceId}"));

            using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DeviceInfo>(json)!;
            }
        }

        public async Task<Property> GetProperty(Guid propertyId, Guid thingId)
        {
            await VerifyAccessToken();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, CreateFullUrl($"/v2/things/{thingId}/properties/{propertyId}"));

            using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Property>(json)!;
            }
        }

        public async Task PublishValue(Guid propertyId, Guid thingId, PropertyPayload payload)
        {
            await VerifyAccessToken();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, CreateFullUrl($"/v2/things/{thingId}/properties/{propertyId}/publish"));
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);                
            }
        }

        #endregion

        protected async Task<TokenInfo> GetToken()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenAcquireUri);
            Dictionary<string, string> content = new Dictionary<string, string>()
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _credentialsProvider.GetClientId(),
                ["client_secret"] = _credentialsProvider.GetClientSecret(),
                ["audience"] = _settings.Audience
            };
            request.Content = new FormUrlEncodedContent(content);
            using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TokenInfo>(json)!;
            }

        }

        protected async Task VerifyAccessToken()
        {
            if (_token == null || _token.IsExpired)
            {
                _token = await GetToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token.TokenType, _token.AccessToken);
            }
        }

        protected virtual void EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.TooManyRequests: 
                    throw new RequestLimitReachedException("You have reached maximum request limit.");

                default:
                    response.EnsureSuccessStatusCode();
                    break;
            }
        }

        protected virtual string CreateFullUrl(string relativeUrl)
        {
            return _settings.BaseUrl + relativeUrl;
        }
    }
}