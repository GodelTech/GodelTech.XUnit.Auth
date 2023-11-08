using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;

namespace GodelTech.XUnit.Auth.Routes
{
    /// <summary>
    /// Route base.
    /// </summary>
    public abstract class RouteBase
    {
        /// <summary>
        /// Creates a new instance of the RouteBase.
        /// </summary>
        /// <param name="routePattern">The route pattern.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="expectedStatusCode">Expected HTTP status code.</param>
        protected RouteBase(
            string routePattern,
            HttpMethod httpMethod,
            RouteValueDictionary routeValues,
            HttpStatusCode expectedStatusCode)
        {
            RoutePattern = routePattern;
            HttpMethod = httpMethod;
            RouteValues = routeValues ?? new RouteValueDictionary();
            ExpectedStatusCode = expectedStatusCode;
        }

        /// <summary>
        /// Route pattern.
        /// </summary>
        public string RoutePattern { get; }

        /// <summary>
        /// HTTP method.
        /// </summary>
        public HttpMethod HttpMethod { get; }

        /// <summary>
        /// Route values.
        /// </summary>
        public RouteValueDictionary RouteValues { get; }

        /// <summary>
        /// Expected HTTP status code.
        /// </summary>
        public HttpStatusCode ExpectedStatusCode { get; }
    }
}
