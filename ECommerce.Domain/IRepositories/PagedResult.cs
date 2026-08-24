using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.IRepositories
{
    public class PagedResult<T>
    {

        public PagedResult(IReadOnlyList<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
    }
}
