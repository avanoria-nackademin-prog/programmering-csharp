namespace IncidentManagement.ConsoleApp.Dialogs.Menu;

internal class MenuDialog : IMenuDialog
{
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("#### MAIN MENU ####");
            Console.ReadKey();
            break;
        }
    }
}
