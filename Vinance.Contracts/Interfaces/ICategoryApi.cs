using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Enumerations;
    using Models.Domain;

    public interface ICategoryApi
    {
        Task<IEnumerable<Category>> GetCategories(CategoryType? type);
        Task<bool> Create(Category category);
    }
}