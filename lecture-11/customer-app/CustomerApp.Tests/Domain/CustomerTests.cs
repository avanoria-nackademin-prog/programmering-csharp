using CustomerApp.Domain.Customers;
using CustomerApp.Domain.Shared;

namespace CustomerApp.Tests.Domain;

public class CustomerTests
{
    [Fact]
    public void Constructor_Should_CreateCustomerWhenValuesAreValid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var emailAddress = new EmailAddress("hans@example.com");
        var phoneNumber = new PhoneNumber("+46731234567");

        // Act
        var customer = new Customer(id, "Hans Mattin-Lassei", emailAddress, phoneNumber, isCompany: false);

        // Assert
        Assert.Equal(id, customer.Id);
        Assert.Equal("Hans Mattin-Lassei", customer.CustomerName);
        Assert.Equal(emailAddress, customer.EmailAddress);
        Assert.Equal(phoneNumber, customer.PhoneNumber);
        Assert.False(customer.IsCompany);
    }

    [Fact]
    public void EnsureRequiredId_Should_ThrowExceptionWhenIdIsEmpty()
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            new Customer(Guid.Empty, "Hans Mattin-Lassei", new EmailAddress("hans@example.com"), null, isCompany: false));

        // Assert
        Assert.Equal("Kundens ID får inte vara tomt.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void EnsureRequiredCustomerName_Should_ThrowExceptionWhenNameIsNullOrEmpty(string? customerName)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            new Customer( Guid.NewGuid(),  customerName!, new EmailAddress("hans@example.com"), null, isCompany: false));

        // Assert
        Assert.Equal("Kundnamn krävs.", exception.Message);
    }

    [Theory]
    [InlineData("  Hans Mattin-Lassei  ", "Hans Mattin-Lassei")]
    [InlineData("\tAvanoria AB\n", "Avanoria AB")]
    public void NormalizeCustomerName_Should_TrimWhiteSpaces_ReturnValidCustomerName(string customerName, string expectedCustomerName)
    {
        // Act
        var customer = new Customer(Guid.NewGuid(), customerName, new EmailAddress("hans@example.com"), null, isCompany: false);

        // Assert
        Assert.Equal(expectedCustomerName, customer.CustomerName);
    }

    [Fact]
    public void EnsureRequiredEmailAddress_Should_ThrowExceptionWhenEmailAddressIsNull()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new Customer(Guid.NewGuid(), "Hans Mattin-Lassei", null!, null, isCompany: false));

        // Assert
        Assert.Equal("emailAddress", exception.ParamName);
    }

    [Fact]
    public void Constructor_Should_CreateCustomerWithoutPhoneNumber()
    {
        // Act
        var customer = new Customer(Guid.NewGuid(), "Hans Mattin-Lassei", new EmailAddress("hans@example.com"), null, isCompany: false);

        // Assert
        Assert.Null(customer.PhoneNumber);
    }

    [Fact]
    public void Constructor_Should_SetIsCompanyWhenCustomerIsCompany()
    {
        // Act
        var customer = new Customer(Guid.NewGuid(), "Avanoria AB", new EmailAddress("info@avanoria.se"), null, isCompany: true);

        // Assert
        Assert.True(customer.IsCompany);
    }
}
