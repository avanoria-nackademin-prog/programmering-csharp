using IncidentSystem.Application.Features.Customers;
using IncidentSystem.Application.Features.Customers.Dtos.Requests;

namespace IncidentSystem.Presentation.Dialogs;

internal class CustomerDialog(ICustomerService customerService)
{
    public void CreateCustomerDialog()
    {
        var customerName = "Hans";
        var emailAddress = "hans.mattin-lassei@Domain.COM";
        var phoneNumber = "070-345 67 80";

        var createCustomerRequest = new CreateCustomerRequest(customerName, emailAddress, phoneNumber);

        var result = customerService.CreateCustomer(createCustomerRequest);

        if (!result.Succeeded)
            Console.WriteLine(result.ErrorMessage);

        else
        {
            Console.WriteLine("Customer was created");

            if (result.Customer is not null)
                Console.WriteLine($"Customer ID: {result.Customer.CustomerId}");
        }
    }

    public void ViewCustomersDialog()
    {
        var result = customerService.GetAllCustomers();

        foreach (var customer in result.Customers)
            Console.WriteLine($"{customer.CustomerName} <{customer.EmailAddress}> ({customer.PhoneNumber})");
    }
}
