using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_ProduksiSendiri_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TextBoxIDProyeksi.Text = string.Empty;
                TextBoxPegawai.Text = pengguna.NamaLengkap;
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalPengiriman.Text = DateTime.Now.ToString("d MMMM yyyy");

                Pengguna dmPengguna = new Pengguna();
                DropDownListPenggunaPIC.DataSource = dmPengguna.CariBawahanSemua(db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == pengguna.IDPengguna)).OrderBy(item => item.LevelJabatan).ThenBy(item => item.NamaLengkap);
                DropDownListPenggunaPIC.DataTextField = "NamaLengkap";
                DropDownListPenggunaPIC.DataValueField = "IDPengguna";
                DropDownListPenggunaPIC.DataBind();
                DropDownListPenggunaPIC.Items.Insert(0, new ListItem { Text = pengguna.NamaLengkap, Value = pengguna.IDPengguna.ToString() });

                TBBahanBaku[] daftarBahanBaku = db.TBBahanBakus.OrderBy(item => item.Nama).ToArray();

                if (daftarBahanBaku.Count() == 0)
                {
                    ButtonSimpanBahanBaku.Enabled = false;
                }
                else
                {
                    TBBahanBaku bahanBaku = daftarBahanBaku.FirstOrDefault();

                    DropDownListBahanBaku.DataSource = daftarBahanBaku;
                    DropDownListBahanBaku.DataTextField = "Nama";
                    DropDownListBahanBaku.DataValueField = "IDBahanBaku";
                    DropDownListBahanBaku.DataBind();

                    DropDownListSatuan.Items.Clear();
                    DropDownListSatuan.DataBind();
                    DropDownListSatuan.Items.Insert(0, new ListItem { Text = bahanBaku.TBSatuan1.Nama, Value = bahanBaku.IDSatuanKonversi.ToString() });

                    if (bahanBaku.TBSatuan != bahanBaku.TBSatuan1)
                    {
                        DropDownListSatuan.Items.Insert(1, new ListItem { Text = bahanBaku.TBSatuan.Nama, Value = bahanBaku.IDSatuan.ToString() });
                    }
                    HiddenFieldHargaBeli.Value = bahanBaku.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == pengguna.IDTempat).HargaBeli.ToString();
                    HiddenFieldKonversi.Value = bahanBaku.Konversi.ToString();
                }

                TBJenisBiayaProduksi[] daftarjenisBiayaProduksi = db.TBJenisBiayaProduksis.OrderBy(item => item.Nama).ToArray();
                if (daftarjenisBiayaProduksi.Count() == 0)
                {
                    ButtonSimpanBiayaTambahan.Enabled = false;
                }
                else
                {
                    DropDownListJenisBiayaProduksi.DataSource = daftarjenisBiayaProduksi;
                    DropDownListJenisBiayaProduksi.DataTextField = "Nama";
                    DropDownListJenisBiayaProduksi.DataValueField = "IDJenisBiayaProduksi";
                    DropDownListJenisBiayaProduksi.DataBind();
                }

                ViewState["ViewStateListDetail"] = new List<POProduksiDetail_Model>();
                ViewState["ViewStateListKomposisi"] = new List<POProduksiKomposisi_Model>();
                ViewState["ViewStateListBiayaTambahan"] = new List<POProduksiBiayaTambahan_Model>();

                if (!string.IsNullOrEmpty(Request.QueryString["baru"]))
                    LoadPOLama(db, Request.QueryString["baru"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                    LoadPOLama(db, Request.QueryString["edit"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
                    LoadProyeksi(db, Request.QueryString["proy"]);
                else
                {
                    DropDownListStokProduk.DataSource = db.TBStokProduks
                        .Where(item =>
                            item.IDTempat == pengguna.IDTempat &&
                            item.TBKombinasiProduk.TBProduk._IsActive)
                        .Select(item => new
                        {
                            item.IDStokProduk,
                            item.TBKombinasiProduk.Nama
                        })
                        .Distinct()
                        .OrderBy(item => item.Nama)
                        .ToArray();

                    DropDownListStokProduk.DataTextField = "Nama";
                    DropDownListStokProduk.DataValueField = "IDStokProduk";
                    DropDownListStokProduk.DataBind();

                    if (DropDownListStokProduk.Items.Count == 0)
                    {
                        ButtonSimpanDetail.Enabled = false;
                        ButtonSimpan.Enabled = false;
                    }
                }
            }
        }
    }

    private void LoadPOLama(DataClassesDatabaseDataContext db, string IDPOProduksiProduk)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);

        TextBoxIDProyeksi.Text = poProduksiProduk.IDProyeksi != null ? poProduksiProduk.IDProyeksi : string.Empty;
        TextBoxPegawai.Text = pengguna.NamaLengkap;
        DropDownListPenggunaPIC.SelectedValue = poProduksiProduk.IDPenggunaPIC.ToString();
        TextBoxTanggal.Text = poProduksiProduk.Tanggal.ToString("d MMMM yyyy");
        TextBoxTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman != null ? poProduksiProduk.TanggalPengiriman.Value.ToString("d MMMM yyyy") : DateTime.Now.ToString("d MMMM yyyy");

        TBStokProduk[] daftarStokProduk = null;
        if (TextBoxIDProyeksi.Text != string.Empty)
        {
            TBProyeksiDetail[] proyeksiDetail = db.TBProyeksiDetails.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
            daftarStokProduk = db.TBStokProduks.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiDetail.Any(data => data.IDKombinasiProduk == item.IDKombinasiProduk)).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
        }
        else
        {
            daftarStokProduk = db.TBStokProduks.Where(item => item.IDTempat == pengguna.IDTempat && item.TBKombinasiProduk.TBProduk._IsActive).ToArray();
        }
        DropDownListStokProduk.DataSource = daftarStokProduk
            .Select(item => new
            {
                item.IDStokProduk,
                item.TBKombinasiProduk.Nama
            })
            .Distinct()
            .OrderBy(item => item.Nama)
            .ToArray();

        DropDownListStokProduk.DataTextField = "Nama";
        DropDownListStokProduk.DataValueField = "IDStokProduk";
        DropDownListStokProduk.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in poProduksiProduk.TBPOProduksiProdukDetails)
        {
            TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
            detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
            detail.IDStokProduk = stokProduk.IDStokProduk;
            detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
            detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
            detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
            detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = 0;
            detail.Harga = 0;
            detail.PotonganHarga = 0;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Jumlah;
            detail.Sisa = detail.Jumlah;
            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        RadioButtonListStatusHPP.SelectedValue = poProduksiProduk.EnumJenisHPP.ToString();
        if (RadioButtonListStatusHPP.SelectedValue == ((int)PilihanEnumJenisHPP.Komposisi).ToString())
            PengaturanHPPKomposisi(db);
        else
        {
            List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
            List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

            ViewStateListKomposisi.AddRange(poProduksiProduk.TBPOProduksiProdukKomposisis.Select(item => new POProduksiKomposisi_Model
            {
                IDBahanBaku = item.IDBahanBaku,
                IDSatuan = item.IDSatuan,
                BahanBaku = item.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                HargaBeli = item.HargaBeli,
                JumlahKebutuhan = item.Kebutuhan,
                JumlahSisa = 0,
                JumlahKurang = 0
            }));
            ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;

            ViewStateListBiayaTambahan.AddRange(poProduksiProduk.TBPOProduksiProdukBiayaTambahans.Select(item => new POProduksiBiayaTambahan_Model
            {
                IDJenisBiayaProduksi = item.IDJenisBiayaProduksi,
                Nama = item.TBJenisBiayaProduksi.Nama,
                JumlahProduksi = 1,
                Biaya = item.Nominal
            }));
            ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;

            PengaturanHPPRataRata();
        }

        TextBoxKeterangan.Text = poProduksiProduk.Keterangan;
        LoadData();

    }
    private void LoadProyeksi(DataClassesDatabaseDataContext db, string IDProyeksi)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TextBoxIDProyeksi.Text = IDProyeksi;
        TextBoxPegawai.Text = pengguna.NamaLengkap;

        TBProyeksiDetail[] proyeksiDetail = db.TBProyeksiDetails.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
        TBStokProduk[] daftarStokProduk = db.TBStokProduks.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiDetail.Any(data => data.IDKombinasiProduk == item.IDKombinasiProduk)).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        DropDownListStokProduk.DataSource = daftarStokProduk.Select(item => new { item.IDStokProduk, item.TBKombinasiProduk.Nama }).Distinct().OrderBy(item => item.Nama).ToArray();
        DropDownListStokProduk.DataTextField = "Nama";
        DropDownListStokProduk.DataValueField = "IDStokProduk";
        DropDownListStokProduk.DataBind();

        foreach (var item in proyeksiDetail.Where(item => item.Sisa > 0))
        {
            TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
            detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
            detail.IDStokProduk = stokProduk.IDStokProduk;
            detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
            detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
            detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
            detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = 0;
            detail.Harga = 0;
            detail.PotonganHarga = 0;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Sisa;
            detail.Sisa = detail.Jumlah;
            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        PengaturanHPPKomposisi(db);

        LoadData();

    }

    protected void ButtonSimpanDetail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (TextBoxJumlah.Text.ToDecimal() > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

                    POProduksiDetail_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                        detail = new POProduksiDetail_Model();
                        detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
                        detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
                        detail.IDStokProduk = stokProduk.IDStokProduk;
                        detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
                        detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
                        detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
                        detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
                        detail.HargaPokokKomposisi = 0;
                        detail.BiayaTambahan = 0;
                        detail.TotalHPP = 0;
                        detail.Harga = 0;
                        detail.PotonganHarga = 0;
                        detail.TotalHarga = detail.Harga - detail.PotonganHarga;
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt();
                        detail.Sisa = detail.Jumlah;
                        ViewStateListDetail.Add(detail);
                    }
                    else
                    {
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt();
                        detail.Sisa = detail.Jumlah;
                    }

                    ViewState["ViewStateListDetail"] = ViewStateListDetail;

                    if (RadioButtonListStatusHPP.SelectedValue == ((int)PilihanEnumJenisHPP.Komposisi).ToString())
                        PengaturanHPPKomposisi(db);
                    else
                        PengaturanHPPRataRata();
                }

                LoadData();
            }
        }
    }

    private void LoadData()
    {
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

        RepeaterDetail.DataSource = ViewStateListDetail;
        RepeaterDetail.DataBind();

        if (ViewStateListDetail.Count == 0)
        {
            LabelTotalJumlah.Text = "0";
            LabelTotalSubtotal.Text = "0";
        }
        else
        {
            LabelTotalJumlah.Text = ViewStateListDetail.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelTotalSubtotal.Text = ViewStateListDetail.Sum(item => item.SubtotalHPP).ToFormatHarga();
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
        RepeaterKomposisi.DataSource = ViewStateListKomposisi
            .GroupBy(item => new
            {
                item.BahanBaku,
                item.Satuan
            })
            .Select(item => new
            {
                item.Key.BahanBaku,
                item.Key.Satuan,
                JumlahKebutuhan = item.Sum(x => x.JumlahKebutuhan),
                SubtotalKebutuhan = item.Sum(x => x.SubtotalKebutuhan)
            })
            .OrderBy(item => item.BahanBaku);
        RepeaterKomposisi.DataBind();

        if (ViewStateListKomposisi.Count == 0)
        {
            LabelTotalSubtotalKomposisi.Text = string.Empty;
        }
        else
        {
            LabelTotalSubtotalKomposisi.Text = ViewStateListKomposisi.Sum(item => item.SubtotalKebutuhan).ToFormatHarga();
        }
        ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;

        List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];
        RepeaterBiayaTambahan.DataSource = ViewStateListBiayaTambahan
            .GroupBy(item => new
            {
                item.Nama
            })
            .Select(item => new
            {
                item.Key.Nama,
                Biaya = item.Sum(x => x.SubtotalBiaya)
            })
            .OrderBy(item => item.Nama);
        RepeaterBiayaTambahan.DataBind();

        if (ViewStateListBiayaTambahan.Count == 0)
        {
            LabelTotalSubtotalBiayaTambahan.Text = string.Empty;
        }
        else
        {
            LabelTotalSubtotalBiayaTambahan.Text = ViewStateListBiayaTambahan.Sum(item => item.SubtotalBiaya).ToFormatHarga();
        }
        ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;
    }

    protected void RepeaterDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
        List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

        ViewStateListDetail.RemoveAll(item => item.IDKombinasiProduk == e.CommandArgument.ToInt());
        ViewStateListKomposisi.RemoveAll(item => item.IDKombinasiProduk == e.CommandArgument.ToInt());
        ViewStateListBiayaTambahan.RemoveAll(item => item.IDKombinasiProduk == e.CommandArgument.ToInt());

        ViewState["ViewStateListDetail"] = ViewStateListDetail;
        ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;
        ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;


        if (RadioButtonListStatusHPP.SelectedValue == ((int)PilihanEnumJenisHPP.Komposisi).ToString())
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PengaturanHPPKomposisi(db);
            }
        }
        else
            PengaturanHPPRataRata();

        LoadData();
    }

    protected void RadioButtonListStatusHPP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListStatusHPP.SelectedValue == ((int)PilihanEnumJenisHPP.Komposisi).ToString())
        {
            PanelTambahKomposisi.Visible = false;
            PanelTambahBiayaProduksi.Visible = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PengaturanHPPKomposisi(db);
            }
        }
        else
        {
            PanelTambahKomposisi.Visible = true;
            PanelTambahBiayaProduksi.Visible = true;

            List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
            List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

            ViewStateListKomposisi.Clear();
            ViewStateListBiayaTambahan.Clear();

            ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;
            ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;

            PengaturanHPPRataRata();
        }

        LoadData();
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
            List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
            List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

            if (ViewStateListDetail.Count > 0)
            {
                string IDPOProduksiProduk = string.Empty;
                TBPOProduksiProduk produksiProduk = null;
                bool statusBerhasil = false;

                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        peringatan.Visible = false;

                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                        {
                            produksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["edit"]);

                            if (produksiProduk.IDProyeksi != null)
                            {
                                foreach (var item in db.TBProyeksiDetails.Where(item => item.IDProyeksi == produksiProduk.IDProyeksi).OrderBy(data => data.TBKombinasiProduk.Nama).ToArray())
                                {
                                    TBPOProduksiProdukDetail poProduksiProdukDetail = produksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);
                                    if (poProduksiProdukDetail != null)
                                    {
                                        item.Sisa = item.Sisa + poProduksiProdukDetail.Jumlah;
                                    }
                                }
                            }

                            db.TBPOProduksiProdukDetails.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukDetails);
                            db.TBPOProduksiProdukKomposisis.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukKomposisis);
                            db.TBPOProduksiProdukBiayaTambahans.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukBiayaTambahans);
                            produksiProduk.TBPOProduksiProdukDetails.Clear();
                            produksiProduk.TBPOProduksiProdukKomposisis.Clear();
                            produksiProduk.TBPOProduksiProdukBiayaTambahans.Clear();

                            produksiProduk.IDTempat = pengguna.IDTempat;
                            produksiProduk.IDPengguna = pengguna.IDPengguna;
                            produksiProduk.EnumJenisProduksi = (int)PilihanEnumJenisProduksi.ProduksiSendiri;
                            produksiProduk.Tanggal = TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                        }
                        else
                        {
                            db.Proc_InsertPOProduksiProduk(ref IDPOProduksiProduk, pengguna.IDTempat, pengguna.IDPengguna, (int)PilihanEnumJenisProduksi.ProduksiSendiri, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                            produksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);
                        }

                        produksiProduk.IDProyeksi = TextBoxIDProyeksi.Text != string.Empty ? TextBoxIDProyeksi.Text : null;
                        produksiProduk.IDVendor = null;
                        produksiProduk.IDPenggunaPIC = DropDownListPenggunaPIC.SelectedValue.ToInt();
                        produksiProduk.IDPenggunaDP = null;
                        produksiProduk.IDJenisPOProduksi = null;
                        produksiProduk.IDJenisPembayaran = null;
                        produksiProduk.TanggalDownPayment = null;
                        produksiProduk.TanggalJatuhTempo = null;
                        produksiProduk.TanggalPengiriman = TextBoxTanggalPengiriman.Text.ToDateTime();
                        produksiProduk.TBPOProduksiProdukDetails.AddRange(ViewStateListDetail.Select(item => new TBPOProduksiProdukDetail
                        {
                            IDKombinasiProduk = item.IDKombinasiProduk,
                            HargaPokokKomposisi = item.HargaPokokKomposisi,
                            BiayaTambahan = item.BiayaTambahan,
                            TotalHPP = item.TotalHPP,
                            HargaVendor = item.Harga,
                            PotonganHargaVendor = item.PotonganHarga,
                            TotalHargaVendor = item.TotalHarga,
                            Jumlah = (int)item.Jumlah,
                            Sisa = (int)item.Sisa
                        }));

                        produksiProduk.TotalJumlah = produksiProduk.TBPOProduksiProdukDetails.Sum(item => item.Jumlah);
                        produksiProduk.EnumJenisHPP = RadioButtonListStatusHPP.SelectedValue.ToInt();
                        produksiProduk.SubtotalBiayaTambahan = produksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.Jumlah * item.BiayaTambahan));
                        produksiProduk.SubtotalTotalHPP = produksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.Jumlah * item.TotalHPP));
                        produksiProduk.SubtotalTotalHargaVendor = produksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.Jumlah * item.TotalHargaVendor));
                        produksiProduk.PotonganPOProduksiProduk = 0;
                        produksiProduk.BiayaLainLain = 0;
                        produksiProduk.PersentaseTax = 0;
                        produksiProduk.Tax = 0;
                        produksiProduk.Grandtotal = produksiProduk.SubtotalTotalHPP;
                        produksiProduk.DownPayment = 0;
                        produksiProduk.Keterangan = TextBoxKeterangan.Text;

                        produksiProduk.TBPOProduksiProdukKomposisis.AddRange(ViewStateListKomposisi.OrderBy(item => item.BahanBaku).GroupBy(item => new
                        {
                            item.IDBahanBaku,
                            item.IDSatuan,
                            item.HargaBeli
                        })
                        .Select(item => new TBPOProduksiProdukKomposisi
                        {
                            IDBahanBaku = item.Key.IDBahanBaku,
                            IDSatuan = item.Key.IDSatuan,
                            HargaBeli = item.Key.HargaBeli,
                            Kebutuhan = item.Sum(x => x.JumlahKebutuhan),
                            Kirim = 0,
                            Sisa = item.Sum(x => x.JumlahKebutuhan)
                        }));

                        produksiProduk.TBPOProduksiProdukBiayaTambahans.AddRange(ViewStateListBiayaTambahan.OrderBy(item => item.Nama).GroupBy(item => new
                        {
                            item.IDJenisBiayaProduksi
                        })
                        .Select(item => new TBPOProduksiProdukBiayaTambahan
                        {
                            IDJenisBiayaProduksi = item.Key.IDJenisBiayaProduksi,
                            Nominal = item.Sum(x => x.SubtotalBiaya)
                        }));

                        if (produksiProduk.IDProyeksi != null)
                        {
                            foreach (var item in db.TBProyeksiDetails.Where(item => item.IDProyeksi == produksiProduk.IDProyeksi).OrderBy(data => data.TBKombinasiProduk.Nama).ToArray())
                            {
                                TBPOProduksiProdukDetail poProduksiProdukDetail = produksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);
                                if (poProduksiProdukDetail != null)
                                {
                                    item.Sisa = item.Sisa - poProduksiProdukDetail.Jumlah;
                                }
                            }
                        }

                        db.SubmitChanges();

                        statusBerhasil = true;
                    }
                }
                catch (Exception ex)
                {
                    if (statusBerhasil != true && string.IsNullOrEmpty(Request.QueryString["edit"]))
                    {
                        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                        {
                            produksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);
                            if (produksiProduk != null)
                            {
                                db.TBPOProduksiProdukDetails.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukDetails);
                                db.TBPOProduksiProdukKomposisis.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukKomposisis);
                                db.TBPOProduksiProdukBiayaTambahans.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukBiayaTambahans);
                                db.TBPOProduksiProduks.DeleteOnSubmit(produksiProduk);
                                db.SubmitChanges();

                                IDPOProduksiProduk = string.Empty;
                            }
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Produksi Produk Sendiri (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
                    LabelPeringatan.Text = "Terjadi kesalahan, silahkan cek kembali data yang diinputkan";
                    peringatan.Visible = true;
                }
                finally
                {
                    if (statusBerhasil == true)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                            Response.Redirect("Detail.aspx?id=" + produksiProduk.IDPOProduksiProduk);
                        else
                            Response.Redirect("Default.aspx");
                    }
                }
            }
            else
            {
                LabelPeringatan.Text = "Tidak ada Produk yang dipilih";
                peringatan.Visible = true;
            }
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
            Response.Redirect("/WITAdministrator/Produk/Proyeksi/Detail.aspx?id=" + Request.QueryString["proy"]);
        else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
            Response.Redirect("/WITAdministrator/Produk/POProduksi/ProduksiSendiri/Detail.aspx?id=" + Request.QueryString["edit"]);
        else
            Response.Redirect("Default.aspx");
    }

    #region Komposisi
    private void PengaturanHPPKomposisi(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
        List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

        ViewStateListKomposisi.Clear();
        ViewStateListBiayaTambahan.Clear();

        TBKombinasiProduk[] daftarKombinasiProduk = db.TBKombinasiProduks.ToArray();

        foreach (var detail in ViewStateListDetail)
        {
            decimal hargaPokokKomposisi = 0;
            foreach (var item in daftarKombinasiProduk.FirstOrDefault(data => data.IDKombinasiProduk == detail.IDKombinasiProduk).TBKomposisiKombinasiProduks)
            {
                POProduksiKomposisi_Model komposisi = new POProduksiKomposisi_Model()
                {
                    IDKombinasiProduk = item.IDKombinasiProduk,
                    IDBahanBaku = item.IDBahanBaku,
                    IDSatuan = item.TBBahanBaku.IDSatuan,
                    BahanBaku = item.TBBahanBaku.Nama,
                    Satuan = item.TBBahanBaku.TBSatuan.Nama,
                    HargaBeli = item.TBBahanBaku.TBStokBahanBakus.FirstOrDefault(data => data.IDTempat == pengguna.IDTempat).HargaBeli.Value,
                    JumlahProduksi = detail.Jumlah,
                    JumlahKomposisi = item.Jumlah.Value,
                    JumlahKebutuhan = detail.Jumlah * item.Jumlah.Value,
                    JumlahSisa = 0,
                    JumlahKurang = 0
                };
                ViewStateListKomposisi.Add(komposisi);

                hargaPokokKomposisi += komposisi.SubtotalKomposisi;
            }

            decimal biayaTambahan = 0;
            foreach (var item in daftarKombinasiProduk.FirstOrDefault(data => data.IDKombinasiProduk == detail.IDKombinasiProduk).TBRelasiJenisBiayaProduksiKombinasiProduks)
            {
                POProduksiBiayaTambahan_Model biaya = new POProduksiBiayaTambahan_Model()
                {
                    IDKombinasiProduk = item.IDKombinasiProduk,
                    IDJenisBiayaProduksi = item.IDJenisBiayaProduksi,
                    Nama = item.TBJenisBiayaProduksi.Nama,
                    JumlahProduksi = detail.Jumlah,
                    Jenis = item.EnumBiayaProduksi == 1 ? (item.Persentase * 100).ToFormatHarga() + "% dari Komposisi Produk" : "Nominal",
                    EnumBiayaProduksi = item.EnumBiayaProduksi.Value,
                    Persentase = item.Persentase.Value,
                    Nominal = item.Nominal.Value,
                    Biaya = item.EnumBiayaProduksi.Value == 1 ? (item.Persentase.Value * ViewStateListKomposisi.Where(data => data.IDKombinasiProduk == item.IDKombinasiProduk).Sum(data => data.SubtotalKomposisi)) : item.Nominal.Value
                };
                ViewStateListBiayaTambahan.Add(biaya);

                biayaTambahan += biaya.Biaya;
            }

            detail.HargaPokokKomposisi = hargaPokokKomposisi;
            detail.BiayaTambahan = biayaTambahan;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;
        ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;
        ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;
    }
    #endregion

    #region Komposisi Rata Rata
    private void PengaturanHPPRataRata()
    {
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
        List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

        if (ViewStateListDetail.Count > 0)
        {
            decimal rataRataHargaPokokKomposisi = ViewStateListKomposisi.Sum(item => item.SubtotalKebutuhan) / ViewStateListDetail.Sum(item => item.Jumlah);
            decimal rataRataBiayaTambahan = ViewStateListBiayaTambahan.Sum(item => item.SubtotalBiaya) / ViewStateListDetail.Sum(item => item.Jumlah);

            ViewStateListDetail.ForEach(item => item.HargaPokokKomposisi = rataRataHargaPokokKomposisi);
            ViewStateListDetail.ForEach(item => item.BiayaTambahan = rataRataBiayaTambahan);
            ViewStateListDetail.ForEach(item => item.TotalHPP = (rataRataHargaPokokKomposisi + rataRataBiayaTambahan));
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;
        ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;
        ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;
    }

    protected void DropDownListBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            TBBahanBaku bahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt());
            DropDownListSatuan.Items.Clear();
            DropDownListSatuan.DataBind();
            DropDownListSatuan.Items.Insert(0, new ListItem { Text = bahanBaku.TBSatuan1.Nama, Value = bahanBaku.IDSatuanKonversi.ToString() });

            if (bahanBaku.TBSatuan != bahanBaku.TBSatuan1)
            {
                DropDownListSatuan.Items.Insert(1, new ListItem { Text = bahanBaku.TBSatuan.Nama, Value = bahanBaku.IDSatuan.ToString() });
            }
            HiddenFieldHargaBeli.Value = bahanBaku.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == pengguna.IDTempat).HargaBeli.ToString();
            HiddenFieldKonversi.Value = bahanBaku.Konversi.ToString();
        }
    }

    protected void ButtonSimpanBahanBaku_Click(object sender, EventArgs e)
    {
        List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];

        ViewStateListKomposisi.RemoveAll(item => item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt());

        if (TextBoxJumlahBahanBaku.Text.ToDecimal() > 0)
        {
            ViewStateListKomposisi.Add(new POProduksiKomposisi_Model()
            {
                IDBahanBaku = DropDownListBahanBaku.SelectedValue.ToInt(),
                IDSatuan = DropDownListSatuan.Items.Count == 1 ? DropDownListSatuan.Items[0].Value.ToInt() : DropDownListSatuan.Items[1].Value.ToInt(),
                BahanBaku = DropDownListBahanBaku.SelectedItem.Text,
                Satuan = DropDownListSatuan.Items.Count == 1 ? DropDownListSatuan.Items[0].Text : DropDownListSatuan.Items[1].Text,
                HargaBeli = HiddenFieldHargaBeli.Value.ToDecimal(),
                JumlahKebutuhan = DropDownListSatuan.SelectedIndex == 0 ? TextBoxJumlahBahanBaku.Text.ToDecimal() * HiddenFieldKonversi.Value.ToDecimal() : TextBoxJumlahBahanBaku.Text.ToDecimal(),
                JumlahSisa = 0,
                JumlahKurang = 0
            });
        }


        ViewState["ViewStateListKomposisi"] = ViewStateListKomposisi;

        PengaturanHPPRataRata();

        LoadData();

        TextBoxJumlahBahanBaku.Text = "0";
    }

    protected void ButtonSimpanBiayaTambahan_Click(object sender, EventArgs e)
    {
        List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

        ViewStateListBiayaTambahan.RemoveAll(item => item.IDJenisBiayaProduksi == DropDownListJenisBiayaProduksi.SelectedValue.ToInt());

        if (TextBoxJumlahBiayaTambahan.Text.ToDecimal() > 0)
        {
            ViewStateListBiayaTambahan.Add(new POProduksiBiayaTambahan_Model()
            {
                IDJenisBiayaProduksi = DropDownListJenisBiayaProduksi.SelectedValue.ToInt(),
                Nama = DropDownListJenisBiayaProduksi.SelectedItem.Text,
                JumlahProduksi = 1,
                Biaya = TextBoxJumlahBiayaTambahan.Text.ToDecimal()
            });
        }

        ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;

        PengaturanHPPRataRata();

        LoadData();

        TextBoxJumlahBiayaTambahan.Text = "0";
    }
    #endregion
}