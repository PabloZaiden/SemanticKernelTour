
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SemanticKernelTour;

var model = Config.Get("Model");
var openAIAPIKey = Config.Get("OpenAIAPIKey");
var openAIEndpoint = Config.GetOptional("OpenAIEndpoint");

var builder = Kernel.CreateBuilder();

if (openAIEndpoint != null)
{
    var endpoint = new Uri(openAIEndpoint);
    builder.AddOpenAIChatCompletion(model, endpoint, openAIAPIKey);
}
else
{
    builder.AddOpenAIChatCompletion(model, openAIAPIKey);
}

builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.None));

var kernel = builder.Build();
