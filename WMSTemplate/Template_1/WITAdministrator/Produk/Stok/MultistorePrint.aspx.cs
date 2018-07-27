using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Stok_MultiStorePrint : System.Web.UI.Page
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
                Laporan_Class Laporan_Class = new Laporan_Class(db, Pengguna, DateTime.Now, DateTime.Now, false);

                Result = Laporan_Class.StokMultistore(Request.QueryString["IDJenisStokProduk"].ToInt(), Request.QueryString["IDKategoriTempat"].ToInt(), Request.QueryString["Kode"], Request.QueryString["IDProduk"].ToInt(), Request.QueryString["IDAtributProduk"].ToInt(), Request.QueryString["IDPemilikProduk"].ToInt(), Request.QueryString["IDKategoriProduk"].ToInt());

                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();

                RepeaterTempat.DataSource = Result["Tempat"];
                RepeaterTempat.DataBind();

                RepeaterTotalTempat1.DataSource = Result["Tempat"];
                RepeaterTotalTempat1.DataBind();

                RepeaterTotalTempat2.DataSource = Result["Tempat"];
                RepeaterTotalTempat2.DataBind();

                LabelJudul.Text = "Stock Multistore";
                LabelSubJudul.Text = Result["JenisStokProduk"];

                Title = LabelJudul.Text + " " + LabelSubJudul.Text;

                //LabelStoreTempat.Text

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