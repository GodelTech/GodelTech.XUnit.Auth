using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace GodelTech.XUnit.Auth.Routes
{
    /// <summary>
    /// Route base extensions.
    /// </summary>
    public static class RouteBaseExtensions
    {
        /// <summary>
        /// Finds matching endpoints to current route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="endpoints">The list of route endpoints.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns>The list of matching route endpoints.</returns>
        public static IList<RouteEndpoint> FindMatchingEndpoints(
            this RouteBase route,
            IList<RouteEndpoint> endpoints,
            IRouteEndpointMatcher matcher)
        {
            return endpoints
                .Where(x => matcher.Match(x, route))
                .ToList();
        }
    }
}
