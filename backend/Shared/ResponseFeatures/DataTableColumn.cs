using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseFeatures;

public class DataTableColumn
{
    public string Field { get; set; }
    public string HeaderName { get; set; }
    public bool Hide { get; set; }
    public DataType PropertyType { get; set; }
}
public enum DataType
{
    Number,
    String,
    DateTime,
    Decimal,
    Boolean,
    Actions,
    Link,
    Custom
}
