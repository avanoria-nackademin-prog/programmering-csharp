using System.Net.Mail;

namespace IncidentManagement.Domain.ValueObjects;

public record EmailAddress
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email address is required.", nameof(value));

        var normalizedValue = value.Trim().ToLower();

        if (!MailAddress.TryCreate(normalizedValue, out var emailAddress) || !string.Equals(emailAddress.Address, normalizedValue, StringComparison.Ordinal))
            throw new ArgumentException("Email address must be valid.", nameof(value));

        Value = normalizedValue;
    }

    public override string ToString() => Value;
}


