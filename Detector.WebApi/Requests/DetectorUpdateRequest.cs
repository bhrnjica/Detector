using Detector.Contract.Interfaces;

namespace Detector.WebApi.Requests
{
    public class DetectorUpdateRequest: IDetectorRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Version { get; set; }
        public string Notes { get; set; }
    }
}
