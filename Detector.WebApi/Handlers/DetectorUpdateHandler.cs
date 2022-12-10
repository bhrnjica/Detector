using Detector.Contract.DTO;
using Detector.Contract.Interfaces;
using Detector.WebApi.Requests;
using MediatR;
using System.Reflection.Metadata;

namespace Detector.WebApi.Handlers;

public class DetectorUpdateHandler : IRequestHandler<DetectorUpdateRequest, IResult>
{
    private readonly IDetectorRepository _repository;
    public DetectorUpdateHandler(IDetectorRepository repository)
    {
        _repository = repository;
    }
    public async Task<IResult> Handle( DetectorUpdateRequest request, CancellationToken cancellationToken )
    {
        await Task.Delay(10, cancellationToken);

        var itm = new DetectorItem {Id=request.Id, Name = request.Name, Version=request.Version, Notes = request.Notes };

        var retVal = await _repository.UpdateAsync(itm);

        return TypedResults.Created($"/detectors/{retVal.Name}", retVal);
    }


}
