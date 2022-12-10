﻿using Detector.Api.Authentication;
using Detector.Api.Filters;
using Detector.Api.Users;
using Detector.Contract.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Detector.Api;

public static class DetectorApi
{
    public static RouteGroupBuilder MapDetector(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/detector");

        group.WithTags("Detector");

        group.WithParameterValidation(typeof(UserInfo));

        group.MapPost("/", async Task<Results<Ok, ValidationProblem>> (UserInfo newUser, UserManager<DetectorUser> userManager) =>
        {
            var result = await userManager.CreateAsync(new() { UserName = newUser.Username }, newUser.Password);

            if (result.Succeeded)
            {
                return TypedResults.Ok();
            }

            return TypedResults.ValidationProblem(result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description }));
        });

        group.MapPost("/token", async Task<Results<BadRequest, Ok<AuthToken>>> (UserInfo userInfo, UserManager<DetectorUser> userManager, ITokenService tokenService) =>
        {
            var user = await userManager.FindByNameAsync(userInfo.Username);

            if (user == null || !await userManager.CheckPasswordAsync(user, userInfo.Password))
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(new AuthToken(tokenService.GenerateToken(user.UserName!)));
        });

        return group;
    }
}
