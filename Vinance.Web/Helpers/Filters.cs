using System;

namespace Vinance.Web.Helpers
{
    using Contracts.Enumerations;

    public class Filters
    {
        public int? CategoryId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Order { get; set; }
    }
}