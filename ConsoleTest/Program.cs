// See https://aka.ms/new-console-template for more information
using DashScope.SDK.Net6;
using DashScope.SDK.Net6.Utils;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var allqwenParam = new DashScope.SDK.Net6.QwenModels.QwenParam()
{
    Model = ModelNames.QWEN_PLUS,
    TopP = 0.8
};
var allqwenClient = new QwenClient("apikey");


string CallMulti(string msg)
{

    allqwenParam.Prompt = msg; //Message格式 多轮对话 第一种方式  兼容Prompt
                               //allqwenParam.Message = new DashScope.SDK.Net6.Models.Message() { Role =Role.User, Content = msg }; //Message格式 多轮对话 第二种方式
    var result = allqwenClient.QwenCallAsync(allqwenParam, new QwenEndpoint(), true).Result;

    return JsonConvert.SerializeObject(result);
}


//单轮
var single = CallQwen("广州最长的桥是哪个？");
Console.WriteLine(single);
Console.ReadLine();
//多轮
var mulns = CallMulti("广州最高的建筑是哪个？");
Console.WriteLine(mulns);
mulns = CallMulti("我上一个问题是什么？");
Console.WriteLine(mulns);

Console.ReadLine();


string CallQwen(string msg)
{
    QwenClient qwenClient = new QwenClient("apikey");
    var param = new DashScope.SDK.Net6.QwenModels.QwenParam()
    {
        Model = ModelNames.QWEN_PLUS,
        Prompt = msg,
        TopP = 0.8
    };

    var result = qwenClient.QwenCallAsync(param, new QwenEndpoint()).Result;
    return JsonConvert.SerializeObject(result);
}