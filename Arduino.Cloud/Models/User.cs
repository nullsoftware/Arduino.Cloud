using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class User
    {
        [JsonPropertyName("user_id")]
        public Guid Id { get; set; } = Guid.Empty;

        [JsonPropertyName("username")]
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
