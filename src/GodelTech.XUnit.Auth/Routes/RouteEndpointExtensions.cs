using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Routing;
using System;

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
    }
}
