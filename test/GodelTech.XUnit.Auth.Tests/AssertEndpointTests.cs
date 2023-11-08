using System;
using System.Collections.Generic;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests;

public class AssertEndpointTests
{
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
}
