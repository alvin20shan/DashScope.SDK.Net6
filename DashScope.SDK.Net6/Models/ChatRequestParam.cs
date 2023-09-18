using DashScope.SDK.Net6.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Models
{
    public abstract class ChatRequestParam : RequestParam
    {
        /// <summary>
        /// 用户当前输入的期望模型执行指令，支持中英文。qwen-v1 prompt字段支持 1.5k Tokens 长度； qwen-plus-v1 prompt字段支持 6.5k Tokens 长度
        /// 使用Message格式时，也可以用此字段，但是输入会将该字段清空，内容会添加到Messages里面
        /// </summary>
        public string Prompt { get; set; } = null;
        /// <summary>
        /// 使用Message格式时,Prompt字段无内容，则对象不能为空
        /// </summary>
        public Message Message { get; set; }
        /// <summary>
        /// 用户与模型的对话历史，list中的每个元素是形式为{Role.User:"用户输入","bot":"模型输出"}的一轮对话，多轮对话按时间正序排列。
        /// </summary>
        public List<History>? History { get; set; }

        /// <summary>
        /// 用户与模型的对话历史，对话接口未来都会有message传输，不过prompt和history会持续兼容，list中的每个元素形式为{"role":角色, "content": 内容}。角色当前可选值：system、user、assistant。未来可以扩展到更多role。
        /// </summary>
        public List<Message>? Messages { get; set; }

        public string ResultFormat { get; set; } = ResultFormatType.TEXT;

        private Dictionary<string, object> Input
        {

            get
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>()
                {
                    {
                       FieldKeyNames.PROMPT, Prompt
                    }
                };

                if (Messages != null && Messages.Any())
                {
                    if (!string.IsNullOrEmpty(this.Prompt))
                    {
                        var msg = new Message { Content = Prompt, Role = Role.User };
                        if (Messages.Where(d => d.Content.Equals(msg.Content) && d.Role.Equals(msg.Role)).Count() <= 0)
                        {
                            Messages.Add(msg);
                        }
                        this.Prompt = "";//只允许一个参数。
                    }
                    else
                    {
                        if (Message != null)
                        {
                            Messages.Add(Message);
                        }
                    }
                    dictionary.Add(FieldKeyNames.MESSAGES, Messages);
                }
                else
                {
                    if (Messages == null)
                    {
                        Messages = new List<Message>();
                        Messages.Add(Message);
                        dictionary.Add(FieldKeyNames.MESSAGES, Messages);
                    }
                }
                if (History != null && History.Any())
                {
                    dictionary.Add(FieldKeyNames.HISTORY, History);
                }

                return dictionary;
            }
        }

        public abstract ChatRequestParam BuildParam();

        /// <summary>
        /// 历史会话列表
        /// </summary>
        /// <param name="outputtext">本轮会话结果</param>
        /// <param name="message">本轮会话信息</param>
        /// <returns></returns>
        public ChatRequestParam BuildHistory(ApiOutputResult output)
        {

            if (this.ResultFormat == ResultFormatType.TEXT)
            {

                if (this.History == null)
                {
                    this.History = new List<History>();
                }
                if (!string.IsNullOrEmpty(this.Prompt))
                {
                    this.History.Add(new History
                    {
                        User = this.Prompt,
                        Bot = output!.Output!.Text
                    });
                }
            }
            else if (this.ResultFormat == ResultFormatType.MESSAGE)//用户与模型的对话历史，对话接口未来都会有message传输，不过prompt和history会持续兼容
            {
                if (this.Messages == null)
                {
                    this.Messages = new List<Message>();
                    if (!string.IsNullOrEmpty(this.Prompt))
                    {
                        this.Messages.Add(new Message { Content = Prompt, Role = Role.User });
                    }
                }


                if (output != null)
                {
                    foreach (var item in output!.Output!.Choices)
                    {
                        Message message1 = new Message
                        {
                            Content = item.Message!.Content,
                            Role = item.Message!.Role
                        };
                        this.Messages.Add(message1);
                    }
                }
            }
            return this;
        }

        public override HttpContent GetHttpBody()
        {


            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add(FieldKeyNames.MODEL, base.Model);
            dictionary.Add(FieldKeyNames.INPUT, Input);
            if (base.Parameters != null && base.Parameters.Any())
            {
                dictionary.Add(FieldKeyNames.PARAMETERS, base.Parameters);
            }

            return new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
        }

        public override HttpRequestMessage GetHttpRequestMessage(string apikey, HttpMethod httpMethod, string url)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            var reqHeaders = RequestHeader.BuildRequestHeaders(apikey, base.IsSSE);
            if (httpMethod.Method == HttpMethod.Get.Method)
            {

                foreach (KeyValuePair<string, string> item in reqHeaders!)
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation(item.Key, item.Value);
                }

                if (base.Parameters != null)
                {
                    NameValueCollection nameValueCollection = new NameValueCollection();
                    foreach (KeyValuePair<string, object> item in base.Parameters!)
                    {
                        nameValueCollection[item.Key] = item.Value.ToString();
                    }

                    url = url + "?" + nameValueCollection;
                }

            }
            else
            {
                if (reqHeaders != null)
                {
                    foreach (KeyValuePair<string, string> item in reqHeaders!)
                    {
                        httpRequestMessage.Headers.TryAddWithoutValidation(item.Key, item.Value);
                    }
                }
                var httpbody = this.GetHttpBody();
                if (httpbody != null)
                {
                    httpRequestMessage.Content = httpbody;
                    httpRequestMessage.Content!.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
                }
            }
            httpRequestMessage.Method = httpMethod;
            httpRequestMessage.RequestUri = new Uri(url);

            return httpRequestMessage;

        }

        public override bool Verify()
        {
            if (string.IsNullOrEmpty(Prompt) && Message == null && (History == null || !History.Any()) && (Messages == null || !Messages.Any()))
            {
                return false;
            }
            return true;
        }
    }
}
