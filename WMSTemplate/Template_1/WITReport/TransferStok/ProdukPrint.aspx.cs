using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_TransferStok_ProdukPrint : System.Web.UI.Page
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
                LabelCariIDTransfer.Text = string.IsNullOrEmpty(Request.QueryString["IDTransfer"]) ? string.Empty : '"' + Request.QueryString["kode"] + '"';
                LabelCariTempatPengirim.Text = Request.QueryString["IDTempatPengirim"] == "0" ? "Semua" : db.TBTempats.FirstOrDefault(item => item.IDTempat == Request.QueryString["IDTempatPengirim"].ToInt()).Nama;
                LabelCariPengirim.Text = Request.QueryString["IDPengirim"] == "0" ? "Semua" : db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["IDPengirim"].ToInt()).NamaLengkap;
                LabelCariTempatPenerima.Text = Request.QueryString["IDTempatPenerima"] == "0" ? "Semua" : db.TBTempats.FirstOrDefault(item => item.IDTempat == Request.QueryString["IDTempatPenerima"].ToInt()).Nama;
                LabelCariPenerima.Text = Request.QueryString["IDPenerima"] == "0" ? "Semua" : db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["IDPenerima"].ToInt()).NamaLengkap;
                LabelCariStatusTransfer.Text = Request.QueryString["EnumStatusTransfer"] == "0" ? "Semua" : StatusTransfer(Request.QueryString["EnumStatusTransfer"].ToInt());
                LabelCariKeterangan.Text = string.IsNullOrEmpty(Request.QueryString["Keterangan"]) ? string.Empty : '"' + Request.QueryString["Keterangan"] + '"';

                Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], Request.QueryString["TanggalAwal"].ToDateTime(), Request.QueryString["TanggalAkhir"].ToDateTime(), false);

                var Result = Laporan_Class.TransferProduk(Request.QueryString["IDTransfer"], Request.QueryString["IDTempatPengirim"].ToInt(), Request.QueryString["IDPengirim"].ToInt(), Request.QueryString["IDTempatPenerima"].ToInt(), Request.QueryString["IDPenerima"].ToInt(), Request.QueryString["EnumStatusTransfer"].ToInt(), Request.QueryString["Keterangan"]);

                #region USER INTERFACE LAPORAN
                LabelPeriode.Text = Laporan_Class.Periode;

                LabelTotalJumlahHeaderTransfer.Text = Result["Jumlah"];
                LabelTotalJumlahFooterTransfer.Text = LabelTotalJumlahHeaderTransfer.Text;

                LabelTotalGrandtotalHeaderTransfer.Text = Result["GrandTotalHargaJual"];
                LabelTotalGrandtotalFooterTransfer.Text = LabelTotalGrandtotalHeaderTransfer.Text;

                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();
                #endregion
            }

            LabelJudul.Text = "Transfer Produk";

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

    private string StatusTransfer(int status)
    {
        switch (status)
        {
            case 1: return "TransferProses";
            case 2: return "PermintaanProses";
            case 3: return "TransferBatal";
            case 4: return "PermintaanBatal";
            case 5: return "TransferPending";
            case 6: return "PermintaanPending";
            case 7: return "TransferSelesai";
            case 8: return "PermintaanSelesai";
            default: return string.Empty;
        }
    }
}