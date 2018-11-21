using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Enumerations;
    using Models.Domain;

    public interface ICategoryApi
    {
        Task<IEnumerable<Category>> GetCategories(CategoryType? type = null);
        Task<bool> Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int categoryId);
    }
}