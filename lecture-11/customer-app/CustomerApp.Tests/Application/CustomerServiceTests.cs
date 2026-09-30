using CustomerApp.Application.Customers;
using CustomerApp.Domain.Customers;
using NSubstitute;

namespace CustomerApp.Tests.Application;

public class CustomerServiceTests
{
    [Fact]
    public void CreateCustomer_GenerateIdAndSaveCustomer_ReturnCustomer()
    {
        // Arrange
        var createCustomerRequest = new CreateCustomerRequest("Hans", "hans@domain.com", "+460731234567", false);

        ICustomerRepository repository = Substitute.For<ICustomerRepository>();
        var customerService = new CustomerService(repository);

        // Act
        var createdCustomer = customerService.CreateCustomer(createCustomerRequest);


        // Assert
        Assert.NotEqual(Guid.Empty, createdCustomer.Id);
        Assert.Equal(createCustomerRequest.CustomerName, createdCustomer.CustomerName);
        Assert.Equal(createCustomerRequest.Email, createdCustomer.EmailAddress.Value);
        Assert.False(createdCustomer.IsCompany);

        repository.Received(1).Add(Arg.Is<Customer>(customer =>
            customer.Id == createdCustomer.Id &&
            customer.CustomerName == createCustomerRequest.CustomerName));
    }
}
