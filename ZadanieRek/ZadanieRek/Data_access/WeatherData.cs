using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZadanieRek.Interfaces;

namespace ZadanieRek.Data_access
{
    public class WeatherData5Days : IWeatherData
    {
        public string? CityName { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string? TimeZone { get; set; }
        public int TimeZone_Offset { get; set; }
        public Data[]? Data { get; set; }

        private string? filePath;

        public IWeatherData ReadFile(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string json = File.ReadAllText(filePath);

            WeatherData5Days? weatherData = JsonSerializer.Deserialize<WeatherData5Days>(json, options);

            return weatherData;
        }
        public void SaveFile(long dt)
        {
            filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataFiles\", CityName + "_dt_" + dt + ".json");
            string weatherData = JsonSerializer.Serialize(this, typeof(WeatherData5Days));
            File.WriteAllText(filePath, weatherData);
        }
    }
    public class Data
    {
        public int Dt { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public float Temp { get; set; }
        public float Feels_Like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public float Dew_Point { get; set; }
        public float UVI { get; set; }
        public int Clouds { get; set; }
        public int Visibility { get; set; }
        public float Wind_Speed { get; set; }
        public int Wind_Deg { get; set; }
        public Weather[]? Weather { get; set; }
    }
    public class Weather
    {
        public int Id { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }
}
