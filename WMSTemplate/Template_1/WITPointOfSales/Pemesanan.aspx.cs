using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
                GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

                RepeaterKombinasiProduk.DataSource = db.TBStokProduks
                    .Where(item => item.IDTempat == Pengguna.IDTempat)
                    .Select(item => new
                    {
                        item.TBKombinasiProduk.Urutan,
                        item.IDKombinasiProduk,
                        item.TBKombinasiProduk.Nama,
                        item.HargaJual
                    })
                    .OrderBy(item => item.Urutan);
                RepeaterKombinasiProduk.DataBind();

                DropDownListPelanggan.Items.AddRange(ClassPelanggan.DataDropDownListNamaHandphone());
                DropDownListGrupPelanggan.Items.AddRange(GrupPelanggan_Class.DataDropDownListNamaPotongan(db));
            }

            TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");

            if (Transaksi == null)
            {
                Transaksi = new Transaksi_Class(Pengguna.IDPengguna, Pengguna.IDTempat, DateTime.Now);
                ViewState["Transaksi"] = Transaksi;
            }
        }
    }
    protected void DropDownListPelanggan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListPelanggan.SelectedValue != "0")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                var Pelanggan = ClassPelanggan.Cari(DropDownListPelanggan.SelectedValue.ToInt());

                if (Pelanggan != null)
                {
                    TextBoxNama.Text = Pelanggan.NamaLengkap;
                    DropDownListGrupPelanggan.SelectedValue = Pelanggan.IDGrupPelanggan.ToString();

                    if (Pelanggan.TBAlamats.Count > 0)
                    {
                        var Alamat = Pelanggan.TBAlamats.FirstOrDefault();

                        TextBoxTelepon.Text = Pelanggan.Handphone;
                        TextBoxAlamat.Text = Alamat.AlamatLengkap;

                        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
                        Transaksi.BiayaPengiriman = Alamat.BiayaPengiriman.Value;
                        TextBoxBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);
                    }
                }
            }

            LoadDataTransaksi();
        }
    }
    protected void DropDownListGrupPelanggan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListGrupPelanggan.SelectedValue != "0")
            LoadDataTransaksi();
    }
    private void LoadDataTransaksi()
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        decimal Persentase = 0;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {

            GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);
            var PelangganGrup = GrupPelanggan_Class.Cari(db, Parse.Int(DropDownListGrupPelanggan.SelectedValue));
            Persentase = PelangganGrup.Persentase.Value;
        }

        //RESET DETAIL TRANSAKSI
        Transaksi.Detail.Clear();

        foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
        {
            HiddenField HiddenFieldIDKombinasiProduk = (HiddenField)item.FindControl("HiddenFieldIDKombinasiProduk");
            TextBox TextBoxJumlahProduk = (TextBox)item.FindControl("TextBoxJumlahProduk");
            TextBox TextBoxHarga = (TextBox)item.FindControl("TextBoxHarga");
            TextBox TextBoxDiscount = (TextBox)item.FindControl("TextBoxDiscount");
            TextBox TextBoxSubtotal = (TextBox)item.FindControl("TextBoxSubtotal");

            if (!string.IsNullOrWhiteSpace(TextBoxJumlahProduk.Text) && Pengaturan.FormatAngkaInput(TextBoxJumlahProduk.Text) > 0)
            {
                int idTransaksiDetail = Transaksi.TambahDetailTransaksi(Parse.Int(HiddenFieldIDKombinasiProduk.Value), (int)Pengaturan.FormatAngkaInput(TextBoxJumlahProduk.Text));

                if (!string.IsNullOrWhiteSpace(TextBoxDiscount.Text))
                    Transaksi.UbahPotonganHargaJualProduk(idTransaksiDetail, TextBoxDiscount.Text);

                Transaksi.UbahPotonganHargaJualProduk(idTransaksiDetail, Pengaturan.FormatHarga(Persentase) + "%");

                var TransaksiDetail = Transaksi.Detail.FirstOrDefault(item2 => item2.IDDetailTransaksi == idTransaksiDetail);

                TextBoxDiscount.Text = Pengaturan.FormatHarga(TransaksiDetail.Discount);
                TextBoxSubtotal.Text = Pengaturan.FormatHarga(TransaksiDetail.Subtotal);
            }
            else
            {
                TextBoxDiscount.Text = "";
                TextBoxSubtotal.Text = "";
            }
        }

        if (DateTime.Now.Date == Parse.dateTime(TextBoxTanggal.Text).Date)
            Transaksi.TanggalTransaksi = DateTime.Now;
        else
            Transaksi.TanggalTransaksi = Parse.dateTime(TextBoxTanggal.Text);

        Transaksi.BiayaPengiriman = Pengaturan.FormatAngkaInput(TextBoxBiayaPengiriman.Text);
        TextBoxBiayaPengiriman.Text = Transaksi.BiayaPengiriman.ToString();

        LabelSubtotal.Text = Pengaturan.FormatHarga(Transaksi.Subtotal);
        LabelDiscount.Text = Pengaturan.FormatHarga(Transaksi.PotonganTransaksi + Transaksi.TotalPotonganHargaJualDetail + Transaksi.TotalDiscountVoucher);

        LabelGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);
    }
    protected void ButtonHitung_Click(object sender, EventArgs e)
    {
        LoadDataTransaksi();
    }
    protected void ButtonProduksi_Click(object sender, EventArgs e)
    {
        //PRODUKSI
        Simpan(7);
    }
    protected void ButtonPesan_Click(object sender, EventArgs e)
    {
        //ORDER
        Simpan(2);
    }
    private void Simpan(int idStatusTransaksi)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        if (Transaksi != null)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataTransaksi();

                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
                Alamat_Class ClassAlamat = new Alamat_Class();

                TBPelanggan Pelanggan;

                if (DropDownListPelanggan.SelectedValue == "0")
                {
                    Pelanggan = ClassPelanggan.Tambah(
                        IDGrupPelanggan: DropDownListGrupPelanggan.SelectedValue.ToInt(),
                        IDPenggunaPIC: Pengguna.IDPengguna,
                        NamaLengkap: TextBoxNama.Text,
                        Username: "",
                        Password: "",
                        Email: "",
                        Handphone: TextBoxTelepon.Text,
                        TeleponLain: "",
                        TanggalLahir: DateTime.Now,
                        Deposit: 0,
                        Catatan: "",
                        _IsActive: true
                        );

                    ClassAlamat.Tambah(db, 0, Pelanggan, TextBoxAlamat.Text, "", Transaksi.BiayaPengiriman, "");

                    db.SubmitChanges();
                }
                else
                {
                    Pelanggan = ClassPelanggan.Ubah(
                        IDPelanggan: DropDownListPelanggan.SelectedValue.ToInt(),
                        IDGrupPelanggan: DropDownListGrupPelanggan.SelectedValue.ToInt(),
                        NamaLengkap: TextBoxNama.Text,
                        Handphone: TextBoxTelepon.Text
                        );

                    if (Pelanggan.TBAlamats.Count > 0)
                        ClassAlamat.Ubah(db, 0, Pelanggan.TBAlamats.FirstOrDefault(), Pelanggan, TextBoxAlamat.Text, "", Transaksi.BiayaPengiriman, "");
                }

                Transaksi.PengaturanPelanggan(Pelanggan.IDPelanggan);
                Transaksi.IDStatusTransaksi = idStatusTransaksi;

                string IDTransaksi = Transaksi.ConfirmTransaksi(db);

                db.SubmitChanges();

                Response.Redirect("Default.aspx?id=" + IDTransaksi);
            }
        }
    }
}
