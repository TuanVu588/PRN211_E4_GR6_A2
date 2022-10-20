using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211_E4_Group6_A2
{
    internal class PageList<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int PageCount { get; private set; }

        public PageList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageCount = (int) Math.Ceiling((double) count/(double) pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < PageCount; }
        }
        public static PageList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PageList<T>(items, count, pageIndex, pageSize);
        }
    }
}
