namespace SupportTicketSystem.Presentation.Services;

public class CustomerDialogService(CustomerService customerService)
{
    private string RequiredDialogInput(string text)
    {
        string? value = string.Empty;

        do
        {
            Console.Clear();
            Console.Write($"{text}: ");
            value = Console.ReadLine();
        }
        while (string.IsNullOrWhiteSpace(value));

        return value;
    }

    private string? DialogInput(string text)
    {
        string? value = string.Empty;
        
        Console.Clear();
        Console.Write($"{text}: ");
        value = Console.ReadLine();

        return value;
    }


    public void CreateCustomerDialog()
    {
        var customerName = RequiredDialogInput("Ange kundnamn");
        var emailAddress = RequiredDialogInput("Ange e-postadress");
        var phoneNumber = DialogInput("Ange telefonnummer (valfritt)");

        var result = customerService.CreateCustomer(customerName, emailAddress, phoneNumber);

        if (!result.Succeeded)
            Console.WriteLine($"{result.ErrorMessage}");
        else
        {
            if (result.Customer is not null)
            {
                Console.WriteLine($"Customer Id: {result.Customer.Id}");
                Console.WriteLine($"Customer Name: {result.Customer.Name}");
                Console.WriteLine($"Customer Email: {result.Customer.EmailAddress}");
                Console.WriteLine($"Customer Phone: {result.Customer.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("No customer information found.");
            }

        }

        Console.ReadKey();
    }
}
