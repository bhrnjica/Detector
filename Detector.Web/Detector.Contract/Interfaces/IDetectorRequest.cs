using MediatR;


namespace Detector.Contract.Interfaces
{
    public interface IDetectorRequest <T> : IRequest< T >
    {

    }
}