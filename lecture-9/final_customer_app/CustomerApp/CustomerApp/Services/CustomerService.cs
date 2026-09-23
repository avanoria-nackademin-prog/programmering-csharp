using CustomerApp.Models;
using CustomerApp.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CustomerApp.Services;

public class CustomerService(ICustomerRepository repository) : ICustomerService
{
    public Task<List<Customer>> GetAllAsync()
    {
        return repository.GetAllAsync();
    }

    public async Task CreateAsync(string name, string email, bool isCompany)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                isCompany ? "Ange företagets namn." : "Ange kundens namn.");
        }

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Ange en e-postadress.");

        name = name.Trim();
        email = email.Trim();

        if (!MailAddress.TryCreate(email, out var address) || address.Address != email)
            throw new ArgumentException("Ange en giltig e-postadress.");

        var customers = await repository.GetAllAsync();

        customers.Add(new Customer
        {
            Name = name,
            Email = email,
            IsCompany = isCompany
        });

        await repository.SaveAllAsync(customers);
    }

    public async Task DeleteAsync(Guid customerId)
    {
        var customers = await repository.GetAllAsync();

        var removedCount = customers.RemoveAll(customer => customer.Id == customerId);

        if (removedCount > 0)
            await repository.SaveAllAsync(customers);
    }
}
