using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public class History
    {
        [JsonProperty("user")]
        public string? User { get; set; }

        [JsonProperty("bot")]
        public string? Bot { get; set; }
    }
}
