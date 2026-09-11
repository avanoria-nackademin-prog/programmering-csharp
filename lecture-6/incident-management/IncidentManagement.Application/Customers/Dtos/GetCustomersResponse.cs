using IncidentManagement.Domain.Customers;

namespace IncidentManagement.Application.Customers.Dtos;

public record GetCustomersResponse
(
    bool Succeeded,
    IReadOnlyList<Customer>? Customers,
    string? ErrorMessage
);