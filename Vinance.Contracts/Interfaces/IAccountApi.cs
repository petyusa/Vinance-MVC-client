using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models.Domain;

    public interface IAccountApi
    {
        Task<Account> Create(Account account);
        Task<IEnumerable<Account>> GetAll();
        Task<string> GetById();
        Task<string> Update();
        Task Delete(int accountId);
    }
}