using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public class Widget
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;


        [JsonPropertyName("options")]
        public WidgetOptions? Options { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;


        [JsonPropertyName("has_permission_incompatibility")]
        public bool HasPermissionIncompatibility { get; set; }

        [JsonPropertyName("has_type_incompatibility")]
        public bool HasTypeIncompatibility { get; set; }

        [JsonPropertyName("variables")]
        public List<Property> Variables { get; set; } = new List<Property>();


        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }


        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("x_mobile")]
        public int MobileX { get; set; }

        [JsonPropertyName("y_mobile")]
        public int MobileY { get; set; }


        [JsonPropertyName("width_mobile")]
        public int MobileWidth { get; set; }

        [JsonPropertyName("height_mobile")]
        public int MobileHeight { get; set; }

        public override string ToString()
        {
            return $"Widget: {Name}";
        }
    }
}
