using Detector.Contract.Interfaces;

namespace Detector.WebApi.Requests
{
    public class DetectorUpdateRequest: DetectorCreateRequest
    {
        public int Id { get; set; }
      
    }
}
