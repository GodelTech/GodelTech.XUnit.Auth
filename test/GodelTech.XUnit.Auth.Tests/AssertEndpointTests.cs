using System;
using System.Collections.Generic;
using GodelTech.XUnit.Auth.Routes;
using Xunit;
using GodelTech.XUnit.Auth.Tests.Fakes;

namespace GodelTech.XUnit.Auth.Tests;

public class AssertEndpointTests
{
    [Fact]
    public void Check_WhenWebApplicationFactoryIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => AssertEndpoint.Check<FakeEntryPoint>(null, new List<RouteBase>())
        );

        Assert.Equal("factory", exception.ParamName);
    }
}
