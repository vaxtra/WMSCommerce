using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_Percobaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<string, string> VarianProduk = new Dictionary<string, string>();

        VarianProduk.Add("A", "S");
        VarianProduk.Add("B", "M");
        VarianProduk.Add("C", "L");
        VarianProduk.Add("D", "XL");

        foreach (var item in VarianProduk)
        {
            Response.Write(item.Key + " " + item.Value + "</br>");
        }

    }
}