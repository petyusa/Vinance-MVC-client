﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vinance.Web.Models.Transfer
{
    using Attributes;

    public class CreateTransferViewmodel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kérlek add meg a dátumot.")]
        [Display(Name = "Dátum")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Kérlek add meg az összeget.")]
        [Range(1, int.MaxValue, ErrorMessage = "Az összeg nem lehet kisebb, mint 1.")]
        [Display(Name = "Összeg")]
        public int Amount { get; set; }

        [Display(Name = "Komment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Kérlek válassz számlát.")]
        [Display(Name = "Honnan")]
        public int FromId { get; set; }

        [Required(ErrorMessage = "Kérlek válassz számlát.")]
        [Display(Name = "Hova")]
        [NoteEqual(nameof(FromId), ErrorMessage = "A két számla nem egyezhet.")]
        public int ToId { get; set; }

        public IEnumerable<SelectListItem> AccountList { get; set; }
    }
}