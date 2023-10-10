using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.CityRepo
{
    public class CitiesRepository
    {
        private List<City> cityList = new();
        public CitiesConfig cityConfig = new CitiesConfig();

        public CitiesRepository()
        {
            cityList = cityConfig.ReadConfFile();
        }

        public City FindCity(string cityName)
        {
            foreach (City city in cityList)
            {
                if (city.Name == cityName) return city;
            }
            return new City();
        }

        public bool CheckIfCityDataExists(string cityName)
        {
            string[] fileNames = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles"));

            foreach (string file in fileNames)
            {
                if (Path.GetFileName(file).Split('_')[0] == cityName)
                {
                    return true;
                }
            }
            return false;
        }

        public string[] GetFilePathsForChosenCity(string cityName)
        {
            string[] fileNames = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles"));
            List<string> filePaths = new List<string>();

            foreach (string file in fileNames)
            {
                if (Path.GetFileName(file).Split('_')[0] == cityName)
                {
                    string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles\", file);
                    filePaths.Add(filePath);
                }
            }

            return filePaths.ToArray();
        }

        public void ShowCities()
        {
            foreach (City city in cityList)
            {
                city.ShowCityName();
            }
        }
    }
}
