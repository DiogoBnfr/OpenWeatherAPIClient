using System.ComponentModel;

namespace OpenWeatherAPIClient;

public class WeatherForecast
{
    public Weather[]? weather { get; set; }
    public Main? main { get; set; }
}

public class Weather
{
    public string? main { get; set; }
    public string? description { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public double umidity { get; set; }

}