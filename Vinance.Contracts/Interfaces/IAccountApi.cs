using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models.Domain;

    public interface IAccountApi
    {
        Task<bool> Create(Account account);
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetById(int accountId);
        Task<bool> Update(Account account);
        Task<bool> Delete(int accountId);
        Task<Dictionary<DateTime, int>> GetDailyBalances(DateTime? from = null, DateTime? to = null);
    }
}