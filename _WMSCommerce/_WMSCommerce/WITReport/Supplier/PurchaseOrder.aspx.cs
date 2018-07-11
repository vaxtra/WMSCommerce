using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Supplier_PurchaseOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);

                DropDownListCariTempatPurchaseOrder.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempatPurchaseOrder.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariPenggunaPurchaseOrder.Items.AddRange(ClassPengguna.DropDownList(true));

                TBSupplier[] daftarSupplier = db.TBSuppliers.OrderBy(item => item.Nama).ToArray();

                DropDownListCariSupplierPurchaseOrder.DataSource = daftarSupplier;
                DropDownListCariSupplierPurchaseOrder.DataValueField = "IDSupplier";
                DropDownListCariSupplierPurchaseOrder.DataTextField = "Nama";
                DropDownListCariSupplierPurchaseOrder.DataBind();
                DropDownListCariSupplierPurchaseOrder.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
            }

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
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            var Result = Laporan_Class.POProduksiBahanBaku(DropDownListCariTempatPurchaseOrder.SelectedValue.ToInt(), TextBoxCariIDPOProduksiBahanBakuPurchaseOrder.Text, 0, DropDownListCariSupplierPurchaseOrder.SelectedValue.ToInt(), DropDownListCariPenggunaPurchaseOrder.SelectedValue.ToInt(), TextBoxCariKeteranganPurchaseOrder.Text);

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            //LinkDownload.Visible = GenerateExcel;

            //if (LinkDownload.Visible)
            //    LinkDownload.HRef = Laporan_Class.LinkDownload;

            //ButtonPrintPurchaseOrder.OnClientClick = "return popitup('PurchaseOrderRawMaterialPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

            RepeaterPurchaseOrder.DataSource = Result["Data"];
            RepeaterPurchaseOrder.DataBind();
            LabelGrandTotalPurchaseOrderHeader.Text = Result["Grandtotal"];
            LabelGrandTotalPurchaseOrderFooter.Text = LabelGrandTotalPurchaseOrderHeader.Text;
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