using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_Laporan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        LoadDataCetakLaporan(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }

    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        LoadDataCetakLaporan(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    #endregion

    private void LoadDataCetakLaporan(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        tanggalAwal = tanggalAwal.Date;
        tanggalAkhir = tanggalAkhir.Date;

        LabelPrintNamaStore.Text = Pengguna.Store;
        LabelPrintNamaLokasi.Text = Pengguna.Tempat;
        LabelPrintTanggal.Text = Pengaturan.FormatTanggal(DateTime.Now);

        if (tanggalAwal == tanggalAkhir)
            LabelPrintTanggalLaporan.Text = Pengaturan.FormatTanggalRingkas(tanggalAwal);
        else
            LabelPrintTanggalLaporan.Text = Pengaturan.FormatTanggalRingkas(tanggalAwal) + " - " + Pengaturan.FormatTanggalRingkas(tanggalAkhir);

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region PENCARIAN DATA
            TBTransaksi[] TransaksiKeseluruhan;
            TBTransaksiDetail[] TransaksiDetail;
            TBTransaksiJenisPembayaran[] TransaksiJenisPembayaran;
            TBTransaksiJenisPembayaran[] TransaksiPembayaranAwaitingPayment;

            //TRANSAKSI PEMBAYARAN
            TransaksiJenisPembayaran = db.TBTransaksiJenisPembayarans
                .Where(item =>
                    item.TBTransaksi.IDJenisTransaksi == 1 && //1 : POINT OF SALES
                    item.TBTransaksi.IDTempat == Pengguna.IDTempat &&
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            //TRANSAKSI PEMBAYARAN AWAITING PAYMENT
            TransaksiPembayaranAwaitingPayment = db.TBTransaksiJenisPembayarans
                .Where(item =>
                    item.TBTransaksi.IDJenisTransaksi == 1 && //1 : POINT OF SALES
                    item.TBTransaksi.IDTempat == Pengguna.IDTempat &&
                    item.Tanggal.Value.Date >= tanggalAwal &&
                    item.Tanggal.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled)
                .ToArray();

            //TRANSAKSI KESELURUHAN
            TransaksiKeseluruhan = db.TBTransaksis
                .Where(item =>
                    item.IDJenisTransaksi == 1 && //1 : POINT OF SALES
                    item.IDTempat == Pengguna.IDTempat &&
                    item.TanggalOperasional.Value >= tanggalAwal &&
                    item.TanggalOperasional.Value <= tanggalAkhir)
                .ToArray();

            //DETAIL TRANSAKSI
            TransaksiDetail = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.IDJenisTransaksi == 1 && //1 : POINT OF SALES
                    item.TBTransaksi.IDTempat == Pengguna.IDTempat &&
                    item.TBTransaksi.TanggalOperasional.Value >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            if (DropDownListStatusCetak.SelectedValue == "-1")
            {
                //OPERATOR
                LabelStatusLaporan.Text = "Laporan Tutup Kasir " + Pengguna.NamaLengkap;

                //TRANSAKSI PEMBAYARAN
                TransaksiJenisPembayaran = TransaksiJenisPembayaran
                    .Where(item => item.IDPengguna == Pengguna.IDPengguna)
                    .ToArray();

                //TRANSAKSI PEMBAYARAN AWAITING PAYMENT
                TransaksiPembayaranAwaitingPayment = TransaksiPembayaranAwaitingPayment
                    .Where(item => item.IDPengguna == Pengguna.IDPengguna)
                    .ToArray();

                //TRANSAKSI KESELURUHAN
                TransaksiKeseluruhan = TransaksiKeseluruhan
                    .Where(item => item.IDPenggunaPembayaran == Pengguna.IDPengguna)
                    .ToArray();

                //DETAIL TRANSAKSI
                TransaksiDetail = TransaksiDetail
                    .Where(item => item.TBTransaksi.IDPenggunaPembayaran == Pengguna.IDPengguna)
                    .ToArray();
            }
            else
                LabelStatusLaporan.Text = "Laporan Tutup Kasir Keseluruhan";

            //TRANSAKSI COMPLETE
            var TransaksiComplete = TransaksiKeseluruhan
                .Where(item => item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).ToArray();
            #endregion

            JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);
            var ListJenisPembayaran = ClassJenisPembayaran.Data();

            #region JENIS PEMBAYARAN
            Dictionary<string, decimal> ListPembayaranLainnya = new Dictionary<string, decimal>();

            foreach (var item in ListJenisPembayaran)
            {
                var Pembayaran = TransaksiJenisPembayaran
                    .Where(item2 => item2.IDJenisPembayaran == item.IDJenisPembayaran);

                if (Pembayaran.Count() > 0)
                    ListPembayaranLainnya.Add(item.Nama.ToUpper(), Pembayaran.Sum(x => x.Total.Value));
            }

            RepeaterTransaksiPembayaranLainnya.DataSource = ListPembayaranLainnya;
            RepeaterTransaksiPembayaranLainnya.DataBind();

            LabelTotalJenisPembayaran.Text = Pengaturan.FormatHarga(ListPembayaranLainnya.Sum(item => item.Value));
            #endregion

            #region PEMBAYARAN AWAITING PAYMENT
            Dictionary<string, decimal> ListPembayaranAwaitingPayment = new Dictionary<string, decimal>();
            Dictionary<string, decimal> ListCashDrawer = new Dictionary<string, decimal>();

            foreach (var item in ListJenisPembayaran)
            {
                var Pembayaran = TransaksiJenisPembayaran
                    .Where(item2 => item2.IDJenisPembayaran == item.IDJenisPembayaran);

                var AwaitingPayment = TransaksiPembayaranAwaitingPayment
                    .Where(item2 => item2.IDJenisPembayaran == item.IDJenisPembayaran);

                if (AwaitingPayment.Count() > 0)
                {
                    decimal TotalPembayaran = 0;
                    decimal TotalAwaitingPayment;

                    if (Pembayaran.Count() > 0)
                        TotalPembayaran = Pembayaran.Sum(x => x.Total.Value);

                    TotalAwaitingPayment = AwaitingPayment.Sum(x => x.Total.Value);

                    if ((TotalAwaitingPayment - TotalPembayaran) > 0)
                        ListPembayaranAwaitingPayment.Add(item.Nama.ToUpper(), (TotalAwaitingPayment - TotalPembayaran));

                    ListCashDrawer.Add(item.Nama.ToUpper(), TotalAwaitingPayment);
                }
            }

            RepeaterPembayaranAwaitingPayment.DataSource = ListPembayaranAwaitingPayment;
            RepeaterPembayaranAwaitingPayment.DataBind();

            RepeaterCashDrawer.DataSource = ListCashDrawer;
            RepeaterCashDrawer.DataBind();

            LabelTotalJenisPembayaranAwaitingPayment.Text = Pengaturan.FormatHarga(ListPembayaranAwaitingPayment.Sum(item => item.Value));
            LabelTotalCashDrawer.Text = Pengaturan.FormatHarga(ListCashDrawer.Sum(item => item.Value));

            PanelPembayaranAwaitingPayment.Visible = ListPembayaranAwaitingPayment.Count() > 0;
            PanelCashDrawer.Visible = ListCashDrawer.Count() > 0;
            #endregion

            #region TRANSAKSI SELAIN COMPLETE
            List<dynamic> TransaksiStatusLainnya = new List<dynamic>();
            foreach (var item in db.TBStatusTransaksis.Where(item2 => item2.IDStatusTransaksi != 5).OrderBy(item2 => item2.Urutan).ToArray())
            {
                var _data = TransaksiKeseluruhan
                    .Where(item2 => item2.IDStatusTransaksi == item.IDStatusTransaksi);

                if (_data.Count() > 0)
                    TransaksiStatusLainnya.Add(new
                    {
                        Nama = item.Nama.ToUpper(),
                        Jumlah = _data.Count(),
                        Total = _data.Sum(x => x.GrandTotal)
                    });
            }

            RepeaterTransaksiStatusLainnya.DataSource = TransaksiStatusLainnya;
            RepeaterTransaksiStatusLainnya.DataBind();
            #endregion

            //#region KATEGORI PRODUK
            //var DataKategori = TransaksiDetail
            //    .GroupBy(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 1 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama)
            //    .Select(item => new
            //    {
            //        Key = item.Key,
            //        Quantity = item.Sum(item2 => item2.JumlahProduk) ?? 0
            //    });

            //RepeaterPenjualanKategori.DataSource = DataKategori.OrderBy(item => item.Key);
            //RepeaterPenjualanKategori.DataBind();

            //LabelTotalPenjualanKategori.Text = Pengaturan.FormatHarga(DataKategori.Sum(item => item.Quantity));
            //#endregion

            #region PENJUALAN PRODUK
            var DataProduk = TransaksiDetail
                .GroupBy(item => item.TBKombinasiProduk.Nama)
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity)
                });

            RepeaterPenjualanProduk.DataSource = DataProduk.OrderBy(item => item.Key);
            RepeaterPenjualanProduk.DataBind();

            LabelTotalPenjualanProduk.Text = Pengaturan.FormatHarga(DataProduk.Sum(item => item.Quantity));
            LabelJumlahProduk.Text = LabelTotalPenjualanProduk.Text;
            #endregion

            var GrandTotal = TransaksiComplete.Sum(item => item.GrandTotal);
            var PotonganTransaksi = TransaksiComplete.Sum(item => item.PotonganTransaksi);
            var TotalPotonganHargaJualDetail = TransaksiComplete.Sum(item => item.TotalPotonganHargaJualDetail);
            var Discount = PotonganTransaksi + TotalPotonganHargaJualDetail;
            var SebelumDiscount = GrandTotal + Discount;

            LabelSebelumDiscount.Text = Pengaturan.FormatHarga(SebelumDiscount);
            LabelDiscount.Text = Pengaturan.FormatHarga(Discount);
            LabelGrandTotal.Text = Pengaturan.FormatHarga(GrandTotal);
            LabelJumlahTransaksi.Text = Pengaturan.FormatHarga(TransaksiComplete.Count());

            LabelDiscountTransaksi.Text = Pengaturan.FormatHarga(PotonganTransaksi);
            LabelDiscountProduk.Text = Pengaturan.FormatHarga(TotalPotonganHargaJualDetail);

            #region Addictea
            //TBPerpindahanStokProduk[] perpindahanStokProduk;

            //if (DropDownListStatusCetak.SelectedValue == "-1")
            //    perpindahanStokProduk = db.TBPerpindahanStokProduks.Where(item => item.IDPengguna == Pengguna.IDPengguna).ToArray();
            //else
            //    perpindahanStokProduk = db.TBPerpindahanStokProduks.ToArray();

            //var stok = db.TBAtributProduks.AsEnumerable().Select(itemAtribut => new
            //{
            //    NamaProduk = itemAtribut.Nama,
            //    Atribut = db.TBStokProduks.AsEnumerable().Where(itemStok => itemStok.TBKombinasiProduk.IDAtributProduk == itemAtribut.IDAtributProduk && itemStok.IDTempat == Pengguna.IDTempat).Select(data => new
            //    {
            //        Nama = data.TBKombinasiProduk.TBProduk.Nama,
            //        Restok = perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 30 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Count() == 0 ? 0 : perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 30 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Sum(perpindahan => perpindahan.Jumlah),
            //        JumlahJual = perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 4 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Count() == 0 ? 0 : (perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 4 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Sum(perpindahan => perpindahan.Jumlah) - (perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 5 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Count() == 0 ? 0 : perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 5 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Sum(perpindahan => perpindahan.Jumlah))),
            //        Retur = perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 14 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Count() == 0 ? 0 : perpindahanStokProduk.Where(perpindahan => perpindahan.IDJenisPerpindahanStok == 14 && perpindahan.IDStokProduk == data.IDStokProduk && perpindahan.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwalCetakLaporan.Text).Date && perpindahan.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhirCetakLaporan.Text).Date).Sum(perpindahan => perpindahan.Jumlah),
            //        Stok = data.Jumlah
            //    }).ToList()
            //}).ToList();

            //LabelPrintRestok.Text = stok.Count == 0 ? "0" : Pengaturan.FormatHarga(stok.Sum(item => item.Atribut.Sum(data => data.Restok)));
            //LabelPrintPenjualan.Text = stok.Count == 0 ? "0" : Pengaturan.FormatHarga(stok.Sum(item => item.Atribut.Sum(data => data.JumlahJual)));
            //LabelPrintRetur.Text = stok.Count == 0 ? "0" : Pengaturan.FormatHarga(stok.Sum(item => item.Atribut.Sum(data => data.Retur)));
            //LabelPrintStok.Text = stok.Count == 0 ? "0" : Pengaturan.FormatHarga(stok.Sum(item => item.Atribut.Sum(data => data.Stok)));

            //var dataStok = stok.Select(item => new
            //{
            //    item.NamaProduk,
            //    item.Atribut,
            //    TotalRestok = item.Atribut.Sum(item2 => item2.Restok),
            //    TotalJumlahJual = item.Atribut.Sum(item2 => item2.JumlahJual),
            //    TotalRetur = item.Atribut.Sum(item2 => item2.Retur),
            //    TotalStok = item.Atribut.Sum(item2 => item2.Stok)
            //});

            //RepeaterStokPrint.DataSource = dataStok;
            //RepeaterStokPrint.DataBind();
            #endregion
        }
    }
    protected void DropDownListJenisLaporan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListJenisLaporan.SelectedValue == "Ringkas")
            PanelPenjualanProduk.Visible = false;
        else if (DropDownListJenisLaporan.SelectedValue == "Lengkap")
            PanelPenjualanProduk.Visible = true;
    }
}