using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_ProduksiKeVendor_Default : System.Web.UI.Page
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

                DropDownListCariBulanPenerimaan.SelectedValue = DateTime.Now.Month.ToString();
                DropDownListCariTahunPenerimaan.Items.Insert(0, new ListItem { Value = (DateTime.Now.Year - 3).ToString(), Text = (DateTime.Now.Year - 3).ToString() });
                DropDownListCariTahunPenerimaan.Items.Insert(1, new ListItem { Value = (DateTime.Now.Year - 2).ToString(), Text = (DateTime.Now.Year - 2).ToString() });
                DropDownListCariTahunPenerimaan.Items.Insert(2, new ListItem { Value = (DateTime.Now.Year - 1).ToString(), Text = (DateTime.Now.Year - 1).ToString() });
                DropDownListCariTahunPenerimaan.Items.Insert(3, new ListItem { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
                DropDownListCariTahunPenerimaan.SelectedValue = DateTime.Now.Year.ToString();

                DropDownListCariPegawaiPenerimaan.DataSource = db.TBPenggunas.ToArray();
                DropDownListCariPegawaiPenerimaan.DataTextField = "NamaLengkap";
                DropDownListCariPegawaiPenerimaan.DataValueField = "IDPengguna";
                DropDownListCariPegawaiPenerimaan.DataBind();
                DropDownListCariPegawaiPenerimaan.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariVendorPenerimaan.DataSource = db.TBVendors.ToArray();
                DropDownListCariVendorPenerimaan.DataTextField = "Nama";
                DropDownListCariVendorPenerimaan.DataValueField = "IDVendor";
                DropDownListCariVendorPenerimaan.DataBind();
                DropDownListCariVendorPenerimaan.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadDataPO(db);
                LoadDataPenerimaan(db);
            }
        }
    }

    #region PO
    private void LoadDataPO(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPO.DataSource = db.TBPOProduksiProduks.Where(item => item.IDTempat == pengguna.IDTempat && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor &&
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
                StatusKirim = item.TBPOProduksiProdukKomposisis.Sum(data => data.Sisa) > 0 ? "class='btn btn-info btn-xs'" : "class='hidden'",
                CetakPO = "return popitup('../Cetak.aspx?id=" + item.IDPOProduksiProduk + "')",
                Hapus = item.TBPenerimaanPOProduksiProduks.Count == 0 && item.TBPengirimanPOProduksiProduks.Count == 0 ? "btn btn-danger btn-xs" : "hidden"
            }).OrderByDescending(item => item.Nomor).ToArray();
        RepeaterDataPO.DataBind();

    }

    protected void RepeaterDataPO_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == e.CommandArgument.ToString());

                if (poProduksiProduk.IDProyeksi != null)
                {
                    foreach (var item in db.TBProyeksiDetails.Where(item => item.IDProyeksi == poProduksiProduk.IDProyeksi).OrderBy(data => data.TBKombinasiProduk.Nama).ToArray())
                    {
                        TBPOProduksiProdukDetail poProduksiProdukDetail = poProduksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);
                        if (poProduksiProdukDetail != null)
                        {
                            item.Sisa = item.Sisa + poProduksiProdukDetail.Jumlah;
                        }
                    }
                }

                db.TBPOProduksiProdukBiayaTambahans.DeleteAllOnSubmit(poProduksiProduk.TBPOProduksiProdukBiayaTambahans);
                db.TBPOProduksiProdukKomposisis.DeleteAllOnSubmit(poProduksiProduk.TBPOProduksiProdukKomposisis);
                db.TBPOProduksiProdukDetails.DeleteAllOnSubmit(poProduksiProduk.TBPOProduksiProdukDetails);
                db.TBPOProduksiProduks.DeleteOnSubmit(poProduksiProduk);
                db.SubmitChanges();

                LoadDataPO(db);
            }
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

    #region Penerimaan
    private void LoadDataPenerimaan(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPenerimaan.DataSource = db.TBPenerimaanPOProduksiProduks.Where(item => item.TBPOProduksiProduk.IDTempat == pengguna.IDTempat && item.TBPOProduksiProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor &&
                (!string.IsNullOrWhiteSpace(TextBoxCariIDPenerimaanPOProduksiProduk.Text) ? item.IDPenerimaanPOProduksiProduk.Contains(TextBoxCariIDPenerimaanPOProduksiProduk.Text.ToUpper()) : true) &&
                (DropDownListCariBulanPenerimaan.SelectedValue != "0" ? item.TanggalTerima.Value.Month == DropDownListCariBulanPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariTahunPenerimaan.SelectedValue != "0" ? item.TanggalTerima.Value.Year == DropDownListCariTahunPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariPegawaiPenerimaan.SelectedValue != "0" ? item.IDPenggunaTerima == DropDownListCariPegawaiPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariVendorPenerimaan.SelectedValue != "0" ? item.TBPOProduksiProduk.IDVendor == DropDownListCariVendorPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariStatusPenerimaan.SelectedValue != "0" ? (DropDownListCariStatusPenerimaan.SelectedValue != "Baru" ? (DropDownListCariStatusPenerimaan.SelectedValue != "Kontra" ? item.TBPOProduksiProdukPenagihan.StatusPembayaran == true : item.TBPOProduksiProdukPenagihan.StatusPembayaran == false) : item.TBPOProduksiProdukPenagihan == null) : true))
            .Select(item => new
            {
                item.IDPenerimaanPOProduksiProduk,
                item.Nomor,
                item.TanggalTerima,
                Pegawai = item.TBPengguna.NamaLengkap,
                Vendor = item.TBPOProduksiProduk.TBVendor.Nama,
                item.Grandtotal,
                Status = item.TBPOProduksiProdukPenagihan != null ? item.TBPOProduksiProdukPenagihan.StatusPembayaran == false ? "<label class=\"label label-warning\">Kontra</label>" : "<label class=\"label label-success\">Lunas</label>" : "<label class=\"label label-default\">Baru</label>",
                CetakPO = "return popitup('../CetakPenerimaan.aspx?id=" + item.IDPenerimaanPOProduksiProduk + "')",
            }).OrderByDescending(item => item.Nomor).ToArray();
        RepeaterDataPenerimaan.DataBind();
    }

    protected void Event_CariPenerimaan(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadDataPenerimaan(db);
        }
    }
    #endregion
}