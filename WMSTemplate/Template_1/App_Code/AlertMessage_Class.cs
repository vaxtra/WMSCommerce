using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

public static class AlertMessage_Class
{
    public static void Show(Control control, List<AlertMessage> ListAlertMessage)
    {
        string message = "";

        foreach (var item in ListAlertMessage)
        {
            if (!string.IsNullOrWhiteSpace(item.Title))
                message += "AlertMessage('" + item.EnumAlert.ToString() + "', '" + item.Title + "', '" + item.Body + "');";
            else
                message += "AlertMessage('" + item.EnumAlert.ToString() + "', '" + item.Body + "');";
        }

        if (!string.IsNullOrWhiteSpace(message))
            ScriptManager.RegisterStartupScript(control, control.GetType(), ",toastr", message, true);
    }
    public static void Show(Control control, EnumAlert enumAlert, string message)
    {
        ScriptManager.RegisterStartupScript(control, control.GetType(), ",toastr", "AlertMessage('" + enumAlert.ToString() + "', '" + message + "');", true);
    }
    public static void Show(Control control, EnumAlert enumAlert, string title, string message)
    {
        ScriptManager.RegisterStartupScript(control, control.GetType(), ",toastr", "AlertMessage('" + enumAlert.ToString() + "', '" + title + "', '" + message + "');", true);
    }
    public static void ShowRedirect(Control control, EnumAlert enumAlert, string message, string url)
    {
        ScriptManager.RegisterStartupScript(control, control.GetType(), ",toastr", "AlertMessageRedirect('" + enumAlert.ToString() + "', '" + message + "', '" + url + "');", true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    /// <param name="exception"></param>
    /// <param name="url">Request.Url.PathAndQuery</param>
    public static void ShowException(Control control, Exception exception, string url)
    {
        if (!exception.Message.StartsWith("[WMS Error] "))
        {
            LogError_Class LogError = new LogError_Class(exception, url);
            Show(control, EnumAlert.danger, "System Error - " + LogError.IdBlackBox);
        }
        else
            Show(control, EnumAlert.danger, "Terjadi Kesalahan", exception.Message.Replace("[WMS Error] ", ""));
    }
}

public class AlertMessage
{
    public EnumAlert EnumAlert { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}