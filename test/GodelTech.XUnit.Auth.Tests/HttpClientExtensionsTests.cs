using System;
using System.Net.Http;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests;

public class HttpClientExtensionsTests
{
    [Fact]
    public void WithJwtBearerToken_WhenHttpClientIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => HttpClientExtensions.WithJwtBearerToken(null)
        );

        Assert.Equal("client", exception.ParamName);
    }

    [Fact]
    public void WithJwtBearerToken_Success()
    {
        // Arrange
        using var client = new HttpClient();

        // Act
        var result = client.WithJwtBearerToken();

        // Assert
        Assert.Equal(new Uri("https://localhost:8080"), client.BaseAddress);

        Assert.NotNull(client.DefaultRequestHeaders.Authorization);
        Assert.Equal("Bearer", client.DefaultRequestHeaders.Authorization.Scheme);
        Assert.True(!string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.Authorization.Parameter));

        Assert.Equal(client, result);
    }

    [Fact]
    public void WithJwtBearerToken_WithConfiguration_Success()
    {
        // Arrange
        var wasInvoked = false;

        using var client = new HttpClient();

        // Act
        var result = client.WithJwtBearerToken(
            _ =>
            {
                wasInvoked = true;
            },
            "https://test.dev"
        );

        // Assert
        Assert.True(wasInvoked);

        Assert.Equal(new Uri("https://test.dev"), client.BaseAddress);

        Assert.NotNull(client.DefaultRequestHeaders.Authorization);
        Assert.Equal("Bearer", client.DefaultRequestHeaders.Authorization.Scheme);
        Assert.True(!string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.Authorization.Parameter));

        Assert.Equal(client, result);
    }
}
