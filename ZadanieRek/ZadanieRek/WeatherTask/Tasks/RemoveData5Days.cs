using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;
using ZadanieRek.DataRepo;
using ZadanieRek.Interfaces;

namespace ZadanieRek.WeatherTask.Tasks
{
    public class RemoveData5Days : IWeatherTask
    {
        public bool CheckTask(string cityName)
        {
            if (Repositories.citiesRepository.CheckIfCityDataExists(cityName))
            {
                return true;
            }
            return false;
        }

        public void RunTask(string cityName)
        {
            DataAccess5Days dataAccess5Days = new DataAccess5Days();
            Task t = dataAccess5Days.RemoveOldDataAsync(cityName);
            t.Wait();
        }

        public void ShowResult()
        {
            Console.WriteLine("Pomyslnie usunieto dane z ostatnich 5 dni");
        }
    }
}
