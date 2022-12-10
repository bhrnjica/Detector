using Detector.Contract.Interfaces;

namespace Detector.WebApi.Requests
{
    public class DetectorDeleteRequest: IDetectorRequest<IResult>
    {
        public int Id { get; set; }
    }
}
