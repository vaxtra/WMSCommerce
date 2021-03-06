﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Proyeksi_ProyeksiDetailPrint : System.Web.UI.Page
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
                LabelCariKode.Text = string.IsNullOrEmpty(Request.QueryString["Kode"]) ? string.Empty : '"' + Request.QueryString["Kode"] + '"';
                LabelCariPemilikProduk.Text = Request.QueryString["IDPemilikProduk"] == "0" ? "Semua" : db.TBPemilikProduks.FirstOrDefault(item => item.IDPemilikProduk == Request.QueryString["IDPemilikProduk"].ToInt()).Nama;
                LabelCariProduk.Text = Request.QueryString["IDProduk"] == "0" ? "Semua" : db.TBProduks.FirstOrDefault(item => item.IDProduk == Request.QueryString["IDProduk"].ToInt()).Nama;
                LabelCariAtributProduk.Text = Request.QueryString["IDAtributProduk"] == "0" ? "Semua" : db.TBAtributProduks.FirstOrDefault(item => item.IDAtributProduk == Request.QueryString["IDAtributProduk"].ToInt()).Nama;
                LabelCariKategori.Text = Request.QueryString["IDKategoriProduk"] == "0" ? "Semua" : db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["IDKategoriProduk"].ToInt()).NamaLengkap;

                Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], DateTime.Parse(Request.QueryString["TanggalAwal"]), DateTime.Parse(Request.QueryString["TanggalAkhir"]), false);

                var Result = Laporan_Class.ProyeksiDetail(Request.QueryString["IDTempat"].ToInt(), null, 0, Request.QueryString["EnumStatusProyeksi"].ToInt(), Request.QueryString["Kode"], Request.QueryString["IDPemilikProduk"].ToInt(), Request.QueryString["IDProduk"].ToInt(), Request.QueryString["IDAtributProduk"].ToInt(), Request.QueryString["IDKategoriProduk"].ToInt(), true);

                #region USER INTERFACE LAPORAN
                LabelPeriode.Text = Laporan_Class.Periode;
                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();
                #endregion

                LabelTotalJumlahHeaderProyeksiDetail.Text = Result["Jumlah"];
                LabelTotalJumlahFooterProyeksiDetail.Text = Result["Jumlah"];

                LabelJudul.Text = "Proyeksi Detail";

                LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();
                LabelPrintPengguna.Text = Pengguna.NamaLengkap;
                LabelPrintStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

            }

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