using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Moq;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests;

public class AssertRouteTests
{
    private readonly Mock<TemplateBinderFactory> _mockTemplateBinderFactory;

    public AssertRouteTests()
    {
        _mockTemplateBinderFactory = new Mock<TemplateBinderFactory>(MockBehavior.Strict);
    }

    [Fact]
    public async Task CheckAsync_WhenRouteIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => AssertRoute.CheckAsync(
                null,
                new List<RouteEndpoint>(),
                _mockTemplateBinderFactory.Object,
                new HttpClient(),
                new RouteEndpointMatcher()
            )
        );

        Assert.Equal("route", exception.ParamName);
    }

    [Fact]
    public async Task CheckAsync_WhenEndpointsAreNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => AssertRoute.CheckAsync(
                new AuthorizedRoute("", HttpMethod.Get),
                null,
                _mockTemplateBinderFactory.Object,
                new HttpClient(),
                new RouteEndpointMatcher()
            )
        );

        Assert.Equal("endpoints", exception.ParamName);
    }

    [Fact]
    public async Task CheckAsync_WhenHttpClientIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => AssertRoute.CheckAsync(
                new AuthorizedRoute("", HttpMethod.Get),
                new List<RouteEndpoint>(),
                _mockTemplateBinderFactory.Object,
                null,
                new RouteEndpointMatcher()
            )
        );

        Assert.Equal("client", exception.ParamName);
    }
}
