using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_StokProduk_Bertambah : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    private void LoadData()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var ListStokProduk = db.TBStokProduks
                .Where(item => item.IDTempat == Pengguna.IDTempat)
                .Select(item => new
                {
                    item.IDStokProduk,
                    item.TBKombinasiProduk.IDKombinasiProduk,
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                    PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    item.Jumlah,
                    item.JumlahMinimum
                })
                .OrderBy(item => item.Produk)
                .ToArray();

            RepeaterStokProduk.DataSource = ListStokProduk;
            RepeaterStokProduk.DataBind();
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                foreach (RepeaterItem item in RepeaterStokProduk.Items)
                {
                    Label LabelIDStokProduk = (Label)item.FindControl("LabelIDStokProduk");
                    TextBox TextBoxJumlahMinimum = (TextBox)item.FindControl("TextBoxJumlahMinimum");

                    if (TextBoxJumlahMinimum.Text.ToInt() > 0)
                    {
                        var StokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == LabelIDStokProduk.Text.ToInt());

                        StokProduk.JumlahMinimum = TextBoxJumlahMinimum.Text.ToInt();
                    }
                }

                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }

        LoadData();
    }
}