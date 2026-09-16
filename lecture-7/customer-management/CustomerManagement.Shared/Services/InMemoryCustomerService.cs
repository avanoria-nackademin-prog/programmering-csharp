using CustomerManagement.Shared.Models;
using System.Net.Mail;

namespace CustomerManagement.Shared.Services;

public class InMemoryCustomerService : ICustomerService
{
    private readonly List<Customer> _customers = [];

    public Customer Create(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Enter a customer name.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Enter a customer email address.");

        name = name.Trim();
        email = email.Trim().ToLower();

        if (!MailAddress.TryCreate(email, out var emailAddress) || emailAddress.Address != email)
            throw new ArgumentException("Enter a valid email address.");

        var customer = new Customer
        {
            Name = name,
            Email = emailAddress.Address
        };

        _customers.Add(customer);

        return customer;
    }

    public bool Delete(Guid customerId)
    {
        var customer = _customers.FirstOrDefault(customer => customer.Id == customerId);

        if (customer is null)
            return false;

        return _customers.Remove(customer);
    }

    public IReadOnlyList<Customer> GetAll() => [.. _customers];
}