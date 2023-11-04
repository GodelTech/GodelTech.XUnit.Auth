using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using GodelTech.XUnit.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// WebHostBuilder extensions.
/// </summary>
public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Configures the JSON Web Token for tests.
    /// </summary>
    /// <param name="builder">The <see cref="IWebHostBuilder" />.</param>
    /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
    public static IWebHostBuilder ConfigureTestJwtToken([NotNull] this IWebHostBuilder builder)
    {
        builder.ConfigureServices(
            services =>
            {
                services.PostConfigure<JwtBearerOptions>(
                    JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                            TestJwtTokenProvider.MetadataAddress,
                            new OpenIdConnectConfigurationRetriever(),
                            new HttpDocumentRetriever(
                                new HttpClient(
                                    TestJwtTokenProvider.Backchannel
                                )
                            )
                        );
                        options.TokenValidationParameters.ValidIssuer = TestJwtTokenProvider.Issuer;
                        options.TokenValidationParameters.ValidAudience = TestJwtTokenProvider.Audience;
                        options.TokenValidationParameters.IssuerSigningKey = TestJwtTokenProvider.SecurityKey;
                    }
                );
            }
        );

        return builder;
    }
}
