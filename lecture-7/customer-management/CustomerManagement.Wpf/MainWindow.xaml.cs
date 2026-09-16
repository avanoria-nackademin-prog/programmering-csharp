using CustomerManagement.Shared.Models;
using CustomerManagement.Shared.Services;
using System.Windows;

namespace CustomerManagement.Wpf;

public partial class MainWindow : Window
{
    private readonly ICustomerService _customerService;

    public MainWindow(ICustomerService customerSerivce)
    {
        _customerService = customerSerivce;

        InitializeComponent();

        LoadCustomers();
    }

    private void LoadCustomers()
    {
        var customers = _customerService.GetAll();

        CustomersDataGrid.ItemsSource = customers;
        StatusTextBlock.Text = $"Customers: {customers.Count}";
    }

    private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _customerService.Create(NameTextBox.Text, EmailTextBox.Text);

            LoadCustomers();

            NameTextBox.Clear();
            EmailTextBox.Clear();
            NameTextBox.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
    {
        if (CustomersDataGrid.SelectedItem is not Customer customer)
        {
            MessageBox.Show(this, "Select a cstuomer to delete", "No Customer Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var confirmation = MessageBox.Show(this, $"Delete {customer.Name}", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (confirmation != MessageBoxResult.Yes)
            return;

        var deleted = _customerService.Delete(customer.Id);

        LoadCustomers();
    }
}