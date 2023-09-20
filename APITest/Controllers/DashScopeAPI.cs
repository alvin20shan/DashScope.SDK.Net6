using DashScope.SDK.Net6;
using DashScope.SDK.Net6.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableCors("DashScopeAPI")]
    public class DashScopeAPI : ControllerBase
    {
        private readonly QwenClient _qwenClient;
        public DashScopeAPI(QwenClient qwenClient)
        {
            _qwenClient = qwenClient;
        }



        [HttpPost("QianwenChatAI")]
        public async Task<IActionResult> QianwenChatAI([FromBody] dynamic content)
        {
            string json = Convert.ToString(content);
            try
            {
                
                var param = new DashScope.SDK.Net6.QwenModels.QwenParam()
                {
                    Model = ModelNames.QWEN_V1,
                    Prompt = json,
                    TopP = 0.8
                };

                var result = await _qwenClient.QwenCallAsync(param);

                return Content(result!.Output!.Text!);
            }
            catch (Exception ex)
            {

            }

            return Content("");
        }

    }
}
