using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Stok_PengaturanPrint : System.Web.UI.Page
{
    public Dictionary<string, dynamic> Result { get; set; }
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
                Laporan_Class Laporan_Class = new Laporan_Class(db, Pengguna, Request.QueryString["TanggalAwal"].ToDateTime(), Request.QueryString["TanggalAkhir"].ToDateTime(), false);

                Result = Laporan_Class.StokBahanBaku_Class(Request.QueryString["do"], Request.QueryString["IDTempat"].ToInt(), Request.QueryString["JenisStokProduk"].ToInt(), Request.QueryString["BahanBaku"], Request.QueryString["IDSatuan"].ToInt(), Request.QueryString["SatuanBesar"].ToBool(), Request.QueryString["IDKategori"].ToInt(), Request.QueryString["Kode"], Request.QueryString["Harga"], Request.QueryString["Minimum"], Request.QueryString["Quantity"], Request.QueryString["Status"]);

                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();

                LabelJudul.Text = Result["Judul"];
                LabelSubJudul.Text = "";

                Title = LabelJudul.Text + " " + LabelSubJudul.Text;

                LabelStoreTempat.Text = Result["Tempat"];

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
}