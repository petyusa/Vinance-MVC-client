using System;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models;
    using Models.Domain;

    public interface IExpenseApi
    {
        Task<PagedList<Expense>> GetAll(int? categoryId, DateTime? from = null, DateTime? to = null, int? page = null, int? pageSize = null, string order = null);
        Task<Expense> Get(int expenseId);
        Task<bool> Create(Expense expense);
        Task<bool> Delete(int expenseId);
        Task<bool> Update(Expense expense);
    }
}