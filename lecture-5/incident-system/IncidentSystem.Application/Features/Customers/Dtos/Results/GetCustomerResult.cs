using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Application.Features.Customers.Dtos.Results;

public record GetCustomerResult
(
    bool Succeeded,
    Customer? Customer,
    string? ErrorMessage
);
