using CustomerApp.Domain.Customers;
using CustomerApp.Domain.Shared;

namespace CustomerApp.Application.Customers;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public Customer CreateCustomer(CreateCustomerRequest request)
    {
        var customerId = Guid.NewGuid();

        var customer = new Customer(
            customerId,
            request.CustomerName,
            new EmailAddress(request.Email),
            string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : new PhoneNumber(request.PhoneNumber),
            request.IsCompany);

        customerRepository.Add(customer);

        return customer;
    }

    public IReadOnlyList<Customer> GetAllCustomers()
    {
        throw new NotImplementedException();
    }
}