using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Types;

public enum FilterOperation
{
    Contains,
    StartsWith,
    EndsWith,
    Equals,
    Less,
    LessOrEquals,
    More,
    MoreOrEquals,
    RangeDate,
    NotEqual
}

public enum SortDirection
{
    Asc,
    Desc
}
