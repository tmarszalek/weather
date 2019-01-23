using MyWeather.Helpers;
using MyWeather.Models;
using MyWeather.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using MvvmHelpers;

using Xamarin.Essentials;

namespace MyWeather.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        WeatherService WeatherService { get; } = new WeatherService();

        string location = Settings.City;
        public string Location
        {
            get { return location; }
            set
            {
                SetProperty(ref location, value);
                Settings.City = value;
            }
        }

        string temp = string.Empty;
        public string Temp
        {
            get { return temp; }
            set { SetProperty(ref temp, value); }
        }

        string condition = string.Empty;
        public string Condition
        {
            get { return condition; }
            set { SetProperty(ref condition, value); ; }
        }

        
        WeatherForecastRoot forecast;
        public WeatherForecastRoot Forecast
        {
            get { return forecast; }
            set { forecast = value; OnPropertyChanged(); }
        }


        ICommand getWeather;
        public ICommand GetWeatherCommand =>
                getWeather ??
                (getWeather = new Command(async () => await ExecuteGetWeatherCommand()));

        private async Task ExecuteGetWeatherCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                WeatherRoot weatherRoot = null;
                var units = Units.Metric;
               
                //Get weather by city
                weatherRoot = await WeatherService.GetWeather(Location.Trim(), units);
               
                //Get forecast based on cityId
                Forecast = await WeatherService.GetForecast(weatherRoot.CityId, units);

                var unit = "C";
                Temp = $"Temp: {weatherRoot?.MainWeather?.Temperature ?? 0}°{unit}";
                Condition = $"{weatherRoot.Name}: {weatherRoot?.Weather?[0]?.Description ?? string.Empty}";

                await TextToSpeech.SpeakAsync(Temp + " " + Condition);
            }
            catch (Exception ex)
            {
                Temp = "Nie można pobrać danych";
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
