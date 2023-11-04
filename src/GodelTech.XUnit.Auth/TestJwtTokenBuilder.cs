using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using GodelTech.XUnit.Auth.Utilities;
using IdentityModel;

[assembly: CLSCompliant(false)]
[assembly: InternalsVisibleTo("GodelTech.XUnit.Auth.Tests")]
namespace GodelTech.XUnit.Auth;

/// <summary>
/// JSON Web Token builder.
/// </summary>
public class TestJwtTokenBuilder
{
    private readonly IDateTime _dateTime;

    /// <summary>
    /// Creates a new instance of the TestJwtTokenBuilder.
    /// </summary>
    /// <param name="dateTime">The DateTime utility.</param>
    public TestJwtTokenBuilder(IDateTime dateTime = default(SystemDateTime))
    {
        _dateTime = dateTime ?? new SystemDateTime();
    }

    /// <summary>
    /// Claims.
    /// </summary>
    public IList<Claim> Claims { get; } = new List<Claim>();

    /// <summary>
    /// Token expiration in minutes.
    /// </summary>
    public int ExpiresInMinutes { get; set; } = 30;

    /// <summary>
    /// Adds User Id claim.
    /// </summary>
    /// <param name="userId">User Id.</param>
    /// <returns>Builder.</returns>
    public TestJwtTokenBuilder WithUserId(string userId)
    {
        return WithClaim(JwtClaimTypes.Subject, userId);
    }

    /// <summary>
    /// Adds email claim.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <returns>Builder.</returns>
    public TestJwtTokenBuilder WithEmail(string email)
    {
        return WithClaim(JwtClaimTypes.Email, email);
    }

    /// <summary>
    /// Adds scope claim.
    /// </summary>
    /// <param name="scope">Scope.</param>
    /// <returns>Builder.</returns>
    public TestJwtTokenBuilder WithScope(string scope)
    {
        return WithClaim(JwtClaimTypes.Scope, scope);
    }

    /// <summary>
    /// Adds claim.
    /// </summary>
    /// <param name="type">Claim type.</param>
    /// <param name="value">Claim value.</param>
    /// <returns>Builder.</returns>
    public TestJwtTokenBuilder WithClaim(string type, string value)
    {
        Claims.Add(new Claim(type, value));

        return this;
    }

    /// <summary>
    /// Set token expiration in minutes.
    /// </summary>
    /// <param name="expiresInMinutes">Expiration in minutes.</param>
    /// <returns>Builder.</returns>
    public TestJwtTokenBuilder WithExpiration(int expiresInMinutes)
    {
        ExpiresInMinutes = expiresInMinutes;

        return this;
    }

    /// <summary>
    /// Builds token.
    /// </summary>
    /// <returns>Token.</returns>
    public string Build()
    {
        var token = new JwtSecurityToken(
            TestJwtTokenProvider.Issuer,
            TestJwtTokenProvider.Audience,
            Claims,
            expires: _dateTime.GetUtcNow().AddMinutes(ExpiresInMinutes),
            signingCredentials: TestJwtTokenProvider.SigningCredentials
        );

        return TestJwtTokenProvider.JwtSecurityTokenHandler.WriteToken(token);
    }
}
