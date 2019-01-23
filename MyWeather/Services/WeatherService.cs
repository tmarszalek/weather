using MyWeather.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace MyWeather.Services
{
    public enum Units
    {
        Metric
    }

    public class WeatherService
    {
        const string WeatherCoordinatesUri = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&appid=fc9f6c524fc093759cd28d41fda89a1b&lang=pl";
        const string WeatherCityUri = "http://api.openweathermap.org/data/2.5/weather?q={0}&units={1}&appid=fc9f6c524fc093759cd28d41fda89a1b&lang=pl";
        const string ForecaseUri = "http://api.openweathermap.org/data/2.5/forecast?id={0}&units={1}&appid=fc9f6c524fc093759cd28d41fda89a1b&lang=pl";

       

        public async Task<WeatherRoot> GetWeather(string city, Units units = Units.Metric)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WeatherCityUri, city, units.ToString().ToLower());
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherRoot>(json);
            }

        }

        public async Task<WeatherForecastRoot> GetForecast(int id, Units units = Units.Metric)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(ForecaseUri, id, units.ToString().ToLower());
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }

        }
    }
}
