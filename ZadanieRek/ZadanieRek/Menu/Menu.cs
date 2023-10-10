using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.CityRepo;
using ZadanieRek.WeatherTaskClasses;

namespace ZadanieRek.Menu
{
    public class Menu
    {
        private bool AppOpen = true;

        public void ShowMainMenu()
        {
            CitiesRepository citiesRepository = new CitiesRepository();
            WeatherTasks weatherTasks = new WeatherTasks();

            Console.WriteLine("Dostepne miasta:");
            citiesRepository.ShowCities();
            Console.WriteLine("Podaj polecenie lub wpisz 'q' by opuscic aplikacje");

            while (AppOpen)
            {
                string? command = Console.ReadLine();
                string[] commandParts = command.Split(" ");

                switch (commandParts[0])
                {
                    case "avgtemp":
                        weatherTasks.UseTask(commandParts[0], commandParts[1]);
                        break;
                    case "q":
                        AppOpen = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Podano nieznana komende!\n");
                        break;
                }
            }
        }
    }
}
