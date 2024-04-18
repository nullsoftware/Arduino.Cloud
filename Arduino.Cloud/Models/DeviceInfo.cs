using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class DeviceInfo
    {
        [JsonPropertyName("connection_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConnectionType ConnectionType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("device_status")]
        public string? Status { get; set; }

        public bool IsOnline => Status == "ONLINE";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("events")]
        public List<DeviceEvent>? Events { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("fqbn")]
        public string FullyQualifiedBoardName { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("last_activity_at")]
        public DateTime? LastActivityAt { get; set; }

        [JsonPropertyName("serial")]
        public string Serial { get; set; } = string.Empty;

        [JsonPropertyName("thing")]
        public Thing? Thing { get; set; }

        [JsonPropertyName("type")]
        public string DeviceType { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; }

        /// <summary>
        /// The version of the NINA/WIFI101 firmware running on the device.
        /// </summary>
        [JsonPropertyName("wifi_fw_version")]
        public string? WiFiFirmwareVersion { get; set; }


        public override string ToString()
        {
            return $"DeviceInfo: {Name}";
        }
    }
}
