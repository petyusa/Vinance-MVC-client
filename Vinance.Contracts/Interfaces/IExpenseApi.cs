using System;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models;
    using Models.Domain;

    public interface IExpenseApi
    {
        Task<PagedList<Expense>> GetAll(DateTime? from, DateTime? to, int page = 1, int pageSize = 20, string order = "date_desc");
        Task<Expense> Get(int expenseId);
        Task<bool> Create(Expense expense);
        Task<bool> Delete(int expenseId);
        Task<bool> Update(Expense expense);
    }
}