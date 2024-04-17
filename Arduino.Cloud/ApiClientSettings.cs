using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    public class ApiClientSettings : IApiClientSettings
    {
        public const string DefaultBaseUrl = "https://api2.arduino.cc/iot";
        public const string DefaultTokenAcquireUri = "https://api2.arduino.cc/iot/v1/clients/token";

        internal readonly static ApiClientSettings Default = new ApiClientSettings(DefaultBaseUrl, DefaultTokenAcquireUri, DefaultBaseUrl);


        public string BaseUrl { get; }

        public string TokenAcquireUri { get; }

        public string Audience { get; }


        public ApiClientSettings(string baseUrl, string tokenAcquireUri, string audience)
        {
            BaseUrl = baseUrl;
            TokenAcquireUri = tokenAcquireUri;
            Audience = audience;
        }
    }
}
