namespace IncidentSystem.Application.Features.Customers.Dtos.Results;

public record DeleteCustomerResult
(
    bool Succeeded,
    string? ErrorMessage
);