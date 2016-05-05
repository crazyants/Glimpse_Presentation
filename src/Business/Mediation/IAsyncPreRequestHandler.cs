using System.Threading.Tasks;

namespace Kiehl.App.Business.Mediation
{
    public interface IAsyncPreRequestHandler<in TRequest>
    {
        Task Handle(TRequest request);
    }
}