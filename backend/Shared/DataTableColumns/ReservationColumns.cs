using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTableColumns;

public class ReservationColumns
{
    public static string id = "Id";
    public static string firstName = "Emri";
    public static string lastName = "Mbiemri";
    public static string email = "Email";
    public static string gender = "Gjinia";
    public static string identityNumber = "Numri i telefonit";
    public static string birthday = "Ditëlindja";
    public static string reason = "Arsyeja";
    public static string date = "Data";
    public static string startTime = "Koha e fillimit";
    public static string endTime = "Koha e mbarimit";



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
            case nameof(identityNumber):
                return identityNumber;
            case nameof(gender):
                return gender;
            case nameof(birthday):
                return birthday;
            case nameof(reason):
                return reason;
            case nameof(date):
                return date;
            case nameof(startTime):
                return startTime;
            case nameof(endTime):
                return endTime;
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
            case nameof(identityNumber):
            case nameof(gender):
            case nameof(birthday):
            case nameof(reason):
            case nameof(date):
            case nameof(startTime):
            case nameof(endTime):
            default:
                return false;
        }
    }

    public static bool GetPropertyFilterable(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(id):
                return false;
            case nameof(firstName):
            case nameof(lastName):
            case nameof(email):
            case nameof(identityNumber):
            case nameof(gender):
            case nameof(birthday):
            case nameof(reason):
            case nameof(date):
            case nameof(startTime):
            case nameof(endTime):
            default:
                return true;
        }
    }


    public static DataType GetPropertyDataType(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(id):
            case nameof(firstName):
            case nameof(lastName):
            case nameof(email):
            case nameof(identityNumber):
            case nameof(gender):
            case nameof(birthday):
                return DataType.DateTime;
            case nameof(reason):
            case nameof(date):
                return DataType.DateTime;
            case nameof(startTime):
            case nameof(endTime):
            default:
                return DataType.String;
        }
    }
}
