using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;
using ZadanieRek.DataRepo;
using ZadanieRek.Interfaces;

namespace ZadanieRek.WeatherTask.Tasks
{
    public class LeastHumidDay : IWeatherTask
    {
        double MinHumid = double.MaxValue;
        DateTime dateTime = new DateTime();

        public bool CheckTask(string cityName)
        {
            if (Repositories.weatherData5Days.CheckIfCityExists(cityName))
            {
                return true;
            }
            return false;
        }

        public void RunTask(string cityName)
        {
            foreach (WeatherData5Days weather in Repositories.weatherData5Days.weatherDataSet[cityName])
            {
                if (MinHumid > weather.Data[0].Humidity)
                {
                    MinHumid = weather.Data[0].Humidity;
                    dateTime = DateTimeOffset.FromUnixTimeSeconds(weather.Data[0].Dt).DateTime;
                }
            }
        }

        public void ShowResult()
        {
            Console.WriteLine("Dzien z najmniejsza wilgotnoscia: " + dateTime.ToString() + " wilgotnosc: " + MinHumid);
        }
    }
}
