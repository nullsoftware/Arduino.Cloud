using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class WidgetOptions
    {
        [JsonPropertyName("created_by")]
        public User? CreatedBy { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("lock")]
        public bool IsLocked { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; } = string.Empty;

        [JsonPropertyName("readOnly")]
        public bool IsReadOnly { get; set; }

        [JsonPropertyName("showThing")]
        public bool IsThingVisible { get; set; }

        [JsonPropertyName("thingId")]
        public Guid? ThingId { get; set; }
    }
}
