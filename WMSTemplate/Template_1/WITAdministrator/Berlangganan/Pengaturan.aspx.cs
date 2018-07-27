using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Berlangganan_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Berlangganan_Class ClassBerlangganan = new Berlangganan_Class();

            var Berlangganan = ClassBerlangganan.Cari(Request.QueryString["id"].ToInt());

            if (Berlangganan != null)
            {
                TextBoxEmail.Text = Berlangganan.Email;
                TextBoxNoTelepon.Text = Berlangganan.NoTelepon;

                ButtonOk.Text = "Ubah";
                LabelKeterangan.Text = "Ubah";
            }
            else
            {
                ButtonOk.Text = "Tambah";
                LabelKeterangan.Text = "Tambah";
            }
        }
    }

    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Berlangganan_Class ClassBerlangganan = new Berlangganan_Class();

            if (ButtonOk.Text == "Tambah")
                ClassBerlangganan.Tambah(db, TextBoxEmail.Text, TextBoxNoTelepon.Text);
            else if (ButtonOk.Text == "Ubah")
                ClassBerlangganan.Ubah(db, Request.QueryString["id"].ToInt(), TextBoxEmail.Text, TextBoxNoTelepon.Text);

            db.SubmitChanges();
        }

        Response.Redirect("Default.aspx");
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}