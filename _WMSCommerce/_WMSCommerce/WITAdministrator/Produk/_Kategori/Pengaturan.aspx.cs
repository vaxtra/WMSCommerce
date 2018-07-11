using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

                    ClassProdukKategori.DropDownList(DropDownListKategoriParent);
                    DropDownListKategoriParent.Items.Remove(DropDownListKategoriParent.Items.FindByValue("1"));
                    DropDownListKategoriParent.Items.Insert(0, new ListItem { Value = "0", Text = "- Kategori Utama -" });

                    var Data = ClassProdukKategori.Cari(Request.QueryString["id"].ToInt());

                    if (Data != null && Data.IDProdukKategori != 1)
                    {
                        DropDownListKategoriParent.SelectedValue = Data.IDProdukKategoriParent.HasValue ? Data.IDProdukKategoriParent.Value.ToString() : "0";
                        TextBoxNama.Text = Data.Nama;
                        TextBoxDeskripsi.Text = Data.Deskripsi;
                        CheckBoxActive.Checked = Data._IsActive;

                        ButtonOk.Text = "Ubah";
                    }
                    else
                    {
                        CheckBoxActive.Checked = true;
                        ButtonOk.Text = "Tambah";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

                if (ButtonOk.Text == "Tambah")
                    ClassProdukKategori.Tambah(DropDownListKategoriParent.SelectedValue.ToInt(), TextBoxNama.Text, TextBoxDeskripsi.Text);
                else if (ButtonOk.Text == "Ubah")
                    ClassProdukKategori.Ubah(Request.QueryString["id"].ToInt(), DropDownListKategoriParent.SelectedValue.ToInt(), TextBoxNama.Text, TextBoxDeskripsi.Text, CheckBoxActive.Checked);

                db.SubmitChanges();

                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}