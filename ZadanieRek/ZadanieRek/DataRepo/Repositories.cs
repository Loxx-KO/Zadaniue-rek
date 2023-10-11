using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.DataRepo.CityRepo;
using ZadanieRek.DataRepo.TasksRepository;
using ZadanieRek.DataRepo.Weather5DaysRepo;

namespace ZadanieRek.DataRepo
{
    public static class Repositories
    {
        public static CitiesRepository citiesRepository = new CitiesRepository();
        public static WeatherTaskRepository weatherTasks = new WeatherTaskRepository();
        public static WeatherDataRepository5Days weatherData5Days = new WeatherDataRepository5Days();
    }
}
