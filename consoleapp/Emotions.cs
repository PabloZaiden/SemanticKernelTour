using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices;
using Microsoft.SemanticKernel;

namespace SemanticKernelTour;

public class Emotions
{

    [KernelFunction]
    public Emotion ShowCurrentEmotion()
    {
        var emotions = Directory.GetFiles("emotions", "*.jpg");
        
        var file = emotions[new Random().Next(emotions.Length)];

        return new Emotion(file);
    }
}

public class Emotion
{
    public string File { get; }

    public Emotion(string file)
    {
        File = file;
    }
}

public class EmotionFilter : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
    {
        await next(context);
        
        if (context.Result.ValueType == typeof(Emotion))
        {
            var emotion = context.Result.GetValue<Emotion>()!;

            Utils.ShowImage(emotion.File);
        }
    }
}
