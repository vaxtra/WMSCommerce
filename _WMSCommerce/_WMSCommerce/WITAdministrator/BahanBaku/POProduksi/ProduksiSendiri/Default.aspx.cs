using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_ProduksiSendiri_Default : System.Web.UI.Page
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
                DropDownListCariPegawaiPO.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });

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
                DropDownListCariPegawaiPenerimaan.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });

                LoadDataPO(db);
                LoadDataPenerimaan(db);
            }
        }
    }

    #region In-House Production
    private void LoadDataPO(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPO.DataSource = db.TBPOProduksiBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri &&
            (!string.IsNullOrWhiteSpace(TextBoxCariIDPOProduksiBahanBaku.Text) ? item.IDPOProduksiBahanBaku.Contains(TextBoxCariIDPOProduksiBahanBaku.Text.ToUpper()) : true) &&
            (DropDownListCariBulanPO.SelectedValue != "0" ? item.Tanggal.Month == DropDownListCariBulanPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariTahunPO.SelectedValue != "0" ? item.Tanggal.Year == DropDownListCariTahunPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariPegawaiPO.SelectedValue != "0" ? item.IDPengguna == DropDownListCariPegawaiPO.SelectedValue.ToInt() : true))
        .Select(item => new
        {
            item.IDPOProduksiBahanBaku,
            item.IDProyeksi,
            item.Nomor,
            item.Tanggal,
            Pegawai = item.TBPengguna.NamaLengkap,
            item.Grandtotal,
            StatusKirim = item.TBPOProduksiBahanBakuKomposisis.Sum(data => data.Sisa) > 0 ? "class='btn btn-info btn-xs'" : "class='d-none'",
            CetakPO = "return popitup('../Cetak.aspx?id=" + item.IDPOProduksiBahanBaku + "')",
            Hapus = item.TBPenerimaanPOProduksiBahanBakus.Count == 0 && item.TBPengirimanPOProduksiBahanBakus.Count == 0 ? "btn btn-danger btn-xs" : "d-none"
        }).OrderByDescending(item => item.Nomor).ToArray(); ;
        RepeaterDataPO.DataBind();
    }

    protected void RepeaterDataPO_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Hapus")
            {
                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == e.CommandArgument.ToString());

                if (poProduksiBahanBaku.IDProyeksi != null)
                {
                    foreach (var item in db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == poProduksiBahanBaku.IDProyeksi && item.LevelProduksi == Request.QueryString["urutan"].ToInt() && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray())
                    {
                        TBPOProduksiBahanBakuDetail poProduksiBahanBakuDetail = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                        if (poProduksiBahanBakuDetail != null)
                        {
                            item.Sisa = item.Sisa + (poProduksiBahanBakuDetail.Jumlah * poProduksiBahanBakuDetail.TBBahanBaku.Konversi.Value);
                        }
                    }
                }

                db.TBPOProduksiBahanBakuBiayaTambahans.DeleteAllOnSubmit(poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans);
                db.TBPOProduksiBahanBakuKomposisis.DeleteAllOnSubmit(poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis);
                db.TBPOProduksiBahanBakuDetails.DeleteAllOnSubmit(poProduksiBahanBaku.TBPOProduksiBahanBakuDetails);
                db.TBPOProduksiBahanBakus.DeleteOnSubmit(poProduksiBahanBaku);
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

        RepeaterDataPenerimaan.DataSource = db.TBPenerimaanPOProduksiBahanBakus.Where(item => item.TBPOProduksiBahanBaku.IDTempat == pengguna.IDTempat && item.TBPOProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri &&
                (!string.IsNullOrWhiteSpace(TextBoxCariIDPenerimaanPOProduksiBahanBaku.Text) ? item.IDPenerimaanPOProduksiBahanBaku.Contains(TextBoxCariIDPenerimaanPOProduksiBahanBaku.Text.ToUpper()) : true) &&
                (DropDownListCariBulanPenerimaan.SelectedValue != "0" ? item.TanggalTerima.Value.Month == DropDownListCariBulanPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariTahunPenerimaan.SelectedValue != "0" ? item.TanggalTerima.Value.Year == DropDownListCariTahunPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariPegawaiPenerimaan.SelectedValue != "0" ? item.IDPenggunaTerima == DropDownListCariPegawaiPenerimaan.SelectedValue.ToInt() : true))
            .Select(item => new
            {
                item.IDPenerimaanPOProduksiBahanBaku,
                item.Nomor,
                item.TanggalTerima,
                Pegawai = item.TBPengguna.NamaLengkap,
                item.Grandtotal,
                CetakPO = "return popitup('../CetakPenerimaan.aspx?id=" + item.IDPenerimaanPOProduksiBahanBaku + "')",
            }).OrderByDescending(item => item.Nomor).ToArray();
        RepeaterDataPenerimaan.DataBind();
    }

    protected void RepeaterDataPenerimaan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Bayar")
        {
            Response.Redirect("../Penagihan/Pengaturan.aspx?id=" + e.CommandArgument);
        }
        else if (e.CommandName == "Batal")
        {
            Response.Redirect("../Penagihan/Pengaturan.aspx?id=" + e.CommandArgument);
        }
    }

    protected void Event_CariPenerimaan(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadDataPenerimaan(db);
        }
    }
    #endregion

    protected void ButtonPO_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }

    protected void ButtonPenerimaan_Click(object sender, EventArgs e)
    {
        Response.Redirect("Penerimaan.aspx");
    }
}