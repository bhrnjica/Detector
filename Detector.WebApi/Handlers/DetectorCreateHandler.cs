using Detector.Contract.DTO;
using Detector.Contract.Interfaces;
using Detector.WebApi.Requests;
using MediatR;
using System.Reflection.Metadata;

namespace Detector.WebApi.Handlers;

public class DetectorCreateHandler : IRequestHandler<DetectorCreateRequest, IResult>
{
    private readonly IDetectorRepository _repository;
    public DetectorCreateHandler(IDetectorRepository repository)
    {
        _repository = repository;
    }
    public async Task<IResult> Handle(DetectorCreateRequest request, CancellationToken cancellationToken )
    {
        var itm = new DetectorItem { Name = request.Name, Version=request.Version, Notes = request.Notes };

        var retVal = await _repository.CreateAsync(itm);

        return TypedResults.Created($"/detectors/{retVal.Name}", retVal);
    }


}
