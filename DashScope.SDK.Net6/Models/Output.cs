using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public class Output
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
        [JsonProperty("choices")]
        public List<OutputChoice>? Choices { get; set; }


        [JsonProperty("finish_reason")]
        public string? FinishReason { get; set; }
    }
}
