﻿
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using SemanticKernelTour;
using SemanticKernelTour.Plugins;

var model = Config.Get("Model");
var openAIAPIKey = Config.Get("OpenAIAPIKey");

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(model, openAIAPIKey);
builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings() {
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
    Temperature = 0
};

AddPluginsToKernel(kernel);
AddFiltersToKernel(kernel);

var history = new ChatHistory();

var systemMessage = "You are a bot. Return small responses to the user. Always try to get data using tools before saying you can't do something. Never tell the user to browse to an URL. You should read the content of the web page and answer the user's question. For math questions, you should use Wolfram Alpha.";
string? userInput = null;

history.AddSystemMessage(systemMessage);
do {
    Console.Write("You: ");
    userInput = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(userInput)) {
        history.AddUserMessage(userInput);
        var response = await chatService.GetChatMessageContentAsync(
            history, 
            openAIPromptExecutionSettings,
            kernel);

        Console.WriteLine($"Bot: {response}");
    }
} while (!string.IsNullOrWhiteSpace(userInput));


void AddPluginsToKernel(Kernel kernel)
{
    // kernel.Plugins.AddFromType<UserContextData>();
    // kernel.Plugins.AddFromType<Weather>();
    // kernel.Plugins.AddFromType<WebBrowser>();

    // var bingAPIKey = Config.Get("BingAPIKey");
    // var bingConnector = new BingConnector(bingAPIKey);
    // var webSearchPlugin = new WebSearchEnginePlugin(bingConnector);
    // kernel.Plugins.AddFromObject(webSearchPlugin, "WebSearch");

    // var wolframAlphaPlugin = new WolframAlpha(kernel);
    // kernel.Plugins.AddFromObject(wolframAlphaPlugin, "WolframAlpha");
    // kernel.Plugins.AddFromType<StringUtilities>();
    kernel.Plugins.AddFromType<Emotions>();
}

void AddFiltersToKernel(Kernel kernel)
{
    kernel.FunctionInvocationFilters.Add(new EmotionFilter());
}

