using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
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
        /// <param name="route">The route.</param>
        /// <param name="endpoints">The list of endpoints.</param>
        /// <param name="templateBinderFactory">The <see cref="TemplateBinderFactory"/>.</param>
        /// <param name="client">The HttpClient.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns>The matching endpoint.</returns>
        public static async Task<RouteEndpoint> CheckAsync(
            Routes.RouteBase route,
            IList<RouteEndpoint> endpoints,
            TemplateBinderFactory templateBinderFactory,
            HttpClient client,
            IRouteEndpointMatcher matcher = default(RouteEndpointMatcher))
        {
            ArgumentNullException.ThrowIfNull(route);
            ArgumentNullException.ThrowIfNull(endpoints);
            ArgumentNullException.ThrowIfNull(client);
            if (matcher == null) matcher = new RouteEndpointMatcher();

            // Arrange
            var matchingEndpoint = Assert.Single(matcher.FindMatchingEndpoints(route, endpoints));

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
