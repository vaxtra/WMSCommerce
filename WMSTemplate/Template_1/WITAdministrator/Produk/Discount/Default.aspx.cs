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
        if (!IsPostBack)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
            }
        }
    }

    public void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            RepeaterStokProduk.DataSource = db.TBStokProduks
                .Where(item =>
                    item.TBKombinasiProduk.TBProduk._IsActive &&
                    item.IDTempat == Pengguna.IDTempat)
                .AsEnumerable()
                .Select(item => new
                {
                    item.IDStokProduk,
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    item.TBKombinasiProduk.TBProduk.Nama,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                    Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    item.HargaJual,
                    item.Jumlah,
                    DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore.ToFormatHarga() : "",
                    DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore.ToFormatHarga() : "",
                    DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi.ToFormatHarga() : "",
                    DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi.ToFormatHarga() : ""
                })
                .ToArray();
            RepeaterStokProduk.DataBind();
        }
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                //VALIDASI
                foreach (RepeaterItem item in RepeaterStokProduk.Items)
                {
                    Label LabelIDStokProduk = (Label)item.FindControl("LabelIDStokProduk");
                    TextBox TextBoxDiscountStorePersentase = (TextBox)item.FindControl("TextBoxDiscountStorePersentase");
                    TextBox TextBoxDiscountStoreNominal = (TextBox)item.FindControl("TextBoxDiscountStoreNominal");
                    TextBox TextBoxDiscountConsignmentPersentase = (TextBox)item.FindControl("TextBoxDiscountConsignmentPersentase");
                    TextBox TextBoxDiscountConsignmentNominal = (TextBox)item.FindControl("TextBoxDiscountConsignmentNominal");

                    if (!string.IsNullOrWhiteSpace(TextBoxDiscountStorePersentase.Text) && !string.IsNullOrWhiteSpace(TextBoxDiscountStoreNominal.Text))
                        WMSError.Pesan("Discount Store hanya boleh memilih antara Discount Persentase atau Nominal");

                    if (!string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentPersentase.Text) && !string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentNominal.Text))
                        WMSError.Pesan("Discount Consignment hanya boleh memilih antara Discount Persentase atau Nominal");

                    var StokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == LabelIDStokProduk.Text.ToInt());

                    if (!string.IsNullOrWhiteSpace(TextBoxDiscountStorePersentase.Text) && TextBoxDiscountStorePersentase.Text != "0")
                    {
                        StokProduk.EnumDiscountStore = (int)EnumDiscount.Persentase;
                        StokProduk.DiscountStore = TextBoxDiscountStorePersentase.Text.ToDecimal();
                    }
                    else if (!string.IsNullOrWhiteSpace(TextBoxDiscountStoreNominal.Text) && TextBoxDiscountStoreNominal.Text != "0")
                    {
                        StokProduk.EnumDiscountStore = (int)EnumDiscount.Nominal;
                        StokProduk.DiscountStore = TextBoxDiscountStoreNominal.Text.ToDecimal();
                    }
                    else
                    {
                        StokProduk.EnumDiscountStore = (int)EnumDiscount.TidakAda;
                        StokProduk.DiscountStore = 0;
                    }

                    if (!string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentPersentase.Text) && TextBoxDiscountConsignmentPersentase.Text != "0")
                    {
                        StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.Persentase;
                        StokProduk.DiscountKonsinyasi = TextBoxDiscountConsignmentPersentase.Text.ToDecimal();
                    }
                    else if (!string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentNominal.Text) && TextBoxDiscountConsignmentNominal.Text != "0")
                    {
                        StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.Nominal;
                        StokProduk.DiscountKonsinyasi = TextBoxDiscountConsignmentNominal.Text.ToDecimal();
                    }
                    else
                    {
                        StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.TidakAda;
                        StokProduk.DiscountKonsinyasi = 0;
                    }
                }

                db.SubmitChanges();

                LoadData();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}