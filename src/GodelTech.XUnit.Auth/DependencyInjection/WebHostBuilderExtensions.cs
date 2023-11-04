using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Xunit.Abstractions;

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
    /// <param name="output">The <see cref="ITestOutputHelper" />.</param>
    /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
    public static IWebHostBuilder ConfigureTestJwtToken(
        [NotNull] this IWebHostBuilder builder,
        ITestOutputHelper output = null)
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
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                output?.WriteLine(context.Exception.Message);

                                return Task.CompletedTask;
                            }
                        };
                    }
                );
            }
        );

        return builder;
    }
}
