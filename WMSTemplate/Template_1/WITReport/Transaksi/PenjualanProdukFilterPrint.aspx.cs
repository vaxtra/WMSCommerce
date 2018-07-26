using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Transaksi_PenjualanProdukFilterPrint : System.Web.UI.Page
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

            Label LabelPeriode = (Label)Page.Master.FindControl("LabelPeriode");

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
                var _hasilDetailTransaksi = db.TBTransaksiDetails.ToArray();

                if (Request.QueryString["IDTempat"] != "0")
                {
                    _hasilDetailTransaksi = _hasilDetailTransaksi.Where(item => item.TBTransaksi.IDTempat == Request.QueryString["IDTempat"].ToInt()).ToArray();
                }

                if (Request.QueryString["IDStatusTransaksi"] != "0")
                {
                    _hasilDetailTransaksi = _hasilDetailTransaksi.Where(item => item.TBTransaksi.IDStatusTransaksi == Request.QueryString["IDStatusTransaksi"].ToInt()).ToArray();
                }

                var _dataDatabase = _hasilDetailTransaksi.Where(item => item.TBTransaksi.TanggalOperasional.Value.Date >= DateTime.Parse(Request.QueryString["TanggalAwal"]).Date && item.TBTransaksi.TanggalOperasional.Value.Date <= DateTime.Parse(Request.QueryString["TanggalAkhir"]).Date)
                    .ToArray();

                var _data = _dataDatabase.AsEnumerable().GroupBy(item => new
                {
                    item.TBKombinasiProduk
                })
                .Select(item => new
                {
                    KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                    PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                    IDPemilikProduk = item.Key.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                    IDProduk = item.Key.TBKombinasiProduk.TBProduk.IDProduk,
                    IDAtributProduk = item.Key.TBKombinasiProduk.IDAtributProduk,
                    RelasiKategori = item.Key.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.Key.TBKombinasiProduk),
                    JumlahProduk = item.Sum(item2 => item2.Quantity),
                    HargaPokok = item.Sum(item2 => item2.HargaBeli * item2.Quantity),
                    HargaJual = item.Sum(item2 => item2.HargaJual * item2.Quantity),
                    PotonganHargaJual = item.Sum(item2 => item2.Discount * item2.Quantity),
                    Subtotal = item.Sum(item2 => item2.Subtotal),
                    PenjualanBersih = item.Sum(item2 => (item2.HargaJual - item2.Discount - item2.HargaBeli) * item2.Quantity),
                });

                if (Request.QueryString["Filter"] == "brand")
                {
                    var hasil = db.TBPemilikProduks.AsEnumerable().Where(item => _data.Any(data => data.IDPemilikProduk == item.IDPemilikProduk)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk),
                        TotalJumlahProduk = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterLaporan.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterLaporan.DataBind();
                }
                else if (Request.QueryString["Filter"] == "produk")
                {
                    var hasil = db.TBProduks.AsEnumerable().Where(item => _data.Any(data => data.IDProduk == item.IDProduk)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.IDProduk == item.IDProduk),
                        TotalJumlahProduk = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterLaporan.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterLaporan.DataBind();
                }
                else if (Request.QueryString["Filter"] == "varian")
                {
                    var hasil = db.TBAtributProduks.AsEnumerable().Where(item => _data.Any(data => data.IDAtributProduk == item.IDAtributProduk)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk),
                        TotalJumlahProduk = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterLaporan.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterLaporan.DataBind();
                }
                else if (Request.QueryString["Filter"] == "kategori")
                {
                    var hasil = db.TBKategoriProduks.AsEnumerable().Where(item => _data.Any(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null),
                        TotalJumlahProduk = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterLaporan.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterLaporan.DataBind();
                }
            }

            if (DateTime.Parse(Request.QueryString["TanggalAwal"]).Date == DateTime.Parse(Request.QueryString["TanggalAkhir"]).Date)
                LabelPeriode.Text = Request.QueryString["TanggalAwal"].ToFormatTanggal();
            else
                LabelPeriode.Text = Request.QueryString["TanggalAwal"].ToFormatTanggal() + " - " + Request.QueryString["TanggalAkhir"].ToFormatTanggal();

            LabelJudul.Text = "Penjualan Produk";

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