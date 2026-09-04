using CustomerManager.ConsoleApp.Customers.Services;

namespace CustomerManager.ConsoleApp.Customers.Dialogs;

public class CustomerDialog(ICustomerService customerService) : ICustomerDialog
{
    public void AddCustomerDialog()
    {
        Console.Clear();
        Console.WriteLine("### ADD CUSTOMER ###");

        InputDialog("Enter customer name", out string name);
        InputDialog("Enter customer email", out string email);

        var customer = customerService.AddCustomer(name, email);

        if (customer is not null)
            Console.WriteLine($"Customer with id '{customer.Id}' was created.");
        else
            Console.WriteLine($"Unable to create new customer.");

        Console.ReadKey();
    }

    public void ShowAllCustomersDialog()
    {
        Console.Clear();
        Console.WriteLine("### CUSTOMER LIST ###");

        var customers = customerService.GetAllCustomers();

        foreach (var customer in customers)
            Console.WriteLine($"{customer.Name} <{customer.Email}>");

        Console.ReadKey();
    }


    private static void InputDialog(string text, out string value)
    {
        do
        {
            Console.Write($"{text}: ");
            value = Console.ReadLine() ?? string.Empty;

            Console.Clear();
        }
        while (string.IsNullOrWhiteSpace(value));
    }
}