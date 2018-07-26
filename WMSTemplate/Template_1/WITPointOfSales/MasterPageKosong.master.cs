using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_MasterPageKosong : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["PenggunaLogin"] == null)
            Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=" + Request.RawUrl);
        else
        {
            Session.Timeout = 525000;
        }
    }
}
