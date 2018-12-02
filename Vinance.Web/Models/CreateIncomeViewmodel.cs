﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Models
{
    public class CreateIncomeViewmodel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dátum")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Összeg")]
        public int Amount { get; set; }

        [Display(Name = "Komment")]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Hova")]
        public int ToId { get; set; }

        [Required]
        [Display(Name = "Kategória")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> AccountList { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}