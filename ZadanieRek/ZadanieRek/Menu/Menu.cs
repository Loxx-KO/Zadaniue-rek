using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.DataRepo;
using ZadanieRek.DataRepo.CityRepo;
using ZadanieRek.DataRepo.Weather5DaysRepo;
using ZadanieRek.WeatherTaskClasses;

namespace ZadanieRek.Menu
{
    public class Menu
    {
        private bool AppOpen = true;

        public void ShowMainMenu()
        {
            Console.WriteLine("Dostepne miasta:");
            Repositories.citiesRepository.ShowCities();
            Console.WriteLine("Podaj polecenie lub wpisz 'q' by opuscic aplikacje");
            Console.WriteLine();

            while (AppOpen)
            {
                string? command = Console.ReadLine();
                string[] commandParts = command.Split(" ");

                if(commandParts[0] == "q")
                {
                    AppOpen = false;
                }
                else if (Repositories.weatherTasks.CheckIfTaskExists(commandParts[0])) 
                {
                    string cityName = Repositories.citiesRepository.FindCityFullName(commandParts[1]);
                    Repositories.weatherTasks.TryToUseTask(commandParts[0], cityName);
                }
                else
                {
                    Console.WriteLine("Podano nieznana komende!\n");
                    Console.WriteLine();
                }
            }
        }
    }
}
