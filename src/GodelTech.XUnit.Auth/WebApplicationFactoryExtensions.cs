using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;

namespace GodelTech.XUnit.Auth
{
    /// <summary>
    /// WebApplicationFactory extensions.
    /// </summary>
    public static class WebApplicationFactoryExtensions
    {
        /// <summary>
        /// Get route endpoints.
        /// </summary>
        /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.
        /// Typically the Startup or Program classes can be used.</typeparam>
        /// <param name="factory">Factory for bootstrapping an application in memory for functional end to end tests.</param>
        /// <returns>The list of route endpoints.</returns>
        public static IList<RouteEndpoint> GetEndpoints<TEntryPoint>(this WebApplicationFactory<TEntryPoint> factory)
            where TEntryPoint : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            return factory
                .Services
                .GetRequiredService<EndpointDataSource>()
                .Endpoints.OfType<RouteEndpoint>()
                .ToList();
        }

        /// <summary>
        /// Get TemplateBinder factory.
        /// </summary>
        /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.
        /// Typically the Startup or Program classes can be used.</typeparam>
        /// <param name="factory">Factory for bootstrapping an application in memory for functional end to end tests.</param>
        /// <returns>The <see cref="TemplateBinderFactory"/>.</returns>
        public static TemplateBinderFactory GeTemplateBinderFactory<TEntryPoint>(this WebApplicationFactory<TEntryPoint> factory)
            where TEntryPoint : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            return factory.Services.GetRequiredService<TemplateBinderFactory>();
        }
    }
}
