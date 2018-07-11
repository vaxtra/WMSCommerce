using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_Datatable_Default : System.Web.UI.Page
{
    [WebMethod]
    //[ScriptMethod()]
    public static string GetData()
    {
        //return db.TBAtributProduks.Select(x => new
        //{
        //    IDAtributProduk = x.IDAtributProduk,
        //    NamaAtributProduk = x.Nama,
        //    IDGrupAtributProduk = x.IDGrupAtributProduk,
        //    NamaGrupAtributProduk = x.TBGrupAtributProduk.Nama
        //}).ToArray();

        //return "{ \"aaData\": [[ \"1\", \"Armand\", \"Warren\", \"56045\", \"Taiwan, Province of China\" ], [ \"2\", \"Xenos\", \"Salas\", \"71090\", \"Liberia\" ]] }";

        return "[[ \"1\", \"Armand\", \"Warren\", \"56045\", \"Taiwan, Province of China\" ], [ \"2\", \"Xenos\", \"Salas\", \"71090\", \"Liberia\" ]]";
    }

    //[System.Web.Services.WebMethod]
    //public static string GetSystemLog()
    //{
    //    return "{ \"aaData\": [[ \"1\", \"Armand\", \"Warren\", \"56045\", \"Taiwan, Province of China\" ]] }";

    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Response.Write(File.ReadAllText(@"D:\_RENDY HERDIAWAN\WITEnterpriseSystem\_WITEnterpriseSystemFinal\plugins\datatable\data.txt"));
        }
    }
}