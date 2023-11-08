# GodelTech.XUnit.Auth

## Overview

`GodelTech.XUnit.Auth` contains helpers to assert routes in project.

## Example of usage Test JSON Web Token

`AppTestFixture.cs`
```c#
    public class AppTestFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder
                .UseSetting("https_port", "8080")
                .ConfigureTestJwtToken(); // Configures Test JSON Web Token
        }
    }
```

`ControllerTests.cs`
```c#
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
                .WithJwtBearerToken() // Adds Test JSON Web Token to HttpClient
                .GetAsync(new Uri("/fakes", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var resultValue = await result.Content.ReadFromJsonAsync<IList<FakeModel>>();
            Assert.NotNull(resultValue);
            Assert.Equal(2, resultValue.Count);
            resultValue.Should().BeEquivalentTo(expectedResult);
        }
    }
```

You can check example of usage in Integration Tests for this library here: https://github.com/GodelTech/GodelTech.XUnit.Auth/tree/main/test/GodelTech.XUnit.Auth.IntegrationTests.

## Exmaple of usage RouteTests

```c#
    public sealed class RouteTests : IDisposable
    {
        private readonly AppTestFixture _fixture;
        private readonly HttpClient _client;

        public RouteTests(ITestOutputHelper output)
        {
            _fixture = new AppTestFixture
            {
                Output = output
            };

            _client = _fixture.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("https://localhost:8080"),
                    AllowAutoRedirect = false
                }
            );
        }

        public void Dispose()
        {
            _client.Dispose();
            _fixture.Dispose();
        }

        private static readonly IList<Routes.RouteBase> Routes = new List<Routes.RouteBase>
        {
            // fakes
            new AuthorizedRoute("/fakes", HttpMethod.Get),
            new AuthorizedRoute(
                "/fakes/{id}",
                HttpMethod.Get,
                new RouteValueDictionary
                {
                    { "id", 1 }
                }
            ),
            new AuthorizedRoute("/fakes", HttpMethod.Post),
            new AuthorizedRoute(
                "/fakes/{id}",
                HttpMethod.Put,
                new RouteValueDictionary
                {
                    { "id", 1 }
                }
            ),
            new AuthorizedRoute(
                "/fakes/{id}",
                HttpMethod.Delete,
                new RouteValueDictionary
                {
                    { "id", 1 }
                }
            ),
            // openFakes
            new AllowAnonymousRoute(
                "/openFakes",
                HttpMethod.Get
            )
        };

        public static IEnumerable<object[]> CheckRoutesMemberData => Routes
            .Select(
                route => new object[]
                {
                    route
                }
            );

        [Theory]
        [MemberData(nameof(CheckRoutesMemberData))]
        public async Task CheckRoutes(Routes.RouteBase route)
        {
            // Arrange & Act & Assert
            await AssertRoute.CheckAsync(
                route,
                _fixture.GetEndpoints(),
                _fixture.GeTemplateBinderFactory(),
                _client
            );
        }

        [Fact]
        public void CheckEndpoints()
        {
            // Arrange & Act & Assert
            AssertEndpoint.Check(
                _fixture.GetEndpoints(),
                Routes
            );
        }
    }
```

You can check example of usage in Integration Tests for this library here: https://github.com/GodelTech/GodelTech.XUnit.Auth/tree/main/test/GodelTech.XUnit.Auth.IntegrationTests.