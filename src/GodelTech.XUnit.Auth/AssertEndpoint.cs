using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using GodelTech.XUnit.Auth.Routes;
using Xunit;

namespace GodelTech.XUnit.Auth
{
    /// <summary>
    /// Assert endpoint.
    /// </summary>
    public static class AssertEndpoint
    {
        /// <summary>
        /// Check endpoints.
        /// </summary>
        /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.
        /// Typically the Startup or Program classes can be used.</typeparam>
        /// <param name="factory">Factory for bootstrapping an application in memory for functional end to end tests.</param>
        /// <returns>The list of route endpoints.</returns>
        /// <param name="routes">The list of routes.</param>
        public static void Check<TEntryPoint>(
            WebApplicationFactory<TEntryPoint> factory,
            IList<RouteBase> routes)
            where TEntryPoint : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            // Arrange
            var endpoints = factory.GetEndpoints();

            // Act & Assert
            foreach (var endpoint in endpoints)
            {
                var matchingRoutes = endpoint.FindMatchingRoutes(routes);

                Assert.True(matchingRoutes.Any(), $"No matching route found for '{endpoint.RoutePattern.RawText}'");
                Assert.True(matchingRoutes.Count == 1, $"More than one matching route found for '{endpoint.RoutePattern.RawText}'");
            }
        }
    }
}
