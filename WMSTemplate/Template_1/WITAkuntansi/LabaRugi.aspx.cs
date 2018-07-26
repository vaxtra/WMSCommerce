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

public partial class WITAkuntansi_LabaRugi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownListBulan.Items.Clear();
            DropDownListBulan.Items.AddRange(Akuntansi_Class.DropdownlistBulanLaporan());
            DropDownListBulan.SelectedValue = DateTime.Now.Month.ToString();

            DropDownListTahun.Items.Clear();
            DropDownListTahun.Items.AddRange(Akuntansi_Class.DropdownlistTahunLaporan());
            DropDownListTahun.SelectedValue = DateTime.Now.Year.ToString();

            LoadData();

            TextBoxTanggalPeriode1.Text = DateTime.Now.Date.ToString("dd MMMM yyyy");
            TextBoxTanggalPeriode2.Text = DateTime.Now.Date.ToString("dd MMMM yyyy");
        }
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        var _result = Akuntansi_Class.LaporanLabaRugi(DropDownListBulan.SelectedValue, DropDownListTahun.SelectedValue, false, Pengguna, "LabaRugi");

        #region MOD TEST
        LabelPenjualan.Text = _result["NamaAkunPenjualan"];
        LabelNominalPenjualan.Text = Parse.ToFormatHarga(_result["NominalAkunPenjualan"]);
        LabelCOGS.Text = _result["NamaAkunCOGS"];
        LabelNominalCOGS.Text = Parse.ToFormatHarga(_result["NominalCOGS"]);
        LabelNominalGrossProfit.Text = Parse.ToFormatHarga(_result["NominalGrossProfit"]);
        LabelTotalOPEX.Text = Parse.ToFormatHarga(_result["NominalOPEX"]);
        LabelNominalEBIT.Text = Parse.ToFormatHarga(_result["NominalEBIT"]);
        #endregion

        RepeaterPemasukan.DataSource = _result["Pemasukan"];
        RepeaterPemasukan.DataBind();

        RepeaterPengeluaran.DataSource = _result["Pengeluaran"];
        RepeaterPengeluaran.DataBind();

        RepeaterPengeluaranTax.DataSource = _result["PengeluaranTax"];
        RepeaterPengeluaranTax.DataBind();

        var NetIncome = _result["NominalNetIncome"];

        if (NetIncome >= 0)
        {
            PanelProfit.Visible = true;
            PanelLoss.Visible = false;
            LabelNetIncomeProfit.Text = Parse.ToFormatHarga(NetIncome);
        }
        else
        {
            PanelProfit.Visible = false;
            PanelLoss.Visible = true;
            LabelNetIncomeLoss.Text = Parse.ToFormatHarga(NetIncome);
        }

        ButtonPrint.OnClientClick = "return popitup('LabaRugiPrint.aspx" + "?Bulan="+ DropDownListBulan.SelectedValue + "&Tahun="+ DropDownListTahun.SelectedValue + "')";

    }
    private void LoadData2(string _tgl1, string _tgl2)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        var _result = Akuntansi_Class.LaporanLabaRugi(false, Pengguna, "LabaRugi",_tgl1, _tgl2);

        #region MOD TEST
        LabelPenjualan.Text = _result["NamaAkunPenjualan"];
        LabelNominalPenjualan.Text = (_result["NominalAkunPenjualan"]).ToFormatHarga();
        LabelCOGS.Text = _result["NamaAkunCOGS"];
        LabelNominalCOGS.Text = (_result["NominalCOGS"]).ToFormatHarga();
        LabelNominalGrossProfit.Text = (_result["NominalGrossProfit"]).ToFormatHarga();
        LabelTotalOPEX.Text = (_result["NominalOPEX"]).ToFormatHarga();
        LabelNominalEBIT.Text = (_result["NominalEBIT"]).ToFormatHarga();
        #endregion

        RepeaterPemasukan.DataSource = _result["Pemasukan"];
        RepeaterPemasukan.DataBind();

        RepeaterPengeluaran.DataSource = _result["Pengeluaran"];
        RepeaterPengeluaran.DataBind();

        RepeaterPengeluaranTax.DataSource = _result["PengeluaranTax"];
        RepeaterPengeluaranTax.DataBind();

        var NetIncome = _result["NominalNetIncome"];

        if (NetIncome >= 0)
        {
            PanelProfit.Visible = true;
            PanelLoss.Visible = false;
            LabelNetIncomeProfit.Text = (NetIncome).ToFormatHarga();
        }
        else
        {
            PanelProfit.Visible = false;
            PanelLoss.Visible = true;
            LabelNetIncomeLoss.Text = (NetIncome).ToFormatHarga();
        }


        ButtonPrint2.OnClientClick = "return popitup('LabaRugiPrint.aspx" + "?Periode1=" + TextBoxTanggalPeriode1.Text + "&Periode2=" + TextBoxTanggalPeriode1.Text + "')";

    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            var _result = Akuntansi_Class.LaporanLabaRugi(DropDownListBulan.SelectedValue, DropDownListTahun.SelectedValue, true, Pengguna,"LabaRugi");
            LinkDownload.Visible = true;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Akuntansi_Class.LinkDownload;
        }
    }

    protected void ButtonCari2_Click(object sender, EventArgs e)
    {
        LoadData2(TextBoxTanggalPeriode1.Text,TextBoxTanggalPeriode2.Text);
    }
}