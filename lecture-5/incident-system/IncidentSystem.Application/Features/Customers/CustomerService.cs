using IncidentSystem.Application.Features.Customers.Dtos.Requests;
using IncidentSystem.Application.Features.Customers.Dtos.Results;
using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Application.Features.Customers;

internal class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public CreateCustomerResult CreateCustomer(CreateCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Customer customer;

        try
        {
            var customerId = Guid.NewGuid();

            customer = new Customer
            (
                customerId,
                request.CustomerName,
                request.EmailAddress,
                request.PhoneNumber
            );
        }
        catch(Exception ex)
        {
            return new CreateCustomerResult(false, null, ex.Message);
        }

        bool saved = customerRepository.Create(customer);

        return saved
            ? new CreateCustomerResult(true, customer, null)
            : new CreateCustomerResult(false, null, "Unable to save customer");

    }

    public GetAllCustomersResult GetAllCustomers()
    {
        var customers = customerRepository.GetAll();
        return new GetAllCustomersResult(true, customers, null);
    }

    public GetCustomerResult GetCustomerByCustomerId(string customerId)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            return new GetCustomerResult(false, null, "Customer Id is required");

        var customer = customerRepository.GetById(customerId);

        if (customer is null)
            return new GetCustomerResult(false, null, $"Customer with id '{customerId}' was not found");

        return new GetCustomerResult(true, customer, null);

    }

    public UpdateCustomerResult UpdateCustomer(UpdateCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.UpdatedCustomer.CustomerId))
            return new UpdateCustomerResult(false, null, "Customer Id is required");

        var customer = customerRepository.GetById(request.UpdatedCustomer.CustomerId);

        if (customer is null)
            return new UpdateCustomerResult(false, null, $"Customer with id '{request.UpdatedCustomer.CustomerId}' was not found");

        var updated = customerRepository.Update(customer);

        return updated
            ? new UpdateCustomerResult(true, customer, null)
            : new UpdateCustomerResult(false, null, "Unable to update customer");
    }

    public DeleteCustomerResult DeleteCustomerByCustomerId(string customerId)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            return new DeleteCustomerResult(false, "Customer Id is required");

        var customer = customerRepository.GetById(customerId);

        if (customer is null)
            return new DeleteCustomerResult(false, $"Customer with id '{customerId}' was not found");

        var deleted = customerRepository.Delete(customer);

        return deleted
            ? new DeleteCustomerResult(true, null)
            : new DeleteCustomerResult(false, "Unable to delete customer");
    }
}
