using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using GodelTech.XUnit.Auth.Utilities;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace GodelTech.XUnit.Auth.Tests;

public class TestJwtTokenBuilderTests
{
    private readonly Mock<IDateTime> _dateTime;

    private readonly TestJwtTokenBuilder _builder;

    public TestJwtTokenBuilderTests()
    {
        _dateTime = new Mock<IDateTime>(MockBehavior.Strict);

        _builder = new TestJwtTokenBuilder(_dateTime.Object);
    }

    [Fact]
    public void Claims_Default()
    {
        // Arrange & Act
        var result = _builder.Claims;

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ExpiresInMinutes_Default()
    {
        // Arrange & Act
        var result = _builder.ExpiresInMinutes;

        // Assert
        Assert.Equal(30, result);
    }

    [Fact]
    public void WithUserId_Success()
    {
        // Arrange & Act
        _builder.WithUserId("TestUserId");

        // Assert
        ValidateClaim(JwtClaimTypes.Subject, "TestUserId");
    }

    [Fact]
    public void WithEmail_Success()
    {
        // Arrange & Act
        _builder.WithEmail("TestEmail");

        // Assert
        ValidateClaim(JwtClaimTypes.Email, "TestEmail");
    }

    [Fact]
    public void WithScope_Success()
    {
        // Arrange & Act
        _builder.WithScope("TestScope");

        // Assert
        ValidateClaim(JwtClaimTypes.Scope, "TestScope");
    }

    [Fact]
    public void WithClaim_Success()
    {
        // Arrange & Act
        _builder.WithClaim("TestType", "TestValue");

        // Assert
        ValidateClaim("TestType", "TestValue");
    }

    private void ValidateClaim(string type, string value)
    {
        var claim = Assert.Single(_builder.Claims);
        Assert.Equal(type, claim.Type);
        Assert.Equal(value, claim.Value);
    }

    [Fact]
    public void WithExpiration_Success()
    {
        // Arrange & Act
        _builder.WithExpiration(123);

        // Assert
        Assert.Equal(123, _builder.ExpiresInMinutes);
    }

    [Fact]
    public async Task Build_ReturnsValidToken()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;
        dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc);

        _dateTime
            .Setup(x => x.GetUtcNow())
            .Returns(dateTime);

        // Act
        var result = _builder
            .WithScope("TestScope")
            .WithExpiration(1)
            .Build();

        // Assert
        var validationResult = await TestJwtTokenProvider.JwtSecurityTokenHandler.ValidateTokenAsync(
            result,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "TestIssuer",
                ValidAudience = "TestAudience",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes("TestSecurityKeyRequires128BitsSize")
                ),
                // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }
        );

        Assert.True(validationResult.IsValid);

        var token = new JwtSecurityToken(result);

        Assert.Equal(dateTime.AddMinutes(1), token.ValidTo);
    }
}
