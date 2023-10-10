using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.CityRepo
{
    public class CitiesConfig
    {
        private string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles\", "appConfig.txt");

        public List<City> ReadConfFile()
        {
            List<City> cityList = new List<City>();
            IEnumerable<string> lines = File.ReadLines(filePath);
            lines = lines.Skip(1).ToArray();
            int index = 1;

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cityList.Add(new City(index, parts[0], Convert.ToDouble(parts[1], CultureInfo.InvariantCulture), Convert.ToDouble(parts[2], CultureInfo.InvariantCulture)));
                index++;
            }

            return cityList;
        }
    }
}
