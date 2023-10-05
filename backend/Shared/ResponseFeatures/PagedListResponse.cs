using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseFeatures;

public class PagedListResponse<T>
{
    public int RowCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<DataTableColumn> Columns { get; set; }
    public T Rows { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public int TotalPages { get; set; }
}
