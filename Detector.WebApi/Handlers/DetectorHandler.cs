using Detector.Contract.Interfaces;
using Detector.WebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Detector.WebApi.Handlers;

public class DetectorHandler : IRequestHandler<DetectorByIdRequest, IResult>
{

    private readonly IDetectorRepository _repository;
    public DetectorHandler(IDetectorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(DetectorByIdRequest request, CancellationToken cancellationToken)
    {
       
        var retVal =  await _repository.FindByIdAsync(request.Id);

        return Results.Ok( retVal );
    }


}
