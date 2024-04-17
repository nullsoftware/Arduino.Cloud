using Arduino.Cloud.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using Arduino.Cloud.Exceptions;
using System.Text;
using System.Text.Encodings.Web;

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

            Dictionary<string, string> content = new Dictionary<string, string>();
            QueryBuilder bld = new QueryBuilder();
            bld.AddIfNotNull("name", name);
            bld.AddIfNotNull("user_id", userId?.ToString());

            using (HttpResponseMessage response = await _client.GetAsync(
                bld.Build(CreateFullUrl("/v2/dashboards")), HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Dashboard>>(json)!;
            }
        }

        /// <summary>
        /// Returns the list of devices associated to the user.
        /// </summary>
        /// <param name="acrossUserIds">If true, returns all the devices.</param>
        /// <param name="serial">Filter by device serial number.</param>
        /// <param name="tags">Filter by tags.</param>
        /// <returns>The list of devices associated to the user.</returns>
        public async Task<List<DeviceInfo>> GetDeviceList(bool? acrossUserIds = null, string? serial = null, string? tags = null)
        {
            await VerifyAccessToken();

            QueryBuilder bld = new QueryBuilder();
            bld.AddIfNotNull("across_user_ids", acrossUserIds);
            bld.AddIfNotNull("serial", serial);
            bld.AddIfNotNull("tags", tags);
            
            using (HttpResponseMessage response = await _client.GetAsync(
                bld.Build(CreateFullUrl("/v2/devices")), HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DeviceInfo>>(json)!;
            }
        }

        public async Task<DeviceInfo> GetDevice(Guid deviceId)
        {
            await VerifyAccessToken();

            using (HttpResponseMessage response = await _client.GetAsync(CreateFullUrl($"/v2/devices/{deviceId}"), HttpCompletionOption.ResponseContentRead))
            {
                EnsureSuccessStatusCode(response);

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DeviceInfo>(json)!;
            }
        }

        public async Task<Property> GetProperty(Guid propertyId, Guid thingId)
        {
            await VerifyAccessToken();

            using (HttpResponseMessage response = await _client.GetAsync(
                CreateFullUrl($"/v2/things/{thingId}/properties/{propertyId}"), 
                HttpCompletionOption.ResponseContentRead))
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