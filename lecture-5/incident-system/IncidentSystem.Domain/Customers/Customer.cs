namespace IncidentSystem.Domain.Customers;

public class Customer(Guid customerId, string customerName, string emailAddress, string? phoneNumber)
{
    public string CustomerId { get; private set; } = NormalizeRequiredCustomerId(customerId);
    public string CustomerName { get; private set; } = NormalizeRequiredName(nameof(CustomerName),customerName);
    public string EmailAddress { get; private set; } = NormalizeRequiredEmailAddress(emailAddress);
    public string? PhoneNumber { get; private set; } = NormailizePhoneNumber(phoneNumber);

    private static string NormalizeRequiredCustomerId(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new InvalidOperationException("Customer id is required");

        return customerId.ToString();
    }


    private static string NormalizeRequiredName(string propertyName, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException($"{propertyName} is required");

        if (name.Length < 2)
            throw new InvalidOperationException($"{propertyName} must be a valid name and contain at least 2 letters");

        return name.Trim();
    }

    private static string NormalizeRequiredEmailAddress(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            throw new InvalidOperationException("Email is required");

        if (!emailAddress.Contains('@'))
            throw new InvalidOperationException("Email must be a valid email address");

        return emailAddress.Trim().ToLower();
    }

    private static string? NormailizePhoneNumber(string? phoneNumber)
    {
        if (!string.IsNullOrWhiteSpace(phoneNumber))
            phoneNumber = phoneNumber.Trim().Replace("-", "").Replace(" ", "");

        return phoneNumber;
    }


    public void Rename(string customerName)
    {
        CustomerName = NormalizeRequiredName(nameof(CustomerName), customerName);
    }

    public void ChangeEmailAddress(string emailAddress)
    {
        EmailAddress = NormalizeRequiredEmailAddress(emailAddress);
    }

    public void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = NormailizePhoneNumber(phoneNumber);
    }
}