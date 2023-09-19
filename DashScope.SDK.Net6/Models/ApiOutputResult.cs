using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public class ApiOutputResult
    {
        [JsonProperty("usage")]
        public OutputUsage? Usage { get; set; }
        [JsonProperty("output")]
        public Output? Output { get; set; }

        [JsonProperty("request_id")]
        public string? RequestId { get; set; }
    }
}
