using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    /// <summary>
    /// Provides a mechanism for retrieving necessary data to acquire access token.
    /// </summary>
    public interface ICredentialsProvider
    {
        /// <summary>
        /// Gets a <b>Client ID</b> string that is required to acquire access token.
        /// </summary>
        string GetClientId();

        /// <summary>
        /// Gets a <b>Client Secret</b> string that is required to acquire access token.
        /// </summary>
        string GetClientSecret();
    }
}
