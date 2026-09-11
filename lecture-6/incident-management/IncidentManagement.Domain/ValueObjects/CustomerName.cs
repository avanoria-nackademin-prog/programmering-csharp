namespace IncidentManagement.Domain.ValueObjects;

public record CustomerName
{
    public string Value { get; }

    public CustomerName(string value, int minLength = 2)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Customer name is required.", nameof(value));

        var normalizedValue = value.Trim();

        if (normalizedValue.Length < minLength)
            throw new ArgumentException($"Customer name must contain at least {minLength} characters.", nameof(value));

        Value = normalizedValue;
    }

    public override string ToString() => Value;
}