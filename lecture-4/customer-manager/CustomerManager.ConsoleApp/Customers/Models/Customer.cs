namespace CustomerManager.ConsoleApp.Customers.Models;

public record Customer
(
    Guid Id, 
    string Name,
    string Email
);