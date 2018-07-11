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
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();
                DropDownListBrand.Items.AddRange(ClassPemilikProduk.Dropdownlist());

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");
            }

            LoadData();
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

            var Result = Laporan_Class.Consignment(DropDownListTempat.SelectedValue.ToInt(), DropDownListBrand.SelectedValue.ToInt());

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();

            LabelPeriode.Text = Laporan_Class.Periode;

            LabelStok.Text = Parse.ToFormatHargaBulat(Result["StockQuantity"]);
            LabelNominalStok.Text = Parse.ToFormatHarga(Result["StockNominal"]);
            LabelQuantity.Text = Parse.ToFormatHargaBulat(Result["SalesQuantity"]);
            LabelBeforeDiscount.Text = Parse.ToFormatHarga(Result["SalesBeforeDiscount"]);
            LabelDiscount.Text = Parse.ToFormatHarga(Result["SalesDiscount"]);
            LabelSubtotal.Text = Parse.ToFormatHarga(Result["SalesSubtotal"]);
            LabelConsignment.Text = Parse.ToFormatHarga(Result["SalesConsignment"]);
            LabelPayToBrand.Text = Parse.ToFormatHarga(Result["SalesPayToBrand"]);
            LabelTotalProduk.Text = Parse.ToFormatHargaBulat(Result["TotalProduk"]);

            LabelStok1.Text = LabelStok.Text;
            LabelNominalStok1.Text = LabelNominalStok.Text;
            LabelQuantity1.Text = LabelQuantity.Text;
            LabelBeforeDiscount1.Text = LabelBeforeDiscount.Text;
            LabelDiscount1.Text = LabelDiscount.Text;
            LabelSubtotal1.Text = LabelSubtotal.Text;
            LabelConsignment1.Text = LabelConsignment.Text;
            LabelPayToBrand1.Text = LabelPayToBrand.Text;
            LabelTotalProduk1.Text = LabelTotalProduk.Text;

            //FILE EXCEL
            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "return popitup('DefaultPrint.aspx" + Laporan_Class.TempPencarian + "')";
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
        DropDownListTempat.SelectedValue = "0";
        DropDownListBrand.SelectedValue = "0";

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