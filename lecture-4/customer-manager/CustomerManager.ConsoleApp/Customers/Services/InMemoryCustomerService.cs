using CustomerManager.ConsoleApp.Customers.Models;

namespace CustomerManager.ConsoleApp.Customers.Services;

public class InMemoryCustomerService : ICustomerService
{
    private readonly List<Customer> _customerList = [];


    public Customer AddCustomer(string name, string email)
    {
        Customer customer = CreateCustomer(name, email);

        _customerList.Add(customer);

        return customer;
    }

    public IReadOnlyList<Customer> GetAllCustomers()
    {
        return _customerList
            .OrderBy(customer => customer.Name)
            .ToList();
    }

    private static Customer CreateCustomer(string name, string email)
        => new(Guid.NewGuid(), name.Trim(), email.ToLower());
}