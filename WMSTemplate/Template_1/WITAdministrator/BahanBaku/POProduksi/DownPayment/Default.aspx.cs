using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_DownPayment_Default : System.Web.UI.Page
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

                DropDownListCariSupplierPO.DataSource = db.TBSuppliers.ToArray();
                DropDownListCariSupplierPO.DataTextField = "Nama";
                DropDownListCariSupplierPO.DataValueField = "IDSupplier";
                DropDownListCariSupplierPO.DataBind();
                DropDownListCariSupplierPO.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadDataPO(db);
            }
        }
    }
    #region PO
    private void LoadDataPO(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPO.DataSource = db.TBPOProduksiBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder &&
            (!string.IsNullOrWhiteSpace(TextBoxCariIDPOProduksiBahanBaku.Text) ? item.IDPOProduksiBahanBaku.Contains(TextBoxCariIDPOProduksiBahanBaku.Text.ToUpper()) : true) &&
            (DropDownListCariBulanPO.SelectedValue != "0" ? item.Tanggal.Month == DropDownListCariBulanPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariTahunPO.SelectedValue != "0" ? item.Tanggal.Year == DropDownListCariTahunPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariPegawaiPO.SelectedValue != "0" ? item.IDPengguna == DropDownListCariPegawaiPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariSupplierPO.SelectedValue != "0" ? item.IDSupplier == DropDownListCariSupplierPO.SelectedValue.ToInt() : true))
            .Select(item => new
            {
                item.IDPOProduksiBahanBaku,
                item.IDProyeksi,
                item.Nomor,
                item.Tanggal,
                Pegawai = item.TBPengguna.NamaLengkap,
                Supplier = item.TBSupplier.Nama,
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