using DashScope.SDK.Net6.Models;
using DashScope.SDK.Net6.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6
{
    public  class QwenClient
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;

        public QwenClient(string apiKey, HttpClient? httpClient=null)
        {
            _httpClient = httpClient??HttpClientProvider.CreateClient();
            _apiKey = apiKey;
        }

        /// <summary>
        /// 通义千问   /aigc/text-generation/generation
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public Task<ApiOutputResult> QwenCallAsync(ChatRequestParam requestParam) => this.QwenCallAsync(requestParam, new QwenEndpoint());


        /// <summary>
        /// 通用文本向量
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public Task<ApiOutputResult> EmbeddingsCallAsync(ChatRequestParam requestParam) => this.QwenCallAsync(requestParam, new EmbeddingsEndpoint());


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParam"></param>
        /// <param name="endpoint">配置不同的api url  /{task}/{taskgroup}/{function}</param>
        /// <param name="isMultipleConversation">是否是多伦对话</param>
        /// <param name="isAsycn"></param>
        /// <returns></returns>
        public async Task<ApiOutputResult> QwenCallAsync(ChatRequestParam requestParam, Endpoint endpoint, bool isMultipleConversation = false)
        {
            if (requestParam == null)
                return null;

            if (!requestParam.Verify())
                return null;

            requestParam = requestParam.BuildParam();

            if (isMultipleConversation)
            {

            }

            var httprequest = requestParam.GetHttpRequestMessage(_apiKey, HttpMethod.Post, Defaults.Endpoint(endpoint));

            var response = await _httpClient.SendAsync(httprequest);

            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiOutputResult>(responseJson);

            if (isMultipleConversation)
            {
                requestParam.BuildHistory(result);
            }
            return result;
        }



    }
}
