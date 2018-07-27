using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Laporan_Transaksi_Transaksi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                Tempat_Class ClassTempat = new Tempat_Class(db);
                JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
                StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
                Meja_Class Meja_Class = new Meja_Class();

                DropDownListCariPenggunaTransaksi.Items.AddRange(ClassPengguna.DropDownList(true));
                DropDownListCariPenggunaUpdate.Items.AddRange(ClassPengguna.DropDownList(true));
                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariJenisTransaksi.Items.AddRange(ClassJenisTransaksi.DataDropDownList(db));
                DropDownListCariStatusTransaksi.Items.AddRange(StatusTransaksi_Class.DataDropDownList(db));
                ClassPelanggan.DropDownList(DropDownListCariPelanggan, true);
                DropDownListCariMeja.Items.AddRange(Meja_Class.DataDropDownList(db));

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                LoadData();
            }
        }
        else
            LinkDownload.Visible = false;
    }

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            var Result = Laporan_Class.Transaksi(TextBoxCariIDTransaksi.Text, DropDownListCariPenggunaTransaksi.SelectedValue.ToInt(), DropDownListCariPenggunaUpdate.SelectedValue.ToInt(), DropDownListCariTempat.SelectedValue.ToInt(), DropDownListCariJenisTransaksi.SelectedValue.ToInt(), DropDownListCariStatusTransaksi.SelectedValue.ToInt(), DropDownListCariPelanggan.SelectedValue.ToInt(), DropDownListCariMeja.SelectedValue.ToInt());

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            ButtonPrint.OnClientClick = "return popitup('DefaultPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

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

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();
            #endregion
        }
    }

    #region DEFAULT
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        GenerateExcel();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    private void LoadData()
    {
        LoadData(false);
    }
    private void GenerateExcel()
    {
        LoadData(true);
    }
}