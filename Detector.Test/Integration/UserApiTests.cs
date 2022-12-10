﻿using Detector.Contract.DTO;
using Detector.Contract.Models;
using Detector.Test.Extensions;
using Detector.WebApi.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Detector.Test.Integration;

public class UserApiTests
{
    [Fact]
    public async Task CanCreateAUser()
    {
        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users", new UserItem { Username = "bahrudin", Password = "Bahrudin$2022" });

        Assert.True(response.IsSuccessStatusCode);

        var user = db.Users.Single();
        Assert.NotNull(user);

        Assert.Equal("bahrudin", user.UserName);
    }

    [Fact]
    public async Task MissingUserOrPasswordReturnsBadRequest()
    {
        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users", new UserItem { Username = "bahrudin", Password = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        Assert.Equal(new[] { "The Password field is required." }, problemDetails.Errors["Password"]);

        response = await client.PostAsJsonAsync("/users", new UserItem { Username = "", Password = "password" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        Assert.Equal(new[] { "The Username field is required." }, problemDetails.Errors["Username"]);
    }



    [Fact]
    public async Task CanGetATokenForValidUser()
    {
        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();
        await application.CreateUserAsync("bahrudin", "Bahrudin$2022");

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users/token", new UserItem { Username = "bahrudin", Password = "Bahrudin$2022" });

        Assert.True(response.IsSuccessStatusCode);

        var token = await response.Content.ReadFromJsonAsync<AuthToken>();

        Assert.NotNull(token);

        // Check that the token is indeed valid

        var req = new HttpRequestMessage(HttpMethod.Get, "/detectors");
        req.Headers.Authorization = new("Bearer", token.Token);
        response = await client.SendAsync(req);

        Assert.True(response.IsSuccessStatusCode);
    }



    [Fact]
    public async Task BadRequestForInvalidCredentials()
    {
        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();
        await application.CreateUserAsync("bahrudin", "Bahrudin$2022");

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users/token", new UserItem { Username = "bahrudin", Password = "pass" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}