using System.Net.Http;
using GodelTech.XUnit.Auth.Routes;
using GodelTech.XUnit.Auth.Tests.Fakes;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests.Routes;

public class RouteEndpointMatcherTests
{
    private readonly RouteEndpointMatcher _matcher;

    public RouteEndpointMatcherTests()
    {
        _matcher = new RouteEndpointMatcher();
    }

    [Fact]
    public void Match_WhenEndpointIsNull_ReturnsFalse()
    {
        // Arrange
        var route = new AuthorizedRoute("", HttpMethod.Get);

        // Act
        var result = _matcher.Match(null, route);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Match_WhenEndpointRoutePatternTextIsNull_ReturnsFalse()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create(null);

        var route = new AuthorizedRoute("", HttpMethod.Get);

        // Act
        var result = _matcher.Match(endpoint, route);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Match_WhenRouteIsNull_ReturnsFalse()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create("");

        // Act
        var result = _matcher.Match(endpoint, null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Match_WhenRoutePatternsNotEqual_ReturnsFalse()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create("/testRoute", HttpMethod.Get);

        var route = new AuthorizedRoute("/otherTestRoute", HttpMethod.Get);

        // Act
        var result = _matcher.Match(endpoint, route);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Match_WhenHttpMethodMetadataNotEqual_ReturnsFalse()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create("", HttpMethod.Get);

        var route = new AuthorizedRoute("", HttpMethod.Post);

        // Act
        var result = _matcher.Match(endpoint, route);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Match_WhenEqual_ReturnsTrue()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create("", HttpMethod.Get);

        var route = new AuthorizedRoute("", HttpMethod.Get);

        // Act
        var result = _matcher.Match(endpoint, route);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Match_WhenHasHttpMethodMetadata_ReturnsOne()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create("", HttpMethod.Get, HttpMethod.Post);

        var route = new AuthorizedRoute("", HttpMethod.Get);

        // Act
        var result = _matcher.Match(endpoint, route);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Match_WhenEndpointHasEmptyHttpMethodMetadata_ReturnsTrue()
    {
        // Arrange
        var endpoint = RouteEndpointHelpers.Create("");

        var route = new AuthorizedRoute("", HttpMethod.Get);

        // Act
        var result = _matcher.Match(endpoint, route);

        // Assert
        Assert.True(result);
    }
}
