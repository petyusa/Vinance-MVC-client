using System;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models;
    using Models.Domain;

    public interface IIncomeApi
    {
        Task<PagedList<Income>> GetAll(int? accountId, int? categoryId, DateTime? from = null, DateTime? to = null, int? page = null, int? pageSize = null, string order = null);
        Task<Income> Get(int incomeId);
        Task<bool> Create(Income expense);
        Task<bool> Delete(int expenseId);
        Task<bool> Update(Income expense);
    }
}