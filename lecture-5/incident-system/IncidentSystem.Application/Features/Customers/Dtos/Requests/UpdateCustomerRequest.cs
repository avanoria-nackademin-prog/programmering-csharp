using IncidentSystem.Domain.Customers;

namespace IncidentSystem.Application.Features.Customers.Dtos.Requests;

public record UpdateCustomerRequest
(
    Customer UpdatedCustomer
);
