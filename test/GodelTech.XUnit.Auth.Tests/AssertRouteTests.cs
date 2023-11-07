using System;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using GodelTech.XUnit.Auth.Tests.Fakes;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests;

public class AssertRouteTests
{
    [Fact]
    public async Task CheckAsync_WhenHttpClientIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => AssertRoute.CheckAsync(new WebApplicationFactory<FakeEntryPoint>(), null, new AuthorizedRoute("", HttpMethod.Get))
        );

        Assert.Equal("client", exception.ParamName);
    }

    [Fact]
    public async Task CheckAsync_WhenRouteIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => AssertRoute.CheckAsync(new WebApplicationFactory<FakeEntryPoint>(), new HttpClient(), null)
        );

        Assert.Equal("route", exception.ParamName);
    }
}
