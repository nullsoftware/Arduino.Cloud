using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class Property
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("last_value")]
        public object? LastValue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("thing_id")]
        public string ThingId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("permission")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Permission Permission { get; set; }

        [JsonPropertyName("persist")]
        public bool IsPersist { get; set; }

        [JsonPropertyName("tag")]
        public long Tag { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PropertyType PropertyType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("value_updated_at")]
        public DateTime? ValueUpdatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("update_parameter")]
        public double? UpdateParameter { get; set; }

        [JsonPropertyName("update_strategy")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UpdateStrategy UpdateStrategy { get; set; }

        [JsonPropertyName("variable_name")]
        public string VariableName { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("min_value")]
        public double? MinValue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("max_value")]
        public double? MaxValue { get; set; }

        public override string ToString()
        {
            return $"Property: {Name}";
        }
    }
}
