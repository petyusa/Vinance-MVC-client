using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Category
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Web.Models.Category;

    public class CategoryStats : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public CategoryStats(ICategoryApi categoryApi, IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime? from = null, DateTime? to = null, int page = 1)
        {
            if (!from.HasValue || !to.HasValue)
            {
                var now = DateTime.Now;
                to = new DateTime(now.Year, now.Month, 1).AddMonths(1);
                from = to.Value.AddYears(-1);
            }

            var expenseStatistics = await _categoryApi.GetStats(CategoryType.Expense, from, to);
            var incomeStatistics = await _categoryApi.GetStats(CategoryType.Income, from, to);
            var model = new CategoryStatisticsList
            {
                From = from.Value,
                To = to.Value,
                Page = page,
                ExpenseStats = _mapper.Map<IEnumerable<CategoryStatisticsViewmodel>>(expenseStatistics),
                IncomeStats = _mapper.Map<IEnumerable<CategoryStatisticsViewmodel>>(incomeStatistics)
            };

            return View("CategoryStats", model);
        }
    }
}