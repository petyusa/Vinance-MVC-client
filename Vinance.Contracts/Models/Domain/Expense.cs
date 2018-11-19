using System;
using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    public class Expense
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public string Comment { get; set; }

        public int FromId { get; set; }

        [Display(Name = "From")]
        public string FromName { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}