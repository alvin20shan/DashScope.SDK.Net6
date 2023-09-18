using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public class OutputChoice
    {
        [JsonProperty("finish_reason")]
        public string? FinishReason { get; set; }
        [JsonProperty("message")]
        public Message? Message { get; set; }
    }
}
