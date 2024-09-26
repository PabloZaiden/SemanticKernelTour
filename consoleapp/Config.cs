namespace SemanticKernelTour;

using System;
using Microsoft.Extensions.Configuration;

public static class Config
{
    static IConfigurationRoot _configuration;

    static Config() {
        System.Console.WriteLine(AppContext.BaseDirectory);
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory + "../../..")
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
    public static string Get(string key)
    {
        var val = _configuration[key];

        if (val == null)
        {
            throw new Exception($"Key {key} not found in configuration");
        }

        return val;
    }
}
