using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Parse
/// </summary>
public static class Parsing
{
    #region FORMAT
    public static string ToFormatDecimal(this object parameter)
    {
        //1,2345.00
        if (parameter != null)
        {
            decimal result;
            decimal.TryParse(parameter.ToString(), out result);

            return result.ToString("N", CultureInfo.InvariantCulture);
        }
        else
            return "0";
    }

    public static string ToFormatNumber(this object parameter)
    {
        //1,2345
        if (parameter != null)
        {
            decimal result;
            decimal.TryParse(parameter.ToString(), out result);

            return result.ToString("N0", CultureInfo.InvariantCulture);
        }
        else
            return "0";
    }

    public static string ToFormatPercentace(this object parameter)
    {
        //12.34 %
        if (parameter != null)
        {
            decimal result;
            decimal.TryParse(parameter.ToString(), out result);

            return (result / 100).ToString("P", CultureInfo.InvariantCulture);
        }
        else
            return "0%";
    }
    #endregion

    #region DATETIME
    public static string ToFormatDateShort(this object date)
    {
        //6/2/2018
        if (date != null)
        {
            return DateTime.Parse(date.ToString()).ToShortDateString();
        }
        else
            return string.Empty;
    }

    public static string ToFormatDateMedium(this object date)
    {
        //2 June 2018
        if (date != null)
        {
            return DateTime.Parse(date.ToString()).ToString("d MMMM yyyy");
        }
        else
            return string.Empty;
    }

    public static string ToFormatDateLong(this object date)
    {
        //Saturday, June 2, 2018
        if (date != null)
        {
            return DateTime.Parse(date.ToString()).ToLongDateString();
        }
        else
            return string.Empty;
    }

    public static string ToFormatDateFull(this object date)
    {
        //2 June 2018 22:00
        if (date != null)
        {
            return DateTime.Parse(date.ToString()).ToString("d MMMM yyyy HH:mm");
        }
        else
            return string.Empty;
    }

    public static string ToFormatTimeHour(this object date)
    {
        //18:45
        if (date != null)
        {
            return DateTime.Parse(date.ToString()).ToString("HH:mm");
        }
        else
            return string.Empty;
    }
    #endregion

    //public static decimal ToDecimal(this object parameter)
    //{
    //    decimal result;
    //    decimal.TryParse(parameter.ToString(), out result);

    //    return result;
    //}

    //public static int ToInt(this object parameter)
    //{
    //    int result;
    //    int.TryParse(parameter.ToString(), out result);

    //    return result;
    //}

    //public static DateTime ToDateTime(this object parameter)
    //{
    //    DateTime result;
    //    DateTime.TryParse(parameter.ToString(), out result);

    //    return result;
    //}

    //public static bool ToBool(this object parameter)
    //{
    //    bool result;
    //    bool.TryParse(parameter.ToString(), out result);

    //    return result;
    //}
}