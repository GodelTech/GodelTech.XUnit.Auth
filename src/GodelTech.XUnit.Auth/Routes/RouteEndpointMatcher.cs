using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace GodelTech.XUnit.Auth.Routes
{
    internal class RouteEndpointMatcher : IRouteEndpointMatcher
    {
        public bool Match(RouteEndpoint endpoint, RouteBase route)
        {
            if (endpoint?.RoutePattern.RawText == null) return false;
            if (route == null) return false;

            return new TemplateMatcher(
                       TemplateParser.Parse(endpoint.RoutePattern.RawText),
                       new RouteValueDictionary(endpoint.RoutePattern.RequiredValues)
                   ).TryMatch(route.RoutePattern, new RouteValueDictionary(route.RouteValues))
                   &&
                   (
                       endpoint.Metadata.OfType<HttpMethodMetadata>().Any(y => y.HttpMethods.Contains(route.HttpMethod.Method))
                       || !endpoint.Metadata.OfType<HttpMethodMetadata>().Any()
                   );
        }
    }
}
