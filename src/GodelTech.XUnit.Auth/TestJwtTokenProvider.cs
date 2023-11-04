using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;

namespace GodelTech.XUnit.Auth;

/// <summary>
/// JSON Web Token provider.
/// </summary>
public static class TestJwtTokenProvider
{
    /// <summary>
    /// Issuer.
    /// </summary>
    public static string Issuer { get; } = "TestIssuer";

    /// <summary>
    /// Audience.
    /// </summary>
    public static string Audience { get; } = "TestAudience";

    /// <summary>
    /// Metadata address.
    /// </summary>
    public static string MetadataAddress { get; } = "https://localhost:8181/.well-known/openid-configuration";

    /// <summary>
    /// Security key.
    /// </summary>
    public static SecurityKey SecurityKey { get; } =
        new SymmetricSecurityKey(
            // IDX10653: The encryption algorithm 'HS256' requires a key size of at least '128' bits.
            Encoding.ASCII.GetBytes("TestSecurityKeyRequires128BitsSize")
        );

    /// <summary>
    /// Signing credentials.
    /// </summary>
    public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

    /// <summary>
    /// Backchannel handler.
    /// </summary>
    public static HttpMessageHandler Backchannel { get; } = new TestJwtTokenHttpMessageHandler();

    /// <summary>
    /// Handler.
    /// </summary>
    public static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
}
