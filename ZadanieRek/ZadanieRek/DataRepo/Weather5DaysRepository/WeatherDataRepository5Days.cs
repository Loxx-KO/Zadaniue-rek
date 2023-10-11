using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;
using ZadanieRek.DataRepo.CityRepo;
using ZadanieRek.Interfaces;

namespace ZadanieRek.DataRepo.Weather5DaysRepo
{
    public class WeatherDataRepository5Days
    {
        public Dictionary<string, List<WeatherData5Days>> weatherDataSet { get; private set; }

        public WeatherDataRepository5Days() 
        {
            weatherDataSet = new Dictionary<string, List<WeatherData5Days>>();
        }   

        public void AddCity(string cityName)
        {
            weatherDataSet.Add(cityName, new List<WeatherData5Days>());
        }

        public void AddDataForCity(string cityName, WeatherData5Days weatherData)
        {
            weatherDataSet[cityName].Add(weatherData);
        }

        public bool CheckIfCityExists(string cityName)
        {
            if(weatherDataSet.ContainsKey(cityName)) return true;
            return false;
        }

        public void RemoveData(City city)
        {
            foreach (string cityName in weatherDataSet.Keys)
            {
                if (cityName == city.Name)
                {
                    weatherDataSet.Remove(cityName);
                    return;
                }
            }
        }

        public List<WeatherData5Days> GetChosenCityWeatherData(string cityName)
        {
            City city = Repositories.citiesRepository.FindCityByName(cityName);

            if (cityName == city.Name)
            {
                return weatherDataSet[cityName];
            }

            return new List<WeatherData5Days>();
        }
    }
}
