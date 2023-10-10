using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.CityRepo
{
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
    }
}
