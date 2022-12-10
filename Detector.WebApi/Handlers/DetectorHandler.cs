using Detector.Contract.Interfaces;
using Detector.WebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Detector.WebApi.Handlers;

public class DetectorHandler : IRequestHandler<DetectorByNameRequest, IResult>
{

    private readonly IDetectorRepository _repository;
    public DetectorHandler(IDetectorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(DetectorByNameRequest request, CancellationToken cancellationToken)
    {
       
        var retVal =  await _repository.FindByNameAsync(request.Name);

        return Results.Ok( retVal );
    }


}
