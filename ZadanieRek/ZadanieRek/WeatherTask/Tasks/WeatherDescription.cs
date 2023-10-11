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
    public class WeatherDescription : IWeatherTask
    {
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
                dateTime = DateTimeOffset.FromUnixTimeSeconds(weather.Data[0].Dt).DateTime;
                Console.WriteLine("Dzien: " + dateTime.ToString() + " Opis: " + weather.Data[0].Weather[0].Description);
                Console.WriteLine();
            }
        }

        public void ShowResult()
        {
            Console.WriteLine("Rezultaty dla wybranego miasta pomyslnie wyswietlone powyzej.");
        }
    }
}
