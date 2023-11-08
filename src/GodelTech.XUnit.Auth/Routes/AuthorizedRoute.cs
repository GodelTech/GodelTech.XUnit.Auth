using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;

namespace GodelTech.XUnit.Auth.Routes
{
    /// <summary>
    /// Authorized route.
    /// </summary>
    public class AuthorizedRoute : RouteBase
    {
        /// <summary>
        /// Creates a new instance of the AuthorizedRoute.
        /// </summary>
        /// <param name="routePattern">The route pattern.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="routeValues">The route values.</param>
        public AuthorizedRoute(
            string routePattern,
            HttpMethod httpMethod,
            RouteValueDictionary routeValues = null)
            : base(
                routePattern,
                httpMethod,
                routeValues,
                HttpStatusCode.Unauthorized)
        {

        }
    }
}
