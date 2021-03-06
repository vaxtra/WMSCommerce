﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Supplier_PenerimaanDetailPrint : System.Web.UI.Page
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
                LabelCariIDPenerimaanPOProduksiBahanBaku.Text = string.IsNullOrEmpty(Request.QueryString["IDPenerimaanPOProduksiBahanBaku"]) ? string.Empty : '"' + Request.QueryString["IDPenerimaanPOProduksiBahanBaku"] + '"';
                LabelCariIDPOProduksiBahanBaku.Text = string.IsNullOrEmpty(Request.QueryString["IDPOProduksiBahanBaku"]) ? string.Empty : '"' + Request.QueryString["IDPOProduksiBahanBaku"] + '"';
                LabelCariTempat.Text = Request.QueryString["IDTempat"] == "0" ? "Semua" : db.TBTempats.FirstOrDefault(item => item.IDTempat == Request.QueryString["IDTempat"].ToInt()).Nama;
                LabelCariSupplier.Text = Request.QueryString["IDSupplier"] == "0" ? "Semua" : db.TBVendors.FirstOrDefault(item => item.IDVendor == Request.QueryString["IDSupplier"].ToInt()).Nama;
                LabelCariPengguna.Text = Request.QueryString["IDPengguna"] == "0" ? "Semua" : db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["IDPengguna"].ToInt()).NamaLengkap;
                LabelCariKode.Text = string.IsNullOrEmpty(Request.QueryString["Kode"]) ? string.Empty : '"' + Request.QueryString["Kode"] + '"';
                LabelCariBahanBaku.Text = Request.QueryString["IDBahanBaku"] == "0" ? "Semua" : db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Request.QueryString["IDBahanBaku"].ToInt()).Nama;
                LabelCariSatuan.Text = Request.QueryString["IDSatuan"] == "0" ? "Semua" : db.TBSatuans.FirstOrDefault(item => item.IDSatuan == Request.QueryString["IDSatuan"].ToInt()).Nama;
                LabelCariKategori.Text = Request.QueryString["IDKategoriBahanBaku"] == "0" ? "Semua" : db.TBKategoriBahanBakus.FirstOrDefault(item => item.IDKategoriBahanBaku == Request.QueryString["IDKategoriBahanBaku"].ToInt()).Nama;

                Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], Request.QueryString["TanggalAwal"].ToDateTime(), Request.QueryString["TanggalAkhir"].ToDateTime(), false);

                var Result = Laporan_Class.PenerimaanPOProduksiBahanBakuDetail(Request.QueryString["IDTempat"].ToInt(), Request.QueryString["IDPenerimaanPOProduksiBahanBaku"], Request.QueryString["IDPOProduksiBahanBaku"], Request.QueryString["EnumJenisProduksi"].ToInt(), Request.QueryString["IDSupplier"].ToInt(), Request.QueryString["IDPengguna"].ToInt(), Request.QueryString["Kode"], Request.QueryString["IDBahanBaku"].ToInt(), Request.QueryString["IDSatuan"].ToInt(), Request.QueryString["IDKategoriBahanBaku"].ToInt(), false);

                #region USER INTERFACE LAPORAN
                LabelSubtotalHeader.Text = Result["Subtotal"];
                LabelSubtotalFooter.Text = LabelSubtotalHeader.Text;
                LabelPeriode.Text = Laporan_Class.Periode;

                RepeaterLaporan.DataSource = Result["Data"];
                RepeaterLaporan.DataBind();
                #endregion
            }

            LabelJudul.Text = "Penerimaan PO Produksi Produk (" + Pengaturan.JenisPOProduksi(Request.QueryString["EnumJenisProduksi"].ToInt(), "Produk") + ")";

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