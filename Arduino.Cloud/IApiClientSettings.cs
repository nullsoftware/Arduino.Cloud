﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    // [TODO]: reafactor this
    public interface IApiClientSettings
    {
        string BaseUrl { get; }

        string TokenAcquireUri { get; }

        string Audience { get; }
    }
}
