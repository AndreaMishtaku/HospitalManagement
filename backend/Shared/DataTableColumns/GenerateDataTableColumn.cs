using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTableColumns;

public static class GenerateDataTableColumn<T>
{
    public static List<DataTableColumn> GetDataTableColumns()
    {
        var dataTableColumns = new List<DataTableColumn>();

        Type type = typeof(T);
        FieldInfo[] fields = type.GetFields();

        foreach (FieldInfo fi in fields)
        {
            dataTableColumns.Add(new DataTableColumn
            {
                Field = fi.Name,
                HeaderName = GetPropertyDescription(type, fi.Name),
                Hide = GetPropertyIsHidden(type, fi.Name),
                PropertyType = GetPropertyDataType(type, fi.Name),
            });
        }

        return dataTableColumns;
    }

    private static string GetPropertyDescription(Type type, string propertyName)
    {
        string propertyDescription = "";
        var descriptionMethod = type.GetMethod(nameof(GetPropertyDescription));
        if (descriptionMethod is not null)
        {
            var descriptionObjValue = descriptionMethod.Invoke(null, new object[] { propertyName });
            if (descriptionObjValue is not null)
                propertyDescription = (string)descriptionObjValue;
        }

        return propertyDescription;
    }

    private static bool GetPropertyIsHidden(Type type, string propertyName)
    {
        bool propertyHidden = true;
        var hiddenMethod = type.GetMethod(nameof(GetPropertyIsHidden));
        if (hiddenMethod is not null)
        {
            var hiddenObjValue = hiddenMethod.Invoke(null, new object[] { propertyName });
            if (hiddenObjValue is not null)
                propertyHidden = (bool)hiddenObjValue;
        }

        return propertyHidden;
    }

    private static bool GetPropertyHideable(Type type, string propertyName)
    {
        bool propertyHideable = true;
        var hideableMethod = type.GetMethod(nameof(GetPropertyHideable));
        if (hideableMethod is not null)
        {
            var hiddenObjValue = hideableMethod.Invoke(null, new object[] { propertyName });
            if (hiddenObjValue is not null)
                propertyHideable = (bool)hiddenObjValue;
        }

        return propertyHideable;
    }

    private static bool GetPropertyFilterable(Type type, string propertyName)
    {
        bool propertyFilterable = true;
        var filterableMethod = type.GetMethod(nameof(GetPropertyFilterable));
        if (filterableMethod is not null)
        {
            var filterableObjValue = filterableMethod.Invoke(null, new object[] { propertyName });
            if (filterableObjValue is not null)
                propertyFilterable = (bool)filterableObjValue;
        }

        return propertyFilterable;
    }

    private static int GetPropertyMinWidth(Type type, string propertyName)
    {
        int propertyMinWidth = 50;
        var minWidthMethod = type.GetMethod(nameof(GetPropertyMinWidth));
        if (minWidthMethod is not null)
        {
            var minWidthObjValue = minWidthMethod.Invoke(null, new object[] { propertyName });
            if (minWidthObjValue is not null)
                propertyMinWidth = (int)minWidthObjValue;
        }

        return propertyMinWidth;
    }

    private static DataType GetPropertyDataType(Type type, string propertyName)
    {
        DataType propertyType = DataType.String;
        var dataTypeMethod = type.GetMethod(nameof(GetPropertyDataType));
        if (dataTypeMethod is not null)
        {
            var dataTypeObjValue = dataTypeMethod.Invoke(null, new object[] { propertyName });

            if (dataTypeObjValue is not null)
                propertyType = (DataType)dataTypeObjValue;
        }

        return propertyType;
    }
}
