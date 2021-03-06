﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    public class Income
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
        public int ToId { get; set; }

        [Display(Name = "Hova")]
        public string ToName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Kategória")]
        public string CategoryName { get; set; }
    }
}