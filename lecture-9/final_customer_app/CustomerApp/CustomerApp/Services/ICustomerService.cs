using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApp.Services;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
    Task CreateAsync(string name, string email, bool isCompany);
    Task DeleteAsync(Guid customerId);
}
