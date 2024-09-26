using System.Text.Json;
using Microsoft.SemanticKernel;

namespace SemanticKernelTour;

class UserContextData
{
    JsonDocument GetGeoData()
    {
        using (var client = new HttpClient())
        {
            var ip = client.GetStringAsync("https://ifconfig.io/ip").Result;
            var geoDataUrl = $"http://ip-api.com/json/{ip}";
            var geoDataResponse = client.GetStringAsync(geoDataUrl).Result;

            return JsonDocument.Parse(geoDataResponse);
        }
    }

    [KernelFunction]
    public string GetGeographicData()
    {
        return GetGeoData().RootElement.ToString();
    }
    
    [KernelFunction]
    public DateTimeOffset GetCurrentDateAndTime() {
        return DateTimeOffset.Now;
    }
}