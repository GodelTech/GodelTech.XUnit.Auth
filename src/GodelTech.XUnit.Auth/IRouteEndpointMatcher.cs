using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace GodelTech.XUnit.Auth
{
    /// <summary>
    /// Interface for <see cref="RouteEndpoint"/> matching.
    /// </summary>
    public interface IRouteEndpointMatcher
    {
        /// <summary>
        /// Evaluates if the provided <paramref name="endpoint"/> matches the <paramref name="route"/>. Populates
        /// </summary>
        /// <param name="endpoint">The <see cref="RouteEndpoint"/>.</param>
        /// <param name="route">A <see cref="Routes.RouteBase"/> representing the route to match.</param>
        /// <returns><see langword="true"/> if <paramref name="endpoint"/> matches <see cref="Routes.RouteBase"/>.</returns>
        bool Match(RouteEndpoint endpoint, Routes.RouteBase route);

        /// <summary>
        /// Finds matching routes to current endpoint.
        /// </summary>
        /// <param name="endpoint">The route endpoint.</param>
        /// <param name="routes">The list of routes.</param>
        /// <returns>The list of matching routes.</returns>
        IList<Routes.RouteBase> FindMatchingRoutes(
            RouteEndpoint endpoint,
            IList<Routes.RouteBase> routes);

        /// <summary>
        /// Finds matching endpoints to current route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="endpoints">The list of route endpoints.</param>
        /// <returns>The list of matching route endpoints.</returns>
        IList<RouteEndpoint> FindMatchingEndpoints(
            Routes.RouteBase route,
            IList<RouteEndpoint> endpoints);
    }
}
