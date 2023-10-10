using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Interfaces;
using ZadanieRek.WeatherTaskClasses.Tasks;

namespace ZadanieRek.WeatherTaskClasses
{
    public class WeatherTasks
    {
        private Dictionary<string, IWeatherTask> weatherTasks;
        public WeatherTasks() 
        {
            weatherTasks = new Dictionary<string, IWeatherTask>
            {
                { "avgtemp", new AvgTemp() }
            };
        }

        public void UseTask(string taskName, string cityName)
        {
            if (weatherTasks.ContainsKey(taskName)) 
            {
                if(weatherTasks[taskName].CheckTask())
                {
                    weatherTasks[taskName].RunTask();
                }
            }
        }
    }
}
