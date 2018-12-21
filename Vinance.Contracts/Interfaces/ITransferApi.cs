using System;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models;
    using Models.Domain;

    public interface ITransferApi
    {
        Task<PagedList<Transfer>> GetAll(int? accountId, DateTime? from = null, DateTime? to = null, int? page = null, int? pageSize = null, string order = null);
        Task<Transfer> Get(int transferId);
        Task<bool> Create(Transfer transfer);
        Task<bool> Delete(int transferId);
        Task<bool> Update(Transfer transfer);
    }
}