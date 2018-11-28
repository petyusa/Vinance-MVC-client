using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Models
{
    public class CategoryViewmodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public CategoryType Type { get; set; }
        public int MonthlyLimit { get; set; }
        public int MonthlyLimitUsed { get; set; }
    }
}