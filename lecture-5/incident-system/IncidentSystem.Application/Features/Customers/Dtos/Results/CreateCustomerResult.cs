using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Application.Features.Customers.Dtos.Results;

public record CreateCustomerResult
(
    bool Succeeded,
    Customer? Customer,
    string? ErrorMessage
);
