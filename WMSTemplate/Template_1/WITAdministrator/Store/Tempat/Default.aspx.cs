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
        {
            LoadDataTempat();
            LoadDataKategori();
        }
    }

    #region Kategori
    private void LoadDataTempat()
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

                LoadDataTempat();
            }
        }
    }
    #endregion

    #region Kategori
    private void LoadDataKategori()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            KategoriTempat_Class KategoriTempat_Class = new KategoriTempat_Class();

            RepeaterKategoriTempat.DataSource = KategoriTempat_Class.Data(db);
            RepeaterKategoriTempat.DataBind();
        }
    }

    protected void RepeaterKategoriTempat_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {
                TBKategoriTempat kategori = db.TBKategoriTempats.FirstOrDefault(item => item.IDKategoriTempat == e.CommandArgument.ToInt());
                HiddenFieldIDKategoriTempat.Value = kategori.IDKategoriTempat.ToString();
                TextBoxKategoriNama.Text = kategori.Nama;

                ButtonSimpanKategori.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                db.TBKategoriTempats.DeleteOnSubmit(db.TBKategoriTempats.FirstOrDefault(item => item.IDKategoriTempat == e.CommandArgument.ToInt()));
                db.SubmitChanges();

                LoadDataKategori();
            }
        }

        
    }

    protected void ButtonSimpanKategori_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (ButtonSimpanKategori.Text == "Tambah")
                    db.TBKategoriTempats.InsertOnSubmit(new TBKategoriTempat { Nama = TextBoxKategoriNama.Text });
                else if (ButtonSimpanKategori.Text == "Ubah")
                {
                    TBKategoriTempat kategori = db.TBKategoriTempats.FirstOrDefault(item => item.IDKategoriTempat == HiddenFieldIDKategoriTempat.Value.ToInt());
                    kategori.Nama = TextBoxKategoriNama.Text;
                }
                db.SubmitChanges();

                HiddenFieldIDKategoriTempat.Value = null;
                TextBoxKategoriNama.Text = string.Empty;
                ButtonSimpanKategori.Text = "Tambah";              
            }

            LoadDataKategori();
        }
    }
    #endregion
}