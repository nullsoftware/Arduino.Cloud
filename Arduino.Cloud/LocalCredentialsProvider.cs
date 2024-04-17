using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    public class LocalCredentialsProvider : ICredentialsProvider
    {
        private string _clientId;
        private string _clientSecret;

        public LocalCredentialsProvider(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public string GetClientId()
        {
            return _clientId;
        }

        public string GetClientSecret()
        {
            return _clientSecret;
        }


    }
}
