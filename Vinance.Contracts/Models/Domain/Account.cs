using System.ComponentModel.DataAnnotations;

namespace Vinance.Contracts.Models.Domain
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Balance { get; set; }
    }
}