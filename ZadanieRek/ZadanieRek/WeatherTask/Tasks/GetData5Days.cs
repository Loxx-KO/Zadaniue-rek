using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;
using ZadanieRek.DataRepo;
using ZadanieRek.Interfaces;

namespace ZadanieRek.WeatherTaskClasses.Tasks
{
    public class GetData5Days : IWeatherTask
    {
        public bool CheckTask(string cityName)
        {
            return true;
        }

        public void RunTask(string cityName)
        {
            DataAccess5Days dataAccess5Days = new DataAccess5Days();
            dataAccess5Days.GetDataFromLast5Days(cityName);
        }

        public void ShowResult()
        {
            Console.WriteLine("Pomyslnie pobrano dane z ostatnich 5 dni");
        }
    }
}
