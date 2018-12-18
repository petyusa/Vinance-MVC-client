using System.Net.Http;
using System.Threading.Tasks;
using Vinance.Contracts.Models;

namespace Vinance.Contracts.Interfaces
{
    public interface IResponseHandler
    {
        Task<T> HandleAsync<T>(HttpResponseMessage response);
        Task<Response<T>> HandleWithErrorAsync<T>(HttpResponseMessage response);
    }
}