using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Atribut_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadData(db);
            }
        }
    }
    private void LoadData(DataClassesDatabaseDataContext db)
    {
        TemplateKeterangan_Class TemplateKeterangan_Class = new TemplateKeterangan_Class();

        RepeaterTemplateKeterangan.DataSource = TemplateKeterangan_Class.Data(db);
        RepeaterTemplateKeterangan.DataBind();
    }
    private void ResetForm()
    {
        TextBoxKeterangan.Text = string.Empty;
        ButtonSimpan.Text = "Tambah";
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TemplateKeterangan_Class TemplateKeterangan_Class = new TemplateKeterangan_Class();

            if (ButtonSimpan.Text == "Tambah")
                TemplateKeterangan_Class.CariTambah(db, TextBoxKeterangan.Text);
            else if (ButtonSimpan.Text == "Ubah")
                TemplateKeterangan_Class.Ubah(db, HiddenFieldIDTemplateKeterangan.Value.ToInt(), TextBoxKeterangan.Text);

            db.SubmitChanges();
            LoadData(db);
        }

        ResetForm();
    }
    protected void RepeaterTemplateKeterangan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TemplateKeterangan_Class TemplateKeterangan_Class = new TemplateKeterangan_Class();

            if (e.CommandName == "Hapus")
            {
                if (TemplateKeterangan_Class.Hapus(db, e.CommandArgument.ToInt()))
                {
                    db.SubmitChanges();
                    LoadData(db);
                }
                else
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Tidak bisa dihapus");
            }
            else if (e.CommandName == "Ubah")
            {
                var TemplateKeterangan = TemplateKeterangan_Class.Cari(db, e.CommandArgument.ToInt());

                if (TemplateKeterangan != null)
                {
                    HiddenFieldIDTemplateKeterangan.Value = e.CommandArgument.ToString();
                    TextBoxKeterangan.Text = TemplateKeterangan.Isi;

                    ButtonSimpan.Text = "Ubah";
                }
            }
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Default.aspx");
    }
}