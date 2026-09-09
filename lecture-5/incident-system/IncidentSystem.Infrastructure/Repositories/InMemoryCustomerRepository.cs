using IncidentSystem.Domain.Customers;
using IncidentSystem.Infrastructure.Stores;

namespace IncidentSystem.Infrastructure.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    public void Create(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        InMemoryCustomerStore.Customers.Add(customer);
    }

    public IReadOnlyList<Customer> GetAll()
    {
        var customers = InMemoryCustomerStore.Customers;

        return customers;
    }

    public Customer? GetById(string customerId)
    {
        var customer = InMemoryCustomerStore.Customers.FirstOrDefault(c => c.CustomerId == customerId);

        return customer;
    }


    public void Update(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var index = InMemoryCustomerStore.Customers.FindIndex(c => c.CustomerId == customer.CustomerId);

        if (index == -1)
            throw new KeyNotFoundException($"Customer with Id '{customer.CustomerId}' was not found.");

        InMemoryCustomerStore.Customers[index] = customer;
    }

    public void Delete(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        InMemoryCustomerStore.Customers.Remove(customer);
    }

}
