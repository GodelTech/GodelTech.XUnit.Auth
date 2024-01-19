using System;
using System.Collections.Generic;
using System.Linq;
using GodelTech.XUnit.Auth.Routes;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace GodelTech.XUnit.Auth
{
    /// <summary>
    /// Assert endpoint.
    /// </summary>
    public static class AssertEndpoint
    {
        /// <summary>
        /// Check endpoints.
        /// </summary>
        /// <param name="endpoints">The list of endpoints.</param>
        /// <param name="routes">The list of routes.</param>
        /// <param name="matcher">The matcher.</param>
        public static void Check(
            IList<RouteEndpoint> endpoints,
            IList<Routes.RouteBase> routes,
            IRouteEndpointMatcher matcher = default(RouteEndpointMatcher))
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            ArgumentNullException.ThrowIfNull(routes);
            if (matcher == null) matcher = new RouteEndpointMatcher();

            // Arrange & Act & Assert
            foreach (var endpoint in endpoints)
            {
                var matchingRoutes = matcher.FindMatchingRoutes(endpoint, routes);

                Assert.True(matchingRoutes.Any(), $"No matching route found for '{endpoint.RoutePattern.RawText}'");
                Assert.True(matchingRoutes.Count == 1, $"More than one matching route found for '{endpoint.RoutePattern.RawText}'");
            }
        }
    }
}
