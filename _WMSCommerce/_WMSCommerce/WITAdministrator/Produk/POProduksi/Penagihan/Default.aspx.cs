using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Penagihan_Default : System.Web.UI.Page
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

                DropDownListCariVendor.DataSource = db.TBVendors;
                DropDownListCariVendor.DataTextField = "Nama";
                DropDownListCariVendor.DataValueField = "IDVendor";
                DropDownListCariVendor.DataBind();
                DropDownListCariVendor.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadData();
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            TBPOProduksiProdukPenagihan[] daftarPOProduksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.Where(item => item.Tanggal.Month == DropDownListCariBulan.SelectedValue.ToInt() && item.Tanggal.Year == DropDownListCariTahun.SelectedValue.ToInt()).ToArray();

            if (!string.IsNullOrWhiteSpace(TextBoxCariIDIDPOProduksiProdukPenagihan.Text))
                daftarPOProduksiProdukPenagihan = daftarPOProduksiProdukPenagihan.Where(item => item.IDPOProduksiProdukPenagihan.Contains(TextBoxCariIDIDPOProduksiProdukPenagihan.Text.ToUpper())).ToArray();

            if (DropDownListCariPegawai.SelectedValue != "0")
                daftarPOProduksiProdukPenagihan = daftarPOProduksiProdukPenagihan.Where(item => item.IDPengguna == DropDownListCariPegawai.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariVendor.SelectedValue != "0")
                daftarPOProduksiProdukPenagihan = daftarPOProduksiProdukPenagihan.Where(item => item.IDVendor == DropDownListCariVendor.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariStatus.SelectedValue != "0")
                daftarPOProduksiProdukPenagihan = daftarPOProduksiProdukPenagihan.Where(item => item.StatusPembayaran == DropDownListCariStatus.SelectedValue.ToBool()).ToArray();

            RepeaterData.DataSource = daftarPOProduksiProdukPenagihan.Where(item => item.IDTempat == pengguna.IDTempat).Select(item => new
            {
                item.IDPOProduksiProdukPenagihan,
                item.Nomor,
                item.Tanggal,
                Pegawai = item.TBPengguna.NamaLengkap,
                Vendor = item.TBVendor.Nama,
                item.TotalTagihan,
                item.TotalBayar,
                Status = item.StatusPembayaran == false ? "<label class=\"label label-warning\">Tagihan</label>" : "<label class=\"label label-success\">Lunas</label>",
                item.StatusPembayaran,
                Cetak = "return popitup('Cetak.aspx?id=" + item.IDPOProduksiProdukPenagihan + "')",
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
                TBPOProduksiProdukPenagihan poProduksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == e.CommandArgument.ToString());
                poProduksiProdukPenagihan.TBPenerimaanPOProduksiProduks.ToList().ForEach(item => item.IDPOProduksiProdukPenagihan = null);
                poProduksiProdukPenagihan.TBPOProduksiProdukReturs.ToList().ForEach(item => { item.EnumStatusRetur = (int)EnumStatusPORetur.Baru; item.IDPOProduksiProdukPenagihan = null; } );
                db.TBPOProduksiProduks.Where(item => item.IDPOProduksiProdukPenagihan == poProduksiProdukPenagihan.IDPOProduksiProdukPenagihan).ToList().ForEach(item => item.IDPOProduksiProdukPenagihan = null);
                db.TBPOProduksiProdukPenagihans.DeleteOnSubmit(poProduksiProdukPenagihan);

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
}