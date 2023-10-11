using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Interfaces;
using ZadanieRek.WeatherTask.Tasks;
using ZadanieRek.WeatherTaskClasses.Tasks;

namespace ZadanieRek.DataRepo.TasksRepository
{
    public class WeatherTaskRepository
    {
        private Dictionary<string, IWeatherTask> weatherTasks;
        public WeatherTaskRepository()
        {
            weatherTasks = new Dictionary<string, IWeatherTask>
            {
                { "getdata", new GetData5Days() },
                { "avgtemp", new AvgTemp() },
                { "rmdata", new RemoveData5Days() },
                { "readdata", new ReadData5Days() },
                { "warmestday", new WarmestDay() },
                { "leasthuminday", new LeastHumidDay() },
                { "weatherdesc", new WeatherDescription() }
            };
        }

        public bool CheckIfTaskExists(string taskName)
        {
            if(weatherTasks.ContainsKey(taskName)) {  return true; }
            return false;
        }

        public void TryToUseTask(string taskName, string cityName)
        {
            if (weatherTasks.ContainsKey(taskName))
            {
                if (weatherTasks[taskName].CheckTask(cityName))
                {
                    weatherTasks[taskName].RunTask(cityName);
                    weatherTasks[taskName].ShowResult();
                }
                else
                {
                    Console.WriteLine("Task failed to run.");
                }
            }
        }
    }
}
