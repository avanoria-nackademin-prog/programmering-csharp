using CustomerApp.Domain.Shared;

namespace CustomerApp.Domain.Customers;

public sealed record Customer
{
    public Guid Id { get; }
    public string CustomerName { get; }
    public EmailAddress EmailAddress { get; }
    public PhoneNumber? PhoneNumber { get; }
    public bool IsCompany { get; }

    public Customer(Guid id, string customerName, EmailAddress emailAddress, PhoneNumber? phoneNumber, bool isCompany)
    {
        Id = EnsureRequiredId(id);

        var requiredCustomerName = EnsureRequiredCustomerName(customerName);
        CustomerName = NormalizeCustomerName(requiredCustomerName);

        EmailAddress = EnsureRequiredEmailAddress(emailAddress);
        PhoneNumber = phoneNumber;
        IsCompany = isCompany;
    }

    private static Guid EnsureRequiredId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Kundens ID får inte vara tomt.");

        return id;
    }

    private static string EnsureRequiredCustomerName(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Kundnamn krävs.");

        return customerName;
    }

    private static string NormalizeCustomerName(string customerName) =>
        customerName.Trim();

    private static EmailAddress EnsureRequiredEmailAddress(EmailAddress emailAddress)
    {
        return emailAddress is null 
            ? throw new ArgumentNullException(nameof(emailAddress)) : emailAddress;
    }
}
