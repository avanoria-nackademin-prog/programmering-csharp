using IncidentSystem.Application.Features.Customers.Dtos.Requests;
using IncidentSystem.Application.Features.Customers.Dtos.Results;

namespace IncidentSystem.Application.Features.Customers;

public interface ICustomerService
{
    CreateCustomerResult CreateCustomer(CreateCustomerRequest request);
    GetAllCustomersResult GetAllCustomers();
    GetCustomerResult GetCustomerByCustomerId(string customerId);
    UpdateCustomerResult UpdateCustomer(UpdateCustomerRequest request);
    DeleteCustomerResult DeleteCustomerByCustomerId(string customerId);
}
