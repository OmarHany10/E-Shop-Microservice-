using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Pagination
{
    public class PaginationRequest
    {
        public int PageSize { get; set; } = 10;
        public int PageNumebr { get; set; } = 1;
    }
}
