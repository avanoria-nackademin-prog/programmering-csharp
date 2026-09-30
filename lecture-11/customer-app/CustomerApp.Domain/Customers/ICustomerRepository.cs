namespace CustomerApp.Domain.Customers;

public interface ICustomerRepository
{
    void Add(Customer customer);
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(Guid id);
}
