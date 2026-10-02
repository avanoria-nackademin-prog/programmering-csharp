using IncidentManagement.Api.Incidents.Dtos;
using IncidentManagement.Domain.Incidents;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace IncidentManagement.Tests.Api;

public class IncidentApiTests
{
    [Fact]
    public async Task PostIncident_Should_SaveIncidentAndReturnItWhenRequested()
    {
        // Arrange
        var temporaryDirectory = Path.Combine(
            Path.GetTempPath(),
            "IncidentManagement.EndToEndTests",
            Guid.NewGuid().ToString("N"));

        Directory.CreateDirectory(temporaryDirectory);

        var databasePath = Path.Combine(temporaryDirectory, "incidents.db");
        var port = GetAvailablePort();
        var baseAddress = new Uri($"http://127.0.0.1:{port}");
        var projectPath = FindApiProjectPath();

        var startInfo = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = Path.GetDirectoryName(projectPath)!,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        startInfo.ArgumentList.Add("run");
        startInfo.ArgumentList.Add("--no-build");
        startInfo.ArgumentList.Add("--no-launch-profile");
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(projectPath);
        startInfo.ArgumentList.Add("--");
        startInfo.ArgumentList.Add("--urls");
        startInfo.ArgumentList.Add(baseAddress.ToString());

        startInfo.Environment["ConnectionStrings__Incidents"] =
            $"Data Source={databasePath};Pooling=False";

        // Simulera att den externa notifieringstjänsten inte är tillgänglig.
        startInfo.Environment["Notifications__BaseUrl"] =
            "http://127.0.0.1:1";

        using var apiProcess = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Kunde inte starta Incident API.");

        try
        {
            using var client = new HttpClient
            {
                BaseAddress = baseAddress
            };

            await WaitForApiToStartAsync(client, apiProcess);

            var request = new CreateIncidentRequest(
                "Unable to sign in",
                "The user receives an error when signing in.",
                "High");

            // Act
            var postResponse = await client.PostAsJsonAsync(
                "/api/incidents",
                request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            var createdIncident =
                await postResponse.Content.ReadFromJsonAsync<IncidentResponse>();

            Assert.NotNull(createdIncident);
            Assert.NotEqual(Guid.Empty, createdIncident.Id);
            Assert.Equal("Unable to sign in", createdIncident.Title);
            Assert.Equal(IncidentPriority.High, createdIncident.Priority);
            Assert.Equal(IncidentStatus.New, createdIncident.Status);
            Assert.False(createdIncident.NotificationSent);

            var getResponse = await client.GetAsync(
                $"/api/incidents/{createdIncident.Id}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var retrievedIncident =
                await getResponse.Content.ReadFromJsonAsync<IncidentResponse>();

            Assert.NotNull(retrievedIncident);
            Assert.Equal(createdIncident.Id, retrievedIncident.Id);
            Assert.Equal("Unable to sign in", retrievedIncident.Title);
            Assert.Equal(IncidentPriority.High, retrievedIncident.Priority);
        }
        finally
        {
            await StopApiProcessAsync(apiProcess);
            await DeleteDirectoryWithRetryAsync(temporaryDirectory);
        }
    }

    private static async Task WaitForApiToStartAsync(
        HttpClient client,
        Process apiProcess)
    {
        var timeout = DateTime.UtcNow.AddSeconds(20);

        while (DateTime.UtcNow < timeout)
        {
            if (apiProcess.HasExited)
            {
                throw new InvalidOperationException(
                    $"Incident API avslutades vid uppstart med exit code {apiProcess.ExitCode}.");
            }

            try
            {
                using var response = await client.GetAsync("/api/incidents");

                if (response.IsSuccessStatusCode)
                    return;
            }
            catch (HttpRequestException)
            {
                // API:t har inte hunnit börja lyssna ännu.
            }

            await Task.Delay(100);
        }

        throw new TimeoutException("Incident API startade inte inom 20 sekunder.");
    }

    private static async Task StopApiProcessAsync(Process apiProcess)
    {
        try
        {
            if (!apiProcess.HasExited)
                apiProcess.Kill(entireProcessTree: true);

            await apiProcess.WaitForExitAsync();
        }
        catch (InvalidOperationException) when (apiProcess.HasExited)
        {
            // Processen avslutades mellan kontrollen och Kill-anropet.
        }
    }

    private static async Task DeleteDirectoryWithRetryAsync(string path)
    {
        for (var attempt = 0; attempt < 20; attempt++)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, recursive: true);

                return;
            }
            catch (IOException) when (attempt < 19)
            {
                await Task.Delay(250);
            }
            catch (UnauthorizedAccessException) when (attempt < 19)
            {
                await Task.Delay(250);
            }
        }
    }

    private static int GetAvailablePort()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);

        listener.Start();

        return ((IPEndPoint)listener.LocalEndpoint).Port;
    }

    private static string FindApiProjectPath()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var projectPath = Path.Combine(
                directory.FullName,
                "IncidentManagement.Api",
                "IncidentManagement.Api.csproj");

            if (File.Exists(projectPath))
                return projectPath;

            directory = directory.Parent;
        }

        throw new FileNotFoundException(
            "Kunde inte hitta IncidentManagement.Api.csproj. " +
            "Kontrollera att API-projektet ligger bredvid testprojektets mapp.");
    }
}