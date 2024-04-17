using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class PropertyPayload
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [JsonPropertyName("value")]
        public object Value { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("device_id")]
        public string? DeviceId { get; }


        public PropertyPayload(object value, string? deviceId = null)
        {
            Value = value;
            DeviceId = deviceId;
        }

        public override string ToString()
        {
            return $"Property Payload: {Value}";
        }
    }
}
