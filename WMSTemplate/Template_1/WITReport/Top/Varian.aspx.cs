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
                JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListJenisTransaksi.Items.AddRange(ClassJenisTransaksi.DataDropDownList(db));
                DropDownListJenisTransaksi.SelectedValue = "1";

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                PanelTabel.Visible = true;
                PanelChart.Visible = false;

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

        //MEMBUAT OBJECT LAPORAN
        LaporanTop_Class LaporanTop_Class = new LaporanTop_Class((PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], DropDownListTempat.SelectedValue.ToInt(), DropDownListJenisTransaksi.SelectedValue.ToInt(), DropDownListOrderBy.SelectedValue.ToInt(), GenerateExcel, PanelChart.Visible);

        //GENERATE LAPORAN
        var ResultTransaksi = LaporanTop_Class.TopVarian();

        if (PanelTabel.Visible)
        {
            //MENAMPILKAN LAPORAN KE REPEATER
            RepeaterLaporan.DataSource = ResultTransaksi;
            RepeaterLaporan.DataBind();

            PanelChart.Visible = false;
        }
        else if (PanelChart.Visible)
        {
            //MENAMPILKAN LAPORAN KE CHART
            //Literal LiteralChart = (Literal)Page.Master.FindControl("LiteralChart");
            LiteralChart.Text = string.Empty;

            int Height = LaporanTop_Class.JumlahData * 30;
            container.Attributes.Add("style", "width: auto; height: " + (Height > 250 ? Height : 250) + "px; margin: 0 auto;");

            LiteralChart.Text = LaporanTop_Class.JavascriptChart;

            PanelTabel.Visible = false;
        }

        //KETERANGAN LAPORAN
        LabelPeriode.Text = LaporanTop_Class.Periode;

        LabelQuantity.Text = LaporanTop_Class.TotalQuantity.ToFormatHargaBulat();
        LabelTotalDiscount.Text = LaporanTop_Class.TotalDiscount.ToFormatHarga();
        LabelTotalPenjualan.Text = LaporanTop_Class.TotalPenjualan.ToFormatHarga();

        LabelQuantity1.Text = LabelQuantity.Text;
        LabelTotalDiscount1.Text = LabelTotalDiscount.Text;
        LabelTotalPenjualan1.Text = LabelTotalPenjualan.Text;

        //FILE EXCEL
        LinkDownload.Visible = GenerateExcel;

        if (LinkDownload.Visible)
            LinkDownload.HRef = LaporanTop_Class.LinkDownload;

        //PRINT LAPORAN
        ButtonPrint.OnClientClick = "return popitup('VarianPrint.aspx" + LaporanTop_Class.TempPencarian + "')";
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

    protected void ButtonChart_Click(object sender, EventArgs e)
    {
        PanelTabel.Visible = false;
        PanelChart.Visible = true;

        LoadData();
    }
    protected void ButtonTabel_Click(object sender, EventArgs e)
    {
        PanelTabel.Visible = true;
        PanelChart.Visible = false;

        LoadData();
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void RepeaterLaporan_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (DropDownListOrderBy.SelectedValue == "1")
        {
            //Quantity
            HtmlTableCell quantity = (HtmlTableCell)e.Item.FindControl("quantity");
            quantity.Attributes.Add("class", "text-right warning");
        }
        else if (DropDownListOrderBy.SelectedValue == "2")
        {
            //Discount
            HtmlTableCell totalDiscount = (HtmlTableCell)e.Item.FindControl("totalDiscount");
            totalDiscount.Attributes.Add("class", "text-right warning");
        }
        else if (DropDownListOrderBy.SelectedValue == "3")
        {
            //Penjualan
            HtmlTableCell totalPenjualan = (HtmlTableCell)e.Item.FindControl("totalPenjualan");
            totalPenjualan.Attributes.Add("class", "text-right warning");
        }
        else
        {
            //Quantity
            HtmlTableCell quantity = (HtmlTableCell)e.Item.FindControl("quantity");
            quantity.Attributes.Add("class", "text-right warning");
            //Penjualan
            HtmlTableCell totalPenjualan = (HtmlTableCell)e.Item.FindControl("totalPenjualan");
            totalPenjualan.Attributes.Add("class", "text-right warning");
        }
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        GenerateExcel();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        DropDownListTempat.SelectedValue = "0";
        DropDownListJenisTransaksi.SelectedValue = "0";
        DropDownListOrderBy.SelectedValue = "1";

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