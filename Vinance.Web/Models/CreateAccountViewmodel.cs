using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models
{
    using Contracts.Enumerations;

    public class CreateAccountViewmodel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Kérlek add meg a számla nevét.")]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Számla típusa")]
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