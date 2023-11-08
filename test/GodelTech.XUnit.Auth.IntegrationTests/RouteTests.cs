using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Xunit;
using Xunit.Abstractions;

namespace GodelTech.XUnit.Auth.IntegrationTests;

public sealed class RouteTests : IDisposable
{
    private readonly AppTestFixture _fixture;
    private readonly HttpClient _client;

    public RouteTests(ITestOutputHelper output)
    {
        _fixture = new AppTestFixture
        {
            Output = output
        };

        _client = _fixture.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost:8080"),
                AllowAutoRedirect = false
            }
        );
    }

    public void Dispose()
    {
        _client.Dispose();
        _fixture.Dispose();
    }

    private static readonly IList<Routes.RouteBase> Routes = new List<Routes.RouteBase>
    {
        // fakes
        new AuthorizedRoute("/fakes", HttpMethod.Get),
        new AuthorizedRoute(
            "/fakes/{id}",
            HttpMethod.Get,
            new RouteValueDictionary
            {
                { "id", 1 }
            }
        ),
        new AuthorizedRoute("/fakes", HttpMethod.Post),
        new AuthorizedRoute(
            "/fakes/{id}",
            HttpMethod.Put,
            new RouteValueDictionary
            {
                { "id", 1 }
            }
        ),
        new AuthorizedRoute(
            "/fakes/{id}",
            HttpMethod.Delete,
            new RouteValueDictionary
            {
                { "id", 1 }
            }
        ),
        // openFakes
        new AllowAnonymousRoute(
            "/openFakes",
            HttpMethod.Get
        )
    };

    public static IEnumerable<object[]> CheckRoutesMemberData => Routes
        .Select(
            route => new object[]
            {
                route
            }
        );

    [Theory]
    [MemberData(nameof(CheckRoutesMemberData))]
    public async Task CheckRoutes(Routes.RouteBase route)
    {
        // Arrange & Act & Assert
        await AssertRoute.CheckAsync(
            route,
            _fixture.GetEndpoints(),
            _fixture.GeTemplateBinderFactory(),
            _client
        );
    }

    [Fact]
    public void CheckEndpoints()
    {
        // Arrange & Act & Assert
        AssertEndpoint.Check(
            _fixture.GetEndpoints(),
            Routes
        );
    }
}
