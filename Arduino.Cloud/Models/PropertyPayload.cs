using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    /// <summary>
    /// Represents a new value data for property change.
    /// </summary>
    public class PropertyPayload
    {
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [JsonPropertyName("value")]
        public object Value { get; }

        /// <summary>
        /// Gets or sets a value that indicates the device who send the property.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("device_id")]
        public Guid? DeviceId { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PropertyPayload"/> class.
        /// </summary>
        /// <param name="value">The property value.</param>
        /// <param name="deviceId">The device who send the property.</param>
        /// <remarks>
        /// If if you want to notify your device that property is updated/changed
        /// leave <paramref name="deviceId"/> null.
        /// </remarks>
        public PropertyPayload(object value, Guid? deviceId = null)
        {
            Value = value;
            DeviceId = deviceId;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Property Payload: {Value}";
        }
    }
}
