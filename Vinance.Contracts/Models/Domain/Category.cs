
namespace Vinance.Contracts.Models.Domain
{
    using Enumerations;

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public CategoryType Type { get; set; }
    }
}