using Detector.Contract.Interfaces;

namespace Detector.WebApi.Requests
{
    public class DetectorByIdRequest: IDetectorRequest<IResult>
    {
        public int Id { get; set; }

    }
}
