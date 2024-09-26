using Microsoft.SemanticKernel;

namespace SemanticKernelTour
{
    public class Weather
    {
        [KernelFunction]
        public string GetWeather(float latitude, float longitude)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m";
                var weatherResponse = client.GetStringAsync(url).Result;

                return weatherResponse;
            }
        }
    }
}