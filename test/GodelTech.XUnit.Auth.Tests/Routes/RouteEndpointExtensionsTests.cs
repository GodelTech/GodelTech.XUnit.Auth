using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.Template;
using Moq;
using Xunit;
using RouteBase = GodelTech.XUnit.Auth.Routes.RouteBase;

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
            () => RouteEndpointExtensions.FindMatchingRoutes(null, new List<RouteBase>())
        );

        Assert.Equal("endpoint", exception.ParamName);
    }
}
