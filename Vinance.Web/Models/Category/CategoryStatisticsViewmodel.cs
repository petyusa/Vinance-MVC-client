using System;
using System.Collections.Generic;

namespace Vinance.Web.Models.Category
{
    public class CategoryStatisticsList
    {
        public int Page { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public IEnumerable<CategoryStatisticsViewmodel> ExpenseStats { get; set; }
        public IEnumerable<CategoryStatisticsViewmodel> IncomeStats { get; set; }
    }

    public class CategoryStatisticsViewmodel
    {
        public DateTime Date { get; set; }
        public IEnumerable<CategoryStatisticsItemViewmodel> Items { get; set; }
    }

    public class CategoryStatisticsItemViewmodel
    {
        public int Balance { get; set; }
        public string Name { get; set; }
    }
}