using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Dtos
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}
