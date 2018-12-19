using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        public bool IsActive { get; set; }

        public bool IsSaving { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}