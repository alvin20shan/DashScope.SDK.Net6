using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public abstract class RequestParam:RequestBaseParam
    { 
        public Dictionary<string, object>? Parameters { get; set; }=new Dictionary<string, object>();

        public bool IsSecurityCheck { get; set; } = false;

        public bool IsSSE { get; set; }= false;
         
        public abstract HttpContent GetHttpBody();

        public abstract HttpRequestMessage GetHttpRequestMessage(string apikey, HttpMethod httpMethod, string url, bool isAsyncTask);

        public abstract bool Verify();

   
    }
}
