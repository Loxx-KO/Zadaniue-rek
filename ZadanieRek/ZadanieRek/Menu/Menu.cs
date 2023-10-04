using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;

namespace ZadanieRek.Menu
{
    public class Menu
    {
        private bool AppOpen = true;

        public void ShowMainMenu()
        {
            while (AppOpen)
            {
                Console.WriteLine("Opcje:\n1) Wybór miast:\n0) Zamknij aplikacje\n");
                string? number = Console.ReadLine();

                switch (number)
                {
                    case "1":
                        ShowCityMenu();
                        break;
                    case "0":
                        AppOpen = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Podano zły numer!\n");
                        break;
                }
            }
        }

        private void ShowCityMenu()
        {
            Console.Clear();
            bool menuOpen = true;

            while (menuOpen)
            {
                Console.WriteLine("Wybierz Miasto:");
                CitiesConfig.ShowCities();
                Console.WriteLine("0) Wróć\n");
                string? number = Console.ReadLine();

                if(number != null) 
                {
                    int choice = int.Parse(number);
                    if(choice == 0)
                    {
                        menuOpen = false;
                        Console.Clear();
                    }
                    else if(choice > 0 && choice <= CitiesConfig.GetNumberOfCities())
                    {
                        Console.Clear();
                        ShowCityDataChoiceMenu(choice);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Podano zły numer!\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Nie podano numeru!\n");
                }
            }
        }

        public void ShowCityDataChoiceMenu(int choice)
        {
            bool cityInfoExists = CitiesConfig.CheckIfCityDataExists(choice);

            if (cityInfoExists)
            {
                bool choiceMade = false;
                string? choiceNumber;
                while (!choiceMade)
                {
                    Console.WriteLine("Istnieja juz zapisanie dane o wybranym miescie, czy chcesz je nadpisać?\n1)Tak\n2)Nie\n");
                    choiceNumber = Console.ReadLine();
                    switch (choiceNumber)
                    {
                        case "1":
                            Console.Clear();
                            DataAccess.RemoveOldData(choice);
                            Console.Clear();
                            DataAccess.GetDataFromLast5Days(choice);
                            Console.WriteLine();
                            choiceMade = true;
                            break;
                        case "2":
                            Console.Clear();
                            DataAccess.ReadDataFromLast5Days(choice);
                            Console.WriteLine();
                            choiceMade = true;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Podano zly numer!");
                            break;
                    }
                }
            }
            else
            {
                Console.Clear();
                DataAccess.GetDataFromLast5Days(choice);
            }

            ShowCityOptionsMenu(choice);
        }

        private void ShowCityOptionsMenu(int choice)
        {
            Console.Clear();
            bool backChoosen = false;
            List<WeatherData> weatherData = DataAccess.GetChosenCityWeatherData(choice);

            while (!backChoosen)
            {
                Console.WriteLine("Opcje:\n1) Pokaz dane o temperaturze z 5 dni\n2) Pokaz najcieplejszy dzien\n3) Pokaz dzien z najmniejsza wilgotnoscia\n4) Pokaz opisy podogy z ostatnich 5 dni\n0) Wroc\n");
                string? number = Console.ReadLine();

                switch (number)
                {
                    case "1":
                        DataProcessing.GetAverageTemps(weatherData);
                        break;
                    case "2":
                        DataProcessing.GetTheWarmestDay(weatherData);
                        break;
                    case "3":
                        DataProcessing.GetTheLeastHumidDay(weatherData);
                        break;
                    case "4":
                        DataProcessing.GetShowWeatherDescriptions(weatherData);
                        break;
                    case "0":
                        Console.Clear();
                        backChoosen = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Podano zły numer!\n");
                        break;
                }
            }
        }
    }
}
