using GodelTech.XUnit.Auth.Demo.Api;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodelTech.XUnit.Auth.IntegrationTests;

public class AppTestFixture : WebApplicationFactory<Startup>
{
    public ITestOutputHelper Output { get; set; }

    protected override IHostBuilder CreateHostBuilder()
    {
        var builder = base.CreateHostBuilder();

        builder.ConfigureLogging(
            logging =>
            {
                logging.ClearProviders(); // Remove other loggers
                logging.AddXUnit(Output); // Use the ITestOutputHelper instance
            }
        );

        return builder;
    }

    protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
    {
        builder
            .UseSetting("https_port", "8080")
            .ConfigureTestJwtToken();
    }
}
