﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Enumerations;
    using Models;
    using Models.Domain;

    public interface ICategoryApi
    {
        Task<IEnumerable<Category>> GetCategories(CategoryType? type = null, DateTime? from = null, DateTime? to = null);

        Task<IEnumerable<CategoryStatistics>> GetStats(CategoryType type = CategoryType.Expense, DateTime? from = null, DateTime? to = null, string by = null);
        Task<Category> Get(int categoryId);
        Task<bool> Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int categoryId);
    }
}