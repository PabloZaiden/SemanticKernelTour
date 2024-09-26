
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelTour;

var model = Config.Get("Model");
var openAIAPIKey = Config.Get("OpenAIAPIKey");

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(model, openAIAPIKey);
builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.None));

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings() {
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
};

AddPluginsToKernel(kernel);

var history = new ChatHistory();

var systemMessage = "You are a bot. Return small responses to the user. Always try to get data using tools before saying you can't do something.";
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
    kernel.Plugins.AddFromType<UserContextData>();
    kernel.Plugins.AddFromType<Weather>();
}