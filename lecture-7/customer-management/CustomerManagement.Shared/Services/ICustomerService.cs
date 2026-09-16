using CustomerManagement.Shared.Models;

namespace CustomerManagement.Shared.Services;

public interface ICustomerService
{
    Customer Create(string name, string email);
    bool Delete(Guid customerId);
    IReadOnlyList<Customer> GetAll();

}
