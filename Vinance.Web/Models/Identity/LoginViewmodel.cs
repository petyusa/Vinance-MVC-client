using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models.Identity
{
    public class LoginViewmodel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
