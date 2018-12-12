using System.Collections.Generic;

namespace Vinance.Contracts.Models
{
    public class PagedList<T>
    {
        public int Page { get;  set; }
        public int PageSize { get; set; }
        public int TotalPages { get;  set; }
        public List<T> Items { get; private set; } = new List<T>();
    }
}