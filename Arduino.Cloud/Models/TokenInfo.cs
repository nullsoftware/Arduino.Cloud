using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class TokenInfo
    {
        /// <summary>
        /// Value of token.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Token lifetime in seconds.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Token type.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// Token creation date.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAt { get; } = DateTime.Now;

        /// <summary>
        /// Gets a value that indicates if token is expired.
        /// </summary>
        public bool IsExpired => (CreatedAt.AddSeconds(ExpiresIn) - DateTime.Now) <= TimeSpan.Zero;
    }
}
