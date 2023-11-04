using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using GodelTech.XUnit.Auth.Demo.Api.Controllers;
using GodelTech.XUnit.Auth.Demo.Api.Models.Fake;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace GodelTech.XUnit.Auth.IntegrationTests.Controllers;

public sealed class FakeControllerTests : IDisposable
{
    private readonly AppTestFixture _fixture;
    private readonly HttpClient _client;

    public FakeControllerTests(ITestOutputHelper output)
    {
        _fixture = new AppTestFixture
        {
            Output = output
        };

        _client = _fixture.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost:8080")
            }
        );
    }

    public void Dispose()
    {
        _client.Dispose();
        _fixture.Dispose();
    }

    [Fact]
    public async Task GetListAsync_ReturnsOkWithList()
    {
        // Arrange
        var expectedResult = FakeController.Items;

        // Act
        var result = await _client
            .WithJwtBearerToken()
            .GetAsync(new Uri("/fakes", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var resultValue = await result.Content.ReadFromJsonAsync<IList<FakeModel>>();
        Assert.NotNull(resultValue);
        Assert.Equal(2, resultValue.Count);
        resultValue.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetListAsync_ReturnsUnauthorized()
    {
        // Arrange & Act
        var result = await _client
            .GetAsync(new Uri("/fakes", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task PostAsync_ReturnsCreated()
    {
        // Arrange
        var postModel = new FakePostModel
        {
            Title = "TestTitle"
        };

        var expectedResult = new FakeModel
        {
            Id = 2,
            Title = "TestTitle"
        };

        // Act
        var result = await _client
            .WithJwtBearerToken(token => token.WithScope("fake.add"))
            .PostAsJsonAsync(
                "/fakes",
                postModel
            );

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        Assert.Equal($"https://localhost:8080/fakes/{expectedResult.Id}", result.Headers.Location?.AbsoluteUri);

        var resultValue = await result.Content.ReadFromJsonAsync<FakeModel>();
        resultValue.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task PostAsync_Forbidden()
    {
        // Arrange
        var postModel = new FakePostModel
        {
            Title = "TestTitle"
        };

        // Act
        var result = await _client
            .WithJwtBearerToken(token => token.WithScope("invalid"))
            .PostAsJsonAsync(
                "/fakes",
                postModel
            );

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }
}
