using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models.Domain;

    public interface IExpenseApi
    {
        Task<IEnumerable<Expense>> GetAll();
        Task<bool> Create(Expense expense);
    }
}