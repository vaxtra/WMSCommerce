using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_POProduksi_PerformancePengguna : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                #region Performance Purchasing
                TextBoxTanggalAwal.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                    LabelPeriode.Text = TextBoxTanggalAwal.Text;
                else
                    LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                DropDownListCariPengguna.Items.AddRange(ClassPengguna.DropDownList(false));
                DropDownListCariPengguna.SelectedValue = pengguna.IDPengguna.ToString();

                LoadData(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));
                #endregion

                MultiViewPerformance.SetActiveView(ViewGrafik);
            }
        }
    }

    #region View PIC
    protected void DropDownListCariPengguna_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));
    }
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        LoadData(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));
    }
    private void LoadData(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwal.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                LabelPeriode.Text = TextBoxTanggalAwal.Text;
            else
                LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

            TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = db.TBPOProduksiBahanBakus.Where(item => item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();
            TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();

            #region PO Selesai
            RepeaterPOProduksiBahanBaku.DataSource = db.TBPenggunas.AsEnumerable().Select(item => new
            {
                pengguna = item,
                Order = daftarPOProduksiBahanBaku.Where(item2 => item2.IDPenggunaPIC == item.IDPengguna).Sum(item2 => item2.TotalJumlah),
                Receive = daftarPOProduksiBahanBaku.Where(item2 => item2.IDPenggunaPIC == item.IDPengguna).Sum(item2 => item2.TBPenerimaanPOProduksiBahanBakus.Sum(item3 => item3.TotalDiterima)),
            }).Select(item => new
            {
                CommandName = "POProduksiBahanBaku",
                item.pengguna.IDPengguna,
                GrupPengguna = item.pengguna.TBGrupPengguna.Nama,
                NamaLengkap = item.pengguna.NamaLengkap,
                item.Order,
                item.Receive,
                Progress = item.Order == 0 ? -1 : (item.Receive / item.Order) * 100
            }).OrderByDescending(item => item.Progress).ThenBy(item => item.NamaLengkap);
            RepeaterPOProduksiBahanBaku.DataBind();

            RepeaterPOProduksiProduk.DataSource = db.TBPenggunas.AsEnumerable().Select(item => new
            {
                pengguna = item,
                Order = daftarPOProduksiProduk.Where(item2 => item2.IDPenggunaPIC == item.IDPengguna).Sum(item2 => item2.TotalJumlah),
                Receive = daftarPOProduksiProduk.Where(item2 => item2.IDPenggunaPIC == item.IDPengguna).Sum(item2 => item2.TBPenerimaanPOProduksiProduks.Sum(item3 => item3.TotalDiterima)),
            }).Select(item => new
            {
                CommandName = "POProduksiProduk",
                item.pengguna.IDPengguna,
                GrupPengguna = item.pengguna.TBGrupPengguna.Nama,
                NamaLengkap = item.pengguna.NamaLengkap,
                item.Order,
                item.Receive,
                Progress = item.Order == 0 ? -1 : (item.Receive / item.Order) * 100
            }).OrderByDescending(item => item.Progress).ThenBy(item => item.NamaLengkap);
            RepeaterPOProduksiProduk.DataBind();
            #endregion
        }
    }
    #endregion

    #region View Purchase Order
    protected void RepeaterPOProduksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewPOProduksi);
        ButtonKembali.Visible = true;
        ButtonKembaliPOProduksi.Visible = false;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            headPurchaseOrder.Visible = true;
            bodyPurchaseOrder.Visible = true;
            headProduksiSendiri.Visible = true;
            bodyProduksiSendiri.Visible = true;
            headProduksiKeSupplier.Visible = true;
            bodyProduksiKeSupplier.Visible = true;

            TBPengguna pengguna = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == e.CommandArgument.ToInt());

            TextBoxGrupPengguna.Text = pengguna.TBGrupPengguna.Nama;
            TextBoxNamaLengkap.Text = pengguna.NamaLengkap;

            if (e.CommandName == "POProduksiBahanBaku")
            {
                List<TBPOProduksiBahanBaku> daftarPOProduksiBahanBaku = new List<TBPOProduksiBahanBaku>();
                daftarPOProduksiBahanBaku.AddRange(pengguna.TBPOProduksiBahanBakus1.Where(item => item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date));
                CariPOProduksiBahanBaku(daftarPOProduksiBahanBaku, pengguna);

                RepeaterPurchaseOrder.DataSource = daftarPOProduksiBahanBaku.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(item => new
                {
                    CommandName = "POProduksiBahanBakuDetail",
                    item.EnumJenisProduksi,
                    IDPOProduksi = item.IDPOProduksiBahanBaku,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    Tanggal_ClassJatuhTempo = WarnaTanggal(item.Tanggal),
                    TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHarga(),
                    TotalSisa = item.TBPOProduksiBahanBakuDetails.Sum(data => data.Sisa).ToFormatHarga(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterPurchaseOrder.DataBind();
                if (RepeaterPurchaseOrder.Items.Count == 0)
                {
                    headPurchaseOrder.Visible = false;
                    bodyPurchaseOrder.Visible = false;
                }

                RepeaterProduksiSendiri.DataSource = daftarPOProduksiBahanBaku.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(item => new
                {
                    CommandName = "POProduksiBahanBakuDetail",
                    item.EnumJenisProduksi,
                    IDPOProduksi = item.IDPOProduksiBahanBaku,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    Tanggal_ClassJatuhTempo = WarnaTanggal(item.Tanggal),
                    TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHarga(),
                    TotalSisa = item.TBPOProduksiBahanBakuDetails.Sum(data => data.Sisa).ToFormatHarga(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterProduksiSendiri.DataBind();
                if (RepeaterProduksiSendiri.Items.Count == 0)
                {
                    headProduksiSendiri.Visible = false;
                    bodyProduksiSendiri.Visible = false;
                }

                RepeaterProduksiKeSupplier.DataSource = daftarPOProduksiBahanBaku.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(item => new
                {
                    CommandName = "POProduksiBahanBakuDetail",
                    item.EnumJenisProduksi,
                    IDPOProduksi = item.IDPOProduksiBahanBaku,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    Tanggal_ClassJatuhTempo = WarnaTanggal(item.Tanggal),
                    TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHarga(),
                    TotalSisa = item.TBPOProduksiBahanBakuDetails.Sum(data => data.Sisa).ToFormatHarga(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterProduksiKeSupplier.DataBind();
                if (RepeaterProduksiKeSupplier.Items.Count == 0)
                {
                    headProduksiKeSupplier.Visible = false;
                    bodyProduksiKeSupplier.Visible = false;
                }
            }
            else if (e.CommandName == "POProduksiProduk")
            {
                List<TBPOProduksiProduk> daftarPOProduksiProduk = new List<TBPOProduksiProduk>();
                daftarPOProduksiProduk.AddRange(pengguna.TBPOProduksiProduks1.Where(item => item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date));
                CariPOProduksiProduk(daftarPOProduksiProduk, pengguna);

                RepeaterPurchaseOrder.DataSource = daftarPOProduksiProduk.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(item => new
                {
                    CommandName = "POProduksiProdukDetail",
                    item.EnumJenisProduksi,
                    IDPOProduksi = item.IDPOProduksiProduk,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    Tanggal_ClassJatuhTempo = WarnaTanggal(item.Tanggal),
                    TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
                    TotalSisa = item.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterPurchaseOrder.DataBind();
                if (RepeaterPurchaseOrder.Items.Count == 0)
                {
                    headPurchaseOrder.Visible = false;
                    bodyPurchaseOrder.Visible = false;
                }

                RepeaterProduksiSendiri.DataSource = daftarPOProduksiProduk.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(item => new
                {
                    CommandName = "POProduksiProdukDetail",
                    item.EnumJenisProduksi,
                    IDPOProduksi = item.IDPOProduksiProduk,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    Tanggal_ClassJatuhTempo = WarnaTanggal(item.Tanggal),
                    TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
                    TotalSisa = item.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterProduksiSendiri.DataBind();
                if (RepeaterProduksiSendiri.Items.Count == 0)
                {
                    headProduksiSendiri.Visible = false;
                    bodyProduksiSendiri.Visible = false;
                }

                RepeaterProduksiKeSupplier.DataSource = daftarPOProduksiProduk.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(item => new
                {
                    CommandName = "POProduksiProdukDetail",
                    item.EnumJenisProduksi,
                    IDPOProduksi = item.IDPOProduksiProduk,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    Tanggal_ClassJatuhTempo = WarnaTanggal(item.Tanggal),
                    TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
                    TotalSisa = item.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterProduksiKeSupplier.DataBind();
                if (RepeaterProduksiKeSupplier.Items.Count == 0)
                {
                    headProduksiKeSupplier.Visible = false;
                    bodyProduksiKeSupplier.Visible = false;
                }
            }
        }
    }

    private void CariPOProduksiBahanBaku(List<TBPOProduksiBahanBaku> daftarPOProduksiBahanBaku, TBPengguna pengguna)
    {
        foreach (var item in pengguna.TBPenggunas)
        {
            daftarPOProduksiBahanBaku.AddRange(item.TBPOProduksiBahanBakus1.Where(item2 => item2.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item2.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date));

            if (pengguna.TBPenggunas.Count > 0)
                CariPOProduksiBahanBaku(daftarPOProduksiBahanBaku, item);
        }
    }
    private void CariPOProduksiProduk(List<TBPOProduksiProduk> daftarPOProduksiProduk, TBPengguna pengguna)
    {
        foreach (var item in pengguna.TBPenggunas)
        {
            daftarPOProduksiProduk.AddRange(item.TBPOProduksiProduks1.Where(item2 => item2.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item2.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date));

            if (pengguna.TBPenggunas.Count > 0)
                CariPOProduksiProduk(daftarPOProduksiProduk, item);
        }
    }

    private string WarnaTanggal(DateTime tanggal)
    {
        if (tanggal.Date > DateTime.Now.Date)
            return "fitSize";
        else if (tanggal.Date == DateTime.Now.Date)
            return "fitSize warning";
        else
            return "fitSize danger";
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewGrafik);
        ButtonKembali.Visible = false;
        ButtonKembaliPOProduksi.Visible = false;
    }
    #endregion

    #region Detail
    protected void RepeaterPOProduksiDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewPOProduksiDetail);
        ButtonKembali.Visible = false;
        ButtonKembaliPOProduksi.Visible = true;

        if (e.CommandName == "POProduksiBahanBakuDetail")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == e.CommandArgument.ToString());

                TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiBahanBaku.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;
                TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiBahanBaku.EnumJenisHPP.Value);
                TextBoxPegawaiPIC.Text = poProduksiBahanBaku.TBPengguna1.NamaLengkap;
                TextBoxTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo != null ? poProduksiBahanBaku.TanggalJatuhTempo.ToFormatTanggal() : string.Empty;
                TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman.ToFormatTanggal();
                TextBoxPegawai.Text = poProduksiBahanBaku.TBPengguna.NamaLengkap + " / " + poProduksiBahanBaku.Tanggal.ToFormatTanggal();

                if (poProduksiBahanBaku.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri)
                {
                    TextBoxSupplierVendorTempat.Text = poProduksiBahanBaku.TBSupplier.Nama;
                    TextBoxEmail.Text = poProduksiBahanBaku.TBSupplier.Email;
                    TextBoxAlamat.Text = poProduksiBahanBaku.TBSupplier.Alamat;
                    TextBoxTelepon1.Text = poProduksiBahanBaku.TBSupplier.Telepon1;
                    TextBoxTelepon2.Text = poProduksiBahanBaku.TBSupplier.Telepon2;
                }
                else
                {
                    TextBoxSupplierVendorTempat.Text = poProduksiBahanBaku.TBTempat.Nama;
                    TextBoxEmail.Text = poProduksiBahanBaku.TBTempat.Email;
                    TextBoxAlamat.Text = poProduksiBahanBaku.TBTempat.Alamat;
                    TextBoxTelepon1.Text = poProduksiBahanBaku.TBTempat.Telepon1;
                    TextBoxTelepon2.Text = poProduksiBahanBaku.TBTempat.Telepon2;
                }

                DetailBahanBaku.Visible = true;
                RepeaterPOProdusiBahanBakuDetail.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Select(item => new
                {
                    item.TBBahanBaku.KodeBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    Satuan = item.TBSatuan.Nama,
                    Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(db, null, item.TBBahanBaku),
                    Harga = poProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.TotalHPP : item.TotalHargaSupplier,
                    item.Jumlah,
                    Datang = item.Jumlah - item.Sisa,
                    item.Sisa
                });
                RepeaterPOProdusiBahanBakuDetail.DataBind();

                DetailProduk.Visible = false;
                RepeaterPOProduksiProdukDetail.DataSource = null;
                RepeaterPOProduksiProdukDetail.DataBind();
            }
        }
        else if (e.CommandName == "POProduksiProdukDetail")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProduk poProduksiBProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == e.CommandArgument.ToString());

                TextBoxIDProyeksi.Text = poProduksiBProduk.IDProyeksi != null ? poProduksiBProduk.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiBahanBaku.Text = poProduksiBProduk.IDPOProduksiProduk;
                TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiBProduk.EnumJenisHPP.Value);
                TextBoxPegawaiPIC.Text = poProduksiBProduk.TBPengguna1.NamaLengkap;
                TextBoxTanggalJatuhTempo.Text = poProduksiBProduk.TanggalJatuhTempo != null ? poProduksiBProduk.TanggalJatuhTempo.ToFormatTanggal() : string.Empty;
                TextBoxTanggalPengiriman.Text = poProduksiBProduk.TanggalPengiriman.ToFormatTanggal();
                TextBoxPegawai.Text = poProduksiBProduk.TBPengguna.NamaLengkap + " / " + poProduksiBProduk.Tanggal.ToFormatTanggal();

                if (poProduksiBProduk.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri)
                {
                    TextBoxSupplierVendorTempat.Text = poProduksiBProduk.TBVendor.Nama;
                    TextBoxEmail.Text = poProduksiBProduk.TBVendor.Email;
                    TextBoxAlamat.Text = poProduksiBProduk.TBVendor.Alamat;
                    TextBoxTelepon1.Text = poProduksiBProduk.TBVendor.Telepon1;
                    TextBoxTelepon2.Text = poProduksiBProduk.TBVendor.Telepon2;
                }
                else
                {
                    TextBoxSupplierVendorTempat.Text = poProduksiBProduk.TBTempat.Nama;
                    TextBoxEmail.Text = poProduksiBProduk.TBTempat.Email;
                    TextBoxAlamat.Text = poProduksiBProduk.TBTempat.Alamat;
                    TextBoxTelepon1.Text = poProduksiBProduk.TBTempat.Telepon1;
                    TextBoxTelepon2.Text = poProduksiBProduk.TBTempat.Telepon2;
                }

                DetailBahanBaku.Visible = false;
                RepeaterPOProdusiBahanBakuDetail.DataSource = null;
                RepeaterPOProdusiBahanBakuDetail.DataBind();


                DetailProduk.Visible = true;
                RepeaterPOProduksiProdukDetail.DataSource = poProduksiBProduk.TBPOProduksiProdukDetails.Select(item => new
                {
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.TBKombinasiProduk),
                    Harga = poProduksiBProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.TotalHPP : item.TotalHargaVendor,
                    item.Jumlah,
                    Datang = item.Jumlah - item.Sisa,
                    item.Sisa
                });
                RepeaterPOProduksiProdukDetail.DataBind();

                ButtonKembali.Visible = false;
                ButtonKembaliPOProduksi.Visible = true;
            }
        }
    }

    protected void ButtonKembaliPOProduksi_Click(object sender, EventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewPOProduksi);
        ButtonKembali.Visible = true;
        ButtonKembaliPOProduksi.Visible = false;
    }
    #endregion
}