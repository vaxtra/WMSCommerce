using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Supplier_SisaPO : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBSupplier[] daftarSupplier = db.TBSuppliers.ToArray();

                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListCariTempatPurchaseOrder.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempatPurchaseOrder.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariSupplierPurchaseOrder.DataSource = daftarSupplier;
                DropDownListCariSupplierPurchaseOrder.DataTextField = "Nama";
                DropDownListCariSupplierPurchaseOrder.DataValueField = "IDSupplier";
                DropDownListCariSupplierPurchaseOrder.DataBind();
                DropDownListCariSupplierPurchaseOrder.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });
            }

            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            LoadData();
        }
    }

    #region Purchase Order
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

    private void LoadData(bool GenerateExcel)
    {
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            var Result = Laporan_Class.SisaPOProduksiBahanBaku(DropDownListCariTempatPurchaseOrder.SelectedValue.ToInt(), DropDownListCariSupplierPurchaseOrder.SelectedValue.ToInt(), TextBoxCariIDPOProduksiProdukPurchaseOrder.Text, 0, DropDownListCariStatusSisaPurchaseOrder.SelectedValue);

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            //LinkDownloadPurchaseOrder.Visible = GenerateExcel;

            //if (LinkDownloadPurchaseOrder.Visible)
            //    LinkDownloadPurchaseOrder.HRef = Laporan_Class.LinkDownload;

            //ButtonPrintPurchaseOrder.OnClientClick = "return popitup('SisaPOPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

            RepeaterPurchaseOrder.DataSource = Result["Data"];
            RepeaterPurchaseOrder.DataBind();

            LabelSubtotalHeaderPurchaseOrder.Text = Result["Subtotal"];
            LabelSubtotalFooterPurchaseOrder.Text = LabelSubtotalHeaderPurchaseOrder.Text;
        }
    }

    private void LoadData()
    {
        LoadData(false);
    }
    protected void ButtonExcelPurchaseOrder_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
    protected void LoadData_EventPurchaseOrder(object sender, EventArgs e)
    {
        LoadData();
    }
    #endregion
}