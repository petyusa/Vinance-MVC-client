using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models
{
    using Contracts.Enumerations;

    public class CategoryViewmodel
    {
        public int Id { get; set; }

        [Display(Name = "Név")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        [Display(Name = "Típus")]
        [Required]
        public CategoryType Type { get; set; }

        [Display(Name = "Havi limit")]
        [Range(1, int.MaxValue, ErrorMessage = "A havi limit nem lehet kisebb, mint 0.")]
        public int MonthlyLimit { get; set; }

        public int MonthlyLimitUsed { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}