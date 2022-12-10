using Detector.Contract.DTO;
using Detector.Contract.Models;
using Detector.WebApi.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Detector.WebApi;

public static class UserService
{
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

        group.WithTags("Users");

        group.WithParameterValidation(typeof(UserItem));

        group.MapPost("/", async Task<Results<Ok, ValidationProblem>> (UserItem newUser, UserManager<DetectorUser> userManager) =>
        {
            var result = await userManager.CreateAsync(new() { UserName = newUser.Username }, newUser.Password);

            if (result.Succeeded)
            {
                return TypedResults.Ok();
            }

            return TypedResults.ValidationProblem(result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description }));
        });

        group.MapPost("/token", async Task<Results<BadRequest, Ok<AuthToken>>> (UserItem userInfo, UserManager<DetectorUser> userManager, ITokenService tokenService) =>
        {
            var user = await userManager.FindByNameAsync(userInfo.Username);

            if (user is null || !await userManager.CheckPasswordAsync(user, userInfo.Password))
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(new AuthToken(tokenService.GenerateToken(user.UserName!)));
        });

        return group;
    }
}
