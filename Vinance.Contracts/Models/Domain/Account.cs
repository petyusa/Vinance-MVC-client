using System.ComponentModel.DataAnnotations;
using Vinance.Contracts.Enumerations;

namespace Vinance.Contracts.Models.Domain
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Számla típus")]
        public AccountType AccountType { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        [Display(Name = "Aktív")]
        public bool IsActive { get; set; }

        [Display(Name = "Megtakarítás")]
        public bool IsSaving { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}