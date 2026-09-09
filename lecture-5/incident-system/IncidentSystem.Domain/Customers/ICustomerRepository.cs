namespace IncidentSystem.Domain.Customers;

public interface ICustomerRepository
{
    void Create(Customer customer);
    void Delete(Customer customer);
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(string customerId);
    void Update(Customer customer);
}