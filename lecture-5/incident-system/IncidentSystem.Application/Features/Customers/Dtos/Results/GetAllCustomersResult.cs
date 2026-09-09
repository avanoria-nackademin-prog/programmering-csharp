using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Application.Features.Customers.Dtos.Results;

public record GetAllCustomersResult
(
    bool Succeeded,
    IReadOnlyList<Customer> Customers,
    string? ErrorMessage
);
