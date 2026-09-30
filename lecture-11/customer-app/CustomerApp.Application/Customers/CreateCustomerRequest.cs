namespace CustomerApp.Application.Customers;

public record CreateCustomerRequest
(
    string CustomerName,
    string Email,
    string? PhoneNumber,
    bool IsCompany
);
