﻿using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit.Abstractions;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GodelTech.XUnit.Auth.Demo.Api.Controllers;
using GodelTech.XUnit.Auth.Demo.Api.Models.Fake;
using Xunit;

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
}