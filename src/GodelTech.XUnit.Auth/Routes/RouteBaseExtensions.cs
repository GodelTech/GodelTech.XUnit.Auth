using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing.Template;

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
        /// <returns>The list of matching route endpoints.</returns>
        public static IList<RouteEndpoint> FindMatchingEndpoints(this RouteBase route, IList<RouteEndpoint> endpoints)
        {
            return endpoints
                .Where(
                    x =>
                        new TemplateMatcher(
                            TemplateParser.Parse(x.RoutePattern.RawText),
                            new RouteValueDictionary(x.RoutePattern.RequiredValues)
                        ).TryMatch(route.RoutePattern, new RouteValueDictionary(route.RouteValues))
                        &&
                        (
                            x.Metadata.OfType<HttpMethodMetadata>().Any(y => y.HttpMethods.Contains(route.HttpMethod.Method))
                            || !x.Metadata.OfType<HttpMethodMetadata>().Any()
                        )
                )
                .ToList();
        }
    }
}
