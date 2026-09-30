using CustomerApp.Domain.Shared;

namespace CustomerApp.Tests.Domain;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void EnsureRequired_Should_ThrowExceptionWhenNullOrEmpty(string? value)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new PhoneNumber(value!));

        // Assert
        Assert.Equal("Phone number is required. (Parameter 'value')", exception.Message);
    }

    [Theory]
    [InlineData("+46731234567", "0046731234567")]
    [InlineData("0046731234567", "0046731234567")]
    public void Normalize_Should_NormalizeAreaCodeValue_ReturnValidPhoneNumber(
        string value,
        string expectedValue)
    {
        // Act
        var phoneNumber = new PhoneNumber(value);

        // Assert
        Assert.Equal(expectedValue, phoneNumber.Value);
    }

    [Theory]
    [InlineData("0046 73 123 45 67", "0046731234567")]
    [InlineData("+46 73 123 45 67", "0046731234567")]
    public void Normalize_Should_TrimAndReplaceWhiteSpaces_ReturnValidPhoneNumber(
        string value,
        string expectedValue)
    {
        // Act
        var phoneNumber = new PhoneNumber(value);

        // Assert
        Assert.Equal(expectedValue, phoneNumber.Value);
    }

    [Theory]
    [InlineData("+4673-1234567", "0046731234567")]
    [InlineData("004673-1234567", "0046731234567")]
    public void Normalize_Should_ReplaceDashes_ReturnValidPhoneNumber(
        string value,
        string expectedValue)
    {
        // Act
        var phoneNumber = new PhoneNumber(value);

        // Assert
        Assert.Equal(expectedValue, phoneNumber.Value);
    }

    [Theory]
    [InlineData("0701234567")]
    [InlineData("+123456")]
    [InlineData("0046ABC1234567")]
    [InlineData("00461234567890123456")]
    public void Validate_Should_ThrowExceptionWhenPhoneNumberHasInvalidFormat(string value)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new PhoneNumber(value));

        // Assert
        Assert.Equal(
            "Phone number has an invalid format. (Parameter 'value')",
            exception.Message);
    }

    [Fact]
    public void ToString_Should_ReturnPhoneNumberValue()
    {
        // Arrange
        var phoneNumber = new PhoneNumber("+46731234567");

        // Act
        var result = phoneNumber.ToString();

        // Assert
        Assert.Equal("0046731234567", result);
    }
}