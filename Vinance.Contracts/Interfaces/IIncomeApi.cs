using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models.Domain;

    public interface IIncomeApi
    {
        Task<IEnumerable<Income>> GetAll();
        Task<bool> Create(Income expense);
        Task<bool> Delete(int expenseId);
        Task<bool> Update(Income expense);
    }
}