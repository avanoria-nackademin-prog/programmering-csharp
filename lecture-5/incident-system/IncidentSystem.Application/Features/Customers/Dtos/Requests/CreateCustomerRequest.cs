namespace IncidentSystem.Application.Features.Customers.Dtos.Requests;

public record CreateCustomerRequest
(
    string CustomerName,
    string EmailAddress,
    string? PhoneNumber
);
