using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Proyeksi_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                
                TextBoxPegawai.Text = pengguna.NamaLengkap;

                TBProyeksi proyeksi = db.TBProyeksis.FirstOrDefault(item => item.IDProyeksi == Request.QueryString["id"]);
                TextBoxTanggalProyeksi.Text = proyeksi.TanggalProyeksi.ToFormatTanggal();
                TextBoxTanggalTarget.Text = proyeksi.TanggalTarget.ToFormatTanggal();

                #region Produk
                var daftarProduk = proyeksi.TBProyeksiDetails.Select(item => new
                {
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(null, null, item.TBKombinasiProduk),
                    item.Jumlah,
                    item.Sisa
                }).OrderBy(item => item.Produk).ThenBy(item => item.AtributProduk).ToArray();
                RepeaterProduk.DataSource = daftarProduk;
                RepeaterProduk.DataBind();

                if (daftarProduk.Sum(item => item.Sisa) <= 0)
                    ButtonProduk.Visible = false;
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
                    var produksiBahanBaku = proyeksi.TBProyeksiKomposisis.Where(item => item.LevelProduksi > 0).GroupBy(item => new
                    {
                        item.LevelProduksi
                    })
                    .Select(item => new
                    {
                        item.Key.LevelProduksi,
                        SubData = item.Where(data => data.LevelProduksi == item.Key.LevelProduksi && data.BahanBakuDasar == false).Select(data => new
                        {
                            data.IDBahanBaku,
                            data.IDSatuan,
                            BahanBaku = data.TBBahanBaku.Nama,
                            Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, null, data.TBBahanBaku),
                            Satuan = data.TBSatuan.Nama,
                            data.Jumlah
                        }).OrderBy(data => data.BahanBaku),
                        StatusButton = item.Sum(data => data.Sisa) > 0 ? true : false
                    }).OrderBy(item => item.LevelProduksi);
                    RepeaterKomposisi.DataSource = produksiBahanBaku;
                    RepeaterKomposisi.DataBind();
                    #endregion

                    #region Komposisi Dasar
                    //RepeaterBahanBakuDasar.DataSource = proyeksi.TBProyeksiKomposisis.Where(item => item.BahanBakuDasar == true)
                    //.Select(item => new
                    //{
                    //    item.TBBahanBaku.IDBahanBaku,
                    //    item.TBSatuan.IDSatuan,
                    //    BahanBaku = item.TBBahanBaku.Nama,
                    //    Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, null, item.TBBahanBaku),
                    //    Satuan = item.TBSatuan.Nama,
                    //    item.Jumlah,
                    //}).OrderBy(data => data.BahanBaku);
                    //RepeaterBahanBakuDasar.DataBind();

                    var purchaseOrder = proyeksi.TBProyeksiKomposisis.Where(item => item.BahanBakuDasar == true).GroupBy(item => new
                    {
                        item.TBBahanBaku,
                        item.TBSatuan
                    })
                    .Select(item => new
                    {
                        item.Key.TBBahanBaku.IDBahanBaku,
                        item.Key.TBSatuan.IDSatuan,
                        BahanBaku = item.Key.TBBahanBaku.Nama,
                        Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, null, item.Key.TBBahanBaku),
                        Satuan = item.Key.TBSatuan.Nama,
                        Jumlah = item.Sum(x => x.Jumlah),
                        Sisa = item.Sum(x => x.Sisa)
                    }).OrderBy(data => data.BahanBaku);
                    RepeaterBahanBakuDasar.DataSource = purchaseOrder;
                    RepeaterBahanBakuDasar.DataBind();

                    if (purchaseOrder.Sum(item => item.Sisa) <= 0)
                        LinkButtonPurchaseOrder.Visible = false;
                    #endregion
                }

                RepeaterPOProduksiBahanBaku.DataSource = proyeksi.TBPOProduksiBahanBakus.OrderBy(item => item.Tanggal).Select(item => new
                {
                    item.IDPOProduksiBahanBaku,
                    Jenis = Pengaturan.JenisPOProduksi(item.EnumJenisProduksi, "BahanBaku"),
                    item.Tanggal,
                    Pegawai = item.TBPengguna.NamaLengkap,
                    Supplier = item.IDSupplier == null ? string.Empty : item.TBSupplier.Nama,
                    item.Grandtotal
                }).ToArray();
                RepeaterPOProduksiBahanBaku.DataBind();

                RepeaterPOProduksiProduk.DataSource = proyeksi.TBPOProduksiProduks.OrderBy(item => item.Tanggal).Select(item => new
                {
                    item.IDPOProduksiProduk,
                    Jenis = Pengaturan.JenisPOProduksi(item.EnumJenisProduksi, "Produk"),
                    item.Tanggal,
                    Pegawai = item.TBPengguna.NamaLengkap,
                    Vendor = item.IDVendor == null ? string.Empty : item.TBVendor.Nama,
                    item.Grandtotal
                }).ToArray();
                RepeaterPOProduksiProduk.DataBind();
            }
        }
    }

    protected void RepeaterKomposisi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "produksi")
        {
            Response.Redirect("/WITAdministrator/BahanBaku/POProduksi/ProduksiSendiri/Pengaturan.aspx?proy=" + Request.QueryString["id"] + "&urutan=" + e.CommandArgument.ToString());
        }
        else if (e.CommandName == "supplier")
        {
            Response.Redirect("/WITAdministrator/BahanBaku/POProduksi/ProduksiKeSupplier/Pengaturan.aspx?proy=" + Request.QueryString["id"] + "&urutan=" + e.CommandArgument.ToString());
        }
    }

    protected void LinkButtonPurchaseOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/BahanBaku/POProduksi/PurchaseOrder/Pengaturan.aspx?proy=" + Request.QueryString["id"]);
    }

    protected void LinkButtonPurchase_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Produk/POProduksi/PurchaseOrder/Pengaturan.aspx?proy=" + Request.QueryString["id"]);
    }

    protected void LinkButtonProduksi_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Produk/POProduksi/ProduksiSendiri/Pengaturan.aspx?proy=" + Request.QueryString["id"]);
    }

    protected void LinkButtonVendor_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Produk/POProduksi/ProduksiKeVendor/Pengaturan.aspx?proy=" + Request.QueryString["id"]);
    }
}