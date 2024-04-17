using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Cloud
{
    internal class QueryBuilder : List<KeyValuePair<string, string>>
    {
        public void Add(string name, string? value)
        {
            Add(new KeyValuePair<string, string>(name, value ?? string.Empty));
        }

        public void Add(string name, bool value)
        {
            Add(name, value.ToString().ToLower());
        }

        public void AddIfNotNull(string name, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Add(name, value);
            }
        }

        public string Build(string url)
        {
            if (Count == 0)
            {
                return url;
            }
            else
            {
                return url + "?" + ToString();
            }
        }

        public override string ToString()
        {
            // Encode and concatenate data
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in this)
            {
                if (builder.Length > 0)
                {
                    // Not first, add a seperator
                    builder.Append('&');
                }

                builder.Append(Encode(pair.Key));
                builder.Append('=');
                builder.Append(Encode(pair.Value));
            }

            return builder.ToString();
        }

        private static string Encode(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return String.Empty;
            }
            // Escape spaces as '+'.
            return Uri.EscapeDataString(data).Replace("%20", "+");
        }

    }
}
