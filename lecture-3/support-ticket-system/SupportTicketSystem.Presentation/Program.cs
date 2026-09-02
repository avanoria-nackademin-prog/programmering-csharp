using SupportTicketSystem.Presentation.Services;

var customerService = new CustomerService();

var customerDialogService = new CustomerDialogService(customerService);

customerDialogService.CreateCustomerDialog();