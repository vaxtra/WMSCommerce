using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_KategoriTempat_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                KategoriTempat_Class KategoriTempat_Class = new KategoriTempat_Class();

                var KategoriTempat = KategoriTempat_Class.Cari(db, Request.QueryString["id"].ToInt());

                if (KategoriTempat != null && KategoriTempat.IDKategoriTempat > (int)EnumKategoriTempat.Consignment)
                {
                    TextBoxNama.Text = KategoriTempat.Nama;

                    ButtonOk.Text = "Ubah";
                    LabelKeterangan.Text = "Ubah";
                }
                else
                {
                    LabelKeterangan.Text = "Tambah";
                    ButtonOk.Text = "Tambah";
                }
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            KategoriTempat_Class KategoriTempat_Class = new KategoriTempat_Class();

            if (ButtonOk.Text == "Tambah")
                KategoriTempat_Class.Tambah(db, TextBoxNama.Text);
            else if (ButtonOk.Text == "Ubah")
                KategoriTempat_Class.Ubah(db, Request.QueryString["id"].ToInt(), TextBoxNama.Text);

            db.SubmitChanges();
        }

        Response.Redirect("Default.aspx");
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}