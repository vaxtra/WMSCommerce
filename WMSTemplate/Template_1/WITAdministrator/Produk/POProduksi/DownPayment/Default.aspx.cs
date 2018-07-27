using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_DownPayment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariBulanPO.SelectedValue = DateTime.Now.Month.ToString();
                DropDownListCariTahunPO.Items.Insert(0, new ListItem { Value = (DateTime.Now.Year - 3).ToString(), Text = (DateTime.Now.Year - 3).ToString() });
                DropDownListCariTahunPO.Items.Insert(1, new ListItem { Value = (DateTime.Now.Year - 2).ToString(), Text = (DateTime.Now.Year - 2).ToString() });
                DropDownListCariTahunPO.Items.Insert(2, new ListItem { Value = (DateTime.Now.Year - 1).ToString(), Text = (DateTime.Now.Year - 1).ToString() });
                DropDownListCariTahunPO.Items.Insert(3, new ListItem { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
                DropDownListCariTahunPO.SelectedValue = DateTime.Now.Year.ToString();

                DropDownListCariPegawaiPO.DataSource = db.TBPenggunas.ToArray();
                DropDownListCariPegawaiPO.DataTextField = "NamaLengkap";
                DropDownListCariPegawaiPO.DataValueField = "IDPengguna";
                DropDownListCariPegawaiPO.DataBind();
                DropDownListCariPegawaiPO.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariVendorPO.DataSource = db.TBVendors.ToArray();
                DropDownListCariVendorPO.DataTextField = "Nama";
                DropDownListCariVendorPO.DataValueField = "IDVendor";
                DropDownListCariVendorPO.DataBind();
                DropDownListCariVendorPO.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadDataPO(db);
            }
        }
    }
    #region PO
    private void LoadDataPO(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPO.DataSource = db.TBPOProduksiProduks.Where(item => item.IDTempat == pengguna.IDTempat && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder &&
            (!string.IsNullOrWhiteSpace(TextBoxCariIDPOProduksiProduk.Text) ? item.IDPOProduksiProduk.Contains(TextBoxCariIDPOProduksiProduk.Text.ToUpper()) : true) &&
            (DropDownListCariBulanPO.SelectedValue != "0" ? item.Tanggal.Month == DropDownListCariBulanPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariTahunPO.SelectedValue != "0" ? item.Tanggal.Year == DropDownListCariTahunPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariPegawaiPO.SelectedValue != "0" ? item.IDPengguna == DropDownListCariPegawaiPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariVendorPO.SelectedValue != "0" ? item.IDVendor == DropDownListCariVendorPO.SelectedValue.ToInt() : true))
            .Select(item => new
            {
                item.IDPOProduksiProduk,
                item.IDProyeksi,
                item.Nomor,
                item.Tanggal,
                Pegawai = item.TBPengguna.NamaLengkap,
                Vendor = item.TBVendor.Nama,
                item.Grandtotal,
                item.DownPayment
            }).OrderByDescending(item => item.Nomor).ToArray();
        RepeaterDataPO.DataBind();
    }

    protected void RepeaterDataPO_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "DownPayment")
        {
            Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument);
        }
    }

    protected void Event_CariPO(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadDataPO(db);
        }
    }
    #endregion

    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }
}