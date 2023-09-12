using System.Runtime.InteropServices;
using System.Text.Json;
using OpenWeatherAPIClient;

internal class Program
{
    public static async Task Main(string[] args)
    {
        string lat = @"-29.946";
        string lon = @"-51.144";
        string? apiKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_KEY");

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("A variável de ambiente OPEN_WEATHER_KEY não está definida.");
        }
        else
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

                try
                {
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(url);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string json = await httpResponse.Content.ReadAsStringAsync();

                        WeatherForecast? weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(json);

                        if (weatherForecast != null)
                        {
                            Console.WriteLine($"Name: " + weatherForecast.name);
                            Console.WriteLine($"Weather: " + weatherForecast.weather?[0].main);
                            Console.WriteLine($"Description: " + weatherForecast.weather?[0].description);
                            Console.WriteLine($"Temperature: " + weatherForecast.main?.temp + " C");
                            Console.WriteLine($"Temp. Max: " + weatherForecast.main?.temp_max + " C");
                            Console.WriteLine($"Temp. Min: " + weatherForecast.main?.temp_min + " C");
                            Console.WriteLine($"Humidity: " + weatherForecast.main?.humidity + "%");
                        }
                        else Console.WriteLine("Error: Information Missing");
                    }
                    else
                    {
                        Console.WriteLine("Error: Could not connect to the API");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}