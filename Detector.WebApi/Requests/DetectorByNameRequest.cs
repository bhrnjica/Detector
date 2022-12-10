using Detector.Contract.Interfaces;

namespace Detector.WebApi.Requests
{
    public class DetectorByNameRequest: IDetectorRequest<IResult>
    {
        public string Name { get; set; } = default!;

    }
}
