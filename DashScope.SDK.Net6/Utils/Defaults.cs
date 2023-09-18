
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Utils
{
    public  class Defaults
    {
         
        public static string Endpoint(string task,string taskgroup,string funciton) => $"https://dashscope.aliyuncs.com/api/v1/services/{task}/{taskgroup}/{funciton}";

        public static string Endpoint(Endpoint endpoint) => $"https://dashscope.aliyuncs.com/api/v1/services/{endpoint.Task}/{endpoint.TaskGroup}/{endpoint.Function}";
    }
}
