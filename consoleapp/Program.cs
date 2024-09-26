
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SemanticKernelTour;

var model = Config.Get("Model");
var openAIAPIKey = Config.Get("OpenAIAPIKey");

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(model, openAIAPIKey);
builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

var kernel = builder.Build();
