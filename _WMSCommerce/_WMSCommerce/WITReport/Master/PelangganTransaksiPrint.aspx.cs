using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Master_PelangganTransaksiPrint : System.Web.UI.Page
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
                var transaksi = db.TBTransaksis.Where(item => item.TanggalOperasional.Value.Date >= Request.QueryString["TanggalAwal"].ToDateTime().Date && item.TanggalOperasional.Value.Date <= Request.QueryString["TanggalAkhir"].ToDateTime().Date && item.TBTransaksiDetails.Count > 0).Select(item => new
                {
                    item.IDTempat,
                    item.IDPelanggan,
                    item.IDJenisTransaksi,
                    item.IDStatusTransaksi,
                    item.IDTransaksi,
                    item.TanggalTransaksi,
                    item.TanggalOperasional,
                    item.TanggalPembayaran,
                    item.Keterangan,
                    CountProduk = item.TBTransaksiDetails.GroupBy(item2 => new
                    {
                        item2.TBKombinasiProduk
                    }).Select(item2 => new
                    {
                        Produk = item2.Key.TBKombinasiProduk.TBProduk.Nama,
                        AtributProduk = item2.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                        JumlahProduk = item2.Sum(x => x.Quantity > 0 ? x.Quantity : 0),
                        Retur = item2.Sum(x => x.Quantity < 0 ? x.Quantity : 0)
                    }).Count(),
                    Produk = item.TBTransaksiDetails.GroupBy(item2 => new
                    {
                        item2.TBKombinasiProduk
                    }).Select(item2 => new
                    {
                        Produk = item2.Key.TBKombinasiProduk.TBProduk.Nama,
                        AtributProduk = item2.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                        JumlahProduk = item2.Sum(x => x.Quantity > 0 ? x.Quantity : 0),
                        Retur = item2.Sum(x => x.Quantity < 0 ? x.Quantity : 0)
                    }).FirstOrDefault(),
                    Detail = item.TBTransaksiDetails.GroupBy(item2 => new
                    {
                        item2.TBKombinasiProduk
                    }).Select(item2 => new
                    {
                        Produk = item2.Key.TBKombinasiProduk.TBProduk.Nama,
                        AtributProduk = item2.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                        JumlahProduk = item2.Sum(x => x.Quantity > 0 ? x.Quantity : 0),
                        Retur = item2.Sum(x => x.Quantity < 0 ? x.Quantity : 0)
                    }).Skip(1),
                    item.GrandTotal,
                    item.TotalPembayaran,
                    Penagihan = item.GrandTotal - item.TotalPembayaran,
                    StatusTransaksi = item.TBStatusTransaksi.Nama
                }).OrderBy(item => item.TanggalTransaksi).ToArray();

                if (Request.QueryString["IDTempat"] != "0")
                    transaksi = transaksi.Where(item => item.IDTempat == Request.QueryString["IDTempat"].ToInt()).ToArray();

                if (Request.QueryString["IDPelanggan"] != "0")
                    transaksi = transaksi.Where(item => item.IDPelanggan == Request.QueryString["IDPelanggan"].ToInt()).ToArray();

                if (Request.QueryString["IDJenisTransaksi"] != "0")
                    transaksi = transaksi.Where(item => item.IDJenisTransaksi == Request.QueryString["IDJenisTransaksi"].ToInt()).ToArray();

                LabelNamaPelanggan.Text = db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == Request.QueryString["IDPelanggan"].ToInt()).NamaLengkap;

                #region USER INTERFACE LAPORAN
                if (Request.QueryString["TanggalAwal"].ToDateTime().Date == Request.QueryString["TanggalAkhir"].ToDateTime().Date)
                    LabelPeriode.Text = Request.QueryString["TanggalAwal"].ToFormatTanggal();
                else
                    LabelPeriode.Text = Request.QueryString["TanggalAwal"].ToFormatTanggal() + " - " + Request.QueryString["TanggalAkhir"].ToFormatTanggal();

                RepeaterLaporan.DataSource = transaksi;
                RepeaterLaporan.DataBind();
                #endregion
            }

            LabelJudul.Text = "Transaksi Pelanggan";

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