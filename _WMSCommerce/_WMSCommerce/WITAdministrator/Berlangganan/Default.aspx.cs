using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Berlangganan_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    protected void RepeaterBerlangganan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
            Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument.ToString());
        else if (e.CommandName == "Hapus")
        {
            Berlangganan_Class ClassBerlangganan = new Berlangganan_Class();

            if (ClassBerlangganan.Hapus(e.CommandArgument.ToInt()))
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Hapus berhasil");
            else
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Hapus gagal");

            LoadData();
        }
    }

    private void LoadData()
    {
        Berlangganan_Class ClassBerlangganan = new Berlangganan_Class();

        RepeaterBerlangganan.DataSource = ClassBerlangganan.LoadData();
        RepeaterBerlangganan.DataBind();
    }
}