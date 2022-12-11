using Detector.Contract.DTO;
using Detector.Contract.Models;
using Detector.WebApi.Authentication;
using Detector.WebApi.Extensions;
using Detector.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Detector.WebApi;

public static class DetectorService
{
    public static RouteGroupBuilder MapDetector(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/detectors");

        group.WithTags("Detectors");

        // Add security requirements, all incoming requests to this API *must*
        // be authenticated with a valid user.
        group.RequireAuthorization(pb => pb.RequireCurrentUser()).AddOpenApiSecurityRequirement();

        group.WithParameterValidation(typeof(DetectorItem));


        group.MapGet("/", async (IMediator mediator, [AsParameters] DetectorsRequest request) =>
        {
           return await mediator.Send(request);

        });

        group.MapGet("/{Id}", async (IMediator mediator, [AsParameters] DetectorByIdRequest request) =>
        {
            return await mediator.Send(request); ;
        });

        
        group.MapPost("/", async (IMediator mediator, [FromBody] DetectorCreateRequest request) =>
        {
            return await mediator.Send(request);
        });

        group.MapPut("/{Id}", async (IMediator mediator, [FromBody] DetectorUpdateRequest request) =>
        {
            return await mediator.Send(request);
        });

        group.MapDelete("/{id}", async (IMediator mediator, [AsParameters] DetectorDeleteRequest request) =>
        {
            return await mediator.Send(request);

        });
        return group;
    }
}
