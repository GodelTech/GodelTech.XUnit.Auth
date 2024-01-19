using System.Diagnostics.CodeAnalysis;
using GodelTech.XUnit.Auth.Demo.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace GodelTech.XUnit.Auth.IntegrationTests;

public class AppTestFixture : WebApplicationFactory<Startup>
{
    public ITestOutputHelper Output { get; set; }

    protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
    {
        builder
            .UseSetting("https_port", "8080")
            .ConfigureTestJwtToken();
    }
}
