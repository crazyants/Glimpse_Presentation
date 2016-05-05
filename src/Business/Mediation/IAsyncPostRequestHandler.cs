using System.Threading.Tasks;

namespace Kiehl.App.Business.Mediation
{
    public interface IAsyncPostRequestHandler<in TRequest, in TResponse>
    {
        Task Handle(TRequest command, TResponse response);
    }
}
