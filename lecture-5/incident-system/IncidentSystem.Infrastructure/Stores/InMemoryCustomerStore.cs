using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Infrastructure.Stores;

internal static class InMemoryCustomerStore
{
    public static List<Customer> Customers { get; set; } = [];
}

