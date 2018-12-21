using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Enumerations
{
    public enum CategoryType
    {
        [Display(Name = "Költség")]
        Expense,

        [Display(Name = "Bevétel")]
        Income
    }
}