using System.Collections.Generic;
using GodelTech.Microservices.Security;
using Microsoft.AspNetCore.Authorization;

namespace GodelTech.XUnit.Auth.Demo.Api;

public class PolicyFactory : IAuthorizationPolicyFactory
{
    public IReadOnlyDictionary<string, AuthorizationPolicy> Create()
    {
        return new Dictionary<string, AuthorizationPolicy>
        {
            ["add"] = GetAuthorizationPolicy("fake.add"),
            ["edit"] = GetAuthorizationPolicy("fake.edit"),
            ["delete"] = GetAuthorizationPolicy("fake.delete")
        };
    }

    private static AuthorizationPolicy GetAuthorizationPolicy(string requiredScope)
    {
        var policyBuilder = new AuthorizationPolicyBuilder();

        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.RequireClaim("scope", requiredScope);

        return policyBuilder.Build();
    }
}
