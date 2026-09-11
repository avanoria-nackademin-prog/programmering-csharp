namespace IncidentManagement.Application.Customers.Dtos;

public record CreateCustomerRequest
(
    string CustomerName,
    string EmailAddress,
    string? PhoneNumber
);
