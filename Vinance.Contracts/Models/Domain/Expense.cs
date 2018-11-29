using System;
using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Dátum")]
        public DateTime Date { get; set; }

        [Display(Name = "Összeg")]
        public int Amount { get; set; }

        [Display(Name = "Komment")]
        public string Comment { get; set; }

        public int FromId { get; set; }

        [Display(Name = "Honnan")]
        public string FromName { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Kategória")]
        public string CategoryName { get; set; }
    }
}