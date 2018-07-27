using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPagePrint : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //CEK APAKAH SESSION ADA
        if (Session["PenggunaLogin"] == null)
        {
            if (Request.Cookies["WITEnterpriseSystem"] != null)
            {
                //JIKA COOKIES ADA
                PenggunaLogin Pengguna = new PenggunaLogin(Request.Cookies["WITEnterpriseSystem"].Value, true);

                if (Pengguna.IDPengguna > 0)
                {
                    Session["PenggunaLogin"] = Pengguna;
                    Session.Timeout = 525000;

                    //MEMBUAT COOKIES ENCRYPT
                    Response.Cookies["WITEnterpriseSystem"].Value = Pengguna.EnkripsiIDPengguna;
                    Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddYears(1);
                }
                else
                    Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=" + Request.RawUrl);
            }
            else
                Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=" + Request.RawUrl);
        }

        Session.Timeout = 525000;
    }
}
