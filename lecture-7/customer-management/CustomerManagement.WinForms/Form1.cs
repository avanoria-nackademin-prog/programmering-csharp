using CustomerManagement.Shared.Models;
using CustomerManagement.Shared.Services;

namespace CustomerManagement.WinForms;

public partial class Form1 : Form
{
    private readonly ICustomerService _customerService;

    public Form1(ICustomerService customerService)
    {
        _customerService = customerService;

        InitializeComponent();

        ConfigureCustomerGrid();

        addCustomerButton.Click += AddCustomerButton_Click;
        deleteSelectedButton.Click += DeleteSelectedButton_Click;

        AcceptButton = addCustomerButton;

        LoadCustomers();
    }


    private void AddCustomerButton_Click(object? sender, EventArgs e)
    {
        try
        {
            _customerService.Create(nameTextBox.Text, emailTextBox.Text);

            LoadCustomers();

            nameTextBox.Clear();
            emailTextBox.Clear();
            nameTextBox.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void DeleteSelectedButton_Click(object? sender, EventArgs e)
    {
        if (customersDataGridView.CurrentRow?.DataBoundItem is not Customer customer)
        {
            MessageBox.Show(this, "Select a customer to delete.", "No Customer Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirmation = MessageBox.Show(this, $"Delete {customer.Name}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
            return;

        var deleted = _customerService.Delete(customer.Id);

        LoadCustomers();
    }


    private void LoadCustomers()
    {
        var customers = _customerService.GetAll();
        
        customersDataGridView.DataSource = customers;
        statusLabel.Text = $"Customers: {customers.Count}";

    }

    private void ConfigureCustomerGrid()
    {
        customersDataGridView.AutoGenerateColumns = false;
        customersDataGridView.AllowUserToAddRows = false;
        customersDataGridView.AllowUserToDeleteRows = false;
        customersDataGridView.ReadOnly = true;
        customersDataGridView.MultiSelect = false;
        customersDataGridView.RowHeadersVisible = false;
        customersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        customersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        customersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CustomerNameColumn",
            HeaderText = "Customer name",
            DataPropertyName = nameof(Customer.Name)
        });

        customersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "EmailAddressColumn",
            HeaderText = "Email address",
            DataPropertyName = nameof(Customer.Email)
        });
    }
}
