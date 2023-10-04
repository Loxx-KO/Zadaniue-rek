using ZadanieRek.Data_access;
using ZadanieRek.Menu;

try
{
    CitiesConfig.ReadConfFile();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Menu menu = new Menu();
menu.ShowMainMenu();
