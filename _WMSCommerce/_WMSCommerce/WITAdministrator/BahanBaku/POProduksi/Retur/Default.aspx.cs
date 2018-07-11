using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Retur_Default : System.Web.UI.Page
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

            TBPOProduksiBahanBakuRetur[] daftarPOProduksiBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.Where(item => item.TanggalRetur.Value.Month == DropDownListCariBulan.SelectedValue.ToInt() && item.TanggalRetur.Value.Year == DropDownListCariTahun.SelectedValue.ToInt()).ToArray();

            if (!string.IsNullOrWhiteSpace(TextBoxCariIDPOProduksiBahanBakuRetur.Text))
                daftarPOProduksiBahanBakuRetur = daftarPOProduksiBahanBakuRetur.Where(item => item.IDPOProduksiBahanBakuRetur.Contains(TextBoxCariIDPOProduksiBahanBakuRetur.Text.ToUpper())).ToArray();

            if (DropDownListCariPegawai.SelectedValue != "0")
                daftarPOProduksiBahanBakuRetur = daftarPOProduksiBahanBakuRetur.Where(item => item.IDPengguna == DropDownListCariPegawai.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariSupplier.SelectedValue != "0")
                daftarPOProduksiBahanBakuRetur = daftarPOProduksiBahanBakuRetur.Where(item => item.IDSupplier == DropDownListCariSupplier.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariStatus.SelectedValue != "0")
                daftarPOProduksiBahanBakuRetur = daftarPOProduksiBahanBakuRetur.Where(item => item.EnumStatusRetur.Value == DropDownListCariStatus.SelectedValue.ToInt()).ToArray();

            RepeaterData.DataSource = daftarPOProduksiBahanBakuRetur.Where(item => item.IDTempat == pengguna.IDTempat).Select(item => new
            {
                item.IDPOProduksiBahanBakuRetur,
                item.Nomor,
                item.TanggalRetur,
                Pegawai = item.TBPengguna.NamaLengkap,
                Supplier = item.TBSupplier.Nama,
                item.Grandtotal,
                Status = Pengaturan.StatusPOProduksi(item.EnumStatusRetur.ToString()),
                Cetak = "return popitup('Cetak.aspx?id=" + item.IDPOProduksiBahanBakuRetur + "')",
                Batal = item.EnumStatusRetur != (int)EnumStatusPORetur.Batal ? "btn btn-danger btn-xs" : "d-none"
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
                TBPOProduksiBahanBakuRetur poProduksiBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item => item.IDPOProduksiBahanBakuRetur == e.CommandArgument.ToString());
                poProduksiBahanBakuRetur.EnumStatusRetur = (int)EnumStatusPORetur.Batal;

                foreach (var item in poProduksiBahanBakuRetur.TBPOProduksiBahanBakuReturDetails)
                {
                    StokBahanBaku_Class.UpdateBertambahBerkurang(
                    db: db,
                    tanggal: DateTime.Now,
                    idPengguna: pengguna.IDPengguna,
                    stokBahanBaku: item.TBStokBahanBaku,
                    jumlahStok: item.Jumlah.Value,
                    hargaBeli: item.HargaBeli.Value,
                    satuanBesar: true,
                    enumJenisPerpindahanStok: EnumJenisPerpindahanStok.TransaksiBatal,
                    keterangan: "(" + item.TBStokBahanBaku.TBBahanBaku.Nama + ") Pembatalan Retur PO #" + e.CommandArgument.ToString());
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