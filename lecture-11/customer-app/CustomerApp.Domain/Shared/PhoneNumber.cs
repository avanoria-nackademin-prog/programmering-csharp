namespace CustomerApp.Domain.Shared;

public sealed record PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string phoneNumber)
    {
        var requiredValue = EnsureRequired(phoneNumber);
        var normalizedValue = Normalize(requiredValue);

        Validate(normalizedValue);

        Value = normalizedValue;
    }

    private static string EnsureRequired(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number is required.", nameof(value));

        return value;
    }

    private static string Normalize(string value)
    {
        var normalizedValue = string.Concat(value.Where(character => !char.IsWhiteSpace(character)))
            .Replace("-", "");

        if (normalizedValue.StartsWith('+'))
            normalizedValue = $"00{normalizedValue[1..]}";

        return normalizedValue;
    }

    private static void Validate(string value)
    {
        if (!value.StartsWith("00") ||
            value.Length is < 9 or > 17 ||
            !value[2..].All(char.IsAsciiDigit))
        {
            throw new ArgumentException("Phone number has an invalid format.", nameof(value));
        }
    }

    public override string ToString() => Value;
}