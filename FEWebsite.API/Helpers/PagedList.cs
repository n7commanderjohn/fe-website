using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FEWebsite.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            this.TotalCount = totalCount;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,
            int pageNumber, int pageSize)
        {
            var count = await source.CountAsync().ConfigureAwait(false);
            int elementsToSkip = (pageNumber - 1) * pageSize;
            var items = await source.Skip(elementsToSkip)
                .Take(pageSize).ToListAsync().ConfigureAwait(false);

            var pagedList = new PagedList<T>(items, count, pageNumber, pageSize);

            return pagedList;
        }
    }
}
