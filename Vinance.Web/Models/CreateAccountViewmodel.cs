using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models
{
    public class CreateAccountViewmodel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Kérlek add meg a számla nevét.")]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Display(Name = "Egyenleg")]
        public int Balance { get; set; }

        public bool IsActive { get; set; }

        public bool IsSaving { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}