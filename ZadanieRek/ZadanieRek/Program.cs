using ZadanieRek.DataRepo;
using ZadanieRek.DataRepo.CityRepo;
using ZadanieRek.Menu;

try
{
    CitiesConfig citiesConfig = new CitiesConfig();
    citiesConfig.ReadConfFile();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Menu menu = new Menu();
menu.ShowMainMenu();
