using System.Threading.Tasks;
using GodelTech.XUnit.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Xunit.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// WebHostBuilder extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures the JSON Web Token for tests.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" />.</param>
    /// <param name="output">The <see cref="ITestOutputHelper" />.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureTestJwtToken(
        this IServiceCollection services,
        ITestOutputHelper output = null)
    {
        services.PostConfigure<JwtBearerOptions>(
            JwtBearerDefaults.AuthenticationScheme,
            options =>
            {
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

        return services;
    }
}
