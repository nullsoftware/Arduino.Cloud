using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    /// <summary>
    /// Provides a mechanism for insecure local storage of data required to acquire access token.
    /// </summary>
    public class LocalCredentialsProvider : ICredentialsProvider
    {
        private string _clientId;
        private string _clientSecret;

        /// <summary>
        /// Initializes a new instance of <see cref="LocalCredentialsProvider"/>
        /// that will store necessary data locally as <see cref="string"/>.
        /// </summary>
        /// <param name="clientId">Arduino Cloud <b>Client ID</b>.</param>
        /// <param name="clientSecret">Arduino Cloud <b>Client Secret</b>.</param>
        public LocalCredentialsProvider(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <inheritdoc/>
        public string GetClientId()
        {
            return _clientId;
        }

        /// <inheritdoc/>
        public string GetClientSecret()
        {
            return _clientSecret;
        }
    }
}
