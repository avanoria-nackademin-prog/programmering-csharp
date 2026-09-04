using CustomerManager.ConsoleApp.Customers.Models;

namespace CustomerManager.ConsoleApp.Customers.Services;

public interface ICustomerService
{
    Customer AddCustomer(string name, string email);
    IReadOnlyList<Customer> GetAllCustomers();
}