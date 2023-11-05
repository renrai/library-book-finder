using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryDomain.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchString { get; set; }

        public PaginationFilter()
        {
        }

        public PaginationFilter(int pageNumber, int pageSize, string? seachString = null)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
            SearchString = seachString;
        }
    }
}
