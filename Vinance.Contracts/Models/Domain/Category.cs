
using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    using Enumerations;

    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Név")]
        public string Name { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        [Display(Name = "Havi limit")]
        public int MonthlyLimit { get; set; }

        [Display(Name = "Típus")]
        public CategoryType Type { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}