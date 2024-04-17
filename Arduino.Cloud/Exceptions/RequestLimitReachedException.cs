using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud.Exceptions
{
    public class RequestLimitReachedException : Exception
    {
        public RequestLimitReachedException(string message) : base(message) 
        {

        }
    }
}
