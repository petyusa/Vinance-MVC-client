using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models.Domain;

    public interface IExpenseApi
    {
        Task<IEnumerable<Expense>> GetAll();
        Task<Expense> Get(int expenseId);
        Task<bool> Create(Expense expense);
        Task<bool> Delete(int expenseId);
        Task<bool> Update(Expense expense);
    }
}