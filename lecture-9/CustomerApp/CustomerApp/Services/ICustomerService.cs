using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApp.Services;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomersAsync();
    Task CreateCustomerAsync(string name, string email, bool isCompany);
    Task DeleteCustomerAsync(Guid customerId);
}
