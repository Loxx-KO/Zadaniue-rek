using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZadanieRek.Other;

namespace ZadanieRek.Data_access
{
    public static class DataAccess
    {
        static HttpClient client = new HttpClient();
        static int numOfDays = 5;
        static int progressCounter = 0;
        static HashSet<WeatherData> weatherDataSet = new HashSet<WeatherData>();
        static ProgressBar ProgressBar = new ProgressBar(numOfDays);

        private static async Task<string> GetWeaterInfoAsync(string path)
        {
            string weatherInfo;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                weatherInfo = await response.Content.ReadAsStringAsync();
                return weatherInfo;
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
                return null;
            }

        }

        public static void GetDataFromLast5Days(int choice)
        {
            DateTime currentTime = DateTime.UtcNow;
            const int secInDay = 86400;
            long[] dt = { ((DateTimeOffset)currentTime).ToUnixTimeSeconds(), 
                ((DateTimeOffset)currentTime).ToUnixTimeSeconds() - secInDay, 
                ((DateTimeOffset)currentTime).ToUnixTimeSeconds() - secInDay*2,
                ((DateTimeOffset)currentTime).ToUnixTimeSeconds() - secInDay*3,
                ((DateTimeOffset)currentTime).ToUnixTimeSeconds() - secInDay*4};
            Task task;

            for (int i = 0; i < numOfDays; i++)
            {
                task = GetDataFromURLAsync(choice, dt[i]);
                task.Wait();
            }

            task = ResetProgress();
            task.Wait();

            Console.WriteLine(" GET completed.");
        }

        private static async Task UpdateProgress()
        {
            progressCounter++;
            await ProgressBar.UpdateProgress(progressCounter);
        }

        private static async Task ResetProgress()
        {
            progressCounter = 0;
            await ProgressBar.ResetProgress();
        }

        private static async Task GetDataFromURLAsync(int cityIndex, long dt)
        {
            string APIKey = "1f880e3d3fdc990d8ec44b5d3a14a00e";
            double lat = -1000, lon = -1000;
            string? cityName;

            City city = new City();
            city = city.FindCity(cityIndex);
            lat = city.Lat;
            lon = city.Lon;
            cityName = city.Name;

            if(lat == -1000 || lon == -1000) { Console.WriteLine("Blad podczas wyboru miasta! FUNC_RUNASYNC_IN_APIDATAACCESS.CS\n"); return; }

            string url = $"https://api.openweathermap.org/data/3.0/onecall/timemachine?lat={lat}&lon={lon}&exclude=minutely,hourly&dt={dt}&appid={APIKey}";

            string weatherApiData;
            WeatherData? weatherData;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            try
            {
                weatherApiData = await GetWeaterInfoAsync(url);
                weatherData = JsonSerializer.Deserialize<WeatherData>(weatherApiData, options);
                weatherData.CityName = cityName;
                weatherData.SaveFile(dt);
                weatherDataSet.Add(weatherData);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            await UpdateProgress();
        }

        public static void ReadDataFromLast5Days(int choice)
        {
            Task task = ReadData(choice);
            task.Wait();
            task = ResetProgress();
            task.Wait();

            Console.WriteLine(" READ completed.");
        }

        private static async Task ReadData(int choice)
        {
            string[] filePaths = CitiesConfig.GetFilePathsForChosenCity(choice);

            foreach (string path in filePaths)
            {
                WeatherData weatherData = new WeatherData();
                weatherData = weatherData.ReadFile(path);
                weatherData.CityName = Path.GetFileName(path).Split('_')[0];
                if (weatherData != null) weatherDataSet.Add(weatherData);
                await UpdateProgress();
            }

            Task.WaitAll();
        }

        public static void RemoveOldData(int choice)
        {
            string[] filePaths = CitiesConfig.GetFilePathsForChosenCity(choice);

            foreach (string path in filePaths)
            {
                File.Delete(path);
            }

            City city = new City();
            city.FindCity(choice);

            Task task = RemoveData(city);
            task.Wait();
            task = ResetProgress();
            task.Wait();

            Console.WriteLine("Old data removed.");
        }

        private static async Task RemoveData(City city)
        {
            foreach (WeatherData data in weatherDataSet)
            {
                if (data.CityName == city.Name)
                {
                    weatherDataSet.Remove(data);
                    await UpdateProgress();
                }
            }
        }

        public static List<WeatherData> GetChosenCityWeatherData(int cityIndex) 
        {
            List<WeatherData> weatherData = new List<WeatherData>();
            City city = new City();
            city = city.FindCity(cityIndex);

            foreach (WeatherData data in weatherDataSet) 
            {
                if(data.CityName == city.Name)
                {
                    weatherData.Add(data);
                }
            }

            return weatherData;
        }
    }
}
