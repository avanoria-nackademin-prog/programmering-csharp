namespace IncidentManagement.Domain.ValueObjects;

public record PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number is required.", nameof(value));

        var normalizedValue = value
            .Trim()
            .Replace(" ", "")
            .Replace("-", "");

        if (normalizedValue.Length == 0)
            throw new ArgumentException("Phone number is required.", nameof(value));

        Value = normalizedValue;
    }

    public override string ToString() => Value;
}
