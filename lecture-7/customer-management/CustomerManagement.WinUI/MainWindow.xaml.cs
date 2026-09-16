using CustomerManagement.Shared.Models;
using CustomerManagement.Shared.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomerManagement.WinUI;

public sealed partial class MainWindow : Window
{
    private readonly ICustomerService _customerService;

    public MainWindow(ICustomerService customerService)
    {
        _customerService = customerService;

        InitializeComponent();

        //AppWindow.Resize(new Windows.Graphics.SizeInt32(1000, 450));

        LoadCustomers();
    }

    private void LoadCustomers()
    {
        var customers = _customerService.GetAll();

        CustomersListView.SelectedItem = null;
        CustomersListView.ItemsSource = customers;

        StatusTextBlock.Text = $"Customers: {customers.Count}";
    }

    private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _customerService.Create(NameTextBox.Text, EmailTextBox.Text);

            LoadCustomers();

            NameTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;

            NameTextBox.Focus(FocusState.Programmatic);

        }
        catch (Exception ex)
        {
            await ShowMessageAsync("Validation", ex.Message);
        }
    }

    private async Task ShowMessageAsync(string title, string message)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = RootGrid.XamlRoot,
            Title = title,
            Content = message,
            CloseButtonText = "OK"
        };

        await dialog.ShowAsync();
    }

    private async void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
    {
        if (CustomersListView.SelectedItem is not Customer customer)
        {
            await ShowMessageAsync("No Customer Selected", "Select a customer to delete.");
            return;
        }

        var dialog = new ContentDialog
        {
            XamlRoot = RootGrid.XamlRoot,
            Title = "Confirm Deletion",
            Content = $"Delete {customer.Name}?",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Close",
            DefaultButton = ContentDialogButton.Close
        };

        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary)
            return;

        var deleted = _customerService.Delete(customer.Id);

        LoadCustomers();
    }
}
