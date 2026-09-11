using IncidentManagement.Domain.Customers;

namespace IncidentManagement.Infrastructure.Persistence.InMemory;

internal class InMemoryCustomerStore : ICustomerStore
{
    private readonly List<Customer> _customers = [];

    public bool Add(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        _customers.Add(customer);
        
        return true;
    }

    public IReadOnlyList<Customer> GetAll()
    {
        return _customers.OrderBy(x => x.CustomerName.Value).ToList();
    }
}
