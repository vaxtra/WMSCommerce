using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Top_ProdukVarianPrint : System.Web.UI.Page
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
                LabelCariIDTransaksi.Text = string.IsNullOrEmpty(Request.QueryString["IDTransaksi"]) ? string.Empty : '"' + Request.QueryString["IDTransaksi"] + '"';
                LabelCariPenggunaTransaksi.Text = Request.QueryString["IDPenggunaTransaksi"] == "0" ? "Semua" : db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["IDPenggunaTransaksi"].ToInt()).NamaLengkap;
                LabelCariPenggunaUpdate.Text = Request.QueryString["IDPenggunaUpdate"] == "0" ? "Semua" : db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["IDPenggunaUpdate"].ToInt()).NamaLengkap;
                LabelCariTempat.Text = Request.QueryString["IDTempat"] == "0" ? "Semua" : db.TBTempats.FirstOrDefault(item => item.IDTempat == Request.QueryString["IDTempat"].ToInt()).Nama;
                LabelCariJenisTransaksi.Text = Request.QueryString["IDJenisTransaksi"] == "0" ? "Semua" : db.TBJenisTransaksis.FirstOrDefault(item => item.IDJenisTransaksi == Request.QueryString["IDJenisTransaksi"].ToInt()).Nama;
                LabelCariStatusTransaksi.Text = Request.QueryString["IDStatusTransaksi"] == "0" ? "Semua" : db.TBStatusTransaksis.FirstOrDefault(item => item.IDStatusTransaksi == Request.QueryString["IDStatusTransaksi"].ToInt()).Nama;
                LabelCariPelanggan.Text = Request.QueryString["IDPelanggan"] == "0" ? "Semua" : db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == Request.QueryString["IDPelanggan"].ToInt()).NamaLengkap;
                LabelCariMeja.Text = Request.QueryString["IDMeja"] == "0" ? "Semua" : db.TBMejas.FirstOrDefault(item => item.IDMeja == Request.QueryString["IDMeja"].ToInt()).Nama;

                Laporan_Class Laporan_Class = new Laporan_Class(db, Pengguna, Request.QueryString["TanggalAwal"].ToDateTime(), Request.QueryString["TanggalAkhir"].ToDateTime(), false);

                var Result = Laporan_Class.Transaksi(Request.QueryString["IDTransaksi"], Request.QueryString["IDPenggunaTransaksi"].ToInt(), Request.QueryString["IDPenggunaUpdate"].ToInt(), Request.QueryString["IDTempat"].ToInt(), Request.QueryString["IDJenisTransaksi"].ToInt(), Request.QueryString["IDStatusTransaksi"].ToInt(), Request.QueryString["IDPelanggan"].ToInt(), Request.QueryString["IDMeja"].ToInt());

                #region USER INTERFACE LAPORAN
                LabelJumlahProduk.Text = Result["JumlahProduk"];
                LabelJumlahTamu.Text = Result["JumlahTamu"];
                LabelJumlahBiayaTambahan1.Text = Result["BiayaTambahan1"];
                LabelJumlahBiayaTambahan2.Text = Result["BiayaTambahan2"];
                LabelJumlahBiayaTambahan3.Text = Result["BiayaTambahan3"];
                LabelJumlahBiayaTambahan4.Text = Result["BiayaTambahan4"];
                LabelJumlahBiayaPengiriman.Text = Result["BiayaPengiriman"];
                LabelDiscountTransaksi.Text = Result["DiscountTransaksi"];
                LabelDiscountProduk.Text = Result["DiscountProduk"];
                LabelDiscountVoucher.Text = Result["DiscountVoucher"];
                LabelPembulatan.Text = Result["Pembulatan"];

                LabelSubtotalSebelumDiscount.Text = Result["SubtotalSebelumDiscount"];
                LabelSubtotalSetelahDiscount.Text = Result["SubtotalSetelahDiscount"];
                LabelGrandTotal.Text = Result["GrandTotal"];

                LabelJumlahProduk1.Text = LabelJumlahProduk.Text;
                LabelJumlahTamu1.Text = LabelJumlahTamu.Text;
                LabelJumlahBiayaTambahan11.Text = LabelJumlahBiayaTambahan1.Text;
                LabelJumlahBiayaTambahan21.Text = LabelJumlahBiayaTambahan2.Text;
                LabelJumlahBiayaTambahan31.Text = LabelJumlahBiayaTambahan3.Text;
                LabelJumlahBiayaTambahan41.Text = LabelJumlahBiayaTambahan4.Text;
                LabelJumlahBiayaPengiriman1.Text = LabelJumlahBiayaPengiriman.Text;
                LabelDiscountTransaksi1.Text = LabelDiscountTransaksi.Text;
                LabelDiscountProduk1.Text = LabelDiscountProduk.Text;
                LabelDiscountVoucher1.Text = LabelDiscountVoucher.Text;
                LabelPembulatan1.Text = LabelPembulatan.Text;

                LabelSubtotalSebelumDiscount1.Text = LabelSubtotalSebelumDiscount.Text;
                LabelSubtotalSetelahDiscount1.Text = LabelSubtotalSetelahDiscount.Text;
                LabelGrandTotal1.Text = LabelGrandTotal.Text;

                LabelPeriode.Text = Laporan_Class.Periode;

                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();
                #endregion
            }

            LabelJudul.Text = "Transaksi";

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