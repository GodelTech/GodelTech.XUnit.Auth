using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GodelTech.XUnit.Auth.Routes
{
    /// <summary>
    /// RouteEndpoint extensions.
    /// </summary>
    public static class RouteEndpointExtensions
    {
        /// <summary>
        /// Get Uri.
        /// </summary>
        /// <param name="endpoint">The route endpoint.</param>
        /// <param name="factory">The template binder factory.</param>
        /// <param name="routeValues">The route value dictionary.</param>
        /// <returns>The Uri.</returns>
        /// <exception cref="ArgumentNullException">Throws exception when TemplateBinderFactory is null.</exception>
        public static Uri GetUri(
            this RouteEndpoint endpoint,
            TemplateBinderFactory factory,
            RouteValueDictionary routeValues)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            var templateBinder = factory.Create(endpoint.RoutePattern);

            var url = templateBinder.BindValues(routeValues);

            return new Uri(url, UriKind.Relative);
        }

        /// <summary>
        /// Finds matching routes to current endpoint.
        /// </summary>
        /// <param name="endpoint">The route endpoint.</param>
        /// <param name="routes">The list of routes.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns>The list of matching routes.</returns>
        public static IList<RouteBase> FindMatchingRoutes(
            this RouteEndpoint endpoint,
            IList<RouteBase> routes,
            IRouteEndpointMatcher matcher)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

            return routes
                .Where(x => matcher.Match(endpoint, x))
                .ToList();
        }
    }
}
