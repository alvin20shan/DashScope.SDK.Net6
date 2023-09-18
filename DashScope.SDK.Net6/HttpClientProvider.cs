using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6
{
    public class HttpClientProvider
    {
     
        public static HttpClient CreateClient()
        {
            return new HttpClient();
        } 
         
    }
}
