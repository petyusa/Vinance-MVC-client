using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Models
{
    public class CreateIncomeViewmodel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Amount { get; set; }

        public string Comment { get; set; }

        [Required]
        [Display(Name = "To")]
        public int ToId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> AccountList { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}