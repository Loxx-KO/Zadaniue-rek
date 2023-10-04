using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.Data_access
{
    public static class CitiesConfig
    {
        private static string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles\", "appConfig.txt");
        private static List<City> cityList = new();

        public static void ReadConfFile()
        {
            cityList = new List<City>();
            IEnumerable<string> lines = File.ReadLines(filePath);
            lines = lines.Skip(1).ToArray();
            int index = 1;

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cityList.Add(new City(index, parts[0], Convert.ToDouble(parts[1], CultureInfo.InvariantCulture), Convert.ToDouble(parts[2], CultureInfo.InvariantCulture)));
                index++;
            }
        }

        public static int GetNumberOfCities()
        {
            return cityList.Count;
        }

        public static List<City> GetCityList()
        {
            return cityList;
        }

        public static void ShowCities()
        {
            foreach(City city in cityList) 
            {
                city.ShowCityName();
            }
        }

        public static bool CheckIfCityDataExists(int cityIndex)
        {
            City city = new City();
            city = city.FindCity(cityIndex);
            string[] fileNames = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles"));

            foreach (string file in fileNames)
            {
                if (Path.GetFileName(file).Split('_')[0] == city.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public static string[] GetFilePathsForChosenCity(int choice)
        {
            City city = new City();
            city = city.FindCity(choice);
            string[] fileNames = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles"));

            List<string> filePaths = new List<string>();

            foreach (string file in fileNames)
            {
                if (Path.GetFileName(file).Split('_')[0] == city.Name)
                {
                    string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles\", file);
                    filePaths.Add(filePath);
                }
            }

            return filePaths.ToArray();
        }
    }

    public class City
    {
        public int Index { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public City()
        {

        }
        public City(int index, string name, double lat, double lon)
        { 
            Index = index;
            Name = name;
            Lat = lat;
            Lon = lon;
        }

        public void ShowCityName()
        {
            Console.WriteLine($"{Index}) City: {Name}");
        }

        public City FindCity(int cityIndex)
        {
            foreach (City city in CitiesConfig.GetCityList())
            {
                if (city.Index == cityIndex)
                {
                    return city;
                }
            }
            return new City();
        }
    }
}
