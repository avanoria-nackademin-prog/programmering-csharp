using CustomerApp.Domain.Shared;

namespace CustomerApp.Tests.Domain;

public class EmailAddressTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void EnsureRequired_Should_ThrowExceptionWhenNullOrEmpty(string? value)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new EmailAddress(value!));

        // Assert
        Assert.Equal("E-postadress krävs. (Parameter 'value')", exception.Message);
    }

    [Theory]
    [InlineData("  hans@example.com  ", "hans@example.com")]
    [InlineData("\tkontakt@avanoria.se\n", "kontakt@avanoria.se")]
    public void Normalize_Should_TrimWhiteSpaces_ReturnValidEmailAddress(string value, string expectedValue)
    {
        // Act
        var emailAddress = new EmailAddress(value);

        // Assert
        Assert.Equal(expectedValue, emailAddress.Value);
    }

    [Theory]
    [InlineData("hans@example.com")]
    [InlineData("kontakt@avanoria.se")]
    public void Validate_Should_CreateEmailAddressWhenEmailAddressIsValid(string value)
    {
        // Act
        var emailAddress = new EmailAddress(value);

        // Assert
        Assert.Equal(value, emailAddress.Value);
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("name@")]
    [InlineData("@example.com")]
    [InlineData("Hans <hans@example.com>")]
    public void Validate_Should_ThrowExceptionWhenEmailAddressHasInvalidFormat(string value)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new EmailAddress(value));

        // Assert
        Assert.Equal("E-postadressen är inte giltig. (Parameter 'value')", exception.Message);
    }

    [Fact]
    public void ToString_Should_ReturnEmailAddressValue()
    {
        // Arrange
        var emailAddress = new EmailAddress("hans@example.com");

        // Act
        var result = emailAddress.ToString();

        // Assert
        Assert.Equal("hans@example.com", result);
    }
}