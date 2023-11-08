using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;

namespace GodelTech.XUnit.Auth.Routes
{
    /// <summary>
    /// AllowAnonymous route.
    /// </summary>
    public class AllowAnonymousRoute : RouteBase
    {
        /// <summary>
        /// Creates a new instance of the AllowAnonymousRoute.
        /// </summary>
        /// <param name="routePattern">The route pattern.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="expectedStatusCode">Expected HTTP status code.</param>
        public AllowAnonymousRoute(
            string routePattern,
            HttpMethod httpMethod,
            RouteValueDictionary routeValues = null,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
            : base(
                routePattern,
                httpMethod,
                routeValues,
                expectedStatusCode)
        {

        }
    }
}
