using IncidentManagement.ConsoleApp.Dialogs.Menu;

namespace IncidentManagement.ConsoleApp.Dialogs;

internal class ApplicationDialog(IMenuDialog menuDialog) : IAppDialog
{
    public void StartApplication()
    {
        menuDialog.ShowMainMenu();
    }

    public void CloseApplication()
    {
        Console.Clear();
        Console.WriteLine("Press any key to close application.");
        Console.ReadKey();
    }


}
