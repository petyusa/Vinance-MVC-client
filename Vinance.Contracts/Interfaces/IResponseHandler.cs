using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    public interface IResponseHandler
    {
        Task<T> HandleAsync<T>(HttpResponseMessage response);
        Task<T> HandleWithErrorAsync<T>(HttpResponseMessage response);
    }
}