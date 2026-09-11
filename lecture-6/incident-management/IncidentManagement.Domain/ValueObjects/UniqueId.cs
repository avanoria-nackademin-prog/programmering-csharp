namespace IncidentManagement.Domain.ValueObjects;

public record UniqueId
{
    public Guid Value { get; }

    public UniqueId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Id is required.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value.ToString();
}