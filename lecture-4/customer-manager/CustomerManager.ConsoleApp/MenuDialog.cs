using CustomerManager.ConsoleApp.Customers.Dialogs;

namespace CustomerManager.ConsoleApp;

public class MenuDialog(ICustomerDialog customerDialog)
{
    public void MainMenuDialog()
    {
        Console.Clear();
        Console.WriteLine("#####  MAIN MENU  #####");
        Console.WriteLine("1. Customer Management");
        Console.WriteLine("2. Supplier Management");
        Console.WriteLine("3. Product Management");
        Console.WriteLine("0. Exit Application");
        Console.WriteLine("");
        Console.Write("Menu Option: ");

        var option = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(option) || !int.TryParse(option, out int value))
            InvalidOptionSelectedDialog();

        Console.Clear();
    }

    private static void InvalidOptionSelectedDialog()
    {
        Console.WriteLine("");
        Console.WriteLine("Invalid option selected. Please try again.");
        Console.ReadKey();
    }

    private void MenuOptionSelector(int option)
    {
        switch (option)
        {
            case 0:
                break;

            case 1:
                customerDialog.CustomerMenuDialog();
                break;
            
            default:
                InvalidOptionSelectedDialog();
                break;
        }
    }
}
