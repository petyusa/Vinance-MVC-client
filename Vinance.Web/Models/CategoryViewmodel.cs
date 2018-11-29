using System.ComponentModel.DataAnnotations;
using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Models
{
    public class CategoryViewmodel
    {
        public int Id { get; set; }

        [Display(Name = "Név")]
        public string Name { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        [Display(Name = "Típus")]
        public CategoryType Type { get; set; }

        [Display(Name = "Havi limit")]
        public int MonthlyLimit { get; set; }

        public int MonthlyLimitUsed { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}