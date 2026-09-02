namespace SupportTicketSystem.Presentation.Models;

public record CreateCustomerResult
(
    bool Succeeded,
    Customer? Customer = null,
    string? ErrorMessage = null
);


