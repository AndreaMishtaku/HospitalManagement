using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures;

public class PagedListRequest<T>:List<T>
{
    public MetaData MetaData { get; set; }

    public PagedListRequest(List<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(items);
    }

    public static PagedListRequest<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageNumber) * pageSize)
            .Take(pageSize).ToList();

        return new PagedListRequest<T>(items, count, pageNumber, pageSize);
    }

    public static PagedListRequest<T> ToPagedListByOne(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageNumber) + 1)
            .Take(pageSize).ToList();

        return new PagedListRequest<T>(items, count, pageNumber, pageSize);
    }
}
