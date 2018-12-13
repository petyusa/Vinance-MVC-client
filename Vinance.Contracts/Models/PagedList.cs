using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Contracts.Models
{
    public class PagedList<T>
    {
        public int Page { get;  set; }
        public int PageSize { get; set; }
        public int TotalPages { get;  set; }
        public int? CategoryId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Order { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public List<T> Items { get; private set; } = new List<T>();
    }
}