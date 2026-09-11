using IncidentManagement.Domain.ValueObjects;

namespace IncidentManagement.Domain.Customers;

public class Customer(UniqueId customerId, CustomerName customerName, EmailAddress emailAddress, PhoneNumber? phoneNumber = null)
{
    public UniqueId CustomerId { get; } = customerId;
    public CustomerName CustomerName { get; private set; } = customerName;
    public EmailAddress EmailAddress { get; set; } = emailAddress;
    public PhoneNumber? PhoneNumber { get; set; } = phoneNumber;

    public void Rename(CustomerName customerName)
    {
        CustomerName = customerName;
    }

    public void ChangeEmailAddress(EmailAddress emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public void ChangePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}

