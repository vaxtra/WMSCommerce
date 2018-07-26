using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Tempat_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    protected void RepeaterTempat_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
            Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument.ToString());
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);

                ClassTempat.Hapus(e.CommandArgument.ToInt());

                LoadData();
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterTempat.DataSource = db.TBTempats
                .Select(item => new
                {
                    item.IDTempat,
                    Kategori = item.TBKategoriTempat.Nama,
                    item.Nama,
                    item.Alamat,
                    item.Telepon1,
                    item.Telepon2,
                    item.Email
                })
                .ToArray();
            RepeaterTempat.DataBind();
        }
    }
}