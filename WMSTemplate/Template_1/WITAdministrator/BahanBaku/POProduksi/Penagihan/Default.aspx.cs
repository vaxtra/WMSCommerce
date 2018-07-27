using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Penagihan_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariBulan.SelectedValue = DateTime.Now.Month.ToString();
                DropDownListCariTahun.Items.Insert(0, new ListItem { Value = (DateTime.Now.Year - 2).ToString(), Text = (DateTime.Now.Year - 2).ToString() });
                DropDownListCariTahun.Items.Insert(1, new ListItem { Value = (DateTime.Now.Year - 1).ToString(), Text = (DateTime.Now.Year - 1).ToString() });
                DropDownListCariTahun.Items.Insert(2, new ListItem { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
                DropDownListCariTahun.SelectedIndex = 2;

                DropDownListCariPegawai.DataSource = db.TBPenggunas;
                DropDownListCariPegawai.DataTextField = "NamaLengkap";
                DropDownListCariPegawai.DataValueField = "IDPengguna";
                DropDownListCariPegawai.DataBind();
                DropDownListCariPegawai.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariSupplier.DataSource = db.TBSuppliers;
                DropDownListCariSupplier.DataTextField = "Nama";
                DropDownListCariSupplier.DataValueField = "IDSupplier";
                DropDownListCariSupplier.DataBind();
                DropDownListCariSupplier.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadData();
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            TBPOProduksiBahanBakuPenagihan[] daftarPOProduksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.Where(item => item.Tanggal.Month == DropDownListCariBulan.SelectedValue.ToInt() && item.Tanggal.Year == DropDownListCariTahun.SelectedValue.ToInt()).ToArray();

            if (!string.IsNullOrWhiteSpace(TextBoxCariIDIDPOProduksiBahanBakuPenagihan.Text))
                daftarPOProduksiBahanBakuPenagihan = daftarPOProduksiBahanBakuPenagihan.Where(item => item.IDPOProduksiBahanBakuPenagihan.Contains(TextBoxCariIDIDPOProduksiBahanBakuPenagihan.Text.ToUpper())).ToArray();

            if (DropDownListCariPegawai.SelectedValue != "0")
                daftarPOProduksiBahanBakuPenagihan = daftarPOProduksiBahanBakuPenagihan.Where(item => item.IDPengguna == DropDownListCariPegawai.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariSupplier.SelectedValue != "0")
                daftarPOProduksiBahanBakuPenagihan = daftarPOProduksiBahanBakuPenagihan.Where(item => item.IDSupplier == DropDownListCariSupplier.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariStatus.SelectedValue != "0")
                daftarPOProduksiBahanBakuPenagihan = daftarPOProduksiBahanBakuPenagihan.Where(item => item.StatusPembayaran == DropDownListCariStatus.SelectedValue.ToBool()).ToArray();

            RepeaterData.DataSource = daftarPOProduksiBahanBakuPenagihan.Where(item => item.IDTempat == pengguna.IDTempat).Select(item => new
            {
                item.IDPOProduksiBahanBakuPenagihan,
                item.Nomor,
                item.Tanggal,
                Pegawai = item.TBPengguna.NamaLengkap,
                Supplier = item.TBSupplier.Nama,
                item.TotalTagihan,
                item.TotalBayar,
                Status = item.StatusPembayaran == false ? "<label class=\"label label-warning\">Tagihan</label>" : "<label class=\"label label-success\">Lunas</label>",
                item.StatusPembayaran,
                Cetak = "return popitup('Cetak.aspx?id=" + item.IDPOProduksiBahanBakuPenagihan + "')",
                Batal = item.TotalBayar == 0 && item.StatusPembayaran == false ? "btn btn-danger btn-xs" : "hidden"
            }).OrderByDescending(item => item.Nomor).ToArray();
            RepeaterData.DataBind();
        }
    }

    protected void RepeaterData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBakuPenagihan poProduksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.FirstOrDefault(item => item.IDPOProduksiBahanBakuPenagihan == e.CommandArgument.ToString());
                poProduksiBahanBakuPenagihan.TBPenerimaanPOProduksiBahanBakus.ToList().ForEach(item => item.IDPOProduksiBahanBakuPenagihan = null);
                poProduksiBahanBakuPenagihan.TBPOProduksiBahanBakuReturs.ToList().ForEach(item => { item.EnumStatusRetur = (int)EnumStatusPORetur.Baru; item.IDPOProduksiBahanBakuPenagihan = null; } );
                db.TBPOProduksiBahanBakus.Where(item => item.IDPOProduksiBahanBakuPenagihan == poProduksiBahanBakuPenagihan.IDPOProduksiBahanBakuPenagihan).ToList().ForEach(item => item.IDPOProduksiBahanBakuPenagihan = null);
                db.TBPOProduksiBahanBakuPenagihans.DeleteOnSubmit(poProduksiBahanBakuPenagihan);

                db.SubmitChanges();

                LoadData();
            }
        }
    }

    protected void Event_Cari(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadData();
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }
}