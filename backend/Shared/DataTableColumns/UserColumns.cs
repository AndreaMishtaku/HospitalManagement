using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTableColumns;

public class UserColumns
{
    public static string id = "Id";
    public static string firstName = "Emri";
    public static string lastName = "Mbiemri";
    public static string email = "Email";
    public static string phoneNumber = "Numri i telefonit";
    public static string gender = "Gjinia";
    public static string birthday = "Ditëlindja";
    public static string specialization = "Specializimi";
    public static string role = "Roli";
    


    public static string GetPropertyDescription(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(id):
                return id;
            case nameof(firstName):
                return firstName;
            case nameof(lastName):
                return lastName;
            case nameof(email):
                return email;
            case nameof(phoneNumber):
                return phoneNumber;
            case nameof(gender):
                return gender;
            case nameof(birthday):
                return birthday;
            case nameof(specialization):
                return specialization;
            case nameof(role):
                return role;
            default:
                return "";
        }
    }

    public static bool GetPropertyIsHidden(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(id):
                return true;
            case nameof(firstName):
            case nameof(lastName):
            case nameof(email):
            case nameof(phoneNumber):
            case nameof(role):
            case nameof(gender):
            case nameof(specialization):
            case nameof(birthday):
            default:
                return false;
        }
    }

    public static bool GetPropertyFilterable(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(id):
            case nameof(firstName):
            case nameof(lastName):
            case nameof(email):
            case nameof(phoneNumber):
            case nameof(role):
            case nameof(gender):
            case nameof(specialization):
            case nameof(birthday):
                return true;
            default:
                return true;
        }
    }


    public static DataType GetPropertyDataType(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(id):
            case nameof(phoneNumber):
                return DataType.Number;
            case nameof(firstName):
            case nameof(lastName):
            case nameof(email):
            case nameof(role):
            case nameof(specialization):
            case nameof(gender):
                return DataType.String;
            case nameof(birthday):
                return DataType.DateTime;
            default:
                return DataType.String;
        }
    }
}
