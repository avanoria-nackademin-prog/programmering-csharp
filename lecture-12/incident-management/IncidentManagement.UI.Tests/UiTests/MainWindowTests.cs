using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;
using IncidentManagement.UI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using NSubstitute;

namespace IncidentManagement.UI.Tests.UiTests;

[TestClass]
public class MainWindowTests
{
    [UITestMethod]
    public void MainWindow_Should_BindControlsToViewModel()
    {
        // Arrange
        var incidentService = Substitute.For<IIncidentService>();

        var viewModel = new MainViewModel(incidentService)
        {
            Title = "Cannot sign in",
            Description = "The user cannot sign in.",
            SelectedPriority = nameof(IncidentPriority.High)
        };

        var incident = Incident.Create(
            "Existing incident",
            "An incident already in the list.",
            IncidentPriority.Normal);

        viewModel.Incidents.Add(incident);

        // Act
        var window = new MainWindow(viewModel);
        var root = window.Content as FrameworkElement;

        Assert.IsNotNull(root);

        var titleTextBox = root.FindName("TitleTextBox") as TextBox;
        var descriptionTextBox = root.FindName("DescriptionTextBox") as TextBox;
        var priorityComboBox = root.FindName("PriorityComboBox") as ComboBox;
        var createIncidentButton = root.FindName("CreateIncidentButton") as Button;
        var incidentsListView = root.FindName("IncidentsListView") as ListView;

        // Assert
        Assert.IsNotNull(titleTextBox);
        Assert.IsNotNull(descriptionTextBox);
        Assert.IsNotNull(priorityComboBox);
        Assert.IsNotNull(createIncidentButton);
        Assert.IsNotNull(incidentsListView);

        Assert.AreEqual(viewModel.Title, titleTextBox.Text);
        Assert.AreEqual(viewModel.Description, descriptionTextBox.Text);
        Assert.AreEqual(viewModel.SelectedPriority, priorityComboBox.SelectedItem);

        Assert.AreSame(viewModel.CreateIncidentCommand, createIncidentButton.Command);
        Assert.AreSame(viewModel.Incidents, incidentsListView.ItemsSource);
        Assert.AreEqual(1, incidentsListView.Items.Count);
    }
}
