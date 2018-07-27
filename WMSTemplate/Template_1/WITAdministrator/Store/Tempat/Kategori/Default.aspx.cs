using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_KategoriTempat_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    protected void RepeaterKategoriTempat_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
            Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument.ToString());
        else if (e.CommandName == "Hapus")
        {
            KategoriTempat_Class KategoriTempat_Class = new KategoriTempat_Class();

            if (KategoriTempat_Class.Hapus(e.CommandArgument.ToInt()))
                LoadData();
            else
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "data tidak dapat dihapus");
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            KategoriTempat_Class KategoriTempat_Class = new KategoriTempat_Class();

            RepeaterKategoriTempat.DataSource = KategoriTempat_Class.Data(db);
            RepeaterKategoriTempat.DataBind();
        }
    }
}