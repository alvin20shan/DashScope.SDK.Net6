using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6
{
    public class RequestHeader
    {


        public static Dictionary<string, string> BuildRequestHeaders(string apiKey,  bool isSSE)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("Authorization", "Bearer " + apiKey);

            

            dictionary.Add("Content-Type", "application/json");
            if (isSSE)
            {
                dictionary.Add("Cache-Control", "no-cache");
                dictionary.Add("Accept", "text/event-stream");
                dictionary.Add("X-Accel-Buffering", "no");
                dictionary.Add("X-DashScope-SSE", "enable");
            }
            else
            {
                dictionary.Add("Accept", "application/json; charset=utf-8");
            } 
            return dictionary;
        }
    }
}
