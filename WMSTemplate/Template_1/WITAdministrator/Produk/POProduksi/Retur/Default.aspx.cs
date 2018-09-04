using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Retur_Default : System.Web.UI.Page
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

            TBPOProduksiProdukRetur[] daftarPOProduksiProdukRetur = db.TBPOProduksiProdukReturs.Where(item => item.TanggalRetur.Value.Month == DropDownListCariBulan.SelectedValue.ToInt() && item.TanggalRetur.Value.Year == DropDownListCariTahun.SelectedValue.ToInt()).ToArray();

            if (!string.IsNullOrWhiteSpace(TextBoxCariIDPOProduksiProdukRetur.Text))
                daftarPOProduksiProdukRetur = daftarPOProduksiProdukRetur.Where(item => item.IDPOProduksiProdukRetur.Contains(TextBoxCariIDPOProduksiProdukRetur.Text.ToUpper())).ToArray();

            if (DropDownListCariPegawai.SelectedValue != "0")
                daftarPOProduksiProdukRetur = daftarPOProduksiProdukRetur.Where(item => item.IDPengguna == DropDownListCariPegawai.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariVendor.SelectedValue != "0")
                daftarPOProduksiProdukRetur = daftarPOProduksiProdukRetur.Where(item => item.IDVendor == DropDownListCariVendor.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariStatus.SelectedValue != "0")
                daftarPOProduksiProdukRetur = daftarPOProduksiProdukRetur.Where(item => item.EnumStatusRetur.Value == DropDownListCariStatus.SelectedValue.ToInt()).ToArray();

            RepeaterData.DataSource = daftarPOProduksiProdukRetur.Where(item => item.IDTempat == pengguna.IDTempat).Select(item => new
            {
                item.IDPOProduksiProdukRetur,
                item.Nomor,
                item.TanggalRetur,
                Pegawai = item.TBPengguna.NamaLengkap,
                Vendor = item.TBVendor.Nama,
                item.Grandtotal,
                Status = Pengaturan.StatusPOProduksi(item.EnumStatusRetur.ToString()),
                Cetak = "return popitup('Cetak.aspx?id=" + item.IDPOProduksiProdukRetur + "')",
                Batal = item.EnumStatusRetur != (int)EnumStatusPORetur.Batal ? "btn btn-outline-danger btn-xs" : "d-none"
            }).OrderByDescending(item => item.Nomor).ToArray();
            RepeaterData.DataBind();
        }
    }

    protected void RepeaterData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Batal")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {

                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TBPOProduksiProdukRetur poProduksiProdukRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item => item.IDPOProduksiProdukRetur == e.CommandArgument.ToString());
                poProduksiProdukRetur.EnumStatusRetur = (int)EnumStatusPORetur.Batal;

                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                foreach (var item in poProduksiProdukRetur.TBPOProduksiProdukReturDetails)
                {
                    StokProduk_Class.BertambahBerkurang(poProduksiProdukRetur.IDTempat.Value, pengguna.IDPengguna, item.TBStokProduk, item.Jumlah.Value, item.HargaBeli.Value, item.HargaJual.Value, EnumJenisPerpindahanStok.TransaksiBatal, "(" + item.TBStokProduk.TBKombinasiProduk.Nama + ") Pembatalan Retur PO #" + e.CommandArgument.ToString());
                }

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

    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }
}