using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string filePath = "/App_Data/Konfigurasi/1.json";
        var mimeType = MimeMapping.GetMimeMapping(filePath);

        //Response.Write(mimeType);

        Response.Clear();
        Response.ContentType = mimeType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.Flush();
    }
}