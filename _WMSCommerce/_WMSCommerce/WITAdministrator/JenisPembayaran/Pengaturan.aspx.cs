using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_JenisPembayaran_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);

                DropDownListJenisBebanBiaya.DataSource = db.TBJenisBebanBiayas.ToArray();
                DropDownListJenisBebanBiaya.DataTextField = "Nama";
                DropDownListJenisBebanBiaya.DataValueField = "IDJenisBebanBiaya";
                DropDownListJenisBebanBiaya.DataBind();

                DropDownListAkun.DataSource = db.TBAkuns.Where(item=> item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanAkunGrup.Aset);
                DropDownListAkun.DataTextField = "Nama";
                DropDownListAkun.DataValueField = "IDAkun";
                DropDownListAkun.DataBind();

                var JenisPembayaran = ClassJenisPembayaran.Cari(Request.QueryString["id"].ToInt());

                if (JenisPembayaran != null && JenisPembayaran.IDJenisPembayaran != 1 && JenisPembayaran.IDJenisPembayaran != 2)
                {
                    DropDownListJenisBebanBiaya.SelectedValue = JenisPembayaran.IDJenisBebanBiaya.ToString();
                    TextBoxNama.Text = JenisPembayaran.Nama;

                    if (JenisPembayaran.IDJenisBebanBiaya == 1)
                        TextBoxPersentaseBiaya.Enabled = false;
                    else
                        TextBoxPersentaseBiaya.Enabled = true;

                    TextBoxPersentaseBiaya.Text = JenisPembayaran.PersentaseBiaya.ToFormatHarga();

                    ButtonSimpan.Text = "Ubah";
                    LabelKeterangan.Text = "Ubah";
                }
                else
                {
                    LabelKeterangan.Text = "Tambah";
                    ButtonSimpan.Text = "Tambah";
                    TextBoxPersentaseBiaya.Enabled = false;
                }
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);

            if (ButtonSimpan.Text == "Tambah")
                ClassJenisPembayaran.Tambah(DropDownListJenisBebanBiaya.SelectedValue.ToInt(), TextBoxNama.Text, TextBoxPersentaseBiaya.Text.ToDecimal(), DropDownListAkun.SelectedValue.ToInt());
            else if (ButtonSimpan.Text == "Ubah")
                ClassJenisPembayaran.Ubah(Request.QueryString["id"].ToInt(), DropDownListJenisBebanBiaya.SelectedValue.ToInt(), TextBoxNama.Text, TextBoxPersentaseBiaya.Text.ToDecimal(), DropDownListAkun.SelectedValue.ToInt());

            db.SubmitChanges();
        }

        Response.Redirect("Default.aspx");
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void DropDownListJenisBebanBiaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListJenisBebanBiaya.SelectedValue == "1")
        {
            TextBoxPersentaseBiaya.Enabled = false;
            TextBoxPersentaseBiaya.Text = "0";
        }
        else
            TextBoxPersentaseBiaya.Enabled = true;
    }
}