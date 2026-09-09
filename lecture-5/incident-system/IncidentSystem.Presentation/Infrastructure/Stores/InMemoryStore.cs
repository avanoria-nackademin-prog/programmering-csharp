using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Presentation.Infrastructure.Stores;

internal class InMemoryStore
{
    public List<Customer> Customers { get; set; } = [];
}
