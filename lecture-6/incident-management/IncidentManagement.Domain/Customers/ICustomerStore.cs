namespace IncidentManagement.Domain.Customers;

public interface ICustomerStore
{
    bool Add(Customer customer);
    IReadOnlyList<Customer> GetAll();
}
