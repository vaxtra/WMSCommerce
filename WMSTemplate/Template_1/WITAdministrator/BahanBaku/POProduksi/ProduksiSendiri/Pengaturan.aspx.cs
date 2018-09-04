using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_ProduksiSendiri_Pengaturan : System.Web.UI.Page
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
                    DropDownListJenisBiayaProduksi.DataSource = db.TBJenisBiayaProduksis.Select(item => new { item.IDJenisBiayaProduksi, item.Nama }).OrderBy(item => item.Nama).ToArray();
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
                else if (!string.IsNullOrEmpty(Request.QueryString["proy"]) && !string.IsNullOrEmpty(Request.QueryString["level"]))
                    LoadProyeksi(db, Request.QueryString["proy"], Request.QueryString["level"]);
                else
                {
                    TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
                    DropDownListStokBahanBaku.DataSource = daftarStokBahanBaku.Select(item => new { item.IDStokBahanBaku, item.TBBahanBaku.Nama }).OrderBy(item => item.Nama).ToArray();
                    DropDownListStokBahanBaku.DataTextField = "Nama";
                    DropDownListStokBahanBaku.DataValueField = "IDStokBahanBaku";
                    DropDownListStokBahanBaku.DataBind();

                    if (DropDownListStokBahanBaku.Items.Count == 0)
                    {
                        ButtonSimpanDetail.Enabled = false;
                        ButtonSimpan.Enabled = false;
                    }
                    else
                    {
                        LabelSatuan.Text = "/" + daftarStokBahanBaku.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt()).TBBahanBaku.TBSatuan1.Nama;
                    }
                }
            }
        }
    }

    private void LoadPOLama(DataClassesDatabaseDataContext db, string IDPOProduksiBahanBaku)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);

        TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : string.Empty;
        TextBoxPegawai.Text = pengguna.NamaLengkap;
        DropDownListPenggunaPIC.SelectedValue = poProduksiBahanBaku.IDPenggunaPIC.ToString();
        TextBoxTanggal.Text = poProduksiBahanBaku.Tanggal.ToString("d MMMM yyyy");
        TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman != null ? poProduksiBahanBaku.TanggalPengiriman.Value.ToString("d MMMM yyyy") : DateTime.Now.ToString("d MMMM yyyy");

        TBStokBahanBaku[] daftarStokBahanBaku = null;
        if (TextBoxIDProyeksi.Text != string.Empty)
        {
            if (poProduksiBahanBaku.LevelProduksi != null)
            {
                TBProyeksiKomposisi[] proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text && item.LevelProduksi == poProduksiBahanBaku.LevelProduksi && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray();
                daftarStokBahanBaku = db.TBStokBahanBakus.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiKomposisi.Any(data => data.IDBahanBaku == item.IDBahanBaku)).OrderBy(item => item.TBBahanBaku.Nama).ToArray();
            }
            else
            {
                TBProyeksiKomposisi[] proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray();
                daftarStokBahanBaku = db.TBStokBahanBakus.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiKomposisi.Any(data => data.IDBahanBaku == item.IDBahanBaku)).OrderBy(item => item.TBBahanBaku.Nama).ToArray();
            }
        }
        else
        {
            daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
        }
        DropDownListStokBahanBaku.DataSource = daftarStokBahanBaku.Select(item => new { item.IDStokBahanBaku, item.TBBahanBaku.Nama });
        DropDownListStokBahanBaku.DataTextField = "Nama";
        DropDownListStokBahanBaku.DataValueField = "IDStokBahanBaku";
        DropDownListStokBahanBaku.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in poProduksiBahanBaku.TBPOProduksiBahanBakuDetails)
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDBahanBaku = stokBahanBaku.TBBahanBaku.IDBahanBaku;
            detail.IDSatuan = item.IDSatuan;
            detail.IDStokBahanBaku = stokBahanBaku.IDStokBahanBaku;
            detail.Kode = stokBahanBaku.TBBahanBaku.KodeBahanBaku;
            detail.BahanBaku = stokBahanBaku.TBBahanBaku.Nama;
            detail.Satuan = item.TBSatuan.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
            detail.Harga = 0;
            detail.PotonganHarga = 0;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Jumlah;
            detail.Sisa = detail.Jumlah;
            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        LabelSatuan.Text = "/" + daftarStokBahanBaku.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt()).TBBahanBaku.TBSatuan1.Nama;

        RadioButtonListStatusHPP.SelectedValue = poProduksiBahanBaku.EnumJenisHPP.ToString();
        if (RadioButtonListStatusHPP.SelectedValue == ((int)PilihanEnumJenisHPP.Komposisi).ToString())
            PengaturanHPPKomposisi(db);
        else
        {
            List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
            List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

            ViewStateListKomposisi.AddRange(poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.Select(item => new POProduksiKomposisi_Model
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

            ViewStateListBiayaTambahan.AddRange(poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.Select(item => new POProduksiBiayaTambahan_Model
            {
                IDJenisBiayaProduksi = item.IDJenisBiayaProduksi,
                Nama = item.TBJenisBiayaProduksi.Nama,
                JumlahProduksi = 1,
                Biaya = item.Nominal
            }));
            ViewState["ViewStateListBiayaTambahan"] = ViewStateListBiayaTambahan;

            PengaturanHPPRataRata();
        }

        TextBoxKeterangan.Text = poProduksiBahanBaku.Keterangan;
        LoadData();
    }

    private void LoadProyeksi(DataClassesDatabaseDataContext db, string IDProyeksi, string level)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TextBoxIDProyeksi.Text = IDProyeksi;
        TextBoxPegawai.Text = pengguna.NamaLengkap;

        TBProyeksiKomposisi[] proyeksiKomposisi = null;
        if (!string.IsNullOrEmpty(level))
        {
            proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == IDProyeksi && item.LevelProduksi == level.ToInt() && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray();
        }
        else
        {
            proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == IDProyeksi && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray();
        }

        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat).OrderBy(item => item.TBBahanBaku.Nama).ToArray();
        DropDownListStokBahanBaku.DataSource = daftarStokBahanBaku.Select(item => new { item.IDStokBahanBaku, item.TBBahanBaku.Nama }).ToArray();
        DropDownListStokBahanBaku.DataTextField = "Nama";
        DropDownListStokBahanBaku.DataValueField = "IDStokBahanBaku";
        DropDownListStokBahanBaku.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in proyeksiKomposisi.Where(item => item.Sisa > 0))
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDBahanBaku = stokBahanBaku.TBBahanBaku.IDBahanBaku;
            detail.IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuanKonversi;
            detail.IDStokBahanBaku = stokBahanBaku.IDStokBahanBaku;
            detail.Kode = stokBahanBaku.TBBahanBaku.KodeBahanBaku;
            detail.BahanBaku = stokBahanBaku.TBBahanBaku.Nama;
            detail.Satuan = stokBahanBaku.TBBahanBaku.TBSatuan1.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
            detail.Harga = 0;
            detail.PotonganHarga = 0;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Sisa / stokBahanBaku.TBBahanBaku.Konversi.Value;
            detail.Sisa = detail.Jumlah;
            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        LabelSatuan.Text = "/" + daftarStokBahanBaku.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt()).TBBahanBaku.TBSatuan1.Nama;

        PengaturanHPPKomposisi(db);

        LoadData();
    }

    protected void DropDownListStokBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LabelSatuan.Text = "/" + db.TBStokBahanBakus.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt()).TBBahanBaku.TBSatuan1.Nama;
        }
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

                    POProduksiDetail_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

                        detail = new POProduksiDetail_Model();
                        detail.IDBahanBaku = stokBahanBaku.TBBahanBaku.IDBahanBaku;
                        detail.IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuanKonversi;
                        detail.IDStokBahanBaku = stokBahanBaku.IDStokBahanBaku;
                        detail.Kode = stokBahanBaku.TBBahanBaku.KodeBahanBaku;
                        detail.BahanBaku = stokBahanBaku.TBBahanBaku.Nama;
                        detail.Satuan = stokBahanBaku.TBBahanBaku.TBSatuan1.Nama;
                        detail.HargaPokokKomposisi = 0;
                        detail.BiayaTambahan = 0;
                        detail.TotalHPP = 0;
                        detail.Harga = 0;
                        detail.PotonganHarga = 0;
                        detail.TotalHarga = detail.Harga - detail.PotonganHarga;
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal();
                        detail.Sisa = detail.Jumlah;
                        ViewStateListDetail.Add(detail);
                    }
                    else
                    {
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal();
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
            LabelTotalJumlah.Text = ViewStateListDetail.Sum(item => item.Jumlah).ToFormatHarga();
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

        ViewStateListDetail.RemoveAll(item => item.IDBahanBaku == e.CommandArgument.ToInt());
        ViewStateListKomposisi.RemoveAll(item => item.IDBahanBakuProduksi == e.CommandArgument.ToInt());
        ViewStateListBiayaTambahan.RemoveAll(item => item.IDBahanBakuProduksi == e.CommandArgument.ToInt());

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
            peringatan.Visible = false;
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
            List<POProduksiKomposisi_Model> ViewStateListKomposisi = (List<POProduksiKomposisi_Model>)ViewState["ViewStateListKomposisi"];
            List<POProduksiBiayaTambahan_Model> ViewStateListBiayaTambahan = (List<POProduksiBiayaTambahan_Model>)ViewState["ViewStateListBiayaTambahan"];

            if (ViewStateListDetail.Count > 0)
            {
                string IDPOProduksiBahanBaku = string.Empty;
                TBPOProduksiBahanBaku produksiBahanBaku = null;
                bool statusBerhasil = false;

                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                        {
                            produksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["edit"]);

                            if (produksiBahanBaku.IDProyeksi != null)
                            {
                                foreach (var item in db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == produksiBahanBaku.IDProyeksi && item.LevelProduksi == produksiBahanBaku.LevelProduksi && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray())
                                {
                                    TBPOProduksiBahanBakuDetail poProduksiBahanBakuDetail = produksiBahanBaku.TBPOProduksiBahanBakuDetails.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                                    if (poProduksiBahanBakuDetail != null)
                                    {
                                        item.Sisa = item.Sisa + (poProduksiBahanBakuDetail.Jumlah * poProduksiBahanBakuDetail.TBBahanBaku.Konversi.Value);
                                    }
                                }
                            }

                            db.TBPOProduksiBahanBakuDetails.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuDetails);
                            db.TBPOProduksiBahanBakuKomposisis.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuKomposisis);
                            db.TBPOProduksiBahanBakuBiayaTambahans.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans);
                            produksiBahanBaku.TBPOProduksiBahanBakuDetails.Clear();
                            produksiBahanBaku.TBPOProduksiBahanBakuKomposisis.Clear();
                            produksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.Clear();

                            produksiBahanBaku.IDTempat = pengguna.IDTempat;
                            produksiBahanBaku.IDPengguna = pengguna.IDPengguna;
                            produksiBahanBaku.EnumJenisProduksi = (int)PilihanEnumJenisProduksi.ProduksiSendiri;
                            produksiBahanBaku.Tanggal = TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                        }
                        else
                        {
                            db.Proc_InsertPOProduksiBahanBaku(ref IDPOProduksiBahanBaku, pengguna.IDTempat, pengguna.IDPengguna, (int)PilihanEnumJenisProduksi.ProduksiSendiri, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                            produksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);
                        }

                        produksiBahanBaku.IDProyeksi = TextBoxIDProyeksi.Text != string.Empty ? TextBoxIDProyeksi.Text : null;
                        produksiBahanBaku.IDSupplier = null;
                        produksiBahanBaku.IDPenggunaPIC = DropDownListPenggunaPIC.SelectedValue.ToInt();
                        produksiBahanBaku.IDPenggunaDP = null;
                        produksiBahanBaku.IDJenisPOProduksi = null;
                        produksiBahanBaku.IDJenisPembayaran = null;

                        if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
                        {
                            produksiBahanBaku.LevelProduksi = Request.QueryString["level"].ToInt();
                        }
                        else if (string.IsNullOrEmpty(Request.QueryString["proy"]) && string.IsNullOrEmpty(Request.QueryString["edit"]))
                        {
                            produksiBahanBaku.LevelProduksi = null;
                        }
                        produksiBahanBaku.TanggalDownPayment = null;
                        produksiBahanBaku.TanggalJatuhTempo = null;
                        produksiBahanBaku.TanggalPengiriman = TextBoxTanggalPengiriman.Text.ToDateTime();
                        produksiBahanBaku.TBPOProduksiBahanBakuDetails.AddRange(ViewStateListDetail.Select(item => new TBPOProduksiBahanBakuDetail
                        {
                            IDBahanBaku = item.IDBahanBaku,
                            IDSatuan = item.IDSatuan,
                            HargaPokokKomposisi = item.HargaPokokKomposisi,
                            BiayaTambahan = item.BiayaTambahan,
                            TotalHPP = item.TotalHPP,
                            HargaSupplier = item.Harga,
                            PotonganHargaSupplier = item.PotonganHarga,
                            TotalHargaSupplier = item.TotalHarga,
                            Jumlah = item.Jumlah,
                            Sisa = item.Sisa
                        }));

                        produksiBahanBaku.TotalJumlah = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => item.Jumlah);
                        produksiBahanBaku.EnumJenisHPP = RadioButtonListStatusHPP.SelectedValue.ToInt();
                        produksiBahanBaku.SubtotalBiayaTambahan = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.Jumlah * item.BiayaTambahan));
                        produksiBahanBaku.SubtotalTotalHPP = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.Jumlah * item.TotalHPP));
                        produksiBahanBaku.SubtotalTotalHargaSupplier = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.Jumlah * item.TotalHargaSupplier));
                        produksiBahanBaku.PotonganPOProduksiBahanBaku = 0;
                        produksiBahanBaku.BiayaLainLain = 0;
                        produksiBahanBaku.PersentaseTax = 0;
                        produksiBahanBaku.Tax = 0;
                        produksiBahanBaku.Grandtotal = produksiBahanBaku.SubtotalTotalHPP;
                        produksiBahanBaku.DownPayment = 0;
                        produksiBahanBaku.Keterangan = TextBoxKeterangan.Text;

                        produksiBahanBaku.TBPOProduksiBahanBakuKomposisis.AddRange(ViewStateListKomposisi.OrderBy(item => item.BahanBaku).GroupBy(item => new
                        {
                            item.IDBahanBaku,
                            item.IDSatuan,
                            item.HargaBeli
                        })
                        .Select(item => new TBPOProduksiBahanBakuKomposisi
                        {
                            IDBahanBaku = item.Key.IDBahanBaku,
                            IDSatuan = item.Key.IDSatuan,
                            HargaBeli = item.Key.HargaBeli,
                            Kebutuhan = item.Sum(x => x.JumlahKebutuhan),
                            Kirim = 0,
                            Sisa = item.Sum(x => x.JumlahKebutuhan)
                        }));

                        produksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.AddRange(ViewStateListBiayaTambahan.OrderBy(item => item.Nama).GroupBy(item => new
                        {
                            item.IDJenisBiayaProduksi
                        })
                        .Select(item => new TBPOProduksiBahanBakuBiayaTambahan
                        {
                            IDJenisBiayaProduksi = item.Key.IDJenisBiayaProduksi,
                            Nominal = item.Sum(x => x.SubtotalBiaya)
                        }));

                        if (produksiBahanBaku.IDProyeksi != null)
                        {
                            foreach (var item in db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == produksiBahanBaku.IDProyeksi && item.LevelProduksi == produksiBahanBaku.LevelProduksi && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray())
                            {
                                TBPOProduksiBahanBakuDetail poProduksiBahanBakuDetail = produksiBahanBaku.TBPOProduksiBahanBakuDetails.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                                if (poProduksiBahanBakuDetail != null)
                                {
                                    item.Sisa = item.Sisa - (poProduksiBahanBakuDetail.Jumlah * poProduksiBahanBakuDetail.TBBahanBaku.Konversi.Value);
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
                            produksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);
                            if (produksiBahanBaku != null)
                            {
                                db.TBPOProduksiBahanBakuDetails.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuDetails);
                                db.TBPOProduksiBahanBakuKomposisis.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuKomposisis);
                                db.TBPOProduksiBahanBakuBiayaTambahans.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans);
                                db.TBPOProduksiBahanBakus.DeleteOnSubmit(produksiBahanBaku);
                                db.SubmitChanges();

                                IDPOProduksiBahanBaku = string.Empty;
                            }
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Produksi Bahan Baku Sendiri (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
                    LabelPeringatan.Text = "Terjadi kesalahan, silahkan cek kembali data yang diinputkan";
                    peringatan.Visible = true;
                }
                finally
                {
                    if (statusBerhasil == true)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                            Response.Redirect("Detail.aspx?id=" + produksiBahanBaku.IDPOProduksiBahanBaku);
                        else
                            Response.Redirect("Default.aspx");
                    }
                }
            }
            else
            {
                LabelPeringatan.Text = "Tidak ada Bahan Baku yang dipilih";
                peringatan.Visible = true;
            }
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
            Response.Redirect("/WITAdministrator/Produk/Proyeksi/Detail.aspx?id=" + Request.QueryString["proy"]);
        else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
            Response.Redirect("/WITAdministrator/BahanBaku/POProduksi/ProduksiSendiri/Detail.aspx?id=" + Request.QueryString["edit"]);
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

        TBBahanBaku[] daftarBahanBaku = db.TBBahanBakus.ToArray();

        foreach (var detail in ViewStateListDetail)
        {
            decimal hargaPokokKomposisi = 0;
            foreach (var item in daftarBahanBaku.FirstOrDefault(data => data.IDBahanBaku == detail.IDBahanBaku).TBKomposisiBahanBakus)
            {
                POProduksiKomposisi_Model komposisi = new POProduksiKomposisi_Model()
                {
                    IDBahanBakuProduksi = item.IDBahanBakuProduksi,
                    IDBahanBaku = item.IDBahanBaku,
                    IDSatuan = item.TBBahanBaku1.IDSatuan,
                    BahanBaku = item.TBBahanBaku1.Nama,
                    Satuan = item.TBBahanBaku1.TBSatuan.Nama,
                    HargaBeli = item.TBBahanBaku1.TBStokBahanBakus.FirstOrDefault(data => data.IDTempat == pengguna.IDTempat).HargaBeli.Value,
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
            foreach (var item in daftarBahanBaku.FirstOrDefault(data => data.IDBahanBaku == detail.IDBahanBaku).TBRelasiJenisBiayaProduksiBahanBakus)
            {
                POProduksiBiayaTambahan_Model biaya = new POProduksiBiayaTambahan_Model()
                {
                    IDBahanBakuProduksi = item.IDBahanBaku,
                    IDJenisBiayaProduksi = item.IDJenisBiayaProduksi,
                    Nama = item.TBJenisBiayaProduksi.Nama,
                    JumlahProduksi = detail.Jumlah,
                    Jenis = item.EnumBiayaProduksi == 1 ? (item.Persentase * 100).ToFormatHarga() + "% dari Komposisi Produk" : "Nominal",
                    EnumBiayaProduksi = item.EnumBiayaProduksi.Value,
                    Persentase = item.Persentase.Value,
                    Nominal = item.Nominal.Value,
                    Biaya = item.EnumBiayaProduksi.Value == 1 ? (item.Persentase.Value * ViewStateListKomposisi.Where(data => data.IDBahanBakuProduksi == item.IDBahanBaku).Sum(data => data.SubtotalKomposisi)) : item.Nominal.Value
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

        TextBoxJumlahBahanBaku.Text = "0.00";
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

        TextBoxJumlahBiayaTambahan.Text = "0.00";
    }
    #endregion
}