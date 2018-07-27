using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Proyeksi_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TextBoxTanggalProyeksi.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalTarget.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxPegawai.Text = pengguna.NamaLengkap;

                DropDownListStokProduk.DataSource = db.TBStokProduks
                    .Where(item =>
                        item.IDTempat == pengguna.IDTempat &&
                        item.TBKombinasiProduk.TBProduk._IsActive)
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.TBKombinasiProduk.Nama
                    })
                    .OrderBy(item => item.Nama)
                    .ToArray();

                DropDownListStokProduk.DataTextField = "Nama";
                DropDownListStokProduk.DataValueField = "IDStokProduk";
                DropDownListStokProduk.DataBind();

                ViewState["ViewStateListDetail"] = new List<ProyeksiDetail_Model>();
                ViewState["ViewStateKomposisiProduk"] = new List<KomposisiProduk_Model>();

                if (DropDownListStokProduk.Items.Count == 0)
                {
                    ButtonSimpanDetail.Enabled = false;
                    ButtonSimpan.Enabled = false;
                }
            }
        }
    }

    private void LoadData()
    {
        List<ProyeksiDetail_Model> ViewStateListDetail = (List<ProyeksiDetail_Model>)ViewState["ViewStateListDetail"];
        List<KomposisiProduk_Model> ViewStateKomposisiProduk = (List<KomposisiProduk_Model>)ViewState["ViewStateKomposisiProduk"];

        #region Produk
        if (ViewStateListDetail.Count == 0)
        {
            RepeaterProduk.DataSource = null;
            RepeaterProduk.DataBind();
            LabelTotalJumlah.Text = string.Empty;
        }
        else
        {
            RepeaterProduk.DataSource = ViewStateListDetail;
            RepeaterProduk.DataBind();
            LabelTotalJumlah.Text = ViewStateListDetail.Sum(item => item.Jumlah).ToFormatHargaBulat();
        }
        #endregion


        if (ViewStateKomposisiProduk.Count == 0)
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
            RepeaterKomposisi.DataSource = ViewStateKomposisiProduk.Where(item => item.LevelProduksi < ViewStateKomposisiProduk.Max(data => data.LevelProduksi)).GroupBy(item => new
            {
                item.LevelProduksi
            })
            .Select(item => new
            {
                item.Key,
                SubData = item.Where(data => data.LevelProduksi == item.Key.LevelProduksi && data.BahanBakuDasar == false).Select(data => new
                {
                    data.IDBahanBaku,
                    data.IDSatuan,
                    data.BahanBaku,
                    data.Kategori,
                    data.Satuan,
                    data.JumlahPemakaian
                }).OrderBy(data => data.BahanBaku)
            }).OrderByDescending(item => item.Key.LevelProduksi);
            RepeaterKomposisi.DataBind();
            #endregion

            #region Komposisi Dasar
            RepeaterBahanBakuDasar.DataSource = ViewStateKomposisiProduk.Where(data => data.BahanBakuDasar == true)
            .GroupBy(item => new
            {
                item.IDBahanBaku,
                item.IDSatuan,
                item.BahanBaku,
                item.Kategori,
                item.Satuan
            })
            .Select(item => new
            {
                item.Key.IDBahanBaku,
                item.Key.IDSatuan,
                item.Key.BahanBaku,
                item.Key.Kategori,
                item.Key.Satuan,
                JumlahPemakaian = item.Sum(x => x.JumlahPemakaian)
            }).OrderBy(data => data.BahanBaku);
            RepeaterBahanBakuDasar.DataBind();
            #endregion
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;
        ViewState["ViewStateKomposisiProduk"] = ViewStateKomposisiProduk;

        ButtonSimpan.Enabled = true;

        if (ViewStateListDetail.Count == 0)
        {
            ButtonSimpan.Enabled = false;
        }
    }

    private void TambahKomposisi(DataClassesDatabaseDataContext db, int idKombinasiProduk, int jumlah)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        List<KomposisiProduk_Model> ViewStateKomposisiProduk = (List<KomposisiProduk_Model>)ViewState["ViewStateKomposisiProduk"];

        TBKombinasiProduk[] daftarkombinasiProduk = db.TBKombinasiProduks.ToArray();
        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

        TBKombinasiProduk kombinasiProduk = daftarkombinasiProduk.FirstOrDefault(data => data.IDKombinasiProduk == idKombinasiProduk);

        foreach (var subItem in kombinasiProduk.TBKomposisiKombinasiProduks)
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku);

            int Level = 0;

            KomposisiProduk_Model komposisiDetail = ViewStateKomposisiProduk.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku && data.LevelProduksi == Level);

            if (komposisiDetail == null)
            {
                komposisiDetail = new KomposisiProduk_Model()
                {
                    LevelProduksi = Level,
                    IDBahanBaku = stokBahanBaku.IDBahanBaku.Value,
                    IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuan,
                    Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, stokBahanBaku, null),
                    BahanBaku = stokBahanBaku.TBBahanBaku.Nama,
                    Satuan = stokBahanBaku.TBBahanBaku.TBSatuan.Nama,
                    JumlahPemakaian = subItem.Jumlah.Value * jumlah,
                    BahanBakuDasar = stokBahanBaku.TBBahanBaku.TBKomposisiBahanBakus.Count > 0 ? false : true
                };

                if (komposisiDetail.BahanBakuDasar == true)
                {
                    komposisiDetail.Stok = stokBahanBaku.Jumlah.Value;
                    komposisiDetail.Kurang = stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian < 0 ? Math.Abs(stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian) : 0;
                }

                ViewStateKomposisiProduk.Add(komposisiDetail);
            }
            else
            {
                komposisiDetail.JumlahPemakaian = komposisiDetail.JumlahPemakaian + subItem.Jumlah.Value * jumlah;

                if (komposisiDetail.BahanBakuDasar == true)
                {
                    komposisiDetail.Stok = stokBahanBaku.Jumlah.Value;
                    komposisiDetail.Kurang = stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian < 0 ? Math.Abs(stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian) : 0;
                }
            }


            if (stokBahanBaku.TBBahanBaku.TBKomposisiBahanBakus.Count > 0)
            {
                TambahSubKomposisiBahanBaku(ViewStateKomposisiProduk, stokBahanBaku.TBBahanBaku, (subItem.Jumlah.Value * jumlah), daftarStokBahanBaku, Level + 1);
            }
        }

        ViewState["ViewStateKomposisiProduk"] = ViewStateKomposisiProduk;
    }

    private void TambahSubKomposisiBahanBaku(List<KomposisiProduk_Model> ViewStateKomposisiProduk, TBBahanBaku bahanBaku, decimal jumlahBahanWIP, TBStokBahanBaku[] daftarStokBahanBaku, int Level)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        foreach (var subItem in bahanBaku.TBKomposisiBahanBakus)
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku);

            KomposisiProduk_Model komposisiDetail = ViewStateKomposisiProduk.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku && data.LevelProduksi == Level);

            if (komposisiDetail == null)
            {
                komposisiDetail = new KomposisiProduk_Model()
                {
                    LevelProduksi = Level,
                    IDBahanBaku = stokBahanBaku.IDBahanBaku.Value,
                    IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuan,
                    Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(null, stokBahanBaku, null),
                    BahanBaku = stokBahanBaku.TBBahanBaku.Nama,
                    Satuan = stokBahanBaku.TBBahanBaku.TBSatuan.Nama,
                    JumlahPemakaian = subItem.Jumlah.Value * jumlahBahanWIP,
                    BahanBakuDasar = stokBahanBaku.TBBahanBaku.TBKomposisiBahanBakus.Count > 0 ? false : true
                };

                if (komposisiDetail.BahanBakuDasar == true)
                {
                    komposisiDetail.Stok = stokBahanBaku.Jumlah.Value;
                    komposisiDetail.Kurang = stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian < 0 ? Math.Abs(stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian) : 0;
                }

                ViewStateKomposisiProduk.Add(komposisiDetail);
            }
            else
            {
                komposisiDetail.JumlahPemakaian = komposisiDetail.JumlahPemakaian + subItem.Jumlah.Value * jumlahBahanWIP;

                if (komposisiDetail.BahanBakuDasar == true)
                {
                    komposisiDetail.Stok = stokBahanBaku.Jumlah.Value;
                    komposisiDetail.Kurang = stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian < 0 ? Math.Abs(stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian) : 0;
                }
            }

            if (stokBahanBaku.TBBahanBaku.TBKomposisiBahanBakus.Count > 0)
            {
                TambahSubKomposisiBahanBaku(ViewStateKomposisiProduk, stokBahanBaku.TBBahanBaku, (subItem.Jumlah.Value * jumlahBahanWIP), daftarStokBahanBaku, Level + 1);
            }
        }
    }

    private void KurangKomposisi(DataClassesDatabaseDataContext db, int idKombinasiProduk, int jumlah)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        List<KomposisiProduk_Model> ViewStateKomposisiProduk = (List<KomposisiProduk_Model>)ViewState["ViewStateKomposisiProduk"];

        TBKombinasiProduk[] daftarkombinasiProduk = db.TBKombinasiProduks.ToArray();
        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

        TBKombinasiProduk kombinasiProduk = daftarkombinasiProduk.FirstOrDefault(data => data.IDKombinasiProduk == idKombinasiProduk);

        foreach (var subItem in kombinasiProduk.TBKomposisiKombinasiProduks)
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku);

            int Level = 0;

            KomposisiProduk_Model komposisiDetail = ViewStateKomposisiProduk.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku && data.LevelProduksi == Level);

            komposisiDetail.JumlahPemakaian = komposisiDetail.JumlahPemakaian - subItem.Jumlah.Value * jumlah;
            if (komposisiDetail.BahanBakuDasar == true)
                komposisiDetail.Kurang = stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian < 0 ? Math.Abs(stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian) : 0;

            if (komposisiDetail.JumlahPemakaian == 0)
                ViewStateKomposisiProduk.Remove(komposisiDetail);

            if (stokBahanBaku.TBBahanBaku.TBKomposisiBahanBakus.Count > 0)
            {
                KurangSubKomposisiBahanBaku(ViewStateKomposisiProduk, stokBahanBaku.TBBahanBaku, (subItem.Jumlah.Value * jumlah), daftarStokBahanBaku, Level + 1);
            }
        }

        ViewState["ViewStateKomposisiProduk"] = ViewStateKomposisiProduk;
    }

    private void KurangSubKomposisiBahanBaku(List<KomposisiProduk_Model> ViewStateKomposisiProduk, TBBahanBaku bahanBaku, decimal jumlahBahanWIP, TBStokBahanBaku[] daftarStokBahanBaku, int Level)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        foreach (var subItem in bahanBaku.TBKomposisiBahanBakus)
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku);

            KomposisiProduk_Model komposisiDetail = ViewStateKomposisiProduk.FirstOrDefault(data => data.IDBahanBaku == subItem.IDBahanBaku && data.LevelProduksi == Level);

            komposisiDetail.JumlahPemakaian = komposisiDetail.JumlahPemakaian - subItem.Jumlah.Value * jumlahBahanWIP;
            if (komposisiDetail.BahanBakuDasar == true)
                komposisiDetail.Kurang = stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian < 0 ? Math.Abs(stokBahanBaku.Jumlah.Value - komposisiDetail.JumlahPemakaian) : 0;

            if (komposisiDetail.JumlahPemakaian == 0)
                ViewStateKomposisiProduk.Remove(komposisiDetail);

            if (stokBahanBaku.TBBahanBaku.TBKomposisiBahanBakus.Count > 0)
            {
                KurangSubKomposisiBahanBaku(ViewStateKomposisiProduk, stokBahanBaku.TBBahanBaku, (subItem.Jumlah.Value * jumlahBahanWIP), daftarStokBahanBaku, Level + 1);
            }
        }
    }

    protected void ButtonSimpanDetail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (TextBoxJumlah.Text.ToDecimal().ToInt() > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    List<ProyeksiDetail_Model> ViewStateListDetail = (List<ProyeksiDetail_Model>)ViewState["ViewStateListDetail"];

                    ProyeksiDetail_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                        detail = new ProyeksiDetail_Model();
                        detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
                        detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
                        detail.IDStokProduk = stokProduk.IDStokProduk;
                        detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
                        detail.Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, stokProduk, null);
                        detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
                        detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
                        detail.Warna = stokProduk.TBKombinasiProduk.TBProduk.TBWarna.Nama;
                        detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
                        detail.HargaJual = stokProduk.HargaJual.Value;
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt();
                        detail.SisaBelumProduksi = detail.Jumlah;
                        ViewStateListDetail.Add(detail);

                        TambahKomposisi(db, detail.IDKombinasiProduk, detail.Jumlah);
                    }
                    else
                    {
                        KurangKomposisi(db, detail.IDKombinasiProduk, detail.Jumlah);

                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt();

                        TambahKomposisi(db, detail.IDKombinasiProduk, detail.Jumlah);
                    }

                    ViewState["ViewStateListDetail"] = ViewStateListDetail;


                }
                LoadData();
            }
        }
    }

    protected void RepeaterProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<ProyeksiDetail_Model> ViewStateListDetail = (List<ProyeksiDetail_Model>)ViewState["ViewStateListDetail"];

        ProyeksiDetail_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDKombinasiProduk == e.CommandArgument.ToInt());


        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            KurangKomposisi(db, detail.IDKombinasiProduk, detail.Jumlah);
        }

        ViewStateListDetail.Remove(detail);
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        LoadData();
    }

    protected void CustomValidatorJumlah_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (TextBoxJumlah.Text.ToDecimal().ToInt() <= 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            List<ProyeksiDetail_Model> ViewStateListDetail = (List<ProyeksiDetail_Model>)ViewState["ViewStateListDetail"];
            List<KomposisiProduk_Model> ViewStateKomposisiProduk = (List<KomposisiProduk_Model>)ViewState["ViewStateKomposisiProduk"];

            if (ViewStateListDetail.Count > 0)
            {
                string IDProyeksi = string.Empty;
                TBProyeksi proyeksi = null;

                try
                {
                    peringatan.Visible = false;

                    db.Proc_InsertProyeksi(ref IDProyeksi, pengguna.IDTempat, TextBoxTanggalProyeksi.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                    proyeksi = db.TBProyeksis.FirstOrDefault(item => item.IDProyeksi == IDProyeksi);
                    proyeksi.IDPengguna = pengguna.IDPengguna;
                    proyeksi.TanggalTarget = TextBoxTanggalTarget.Text.ToDateTime();
                    proyeksi.TanggalSelesai = null;
                    proyeksi.TBProyeksiDetails.AddRange(ViewStateListDetail.OrderBy(item => item.KombinasiProduk).Select(item => new TBProyeksiDetail
                    {
                        IDKombinasiProduk = item.IDKombinasiProduk,
                        HargaJual = item.HargaJual,
                        Jumlah = item.Jumlah,
                        Sisa = item.Jumlah
                    }));
                    proyeksi.TotalJumlah = proyeksi.TBProyeksiDetails.Sum(item => item.Jumlah);
                    proyeksi.GrandTotalHargaJual = proyeksi.TBProyeksiDetails.Sum(item => item.HargaJual * item.Jumlah);
                    proyeksi.EnumStatusProyeksi = 1;
                    proyeksi.Keterangan = TextBoxKeterangan.Text;

                    if (ViewStateKomposisiProduk.Count > 0)
                    {
                        int LevelAkhir = ViewStateKomposisiProduk.Max(item => item.LevelProduksi);

                        proyeksi.TBProyeksiKomposisis.AddRange(ViewStateKomposisiProduk.OrderBy(item2 => item2.BahanBaku).ThenBy(item2 => item2.LevelProduksi).Select(item => new TBProyeksiKomposisi
                        {
                            IDBahanBaku = item.IDBahanBaku,
                            IDSatuan = item.IDSatuan,
                            LevelProduksi = LevelAkhir - item.LevelProduksi,
                            BahanBakuDasar = item.BahanBakuDasar,
                            Jumlah = item.JumlahPemakaian,
                            Stok = item.Stok,
                            Kurang = item.Kurang,
                            Sisa = item.JumlahPemakaian
                        }));
                    }

                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    if (IDProyeksi != string.Empty)
                    {
                        proyeksi = db.TBProyeksis.FirstOrDefault(item => item.IDProyeksi == IDProyeksi);
                        if (proyeksi != null)
                        {
                            db.TBProyeksiKomposisis.DeleteAllOnSubmit(proyeksi.TBProyeksiKomposisis);
                            db.TBProyeksiDetails.DeleteAllOnSubmit(proyeksi.TBProyeksiDetails);
                            db.TBProyeksis.DeleteOnSubmit(proyeksi);
                            db.SubmitChanges();

                            IDProyeksi = string.Empty;
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Purchase Order Produk (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
                    LabelPeringatan.Text = "Terjadi kesalahan, silahkan cek kembali data yang diinputkan";
                    peringatan.Visible = true;
                }
                finally
                {
                    if (IDProyeksi != string.Empty)
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }
    }
}