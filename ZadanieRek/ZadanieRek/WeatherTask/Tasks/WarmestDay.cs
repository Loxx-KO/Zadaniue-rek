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
    public class WarmestDay : IWeatherTask
    {
        double MaxTemp = double.MinValue;
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
                if (MaxTemp < weather.Data[0].Temp)
                {
                    MaxTemp = weather.Data[0].Temp;
                    dateTime = DateTimeOffset.FromUnixTimeSeconds(weather.Data[0].Dt).DateTime;
                }
            }
        }

        public void ShowResult()
        {
            Console.WriteLine("Najcieplejszy dzien: " + dateTime.ToString() + " temp: " + Math.Round(MaxTemp - 273.15, 2) + "C");
        }
    }
}
