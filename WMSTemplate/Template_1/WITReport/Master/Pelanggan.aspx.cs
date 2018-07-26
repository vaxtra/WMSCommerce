using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Master_Pelanggan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                MultiViewPelanggan.SetActiveView(ViewPelanggan);

                TBTempat[] tempat = db.TBTempats.OrderBy(item => item.Nama).ToArray();
                TBPelanggan[] pelanggan = db.TBPelanggans.OrderBy(item => item.NamaLengkap).ToArray();
                TBJenisTransaksi[] jenisTransaksi = db.TBJenisTransaksis.ToArray();
                TBStatusTransaksi[] statusTransaksi = db.TBStatusTransaksis.ToArray();

                #region Transaksi
                DropDownListTempatTransaksi.DataSource = tempat;
                DropDownListTempatTransaksi.DataTextField = "Nama";
                DropDownListTempatTransaksi.DataValueField = "IDTempat";
                DropDownListTempatTransaksi.DataBind();
                DropDownListTempatTransaksi.Items.Insert(0, new ListItem { Text = "-Semua Tempat-", Value = "0" });
                DropDownListTempatTransaksi.SelectedValue = pengguna.IDTempat.ToString();

                DropDownListJenisTransaksiTransaksi.DataSource = jenisTransaksi;
                DropDownListJenisTransaksiTransaksi.DataTextField = "Nama";
                DropDownListJenisTransaksiTransaksi.DataValueField = "IDJenisTransaksi";
                DropDownListJenisTransaksiTransaksi.DataBind();
                DropDownListJenisTransaksiTransaksi.Items.Insert(0, new ListItem { Text = "-Semua Jenis-", Value = "0" });
                DropDownListJenisTransaksiTransaksi.SelectedValue = "1";

                DropDownListPelangganTransaksi.DataSource = pelanggan;
                DropDownListPelangganTransaksi.DataTextField = "NamaLengkap";
                DropDownListPelangganTransaksi.DataValueField = "IDPelanggan";
                DropDownListPelangganTransaksi.DataBind();
                LabelNamaPelanggan.Text = DropDownListPelangganTransaksi.SelectedItem.Text;

                TextBoxTanggalAwalTransaksi.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhirTransaksi.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwalTransaksi.Text == TextBoxTanggalAkhirTransaksi.Text)
                    LabelPeriodeTransaksi.Text = TextBoxTanggalAwalTransaksi.Text;
                else
                    LabelPeriodeTransaksi.Text = TextBoxTanggalAwalTransaksi.Text + " - " + TextBoxTanggalAkhirTransaksi.Text;

                LoadDataTransaksi(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
                #endregion

                #region Pembelian Produk
                DropDownListTempatPembelianProduk.DataSource = tempat;
                DropDownListTempatPembelianProduk.DataTextField = "Nama";
                DropDownListTempatPembelianProduk.DataValueField = "IDTempat";
                DropDownListTempatPembelianProduk.DataBind();
                DropDownListTempatPembelianProduk.Items.Insert(0, new ListItem { Text = "-Semua Tempat-", Value = "0" });
                DropDownListTempatPembelianProduk.SelectedValue = pengguna.IDTempat.ToString();

                DropDownListJenisTransaksiPembelianProduk.DataSource = jenisTransaksi;
                DropDownListJenisTransaksiPembelianProduk.DataTextField = "Nama";
                DropDownListJenisTransaksiPembelianProduk.DataValueField = "IDJenisTransaksi";
                DropDownListJenisTransaksiPembelianProduk.DataBind();
                DropDownListJenisTransaksiPembelianProduk.Items.Insert(0, new ListItem { Text = "-Semua Jenis-", Value = "0" });
                DropDownListJenisTransaksiPembelianProduk.SelectedValue = "1";

                DropDownListPelangganPembelianProduk.DataSource = pelanggan;
                DropDownListPelangganPembelianProduk.DataTextField = "NamaLengkap";
                DropDownListPelangganPembelianProduk.DataValueField = "IDPelanggan";
                DropDownListPelangganPembelianProduk.DataBind();
                DropDownListPelangganPembelianProduk.Items.Insert(0, new ListItem { Text = "-Semua Pelanggan-", Value = "0" });

                DropDownListStatusTransaksiPembelianProduk.DataSource = statusTransaksi;
                DropDownListStatusTransaksiPembelianProduk.DataTextField = "Nama";
                DropDownListStatusTransaksiPembelianProduk.DataValueField = "IDStatusTransaksi";
                DropDownListStatusTransaksiPembelianProduk.DataBind();
                DropDownListStatusTransaksiPembelianProduk.Items.Insert(0, new ListItem { Text = "-Semua Status-", Value = "0" });
                DropDownListStatusTransaksiPembelianProduk.SelectedValue = "5";

                TextBoxTanggalAwalPembelianProduk.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhirPembelianProduk.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwalPembelianProduk.Text == TextBoxTanggalAkhirPembelianProduk.Text)
                    LabelPeriodePembelianProduk.Text = TextBoxTanggalAwalPembelianProduk.Text;
                else
                    LabelPeriodePembelianProduk.Text = TextBoxTanggalAwalPembelianProduk.Text + " - " + TextBoxTanggalAkhirPembelianProduk.Text;

                LoadDataPembelianProduk(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
                #endregion

                #region Pelanggan
                RepeaterPelanggan.DataSource = pelanggan.Skip(1).Select(item => new
                {
                    item.IDPelanggan,
                    item.TBGrupPelanggan.Nama,
                    item.NamaLengkap,
                    item.Username,
                    AlamatLengkap = item.TBAlamats.Count == 0 ? string.Empty : item.TBAlamats.Select(data => data.AlamatLengkap).FirstOrDefault(),
                    item.Email,
                    item.Handphone,
                    Status = item._IsActive,
                    item.Deposit,
                    JumlahTransaksi = item.TBTransaksis.Count,
                }).OrderBy(item => item.NamaLengkap).ToArray();
                RepeaterPelanggan.DataBind();
                #endregion

                #region Komisi
                DropDownListTempatKomisi.DataSource = tempat;
                DropDownListTempatKomisi.DataTextField = "Nama";
                DropDownListTempatKomisi.DataValueField = "IDTempat";
                DropDownListTempatKomisi.DataBind();
                DropDownListTempatKomisi.Items.Insert(0, new ListItem { Text = "-Semua Tempat-", Value = "0" });
                DropDownListTempatKomisi.SelectedValue = pengguna.IDTempat.ToString();

                DropDownListJenisTransaksiKomisi.DataSource = jenisTransaksi;
                DropDownListJenisTransaksiKomisi.DataTextField = "Nama";
                DropDownListJenisTransaksiKomisi.DataValueField = "IDJenisTransaksi";
                DropDownListJenisTransaksiKomisi.DataBind();
                DropDownListJenisTransaksiKomisi.Items.Insert(0, new ListItem { Text = "-Semua Jenis-", Value = "0" });
                DropDownListJenisTransaksiKomisi.SelectedValue = "1";

                DropDownListStatusTransaksiKomisi.DataSource = statusTransaksi;
                DropDownListStatusTransaksiKomisi.DataTextField = "Nama";
                DropDownListStatusTransaksiKomisi.DataValueField = "IDStatusTransaksi";
                DropDownListStatusTransaksiKomisi.DataBind();
                DropDownListStatusTransaksiKomisi.Items.Insert(0, new ListItem { Text = "-Semua Status-", Value = "0" });
                DropDownListStatusTransaksiKomisi.SelectedValue = "5";

                TextBoxTanggalAwalKomisi.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhirKomisi.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwalKomisi.Text == TextBoxTanggalAkhirKomisi.Text)
                    LabelPeriodeKomisi.Text = TextBoxTanggalAwalKomisi.Text;
                else
                    LabelPeriodeKomisi.Text = TextBoxTanggalAwalKomisi.Text + " - " + TextBoxTanggalAkhirKomisi.Text;
                #endregion

                #region Potongan
                DropDownListTempatPotongan.DataSource = tempat;
                DropDownListTempatPotongan.DataTextField = "Nama";
                DropDownListTempatPotongan.DataValueField = "IDTempat";
                DropDownListTempatPotongan.DataBind();
                DropDownListTempatPotongan.Items.Insert(0, new ListItem { Text = "-Semua Tempat-", Value = "0" });
                DropDownListTempatPotongan.SelectedValue = pengguna.IDTempat.ToString();

                DropDownListJenisTransaksiPotongan.DataSource = jenisTransaksi;
                DropDownListJenisTransaksiPotongan.DataTextField = "Nama";
                DropDownListJenisTransaksiPotongan.DataValueField = "IDJenisTransaksi";
                DropDownListJenisTransaksiPotongan.DataBind();
                DropDownListJenisTransaksiPotongan.Items.Insert(0, new ListItem { Text = "-Semua Jenis-", Value = "0" });
                DropDownListJenisTransaksiPotongan.SelectedValue = "1";

                DropDownListStatusTransaksiPotongan.DataSource = statusTransaksi;
                DropDownListStatusTransaksiPotongan.DataTextField = "Nama";
                DropDownListStatusTransaksiPotongan.DataValueField = "IDStatusTransaksi";
                DropDownListStatusTransaksiPotongan.DataBind();
                DropDownListStatusTransaksiPotongan.Items.Insert(0, new ListItem { Text = "-Semua Status-", Value = "0" });
                DropDownListStatusTransaksiPotongan.SelectedValue = "5";

                TextBoxTanggalAwalPotongan.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhirPotongan.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwalPotongan.Text == TextBoxTanggalAkhirPotongan.Text)
                    LabelPeriodePotongan.Text = TextBoxTanggalAwalPotongan.Text;
                else
                    LabelPeriodePotongan.Text = TextBoxTanggalAwalPotongan.Text + " - " + TextBoxTanggalAkhirPotongan.Text;
                #endregion
            }
        }
    }

    #region Transaksi
    protected void ButtonHariTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMingguTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulanTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahunTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnyaTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnyaTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnyaTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnyaTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariTransaksi_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi(DateTime.Parse(TextBoxTanggalAwalTransaksi.Text), DateTime.Parse(TextBoxTanggalAkhirTransaksi.Text));
    }
    private void LoadDataTransaksi(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwalTransaksi.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhirTransaksi.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwalTransaksi.Text == TextBoxTanggalAkhirTransaksi.Text)
                LabelPeriodeTransaksi.Text = TextBoxTanggalAwalTransaksi.Text;
            else
                LabelPeriodeTransaksi.Text = TextBoxTanggalAwalTransaksi.Text + " - " + TextBoxTanggalAkhirTransaksi.Text;

            var transaksi = db.TBTransaksis.Where(item => item.TanggalOperasional.Value.Date >= DateTime.Parse(TextBoxTanggalAwalTransaksi.Text).Date && item.TanggalOperasional.Value.Date <= DateTime.Parse(TextBoxTanggalAkhirTransaksi.Text).Date && item.TBTransaksiDetails.Count > 0).Select(item => new
            {
                item.IDTempat,
                item.IDPelanggan,
                item.IDJenisTransaksi,
                item.IDStatusTransaksi,
                item.IDTransaksi,
                item.TanggalTransaksi,
                item.TanggalOperasional,
                item.TanggalPembayaran,
                item.Keterangan,
                CountProduk = item.TBTransaksiDetails.GroupBy(item2 => new
                {
                    item2.TBKombinasiProduk
                }).Select(item2 => new
                {
                    Produk = item2.Key.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item2.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                    JumlahProduk = item2.Sum(x => x.Quantity > 0 ? x.Quantity : 0),
                    Retur = item2.Sum(x => x.Quantity < 0 ? x.Quantity : 0)
                }).Count(),
                Produk = item.TBTransaksiDetails.GroupBy(item2 => new
                {
                    item2.TBKombinasiProduk
                }).Select(item2 => new
                {
                    Produk = item2.Key.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item2.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                    JumlahProduk = item2.Sum(x => x.Quantity > 0 ? x.Quantity : 0),
                    Retur = item2.Sum(x => x.Quantity < 0 ? x.Quantity : 0)
                }).FirstOrDefault(),
                Detail = item.TBTransaksiDetails.GroupBy(item2 => new
                {
                    item2.TBKombinasiProduk
                }).Select(item2 => new
                {
                    Produk = item2.Key.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item2.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                    JumlahProduk = item2.Sum(x => x.Quantity > 0 ? x.Quantity : 0),
                    Retur = item2.Sum(x => x.Quantity < 0 ? x.Quantity : 0)
                }).Skip(1),
                item.GrandTotal,
                item.TotalPembayaran,
                Penagihan = item.GrandTotal - item.TotalPembayaran,
                StatusTransaksi = item.TBStatusTransaksi.Nama
            }).OrderBy(item => item.TanggalTransaksi).ToArray();

            if (DropDownListTempatTransaksi.SelectedValue != "0")
                transaksi = transaksi.Where(item => item.IDTempat == DropDownListTempatTransaksi.SelectedValue.ToInt()).ToArray();

            if (DropDownListPelangganTransaksi.SelectedValue != "0")
                transaksi = transaksi.Where(item => item.IDPelanggan == DropDownListPelangganTransaksi.SelectedValue.ToInt()).ToArray();

            if (DropDownListJenisTransaksiTransaksi.SelectedValue != "0")
                transaksi = transaksi.Where(item => item.IDJenisTransaksi == DropDownListJenisTransaksiTransaksi.SelectedValue.ToInt()).ToArray();

            LabelNamaPelanggan.Text = DropDownListPelangganTransaksi.SelectedItem.Text;

            RepeaterTransaksi.DataSource = transaksi;
            RepeaterTransaksi.DataBind();
        }
    }
    #endregion

    #region Pembelian Produk
    protected void ButtonHariPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMingguPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulanPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahunPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnyaPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnyaPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnyaPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnyaPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariPembelianProduk_Click(object sender, EventArgs e)
    {
        LoadDataPembelianProduk(DateTime.Parse(TextBoxTanggalAwalPembelianProduk.Text), DateTime.Parse(TextBoxTanggalAkhirPembelianProduk.Text));
    }
    private void LoadDataPembelianProduk(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwalPembelianProduk.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhirPembelianProduk.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwalPembelianProduk.Text == TextBoxTanggalAkhirPembelianProduk.Text)
                LabelPeriodePembelianProduk.Text = TextBoxTanggalAwalPembelianProduk.Text;
            else
                LabelPeriodePembelianProduk.Text = TextBoxTanggalAwalPembelianProduk.Text + " - " + TextBoxTanggalAkhirPembelianProduk.Text;

            var pembelianProduk = db.TBTransaksiDetails.Where(item => item.TBTransaksi.TanggalOperasional.Value.Date >= DateTime.Parse(TextBoxTanggalAwalPembelianProduk.Text).Date && item.TBTransaksi.TanggalOperasional.Value.Date <= DateTime.Parse(TextBoxTanggalAkhirPembelianProduk.Text).Date).Select(item => new
            {
                item.TBTransaksi.IDTempat,
                item.TBTransaksi.TBPelanggan.IDPelanggan,
                item.TBTransaksi.TBPelanggan.NamaLengkap,
                item.TBTransaksi.IDJenisTransaksi,
                item.TBTransaksi.IDStatusTransaksi,
                item.TBTransaksi.TanggalTransaksi,
                item.TBTransaksi.TanggalPembayaran,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                HargaJual = item.HargaJual,
                JumlahProduk = item.Quantity,
                Subtotal = item.Subtotal
            }).OrderBy(item => item.TanggalTransaksi).ThenBy(item => item.Produk).ToArray();

            if (DropDownListTempatPembelianProduk.SelectedValue != "0")
                pembelianProduk = pembelianProduk.Where(item => item.IDTempat == DropDownListTempatPembelianProduk.SelectedValue.ToInt()).ToArray();

            if (DropDownListPelangganPembelianProduk.SelectedValue != "0")
                pembelianProduk = pembelianProduk.Where(item => item.IDPelanggan == DropDownListPelangganPembelianProduk.SelectedValue.ToInt()).ToArray();

            if (DropDownListJenisTransaksiPembelianProduk.SelectedValue != "0")
                pembelianProduk = pembelianProduk.Where(item => item.IDJenisTransaksi == DropDownListJenisTransaksiPembelianProduk.SelectedValue.ToInt()).ToArray();

            if (DropDownListStatusTransaksiPembelianProduk.SelectedValue != "0")
                pembelianProduk = pembelianProduk.Where(item => item.IDStatusTransaksi == DropDownListStatusTransaksiPembelianProduk.SelectedValue.ToInt()).ToArray();

            var hasil = db.TBPelanggans.AsEnumerable().Where(itemPelanggan => pembelianProduk.FirstOrDefault(item => item.IDPelanggan == itemPelanggan.IDPelanggan) != null).Select(itemPelanggan => new
            {
                itemPelanggan.IDPelanggan,
                Pelanggan = itemPelanggan.NamaLengkap,
                Produk = pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan),
                TotalJumlahProduk = pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Count() == 0 ? 0 : pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Sum(item => item.JumlahProduk),
                TotalJumlahSubtotal = pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Count() == 0 ? 0 : pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Sum(item => item.Subtotal),
            }).ToArray();

            RepeaterPembelianProduk.DataSource = hasil;
            RepeaterPembelianProduk.DataBind();
        }
    }
    #endregion

    #region Pelanggan
    protected void RepeaterPelanggan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            MultiViewPelanggan.SetActiveView(ViewProfilPelanggan);

            TBPelanggan pelanggan = db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == e.CommandArgument.ToInt());

            if (pelanggan != null)
            {
                if (pelanggan != null && pelanggan.IDPelanggan > 1)
                {
                    TBAlamat alamat = db.TBAlamats.FirstOrDefault(item => item.IDPelanggan == e.CommandArgument.ToInt());

                    TextBoxGrupPelanggan.Text = pelanggan.TBGrupPelanggan.Nama;
                    TextBoxNamaLengkap.Text = pelanggan.NamaLengkap;
                    TextBoxAlamat.Text = alamat == null ? string.Empty : alamat.AlamatLengkap;
                    TextBoxEmail.Text = pelanggan.Email;
                    TextBoxTeleponLain.Text = pelanggan.TeleponLain;
                    TextBoxTanggalDaftar.Text = pelanggan.TanggalDaftar.Value.ToFormatTanggalJam();
                    TextBoxTanggalLahir.Text = pelanggan.TanggalLahir.Value.ToFormatTanggalJam();
                    TextBoxUsername.Text = pelanggan.Username == null ? string.Empty : pelanggan.Username;
                    TextBoxHandphone.Text = pelanggan.Handphone;
                    TextBoxDeposit.Text = pelanggan.Deposit.ToString();
                    TextBoxStatus.Text = pelanggan._IsActive ? "Aktif" : "Non Aktif";
                    TextBoxCatatan.Text = pelanggan.Catatan;
                }
            }
        }
    }

    protected void ButtonKembaliPelanggan_Click(object sender, EventArgs e)
    {
        MultiViewPelanggan.SetActiveView(ViewPelanggan);
    }
    #endregion

    #region Komisi
    protected void ButtonHariKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMingguKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulanKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahunKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnyaKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnyaKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnyaKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnyaKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariKomisi_Click(object sender, EventArgs e)
    {
        LoadDataKomisi(DateTime.Parse(TextBoxTanggalAwalKomisi.Text), DateTime.Parse(TextBoxTanggalAkhirKomisi.Text));
    }
    private void LoadDataKomisi(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwalKomisi.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhirKomisi.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwalKomisi.Text == TextBoxTanggalAkhirKomisi.Text)
                LabelPeriodeKomisi.Text = TextBoxTanggalAwalKomisi.Text;
            else
                LabelPeriodeKomisi.Text = TextBoxTanggalAwalKomisi.Text + " - " + TextBoxTanggalAkhirKomisi.Text;

            var daftarTransaksi = db.TBTransaksis.Select(item => new { item.IDTempat, item.IDPelanggan, item.IDJenisTransaksi, item.IDStatusTransaksi, item.TanggalOperasional, item.GrandTotal }).ToArray();
            if (DropDownListTempatKomisi.SelectedValue != "0")
                daftarTransaksi = daftarTransaksi.Where(item => item.IDTempat == DropDownListTempatKomisi.SelectedValue.ToInt()).ToArray();

            if (DropDownListJenisTransaksiKomisi.SelectedValue != "0")
                daftarTransaksi = daftarTransaksi.Where(item => item.IDJenisTransaksi == DropDownListJenisTransaksiKomisi.SelectedValue.ToInt()).ToArray();

            if (DropDownListStatusTransaksiKomisi.SelectedValue != "0")
                daftarTransaksi = daftarTransaksi.Where(item => item.IDStatusTransaksi == DropDownListStatusTransaksiKomisi.SelectedValue.ToInt()).ToArray();

            RepeaterKomisiPelanggan.DataSource = db.TBPelanggans.Skip(1).Where(item => item.TBGrupPelanggan.EnumBonusGrupPelanggan != 1).AsEnumerable().Select(item => new
            {
                item.IDPelanggan,
                item.TBGrupPelanggan.Nama,
                BonusPelanggan = item.TBGrupPelanggan.EnumBonusGrupPelanggan == 1 ? "Potongan" : "Komisi",
                item.NamaLengkap,
                JumlahTransaksi = daftarTransaksi.Where(data => data.IDPelanggan == item.IDPelanggan).Count(),
                Komisi = daftarTransaksi.Where(data => data.IDPelanggan == item.IDPelanggan && data.TanggalOperasional.Value.Date >= DateTime.Parse(TextBoxTanggalAwalKomisi.Text).Date && data.TanggalOperasional.Value.Date <= DateTime.Parse(TextBoxTanggalAkhirKomisi.Text).Date)
                    .Sum(data => data.GrandTotal) * item.TBGrupPelanggan.Persentase ?? 0,
            }).OrderBy(item => item.NamaLengkap).ToArray();
            RepeaterKomisiPelanggan.DataBind();
        }
    }
    #endregion

    #region Potongan
    protected void ButtonHariPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMingguPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulanPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahunPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnyaPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnyaPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnyaPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnyaPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariPotongan_Click(object sender, EventArgs e)
    {
        LoadDataPotongan(DateTime.Parse(TextBoxTanggalAwalKomisi.Text), DateTime.Parse(TextBoxTanggalAkhirKomisi.Text));
    }
    private void LoadDataPotongan(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwalPotongan.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhirPotongan.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwalPotongan.Text == TextBoxTanggalAkhirPotongan.Text)
                LabelPeriodePotongan.Text = TextBoxTanggalAwalPotongan.Text;
            else
                LabelPeriodePotongan.Text = TextBoxTanggalAwalPotongan.Text + " - " + TextBoxTanggalAkhirPotongan.Text;

            var daftarTransaksi = db.TBTransaksis.Select(item => new { item.IDTempat, item.IDPelanggan, item.IDJenisTransaksi, item.IDStatusTransaksi, item.TanggalOperasional, item.PotonganTransaksi }).ToArray();
            if (DropDownListTempatPotongan.SelectedValue != "0")
                daftarTransaksi = daftarTransaksi.Where(item => item.IDTempat == DropDownListTempatPotongan.SelectedValue.ToInt()).ToArray();

            if (DropDownListJenisTransaksiPotongan.SelectedValue != "0")
                daftarTransaksi = daftarTransaksi.Where(item => item.IDJenisTransaksi == DropDownListJenisTransaksiPotongan.SelectedValue.ToInt()).ToArray();

            if (DropDownListStatusTransaksiPotongan.SelectedValue != "0")
                daftarTransaksi = daftarTransaksi.Where(item => item.IDStatusTransaksi == DropDownListStatusTransaksiPotongan.SelectedValue.ToInt()).ToArray();

            RepeaterPotonganPelanggan.DataSource = db.TBPelanggans.Skip(1).Where(item => item.TBGrupPelanggan.EnumBonusGrupPelanggan == 1).AsEnumerable().Select(item => new
            {
                item.IDPelanggan,
                item.TBGrupPelanggan.Nama,
                BonusPelanggan = item.TBGrupPelanggan.EnumBonusGrupPelanggan == 1 ? "Potongan" : "Komisi",
                item.NamaLengkap,
                JumlahTransaksi = daftarTransaksi.Where(data => data.IDPelanggan == item.IDPelanggan).Count(),
                Potongan = daftarTransaksi.Where(data => data.IDPelanggan == item.IDPelanggan && data.TanggalOperasional.Value.Date >= DateTime.Parse(TextBoxTanggalAwalPotongan.Text).Date && data.TanggalOperasional.Value.Date <= DateTime.Parse(TextBoxTanggalAkhirPotongan.Text).Date)
                    .Sum(data => data.PotonganTransaksi) ?? 0,
            }).OrderBy(item => item.NamaLengkap);
            RepeaterPotonganPelanggan.DataBind();
        }
    }
    #endregion
}