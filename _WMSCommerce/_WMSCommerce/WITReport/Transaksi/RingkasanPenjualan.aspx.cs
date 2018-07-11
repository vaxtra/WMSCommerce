using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Laporan_Transaksi_RingkasanPenjualan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListStatusTransaksi.Items.AddRange(StatusTransaksi_Class.DataDropDownList(db));
                DropDownListStatusTransaksi.SelectedValue = ((int)EnumStatusTransaksi.Complete).ToString();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
            }

            LoadData();
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            TBTempat Tempat = new TBTempat();

            #region Default
            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            if (ViewState["TanggalAwal"].ToString() == ViewState["TanggalAkhir"].ToString())
                LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal();
            else
                LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal() + " - " + ViewState["TanggalAkhir"].ToFormatTanggal();
            #endregion

            RepeaterPenjualanItem.DataSource = null;
            RepeaterPenjualanItem.DataBind();

            RepeaterJenisPembayaran.DataSource = null;
            RepeaterJenisPembayaran.DataBind();

            #region QUERY DATA
            var ListTransaksi = db.TBTransaksis
                .Where(item =>
                    item.TanggalOperasional.Value.Date >= (DateTime)ViewState["TanggalAwal"] &&
                    item.TanggalOperasional.Value.Date <= (DateTime)ViewState["TanggalAkhir"])
                .ToArray();

            var ListTransaksiDetail = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= (DateTime)ViewState["TanggalAwal"] &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= (DateTime)ViewState["TanggalAkhir"])
                .ToArray();

            var ListTransaksiJenisPembayaran = db.TBTransaksiJenisPembayarans
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= (DateTime)ViewState["TanggalAwal"] &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= (DateTime)ViewState["TanggalAkhir"])
                .ToArray();
            #endregion

            #region FILTER TEMPAT
            if (DropDownListTempat.SelectedValue != "0")
            {
                ListTransaksi = ListTransaksi
                    .Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt())
                    .ToArray();

                ListTransaksiDetail = ListTransaksiDetail
                    .Where(item => item.TBTransaksi.IDTempat == DropDownListTempat.SelectedValue.ToInt())
                    .ToArray();

                ListTransaksiJenisPembayaran = ListTransaksiJenisPembayaran
                    .Where(item => item.TBTransaksi.IDTempat == DropDownListTempat.SelectedValue.ToInt())
                    .ToArray();

                Tempat = ClassTempat.Cari(DropDownListTempat.SelectedValue.ToInt());
            }
            else
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                Tempat = ClassTempat.Cari(Pengguna.IDTempat);
            }

            LabelKeteranganBiayaTambahan1.Text = Tempat.KeteranganBiayaTambahan1;
            LabelKeteranganBiayaTambahan2.Text = Tempat.KeteranganBiayaTambahan2;
            #endregion

            #region FILTER STATUS TRANSAKSI
            if (DropDownListStatusTransaksi.SelectedValue != "0")
            {
                ListTransaksi = ListTransaksi
                    .Where(item => item.IDStatusTransaksi == DropDownListStatusTransaksi.SelectedValue.ToInt())
                    .ToArray();

                ListTransaksiDetail = ListTransaksiDetail
                    .Where(item => item.TBTransaksi.IDStatusTransaksi == DropDownListStatusTransaksi.SelectedValue.ToInt())
                    .ToArray();

                ListTransaksiJenisPembayaran = ListTransaksiJenisPembayaran
                    .Where(item => item.TBTransaksi.IDStatusTransaksi == DropDownListStatusTransaksi.SelectedValue.ToInt())
                    .ToArray();
            }
            #endregion

            decimal TotalTransaksi = 0;
            decimal RataRataTransaksi = 0;
            decimal TotalPenjualanItem = 0;
            decimal TotalQuantityItem = 0;
            decimal RataRataItem = 0;
            decimal DiscountItemCustomer = 0;
            decimal DiscountCustomer = 0;
            decimal DiscountItemMember = 0;
            decimal DiscountMember = 0;
            decimal Pembulatan = 0;
            decimal BiayaPengiriman = 0;
            decimal TotalCash = 0;
            decimal TotalNonCash = 0;
            decimal TotalPembayaran = 0;
            decimal BiayaTambahan1 = 0;
            decimal BiayaTambahan2 = 0;
            decimal TotalPenjualan = 0;
            decimal GrandTotal = 0;

            if (ListTransaksi.Count() > 0)
            {
                var ListPenjualanItem = ListTransaksiDetail
                    .GroupBy(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "")
                    .Select(item => new
                    {
                        Nama = item.Key,
                        Quantity = item.Sum(item2 => item2.Quantity),
                        Penjualan = item.Sum(item2 => item2.Quantity * item2.HargaJual)
                    })
                    .OrderBy(item => item.Nama);

                RepeaterPenjualanItem.DataSource = ListPenjualanItem;
                RepeaterPenjualanItem.DataBind();

                TotalTransaksi = ListTransaksi.Count();
                RataRataTransaksi = ListTransaksi.Average(item => item.GrandTotal.Value);

                TotalPenjualanItem = ListPenjualanItem.Sum(item => item.Penjualan);
                TotalQuantityItem = ListPenjualanItem.Sum(item => item.Quantity);
                RataRataItem = ListTransaksiDetail.Average(item => item.Subtotal.Value);

                #region DISCOUNT CUSTOMER
                //TRANSAKSI
                var ListDiscountCustomer = ListTransaksi
                    .Where(item => item.TBPelanggan.IDGrupPelanggan == 1);

                if (ListDiscountCustomer.Count() > 0)
                    DiscountCustomer = ListDiscountCustomer.Sum(item => item.PotonganTransaksi.Value);

                //TRANSAKSI DETAIL
                var ListDiscountItemCustomer = ListTransaksiDetail
                    .Where(item => item.TBTransaksi.TBPelanggan.IDGrupPelanggan == 1);

                if (ListDiscountItemCustomer.Count() > 0)
                    DiscountItemCustomer = ListDiscountItemCustomer.Sum(item => item.Discount.Value * item.Quantity);
                #endregion

                #region DISCOUNT MEMBER
                //TRANSAKSI
                var ListDiscountMember = ListTransaksi
                    .Where(item => item.TBPelanggan.IDGrupPelanggan != 1);

                if (ListDiscountMember.Count() > 0)
                    DiscountMember = ListDiscountMember.Sum(item => item.PotonganTransaksi.Value);

                //TRANSAKSI DETAIL
                var ListDiscountItemMember = ListTransaksiDetail
                    .Where(item => item.TBTransaksi.TBPelanggan.IDGrupPelanggan != 1);

                if (ListDiscountItemMember.Count() > 0)
                    DiscountItemMember = ListDiscountItemMember.Sum(item => item.Discount.Value * item.Quantity);
                #endregion

                Pembulatan = ListTransaksi.Sum(item => item.Pembulatan.Value);
                BiayaPengiriman = ListTransaksi.Sum(item => item.BiayaPengiriman.Value);

                #region JENIS PEMBAYARAN
                var ListPembayaran = ListTransaksiJenisPembayaran
                    .GroupBy(item => new
                    {
                        item.IDJenisPembayaran,
                        item.TBJenisPembayaran.Nama
                    })
                    .Select(item => new
                    {
                        IDJenisPembayaran = item.Key.IDJenisPembayaran,
                        Nama = item.Key.Nama,
                        Total = item.Sum(item2 => item2.Total)
                    });

                //CASH
                var ListPembayaranCash = ListPembayaran.FirstOrDefault(item => item.IDJenisPembayaran == 1);

                if (ListPembayaranCash != null)
                    TotalCash = ListPembayaranCash.Total.Value;

                //NON CASH
                var ListPembayaranNonCash = ListPembayaran.Where(item => item.IDJenisPembayaran != 1);

                if (ListPembayaranNonCash.Count() > 0)
                    TotalNonCash = ListPembayaranNonCash.Sum(item => item.Total.Value);

                RepeaterJenisPembayaran.DataSource = ListPembayaranNonCash;
                RepeaterJenisPembayaran.DataBind();

                //TOTAL PEMBAYARAN
                TotalPembayaran = TotalCash + TotalNonCash;
                #endregion

                BiayaTambahan1 = ListTransaksi.Sum(item => item.BiayaTambahan1.Value);
                BiayaTambahan2 = ListTransaksi.Sum(item => item.BiayaTambahan2.Value);

                TotalPenjualan = TotalPenjualanItem - DiscountItemCustomer - DiscountItemMember - DiscountCustomer - DiscountMember;
                GrandTotal = TotalPenjualan + Pembulatan + BiayaTambahan1 + BiayaTambahan2 + BiayaPengiriman;
            }

            LabelTotalTransaksi.Text = TotalTransaksi.ToFormatHargaBulat();
            LabelRataRataTransaksi.Text = RataRataTransaksi.ToFormatHarga();

            LabelTotalPenjualanItem.Text = TotalPenjualanItem.ToFormatHarga();
            LabelTotalPenjualanItem1.Text = LabelTotalPenjualanItem.Text;

            LabelTotalQuantity.Text = TotalQuantityItem.ToFormatHargaBulat();
            LabelTotalQuantity1.Text = LabelTotalQuantity.Text;

            LabelRataRataItem.Text = RataRataItem.ToFormatHarga();

            LabelDiscountCustomer.Text = DiscountCustomer.ToFormatHarga();
            LabelDiscountItemCustomer.Text = DiscountItemCustomer.ToFormatHarga();

            LabelDiscountMember.Text = DiscountMember.ToFormatHarga();
            LabelDiscountItemMember.Text = DiscountItemMember.ToFormatHarga();

            LabelPembulatan.Text = Pembulatan.ToFormatHarga();
            LabelBiayaPengiriman.Text = BiayaPengiriman.ToFormatHarga();

            LabelTotalCash.Text = TotalCash.ToFormatHarga();
            LabelTotalNonCash.Text = TotalNonCash.ToFormatHarga();
            LabelTotalPembayaran.Text = TotalPembayaran.ToFormatHarga();

            LabelTotalPenjualan.Text = TotalPenjualan.ToFormatHarga();
            LabelGrandTotal.Text = GrandTotal.ToFormatHarga();

            LabelBiayaTambahan1.Text = BiayaTambahan1.ToFormatHarga();
            LabelBiayaTambahan2.Text = BiayaTambahan2.ToFormatHarga();
        }
    }
    protected void DropDownListTempat_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void DropDownListStatusTransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion
}