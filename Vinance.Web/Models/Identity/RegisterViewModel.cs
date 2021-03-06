﻿using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models.Identity
{
    public class RegisterViewmodel
    {
        [Required(ErrorMessage = "Kérlek add meg a felhasználóneved.")]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Kérlek add meg a jelszavad.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{0,}$", ErrorMessage = "A jelszónak tartalmaznia kell nagybetűt és számot.")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "A jelszónak legalább 6 karakterből kell állnia.")]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Kérlek add meg a jelszavad.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Nem egyezik a jelszóval.")]
        [Display(Name = "Jelszó megerősítése")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Kérlek add meg az email címed.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Kérlek egy valós email címet adj meg.")]
        [Display(Name = "Email cím")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kérlek írd be a vezetékneved.")]
        [Display(Name = "Vezetéknév")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kérlek írd be a keresztneved.")]
        [Display(Name = "Keresztnév")]
        public string FirstName { get; set; }
    }
}