using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arduino.Cloud.Models
{
    public enum ConnectionType : short
    {
        WiFi, 
        ETH, // Ethernet
        WiFiAndSecret,
        GSM, // Mobile
        NB, // Narrowband
        LoRa // LoRa (from "long range") is a physical proprietary radio communication technique.
    }
}
