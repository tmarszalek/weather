// Helpers/Settings.cs
using Xamarin.Essentials;

namespace MyWeather.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {

        #region Setting Constants

        private const string UseCityKey = "use_city";
        private static readonly bool UseCityDefault = true;


        private const string CityKey = "city";
        private static readonly string CityDefault = "Wroclaw";

        #endregion

        public static bool UseCity
        {
            get => Preferences.Get(UseCityKey, UseCityDefault);
            set => Preferences.Set(UseCityKey, value);
        }

        public static string City
        {
            get => Preferences.Get(CityKey, CityDefault);
            set => Preferences.Set(CityKey, value);
        }

    }
}