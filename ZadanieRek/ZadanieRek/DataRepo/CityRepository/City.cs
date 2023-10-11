using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.DataRepo.CityRepo
{
    public class City
    {
        public string? ShortName { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public City(string shortName, string name, double lat, double lon)
        {
            ShortName = shortName;
            Name = name;
            Lat = lat;
            Lon = lon;
        }

        public void ShowCityName()
        {
            Console.WriteLine($"{ShortName}) City: {Name}");
        }
    }
}
