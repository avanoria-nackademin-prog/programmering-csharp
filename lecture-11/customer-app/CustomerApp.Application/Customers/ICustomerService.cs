using CustomerApp.Domain.Customers;

namespace CustomerApp.Application.Customers;

public interface ICustomerService
{
    Customer CreateCustomer(CreateCustomerRequest request);
    IReadOnlyList<Customer> GetAllCustomers();
}
