using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Discount_Event_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

                    var PelangganGrup = GrupPelanggan_Class.Cari(db, Request.QueryString["id"].ToInt());

                    if (PelangganGrup != null)
                    {
                        DiscountKombinasiProduk_Class ClassDiscountKombinasiProduk = new DiscountKombinasiProduk_Class(db);

                        LabelGrupPelanggan.Text = PelangganGrup.Nama;

                        RepeaterKombinasiProduk.DataSource = db.TBKombinasiProduks
                            .Select(item => new
                            {
                                item.IDKombinasiProduk,
                                Produk = item.TBProduk.Nama,
                                Brand = item.TBProduk.TBPemilikProduk.Nama,
                                Warna = item.TBProduk.TBWarna.Nama,
                                Kategori = item.TBProduk.TBProdukKategori.Nama,
                                Varian = item.TBAtributProduk.Nama
                            })
                            .OrderBy(item => item.Produk)
                            .ToArray();
                        RepeaterKombinasiProduk.DataBind();

                        var DataDiscount = ClassDiscountKombinasiProduk.Data(Request.QueryString["id"].ToInt());

                        foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
                        {
                            var HiddenFieldIDKombinasiProduk = (HiddenField)item.FindControl("HiddenFieldIDKombinasiProduk");
                            var TextBoxDiscount = (TextBox)item.FindControl("TextBoxDiscount");

                            var Data = DataDiscount.FirstOrDefault(item2 => item2.IDKombinasiProduk == HiddenFieldIDKombinasiProduk.Value.ToInt());

                            if (Data != null)
                                TextBoxDiscount.Text = Data.Discount.ToString();
                            else
                                TextBoxDiscount.Text = "0";
                        }
                    }
                    else
                        Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DiscountKombinasiProduk_Class ClassDiscountKombinasiProduk = new DiscountKombinasiProduk_Class(db);

                ClassDiscountKombinasiProduk.Hapus(Request.QueryString["id"].ToInt());

                db.SubmitChanges();

                foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
                {
                    var HiddenFieldIDKombinasiProduk = (HiddenField)item.FindControl("HiddenFieldIDKombinasiProduk");
                    var TextBoxDiscount = (TextBox)item.FindControl("TextBoxDiscount");

                    ClassDiscountKombinasiProduk.Tambah(Request.QueryString["id"].ToInt(), HiddenFieldIDKombinasiProduk.Value.ToInt(), TextBoxDiscount.Text.ToDecimal());
                }

                Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, Pengguna.IDPengguna, EnumAlert.success, "Discount Grup Pelanggan " + LabelGrupPelanggan.Text + " Berhasil Disimpan");

                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}