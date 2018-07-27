using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_PindahTempat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);

                RepeaterTempat.DataSource = ClassTempat.Data();
                RepeaterTempat.DataBind();
            }
        }
    }
    protected void RepeaterTempat_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pindah")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Pengguna = new PenggunaLogin(Pengguna.IDPengguna, e.CommandArgument.ToInt());

            if (Pengguna.IDPengguna > 0)
            {
                Session["PenggunaLogin"] = Pengguna;
                Session.Timeout = 525000;

                Session["Transaksi"] = null;

                //MEMBUAT COOKIES ENCRYPT
                Response.Cookies["WITEnterpriseSystem"].Value = Pengguna.EnkripsiIDPengguna;
                Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddYears(1);

                Response.Redirect("/WITAdministrator/Default.aspx");
            }
        }
    }
}