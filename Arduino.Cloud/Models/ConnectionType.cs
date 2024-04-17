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
        ETH,
        WiFiAndSecret,
        GSM, 
        NB, 
        Lora
    }
}
