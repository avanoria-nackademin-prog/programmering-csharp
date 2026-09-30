using System.Net.Mail;

namespace CustomerApp.Domain.Shared;

public sealed record EmailAddress
{
    public string Value { get; }

    public EmailAddress(string emailAddress)
    {
        var requiredValue = EnsureRequired(emailAddress);
        var normalizedValue = Normalize(requiredValue);

        Validate(normalizedValue);

        Value = normalizedValue;
    }

    private static string EnsureRequired(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("E-postadress krävs.", nameof(value));

        return value;
    }

    private static string Normalize(string value) => value.Trim();

    private static void Validate(string value)
    {
        try
        {
            var parsedEmailAddress = new MailAddress(value);

            if (!string.Equals( parsedEmailAddress.Address, value, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("E-postadressen är inte giltig.", nameof(value));
            }
        }
        catch (FormatException exception)
        {
            throw new ArgumentException("E-postadressen är inte giltig.", nameof(value), exception);
        }
    }

    public override string ToString() => Value;
}