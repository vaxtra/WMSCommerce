using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_DiscountPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region FIND CONTROL
            Label LabelJudul = (Label)Page.Master.FindControl("LabelJudul");
            Label LabelSubJudul = (Label)Page.Master.FindControl("LabelSubJudul");
            Label LabelStoreTempat = (Label)Page.Master.FindControl("LabelStoreTempat");

            Label LabelPrintTanggal = (Label)Page.Master.FindControl("LabelPrintTanggal");
            Label LabelPrintPengguna = (Label)Page.Master.FindControl("LabelPrintPengguna");
            Label LabelPrintStoreTempat = (Label)Page.Master.FindControl("LabelPrintStoreTempat");

            HtmlGenericControl PanelPengirimHeader = (HtmlGenericControl)Page.Master.FindControl("PanelPengirimHeader");
            HtmlGenericControl PanelPengirimFooter = (HtmlGenericControl)Page.Master.FindControl("PanelPengirimFooter");

            Label LabelPengirimTempat = (Label)Page.Master.FindControl("LabelPengirimTempat");
            Label LabelPengirimPengguna = (Label)Page.Master.FindControl("LabelPengirimPengguna");
            Label LabelPengirimPengguna1 = (Label)Page.Master.FindControl("LabelPengirimPengguna1");
            Label LabelPengirimTanggal = (Label)Page.Master.FindControl("LabelPengirimTanggal");
            Label LabelPengirimAlamat = (Label)Page.Master.FindControl("LabelPengirimAlamat");
            Label LabelPengirimTelepon = (Label)Page.Master.FindControl("LabelPengirimTelepon");
            Label LabelPengirimEmail = (Label)Page.Master.FindControl("LabelPengirimEmail");

            HtmlGenericControl PanelKeterangan = (HtmlGenericControl)Page.Master.FindControl("PanelKeterangan");
            Label LabelPengirimKeterangan = (Label)Page.Master.FindControl("LabelPengirimKeterangan");

            HtmlGenericControl PanelPenerimaHeader = (HtmlGenericControl)Page.Master.FindControl("PanelPenerimaHeader");
            HtmlGenericControl PanelPenerimaFooter = (HtmlGenericControl)Page.Master.FindControl("PanelPenerimaFooter");

            Label LabelPenerimaTempat = (Label)Page.Master.FindControl("LabelPenerimaTempat");
            Label LabelPenerimaPengguna = (Label)Page.Master.FindControl("LabelPenerimaPengguna");
            Label LabelPenerimaPengguna1 = (Label)Page.Master.FindControl("LabelPenerimaPengguna1");
            Label LabelPenerimaTanggal = (Label)Page.Master.FindControl("LabelPenerimaTanggal");
            Label LabelPenerimaAlamat = (Label)Page.Master.FindControl("LabelPenerimaAlamat");
            Label LabelPenerimaTelepon = (Label)Page.Master.FindControl("LabelPenerimaTelepon");
            #endregion

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LabelCariKode.Text = string.IsNullOrEmpty(Request.QueryString["Kode"]) ? string.Empty : '"' + Request.QueryString["kode"] + '"';
                LabelCariPemilikProduk.Text = Request.QueryString["IDPemilikProduk"] == "0" ? "Semua" : db.TBPemilikProduks.FirstOrDefault(item => item.IDPemilikProduk == Request.QueryString["IDPemilikProduk"].ToInt()).Nama;
                LabelCariProduk.Text = Request.QueryString["IDProduk"] == "0" ? "Semua" : db.TBProduks.FirstOrDefault(item => item.IDProduk == Request.QueryString["IDProduk"].ToInt()).Nama;
                LabelCariAtributProduk.Text = Request.QueryString["IDAtributProduk"] == "0" ? "Semua" : db.TBAtributProduks.FirstOrDefault(item => item.IDAtributProduk == Request.QueryString["IDAtributProduk"].ToInt()).Nama;
                LabelCariKategori.Text = Request.QueryString["IDKategoriProduk"] == "0" ? "Semua" : db.TBKategoriProduks.FirstOrDefault(item => item.IDKategoriProduk == Request.QueryString["IDKategoriProduk"].ToInt()).Nama;

                TBStokProduk[] daftarStokProduk = db.TBStokProduks
                .Where(item =>
                    item.TBKombinasiProduk.TBProduk._IsActive &&
                    item.IDTempat == Request.QueryString["IDTempat"].ToInt()).ToArray();

                if (!string.IsNullOrEmpty(Request.QueryString["Kode"]))
                    daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(Request.QueryString["Kode"])).ToArray();

                if (Request.QueryString["IDPemilikProduk"] != "0")
                    daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == Request.QueryString["IDPemilikProduk"].ToInt()).ToArray();

                if (Request.QueryString["IDProduk"] != "0")
                    daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.IDProduk == Request.QueryString["IDProduk"].ToInt()).ToArray();

                if (Request.QueryString["IDAtributProduk"] != "0")
                    daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == Request.QueryString["IDAtributProduk"].ToInt()).ToArray();

                if (Request.QueryString["IDKategoriProduk"] != "0")
                    daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == Request.QueryString["IDKategoriProduk"].ToInt()) != null).ToArray();

                if (Request.QueryString["StatusDiskon"] == "Semua")
                {
                    RepeaterLaporan.DataSource = daftarStokProduk.AsEnumerable()
                        .Select(item => new
                        {
                            item.IDStokProduk,
                            item.IDKombinasiProduk,
                            item.TBKombinasiProduk.KodeKombinasiProduk,
                            item.TBKombinasiProduk.TBProduk.Nama,
                            Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                            Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                            Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                            Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                            item.HargaJual,
                            item.Jumlah,
                            DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore : 0,
                            DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore : 0,
                            DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi : 0,
                            DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi : 0,
                            SetelahDiskon = SetelahDiskon(item.HargaJual.Value, item.EnumDiscountStore, item.DiscountStore, item.EnumDiscountKonsinyasi, item.DiscountKonsinyasi)
                        }).OrderBy(item => item.Nama).ToArray();
                    RepeaterLaporan.DataBind();
                }
                else if (Request.QueryString["StatusDiskon"] == "Diskon")
                {
                    RepeaterLaporan.DataSource = daftarStokProduk.AsEnumerable()
                        .Select(item => new
                        {
                            item.IDStokProduk,
                            item.IDKombinasiProduk,
                            item.TBKombinasiProduk.KodeKombinasiProduk,
                            item.TBKombinasiProduk.TBProduk.Nama,
                            Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                            Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                            Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                            Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                            item.HargaJual,
                            item.Jumlah,
                            DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore : 0,
                            DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore : 0,
                            DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi : 0,
                            DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi : 0,
                            SetelahDiskon = SetelahDiskon(item.HargaJual.Value, item.EnumDiscountStore, item.DiscountStore, item.EnumDiscountKonsinyasi, item.DiscountKonsinyasi)
                        }).Where(item => item.HargaJual.Value != item.SetelahDiskon).OrderBy(item => item.Nama).ToArray();
                    RepeaterLaporan.DataBind();
                }
                else if (Request.QueryString["StatusDiskon"] == "TidakDiskon")
                {
                    RepeaterLaporan.DataSource = daftarStokProduk.AsEnumerable()
                        .Select(item => new
                        {
                            item.IDStokProduk,
                            item.IDKombinasiProduk,
                            item.TBKombinasiProduk.KodeKombinasiProduk,
                            item.TBKombinasiProduk.TBProduk.Nama,
                            Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                            Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                            Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                            Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                            item.HargaJual,
                            item.Jumlah,
                            DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore : 0,
                            DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore : 0,
                            DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi : 0,
                            DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi : 0,
                            SetelahDiskon = SetelahDiskon(item.HargaJual.Value, item.EnumDiscountStore, item.DiscountStore, item.EnumDiscountKonsinyasi, item.DiscountKonsinyasi)
                        }).Where(item => item.HargaJual.Value == item.SetelahDiskon).OrderBy(item => item.Nama).ToArray();
                    RepeaterLaporan.DataBind();
                }

                LabelJudul.Text = "PRODUCT DISCOUNT";
                LabelSubJudul.Text = "";

                Title = LabelJudul.Text + " " + LabelSubJudul.Text;

                LabelStoreTempat.Text = db.TBTempats.FirstOrDefault(item => item.IDTempat == Request.QueryString["IDTempat"].ToInt()).Nama;

                LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();

                LabelPrintPengguna.Text = Pengguna.NamaLengkap;
                LabelPrintStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

                PanelPengirimHeader.Visible = false;
                PanelPengirimFooter.Visible = false;

                //LabelPengirimTempat.Text
                //LabelPengirimPengguna.Text
                //LabelPengirimPengguna1.Text = LabelPengirimPengguna.Text;
                //LabelPengirimTanggal.Text
                //LabelPengirimAlamat.Text
                //LabelPengirimTelepon.Text
                //LabelPengirimEmail.Text

                //PanelKeterangan.Visible
                //LabelPengirimKeterangan.Text

                PanelPenerimaHeader.Visible = false;
                PanelPenerimaFooter.Visible = false;

                //LabelPenerimaTempat.Text 
                //LabelPenerimaPengguna.Text
                //LabelPenerimaPengguna1.Text = LabelPenerimaPengguna.Text;
                //LabelPenerimaTanggal.Text
                //LabelPenerimaAlamat.Text
                //LabelPenerimaTelepon.Text
            }
        }
    }

    private decimal SetelahDiskon(decimal hargaJual, int enumDiscountStore, decimal discountStore, int enumDiscountKonsinyasi, decimal discountKonsinyasi)
    {
        decimal DiscountStore = 0;

        switch ((EnumDiscount)enumDiscountStore)
        {
            case EnumDiscount.Persentase:
                DiscountStore = (hargaJual * discountStore / 100);
                break;
            case EnumDiscount.Nominal:
                DiscountStore = discountStore;
                break;
        }

        decimal DiscountKonsinyasi = 0;

        switch ((EnumDiscount)enumDiscountKonsinyasi)
        {
            case EnumDiscount.Persentase:
                DiscountKonsinyasi = (hargaJual * discountKonsinyasi / 100);
                break;
            case EnumDiscount.Nominal:
                DiscountKonsinyasi = discountKonsinyasi;
                break;
        }

        return hargaJual - (DiscountStore + DiscountKonsinyasi);
    }
}