using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models.Domain;

    public interface ITransferApi
    {
        Task<IEnumerable<Transfer>> GetAll();
        Task<Transfer> Get(int transferId);
        Task<bool> Create(Transfer transfer);
        Task<bool> Delete(int transferId);
        Task<bool> Update(Transfer transfer);
    }
}