using System;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GodelTech.XUnit.Auth
{
    /// <summary>
    /// Assert route.
    /// </summary>
    public static class AssertRoute
    {
        /// <summary>
        /// Check route.
        /// </summary>
        /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.
        /// Typically the Startup or Program classes can be used.</typeparam>
        /// <param name="factory">Factory for bootstrapping an application in memory for functional end to end tests.</param>
        /// <returns>The list of route endpoints.</returns>
        /// <param name="client">The HttpClient.</param>
        /// <param name="route">The route.</param>
        /// <returns>The matching endpoint.</returns>
        public static async Task<RouteEndpoint> CheckAsync<TEntryPoint>(
            WebApplicationFactory<TEntryPoint> factory,
            HttpClient client,
            Routes.RouteBase route)
            where TEntryPoint : class
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (route == null) throw new ArgumentNullException(nameof(route));

            // Arrange
            var endpoints = factory.GetEndpoints();

            var matchingEndpoint = Assert.Single(route.FindMatchingEndpoints(endpoints));

            var templateBinderFactory = factory.Services.GetRequiredService<TemplateBinderFactory>();

            var uri = matchingEndpoint.GetUri(templateBinderFactory, route.RouteValues);

            // Act
            using var httpRequestMessage = new HttpRequestMessage(route.HttpMethod, uri);

            var result = await client.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(route.ExpectedStatusCode, result.StatusCode);

            return matchingEndpoint;
        }
    }
}
