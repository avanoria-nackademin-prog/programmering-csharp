using IncidentManagement.Application.Customers.Dtos;
using IncidentManagement.Domain.Customers;
using IncidentManagement.Domain.ValueObjects;

namespace IncidentManagement.Application.Customers;

internal class CustomerService(ICustomerStore customerStore) : ICustomerSerivce
{
    public CreateCustomerResponse CreateCustomer(CreateCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var id = new UniqueId(Guid.NewGuid());
        var customerName = new CustomerName(request.CustomerName);
        var emailAddress = new EmailAddress(request.EmailAddress);

        var phoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber)
            ? null
            : new PhoneNumber(request.PhoneNumber);

        var customer = new Customer(id, customerName, emailAddress, phoneNumber);

        var added = customerStore.Add(customer);

        return added
            ? new CreateCustomerResponse(true, customer, null)
            : new CreateCustomerResponse(false, null, "Unable to save customer.");
    }

    public GetCustomersResponse GetAllCustomers()
    {
        var customers = customerStore.GetAll();

        var response = new GetCustomersResponse(true, customers, null);

        return response;    
    }
}
