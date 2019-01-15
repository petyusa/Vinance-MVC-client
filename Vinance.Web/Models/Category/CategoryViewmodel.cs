using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models.Category
{
    using Attributes;
    using Contracts.Enumerations;

    public class CategoryViewmodel
    {
        public int Id { get; set; }

        [Display(Name = "Név")]
        [Required(ErrorMessage = "Kérlek add meg a kategória nevét.")]
        public string Name { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        [Display(Name = "Típus")]
        [Required(ErrorMessage = "Kérel add meg a kategória típusát.")]
        public CategoryType Type { get; set; }

        [Display(Name = "Havi limit")]
        [RangeByOther("Type", 1, int.MaxValue, ErrorMessage = "A havi limit nem lehet kisebb, mint 1.")]
        public int MonthlyLimit { get; set; }

        public int MonthlyLimitUsed { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}