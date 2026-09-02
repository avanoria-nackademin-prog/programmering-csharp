namespace SupportTicketSystem.Presentation.Models;

public class Customer(Guid id, string name, string emailAddress, string? phoneNumber = null)
{
    public Guid Id { get; init; } = id;
    public string Name { get; set; } = name;
    public string EmailAddress { get; set; } = emailAddress;
    public string? PhoneNumber { get; set; } = phoneNumber;
}
