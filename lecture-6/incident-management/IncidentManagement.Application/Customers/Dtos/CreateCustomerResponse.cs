using IncidentManagement.Domain.Customers;

namespace IncidentManagement.Application.Customers.Dtos;

public record CreateCustomerResponse
(
    bool Succeeded,
    Customer? Customer,
    string? ErrorMessage
);
