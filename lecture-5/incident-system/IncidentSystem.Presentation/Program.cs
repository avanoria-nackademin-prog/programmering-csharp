using IncidentSystem.Domain.Customers;

var customerId = Guid.NewGuid();
var customerName = "Hans";
var emailAddress = "Hans.mattin-Lasseidomain.COm";
var phoneNumber = "073-123 45 67";


var customer = new Customer(customerId, customerName, emailAddress, phoneNumber);


Console.WriteLine(customer.CustomerId);
Console.WriteLine(customer.CustomerName);
Console.WriteLine(customer.EmailAddress);

Console.WriteLine(customer.PhoneNumber);
