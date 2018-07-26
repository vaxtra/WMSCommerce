﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Top_ProdukVarianPrint : System.Web.UI.Page
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

                List<int> ListIDTempat = new List<int>();
                List<int> ListIDJenisTransaksi = new List<int>();
                List<int> ListIDStatusTransaksi = new List<int>();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["IDTempat"]))
                    ListIDTempat = Request.QueryString["IDTempat"].Split(',').Select(int.Parse).ToList();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["IDJenisTransaksi"]))
                    ListIDJenisTransaksi = Request.QueryString["IDJenisTransaksi"].Split(',').Select(int.Parse).ToList();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["IDStatusTransaksi"]))
                    ListIDStatusTransaksi = Request.QueryString["IDStatusTransaksi"].Split(',').Select(int.Parse).ToList();

                Result = Laporan_Class.NetRevenue(ListIDTempat, ListIDJenisTransaksi, ListIDStatusTransaksi, Request.QueryString["TanggalAwal"].ToDateTime(), Request.QueryString["TanggalAkhir"].ToDateTime());

                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();

                RepeaterJenisPembayaran.DataSource = Result["DataJenisPembayaran"];
                RepeaterJenisPembayaran.DataBind();

                RepeaterRetur.DataSource = Result["DataRetur"];
                RepeaterRetur.DataBind();

                Title1COGS.Visible = Result["MelihatCOGS"];
                Title2COGS.Visible = Title1COGS.Visible;
                Title3COGS.Visible = Title1COGS.Visible;
                Title4COGS.Visible = Title1COGS.Visible;
                Footer1COGS.Visible = Title1COGS.Visible;
                Footer2COGS.Visible = Title1COGS.Visible;

                Title1GrossProfit.Visible = Title1COGS.Visible;
                Title2GrossProfit.Visible = Title1COGS.Visible;
                Title3GrossProfit.Visible = Title1COGS.Visible;
                Title4GrossProfit.Visible = Title1COGS.Visible;
                Footer1GrossProfit.Visible = Title1COGS.Visible;
                Footer2GrossProfit.Visible = Title1COGS.Visible;

                foreach (RepeaterItem item in RepeaterLaporan.Items)
                {
                    HtmlTableCell PanelCOGS = (HtmlTableCell)item.FindControl("PanelCOGS");
                    PanelCOGS.Visible = Title1COGS.Visible;
                }

                foreach (RepeaterItem item in RepeaterRetur.Items)
                {
                    HtmlTableCell PanelCOGS = (HtmlTableCell)item.FindControl("PanelCOGS");
                    PanelCOGS.Visible = Title1COGS.Visible;
                }

                LabelJudul.Text = "Net Revenue";
                LabelSubJudul.Text = Result["Periode"];

                Title = LabelJudul.Text + " " + LabelSubJudul.Text;

                LabelStoreTempat.Text = Result["Tempat"] + "<br/>" + Result["JenisTransaksi"] + "<br/>" + Result["StatusTransaksi"];

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