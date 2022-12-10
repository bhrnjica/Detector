using Detector.Contract.DTO;
using Detector.Contract.Interfaces;
using Detector.WebApi.Requests;
using MediatR;
using System.Reflection.Metadata;

namespace Detector.WebApi.Handlers;

public class DetectorDeleteHandler : IRequestHandler<DetectorDeleteRequest, IResult>
{
    private readonly IDetectorRepository _repository;
    public DetectorDeleteHandler(IDetectorRepository repository)
    {
        _repository = repository;
    }
    public async Task<IResult> Handle( DetectorDeleteRequest request, CancellationToken cancellationToken )
    {
        var retVal = await _repository.DeleteAsync(request.Id);

        return retVal ? TypedResults.Ok() : TypedResults.NotFound();
    }


}
