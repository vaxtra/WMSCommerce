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
                Tanggal_Class Tanggal_Class = new Tanggal_Class();

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListJenisTransaksi.Items.AddRange(ClassJenisTransaksi.DataDropDownList(db));
                DropDownListJenisTransaksi.SelectedValue = ((int)EnumJenisTransaksi.PointOfSales).ToString();

                DropDownListTahun.Items.AddRange(Tanggal_Class.DropdownlistTahun());
                DropDownListTahun.SelectedValue = DateTime.Now.Year.ToString();

                LoadData();
            }
        }
        else
            LinkDownload.Visible = false;
    }
    private void LoadData(bool GenerateExcel)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PanelTahun.Visible = DropDownListJenisLaporan.SelectedValue != "3";

            DateTime TanggalAwal = new DateTime(DropDownListTahun.SelectedValue.ToInt(), 1, 1);
            DateTime TanggalAkhir = new DateTime(DropDownListTahun.SelectedValue.ToInt(), 12, DateTime.DaysInMonth(DropDownListTahun.SelectedValue.ToInt(), 12));

            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], TanggalAwal, TanggalAkhir, GenerateExcel);

            var Result = Laporan_Class.Ringkasan(DropDownListJenisLaporan.SelectedValue.ToInt(), DropDownListTempat.SelectedValue.ToInt(), DropDownListJenisTransaksi.SelectedValue.ToInt());

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Result["JenisLaporan"];

            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            ButtonPrint.OnClientClick = "return popitup('RingkasanPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

            #region USER INTERFACE LAPORAN
            LabelTamu.Text = Result["Tamu"];
            LabelQuantity.Text = Result["Quantity"];
            LabelPelanggan.Text = Result["Pelanggan"];
            LabelNonPelanggan.Text = Result["NonPelanggan"];
            LabelDiscount.Text = Result["Discount"];
            LabelNonDiscount.Text = Result["NonDiscount"];
            LabelPengiriman.Text = Result["Pengiriman"];
            LabelNonPengiriman.Text = Result["NonPengiriman"];
            LabelTransaksi.Text = Result["Transaksi"];
            LabelNominal.Text = Result["Nominal"];

            LabelTamu1.Text = LabelTamu.Text;
            LabelQuantity1.Text = LabelQuantity.Text;
            LabelPelanggan1.Text = LabelPelanggan.Text;
            LabelNonPelanggan1.Text = LabelNonPelanggan.Text;
            LabelDiscount1.Text = LabelDiscount.Text;
            LabelNonDiscount1.Text = LabelNonDiscount.Text;
            LabelPengiriman1.Text = LabelPengiriman.Text;
            LabelNonPengiriman1.Text = LabelNonPengiriman.Text;
            LabelTransaksi1.Text = LabelTransaksi.Text;
            LabelNominal1.Text = LabelNominal.Text;

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();
            #endregion
        }
    }
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