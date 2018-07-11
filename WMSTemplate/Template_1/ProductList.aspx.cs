using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] Data = new string[10];
        for (int i = 0; i < Data.Count(); i++)
        {
            Data[i] = "/frontend/assets/media/content/goods/mens/220x250/1.jpg";
        }

        RepeaterData.DataSource = Data.Select(item => new
        {
            Source = item
        });
        RepeaterData.DataBind();
    }
}