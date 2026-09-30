using CustomerApp.Application.Customers;
using CustomerApp.Domain.Customers;
using CustomerApp.Domain.Shared;
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

    [Fact]
    public void GetAllCustomers_ReturnsListOfCustomers()
    {
        // Arrange
        var expectedList = new[] {
            new Customer(Guid.NewGuid(), "Hans", new EmailAddress("hans@domain.com"), new PhoneNumber("+460731234567"), false),
            new Customer(Guid.NewGuid(), "Tommy", new EmailAddress("tommy@domain.com"), new PhoneNumber("+460738763211"), false)
        };

        var repository = Substitute.For<ICustomerRepository>();
        repository.GetAll().Returns(expectedList);

        var service = new CustomerService(repository);

        
        // Act
        var customers = service.GetAllCustomers();


        // Assert
        Assert.Same(expectedList, customers);
        repository.Received(1).GetAll();
    }

    [Fact]
    public void CreateCustomer_WithNoPhoneNumber_CreatesCustomerWithoutPhoneNumber()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "hans@domain.com", null, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        var customer = service.CreateCustomer(request);

        // Assert
        Assert.Null(customer.PhoneNumber);
        repository.Received(1).Add(Arg.Is<Customer>(savedCustomer =>
            savedCustomer.Id == customer.Id &&
            savedCustomer.PhoneNumber == null));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void CreateCustomer_WithBlankPhoneNumber_CreatesCustomerWithoutPhoneNumber(string phoneNumber)
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "hans@domain.com", phoneNumber, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        var customer = service.CreateCustomer(request);

        // Assert
        Assert.Null(customer.PhoneNumber);
    }

    [Fact]
    public void CreateCustomer_WithPhoneNumber_CreatesCustomerWithPhoneNumber()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "hans@domain.com", "+460731234567", false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        var customer = service.CreateCustomer(request);

        // Assert
        Assert.NotNull(customer.PhoneNumber);
        Assert.Equal("00460731234567", customer.PhoneNumber.Value);
    }

    [Fact]
    public void CreateCustomer_WithCompanyRequest_CreatesCompanyCustomer()
    {
        // Arrange
        var request = new CreateCustomerRequest("Avanoria AB", "info@avanoria.se", null, true);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        var customer = service.CreateCustomer(request);

        // Assert
        Assert.True(customer.IsCompany);
        Assert.Equal("Avanoria AB", customer.CustomerName);
    }

    [Fact]
    public void CreateCustomer_WithValidRequest_AddsCustomerExactlyOnce()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "hans@domain.com", null, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        service.CreateCustomer(request);

        // Assert
        repository.Received(1).Add(Arg.Any<Customer>());
    }

    [Fact]
    public void CreateCustomer_ReturnsSameCustomerThatWasSaved()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "hans@domain.com", null, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);
        Customer? savedCustomer = null;

        repository
            .When(repository => repository.Add(Arg.Any<Customer>()))
            .Do(call => savedCustomer = call.Arg<Customer>());

        // Act
        var returnedCustomer = service.CreateCustomer(request);

        // Assert
        Assert.NotNull(savedCustomer);
        Assert.Same(savedCustomer, returnedCustomer);
    }

    [Fact]
    public void CreateCustomer_WithInvalidEmail_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "not-an-email", null, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.CreateCustomer(request));

        // Assert
        Assert.NotNull(exception);
        repository.DidNotReceive().Add(Arg.Any<Customer>());
    }

    [Fact]
    public void CreateCustomer_WithBlankEmail_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", " ", null, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        Assert.Throws<ArgumentException>(() => service.CreateCustomer(request));

        // Assert
        repository.DidNotReceive().Add(Arg.Any<Customer>());
    }

    [Fact]
    public void CreateCustomer_WithBlankCustomerName_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateCustomerRequest(" ", "hans@domain.com", null, false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        Assert.Throws<ArgumentException>(() => service.CreateCustomer(request));

        // Assert
        repository.DidNotReceive().Add(Arg.Any<Customer>());
    }

    [Fact]
    public void CreateCustomer_WithInvalidPhoneNumber_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateCustomerRequest("Hans", "hans@domain.com", "not-a-phone-number", false);
        var repository = Substitute.For<ICustomerRepository>();
        var service = new CustomerService(repository);

        // Act
        Assert.Throws<ArgumentException>(() => service.CreateCustomer(request));

        // Assert
        repository.DidNotReceive().Add(Arg.Any<Customer>());
    }

    [Fact]
    public void GetAllCustomers_WhenRepositoryReturnsEmptyList_ReturnsEmptyList()
    {
        // Arrange
        IReadOnlyList<Customer> expectedCustomers = [];
        var repository = Substitute.For<ICustomerRepository>();
        repository.GetAll().Returns(expectedCustomers);
        var service = new CustomerService(repository);

        // Act
        var customers = service.GetAllCustomers();

        // Assert
        Assert.Empty(customers);
        Assert.Same(expectedCustomers, customers);
        repository.Received(1).GetAll();
    }
}
