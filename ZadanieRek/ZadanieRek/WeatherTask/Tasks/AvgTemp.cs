using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;
using ZadanieRek.DataRepo;
using ZadanieRek.Interfaces;

namespace ZadanieRek.WeatherTaskClasses.Tasks
{
    public class AvgTemp : IWeatherTask
    {
        double tempsum = 0;
        int numOfDays = 0;

        public bool CheckTask(string cityName)
        {
            if (Repositories.weatherData5Days.CheckIfCityExists(cityName))
            {
                numOfDays = Repositories.weatherData5Days.GetChosenCityWeatherData(cityName).Count;
                return true;
            }
            return false;
        }

        public void RunTask(string cityName)
        {
            foreach (WeatherData5Days weather in Repositories.weatherData5Days.weatherDataSet[cityName])
            {
                tempsum += weather.Data[0].Temp - 273.15;
            }
        }

        public void ShowResult()
        {
            Console.WriteLine("Srednia temperatura z 5 dni: " + Math.Round(tempsum / numOfDays, 2) + "C");
        }
    }
}
