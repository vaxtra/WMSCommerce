using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITWON_MasterPageWebView : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            LabelTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;
        }
    }

    protected void LinkButtonLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Login.aspx?do=logout&returnUrl=" + Request.RawUrl);
    }
}
