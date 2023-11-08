using Microsoft.AspNetCore.Routing;

namespace GodelTech.XUnit.Auth.Routes
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
        /// <param name="route">A <see cref="RouteBase"/> representing the route to match.</param>
        /// <returns><see langword="true"/> if <paramref name="endpoint"/> matches <see cref="RouteBase"/>.</returns>
        bool Match(RouteEndpoint endpoint, RouteBase route);
    }
}
