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
using ZadanieRek.DataRepo;
using ZadanieRek.DataRepo.CityRepo;
using ZadanieRek.DataRepo.Weather5DaysRepo;
using ZadanieRek.Interfaces;
using ZadanieRek.Other;

namespace ZadanieRek.Data_access
{
    public class DataAccess5Days
    {
        HttpClient client;
        int numOfDays;
        int progressCounter;

        public DataAccess5Days()
        {
            client = new HttpClient();
            numOfDays = 5;
            progressCounter = 0;
        }

        private async Task<string> GetWeaterInfoAsync(string path)
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

        public void GetDataFromLast5Days(string cityName)
        {
            RemoveOldDataAsync(cityName).Wait();
            Repositories.weatherData5Days.AddCity(cityName);

            ProgressBar ProgressBar = new ProgressBar(numOfDays);

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
                task = GetDataFromURLAsync(cityName, dt[i], ProgressBar);
                task.Wait();
            }

            task = ResetProgress(ProgressBar);
            task.Wait();
        }

        private async Task UpdateProgress(ProgressBar ProgressBar)
        {
            progressCounter++;
            await ProgressBar.UpdateProgress(progressCounter);
        }

        private async Task ResetProgress(ProgressBar ProgressBar)
        {
            progressCounter = 0;
            await ProgressBar.ResetProgress();
        }

        private async Task GetDataFromURLAsync(string cityName, long dt, ProgressBar ProgressBar)
        {
            string APIKey = "1f880e3d3fdc990d8ec44b5d3a14a00e";
            double lat = -1000, lon = -1000;

            City city = Repositories.citiesRepository.FindCityByName(cityName);
            lat = city.Lat;
            lon = city.Lon;

            if(lat == -1000 || lon == -1000) { Console.WriteLine("Blad podczas wyboru miasta! FUNC_RUNASYNC_IN_APIDATAACCESS.CS\n"); return; }

            string url = $"https://api.openweathermap.org/data/3.0/onecall/timemachine?lat={lat}&lon={lon}&exclude=minutely,hourly&dt={dt}&appid={APIKey}";

            string weatherApiData;
            WeatherData5Days? weatherData;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            try
            {
                weatherApiData = await GetWeaterInfoAsync(url);
                weatherData = JsonSerializer.Deserialize<WeatherData5Days>(weatherApiData, options);
                weatherData.CityName = cityName;
                weatherData.SaveFile(dt);
                Repositories.weatherData5Days.AddDataForCity(cityName, weatherData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            await UpdateProgress(ProgressBar);
        }

        public void ReadDataFromLast5Days(string cityName)
        {
            RemoveDataFromRepository(cityName);
            Repositories.weatherData5Days.AddCity(cityName);

            ProgressBar ProgressBar = new ProgressBar(numOfDays);
            Task task = ReadData(cityName, ProgressBar);
            task.Wait();
            task = ResetProgress(ProgressBar);
            task.Wait();
        }

        private async Task ReadData(string cityName, ProgressBar progressBar)
        {
            string[] filePaths = Repositories.citiesRepository.GetFilePathsForChosenCity(cityName);

            foreach (string path in filePaths)
            {
                WeatherData5Days weatherData = new WeatherData5Days();
                weatherData = (WeatherData5Days)weatherData.ReadFile(path);
                weatherData.CityName = Path.GetFileName(path).Split('_')[0];
                if (weatherData != null) Repositories.weatherData5Days.AddDataForCity(cityName, weatherData);
                await UpdateProgress(progressBar);
            }

            Task.WaitAll();
        }

        private void RemoveDataFromRepository(string cityName)
        {
            City city = Repositories.citiesRepository.FindCityByName(cityName);
            Repositories.weatherData5Days.RemoveData(city);
        }

        public async Task RemoveOldDataAsync(string cityName)
        {
            string[] filePaths = Repositories.citiesRepository.GetFilePathsForChosenCity(cityName);

            if (filePaths.Length > 0)
            {
                ProgressBar ProgressBar = new ProgressBar(filePaths.Length);

                foreach (string path in filePaths)
                {
                    File.Delete(path);
                    await UpdateProgress(ProgressBar);
                }

                RemoveDataFromRepository(cityName);

                Task task = ResetProgress(ProgressBar);
                task.Wait();
            }
        }
    }
}
