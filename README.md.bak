# DashScope.SDK.Net6

#阿里通义千问-灵积模型.Net6 SDK

#调用方式

	#单轮对话
	public string CallQwen(string msg)
	{
    	QwenClient qwenClient = new QwenClient("apikey");
    	var param = new DashScope.SDK.Net6.QwenModels.QwenParam()
    	{ 
             Model = ModelNames.QWEN_V1,
             Prompt = msg, 
             TopP = 0.8
    	};

    	var result = qwenClient.QwenCallAsync(param,new QwenEndpoint()).Result;
   	     return JsonConvert.SerializeObject(result);
	}

	#多伦对话
	string CallMulti(string msg)
	{

    	    allqwenParam.Prompt = msg;
	        //allqwenParam.Message = new DashScope.SDK.Net6.Models.Message() { Role = Role.User, Content = msg }; //Message格式 多轮对话 第二种方式 
    	    var result = allqwenClient.QwenCallAsync(allqwenParam, new QwenEndpoint(),true).Result;

    	    return JsonConvert.SerializeObject(result);
	}

   	
	var allqwenParam = new DashScope.SDK.Net6.QwenModels.QwenParam()
	{
	    Model = ModelNames.QWEN_V1,
	    TopP = 0.8
	};
	var allqwenClient = new QwenClient("apikey");
	
	 
	var mulns = CallMulti("今天广州天气如何？");
	Console.WriteLine(mulns);
	 mulns = CallMulti("我上一个问题是什么？");
	Console.WriteLine(mulns);
	
	Console.ReadLine();


 ![image](https://github.com/alvin20shan/DashScope.SDK.Net6/assets/65529357/dff2742b-201b-4374-9fb8-6049b7e0192b)


