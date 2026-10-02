namespace IncidentManagement.Domain.Incidents;

public sealed class Incident
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = "";
    public string Description { get; private set; } = "";
    public IncidentPriority Priority { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private Incident() { }

    private Incident(Guid id, string title, string description, IncidentPriority priority, DateTime createdAtUtc)
    {
        Id = id;
        Title = title;
        Description = description;
        Priority = priority;
        Status = IncidentStatus.New;
        CreatedAtUtc = createdAtUtc;
    }

    public static Incident Create(string title, string description, IncidentPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Incident title is required.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Incident description is required.", nameof(description));

        if (!Enum.IsDefined(priority))
            throw new ArgumentOutOfRangeException(nameof(priority));

        return new Incident(Guid.NewGuid(), title.Trim(), description.Trim(), priority, DateTime.UtcNow);
    }

    public void StartWork()
    {
        if (Status != IncidentStatus.New)
            throw new InvalidOperationException("Only new incidents can be started.");

        Status = IncidentStatus.InProgress;
    }

    public void Resolve()
    {
        if (Status == IncidentStatus.Resolved)
            throw new InvalidOperationException("Incident is already resolved.");

        Status = IncidentStatus.Resolved;
    }
}
