using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public abstract class RequestBaseParam
    {
        /// <summary>
        /// 指明需要调用的模型
        /// </summary>
        public string Model { get; set; } = null;
    }
}
