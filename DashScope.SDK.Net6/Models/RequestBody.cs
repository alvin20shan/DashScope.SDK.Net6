using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public class RequestBody
    {
        [JsonProperty("model")]
        public string? Model { get; set; }
        [JsonProperty("input")]
        public Input? Input { get; set; }

        [JsonProperty("parameters")]
        public Parameters Parameters { get; set; }
    }

    public class Input
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();

        [JsonProperty("prompt")]
        public string? Prompt { get; set; }
    }

    public class Parameters
    {
        [JsonProperty("result_format")]
        public string ResultFormat { get; set; } = ResultFormatType.TEXT;
        [JsonProperty("top_p")]
        public double TopP { get; set; }
        [JsonProperty("top_k")]
        public double TopK { get; set; }
        [JsonProperty("seed")]
        public int Seed { get; set; }

        [JsonProperty("enable_search")]
        public bool EnableSearch { get; set; }
    }

    public static class ResultFormatType
    {
        public static string TEXT = "text";
        public static string MESSAGE = "message";
    }

    public static class Models
    {
        public static readonly string QWEN_V1 = "qwen-v1";

        public static readonly string BAILIAN_V1 = "bailian-v1";

        public static readonly string DOLLY_12B_V2 = "dolly-12b-v2";

        public static readonly string QWEN_PLUS_V1 = "qwen-plus-v1";
    }
}
