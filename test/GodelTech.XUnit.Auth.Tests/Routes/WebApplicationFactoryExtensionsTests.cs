using System;
using GodelTech.XUnit.Auth.Routes;
using GodelTech.XUnit.Auth.Tests.Fakes;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests.Routes;

public class WebApplicationFactoryExtensionsTests
{
    [Fact]
    public void GetEndpoints_WhenWebApplicationFactoryIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => WebApplicationFactoryExtensions.GetEndpoints<FakeEntryPoint>(null)
        );

        Assert.Equal("factory", exception.ParamName);
    }
}
