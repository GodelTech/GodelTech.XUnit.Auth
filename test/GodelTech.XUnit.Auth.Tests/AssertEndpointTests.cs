using System;
using System.Collections.Generic;
using System.Net.Http;
using GodelTech.XUnit.Auth.Routes;
using GodelTech.XUnit.Auth.Tests.Fakes;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace GodelTech.XUnit.Auth.Tests;

public class AssertEndpointTests
{
    private readonly Mock<IRouteEndpointMatcher> _matcher;

    public AssertEndpointTests()
    {
        _matcher = new Mock<IRouteEndpointMatcher>(MockBehavior.Strict);
    }

    [Fact]
    public void Check_WhenEndpointsAreNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => AssertEndpoint.Check(
                null,
                new List<Auth.Routes.RouteBase>(),
                new RouteEndpointMatcher()
            )
        );

        Assert.Equal("endpoints", exception.ParamName);
    }

    [Fact]
    public void Check_WhenRoutesAreNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => AssertEndpoint.Check(
                new List<RouteEndpoint>(),
                null,
                new RouteEndpointMatcher()
            )
        );

        Assert.Equal("routes", exception.ParamName);
    }

    [Fact]
    public void Check_WhenNoMatchingRoutes_ThrowsTrueException()
    {
        // Arrange
        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("/test", HttpMethod.Get)
        };

        var routes = new List<Auth.Routes.RouteBase>();

        _matcher
            .Setup(x => x.FindMatchingRoutes(endpoints[0], routes))
            .Returns(new List<Auth.Routes.RouteBase>());

        // Act & Assert
        var exception = Assert.Throws<TrueException>(
            () => AssertEndpoint.Check(
                endpoints,
                routes,
                _matcher.Object
            )
        );
        Assert.Equal(
            $"No matching route found for '{endpoints[0].RoutePattern.RawText}'",
            exception.Message
        );
    }

    [Fact]
    public void Check_WhenMoreThanOneMatchingRoutes_ThrowsTrueException()
    {
        // Arrange
        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("/test", HttpMethod.Get)
        };

        var routes = new List<Auth.Routes.RouteBase>();

        _matcher
            .Setup(x => x.FindMatchingRoutes(endpoints[0], routes))
            .Returns(
                new List<Auth.Routes.RouteBase>
                {
                    new AuthorizedRoute("/test", HttpMethod.Get),
                    new AuthorizedRoute("/test", HttpMethod.Post)
                }
            );

        // Act & Assert
        var exception = Assert.Throws<TrueException>(
            () => AssertEndpoint.Check(
                endpoints,
                routes,
                _matcher.Object
            )
        );
        Assert.Equal(
            $"More than one matching route found for '{endpoints[0].RoutePattern.RawText}'",
            exception.Message
        );
    }

    [Fact]
    public void Check_Success()
    {
        // Arrange
        var endpoints = new List<RouteEndpoint>
        {
            RouteEndpointHelpers.Create("/first", HttpMethod.Get),
            RouteEndpointHelpers.Create("/second", HttpMethod.Post)
        };

        var routes = new List<Auth.Routes.RouteBase>
        {
            new AuthorizedRoute("/first", HttpMethod.Get),
            new AuthorizedRoute("/second", HttpMethod.Post)
        };

        _matcher
            .SetupSequence(x => x.FindMatchingRoutes(It.IsIn(endpoints.ToArray()), routes))
            .Returns(
                new List<Auth.Routes.RouteBase>
                {
                    routes[0]
                }
            )
            .Returns(
                new List<Auth.Routes.RouteBase>
                {
                    routes[1]
                }
            );

        // Act
        AssertEndpoint.Check(
            endpoints,
            routes,
            _matcher.Object
        );

        // Assert
        _matcher
            .Verify(
                x => x.FindMatchingRoutes(It.IsIn(endpoints.ToArray()), routes),
                Times.Exactly(2)
            );
    }
}
