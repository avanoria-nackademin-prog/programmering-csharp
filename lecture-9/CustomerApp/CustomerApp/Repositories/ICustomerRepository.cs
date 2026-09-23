using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApp.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task SaveAllAsync(IEnumerable<Customer> customers);
}
