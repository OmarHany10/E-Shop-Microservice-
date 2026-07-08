using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Pagination
{
    public class PaginationResult<TEntity>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<TEntity> data { get; set; }
    }
}
