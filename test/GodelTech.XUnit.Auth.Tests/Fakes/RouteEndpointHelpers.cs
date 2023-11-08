using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;

namespace GodelTech.XUnit.Auth.Tests.Fakes;

public static class RouteEndpointHelpers
{
    public static RouteEndpoint Create(string pattern, params HttpMethod[] httpMethods)
    {
        RoutePattern routePattern;

        if (pattern == null)
        {
            routePattern = RoutePatternFactory.Pattern(new List<RoutePatternPathSegment>());
        }
        else
        {
            routePattern = RoutePatternFactory.Parse(pattern);
        }

        HttpMethodMetadata httpMethodMetadata = null;

        if (httpMethods != null && httpMethods.Any())
        {
            httpMethodMetadata = new HttpMethodMetadata(
                httpMethods.Select(x => x.Method)
            );
        }

        var metadata = EndpointMetadataCollection.Empty;

        if (httpMethodMetadata != null)
        {
            metadata = new EndpointMetadataCollection(httpMethodMetadata);
        }

        return new RouteEndpoint(
            _ => Task.CompletedTask,
            routePattern,
            0,
            metadata,
            string.Empty
        );
    }
}
