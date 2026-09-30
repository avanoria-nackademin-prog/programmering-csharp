using System.Net.Mail;

namespace CustomerApp.Domain.Shared;

public record EmailAddress
{
    public string Value { get; }

    public EmailAddress(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            throw new ArgumentException("E-postadress krävs.");

        var normalizedEmailAddress = emailAddress.Trim();

        try
        {
            var parsedEmailAddress = new MailAddress(normalizedEmailAddress);

            if (!string.Equals(parsedEmailAddress.Address, normalizedEmailAddress, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("E-postadressen är inte giltig.");
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("E-postadressen är inte giltig.", ex);
        }

        Value = normalizedEmailAddress;
    }
}