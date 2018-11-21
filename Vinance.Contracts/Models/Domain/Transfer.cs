using System;
using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    public class Transfer
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Amount { get; set; }

        public string Comment { get; set; }

        [Required]
        public int ToId { get; set; }

        [Display(Name = "To")]
        public string ToName { get; set; }

        [Required]
        public int FromId { get; set; }

        [Display(Name = "From")]
        public string FromName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}