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
    }
}