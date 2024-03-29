﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using GodelTech.XUnit.Auth.Token;

[assembly: CLSCompliant(false)]
[assembly: InternalsVisibleTo("GodelTech.XUnit.Auth.Tests")]
namespace GodelTech.XUnit.Auth;

/// <summary>
/// HttpClient extensions.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Add JSON Web Token.
    /// </summary>
    /// <param name="client">HttpClient.</param>
    /// <param name="configure"></param>
    /// <param name="baseAddress">HttpClient base address.</param>
    /// <returns>HttpClient</returns>
    /// <exception cref="ArgumentNullException">Throws exception when HttpClient is null.</exception>
    public static HttpClient WithJwtBearerToken(
        this HttpClient client,
        Action<TestJwtTokenBuilder> configure = null,
        string baseAddress = "https://localhost:8080")
    {
        ArgumentNullException.ThrowIfNull(client);

        // to make direct https calls
        client.BaseAddress = new Uri(baseAddress);

        var tokenBuilder = new TestJwtTokenBuilder();

        configure?.Invoke(tokenBuilder);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenBuilder.Build());

        return client;
    }
}
