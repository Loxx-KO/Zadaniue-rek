using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.Data_access
{
    public static class DataProcessing
    {
        public static void GetAverageTemps(List<WeatherData> weatherDataList)
        {
            double tempsum = 0;
            int daysNum = weatherDataList.Count;

            foreach(WeatherData weather in weatherDataList)
            {
                tempsum += weather.Data[0].Temp - 273.15;
            }

            Console.WriteLine("Srednia temperatura z 5 dni: " + Math.Round(tempsum/daysNum, 2) + "C");
        }

        public static void GetTheWarmestDay(List<WeatherData> weatherDataList)
        {
            double MaxTemp = 0;
            DateTime dateTime = new DateTime();

            foreach (WeatherData weather in weatherDataList)
            {
                if(MaxTemp < weather.Data[0].Temp)
                {
                    MaxTemp = weather.Data[0].Temp;
                    dateTime = DateTimeOffset.FromUnixTimeSeconds(weather.Data[0].Dt).DateTime;
                }
            }

            Console.WriteLine("Najcieplejszy dzien: " + dateTime.ToString() + " temp: " + Math.Round(MaxTemp - 273.15, 2) + "C");
        }

        public static void GetTheLeastHumidDay(List<WeatherData> weatherDataList)
        {
            double MinHumid = double.MaxValue;
            DateTime dateTime = new DateTime();

            foreach (WeatherData weather in weatherDataList)
            {
                if (MinHumid > weather.Data[0].Humidity)
                {
                    MinHumid = weather.Data[0].Humidity;
                    dateTime = DateTimeOffset.FromUnixTimeSeconds(weather.Data[0].Dt).DateTime;
                }
            }

            Console.WriteLine("Dzien z najmniejsza wilgotnoscia: " + dateTime.ToString() + " wilgotnosc: " + MinHumid);
        }

        public static void GetShowWeatherDescriptions(List<WeatherData> weatherDataList)
        {
            DateTime dateTime = new DateTime();

            foreach (WeatherData weather in weatherDataList)
            {
                dateTime = DateTimeOffset.FromUnixTimeSeconds(weather.Data[0].Dt).DateTime;
                Console.WriteLine("Dzien: " + dateTime.ToString() + " Opis: " + weather.Data[0].Weather[0].Description);
                Console.WriteLine();
            }
        }
    }
}
