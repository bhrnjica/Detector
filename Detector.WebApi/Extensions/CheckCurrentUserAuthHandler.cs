using Detector.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Detector.WebApi.Extensions;

public static class AuthorizationHandlerExtensions
{
    public static AuthorizationBuilder AddCurrentUserHandler(this AuthorizationBuilder builder)
    {
        builder.Services.AddScoped<IAuthorizationHandler, CheckCurrentUserAuthHandler>();
        return builder;
    }

    // Adds the current user requirement that will activate our authorization handler
    public static AuthorizationPolicyBuilder RequireCurrentUser(this AuthorizationPolicyBuilder builder)
    {
        return builder.RequireAuthenticatedUser()
                      .AddRequirements(new CheckCurrentUserRequirement());
    }

    private class CheckCurrentUserRequirement : IAuthorizationRequirement { }

    // This authorization handler verifies that the user exists even if there's
    // a valid token
    private class CheckCurrentUserAuthHandler : AuthorizationHandler<CheckCurrentUserRequirement>
    {
        private readonly CurrentUser _currentUser;
        public CheckCurrentUserAuthHandler(CurrentUser currentUser) => _currentUser = currentUser;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckCurrentUserRequirement requirement)
        {
            // TODO: Check user if the user is locked out as well
            if (_currentUser.User is not null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
