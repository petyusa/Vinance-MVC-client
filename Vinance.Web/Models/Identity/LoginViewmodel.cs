using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models.Identity
{
    public class LoginViewmodel
    {
        [Required(ErrorMessage = "Kérlek add meg a felhasználóneved.")]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Kérlek add meg a jelszavad")]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [Display(Name = "Emlékezz rám")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
