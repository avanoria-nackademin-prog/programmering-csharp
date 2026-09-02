using SupportTicketSystem.Presentation.Models;

namespace SupportTicketSystem.Presentation.Services;

public class CustomerService
{
    private readonly List<Customer> _customerList = [];

    public CreateCustomerResult CreateCustomer(string name, string emailAddress, string? phoneNumber = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name must be provided.");

            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentException("Email address must be provided.");

            var customer = new Customer(Guid.NewGuid(), name, emailAddress, phoneNumber);

            _customerList.Add(customer);

            return new CreateCustomerResult(true, customer);
        }
        catch (Exception ex)
        {
            return new CreateCustomerResult(false, null, ex.Message);
        }

    }
}
