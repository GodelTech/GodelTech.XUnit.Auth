using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using GodelTech.XUnit.Auth.Tests.Fakes;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.Template;
using Moq;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests.Routes;

public class RouteEndpointExtensionsTests
{
    [Fact]
    public void GetUri_WhenRouteEndpointIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mockTemplateBinderFactory = new Mock<TemplateBinderFactory>(MockBehavior.Strict);

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => RouteEndpointExtensions.GetUri(null, mockTemplateBinderFactory.Object, new RouteValueDictionary())
        );

        Assert.Equal("endpoint", exception.ParamName);
    }

    [Fact]
    public void GetUri_WhenTemplateBinderFactoryIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var routeEndpoint = new RouteEndpoint(
            _ => Task.CompletedTask,
            RoutePatternFactory.Pattern(new List<RoutePatternPathSegment>()),
            0,
            null,
            null
        );

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => routeEndpoint.GetUri(null, new RouteValueDictionary())
        );

        Assert.Equal("factory", exception.ParamName);
    }

    [Fact]
    public void FindMatchingRoutes_WhenRouteEndpointIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => RouteEndpointExtensions.FindMatchingRoutes(null, new List<Auth.Routes.RouteBase>())
        );

        Assert.Equal("endpoint", exception.ParamName);
    }

    [Fact]
    public void FindMatchingEndpoints_WhenRoutePatternsNotEqual_ReturnsEmpty()
    {
        // Arrange
        var route = new AuthorizedRoute("/testRoute", HttpMethod.Get);

        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("/otherTestRoute")
        };

        // Act
        var result = route.FindMatchingEndpoints(endpoints);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindMatchingEndpoints_WhenHasHttpMethodMetadata_ReturnsOne()
    {
        // Arrange
        var route = new AuthorizedRoute("", HttpMethod.Get);

        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("", HttpMethod.Get, HttpMethod.Post)
        };

        // Act
        var result = route.FindMatchingEndpoints(endpoints);

        // Assert
        var endpoint = Assert.Single(result);
        Assert.Equal(endpoints[0], endpoint);
    }

    [Fact]
    public void FindMatchingEndpoints_WhenEmptyHttpMethodMetadata_ReturnsOne()
    {
        // Arrange
        var route = new AuthorizedRoute("", HttpMethod.Get);

        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("")
        };

        // Act
        var result = route.FindMatchingEndpoints(endpoints);

        // Assert
        var endpoint = Assert.Single(result);
        Assert.Equal(endpoints[0], endpoint);
    }

    [Fact]
    public void FindMatchingEndpoints_WhenHttpMethodMetadataNotEqual_ReturnsEmpty()
    {
        // Arrange
        var route = new AuthorizedRoute("", HttpMethod.Get);

        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("", HttpMethod.Post)
        };

        // Act
        var result = route.FindMatchingEndpoints(endpoints);

        // Assert
        Assert.Empty(result);
    }
}
