using CustomerApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApp.Repositories;

public class JsonFileCustomerRepository : ICustomerRepository
{
    public Task<List<Customer>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task SaveAllAsync(IEnumerable<Customer> customers)
    {
        throw new System.NotImplementedException();
    }
}
