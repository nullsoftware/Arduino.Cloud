using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    public interface ICredentialsProvider
    {
        string GetClientId();
        string GetClientSecret();
    }
}
