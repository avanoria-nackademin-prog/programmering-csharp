using IncidentManagement.Application.Customers;
using IncidentManagement.Application.Customers.Dtos;

namespace IncidentManagement.ConsoleApp.Dialogs.Customers;

internal class CustomerDialog(ICustomerSerivce customerService)
{
    public void ShowAddCustomer()
    {
        var request = new CreateCustomerRequest("","","");

        var response = customerService.CreateCustomer(request);

        if (response.Succeeded)
            Console.WriteLine("Customer created");
        else 
            Console.WriteLine(response.ErrorMessage);
    }
}
