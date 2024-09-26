using System.Text.Encodings.Web;
using Microsoft.SemanticKernel;

namespace SemanticKernelTour.Plugins;

public class WolframAlpha
{
    const string BaseUrlFormat = "http://api.wolframalpha.com/v2/query?appid={0}&input={1}";
    KernelFunction _extractAnswer;
    Kernel _kernel;
    string _appid;

    public WolframAlpha(Kernel kernel)
    {
        _kernel = kernel;
        _appid = Config.Get("WolframAlphaAppId");

        _extractAnswer = _kernel.CreateFunctionFromPrompt(@"{{$input}}
        
        --------------
        
        Based on the xml response from WolframAlpha above, return just a plain string with the answer.", functionName: "AskWolframAlphaInternal");
    }

    [KernelFunction]
    public string AskWolframAlpha(string questionInEnglish)
    {
        var url = string.Format(BaseUrlFormat, _appid, UrlEncoder.Create().Encode(questionInEnglish));

        string xmlResponse;
        using (var client = new HttpClient())
        {
            xmlResponse = client.GetStringAsync(url).Result;
        }

        var result = _kernel.InvokeAsync(_extractAnswer, new() { ["input"] = xmlResponse }).Result;
        return result.ToString();
    }
}