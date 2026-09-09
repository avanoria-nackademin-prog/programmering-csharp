namespace IncidentSystem.Domain.Customers;

public interface ICustomerRepository
{
    bool Create(Customer customer);
    bool Delete(Customer customer);
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(string customerId);
    bool Update(Customer customer);
}