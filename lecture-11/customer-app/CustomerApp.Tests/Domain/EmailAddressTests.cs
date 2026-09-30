using CustomerApp.Domain.Shared;

namespace CustomerApp.Tests.Domain;

public class EmailAddressTests
{
    [Theory]
    [InlineData("hans@example.com")]
    [InlineData("first.last@example.com")]
    [InlineData("hans+kurs@example.com")]
    public void Constructor_WithValidEmail_SetsValue(string email)
    {
        var emailAddress = new EmailAddress(email);

        Assert.Equal(email, emailAddress.Value);
    }

    [Fact]
    public void Constructor_WithSurroundingWhitespace_TrimsValue()
    {
        var emailAddress = new EmailAddress("  hans@example.com  ");

        Assert.Equal("hans@example.com", emailAddress.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithMissingEmail_ThrowsArgumentException(string? email)
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new EmailAddress(email!));

        Assert.Equal("E-postadress krävs.", exception.Message);
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("hans@")]
    [InlineData("@example.com")]
    public void Constructor_WithInvalidEmail_ThrowsArgumentException(string email)
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new EmailAddress(email));

        Assert.Equal("E-postadressen är inte giltig.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenMailAddressParsesToDifferentAddress_ThrowsArgumentException()
    {
        // MailAddress kan acceptera visningsnamn, men Value ska vara en ren e-postadress.
        var exception = Assert.Throws<ArgumentException>(
            () => new EmailAddress("Hans <hans@example.com>"));

        Assert.Equal("E-postadressen är inte giltig.", exception.Message);
    }
}
