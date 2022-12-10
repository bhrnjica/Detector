using Detector.Contract.Interfaces;

namespace Detector.WebApi.Requests
{
    public class DetectorCreateRequest: IDetectorRequest<IResult>
    {
        public string Name { get; set; }
        public int Version { get; set; }
        public string Notes { get; set; }
    }
}
