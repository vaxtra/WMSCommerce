using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Proyeksi_Cetak : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBStore store = db.TBStores.FirstOrDefault();

                LabelNamaStore.Text = store.Nama;
                LabelAlamatStore.Text = store.Alamat;

                TBProyeksi proyeksi = db.TBProyeksis.FirstOrDefault(item => item.IDProyeksi == Request.QueryString["id"]);

                LabelIDProyeksi.Text = proyeksi.IDProyeksi;
                LabelPegawai.Text = proyeksi.TBPengguna.NamaLengkap;
                LabelTempat.Text = proyeksi.TBTempat.Nama;
                LabelTanggalProyeksi.Text = proyeksi.TanggalProyeksi.ToFormatTanggal();
                LabelTanggalTarget.Text = proyeksi.TanggalTarget.ToFormatTanggal();
                LabelStatusProyeksi.Text = Pengaturan.StatusProyeksi(proyeksi.EnumStatusProyeksi.Value);

                var varian = proyeksi.TBProyeksiDetails.Select(item => new { item.TBKombinasiProduk.IDAtributProduk, item.TBKombinasiProduk.TBAtributProduk.Nama }).OrderBy(item => item.Nama).Distinct();
                RepeaterVarian.DataSource = varian;
                RepeaterVarian.DataBind();

                KolomVarian.Attributes.Add("colspan", varian.Count().ToString());

                #region Produk
                RepeaterDetail.DataSource = proyeksi.TBProyeksiDetails.GroupBy(item => new
                {
                    item.TBKombinasiProduk.TBProduk
                }).Select(item => new
                {
                    PemilikProduk = item.Key.TBProduk.TBPemilikProduk.Nama,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(null, null, item.FirstOrDefault().TBKombinasiProduk),
                    Produk = item.Key.TBProduk.Nama,
                    Warna = item.Key.TBProduk.TBWarna.Nama,
                    AtributProduk = varian.Select(data => new
                    {
                        Jumlah = item.FirstOrDefault(x => x.TBKombinasiProduk.IDAtributProduk == data.IDAtributProduk) != null ? item.FirstOrDefault(x => x.TBKombinasiProduk.IDAtributProduk == data.IDAtributProduk).Jumlah : 0
                    }),
                    Total = item.Sum(x => x.Jumlah)
                }).ToArray();
                RepeaterDetail.DataBind();
                #endregion

                if (proyeksi.TBProyeksiKomposisis.Count == 0)
                {
                    PanelKomposisi.Visible = false;

                    RepeaterKomposisi.DataSource = null;
                    RepeaterKomposisi.DataBind();

                    RepeaterBahanBakuDasar.DataSource = null;
                    RepeaterBahanBakuDasar.DataBind();
                }
                else
                {
                    PanelKomposisi.Visible = true;

                    #region Komposisi
                    RepeaterKomposisi.DataSource = proyeksi.TBProyeksiKomposisis.Where(item => item.LevelProduksi > 1).GroupBy(item => new
                    {
                        item.LevelProduksi
                    })
                    .Select(item => new
                    {
                        item.Key,
                        SubData = proyeksi.TBProyeksiKomposisis.Where(data => data.LevelProduksi == item.Key.LevelProduksi && data.BahanBakuDasar == false).Select(data => new
                        {
                            data.IDBahanBaku,
                            data.IDSatuan,
                            BahanBaku = data.TBBahanBaku.Nama,
                            Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, null, data.TBBahanBaku),
                            Satuan = data.TBSatuan.Nama,
                            data.Jumlah
                        }).OrderBy(data => data.BahanBaku)
                    }).OrderBy(item => item.Key.LevelProduksi);
                    RepeaterKomposisi.DataBind();
                    #endregion

                    #region Komposisi Dasar
                    RepeaterBahanBakuDasar.DataSource = proyeksi.TBProyeksiKomposisis.Where(data => data.BahanBakuDasar == true).Select(data => new
                    {
                        data.IDBahanBaku,
                        data.IDSatuan,
                        BahanBaku = data.TBBahanBaku.Nama,
                        Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, null, data.TBBahanBaku),
                        Satuan = data.TBSatuan.Nama,
                        data.Jumlah,
                        data.Stok,
                        data.Kurang
                    }).OrderBy(data => data.BahanBaku);
                    RepeaterBahanBakuDasar.DataBind();
                    #endregion
                }

                LabelKeterangan.Text = proyeksi.Keterangan != null ? proyeksi.Keterangan : string.Empty;
            }
        }
    }
}