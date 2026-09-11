using IncidentManagement.Application.Customers.Dtos;

namespace IncidentManagement.Application.Customers;

public interface ICustomerSerivce
{
    CreateCustomerResponse CreateCustomer(CreateCustomerRequest request);
    GetCustomersResponse GetAllCustomers();
}
