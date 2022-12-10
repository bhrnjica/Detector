using Detector.Contract.Interfaces;
using Detector.WebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Detector.WebApi.Handlers;

public class DetectorsHandler : IRequestHandler<DetectorsRequest, IResult>
{
    private readonly IDetectorRepository _repository;
    public DetectorsHandler( IDetectorRepository repository )
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(DetectorsRequest request, CancellationToken cancellationToken)
    {
        var retVal = await _repository.FindAllAsync();

        return Results.Ok(retVal);
    }

}
