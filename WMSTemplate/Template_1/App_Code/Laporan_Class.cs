using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Laporan_Class
{
    private PenggunaLogin pengguna;
    private DateTime tanggalAwal;
    private DateTime tanggalAkhir;
    private bool excel;
    private DataClassesDatabaseDataContext db;

    public string Periode
    {
        get
        {
            if (tanggalAwal == tanggalAkhir)
                return tanggalAwal.ToFormatTanggal();
            else
                return tanggalAwal.ToFormatTanggal() + " - " + tanggalAkhir.ToFormatTanggal();
        }
    }

    private string tempPencarian;
    public string TempPencarian
    {
        get { return tempPencarian; }
    }

    private string linkDownload;
    public string LinkDownload
    {
        get { return linkDownload; }
    }


    //========== ERI ===========
    private int idTempat;
    private decimal sumSubtotal;
    public string StoreTempat
    {
        get
        {
            string Result = string.Empty;

            if (idTempat == 0)
                Result = "Semua Tempat";
            else
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Tempat_Class ClassTempat = new Tempat_Class(db);
                    var Tempat = ClassTempat.Cari(idTempat);

                    Result = Tempat.TBStore.Nama + " - " + Tempat.Nama;
                }
            }

            return Result;
        }
    }

    public decimal SumSubtotal { get { return sumSubtotal; } }
    //=========END ERI CAKEP =========
    public Dictionary<string, dynamic> DataStokBahanBaku(int idTempat, int idSatuan, int idKategori, string kondisiStok, string kodeBarang, string namaBarang, string pilihSatuan, string status)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        string judul = "Laporan Stok Bahan Baku";
        int JumlahKolom = 8;

        tanggalAwal = DateTime.Now.Date;
        tanggalAkhir = DateTime.Now.Date;

        tempPencarian += "?IDTempat=" + idTempat;
        tempPencarian += "&KondisiStok=" + kondisiStok;
        tempPencarian += "&Kode=" + kodeBarang;
        tempPencarian += "&BahanBaku=" + namaBarang;
        tempPencarian += "&Kategori=" + idKategori;
        tempPencarian += "&Satuan=" + idSatuan;
        tempPencarian += "&PilihSatuan=" + pilihSatuan;
        tempPencarian += "&Status=" + status;

        var hasil = db.TBStokBahanBakus.Where(item =>
        (idTempat != 0 ? item.IDTempat == idTempat : true) &&
        (!string.IsNullOrWhiteSpace(kodeBarang) ? item.TBBahanBaku.KodeBahanBaku.ToLower().Contains(kodeBarang.ToLower()) : true) &&
        (!string.IsNullOrWhiteSpace(namaBarang) ? item.TBBahanBaku.Nama.ToLower().Contains(namaBarang.ToLower()) : true) &&
        (idKategori != 0 ? (item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategori) != null) : true) &&
        (idSatuan != 0 ? pilihSatuan == "Besar" ? item.TBBahanBaku.IDSatuanKonversi == idSatuan: item.TBBahanBaku.IDSatuan == idSatuan : true) &&
        (status.ToLower() != "semua" ? status.ToLower() == "cukup" ? item.Jumlah >= item.JumlahMinimum : item.Jumlah < item.JumlahMinimum : true)).Select(item => new
        {
            item.IDBahanBaku,
            item.TBBahanBaku.KodeBahanBaku,
            BahanBaku = item.TBBahanBaku.Nama,
            RelasiKategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.ToArray(),
            KategoriBahanBaku = GabungkanSemuaKategoriBahanBaku(item, null),
            IDSatuan = pilihSatuan == "Besar" ? item.TBBahanBaku.IDSatuanKonversi : item.TBBahanBaku.IDSatuan,
            Satuan = pilihSatuan == "Besar" ? item.TBBahanBaku.TBSatuan1.Nama : item.TBBahanBaku.TBSatuan.Nama,
            Jumlah = pilihSatuan == "Besar" ? item.Jumlah / item.TBBahanBaku.Konversi : item.Jumlah,
            HargaBeli = pilihSatuan == "Besar" ? item.HargaBeli * item.TBBahanBaku.Konversi : item.HargaBeli,
            JumlahMinimum = pilihSatuan == "Besar" ? item.JumlahMinimum / item.TBBahanBaku.Konversi : item.JumlahMinimum,
            Status = item.Jumlah >= item.JumlahMinimum ? "Cukup" : "Butuh Restok",
            Subtotal = pilihSatuan == "Besar" ? (item.Jumlah / item.TBBahanBaku.Konversi) * (item.HargaBeli * item.TBBahanBaku.Konversi) : item.Jumlah * item.HargaBeli
        }).OrderBy(item => item.BahanBaku).ToArray();

        if (kondisiStok == "1")
            hasil = hasil.Where(item => item.Jumlah > 0).ToArray();
        else if (kondisiStok == "2")
            hasil = hasil.Where(item => item.Jumlah == 0).ToArray();
        else if (kondisiStok == "3")
            hasil = hasil.Where(item => item.Jumlah < 0).ToArray();

            Result.Add("Data", hasil);

        Result.Add("Subtotal", hasil.Sum(item => item.Subtotal.Value));

        if (excel)
        {
            Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Kode");
            Excel_Class.Header(3, "Bahan Baku");
            Excel_Class.Header(4, "Kategori");
            Excel_Class.Header(5, "Harga");
            Excel_Class.Header(6, "Jumlah");
            Excel_Class.Header(7, "Satuan");
            Excel_Class.Header(8, "Subtotal");

            int index = 2;

            foreach (var item in hasil)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.KodeBahanBaku);
                Excel_Class.Content(index, 3, item.BahanBaku);
                Excel_Class.Content(index, 4, item.KategoriBahanBaku);
                Excel_Class.Content(index, 5, item.HargaBeli.Value);
                Excel_Class.Content(index, 6, item.Jumlah.Value);
                Excel_Class.Content(index, 7, item.Satuan);
                Excel_Class.Content(index, 8, item.Subtotal.Value);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
        }

        return Result;
    }


    public Laporan_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna, DateTime _tanggalAwal, DateTime _tanggalAkhir, bool _excel)
    {
        pengguna = _pengguna;
        tanggalAwal = _tanggalAwal.Date;
        tanggalAkhir = _tanggalAkhir.Date;
        excel = _excel;
        db = _db;
    }

    public static string GabungkanSemuaKategoriProduk(TBStokProduk stokProduk, TBKombinasiProduk kombinasiProduk)
    {
        string kategori = string.Empty;

        if (stokProduk != null)
        {
            if (stokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0)
            {
                foreach (var item in stokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriProduk.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriProduk.Nama;
                }
            }
        }
        else
        {
            if (kombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0)
            {
                foreach (var item in kombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriProduk.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriProduk.Nama;
                }
            }
        }

        return kategori;
    }

    public static string GabungkanSemuaKategoriBahanBaku(TBStokBahanBaku stokBahanBaku, TBBahanBaku bahanBaku)
    {
        string kategori = string.Empty;

        if (stokBahanBaku != null)
        {
            if (stokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0)
            {
                foreach (var item in stokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriBahanBaku.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriBahanBaku.Nama;
                }
            }
        }
        else if (bahanBaku != null)
        {
            if (bahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0)
            {
                foreach (var item in bahanBaku.TBRelasiBahanBakuKategoriBahanBakus)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriBahanBaku.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriBahanBaku.Nama;
                }
            }
        }

        return kategori;
    }

    public Dictionary<string, dynamic> Transaksi()
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransaksis
            .Where(item =>
                item.TanggalOperasional.Value >= tanggalAwal &&
                item.TanggalOperasional.Value <= tanggalAkhir)
            .ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        var DataResult = Data.Select(item => new
        {
            item.IDTransaksi,
            TanggalOperasional = item.TanggalOperasional.ToFormatTanggalHari(),
            TanggalTransaksi = item.TanggalTransaksi.ToFormatTanggal(),
            PenggunaTransaksi = item.TBPengguna.NamaLengkap,
            TanggalUpdate = item.IDPenggunaUpdate.HasValue ? item.TanggalUpdate.ToFormatTanggal() : "",
            PenggunaUpdate = item.IDPenggunaUpdate.HasValue ? item.TBPengguna2.NamaLengkap : "",
            Tempat = item.TBTempat.Nama,
            JenisTransaksi = item.TBJenisTransaksi.Nama,
            StatusTransaksi = item.TBStatusTransaksi.Nama,
            Pelanggan = item.TBPelanggan.NamaLengkap,
            Meja = item.TBMeja.Nama,
            JumlahTamu = item.JumlahTamu.ToFormatHargaBulat(),
            JumlahProduk = item.JumlahProduk.ToFormatHargaBulat(),
            SubtotalSebelumDiscount = (item.Subtotal + item.TotalPotonganHargaJualDetail).ToFormatHarga(),
            PotonganTransaksi = item.PotonganTransaksi.ToFormatHarga(),
            TotalPotonganHargaJualDetail = item.TotalPotonganHargaJualDetail.ToFormatHarga(),
            TotalDiscountVoucher = item.TotalDiscountVoucher.ToFormatHarga(),
            Subtotal = item.Subtotal.ToFormatHarga(),
            BiayaTambahan1 = item.BiayaTambahan1.ToFormatHarga(),
            BiayaTambahan2 = item.BiayaTambahan2.ToFormatHarga(),
            BiayaTambahan3 = item.BiayaTambahan3.ToFormatHarga(),
            BiayaTambahan4 = item.BiayaTambahan4.ToFormatHarga(),
            BiayaPengiriman = item.BiayaPengiriman.ToFormatHarga(),
            Pembulatan = item.Pembulatan.ToFormatHarga(),
            GrandTotal = item.GrandTotal.ToFormatHarga(),
            item.Keterangan
        });

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Transaksi", Periode, 27);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Transaksi");
            Excel_Class.Header(3, "Operasional");
            Excel_Class.Header(4, "Transaksi Tanggal");
            Excel_Class.Header(5, "Transaksi Pengguna");
            Excel_Class.Header(6, "Update Tanggal");
            Excel_Class.Header(7, "Update Pengguna");
            Excel_Class.Header(8, "Tempat");
            Excel_Class.Header(9, "Jenis");
            Excel_Class.Header(10, "Status");
            Excel_Class.Header(11, "Pelanggan");
            Excel_Class.Header(12, "Meja");
            Excel_Class.Header(13, "Jumlah Tamu");
            Excel_Class.Header(14, "Jumlah Produk");
            Excel_Class.Header(15, "Subtotal Sebelum Discount");
            Excel_Class.Header(16, "Discount Transaksi");
            Excel_Class.Header(17, "Discount Produk");
            Excel_Class.Header(18, "Discount Voucher");
            Excel_Class.Header(19, "Subtotal Setelah Discount");
            Excel_Class.Header(20, "Biaya Tambahan 1");
            Excel_Class.Header(21, "Biaya Tambahan 2");
            Excel_Class.Header(22, "Biaya Tambahan 3");
            Excel_Class.Header(23, "Biaya Tambahan 4");
            Excel_Class.Header(24, "Pengiriman");
            Excel_Class.Header(25, "Pembulatan");
            Excel_Class.Header(26, "Grand Total");
            Excel_Class.Header(27, "Keterangan");

            int index = 2;

            foreach (var item in Data)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransaksi);
                Excel_Class.Content(index, 3, item.TanggalOperasional.Value);
                Excel_Class.Content(index, 4, item.TanggalTransaksi.Value);
                Excel_Class.Content(index, 5, item.TBPengguna.NamaLengkap);

                if (item.IDPenggunaUpdate.HasValue)
                    Excel_Class.Content(index, 6, item.TanggalUpdate.Value);
                else
                    Excel_Class.Content(index, 6, "");

                Excel_Class.Content(index, 7, item.IDPenggunaUpdate.HasValue ? item.TBPengguna2.NamaLengkap : "");
                Excel_Class.Content(index, 8, item.TBTempat.Nama);
                Excel_Class.Content(index, 9, item.TBJenisTransaksi.Nama);
                Excel_Class.Content(index, 10, item.TBStatusTransaksi.Nama);
                Excel_Class.Content(index, 11, item.TBPelanggan.NamaLengkap);
                Excel_Class.Content(index, 12, item.TBMeja.Nama);
                Excel_Class.Content(index, 13, item.JumlahTamu.Value);
                Excel_Class.Content(index, 14, item.JumlahProduk.Value);
                Excel_Class.Content(index, 15, item.Subtotal.Value + item.TotalPotonganHargaJualDetail.Value);
                Excel_Class.Content(index, 16, item.PotonganTransaksi.Value);
                Excel_Class.Content(index, 17, item.TotalPotonganHargaJualDetail.Value);
                Excel_Class.Content(index, 18, item.TotalDiscountVoucher.Value);
                Excel_Class.Content(index, 19, item.Subtotal.Value);
                Excel_Class.Content(index, 20, item.BiayaTambahan1.Value);
                Excel_Class.Content(index, 21, item.BiayaTambahan2.Value);
                Excel_Class.Content(index, 22, item.BiayaTambahan3.Value);
                Excel_Class.Content(index, 23, item.BiayaTambahan4.Value);
                Excel_Class.Content(index, 24, item.BiayaPengiriman.Value);
                Excel_Class.Content(index, 25, item.Pembulatan.Value);
                Excel_Class.Content(index, 26, item.GrandTotal.Value);
                Excel_Class.Content(index, 27, item.Keterangan);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Data", DataResult);

        Result.Add("JumlahTamu", Data.Sum(item => item.JumlahTamu).ToFormatHargaBulat());
        Result.Add("JumlahProduk", Data.Sum(item => item.JumlahProduk).ToFormatHargaBulat());

        Result.Add("BiayaTambahan1", Data.Sum(item => item.BiayaTambahan1).ToFormatHarga());
        Result.Add("BiayaTambahan2", Data.Sum(item => item.BiayaTambahan2).ToFormatHarga());
        Result.Add("BiayaTambahan3", Data.Sum(item => item.BiayaTambahan3).ToFormatHarga());
        Result.Add("BiayaTambahan4", Data.Sum(item => item.BiayaTambahan4).ToFormatHarga());
        Result.Add("BiayaPengiriman", Data.Sum(item => item.BiayaPengiriman).ToFormatHarga());

        Result.Add("DiscountTransaksi", Data.Sum(item => item.PotonganTransaksi).ToFormatHarga());
        Result.Add("DiscountProduk", Data.Sum(item => item.TotalPotonganHargaJualDetail).ToFormatHarga());
        Result.Add("DiscountVoucher", Data.Sum(item => item.TotalDiscountVoucher).ToFormatHarga());

        Result.Add("Pembulatan", Data.Sum(item => item.Pembulatan).ToFormatHarga());
        Result.Add("SubtotalSebelumDiscount", Data.Sum(item => item.Subtotal + item.TotalPotonganHargaJualDetail).ToFormatHarga());
        Result.Add("SubtotalSetelahDiscount", Data.Sum(item => item.Subtotal).ToFormatHarga());
        Result.Add("GrandTotal", Data.Sum(item => item.GrandTotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> Transaksi(string idTransaksi, int idPenggunaTransaksi, int idPenggunaUpdate, int idTempat, int idJenisTransaksi, int idStatusTransaksi, int idPelanggan, int idMeja)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransaksis
            .Where(item =>
                item.TanggalOperasional.Value >= tanggalAwal &&
                item.TanggalOperasional.Value <= tanggalAkhir)
            .ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTransaksi=" + idTransaksi;
        tempPencarian += "&IDPenggunaTransaksi=" + idPenggunaTransaksi;
        tempPencarian += "&IDPenggunaUpdate=" + idPenggunaUpdate;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;
        tempPencarian += "&IDStatusTransaksi=" + idStatusTransaksi;
        tempPencarian += "&IDPelanggan=" + idPelanggan;
        tempPencarian += "&IDMeja=" + idMeja;

        if (!string.IsNullOrEmpty(idTransaksi))
            Data = Data.Where(item => item.IDTransaksi.Contains(idTransaksi)).ToArray();

        if (idPenggunaTransaksi != 0)
            Data = Data.Where(item => item.IDPenggunaTransaksi == idPenggunaTransaksi).ToArray();

        if (idPenggunaUpdate != 0)
            Data = Data.Where(item => item.IDPenggunaUpdate == idPenggunaUpdate).ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (idJenisTransaksi != 0)
            Data = Data.Where(item => item.IDJenisTransaksi == idJenisTransaksi).ToArray();

        if (idStatusTransaksi != 0)
            Data = Data.Where(item => item.IDStatusTransaksi == idStatusTransaksi).ToArray();

        if (idPelanggan != 0)
            Data = Data.Where(item => item.IDPelanggan == idPelanggan).ToArray();

        if (idMeja != 0)
            Data = Data.Where(item => item.IDMeja == idMeja).ToArray();

        var DataResult = Data.Select(item => new
        {
            item.IDTransaksi,
            TanggalOperasional = item.TanggalOperasional.ToFormatTanggalHari(),
            TanggalTransaksi = item.TanggalTransaksi.ToFormatTanggal(),
            PenggunaTransaksi = item.TBPengguna.NamaLengkap,
            TanggalUpdate = item.IDPenggunaUpdate.HasValue ? item.TanggalUpdate.ToFormatTanggal() : "",
            PenggunaUpdate = item.IDPenggunaUpdate.HasValue ? item.TBPengguna2.NamaLengkap : "",
            Tempat = item.TBTempat.Nama,
            JenisTransaksi = item.TBJenisTransaksi.Nama,
            StatusTransaksi = item.TBStatusTransaksi.Nama,
            Pelanggan = item.TBPelanggan.NamaLengkap,
            Meja = item.TBMeja.Nama,
            JumlahTamu = item.JumlahTamu.ToFormatHargaBulat(),
            JumlahProduk = item.JumlahProduk.ToFormatHargaBulat(),
            SubtotalSebelumDiscount = (item.Subtotal + item.TotalPotonganHargaJualDetail).ToFormatHarga(),
            PotonganTransaksi = item.PotonganTransaksi.ToFormatHarga(),
            TotalPotonganHargaJualDetail = item.TotalPotonganHargaJualDetail.ToFormatHarga(),
            TotalDiscountVoucher = item.TotalDiscountVoucher.ToFormatHarga(),
            Subtotal = item.Subtotal.ToFormatHarga(),
            BiayaTambahan1 = item.BiayaTambahan1.ToFormatHarga(),
            BiayaTambahan2 = item.BiayaTambahan2.ToFormatHarga(),
            BiayaTambahan3 = item.BiayaTambahan3.ToFormatHarga(),
            BiayaTambahan4 = item.BiayaTambahan4.ToFormatHarga(),
            BiayaPengiriman = item.BiayaPengiriman.ToFormatHarga(),
            Pembulatan = item.Pembulatan.ToFormatHarga(),
            GrandTotal = item.GrandTotal.ToFormatHarga(),
            item.Keterangan
        });

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Transaksi", Periode, 27);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Transaksi");
            Excel_Class.Header(3, "Operasional");
            Excel_Class.Header(4, "Transaksi Tanggal");
            Excel_Class.Header(5, "Transaksi Pengguna");
            Excel_Class.Header(6, "Update Tanggal");
            Excel_Class.Header(7, "Update Pengguna");
            Excel_Class.Header(8, "Tempat");
            Excel_Class.Header(9, "Jenis");
            Excel_Class.Header(10, "Status");
            Excel_Class.Header(11, "Pelanggan");
            Excel_Class.Header(12, "Meja");
            Excel_Class.Header(13, "Jumlah Tamu");
            Excel_Class.Header(14, "Jumlah Produk");
            Excel_Class.Header(15, "Subtotal Sebelum Discount");
            Excel_Class.Header(16, "Discount Transaksi");
            Excel_Class.Header(17, "Discount Produk");
            Excel_Class.Header(18, "Discount Voucher");
            Excel_Class.Header(19, "Subtotal Setelah Discount");
            Excel_Class.Header(20, "Biaya Tambahan 1");
            Excel_Class.Header(21, "Biaya Tambahan 2");
            Excel_Class.Header(22, "Biaya Tambahan 3");
            Excel_Class.Header(23, "Biaya Tambahan 4");
            Excel_Class.Header(24, "Pengiriman");
            Excel_Class.Header(25, "Pembulatan");
            Excel_Class.Header(26, "Grand Total");
            Excel_Class.Header(27, "Keterangan");

            int index = 2;

            foreach (var item in Data)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransaksi);
                Excel_Class.Content(index, 3, item.TanggalOperasional.Value);
                Excel_Class.Content(index, 4, item.TanggalTransaksi.Value);
                Excel_Class.Content(index, 5, item.TBPengguna.NamaLengkap);

                if (item.IDPenggunaUpdate.HasValue)
                    Excel_Class.Content(index, 6, item.TanggalUpdate.Value);
                else
                    Excel_Class.Content(index, 6, "");

                Excel_Class.Content(index, 7, item.IDPenggunaUpdate.HasValue ? item.TBPengguna2.NamaLengkap : "");
                Excel_Class.Content(index, 8, item.TBTempat.Nama);
                Excel_Class.Content(index, 9, item.TBJenisTransaksi.Nama);
                Excel_Class.Content(index, 10, item.TBStatusTransaksi.Nama);
                Excel_Class.Content(index, 11, item.TBPelanggan.NamaLengkap);
                Excel_Class.Content(index, 12, item.TBMeja.Nama);
                Excel_Class.Content(index, 13, item.JumlahTamu.Value);
                Excel_Class.Content(index, 14, item.JumlahProduk.Value);
                Excel_Class.Content(index, 15, item.Subtotal.Value + item.TotalPotonganHargaJualDetail.Value);
                Excel_Class.Content(index, 16, item.PotonganTransaksi.Value);
                Excel_Class.Content(index, 17, item.TotalPotonganHargaJualDetail.Value);
                Excel_Class.Content(index, 18, item.TotalDiscountVoucher.Value);
                Excel_Class.Content(index, 19, item.Subtotal.Value);
                Excel_Class.Content(index, 20, item.BiayaTambahan1.Value);
                Excel_Class.Content(index, 21, item.BiayaTambahan2.Value);
                Excel_Class.Content(index, 22, item.BiayaTambahan3.Value);
                Excel_Class.Content(index, 23, item.BiayaTambahan4.Value);
                Excel_Class.Content(index, 24, item.BiayaPengiriman.Value);
                Excel_Class.Content(index, 25, item.Pembulatan.Value);
                Excel_Class.Content(index, 26, item.GrandTotal.Value);
                Excel_Class.Content(index, 27, item.Keterangan);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Data", DataResult);

        Result.Add("JumlahTamu", Data.Sum(item => item.JumlahTamu).ToFormatHargaBulat());
        Result.Add("JumlahProduk", Data.Sum(item => item.JumlahProduk).ToFormatHargaBulat());

        Result.Add("BiayaTambahan1", Data.Sum(item => item.BiayaTambahan1).ToFormatHarga());
        Result.Add("BiayaTambahan2", Data.Sum(item => item.BiayaTambahan2).ToFormatHarga());
        Result.Add("BiayaTambahan3", Data.Sum(item => item.BiayaTambahan3).ToFormatHarga());
        Result.Add("BiayaTambahan4", Data.Sum(item => item.BiayaTambahan4).ToFormatHarga());
        Result.Add("BiayaPengiriman", Data.Sum(item => item.BiayaPengiriman).ToFormatHarga());

        Result.Add("DiscountTransaksi", Data.Sum(item => item.PotonganTransaksi).ToFormatHarga());
        Result.Add("DiscountProduk", Data.Sum(item => item.TotalPotonganHargaJualDetail).ToFormatHarga());
        Result.Add("DiscountVoucher", Data.Sum(item => item.TotalDiscountVoucher).ToFormatHarga());

        Result.Add("Pembulatan", Data.Sum(item => item.Pembulatan).ToFormatHarga());
        Result.Add("SubtotalSebelumDiscount", Data.Sum(item => item.Subtotal + item.TotalPotonganHargaJualDetail).ToFormatHarga());
        Result.Add("SubtotalSetelahDiscount", Data.Sum(item => item.Subtotal).ToFormatHarga());
        Result.Add("GrandTotal", Data.Sum(item => item.GrandTotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> TransaksiDetail(int idTempat, int idStatusTransaksi, string idTransaksi, int idPelanggan, string kode, int idPemilikProduk, int idProduk, int idAtributProduk, int idKategoriProduk, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TanggalOperasional.Value >= tanggalAwal &&
                item.TBTransaksi.TanggalOperasional.Value <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

        if (idStatusTransaksi != 0)
            Data = Data.Where(item => item.TBTransaksi.IDStatusTransaksi == idStatusTransaksi).ToArray();

        if (!string.IsNullOrEmpty(idTransaksi))
            Data = Data.Where(item => item.IDTransaksi.Contains(idTransaksi)).ToArray();

        if (idPelanggan != 0)
            Data = Data.Where(item => item.TBTransaksi.IDPelanggan == idPelanggan).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(kode)).ToArray();

        if (idPemilikProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();

        if (idProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        if (idKategoriProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == idKategoriProduk) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDStatusTransaksi=" + idStatusTransaksi;
        tempPencarian += "&IDTransaksi=" + idTransaksi;
        tempPencarian += "&IDPelanggan=" + idPelanggan;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDKategoriProduk=" + idKategoriProduk;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.TBTransaksi.IDTempat,
                item.TBTransaksi.IDTransaksi,
                TanggalOperasional = item.TBTransaksi.TanggalOperasional.ToFormatTanggalJam(),
                TanggalTransaksi = item.TBTransaksi.TanggalTransaksi.ToFormatTanggalJam(),
                Pelanggan = item.TBTransaksi.TBPelanggan.NamaLengkap,
                item.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.TBKombinasiProduk),
                JumlahProduk = item.Quantity.ToFormatHargaBulat(),
                HargaPokok = item.HargaBeli.ToFormatHarga(),
                HargaJual = item.HargaJual.ToFormatHarga(),
                PotonganHargaJual = item.Discount.ToFormatHarga(),
                Subtotal = item.Subtotal.ToFormatHarga(),
                PenjualanBersih = ((item.HargaJual - item.Discount - item.HargaBeli) * item.Quantity).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Penjualan Produk Detail", Periode, 14);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Transaksi");
                Excel_Class.Header(3, "Tanggal");
                Excel_Class.Header(4, "Pelanggan");
                Excel_Class.Header(5, "Kode");
                Excel_Class.Header(6, "Brand");
                Excel_Class.Header(7, "Produk");
                Excel_Class.Header(8, "Varian");
                Excel_Class.Header(9, "Kategori");
                Excel_Class.Header(10, "Jumlah");
                Excel_Class.Header(11, "Harga Pokok");
                Excel_Class.Header(12, "Harga Jual");
                Excel_Class.Header(13, "Potongan Harga");
                Excel_Class.Header(14, "Subtotal");
                Excel_Class.Header(15, "Netto");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDTransaksi);
                    Excel_Class.Content(index, 3, item.TanggalTransaksi);
                    Excel_Class.Content(index, 4, item.Pelanggan);
                    Excel_Class.Content(index, 5, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 6, item.PemilikProduk);
                    Excel_Class.Content(index, 7, item.Produk);
                    Excel_Class.Content(index, 8, item.AtributProduk);
                    Excel_Class.Content(index, 9, item.Kategori);
                    Excel_Class.Content(index, 10, item.JumlahProduk);
                    Excel_Class.Content(index, 11, item.HargaPokok);
                    Excel_Class.Content(index, 12, item.HargaJual);
                    Excel_Class.Content(index, 13, item.PotonganHargaJual);
                    Excel_Class.Content(index, 14, item.Subtotal);
                    Excel_Class.Content(index, 15, item.PenjualanBersih);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBKombinasiProduk,
            })
            .Select(item => new
            {
                KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.Key.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                JumlahProduk = item.Sum(x => x.Quantity).ToFormatHargaBulat(),
                HargaPokok = item.Sum(x => x.HargaBeli * x.Quantity).ToFormatHarga(),
                HargaJual = item.Sum(x => x.HargaJual * x.Quantity).ToFormatHarga(),
                PotonganHargaJual = item.Sum(x => x.Discount * x.Quantity).ToFormatHarga(),
                Subtotal = item.Sum(x => x.Subtotal).ToFormatHarga(),
                PenjualanBersih = item.Sum(x => (x.HargaJual - x.Discount - x.HargaBeli) * x.Quantity).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Penjualan Produk", Periode, 12);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Brand");
                Excel_Class.Header(4, "Produk");
                Excel_Class.Header(5, "Varian");
                Excel_Class.Header(6, "Kategori");
                Excel_Class.Header(7, "Jumlah");
                Excel_Class.Header(8, "Harga Pokok");
                Excel_Class.Header(9, "Harga Jual");
                Excel_Class.Header(10, "Potongan Harga");
                Excel_Class.Header(11, "Subtotal");
                Excel_Class.Header(12, "Netto");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 3, item.PemilikProduk);
                    Excel_Class.Content(index, 4, item.Produk);
                    Excel_Class.Content(index, 5, item.AtributProduk);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.HargaPokok);
                    Excel_Class.Content(index, 8, item.HargaJual);
                    Excel_Class.Content(index, 9, item.PotonganHargaJual);
                    Excel_Class.Content(index, 10, item.JumlahProduk);
                    Excel_Class.Content(index, 11, item.Subtotal);
                    Excel_Class.Content(index, 12, item.PenjualanBersih);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("JumlahProduk", Data.Sum(item => item.Quantity).ToFormatHargaBulat());
        Result.Add("HargaPokok", Data.Sum(item => item.HargaBeli * item.Quantity).ToFormatHarga());
        Result.Add("HargaJual", Data.Sum(item => item.HargaJual * item.Quantity).ToFormatHarga());
        Result.Add("PotonganHargaJual", Data.Sum(item => item.Discount * item.Quantity).ToFormatHarga());
        Result.Add("Subtotal", Data.Sum(item => item.Subtotal).ToFormatHarga());
        Result.Add("PenjualanBersih", Data.Sum(item => (item.HargaJual - item.Discount - item.HargaBeli) * item.Quantity).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> Pembayaran(int jenisTanggal, int idTempat, int idJenisTransaksi)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();
        JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);
        Tempat_Class ClassTempat = new Tempat_Class(db);
        JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();

        string Header = string.Empty;
        string Body = string.Empty;
        string Sumary = string.Empty;
        string PrintTempat = string.Empty;
        string PrintSubJudul = string.Empty;

        //1 : TANGGAL OPERASIONAL
        //2 : TANGGAL PEMBAYARAN

        var Database = db.TBTransaksiJenisPembayarans.Where(item => item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete);

        if (jenisTanggal == 1) //TANGGAL OPERASIONAL
        {
            Database = Database
                .Where(item =>
                   item.TBTransaksi.TanggalOperasional.Value >= tanggalAwal &&
                   item.TBTransaksi.TanggalOperasional.Value <= tanggalAkhir);
        }
        else //TANGGAL PEMBAYARAN
        {
            Database = Database
                .Where(item =>
                    item.Tanggal.Value.Date >= tanggalAwal &&
                    item.Tanggal.Value.Date <= tanggalAkhir);
        }

        var Data = Database.Select(item => new
        {
            item.TBTransaksi.IDTempat,
            item.TBTransaksi.IDJenisTransaksi,
            item.IDJenisPembayaran,
            IDPengguna = jenisTanggal == 1 ? item.TBTransaksi.IDPenggunaTransaksi : item.IDPengguna,
            item.IDTransaksi,
            item.Total
        });

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&JenisTanggal=" + tanggalAkhir;

        if (jenisTanggal == 1)
            PrintSubJudul = "Berdasarkan Tanggal Operasional<br/>";
        else
            PrintSubJudul = "Berdasarkan Tanggal Pembayaran<br/>";

        if (idTempat != 0)
        {
            Data = Data.Where(item => item.IDTempat == idTempat);
            PrintTempat = ClassTempat.Cari(idTempat).Nama;
        }
        else
            PrintTempat = "Semua Tempat";

        tempPencarian += "&IDTempat=" + idTempat;

        if (idJenisTransaksi != 0)
        {
            Data = Data.Where(item => item.IDJenisTransaksi == idJenisTransaksi);
            PrintSubJudul += "Transaksi " + ClassJenisTransaksi.Cari(db, idJenisTransaksi).Nama;
        }
        else
            PrintSubJudul += "Semua Jenis Transaksi";

        tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

        var DataResult = Data
            .GroupBy(item => new
            {
                item.IDJenisPembayaran,
                item.IDPengguna
            })
            .Select(item => new
            {
                item.Key,
                Transaksi = item.Select(item2 => item2.IDTransaksi).Distinct().Count(),
                Total = item.Sum(item2 => item2.Total)
            });

        var JenisPembayaran = ClassJenisPembayaran.Data();

        #region HEADER
        Header += "<tr class='active'><th class='text-right' rowspan='2'>No.</th><th rowspan='2'>Pegawai</th>";

        foreach (var item in JenisPembayaran)
            Header += "<th class='text-center' colspan='2'>" + item.Nama + "</th>";

        Header += "<th class='text-center' colspan='2'>TOTAL</th></tr><tr class='active'>";

        for (int i = 0; i <= JenisPembayaran.Count(); i++)
        {
            Header += "<th class='text-right'>Transaksi</th>";
            Header += "<th class='text-right'>Nominal</th>";
        }

        Header += "</tr>";
        #endregion

        #region BODY
        var RingkasanPengguna = DataResult.Select(item => item.Key.IDPengguna).Distinct();

        int indexData = 0;
        foreach (var item in db.TBPenggunas.Where(item => RingkasanPengguna.Any(id => id == item.IDPengguna)))
        {
            Body += "<tr><td>" + (++indexData) + "</td><td class='fitSize'>" + item.NamaLengkap + "</td>";

            decimal _tempTransaksi = 0;
            decimal _tempTotal = 0;

            foreach (var item2 in JenisPembayaran)
            {
                var _temp = DataResult.FirstOrDefault(item3 => item3.Key.IDPengguna == item.IDPengguna && item3.Key.IDJenisPembayaran == item2.IDJenisPembayaran);

                if (_temp != null)
                {
                    _tempTransaksi += _temp.Transaksi;
                    Body += "<td class='text-right warning'>" + _temp.Transaksi.ToFormatHargaBulat() + "</td>";

                    _tempTotal += _temp.Total.Value;
                    Body += "<td class='text-right'>" + _temp.Total.ToFormatHarga() + "</td>";
                }
                else
                {
                    Body += "<td class='warning'></td>";
                    Body += "<td></td>";
                }
            }

            Body += "<td class='text-right warning'><b>" + _tempTransaksi.ToFormatHargaBulat() + "</b></td><td class='text-right'><b>" + _tempTotal.ToFormatHarga() + "</b></td></tr>";
        }
        #endregion

        #region SUMARY
        Sumary += "<tr><td colspan='2' class='info'></td>";

        foreach (var item2 in JenisPembayaran)
        {
            var _temp = DataResult.Where(item3 => item3.Key.IDJenisPembayaran == item2.IDJenisPembayaran);
            int _tempTransaksi = 0;
            decimal _tempTotal = 0;

            if (_temp.Count() > 0)
            {
                _tempTransaksi = _temp.Sum(item3 => item3.Transaksi);
                _tempTotal = _temp.Sum(item3 => item3.Total.Value);
            }

            Sumary += "<td class='text-right info'><b>" + _tempTransaksi.ToFormatHargaBulat() + "</b></td>";
            Sumary += "<td class='text-right info'><b>" + _tempTotal.ToFormatHarga() + "</b></td>";
        }

        decimal _tempSumaryTransaksi = 0;
        decimal _tempSumaryTotal = 0;

        if (DataResult.Count() > 0)
        {
            _tempSumaryTransaksi = DataResult.Sum(item3 => item3.Transaksi);
            _tempSumaryTotal = DataResult.Sum(item3 => item3.Total.Value);
        }

        Sumary += "<td class='text-right info'><b>" + _tempSumaryTransaksi.ToFormatHargaBulat() + "</b></td>";
        Sumary += "<td class='text-right info'><b>" + _tempSumaryTotal.ToFormatHarga() + "</b></td>";

        Sumary += "</tr>";
        #endregion

        Result.Add("Header", Header);
        Result.Add("Body", Body);
        Result.Add("Sumary", Sumary);
        Result.Add("Tempat", PrintTempat);
        Result.Add("SubJudul", PrintSubJudul);

        if (excel)
        {
            #region EXCEL
            int JumlahKolom = JenisPembayaran.Count() * 2 + 4;

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Pembayaran", Periode, JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Pegawai");

            int indexJenis = 2;
            foreach (var item in JenisPembayaran)
            {
                Excel_Class.Header(++indexJenis, item.Nama + " " + "Transaksi");
                Excel_Class.Header(++indexJenis, item.Nama + " " + "Nominal");
            }

            Excel_Class.Header(++indexJenis, "TOTAL Transaksi");
            Excel_Class.Header(++indexJenis, "TOTAL Nominal");

            int index = 2;

            foreach (var item in db.TBPenggunas.Where(item => RingkasanPengguna.Any(id => id == item.IDPengguna)))
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.NamaLengkap);

                indexData = 2;
                decimal _tempTransaksi = 0;
                decimal _tempTotal = 0;

                foreach (var item2 in JenisPembayaran)
                {
                    var _temp = DataResult.FirstOrDefault(item3 => item3.Key.IDPengguna == item.IDPengguna && item3.Key.IDJenisPembayaran == item2.IDJenisPembayaran);

                    if (_temp != null)
                    {
                        _tempTransaksi += _temp.Transaksi;
                        Excel_Class.Content(index, ++indexData, _temp.Transaksi);

                        _tempTotal += _temp.Total.Value;
                        Excel_Class.Content(index, ++indexData, _temp.Total.Value);
                    }
                    else
                    {
                        Excel_Class.Content(index, ++indexData, 0);
                        Excel_Class.Content(index, ++indexData, 0);
                    }
                }

                Excel_Class.Content(index, ++indexData, _tempTransaksi);
                Excel_Class.Content(index, ++indexData, _tempTotal);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    public Dictionary<string, dynamic> Ringkasan(int jenisLaporan, int idTempat, int idJenisTransaksi)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();
        TBTransaksi[] Data;

        if (jenisLaporan == 3) //TAHUNAN
        {
            Data = db.TBTransaksis
                .Where(item => item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();
        }
        else
        {
            Data = db.TBTransaksis
                .Where(item =>
                    item.TanggalOperasional.Value >= tanggalAwal &&
                    item.TanggalOperasional.Value <= tanggalAkhir &&
                    item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();
        }

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        if (idTempat != 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);

            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();
            Result.Add("Tempat", ClassTempat.Cari(idTempat).Nama);
        }
        else
            Result.Add("Tempat", "Semua Tempat");

        tempPencarian += "&IDTempat=" + idTempat;

        if (idJenisTransaksi != 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();

            Data = Data.Where(item => item.IDJenisTransaksi == idJenisTransaksi).ToArray();
            Result.Add("JenisTransaksi", "Jenis Transaksi : " + ClassJenisTransaksi.Cari(db, idJenisTransaksi).Nama);
        }
        else
            Result.Add("JenisTransaksi", "Jenis Transaksi : Semua Jenis");

        tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

        List<dynamic> FinalResult = new List<dynamic>();

        if (jenisLaporan == 1) //HARIAN
        {
            #region HARIAN
            var ResultGroupBy = Data
                .GroupBy(item => item.TanggalOperasional)
                .Select(item => new
                {
                    item.Key,
                    Transaksi = item.Count(),
                    NonPelanggan = item.Where(item2 => item2.IDPelanggan == 1).Count(),
                    Pelanggan = item.Where(item2 => item2.IDPelanggan != 1).Count(), //GENERAL CUSTOMER
                    Tamu = item.Sum(item2 => item2.JumlahTamu),
                    Quantity = item.Sum(item2 => item2.JumlahProduk),
                    Discount = item.Where(item2 => item2.PotonganTransaksi != 0 || item2.TotalPotonganHargaJualDetail != 0 || item2.TotalDiscountVoucher != 0).Count(),
                    NonDiscount = item.Where(item2 => item2.PotonganTransaksi == 0 || item2.TotalPotonganHargaJualDetail == 0 || item2.TotalDiscountVoucher == 0).Count(),
                    Pengiriman = item.Where(item2 => item2.BiayaPengiriman != 0).Count(),
                    NonPengiriman = item.Where(item2 => item2.BiayaPengiriman == 0).Count(),
                    Nominal = item.Sum(item2 => item2.GrandTotal.Value)
                });

            for (DateTime i = tanggalAwal; i <= tanggalAkhir; i = i.AddDays(1))
            {
                var temp = ResultGroupBy.FirstOrDefault(item => item.Key == i);

                if (temp != null)
                {
                    FinalResult.Add(new
                    {
                        Index = i.ToFormatTanggalHari(),
                        Transaksi = temp.Transaksi,
                        NonPelanggan = temp.NonPelanggan,
                        Pelanggan = temp.Pelanggan,
                        Tamu = temp.Tamu,
                        Quantity = temp.Quantity,
                        Discount = temp.Discount,
                        NonDiscount = temp.NonDiscount,
                        Pengiriman = temp.Pengiriman,
                        NonPengiriman = temp.NonPengiriman,
                        Nominal = temp.Nominal
                    });
                }
                else
                {
                    FinalResult.Add(new
                    {
                        Index = i.ToFormatTanggalHari(),
                        Transaksi = 0,
                        NonPelanggan = 0,
                        Pelanggan = 0,
                        Tamu = 0,
                        Quantity = 0,
                        Discount = 0,
                        NonDiscount = 0,
                        Pengiriman = 0,
                        NonPengiriman = 0,
                        Nominal = 0
                    });
                }
            }
            #endregion
        }
        else if (jenisLaporan == 2) //BULANAN
        {
            #region BULANAN
            var ResultGroupBy = Data
                .GroupBy(item => item.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    item.Key,
                    Transaksi = item.Count(),
                    NonPelanggan = item.Where(item2 => item2.IDPelanggan == 1).Count(),
                    Pelanggan = item.Where(item2 => item2.IDPelanggan != 1).Count(), //GENERAL CUSTOMER
                    Tamu = item.Sum(item2 => item2.JumlahTamu),
                    Quantity = item.Sum(item2 => item2.JumlahProduk),
                    Discount = item.Where(item2 => item2.PotonganTransaksi != 0 || item2.TotalPotonganHargaJualDetail != 0 || item2.TotalDiscountVoucher != 0).Count(),
                    NonDiscount = item.Where(item2 => item2.PotonganTransaksi == 0 || item2.TotalPotonganHargaJualDetail == 0 || item2.TotalDiscountVoucher == 0).Count(),
                    Pengiriman = item.Where(item2 => item2.BiayaPengiriman != 0).Count(),
                    NonPengiriman = item.Where(item2 => item2.BiayaPengiriman == 0).Count(),
                    Nominal = item.Sum(item2 => item2.GrandTotal.Value)
                });

            for (int i = 1; i <= 12; i++)
            {
                var temp = ResultGroupBy.FirstOrDefault(item => item.Key == i);

                if (temp != null)
                {
                    FinalResult.Add(new
                    {
                        Index = Pengaturan.Bulan(i),
                        Transaksi = temp.Transaksi,
                        NonPelanggan = temp.NonPelanggan,
                        Pelanggan = temp.Pelanggan,
                        Tamu = temp.Tamu,
                        Quantity = temp.Quantity,
                        Discount = temp.Discount,
                        NonDiscount = temp.NonDiscount,
                        Pengiriman = temp.Pengiriman,
                        NonPengiriman = temp.NonPengiriman,
                        Nominal = temp.Nominal
                    });
                }
                else
                {
                    FinalResult.Add(new
                    {
                        Index = Pengaturan.Bulan(i),
                        Transaksi = 0,
                        NonPelanggan = 0,
                        Pelanggan = 0,
                        Tamu = 0,
                        Quantity = 0,
                        Discount = 0,
                        NonDiscount = 0,
                        Pengiriman = 0,
                        NonPengiriman = 0,
                        Nominal = 0
                    });
                }
            }
            #endregion
        }
        else if (jenisLaporan == 3) //TAHUNAN
        {
            #region TAHUNAN
            var ResultGroupBy = Data
                .GroupBy(item => item.TanggalOperasional.Value.Year)
                .Select(item => new
                {
                    item.Key,
                    Transaksi = item.Count(),
                    NonPelanggan = item.Where(item2 => item2.IDPelanggan == 1).Count(),
                    Pelanggan = item.Where(item2 => item2.IDPelanggan != 1).Count(), //GENERAL CUSTOMER
                    Tamu = item.Sum(item2 => item2.JumlahTamu),
                    Quantity = item.Sum(item2 => item2.JumlahProduk),
                    Discount = item.Where(item2 => item2.PotonganTransaksi != 0 || item2.TotalPotonganHargaJualDetail != 0 || item2.TotalDiscountVoucher != 0).Count(),
                    NonDiscount = item.Where(item2 => item2.PotonganTransaksi == 0 || item2.TotalPotonganHargaJualDetail == 0 || item2.TotalDiscountVoucher == 0).Count(),
                    Pengiriman = item.Where(item2 => item2.BiayaPengiriman != 0).Count(),
                    NonPengiriman = item.Where(item2 => item2.BiayaPengiriman == 0).Count(),
                    Nominal = item.Sum(item2 => item2.GrandTotal.Value)
                });

            FinalResult.AddRange(ResultGroupBy.Select(item => new
            {
                Index = item.Key,
                Transaksi = item.Transaksi,
                NonPelanggan = item.NonPelanggan,
                Pelanggan = item.Pelanggan,
                Tamu = item.Tamu,
                Quantity = item.Quantity,
                Discount = item.Discount,
                NonDiscount = item.NonDiscount,
                Pengiriman = item.Pengiriman,
                NonPengiriman = item.NonPengiriman,
                Nominal = item.Nominal
            }));
            #endregion
        }

        Result.Add("Data", FinalResult);

        tempPencarian += "&JenisLaporan=" + jenisLaporan;

        string tempPeriode = jenisLaporan == 3 ? "Tahunan" : (jenisLaporan == 1 ? "Harian" : "Bulanan") + " " + tanggalAwal.Year;

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Ringkasan", tempPeriode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "");
            Excel_Class.Header(3, "Tamu");
            Excel_Class.Header(4, "Quantity");
            Excel_Class.Header(5, "Pelanggan");
            Excel_Class.Header(6, "Bukan Pelanggan");
            Excel_Class.Header(7, "Discount");
            Excel_Class.Header(8, "Tidak Discount");
            Excel_Class.Header(9, "Delivery");
            Excel_Class.Header(10, "Bukan Delivery");
            Excel_Class.Header(11, "Transaksi");
            Excel_Class.Header(12, "Nominal");

            int index = 2;

            foreach (var item in FinalResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.Index);
                Excel_Class.Content(index, 3, item.Tamu);
                Excel_Class.Content(index, 4, item.Quantity);
                Excel_Class.Content(index, 5, item.Pelanggan);
                Excel_Class.Content(index, 6, item.NonPelanggan);
                Excel_Class.Content(index, 7, item.Discount);
                Excel_Class.Content(index, 8, item.NonDiscount);
                Excel_Class.Content(index, 9, item.Pengiriman);
                Excel_Class.Content(index, 10, item.NonPengiriman);
                Excel_Class.Content(index, 11, item.Transaksi);
                Excel_Class.Content(index, 12, item.Nominal);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("JenisLaporan", tempPeriode);

        Result.Add("Tamu", FinalResult.Sum(item => item.Tamu).ToFormatHargaBulat());
        Result.Add("Quantity", FinalResult.Sum(item => item.Quantity).ToFormatHargaBulat());
        Result.Add("Pelanggan", FinalResult.Sum(item => item.Pelanggan).ToFormatHarga());
        Result.Add("NonPelanggan", FinalResult.Sum(item => item.NonPelanggan).ToFormatHarga());
        Result.Add("Discount", FinalResult.Sum(item => item.Discount).ToFormatHarga());
        Result.Add("NonDiscount", FinalResult.Sum(item => item.NonDiscount).ToFormatHarga());
        Result.Add("Pengiriman", FinalResult.Sum(item => item.Pengiriman).ToFormatHarga());
        Result.Add("NonPengiriman", FinalResult.Sum(item => item.NonPengiriman).ToFormatHarga());
        Result.Add("Transaksi", FinalResult.Sum(item => item.Transaksi).ToFormatHargaBulat());
        Result.Add("Nominal", FinalResult.Sum(item => (decimal)item.Nominal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> Consignment(int idTempat, int idPemilikProduk)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var ListStokProduk = db.TBStokProduks.ToArray();

        var ListPenjualan = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TanggalOperasional.Value >= tanggalAwal &&
                item.TBTransaksi.TanggalOperasional.Value <= tanggalAkhir &&
                item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
            .ToArray();

        #region FILTER TEMPAT
        if (idTempat != 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            Result.Add("Tempat", ClassTempat.Cari(idTempat).Nama);

            ListStokProduk = ListStokProduk.Where(item => item.IDTempat == idTempat).ToArray();
            ListPenjualan = ListPenjualan.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER PEMILIK PRODUK
        if (idPemilikProduk != 0)
        {
            PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

            Result.Add("PemilikProduk", ClassPemilikProduk.Cari(idPemilikProduk).Nama);

            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();
            ListPenjualan = ListPenjualan.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();
        }
        else
            Result.Add("PemilikProduk", "Semua Pemilik Produk");
        #endregion

        var NewListPenjualan = ListPenjualan
            .GroupBy(item => item.IDKombinasiProduk)
            .Select(item => new
            {
                item.Key,
                Quantity = item.Sum(item2 => item2.Quantity),
                BeforeDiscount = item.Sum(item2 => item2.Quantity * item2.HargaJual),
                Discount = item.Sum(item2 => item2.Quantity * item2.Discount),
                Subtotal = item.Sum(item2 => item2.Subtotal),
                Consignment = item.Sum(item2 => item2.Subtotal - (item2.Quantity * item2.HargaBeli)),
                PayToBrand = item.Sum(item2 => item2.Quantity * item2.HargaBeli)
            });

        var DataResult = ListStokProduk
            .GroupBy(item => new
            {
                item.IDKombinasiProduk,
                Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                item.TBKombinasiProduk.IDAtributProduk,
                Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama
            })
            .Select(item => new
            {
                item.Key,
                Stok = item.Sum(item2 => item2.Jumlah),
                HargaJual = item.Average(item2 => item2.HargaJual),
                NominalStok = item.Sum(item2 => item2.Jumlah * item2.HargaJual),
                Penjualan = NewListPenjualan.FirstOrDefault(item2 => item2.Key == item.Key.IDKombinasiProduk)
            })
            .Where(item => item.Stok != 0 || (item.Penjualan != null && item.Penjualan.Quantity > 0))
            .OrderBy(item => item.Key.Produk)
            .ThenBy(item => item.Key.IDAtributProduk);

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;

        Result.Add("Data", DataResult);

        Result.Add("StockQuantity", DataResult.Sum(item => item.Stok));
        Result.Add("StockNominal", DataResult.Sum(item => item.NominalStok));

        Result.Add("SalesQuantity", NewListPenjualan.Sum(item => item.Quantity));
        Result.Add("SalesBeforeDiscount", NewListPenjualan.Sum(item => item.BeforeDiscount));
        Result.Add("SalesDiscount", NewListPenjualan.Sum(item => item.Discount));
        Result.Add("SalesSubtotal", NewListPenjualan.Sum(item => item.Subtotal));
        Result.Add("SalesConsignment", NewListPenjualan.Sum(item => item.Consignment));
        Result.Add("SalesPayToBrand", NewListPenjualan.Sum(item => item.PayToBrand));

        Result.Add("TotalProduk", DataResult.Count());

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Consignment", Periode, 14);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Brand");
            Excel_Class.Header(3, "Produk");
            Excel_Class.Header(4, "Warna");
            Excel_Class.Header(5, "Varian");

            Excel_Class.Header(6, "Stock Qty");
            Excel_Class.Header(7, "Harga");
            Excel_Class.Header(8, "Stock Nominal");

            Excel_Class.Header(9, "Sales Qty");
            Excel_Class.Header(10, "Before Disc.");
            Excel_Class.Header(11, "Disc.");
            Excel_Class.Header(12, "Subtotal");
            Excel_Class.Header(13, "Consignment");
            Excel_Class.Header(14, "Pay to Brand");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.Key.Brand);
                Excel_Class.Content(index, 3, item.Key.Produk);
                Excel_Class.Content(index, 4, item.Key.Warna);
                Excel_Class.Content(index, 5, item.Key.Varian);

                Excel_Class.Content(index, 6, item.Stok.Value);
                Excel_Class.Content(index, 7, item.HargaJual.Value);
                Excel_Class.Content(index, 8, item.NominalStok.Value);

                Excel_Class.Content(index, 9, item.Penjualan != null ? item.Penjualan.Quantity : 0);
                Excel_Class.Content(index, 10, item.Penjualan != null ? item.Penjualan.BeforeDiscount : 0);
                Excel_Class.Content(index, 11, item.Penjualan != null ? item.Penjualan.Discount : 0);
                Excel_Class.Content(index, 12, item.Penjualan != null ? item.Penjualan.Subtotal.Value : 0);
                Excel_Class.Content(index, 13, item.Penjualan != null ? item.Penjualan.Consignment.Value : 0);
                Excel_Class.Content(index, 14, item.Penjualan != null ? item.Penjualan.PayToBrand : 0);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    public Dictionary<string, dynamic> GrossProfit()
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var ListPenjualan = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TanggalOperasional.Value >= tanggalAwal &&
                item.TBTransaksi.TanggalOperasional.Value <= tanggalAkhir &&
                item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
            .GroupBy(item => new
            {
                item.TBTransaksi.IDTempat,
                item.TBTransaksi.TBTempat.Nama
            })
            .Select(item => new
            {
                item.Key,
                Quantity = item.Sum(item2 => item2.Quantity),
                BeforeDiscount = item.Sum(item2 => item2.Quantity * item2.HargaJual),
                Discount = item.Sum(item2 => item2.Quantity * item2.Discount),
                AfterDiscount = item.Sum(item2 => item2.Subtotal),
                COGS = item.Sum(item2 => item2.Quantity * item2.HargaBeli),
                GrossProfit = item.Sum(item2 => item2.Subtotal - (item2.Quantity * item2.HargaBeli)),
            })
            .OrderByDescending(item => item.GrossProfit)
            .ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        Result.Add("Data", ListPenjualan);

        Result.Add("Quantity", ListPenjualan.Sum(item => item.Quantity));
        Result.Add("BeforeDiscount", ListPenjualan.Sum(item => item.BeforeDiscount));
        Result.Add("Discount", ListPenjualan.Sum(item => item.Discount));
        Result.Add("AfterDiscount", ListPenjualan.Sum(item => item.AfterDiscount));
        Result.Add("COGS", ListPenjualan.Sum(item => item.COGS));
        Result.Add("GrossProfit", ListPenjualan.Sum(item => item.GrossProfit));
        Result.Add("Persentase", ListPenjualan.Sum(item => item.AfterDiscount) > 0 ? (ListPenjualan.Sum(item => item.GrossProfit) / ListPenjualan.Sum(item => item.AfterDiscount) * 100) : 0);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Gross Profit Store", Periode, 9);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Lokasi");
            Excel_Class.Header(3, "Quantity");
            Excel_Class.Header(4, "Sales Before Disc.");
            Excel_Class.Header(5, "Disc.");
            Excel_Class.Header(6, "Sales After Disc.");
            Excel_Class.Header(7, "COGS");
            Excel_Class.Header(8, "Gross Profit");
            Excel_Class.Header(9, "%");

            int index = 2;

            foreach (var item in ListPenjualan)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.Key.Nama);
                Excel_Class.Content(index, 3, item.Quantity);
                Excel_Class.Content(index, 4, item.BeforeDiscount);
                Excel_Class.Content(index, 5, item.Discount);
                Excel_Class.Content(index, 6, item.AfterDiscount.Value);
                Excel_Class.Content(index, 7, item.COGS);
                Excel_Class.Content(index, 8, item.GrossProfit.Value);
                Excel_Class.Content(index, 9, item.AfterDiscount.Value > 0 ? (item.GrossProfit.Value / item.AfterDiscount.Value * 100) : 0);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    public Dictionary<string, dynamic> StokMultistore(int idJenisStokProduk, int idKategoriTempat, string kode, int idProduk, int idAtributProduk, int idPemilikProduk, int idKategoriProduk)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        tempPencarian += "?IDJenisStokProduk=" + idJenisStokProduk;
        tempPencarian += "&IDKategoriTempat=" + idKategoriTempat;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;
        tempPencarian += "&IDKategoriProduk=" + idKategoriProduk;

        #region TEMPAT
        Tempat_Class ClassTempat = new Tempat_Class(db);

        var ListTempat = ClassTempat.Data()
            .Where(item => idKategoriTempat != 0 ? item.IDKategoriTempat == idKategoriTempat : item.IDKategoriTempat > 0)
            .Select(item => new
            {
                item.IDTempat,
                item.Nama
            });

        Result.Add("TempatJumlah", ListTempat.Count());
        #endregion

        #region ID JENIS STOK PRODUK
        switch (idJenisStokProduk)
        {
            case 1: Result["JenisStokProduk"] = "Jenis Stok Produk : Ada Stok"; break;
            case 2: Result["JenisStokProduk"] = "Jenis Stok Produk : Tidak Ada Stok"; break;
            case 3: Result["JenisStokProduk"] = "Jenis Stok Produk : Minus"; break;
            case 0: Result["JenisStokProduk"] = "Jenis Stok Produk : Semua"; break;
        }
        #endregion

        TBKombinasiProduk[] ListKombinasiProduk = db.TBKombinasiProduks.Where(item => item.TBProduk._IsActive == true).ToArray();

        if (!string.IsNullOrWhiteSpace(kode))
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.KodeKombinasiProduk.ToLower().Contains(kode.ToLower())).ToArray();

        if (idProduk != 0)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.IDAtributProduk == idAtributProduk).ToArray();

        if (idPemilikProduk != 0)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();

        if (idKategoriProduk != 0)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(item2 => item2.IDKategoriProduk == idKategoriProduk) != null).ToArray();

        if (idJenisStokProduk == 1)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.TBStokProduks.Sum(item2 => item2.Jumlah) > 0).ToArray();
        else if (idJenisStokProduk == 2)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.TBStokProduks.Sum(item2 => item2.Jumlah) == 0).ToArray();
        else if (idJenisStokProduk == 3)
            ListKombinasiProduk = ListKombinasiProduk.Where(item => item.TBStokProduks.Sum(item2 => item2.Jumlah) < 0).ToArray();

        var ResultKombinasiProduk = ListKombinasiProduk
            .Select(item => new
            {
                Produk = item.TBProduk.Nama,
                item.IDAtributProduk,
                AtributProduk = item.TBAtributProduk.Nama,
                item.KodeKombinasiProduk,
                item.Nama,
                PemilikProduk = item.TBProduk.TBPemilikProduk.Nama,
                KategoriProduk = GabungkanSemuaKategoriProduk(null, item),
                Total = item.TBStokProduks.Where(item2 => ListTempat.Any(item3 => item3.IDTempat == item2.IDTempat)).Sum(item2 => item2.Jumlah),
                StokProduk = ListTempat.Select(item2 => new
                {
                    item2.IDTempat,
                    Stok = item.TBStokProduks.FirstOrDefault(item3 => item3.IDTempat == item2.IDTempat)
                })
            })
            .OrderBy(item => item.Produk)
            .ThenBy(item => item.IDAtributProduk);

        Result.Add("Data", ResultKombinasiProduk);

        var ResultListTempat = ListTempat.Select(item => new
        {
            item.IDTempat,
            item.Nama,
            Total = ResultKombinasiProduk.Sum(item2 => item2.StokProduk.Where(item3 => item3.Stok != null && item3.IDTempat == item.IDTempat).Sum(item3 => item3.Stok.Jumlah))
        });

        Result.Add("Tempat", ResultListTempat);
        Result.Add("TempatTotal", ResultKombinasiProduk.Sum(item => item.Total));

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Stock Multistore", Periode, (7 + ListTempat.Count()));
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Kode");
            Excel_Class.Header(3, "Produk");
            Excel_Class.Header(4, "Varian");
            Excel_Class.Header(5, "Brand");
            Excel_Class.Header(6, "Kategori");
            Excel_Class.Header(7, "Total");

            int i = 8;

            foreach (var Tempat in ListTempat)
            {
                Excel_Class.Header(i++, Tempat.Nama);
            }

            int index = 2;

            foreach (var KombinasiProduk in ResultKombinasiProduk)
            {
                Excel_Class.Content(index, 1, index - 1);

                Excel_Class.Content(index, 2, KombinasiProduk.KodeKombinasiProduk);
                Excel_Class.Content(index, 3, KombinasiProduk.Produk);
                Excel_Class.Content(index, 4, KombinasiProduk.AtributProduk);
                Excel_Class.Content(index, 5, KombinasiProduk.PemilikProduk);
                Excel_Class.Content(index, 6, KombinasiProduk.KategoriProduk);
                Excel_Class.Content(index, 7, KombinasiProduk.Total.Value);

                i = 8;

                foreach (var StokProduk in KombinasiProduk.StokProduk)
                {
                    Excel_Class.Content(index, i++, (StokProduk.Stok != null) ? StokProduk.Stok.Jumlah.Value : 0);
                }

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    public Dictionary<string, dynamic> NetRevenue(List<int> ListIDTempat, List<int> ListIDJenisTransaksi, List<int> ListIDStatusTransaksi, DateTime _tanggalAwal, DateTime _tanggalAkhir)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        if (_tanggalAwal == _tanggalAkhir)
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam());
        else
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam() + " - " + _tanggalAkhir.ToFormatTanggalJam());

        var ListTransaksi = db.TBTransaksis
            .Where(item =>
                item.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value >= _tanggalAwal &&
                item.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value <= _tanggalAkhir)
            .ToArray();

        var ListJenisPembayaran = db.TBTransaksiJenisPembayarans
            .Where(item =>
                item.Tanggal.Value >= _tanggalAwal &&
                item.Tanggal.Value <= _tanggalAkhir)
            .ToArray();

        var ListProdukSemua = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value >= _tanggalAwal &&
                item.TBTransaksi.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value <= _tanggalAkhir)
            .ToArray();

        var ListProduk = ListProdukSemua
            .Where(item => item.Quantity > 0)
            .ToArray();

        var ListProdukRetur = ListProdukSemua
            .Where(item => item.Quantity < 0)
            .ToArray();

        #region FILTER STATUS TRANSAKSI
        if (ListIDStatusTransaksi.Count > 0)
        {
            StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
            string tempStatusTranskasi = "Status : ";

            foreach (var item in ListIDStatusTransaksi)
            {
                tempStatusTranskasi += StatusTransaksi_Class.Cari(db, item).Nama + ", ";
            }

            Result.Add("StatusTransaksi", tempStatusTranskasi.Substring(0, tempStatusTranskasi.Length - 2));

            ListTransaksi = ListTransaksi.Where(item => ListIDStatusTransaksi.Contains(item.IDStatusTransaksi.Value)).ToArray();
            ListJenisPembayaran = ListJenisPembayaran.Where(item => ListIDStatusTransaksi.Contains(item.TBTransaksi.IDStatusTransaksi.Value)).ToArray();
            ListProduk = ListProduk.Where(item => ListIDStatusTransaksi.Contains(item.TBTransaksi.IDStatusTransaksi.Value)).ToArray();
            ListProdukRetur = ListProdukRetur.Where(item => ListIDStatusTransaksi.Contains(item.TBTransaksi.IDStatusTransaksi.Value)).ToArray();
        }
        else
            Result.Add("StatusTransaksi", "Semua Status Transaksi");
        #endregion

        #region FILTER TEMPAT
        if (ListIDTempat.Count > 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            string tempTempat = "Tempat : ";

            foreach (var item in ListIDTempat)
            {
                tempTempat += ClassTempat.Cari(item).Nama + ", ";
            }

            Result.Add("Tempat", tempTempat.Substring(0, tempTempat.Length - 2));

            ListTransaksi = ListTransaksi.Where(item => ListIDTempat.Contains(item.IDTempat.Value)).ToArray();
            ListJenisPembayaran = ListJenisPembayaran.Where(item => ListIDTempat.Contains(item.TBTransaksi.IDTempat.Value)).ToArray();
            ListProduk = ListProduk.Where(item => ListIDTempat.Contains(item.TBTransaksi.IDTempat.Value)).ToArray();
            ListProdukRetur = ListProdukRetur.Where(item => ListIDTempat.Contains(item.TBTransaksi.IDTempat.Value)).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER JENIS TRANSAKSI
        if (ListIDJenisTransaksi.Count > 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
            string tempNamaJenisTransaksi = "Jenis : ";

            foreach (var item in ListIDJenisTransaksi)
            {
                tempNamaJenisTransaksi += ClassJenisTransaksi.Cari(db, item).Nama + ", ";
            }

            Result.Add("JenisTransaksi", tempNamaJenisTransaksi.Substring(0, tempNamaJenisTransaksi.Length - 2));

            ListTransaksi = ListTransaksi.Where(item => ListIDJenisTransaksi.Contains(item.IDJenisTransaksi.Value)).ToArray();
            ListJenisPembayaran = ListJenisPembayaran.Where(item => ListIDJenisTransaksi.Contains(item.TBTransaksi.IDJenisTransaksi.Value)).ToArray();
            ListProduk = ListProduk.Where(item => ListIDJenisTransaksi.Contains(item.TBTransaksi.IDJenisTransaksi.Value)).ToArray();
            ListProdukRetur = ListProdukRetur.Where(item => ListIDJenisTransaksi.Contains(item.TBTransaksi.IDJenisTransaksi.Value)).ToArray();
        }
        else
            Result.Add("JenisTransaksi", "Semua Jenis Transaksi");
        #endregion

        var DataResult = ListTransaksi
            .Select(item => new
            {
                Tanggal = item.TanggalTransaksi.Value,
                IDTransaksi = item.IDTransaksi,
                JenisTransaksi = item.TBJenisTransaksi.Nama,
                StatusTransaksi = item.TBStatusTransaksi.Nama,
                TotalPrice = item.Subtotal + item.TotalPotonganHargaJualDetail,
                Discount = item.TotalPotonganHargaJualDetail + item.TotalDiscountVoucher + item.PotonganTransaksi,
                BiayaPengiriman = item.BiayaPengiriman,
                Pembulatan = item.Pembulatan,
                GrandTotal = (item.Subtotal + item.TotalPotonganHargaJualDetail) - (item.TotalPotonganHargaJualDetail + item.TotalDiscountVoucher + item.PotonganTransaksi) + item.BiayaPengiriman + item.Pembulatan,
                NetRevenue = (item.Subtotal + item.TotalPotonganHargaJualDetail) - (item.TotalPotonganHargaJualDetail + item.TotalDiscountVoucher + item.PotonganTransaksi) + item.Pembulatan,
                TotalHargaBeli = item.TBTransaksiDetails.Sum(item2 => item2.Quantity * item2.HargaBeli),
                TotalGrossProfit = ((item.Subtotal + item.TotalPotonganHargaJualDetail) - (item.TotalPotonganHargaJualDetail + item.TotalDiscountVoucher + item.PotonganTransaksi)) - (item.TBTransaksiDetails.Sum(item2 => item2.Quantity * item2.HargaBeli)) + item.Pembulatan,
                Keterangan = item.Keterangan,
                JenisPembayaran = item.TBTransaksiJenisPembayarans.Count > 0 ? item.TBTransaksiJenisPembayarans.FirstOrDefault().TBJenisPembayaran.Nama : "",
                CountProduk = item.TBTransaksiDetails.Count(),

                Produk = item.TBTransaksiDetails
                    .Select(item2 => new
                    {
                        item2.TBKombinasiProduk.Nama,
                        Kategori = item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                        Brand = item2.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                        BeforeDiscount = item2.HargaJual * item2.Quantity,
                        Discount = item2.Discount * item2.Quantity
                    })
                    .FirstOrDefault(),

                Detail = item.TBTransaksiDetails.Skip(1)
                    .Select(item2 => new
                    {
                        item2.TBKombinasiProduk.Nama,
                        Kategori = item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                        Brand = item2.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                        BeforeDiscount = item2.HargaJual * item2.Quantity,
                        Discount = item2.Discount * item2.Quantity
                    }).ToArray(),

                Pembayaran = item.TBTransaksiJenisPembayarans
                    .Select(item2 => new
                    {
                        item2.Total,
                        JenisPembayaran = item2.TBJenisPembayaran.Nama,
                        item2.Tanggal
                    })
            })
            .OrderBy(item => item.Tanggal);

        var DataJenisPembayaran = ListJenisPembayaran
            .GroupBy(item => new
            {
                item.IDJenisPembayaran,
                item.TBJenisPembayaran.Nama
            })
            .Select(item => new
            {
                item.Key,
                Total = item.Sum(item2 => item2.Total)
            })
            .OrderBy(item => item.Key.IDJenisPembayaran);

        var DataResultRetur = ListProdukRetur
            .Select(item => new
            {
                IDTransaksi = item.TBTransaksi.IDTransaksi,
                JenisTransaksi = item.TBTransaksi.TBJenisTransaksi.Nama,
                StatusTransaksi = item.TBTransaksi.TBStatusTransaksi.Nama,
                Keterangan = item.TBTransaksi.Keterangan,
                Produk = item.TBKombinasiProduk.Nama,
                Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                HargaJual = item.HargaJual,
                Discount = item.Discount,
                Quantity = Math.Abs(item.Quantity),
                NetRevenue = Math.Abs(item.Subtotal.Value),
                HargaBeli = Math.Abs(item.Quantity) * item.HargaBeli,
                GrossProfit = Math.Abs(item.Subtotal.Value) - (Math.Abs(item.Quantity) * item.HargaBeli)
            });

        //var DataBrand = ListProduk
        //    .GroupBy(item => new
        //    {
        //        item.TBKombinasiProduk.TBProduk.IDPemilikProduk,
        //        item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama
        //    })
        //    .Select(item => new
        //    {
        //        item.Key,
        //        Total = item.Sum(item2 => item2.Subtotal),
        //        JumlahData = ListProduk
        //        .Where(item2 => item2.TBKombinasiProduk.TBProduk.IDPemilikProduk == item.Key.IDPemilikProduk)
        //        .GroupBy()
        //        .Count(),
        //        Produk = ListProduk
        //        .Where(item2 => item2.TBKombinasiProduk.TBProduk.IDPemilikProduk == item.Key.IDPemilikProduk)
        //        .Select(item2 => new
        //        {
        //            item2.TBKombinasiProduk.Nama
        //        })
        //    });

        //Result.Add("DataBrand", null);

        Result.Add("DataJenisPembayaran", DataJenisPembayaran);

        Result.Add("TotalJenisPembayaran", DataJenisPembayaran.Sum(item => item.Total));

        Result.Add("DataRetur", DataResultRetur);

        Result.Add("ReturQty", DataResultRetur.Sum(item => item.Quantity));
        Result.Add("ReturNetRevenue", DataResultRetur.Sum(item => item.NetRevenue));
        Result.Add("ReturCOGS", DataResultRetur.Sum(item => item.HargaBeli));
        Result.Add("ReturGrossProfit", DataResultRetur.Sum(item => item.GrossProfit));

        tempPencarian += "?TanggalAwal=" + _tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + _tanggalAkhir;
        tempPencarian += "&IDStatusTransaksi=" + string.Join(",", ListIDStatusTransaksi);
        tempPencarian += "&IDTempat=" + string.Join(",", ListIDTempat);
        tempPencarian += "&IDJenisTransaksi=" + string.Join(",", ListIDJenisTransaksi);

        Result.Add("Data", DataResult);

        Result.Add("TotalPrice", DataResult.Sum(item => item.TotalPrice));
        Result.Add("Discount", DataResult.Sum(item => item.Discount));
        Result.Add("BiayaPengiriman", DataResult.Sum(item => item.BiayaPengiriman));
        Result.Add("Pembulatan", DataResult.Sum(item => item.Pembulatan));
        Result.Add("GrandTotal", DataResult.Sum(item => item.GrandTotal));
        Result.Add("NetRevenue", DataResult.Sum(item => item.NetRevenue));
        Result.Add("TotalCOGS", DataResult.Sum(item => item.TotalHargaBeli));
        Result.Add("TotalGrossProfit", DataResult.Sum(item => item.TotalGrossProfit));

        Result.Add("SebelumRetur", Result["NetRevenue"] + Result["ReturNetRevenue"]);

        Konfigurasi_Class Konfigurasi = new Konfigurasi_Class(pengguna.IDGrupPengguna);
        bool MelihatCOGS = Konfigurasi.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue);

        Result.Add("MelihatCOGS", MelihatCOGS);

        if (excel)
        {
            #region EXCEL
            int JumlahKolom = 0;
            int Batas = 0;

            if (MelihatCOGS)
            {
                JumlahKolom = 19;
                Batas = 19;
            }
            else
            {
                JumlahKolom = 18;
                Batas = 18;
            }

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Net Revenue", Result["Periode"], Batas + JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Transaksi");
            Excel_Class.Header(3, "Tanggal");
            Excel_Class.Header(4, "Jenis");
            Excel_Class.Header(5, "Status");
            Excel_Class.Header(6, "Produk");
            Excel_Class.Header(7, "Kategori");
            Excel_Class.Header(8, "Brand");
            Excel_Class.Header(9, "Price");
            Excel_Class.Header(10, "Discount");
            Excel_Class.Header(11, "Biaya Pengiriman");
            Excel_Class.Header(12, "Pembulatan");
            Excel_Class.Header(13, "Grand Total");
            Excel_Class.Header(14, "Net Revenue");

            if (MelihatCOGS)
            {
                Excel_Class.Header(15, "COGS");
                Excel_Class.Header(16, "Gross Profit");
                Excel_Class.Header(17, "Pembayaran");
                Excel_Class.Header(18, "Keterangan");
            }
            else
            {
                Excel_Class.Header(15, "Pembayaran");
                Excel_Class.Header(16, "Keterangan");
            }

            Excel_Class.SetBackground(1, Batas, System.Drawing.Color.Gray);

            int index = 2;
            int Nomor = 1;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, Nomor);
                Excel_Class.Content(index, 2, item.IDTransaksi);
                Excel_Class.Content(index, 3, item.Tanggal);
                Excel_Class.Content(index, 4, item.JenisTransaksi);
                Excel_Class.Content(index, 5, item.StatusTransaksi);
                Excel_Class.Content(index, 6, item.Produk.Nama);
                Excel_Class.Content(index, 7, item.Produk.Kategori);
                Excel_Class.Content(index, 8, item.Produk.Brand);
                Excel_Class.Content(index, 9, item.Produk.BeforeDiscount);
                Excel_Class.Content(index, 10, item.Produk.Discount);

                Excel_Class.Content(index, 11, item.BiayaPengiriman.Value);
                Excel_Class.Content(index, 12, item.Pembulatan.Value);
                Excel_Class.Content(index, 13, item.GrandTotal.Value);
                Excel_Class.Content(index, 14, item.NetRevenue.Value);

                string ListPembayaran = string.Empty;
                int indexPembayaran = 0;
                foreach (var item2 in item.Pembayaran)
                {
                    indexPembayaran += 1;
                    ListPembayaran += item2.Tanggal.Value.ToString("d MMMM yyyy HH:mm") + " - " + item2.JenisPembayaran + " - " + item2.Total;
                    if (indexPembayaran == item.Pembayaran.Count())
                    {
                        ListPembayaran += ", ";
                    }
                }
                if (MelihatCOGS)
                {
                    Excel_Class.Content(index, 15, item.TotalHargaBeli);
                    Excel_Class.Content(index, 16, item.TotalGrossProfit.Value);
                    Excel_Class.Content(index, 17, ListPembayaran);
                    Excel_Class.Content(index, 18, item.Keterangan);
                }
                else
                {
                    Excel_Class.Content(index, 15, item.TotalHargaBeli);
                    Excel_Class.Content(index, 16, item.Keterangan);
                }
                Excel_Class.SetBackground(index, Batas, System.Drawing.Color.Gray);

                index++;
                Nomor++;
                foreach (var item2 in item.Detail)
                {
                    Excel_Class.Content(index, 6, item2.Nama);
                    Excel_Class.Content(index, 7, item2.Kategori);
                    Excel_Class.Content(index, 8, item2.Brand);
                    Excel_Class.Content(index, 9, item2.BeforeDiscount);
                    Excel_Class.Content(index, 10, item2.Discount);

                    Excel_Class.SetBackground(index, Batas, System.Drawing.Color.Gray);

                    index++;
                }
            }

            index = 2;

            Excel_Class.Header(Batas + 1, "No.");
            Excel_Class.Header(Batas + 2, "ID Transaksi");
            Excel_Class.Header(Batas + 3, "Jenis");
            Excel_Class.Header(Batas + 4, "Status");
            Excel_Class.Header(Batas + 5, "Keterangan");
            Excel_Class.Header(Batas + 6, "Produk");
            Excel_Class.Header(Batas + 7, "Kategori");
            Excel_Class.Header(Batas + 8, "Brand");
            Excel_Class.Header(Batas + 9, "Price");
            Excel_Class.Header(Batas + 10, "Discount");
            Excel_Class.Header(Batas + 11, "Qty");
            Excel_Class.Header(Batas + 12, "Net Revenue");

            if (MelihatCOGS)
            {
                Excel_Class.Header(Batas + 13, "COGS");
                Excel_Class.Header(Batas + 14, "Gross Profit");
            }

            foreach (var item in DataResultRetur)
            {
                Excel_Class.Content(index, Batas + 1, index - 1);
                Excel_Class.Content(index, Batas + 2, item.IDTransaksi);
                Excel_Class.Content(index, Batas + 3, item.JenisTransaksi);
                Excel_Class.Content(index, Batas + 4, item.StatusTransaksi);
                Excel_Class.Content(index, Batas + 5, item.Keterangan);
                Excel_Class.Content(index, Batas + 6, item.Produk);
                Excel_Class.Content(index, Batas + 7, item.Kategori);
                Excel_Class.Content(index, Batas + 8, item.Brand);
                Excel_Class.Content(index, Batas + 9, item.HargaJual);
                Excel_Class.Content(index, Batas + 10, item.Discount);
                Excel_Class.Content(index, Batas + 11, item.Quantity);
                Excel_Class.Content(index, Batas + 12, item.NetRevenue);

                if (MelihatCOGS)
                {
                    Excel_Class.Content(index, Batas + 13, item.HargaBeli);
                    Excel_Class.Content(index, Batas + 14, item.GrossProfit);
                }

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }

    public Dictionary<string, dynamic> NetRevenueJenisTransaksi(List<int> ListIDTempat, List<int> ListIDJenisTransaksi, List<int> ListIDStatusTransaksi, DateTime _tanggalAwal, DateTime _tanggalAkhir)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        if (_tanggalAwal == _tanggalAkhir)
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam());
        else
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam() + " - " + _tanggalAkhir.ToFormatTanggalJam());

        var ListProdukSemua = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value >= _tanggalAwal &&
                item.TBTransaksi.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value <= _tanggalAkhir)
            .ToArray();

        #region FILTER STATUS TRANSAKSI
        if (ListIDStatusTransaksi.Count > 0)
        {
            StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
            string tempStatusTranskasi = "Status : ";

            foreach (var item in ListIDStatusTransaksi)
            {
                tempStatusTranskasi += StatusTransaksi_Class.Cari(db, item).Nama + ", ";
            }

            Result.Add("StatusTransaksi", tempStatusTranskasi.Substring(0, tempStatusTranskasi.Length - 2));

            ListProdukSemua = ListProdukSemua.Where(item => ListIDStatusTransaksi.Contains(item.TBTransaksi.IDStatusTransaksi.Value)).ToArray();
        }
        else
            Result.Add("StatusTransaksi", "Semua Status Transaksi");
        #endregion

        #region FILTER TEMPAT
        if (ListIDTempat.Count > 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            string tempTempat = "Tempat : ";

            foreach (var item in ListIDTempat)
            {
                tempTempat += ClassTempat.Cari(item).Nama + ", ";
            }

            Result.Add("Tempat", tempTempat.Substring(0, tempTempat.Length - 2));

            ListProdukSemua = ListProdukSemua.Where(item => ListIDTempat.Contains(item.TBTransaksi.IDTempat.Value)).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER JENIS TRANSAKSI
        if (ListIDJenisTransaksi.Count > 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
            string tempNamaJenisTransaksi = "Jenis : ";

            foreach (var item in ListIDJenisTransaksi)
            {
                tempNamaJenisTransaksi += ClassJenisTransaksi.Cari(db, item).Nama + ", ";
            }

            Result.Add("JenisTransaksi", tempNamaJenisTransaksi.Substring(0, tempNamaJenisTransaksi.Length - 2));

            ListProdukSemua = ListProdukSemua.Where(item => ListIDJenisTransaksi.Contains(item.TBTransaksi.IDJenisTransaksi.Value)).ToArray();
        }
        else
            Result.Add("JenisTransaksi", "Semua Jenis Transaksi");
        #endregion

        var DataResult = ListProdukSemua
            .GroupBy(item => new
            {
                item.TBTransaksi.IDJenisTransaksi,
                item.TBTransaksi.TBJenisTransaksi.Nama
            })
            .Select(item => new
            {
                JenisTransaksi = item.Key.Nama,
                Body = item.GroupBy(item2 => new
                {
                    item2.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                    item2.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Kategori = item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 1 ? item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama : item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama
                })
                .Select(item2 => new
                {
                    PemilikProduk = item2.Key.Nama,
                    Kategori = item2.Key.Kategori,
                    JumlahProduk = item2.Sum(item3 => item3.Quantity),
                    Gross = item2.Sum(item3 => item3.Quantity * item3.HargaJual),
                    Discount = item2.Sum(item3 => item3.Quantity * item3.Discount),
                    NetRevenue = item2.Sum(item3 => item3.Quantity * (item3.HargaJual - item3.Discount)),
                    COGS = item2.Sum(item3 => item3.Quantity * item3.HargaBeli),
                    GrossProfit = item2.Sum(item3 => item3.Quantity * (item3.HargaJual - item3.Discount - item3.HargaBeli))
                }).OrderBy(item2 => item2.PemilikProduk).ThenBy(item2 => item2.Kategori),
                TotalJumlahProduk = item.Sum(item2 => item2.Quantity),
                TotalGross = item.Sum(item2 => item2.Quantity * item2.HargaJual),
                TotalDiscount = item.Sum(item2 => item2.Quantity * item2.Discount),
                TotalNetRevenue = item.Sum(item2 => item2.Quantity * (item2.HargaJual - item2.Discount)),
                TotalCOGS = item.Sum(item2 => item2.Quantity * item2.HargaBeli),
                TotalGrossProfit = item.Sum(item2 => item2.Quantity * (item2.HargaJual - item2.Discount - item2.HargaBeli))
            });


        tempPencarian += "?TanggalAwal=" + _tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + _tanggalAkhir;
        tempPencarian += "&IDStatusTransaksi=" + string.Join(",", ListIDStatusTransaksi);
        tempPencarian += "&IDTempat=" + string.Join(",", ListIDTempat);
        tempPencarian += "&IDJenisTransaksi=" + string.Join(",", ListIDJenisTransaksi);

        Result.Add("Data", DataResult);
        Result.Add("GrandtotalJumlahProduk", DataResult.Sum(item => item.TotalJumlahProduk));
        Result.Add("GrandtotalGross", DataResult.Sum(item => item.TotalGross));
        Result.Add("GrandtotalDiscount", DataResult.Sum(item => item.TotalDiscount));
        Result.Add("GrandtotalNetRevenue", DataResult.Sum(item => item.TotalNetRevenue));
        Result.Add("GrandtotalCOGS", DataResult.Sum(item => item.TotalCOGS));
        Result.Add("GrandtotalGrossProfit", DataResult.Sum(item => item.TotalGrossProfit));

        Konfigurasi_Class Konfigurasi = new Konfigurasi_Class(pengguna.IDGrupPengguna);
        bool MelihatCOGS = Konfigurasi.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue);

        Result.Add("MelihatCOGS", MelihatCOGS);

        if (excel)
        {
            #region EXCEL
            int JumlahKolom = 7;

            if (MelihatCOGS)
            {
                JumlahKolom = 9;
            }

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Net Revenue Jenis Transaksi", Result["Periode"], JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Brand");
            Excel_Class.Header(3, "Kategori");
            Excel_Class.Header(4, "Quantity");
            Excel_Class.Header(5, "Gross");
            Excel_Class.Header(6, "Discount");
            Excel_Class.Header(7, "Net Revenue");

            if (MelihatCOGS)
            {
                Excel_Class.Header(8, "COGS");
                Excel_Class.Header(9, "Gross Profit");
            }

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, item.JenisTransaksi);
                Excel_Class.Worksheet.Cells[index, 1].Style.Font.Bold = true;
                for (int i = 1; i <= JumlahKolom; i++)
                {
                    Excel_Class.SetBackground(index, i, System.Drawing.Color.Gray);
                }
                int Nomor = 1;
                index++;
                foreach (var item2 in item.Body)
                {
                    Excel_Class.Content(index, 1, Nomor);
                    Excel_Class.Content(index, 2, item2.PemilikProduk);

                    Excel_Class.Content(index, 3, item2.Kategori);
                    Excel_Class.Content(index, 4, item2.JumlahProduk);
                    Excel_Class.Content(index, 5, item2.Gross);
                    Excel_Class.Content(index, 6, item2.Discount);
                    Excel_Class.Content(index, 7, item2.NetRevenue);

                    if (MelihatCOGS)
                    {
                        Excel_Class.Content(index, 8, item2.COGS);
                        Excel_Class.Content(index, 9, item2.GrossProfit);
                    }

                    index++;
                    Nomor++;
                }
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    public Dictionary<string, dynamic> NetRevenueJenisTransaksiCustomGEOFF(List<int> ListIDTempat, List<int> ListIDJenisTransaksi, List<int> ListIDStatusTransaksi, DateTime _tanggalAwal, DateTime _tanggalAkhir)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        if (_tanggalAwal == _tanggalAkhir)
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam());
        else
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam() + " - " + _tanggalAkhir.ToFormatTanggalJam());

        var ListProdukSemua = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value >= _tanggalAwal &&
                item.TBTransaksi.TBTransaksiJenisPembayarans.OrderBy(data => data.Tanggal).FirstOrDefault().Tanggal.Value <= _tanggalAkhir)
            .Select(item => new
            {
                item.TBTransaksi.IDTempat,
                item.TBTransaksi.IDStatusTransaksi,
                IDJenisTransaksi = (item.TBTransaksi.IDJenisTransaksi == 11 || item.TBTransaksi.IDJenisTransaksi == 10 || item.TBTransaksi.IDJenisTransaksi == 1 || item.TBTransaksi.IDJenisTransaksi == 2) ? 0 : item.TBTransaksi.IDJenisTransaksi,
                JenisTransaksi = (item.TBTransaksi.IDJenisTransaksi == 11 || item.TBTransaksi.IDJenisTransaksi == 10 || item.TBTransaksi.IDJenisTransaksi == 1 || item.TBTransaksi.IDJenisTransaksi == 2) ? "Retail" : item.TBTransaksi.TBJenisTransaksi.Nama,
                item.TBKombinasiProduk,
                Quantity = item.Quantity,
                item.HargaBeli,
                item.HargaJual,
                item.Discount
            }).ToArray();

        #region FILTER STATUS TRANSAKSI
        if (ListIDStatusTransaksi.Count > 0)
        {
            StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
            string tempStatusTranskasi = "Status : ";

            foreach (var item in ListIDStatusTransaksi)
            {
                tempStatusTranskasi += StatusTransaksi_Class.Cari(db, item).Nama + ", ";
            }

            Result.Add("StatusTransaksi", tempStatusTranskasi.Substring(0, tempStatusTranskasi.Length - 2));

            ListProdukSemua = ListProdukSemua.Where(item => ListIDStatusTransaksi.Contains(item.IDStatusTransaksi.Value)).ToArray();
        }
        else
            Result.Add("StatusTransaksi", "Semua Status Transaksi");
        #endregion

        #region FILTER TEMPAT
        if (ListIDTempat.Count > 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            string tempTempat = "Tempat : ";

            foreach (var item in ListIDTempat)
            {
                tempTempat += ClassTempat.Cari(item).Nama + ", ";
            }

            Result.Add("Tempat", tempTempat.Substring(0, tempTempat.Length - 2));

            ListProdukSemua = ListProdukSemua.Where(item => ListIDTempat.Contains(item.IDTempat.Value)).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER JENIS TRANSAKSI
        if (ListIDJenisTransaksi.Count > 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
            string tempNamaJenisTransaksi = "Jenis : ";

            if (ListIDJenisTransaksi.Where(item => item == 11 || item == 10 || item == 1 || item == 2).Count() > 0)
            {
                ListIDJenisTransaksi.RemoveAll(item => item == 11 || item == 10 || item == 1 || item == 2);
                ListIDJenisTransaksi.Add(0);
            }

            foreach (var item in ListIDJenisTransaksi)
            {
                if (item == 0)
                {
                    tempNamaJenisTransaksi += "Retail" + ", ";
                }
                else
                    tempNamaJenisTransaksi += ClassJenisTransaksi.Cari(db, item).Nama + ", ";

            }

            Result.Add("JenisTransaksi", tempNamaJenisTransaksi.Substring(0, tempNamaJenisTransaksi.Length - 2));

            ListProdukSemua = ListProdukSemua.Where(item => ListIDJenisTransaksi.Contains(item.IDJenisTransaksi.Value)).ToArray();
        }
        else
            Result.Add("JenisTransaksi", "Semua Jenis Transaksi");
        #endregion

        var DataResult = ListProdukSemua
            .GroupBy(item => new
            {
                item.IDJenisTransaksi,
                item.JenisTransaksi
            })
            .Select(item => new
            {
                IDJenisTransaksi = item.Key.IDJenisTransaksi,
                JenisTransaksi = item.Key.JenisTransaksi,
                Body = item.GroupBy(item2 => new
                {
                    item2.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                    item2.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Kategori = item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 1 ? item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama : item2.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama
                })
                .Select(item2 => new
                {
                    PemilikProduk = item2.Key.Nama,
                    Kategori = item2.Key.Kategori,
                    JumlahProduk = item2.Sum(item3 => item3.Quantity),
                    Gross = item2.Sum(item3 => item3.Quantity * item3.HargaJual),
                    Discount = item2.Sum(item3 => item3.Quantity * item3.Discount),
                    NetRevenue = item2.Sum(item3 => item3.Quantity * (item3.HargaJual - item3.Discount)),
                    COGS = item2.Sum(item3 => item3.Quantity * item3.HargaBeli),
                    GrossProfit = item2.Sum(item3 => item3.Quantity * (item3.HargaJual - item3.Discount - item3.HargaBeli))
                }).OrderBy(item2 => item2.PemilikProduk).ThenBy(item2 => item2.Kategori),
                TotalJumlahProduk = item.Sum(item2 => item2.Quantity),
                TotalGross = item.Sum(item2 => item2.Quantity * item2.HargaJual),
                TotalDiscount = item.Sum(item2 => item2.Quantity * item2.Discount),
                TotalNetRevenue = item.Sum(item2 => item2.Quantity * (item2.HargaJual - item2.Discount)),
                TotalCOGS = item.Sum(item2 => item2.Quantity * item2.HargaBeli),
                TotalGrossProfit = item.Sum(item2 => item2.Quantity * (item2.HargaJual - item2.Discount - item2.HargaBeli))
            });

        tempPencarian += "?TanggalAwal=" + _tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + _tanggalAkhir;
        tempPencarian += "&IDStatusTransaksi=" + string.Join(",", ListIDStatusTransaksi);
        tempPencarian += "&IDTempat=" + string.Join(",", ListIDTempat);
        tempPencarian += "&IDJenisTransaksi=" + string.Join(",", ListIDJenisTransaksi);

        Result.Add("Data", DataResult);
        Result.Add("GrandtotalJumlahProduk", DataResult.Sum(item => item.TotalJumlahProduk));
        Result.Add("GrandtotalGross", DataResult.Sum(item => item.TotalGross));
        Result.Add("GrandtotalDiscount", DataResult.Sum(item => item.TotalDiscount));
        Result.Add("GrandtotalNetRevenue", DataResult.Sum(item => item.TotalNetRevenue));
        Result.Add("GrandtotalCOGS", DataResult.Sum(item => item.TotalCOGS));
        Result.Add("GrandtotalGrossProfit", DataResult.Sum(item => item.TotalGrossProfit));

        Konfigurasi_Class Konfigurasi = new Konfigurasi_Class(pengguna.IDGrupPengguna);
        bool MelihatCOGS = Konfigurasi.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue);

        Result.Add("MelihatCOGS", MelihatCOGS);

        if (excel)
        {
            #region EXCEL
            int JumlahKolom = 7;

            if (MelihatCOGS)
            {
                JumlahKolom = 9;
            }

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Net Revenue Jenis Transaksi", Result["Periode"], JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Brand");
            Excel_Class.Header(3, "Kategori");
            Excel_Class.Header(4, "Quantity");
            Excel_Class.Header(5, "Gross");
            Excel_Class.Header(6, "Discount");
            Excel_Class.Header(7, "Net Revenue");

            if (MelihatCOGS)
            {
                Excel_Class.Header(8, "COGS");
                Excel_Class.Header(9, "Gross Profit");
            }

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, item.JenisTransaksi);
                Excel_Class.Worksheet.Cells[index, 1].Style.Font.Bold = true;
                for (int i = 1; i <= JumlahKolom; i++)
                {
                    Excel_Class.SetBackground(index, i, System.Drawing.Color.Gray);
                }
                int Nomor = 1;
                index++;
                foreach (var item2 in item.Body)
                {
                    Excel_Class.Content(index, 1, Nomor);
                    Excel_Class.Content(index, 2, item2.PemilikProduk);

                    Excel_Class.Content(index, 3, item2.Kategori);
                    Excel_Class.Content(index, 4, item2.JumlahProduk);
                    Excel_Class.Content(index, 5, item2.Gross);
                    Excel_Class.Content(index, 6, item2.Discount);
                    Excel_Class.Content(index, 7, item2.NetRevenue);

                    if (MelihatCOGS)
                    {
                        Excel_Class.Content(index, 8, item2.COGS);
                        Excel_Class.Content(index, 9, item2.GrossProfit);
                    }

                    index++;
                    Nomor++;
                }
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }

    public Dictionary<string, dynamic> NetRevenuePembayaran(List<int> ListIDTempat, List<int> ListIDJenisTransaksi, List<int> ListIDStatusTransaksi, List<int> ListIDJenisPembayaran, DateTime _tanggalAwal, DateTime _tanggalAkhir)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        if (_tanggalAwal == _tanggalAkhir)
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam());
        else
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam() + " - " + _tanggalAkhir.ToFormatTanggalJam());

        var ListPembayaran = db.TBTransaksiJenisPembayarans
            .Where(item =>
                item.Tanggal.Value >= _tanggalAwal &&
                item.Tanggal.Value <= _tanggalAkhir)
            .ToArray();

        #region FILTER STATUS TRANSAKSI
        if (ListIDStatusTransaksi.Count > 0)
        {
            StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
            string tempStatusTranskasi = "Status : ";

            foreach (var item in ListIDStatusTransaksi)
            {
                tempStatusTranskasi += StatusTransaksi_Class.Cari(db, item).Nama + ", ";
            }

            Result.Add("StatusTransaksi", tempStatusTranskasi.Substring(0, tempStatusTranskasi.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDStatusTransaksi.Contains(item.TBTransaksi.IDStatusTransaksi.Value)).ToArray();
        }
        else
            Result.Add("StatusTransaksi", "Semua Status Transaksi");
        #endregion

        #region FILTER TEMPAT
        if (ListIDTempat.Count > 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            string tempTempat = "Tempat : ";

            foreach (var item in ListIDTempat)
            {
                tempTempat += ClassTempat.Cari(item).Nama + ", ";
            }

            Result.Add("Tempat", tempTempat.Substring(0, tempTempat.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDTempat.Contains(item.TBTransaksi.IDTempat.Value)).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER JENIS TRANSAKSI
        if (ListIDJenisTransaksi.Count > 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
            string tempNamaJenisTransaksi = "Jenis : ";

            foreach (var item in ListIDJenisTransaksi)
            {
                tempNamaJenisTransaksi += ClassJenisTransaksi.Cari(db, item).Nama + ", ";
            }

            Result.Add("JenisTransaksi", tempNamaJenisTransaksi.Substring(0, tempNamaJenisTransaksi.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDJenisTransaksi.Contains(item.TBTransaksi.IDJenisTransaksi.Value)).ToArray();
        }
        else
            Result.Add("JenisTransaksi", "Semua Jenis Transaksi");
        #endregion

        #region FILTER JENIS PEMBAYARAN
        if (ListIDJenisPembayaran.Count > 0)
        {
            JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);
            string tempNamaJenisPembayaran = "Pembayaran : ";

            foreach (var item in ListIDJenisPembayaran)
            {
                tempNamaJenisPembayaran += ClassJenisPembayaran.Cari(item).Nama + ", ";
            }

            Result.Add("JenisPembayaran", tempNamaJenisPembayaran.Substring(0, tempNamaJenisPembayaran.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDJenisPembayaran.Contains(item.IDJenisPembayaran)).ToArray();
        }
        else
            Result.Add("JenisPembayaran", "Semua Jenis Pembayaran");
        #endregion

        var DataResult = ListPembayaran
            .Select(item => new
            {
                item.TBTransaksi.IDTransaksi,
                item.TBTransaksi.TanggalTransaksi,
                JenisTransaksi = item.TBTransaksi.TBJenisTransaksi.Nama,
                StatusTransaksi = item.TBTransaksi.TBStatusTransaksi.Nama,
                TanggalPembayaran = item.Tanggal,
                JenisPembayaran = item.TBJenisPembayaran.Nama,
                item.TBTransaksi.GrandTotal,
                Pembayaran = item.Total,
                TotalPembayaran = item.TBTransaksi.TBTransaksiJenisPembayarans.Sum(item2 => item2.Total),
            })
            .OrderBy(item => item.TanggalPembayaran);

        tempPencarian += "?TanggalAwal=" + _tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + _tanggalAkhir;
        tempPencarian += "&IDStatusTransaksi=" + string.Join(",", ListIDStatusTransaksi);
        tempPencarian += "&IDTempat=" + string.Join(",", ListIDTempat);
        tempPencarian += "&IDJenisTransaksi=" + string.Join(",", ListIDJenisTransaksi);
        tempPencarian += "&IDJenisPembayaran=" + string.Join(",", ListIDJenisPembayaran);

        Result.Add("Data", DataResult);
        Result.Add("TotalPembayaran", DataResult.Sum(item => item.Pembayaran));

        //if (excel)
        //{
        //    #region EXCEL
        //    int JumlahKolom = 0;
        //    int Batas = 0;

        //    if (MelihatCOGS)
        //    {
        //        JumlahKolom = 19;
        //        Batas = 10;
        //    }
        //    else
        //    {
        //        JumlahKolom = 18;
        //        Batas = 9;
        //    }

        //    Excel_Class Excel_Class = new Excel_Class(pengguna, "Net Revenue", Result["Periode"], JumlahKolom);
        //    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

        //    Excel_Class.Header(1, "No.");
        //    Excel_Class.Header(2, "ID Transaksi");
        //    Excel_Class.Header(3, "Produk");
        //    Excel_Class.Header(4, "Price");
        //    Excel_Class.Header(5, "Discount");
        //    Excel_Class.Header(6, "Biaya Pengiriman");
        //    Excel_Class.Header(7, "Grand Total");
        //    Excel_Class.Header(8, "Net Revenue");

        //    if (MelihatCOGS)
        //        Excel_Class.Header(9, "COGS");

        //    Excel_Class.SetBackground(1, Batas, System.Drawing.Color.Black);

        //    int index = 2;

        //    foreach (var item in DataResult)
        //    {
        //        Excel_Class.Content(index, 1, index - 1);
        //        Excel_Class.Content(index, 2, item.IDTransaksi);

        //        Excel_Class.Content(index, 3, item.Produk.Nama);
        //        Excel_Class.Content(index, 4, item.Produk.BeforeDiscount);
        //        Excel_Class.Content(index, 5, item.Produk.Discount);

        //        Excel_Class.Content(index, 6, item.BiayaPengiriman.Value);
        //        Excel_Class.Content(index, 7, item.GrandTotal.Value);
        //        Excel_Class.Content(index, 8, item.NetRevenue.Value);

        //        if (MelihatCOGS)
        //            Excel_Class.Content(index, 9, item.TotalHargaBeli);

        //        Excel_Class.SetBackground(index, Batas, System.Drawing.Color.Black);

        //        index++;

        //        foreach (var item2 in item.Detail)
        //        {
        //            Excel_Class.Content(index, 3, item2.Nama);
        //            Excel_Class.Content(index, 4, item2.BeforeDiscount);
        //            Excel_Class.Content(index, 5, item2.Discount);

        //            Excel_Class.SetBackground(index, Batas, System.Drawing.Color.Black);

        //            index++;
        //        }
        //    }

        //    index = 2;

        //    Excel_Class.Header(Batas + 1, "No.");
        //    Excel_Class.Header(Batas + 2, "ID Transaksi");
        //    Excel_Class.Header(Batas + 3, "Keterangan");
        //    Excel_Class.Header(Batas + 4, "Produk");
        //    Excel_Class.Header(Batas + 5, "Price");
        //    Excel_Class.Header(Batas + 6, "Discount");
        //    Excel_Class.Header(Batas + 7, "Qty");
        //    Excel_Class.Header(Batas + 8, "Net Revenue");

        //    if (MelihatCOGS)
        //        Excel_Class.Header(19, "COGS");

        //    foreach (var item in DataResultRetur)
        //    {
        //        Excel_Class.Content(index, Batas + 1, index - 1);
        //        Excel_Class.Content(index, Batas + 2, item.IDTransaksi);
        //        Excel_Class.Content(index, Batas + 3, item.Keterangan);
        //        Excel_Class.Content(index, Batas + 4, item.Produk);
        //        Excel_Class.Content(index, Batas + 5, item.HargaJual);
        //        Excel_Class.Content(index, Batas + 6, item.Discount);
        //        Excel_Class.Content(index, Batas + 7, item.Quantity);
        //        Excel_Class.Content(index, Batas + 8, item.NetRevenue);

        //        if (MelihatCOGS)
        //            Excel_Class.Content(index, 19, item.HargaBeli);

        //        index++;
        //    }

        //    Excel_Class.Save();

        //    linkDownload = Excel_Class.LinkDownload;
        //    #endregion
        //}

        return Result;
    }
    #region ARIE
    public Dictionary<string, dynamic> TransaksiPrintLog(List<int> ListIDTempat, List<int> ListIDJenisTransaksi, List<int> ListIDStatusTransaksi, DateTime _tanggalAwal, DateTime _tanggalAkhir)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        if (_tanggalAwal == _tanggalAkhir)
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam());
        else
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam() + " - " + _tanggalAkhir.ToFormatTanggalJam());

        var ListTransaksiPrintLog = db.TBTransaksis
                       .Where(item =>
                           item.TanggalUpdate.Value >= _tanggalAwal &&
                           item.TanggalUpdate.Value <= _tanggalAkhir).ToArray();

        #region FILTER STATUS TRANSAKSI
        if (ListIDStatusTransaksi.Count > 0)
        {
            StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
            string tempStatusTranskasi = "Status : ";

            foreach (var item in ListIDStatusTransaksi)
            {
                tempStatusTranskasi += StatusTransaksi_Class.Cari(db, item).Nama + ", ";
            }

            Result.Add("StatusTransaksi", tempStatusTranskasi.Substring(0, tempStatusTranskasi.Length - 2));

            ListTransaksiPrintLog = ListTransaksiPrintLog.Where(item => ListIDStatusTransaksi.Contains(item.IDStatusTransaksi.Value)).ToArray();
        }
        else
            Result.Add("StatusTransaksi", "Semua Status Transaksi");
        #endregion

        #region FILTER TEMPAT
        if (ListIDTempat.Count > 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            string tempTempat = "Tempat : ";

            foreach (var item in ListIDTempat)
            {
                tempTempat += ClassTempat.Cari(item).Nama + ", ";
            }

            Result.Add("Tempat", tempTempat.Substring(0, tempTempat.Length - 2));

            ListTransaksiPrintLog = ListTransaksiPrintLog.Where(item => ListIDTempat.Contains(item.IDTempat.Value)).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER JENIS TRANSAKSI
        if (ListIDJenisTransaksi.Count > 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
            string tempNamaJenisTransaksi = "Jenis : ";

            foreach (var item in ListIDJenisTransaksi)
            {
                tempNamaJenisTransaksi += ClassJenisTransaksi.Cari(db, item).Nama + ", ";
            }

            Result.Add("JenisTransaksi", tempNamaJenisTransaksi.Substring(0, tempNamaJenisTransaksi.Length - 2));

            ListTransaksiPrintLog = ListTransaksiPrintLog.Where(item => ListIDJenisTransaksi.Contains(item.IDJenisTransaksi.Value)).ToArray();
        }
        else
            Result.Add("JenisTransaksi", "Semua Jenis Transaksi");
        #endregion

        var DataResult = ListTransaksiPrintLog
            .Select(item => new
            {
                item.IDTransaksi,
                item.TanggalTransaksi,
                JenisTransaksi = item.TBJenisTransaksi.Nama,
                StatusTransaksi = item.TBStatusTransaksi.Nama,
                TanggalPembayaran = item.TanggalPembayaran,
                item.GrandTotal,
                Pelanggan = item.TBPelanggan.NamaLengkap,
                Telepon = item.TBPelanggan.Handphone,
                Alamat = item.TBPelanggan.TBAlamats.Count != 0 ? item.TBPelanggan.TBAlamats.FirstOrDefault().AlamatLengkap : " - ",
                Keterangan = item.TBTransaksiJenisPembayarans.FirstOrDefault() != null ? item.TBTransaksiJenisPembayarans.FirstOrDefault().Keterangan : "",
                TotalPembayaran = item.TBTransaksiJenisPembayarans.Sum(item2 => item2.Total),
                BiayaShipping = item.BiayaPengiriman,
                PenggunaTransaksi = item.TBPengguna.NamaLengkap,
                LogPrint = item.TBTransaksiPrintLogs
            })
            .OrderBy(item => item.LogPrint.Count()).
            ThenBy(item => item.TanggalPembayaran);

        tempPencarian += "?TanggalAwal=" + _tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + _tanggalAkhir;
        tempPencarian += "&IDStatusTransaksi=" + string.Join(",", ListIDStatusTransaksi);
        tempPencarian += "&IDTempat=" + string.Join(",", ListIDTempat);
        tempPencarian += "&IDJenisTransaksi=" + string.Join(",", ListIDJenisTransaksi);

        Result.Add("Data", DataResult);
        Result.Add("TotalBiayaShipping", DataResult.Sum(item => item.BiayaShipping));
        Result.Add("TotalGrandTotal", DataResult.Sum(item => item.GrandTotal));
        Result.Add("TotalPembayaran", DataResult.Sum(item => item.TotalPembayaran));

        if (excel)
        {
            #region EXCEL
            int JumlahKolom = 12;

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Laporan Pembayaran", Result["Periode"], JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Transaksi");
            Excel_Class.Header(3, "Tanggal Transaksi");
            Excel_Class.Header(4, "Jenis");
            Excel_Class.Header(5, "Pelanggan");
            Excel_Class.Header(6, "Alamat");
            Excel_Class.Header(7, "Status");
            Excel_Class.Header(8, "Tanggal Pembayaran");
            Excel_Class.Header(9, "BiayaShipping");
            Excel_Class.Header(10, "Pembayaran");
            Excel_Class.Header(11, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransaksi);

                Excel_Class.Content(index, 3, item.TanggalTransaksi.Value);
                Excel_Class.Content(index, 4, item.JenisTransaksi);
                Excel_Class.Content(index, 5, item.Pelanggan);
                Excel_Class.Content(index, 6, item.Alamat + " / " + item.Telepon);
                Excel_Class.Content(index, 7, item.StatusTransaksi);
                Excel_Class.Content(index, 8, item.TanggalPembayaran.Value);
                Excel_Class.Content(index, 9, item.BiayaShipping.Value);
                Excel_Class.Content(index, 10, item.TotalPembayaran.Value);
                Excel_Class.Content(index, 11, item.Keterangan);

                index++;
            }

            index = 2;

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    public Dictionary<string, dynamic> NetRevenuePembayaranDressSofia(List<int> ListIDTempat, List<int> ListIDJenisTransaksi, List<int> ListIDStatusTransaksi, List<int> ListIDJenisPembayaran, DateTime _tanggalAwal, DateTime _tanggalAkhir, string triggerFilterTanggal)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        if (_tanggalAwal == _tanggalAkhir)
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam());
        else
            Result.Add("Periode", _tanggalAwal.ToFormatTanggalJam() + " - " + _tanggalAkhir.ToFormatTanggalJam());

        var ListPembayaran = db.TBTransaksiJenisPembayarans
                       .Where(item =>
                           item.Tanggal.Value >= _tanggalAwal &&
                           item.Tanggal.Value <= _tanggalAkhir)
                       .OrderBy(item => item.TBTransaksi.TanggalTransaksi).ToArray();

        if (triggerFilterTanggal == "5")
        {
            ListPembayaran = db.TBTransaksiJenisPembayarans
                         .Where(item =>
                             item.TBTransaksi.TanggalUpdate.Value >= _tanggalAwal &&
                             item.TBTransaksi.TanggalUpdate.Value <= _tanggalAkhir)
                         .OrderBy(item => item.TBTransaksi.TanggalTransaksi).ToArray();
        }


        #region FILTER STATUS TRANSAKSI
        if (ListIDStatusTransaksi.Count > 0)
        {
            StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
            string tempStatusTranskasi = "Status : ";

            foreach (var item in ListIDStatusTransaksi)
            {
                tempStatusTranskasi += StatusTransaksi_Class.Cari(db, item).Nama + ", ";
            }

            Result.Add("StatusTransaksi", tempStatusTranskasi.Substring(0, tempStatusTranskasi.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDStatusTransaksi.Contains(item.TBTransaksi.IDStatusTransaksi.Value)).ToArray();
        }
        else
            Result.Add("StatusTransaksi", "Semua Status Transaksi");
        #endregion

        #region FILTER TEMPAT
        if (ListIDTempat.Count > 0)
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);
            string tempTempat = "Tempat : ";

            foreach (var item in ListIDTempat)
            {
                tempTempat += ClassTempat.Cari(item).Nama + ", ";
            }

            Result.Add("Tempat", tempTempat.Substring(0, tempTempat.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDTempat.Contains(item.TBTransaksi.IDTempat.Value)).ToArray();
        }
        else
            Result.Add("Tempat", "Semua Tempat");
        #endregion

        #region FILTER JENIS TRANSAKSI
        if (ListIDJenisTransaksi.Count > 0)
        {
            JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
            string tempNamaJenisTransaksi = "Jenis : ";

            foreach (var item in ListIDJenisTransaksi)
            {
                tempNamaJenisTransaksi += ClassJenisTransaksi.Cari(db, item).Nama + ", ";
            }

            Result.Add("JenisTransaksi", tempNamaJenisTransaksi.Substring(0, tempNamaJenisTransaksi.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDJenisTransaksi.Contains(item.TBTransaksi.IDJenisTransaksi.Value)).ToArray();
        }
        else
            Result.Add("JenisTransaksi", "Semua Jenis Transaksi");
        #endregion

        #region FILTER JENIS PEMBAYARAN
        if (ListIDJenisPembayaran.Count > 0)
        {
            JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);
            string tempNamaJenisPembayaran = "Pembayaran : ";

            foreach (var item in ListIDJenisPembayaran)
            {
                tempNamaJenisPembayaran += ClassJenisPembayaran.Cari(item).Nama + ", ";
            }

            Result.Add("JenisPembayaran", tempNamaJenisPembayaran.Substring(0, tempNamaJenisPembayaran.Length - 2));

            ListPembayaran = ListPembayaran.Where(item => ListIDJenisPembayaran.Contains(item.IDJenisPembayaran)).ToArray();
        }
        else
            Result.Add("JenisPembayaran", "Semua Jenis Pembayaran");
        #endregion

        var DataResult = ListPembayaran
            .Select(item => new
            {
                item.TBTransaksi.IDTransaksi,
                item.TBTransaksi.TanggalTransaksi,
                JenisTransaksi = item.TBTransaksi.TBJenisTransaksi.Nama,
                StatusTransaksi = item.TBTransaksi.TBStatusTransaksi.Nama,
                TanggalPembayaran = item.Tanggal,
                JenisPembayaran = item.TBJenisPembayaran.Nama,
                item.TBTransaksi.GrandTotal,
                Pembayaran = item.Total,
                Pelanggan = item.TBTransaksi.TBPelanggan.NamaLengkap,
                Telepon = item.TBTransaksi.TBPelanggan.Handphone,
                Alamat = item.TBTransaksi.TBPelanggan.TBAlamats.Count != 0 ? item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().AlamatLengkap : " - ",
                Keterangan = item.TBTransaksi.TBTransaksiJenisPembayarans.FirstOrDefault() != null ? item.TBTransaksi.TBTransaksiJenisPembayarans.FirstOrDefault().Keterangan : "",
                TotalPembayaran = item.TBTransaksi.TBTransaksiJenisPembayarans.Sum(item2 => item2.Total),
                BiayaShipping = item.TBTransaksi.BiayaPengiriman,
                PenggunaTransaksi = item.TBTransaksi.TBPengguna.NamaLengkap
            })
            .OrderBy(item => item.TanggalPembayaran);

        tempPencarian += "?TanggalAwal=" + _tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + _tanggalAkhir;
        tempPencarian += "&IDStatusTransaksi=" + string.Join(",", ListIDStatusTransaksi);
        tempPencarian += "&IDTempat=" + string.Join(",", ListIDTempat);
        tempPencarian += "&IDJenisTransaksi=" + string.Join(",", ListIDJenisTransaksi);
        tempPencarian += "&IDJenisPembayaran=" + string.Join(",", ListIDJenisPembayaran);
        tempPencarian += "&triggerFilterTanggal=" + string.Join(",", triggerFilterTanggal);

        Result.Add("Data", DataResult);
        Result.Add("TotalBiayaShipping", DataResult.Sum(item => item.BiayaShipping));
        Result.Add("TotalPembayaran", DataResult.Sum(item => item.Pembayaran));

        if (excel)
        {
            #region EXCEL
            int JumlahKolom = 12;

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Laporan Pembayaran", Result["Periode"], JumlahKolom);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Transaksi");
            Excel_Class.Header(3, "Tanggal Transaksi");
            Excel_Class.Header(4, "Jenis");
            Excel_Class.Header(5, "Pelanggan");
            Excel_Class.Header(6, "Alamat");
            Excel_Class.Header(7, "Status");
            Excel_Class.Header(8, "Tanggal Pembayaran");
            Excel_Class.Header(9, "Jenis Pembayaran");
            Excel_Class.Header(10, "BiayaShipping");
            Excel_Class.Header(11, "Pembayaran");
            Excel_Class.Header(12, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransaksi);

                Excel_Class.Content(index, 3, item.TanggalTransaksi.Value);
                Excel_Class.Content(index, 4, item.JenisTransaksi);
                Excel_Class.Content(index, 5, item.Pelanggan);
                Excel_Class.Content(index, 6, item.Alamat + " / " + item.Telepon);
                Excel_Class.Content(index, 7, item.StatusTransaksi);
                Excel_Class.Content(index, 8, item.TanggalPembayaran.Value);
                Excel_Class.Content(index, 9, item.JenisPembayaran);
                Excel_Class.Content(index, 10, item.BiayaShipping.Value);
                Excel_Class.Content(index, 11, item.Pembayaran.Value);
                Excel_Class.Content(index, 12, item.Keterangan);

                index++;
            }

            index = 2;

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }
    #endregion
    public Dictionary<string, dynamic> StokProduk_Class(string action, int idTempat, int jenisStokProduk, string produk, int idWarna, int idPemilikProduk, int idKategori, string kode, int idAtributProduk, string harga, string quantity)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        tempPencarian += "?IDTempat=" + idTempat;
        tempPencarian += "&JenisStokProduk=" + jenisStokProduk;
        tempPencarian += "&Produk=" + produk;
        tempPencarian += "&IDWarna=" + idWarna;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;
        tempPencarian += "&IDKategori=" + idKategori;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&Harga=" + harga;
        tempPencarian += "&Quantity=" + quantity;

        if (action == "opname")
            Result.Add("Judul", "Stock Opname");
        else if (action == "waste")
            Result.Add("Judul", "Pembuangan Produk Rusak");
        else if (action == "restock")
            Result.Add("Judul", "Restock Produk");
        else if (action == "return")
            Result.Add("Judul", "Retur ke Tempat Produksi");
        else
            Result.Add("Judul", "");

        tempPencarian += "&do=" + action;

        var ListStokProduk = db
            .TBStokProduks
            .Where(item => 
                item.TBKombinasiProduk.TBProduk._IsActive && 
                item.IDTempat == idTempat &&
                (!string.IsNullOrWhiteSpace(produk) ? item.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(produk.ToLower()) : true) &&
                (idWarna != 0 ? item.TBKombinasiProduk.TBProduk.IDWarna == idWarna : true) &&
                (idPemilikProduk != 0 ? item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk : true) &&
                (idKategori != 0 ? (item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(item2 => item2.IDKategoriProduk == idKategori) != null) : true) &&
                (!string.IsNullOrWhiteSpace(kode) ? item.TBKombinasiProduk.KodeKombinasiProduk.ToLower().Contains(kode.ToLower()) : true) &&
                (idAtributProduk != 0 ? item.TBKombinasiProduk.IDAtributProduk == idAtributProduk : true) &&
                (jenisStokProduk == 1 ? item.Jumlah > 0 : (jenisStokProduk == 2 ? item.Jumlah == 0 : (jenisStokProduk == 2 ? item.Jumlah < 0 : true))))
            .GroupBy(item => item.TBKombinasiProduk.TBProduk)
            .Select(item => new
            {
                Produk = item.Key.Nama,
                Warna = item.Key.TBWarna.Nama,
                Brand = item.Key.TBPemilikProduk.Nama,
                Kategori = item.Key.TBRelasiProdukKategoriProduks.Count() > 0 ? item.Key.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                Stok = item.Select(item2 => new
                    {
                        Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                        IDStokProduk = item2.IDStokProduk,
                        Varian = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                        HargaJual = item2.HargaJual,
                        Jumlah = item2.Jumlah,
                    }),
                TotalQuantity = item.Sum(item2 => item2.Jumlah),
                Count = item.Count()
            }).OrderBy(item => item.Produk);

        Tempat_Class ClassTempat = new Tempat_Class(db);

        Result.Add("Data", ListStokProduk);
        Result.Add("Tempat", ClassTempat.Cari(idTempat).Nama);
        Result.Add("TotalQuantity", ListStokProduk.Sum(item => item.TotalQuantity));

        return Result;
    }
    public Dictionary<string, dynamic> PersediaanStokProduk(int idTempat, int jenisStokProduk, string produk, int idWarna, int idPemilikProduk, int idKategori, string kode, int idAtributProduk, string harga, string cogs, string quantity, string totalHarga, string totalCOGS)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var ListStokProduk = db.TBStokProduks.Where(item => item.TBKombinasiProduk.TBProduk._IsActive && item.IDTempat == idTempat).ToArray();

        switch (jenisStokProduk)
        {
            case 1: ListStokProduk = ListStokProduk.Where(item => item.Jumlah > 0).ToArray(); break;
            case 2: ListStokProduk = ListStokProduk.Where(item => item.Jumlah == 0).ToArray(); break;
            case 3: ListStokProduk = ListStokProduk.Where(item => item.Jumlah < 0).ToArray(); break;
        }

        tempPencarian += "?IDTempat=" + idTempat;
        tempPencarian += "&JenisStokProduk=" + jenisStokProduk;

        if (!string.IsNullOrWhiteSpace(produk))
            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(produk.ToLower())).ToArray();

        tempPencarian += "&Produk=" + produk;

        if (idWarna != 0)
            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDWarna == idWarna).ToArray();

        tempPencarian += "&IDWarna=" + idWarna;

        if (idPemilikProduk != 0)
            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();

        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;

        if (idKategori != 0)
            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 && item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == idKategori).ToArray();

        tempPencarian += "&IDKategori=" + idKategori;

        if (!string.IsNullOrWhiteSpace(kode))
            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.ToLower().Contains(kode.ToLower())).ToArray();

        tempPencarian += "&Kode=" + kode;

        if (idAtributProduk != 0)
            ListStokProduk = ListStokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        tempPencarian += "&IDAtributProduk=" + idAtributProduk;

        if (!string.IsNullOrWhiteSpace(harga))
        {
            if (harga.Contains("-"))
            {
                string[] Range = harga.Split('-');
                ListStokProduk = ListStokProduk.Where(item => item.HargaJual >= Range[0].ToDecimal() && item.HargaJual <= Range[1].ToDecimal()).OrderBy(item => item.HargaJual).ToArray();
            }
            else
                ListStokProduk = ListStokProduk.Where(item => item.HargaJual == harga.ToDecimal()).ToArray();
        }

        tempPencarian += "&Harga=" + harga;

        if (!string.IsNullOrWhiteSpace(cogs))
        {
            if (cogs.Contains("-"))
            {
                string[] Range = cogs.Split('-');
                ListStokProduk = ListStokProduk
                    .Where(item =>
                        item.HargaBeli >= Range[0].ToDecimal() &&
                        item.HargaBeli <= Range[1].ToDecimal())
                    .OrderBy(item => item.HargaBeli)
                    .ToArray();
            }
            else
                ListStokProduk = ListStokProduk
                    .Where(item =>
                        item.HargaBeli == cogs.ToDecimal())
                    .ToArray();
        }

        tempPencarian += "&COGS=" + cogs;

        #region QUANTITY
        if (!string.IsNullOrWhiteSpace(quantity))
        {
            if (quantity.Contains("-"))
            {
                string[] Range = quantity.Split('-');
                ListStokProduk = ListStokProduk
                    .Where(item =>
                        item.Jumlah >= Range[0].ToDecimal() &&
                        item.Jumlah <= Range[1].ToDecimal())
                    .OrderBy(item => item.Jumlah)
                    .ToArray();
            }
            else
                ListStokProduk = ListStokProduk
                    .Where(item =>
                        item.Jumlah == quantity.ToDecimal())
                    .ToArray();
        }

        tempPencarian += "&Quantity=" + quantity;
        #endregion

        var ResultStokProduk = ListStokProduk.Select(item => new
        {
            Produk = new
            {
                item.TBKombinasiProduk.IDProduk,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count() > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
            },
            item.TBKombinasiProduk.IDAtributProduk,
            item.IDKombinasiProduk,
            item.TBKombinasiProduk.IDProduk,
            Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
            Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
            item.HargaJual,
            item.HargaBeli,
            TotalHargaJual = item.HargaJual * item.Jumlah,
            TotalHargaBeli = item.HargaBeli * item.Jumlah,
            item.Jumlah
        });

        //#region TOTAL HARGA
        //if (!string.IsNullOrWhiteSpace(totalHarga))
        //{
        //    if (totalHarga.Contains("-"))
        //    {
        //        string[] Range = totalHarga.Split('-');
        //        ResultStokProduk = ResultStokProduk
        //            .Where(item =>
        //                item.TotalHargaJual >= Range[0].ToDecimal() &&
        //                item.TotalHargaJual <= Range[1].ToDecimal())
        //            .OrderBy(item => item.TotalHargaJual)
        //            .ToArray();
        //    }
        //    else
        //        ResultStokProduk = ResultStokProduk
        //            .Where(item =>
        //                item.TotalHargaJual == totalHarga.ToDecimal())
        //            .ToArray();
        //}

        //tempPencarian += "&TotalHarga=" + quantity;
        //#endregion

        var ListProduk = ResultStokProduk.Select(item => item.Produk).Distinct();

        var ResultProduk = ListProduk.Select(item => new
        {
            item.Produk,
            item.Brand,
            item.Kategori,
            item.Warna,
            Stok = ResultStokProduk.Where(item2 => item2.IDProduk == item.IDProduk).OrderBy(item2 => item2.IDAtributProduk),
            JumlahStok = ResultStokProduk.Count(item2 => item2.IDProduk == item.IDProduk)
        }).OrderBy(item => item.Produk);

        Tempat_Class ClassTempat = new Tempat_Class(db);

        Result.Add("Data", ResultProduk);
        Result.Add("Tempat", ClassTempat.Cari(idTempat).Nama);
        Result.Add("TotalQuantity", ResultStokProduk.Sum(item => item.Jumlah));
        Result.Add("TotalHargaBeli", ResultStokProduk.Sum(item => item.TotalHargaBeli));
        Result.Add("TotalHargaJual", ResultStokProduk.Sum(item => item.TotalHargaJual));

        return Result;
    }
    public Dictionary<string, dynamic> StokBahanBaku_Class(string action, int idTempat, int jenisStokProduk, string bahanBaku, int idSatuan, bool satuanBesar, int idKategori, string kode, string harga, string minimum, string quantity, string status)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var ListStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == idTempat).ToArray();

        switch (jenisStokProduk)
        {
            case 1: ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah > 0).ToArray(); break;
            case 2: ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah == 0).ToArray(); break;
            case 3: ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah < 0).ToArray(); break;
        }

        tempPencarian += "?IDTempat=" + idTempat;
        tempPencarian += "&JenisStokProduk=" + jenisStokProduk;

        if (!string.IsNullOrWhiteSpace(bahanBaku))
            ListStokBahanBaku = ListStokBahanBaku.Where(item => item.TBBahanBaku.Nama.ToLower().Contains(bahanBaku.ToLower())).ToArray();

        tempPencarian += "&BahanBaku=" + bahanBaku;

        if (satuanBesar)
        {
            if (idSatuan != 0)
                ListStokBahanBaku = ListStokBahanBaku.Where(item => item.TBBahanBaku.IDSatuanKonversi == idSatuan).ToArray();

            tempPencarian += "&IDSatuan=" + idSatuan;
            tempPencarian += "&SatuanBesar=true";
        }
        else
        {
            if (idSatuan != 0)
                ListStokBahanBaku = ListStokBahanBaku.Where(item => item.TBBahanBaku.IDSatuan == idSatuan).ToArray();

            tempPencarian += "&IDSatuan=" + idSatuan;
            tempPencarian += "&SatuanBesar=false";
        }

        if (idKategori != 0)
            ListStokBahanBaku = ListStokBahanBaku.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategori) != null).ToArray();

        tempPencarian += "&IDKategori=" + idKategori;

        if (!string.IsNullOrWhiteSpace(kode))
            ListStokBahanBaku = ListStokBahanBaku.Where(item => item.TBBahanBaku.KodeBahanBaku.ToLower().Contains(kode.ToLower())).ToArray();

        tempPencarian += "&Kode=" + kode;

        if (!string.IsNullOrWhiteSpace(harga))
        {
            if (harga.Contains("-"))
            {
                string[] Range = harga.Split('-');
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.HargaBeli * item.TBBahanBaku.Konversi) : item.HargaBeli) >= Range[0].ToDecimal() && (satuanBesar ? (item.HargaBeli * item.TBBahanBaku.Konversi) : item.HargaBeli) <= Range[1].ToDecimal()).OrderBy(item => (satuanBesar ? (item.HargaBeli * item.TBBahanBaku.Konversi) : item.HargaBeli)).ToArray();
            }
            else
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.HargaBeli * item.TBBahanBaku.Konversi) : item.HargaBeli) == harga.ToDecimal()).ToArray();
        }

        tempPencarian += "&Harga=" + harga;

        if (!string.IsNullOrWhiteSpace(minimum))
        {
            if (quantity.Contains("-"))
            {
                string[] Range = quantity.Split('-');
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum) >= Range[0].ToDecimal() && (satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum) <= Range[1].ToDecimal()).OrderBy(item => (satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum)).ToArray();
            }
            else
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum) == quantity.ToDecimal()).ToArray();
        }

        tempPencarian += "&Minimum=" + minimum;

        if (!string.IsNullOrWhiteSpace(quantity))
        {
            if (quantity.Contains("-"))
            {
                string[] Range = quantity.Split('-');
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah) >= Range[0].ToDecimal() && (satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah) <= Range[1].ToDecimal()).OrderBy(item => (satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah)).ToArray();
            }
            else
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah) == quantity.ToDecimal()).ToArray();
        }

        tempPencarian += "&Quantity=" + quantity;

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (status == "Cukup")
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah) >= (satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum)).ToArray();
            else
                ListStokBahanBaku = ListStokBahanBaku.Where(item => (satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah) < (satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum)).ToArray();
        }

        tempPencarian += "&Status=" + status;

        if (action == "opname")
            Result.Add("Judul", "Stock Opname");
        else if (action == "waste")
            Result.Add("Judul", "Pembuangan Bahan Baku Rusak");
        else if (action == "restock")
            Result.Add("Judul", "Restock Bahan Baku");
        else if (action == "return")
            Result.Add("Judul", "Retur ke Tempat Supplier");
        else
            Result.Add("Judul", "");

        tempPencarian += "&do=" + action;

        var ResultBahanBaku = ListStokBahanBaku.Select(item => new
        {
            BahanBaku = item.TBBahanBaku.Nama,
            SatuanKecil = item.TBBahanBaku.TBSatuan.Nama,
            SatuanBesar = item.TBBahanBaku.TBSatuan1.Nama,
            Kategori = GabungkanSemuaKategoriBahanBaku(item, null),
            Kode = item.TBBahanBaku.KodeBahanBaku,
            Harga = satuanBesar ? (item.HargaBeli * item.TBBahanBaku.Konversi) : item.HargaBeli,
            Minimum = satuanBesar ? (item.JumlahMinimum / item.TBBahanBaku.Konversi) : item.JumlahMinimum,
            Quantity = satuanBesar ? (item.Jumlah / item.TBBahanBaku.Konversi) : item.Jumlah,
            Status = item.Jumlah >= item.JumlahMinimum ? "Cukup" : "Butuh Restok"
        });

        Tempat_Class ClassTempat = new Tempat_Class(db);

        Result.Add("Data", ResultBahanBaku);
        Result.Add("Tempat", ClassTempat.Cari(idTempat).Nama);

        return Result;
    }
    public Dictionary<string, dynamic> Proyeksi(int idTempat, string idProyeksi, int idPengguna, int enumStatusProyeksi, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBProyeksis
            .Where(item =>
                item.TanggalProyeksi.Date >= tanggalAwal &&
                item.TanggalProyeksi.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idProyeksi))
            Data = Data.Where(item => item.IDProyeksi.Contains(idProyeksi)).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (enumStatusProyeksi != 0)
            Data = Data.Where(item => item.EnumStatusProyeksi == enumStatusProyeksi).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDProyeksi=" + idProyeksi;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&EnumStatusProyeksi=" + enumStatusProyeksi;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDProyeksi,
            item.Nomor,
            Tempat = item.TBTempat.Nama,
            Pengguna = item.TBPengguna.NamaLengkap,
            TanggalProyeksi = item.TanggalProyeksi.ToFormatTanggalJam(),
            TanggalTarget = item.TanggalTarget.ToFormatTanggalJam(),
            TanggalSelesai = item.TanggalSelesai.ToFormatTanggalJam(),
            TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
            Status = Pengaturan.StatusProyeksi(item.EnumStatusProyeksi.Value),
            item.EnumStatusProyeksi,
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Proyeksi", Periode, 9);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Proyeksi");
            Excel_Class.Header(3, "Pegawai");
            Excel_Class.Header(4, "Tanggal Proyeksi");
            Excel_Class.Header(5, "Tanggal Target");
            Excel_Class.Header(6, "Tanggal Selesai");
            Excel_Class.Header(7, "Jumlah");
            Excel_Class.Header(8, "Status");
            Excel_Class.Header(9, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDProyeksi);
                Excel_Class.Content(index, 3, item.Pengguna);
                Excel_Class.Content(index, 4, item.TanggalProyeksi);
                Excel_Class.Content(index, 5, item.TanggalTarget);
                Excel_Class.Content(index, 6, item.TanggalSelesai);
                Excel_Class.Content(index, 7, item.TotalJumlah);
                Excel_Class.Content(index, 8, item.Status);
                Excel_Class.Content(index, 9, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Jumlah", Data.Sum(item => item.TotalJumlah).ToFormatHargaBulat());

        return Result;
    }
    public Dictionary<string, dynamic> ProyeksiDetail(int idTempat, string idProyeksi, int idPengguna, int enumStatusProyeksi, string kode, int idPemilikProduk, int idProduk, int idAtributProduk, int idKategoriProduk, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBProyeksiDetails
            .Where(item =>
                item.TBProyeksi.TanggalProyeksi.Date >= tanggalAwal &&
                item.TBProyeksi.TanggalProyeksi.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBProyeksi.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idProyeksi))
            Data = Data.Where(item => item.IDProyeksi.Contains(idProyeksi)).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.TBProyeksi.IDPengguna == idPengguna).ToArray();

        if (enumStatusProyeksi != 0)
            Data = Data.Where(item => item.TBProyeksi.EnumStatusProyeksi == enumStatusProyeksi).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(kode)).ToArray();

        if (idPemilikProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();

        if (idProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        if (idKategoriProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == idKategoriProduk) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDProyeksi=" + idProyeksi;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&EnumStatusProyeksi=" + enumStatusProyeksi;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDKategoriProduk=" + idKategoriProduk;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDProyeksi,
                Pengguna = item.TBProyeksi.TBPengguna.NamaLengkap,
                TanggalProyeksi = item.TBProyeksi.TanggalProyeksi.ToFormatTanggalJam(),
                item.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.TBKombinasiProduk),
                Jumlah = item.Jumlah.ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Proyeksi Produk Detail", Periode, 9);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID Proyeksi");
                Excel_Class.Header(3, "Tanggal Proyeksi");
                Excel_Class.Header(4, "Kode");
                Excel_Class.Header(5, "Brand");
                Excel_Class.Header(6, "Produk");
                Excel_Class.Header(7, "Varian");
                Excel_Class.Header(8, "Kategori");
                Excel_Class.Header(9, "Jumlah");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDProyeksi);
                    Excel_Class.Content(index, 3, item.TanggalProyeksi);
                    Excel_Class.Content(index, 4, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 5, item.PemilikProduk);
                    Excel_Class.Content(index, 6, item.Produk);
                    Excel_Class.Content(index, 7, item.AtributProduk);
                    Excel_Class.Content(index, 8, item.Kategori);
                    Excel_Class.Content(index, 9, item.Jumlah);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBKombinasiProduk
            })
            .Select(item => new
            {
                KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.Key.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                Jumlah = item.Sum(x => x.Jumlah).ToFormatHargaBulat()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Proyeksi Produk Detail", Periode, 7);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Brand");
                Excel_Class.Header(4, "Produk");
                Excel_Class.Header(5, "Varian");
                Excel_Class.Header(6, "Kategori");
                Excel_Class.Header(7, "Jumlah");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 3, item.PemilikProduk);
                    Excel_Class.Content(index, 4, item.Produk);
                    Excel_Class.Content(index, 5, item.AtributProduk);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.Jumlah);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("Jumlah", Data.Sum(item => item.Jumlah).ToFormatHargaBulat());

        return Result;
    }
    public Dictionary<string, dynamic> PerbandinganHargaVendor(int idTempat, int idProduk, int idAtributProduk)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        TBVendor[] daftarsVendor = db.TBVendors.OrderBy(item => item.Nama).ToArray();
        TBHargaVendor[] daftarHargavendor = db.TBHargaVendors.OrderByDescending(item => item.Tanggal).ToArray();
        TBStokProduk[] daftarStokProduk = db.TBStokProduks.ToArray();

        if (idTempat != 0)
            daftarStokProduk = daftarStokProduk.Where(item => item.IDTempat == idTempat).ToArray();

        if (idProduk != 0)
            daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        Result.Add("DataVendor", daftarsVendor);
        Result.Add("DataJumlahVendor", daftarsVendor.Count());

        var daftarHargaProduk = daftarStokProduk.AsEnumerable().OrderBy(stokProduk => stokProduk.TBKombinasiProduk.TBProduk.Nama).Select(stokProduk => new
        {
            Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama,
            AtributProduk = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
            HargaVendor = daftarsVendor.Select(vendor => new
            {
                Harga = daftarHargavendor.FirstOrDefault(data => data.TBVendor == vendor && data.TBStokProduk == stokProduk) == null
                ? "0" : daftarHargavendor.FirstOrDefault(data => data.TBVendor == vendor && data.TBStokProduk == stokProduk).Harga.ToFormatHarga()
            })
        }).ToArray();

        Result.Add("DataHargaVendor", daftarHargaProduk);


        return Result;
    }
    public Dictionary<string, dynamic> PerbandinganHargaSupplier(int idTempat, int idBahanBaku, int idSatuan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        TBSupplier[] daftarsSupplier = db.TBSuppliers.OrderBy(item => item.Nama).ToArray();
        TBHargaSupplier[] daftarHargaSupplier = db.TBHargaSuppliers.OrderByDescending(item => item.Tanggal).ToArray();
        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.ToArray();

        if (idTempat != 0)
            daftarStokBahanBaku = daftarStokBahanBaku.Where(item => item.IDTempat == idTempat).ToArray();

        if (idBahanBaku != 0)
            daftarStokBahanBaku = daftarStokBahanBaku.Where(item => item.TBBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            daftarStokBahanBaku = daftarStokBahanBaku.Where(item => item.TBBahanBaku.IDSatuanKonversi == idSatuan).ToArray();

        Result.Add("DataSupplier", daftarsSupplier);
        Result.Add("DataJumlahSupplier", daftarsSupplier.Count());

        var daftarHargaBahanBaku = daftarStokBahanBaku.AsEnumerable().OrderBy(stokBahanBaku => stokBahanBaku.TBBahanBaku.Nama).Select(stokBahanBaku => new
        {
            BahanBaku = stokBahanBaku.TBBahanBaku.Nama,
            Satuan = stokBahanBaku.TBBahanBaku.TBSatuan1.Nama,
            HargaSupplier = daftarsSupplier.Select(supplier => new
            {
                Harga = daftarHargaSupplier.FirstOrDefault(data => data.TBSupplier == supplier && data.TBStokBahanBaku == stokBahanBaku) == null
                ? "0" : daftarHargaSupplier.FirstOrDefault(data => data.TBSupplier == supplier && data.TBStokBahanBaku == stokBahanBaku).Harga.ToFormatHarga()
            })
        }).ToArray();

        Result.Add("DataHargaSupplier", daftarHargaBahanBaku);


        return Result;
    }
    public Dictionary<string, dynamic> PerpindahanStokProdukDetail(int idTempat, int idPengguna, string kode, int idProduk, int idAtributProduk, int idKategori, int idJenisPerpindahanStok, string keterangan, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPerpindahanStokProduks
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBStokProduk.TBKombinasiProduk.KodeKombinasiProduk.Contains(kode)).ToArray();

        if (idProduk != 0)
            Data = Data.Where(item => item.TBStokProduk.TBKombinasiProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            Data = Data.Where(item => item.TBStokProduk.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        if (idKategori != 0)
            Data = Data.Where(item => item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == idKategori) != null).ToArray();

        if (idJenisPerpindahanStok != 0)
            Data = Data.Where(item => item.IDJenisPerpindahanStok == idJenisPerpindahanStok).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDKategori=" + idKategori;
        tempPencarian += "&IDJenisPerpindahanStok=" + idJenisPerpindahanStok;
        tempPencarian += "&Keterangan=" + keterangan;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                OrderTanggal = item.Tanggal,
                Tanggal = item.Tanggal.ToFormatTanggalJam(),
                Tempat = item.TBTempat.Nama,
                Pengguna = item.TBPengguna.NamaLengkap,
                KodeKombinasiProduk = item.TBStokProduk.TBKombinasiProduk.KodeKombinasiProduk,
                Produk = item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBStokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
                Kategori = GabungkanSemuaKategoriProduk(item.TBStokProduk, null),
                Jenis = item.TBJenisPerpindahanStok.Nama,
                Status = item.TBJenisPerpindahanStok.Status,
                Jumlah = item.TBJenisPerpindahanStok.Status.Value ? item.Jumlah.ToFormatHargaBulat() : (item.Jumlah * -1).ToFormatHargaBulat(),
                item.Keterangan
            }).OrderByDescending(item => item.OrderTanggal);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Perpindahan Stok Produk Detail", Periode, 11);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Tanggal");
                Excel_Class.Header(3, "Tempat");
                Excel_Class.Header(4, "Pegawai");
                Excel_Class.Header(5, "Kode");
                Excel_Class.Header(6, "Produk");
                Excel_Class.Header(7, "Varian");
                Excel_Class.Header(8, "Kategori");
                Excel_Class.Header(9, "Jenis");
                Excel_Class.Header(10, "Jumlah");
                Excel_Class.Header(11, "Keterangan");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Tanggal);
                    Excel_Class.Content(index, 3, item.Tempat);
                    Excel_Class.Content(index, 4, item.Pengguna);
                    Excel_Class.Content(index, 5, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 6, item.Produk);
                    Excel_Class.Content(index, 7, item.AtributProduk);
                    Excel_Class.Content(index, 8, item.Kategori);
                    Excel_Class.Content(index, 9, item.Jenis);
                    Excel_Class.Content(index, 10, item.Jumlah);
                    Excel_Class.Content(index, 11, item.Keterangan);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBStokProduk,
                item.TBJenisPerpindahanStok
            }).Select(item => new
            {
                Tempat = item.Key.TBStokProduk.TBTempat.Nama,
                KodeKombinasiProduk = item.Key.TBStokProduk.TBKombinasiProduk.KodeKombinasiProduk,
                Produk = item.Key.TBStokProduk.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBStokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
                Kategori = GabungkanSemuaKategoriProduk(item.Key.TBStokProduk, null),
                Jenis = item.Key.TBJenisPerpindahanStok.Nama,
                Status = item.Key.TBJenisPerpindahanStok.Status,
                Jumlah = item.Sum(x => item.Key.TBJenisPerpindahanStok.Status.Value ? x.Jumlah : x.Jumlah * -1).ToFormatHargaBulat()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Perpindahan Stok Produk", Periode, 8);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Tempat");
                Excel_Class.Header(3, "Kode");
                Excel_Class.Header(4, "Produk");
                Excel_Class.Header(5, "Varian");
                Excel_Class.Header(6, "Kategori");
                Excel_Class.Header(7, "Jenis");
                Excel_Class.Header(8, "Jumlah");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Tempat);
                    Excel_Class.Content(index, 3, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 4, item.Produk);
                    Excel_Class.Content(index, 5, item.AtributProduk);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.Jenis);
                    Excel_Class.Content(index, 8, item.Jumlah);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("Jumlah", Data.Sum(item => item.TBJenisPerpindahanStok.Status.Value ? item.Jumlah : item.Jumlah * -1).ToFormatHargaBulat());

        return Result;
    }
    public Dictionary<string, dynamic> PerpindahanStokBahanBakuDetail(int idTempat, int idPengguna, string kode, int idBahanBaku, int idSatuan, int idKategori, int idJenisPerpindahanStok, string keterangan, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPerpindahanStokBahanBakus
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idBahanBaku != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.IDSatuan == idSatuan).ToArray();

        if (idKategori != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategori) != null).ToArray();

        if (idJenisPerpindahanStok != 0)
            Data = Data.Where(item => item.IDJenisPerpindahanStok == idJenisPerpindahanStok).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idBahanBaku;
        tempPencarian += "&IDKategori=" + idKategori;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&IDJenisPerpindahanStok=" + idJenisPerpindahanStok;
        tempPencarian += "&Keterangan=" + keterangan;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                OrderTanggal = item.Tanggal,
                Tanggal = item.Tanggal.ToFormatTanggalJam(),
                Tempat = item.TBTempat.Nama,
                Pengguna = item.TBPengguna.NamaLengkap,
                KodeBahanBaku = item.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBStokBahanBaku.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(item.TBStokBahanBaku, null),
                Jenis = item.TBJenisPerpindahanStok.Nama,
                Status = item.TBJenisPerpindahanStok.Status,
                Jumlah = item.TBJenisPerpindahanStok.Status.Value ? item.Jumlah.ToFormatHarga() : (item.Jumlah * -1).ToFormatHarga(),
                item.Keterangan
            }).OrderByDescending(item => item.OrderTanggal);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Perpindahan Stok Bahan Baku Detail", Periode, 11);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Tanggal");
                Excel_Class.Header(3, "Tempat");
                Excel_Class.Header(4, "Pegawai");
                Excel_Class.Header(5, "Kode");
                Excel_Class.Header(6, "Bahan Baku");
                Excel_Class.Header(7, "Satuan");
                Excel_Class.Header(8, "Kategori");
                Excel_Class.Header(9, "Jenis");
                Excel_Class.Header(10, "Jumlah");
                Excel_Class.Header(11, "Keterangan");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Tanggal);
                    Excel_Class.Content(index, 3, item.Tempat);
                    Excel_Class.Content(index, 4, item.Pengguna);
                    Excel_Class.Content(index, 5, item.KodeBahanBaku);
                    Excel_Class.Content(index, 6, item.BahanBaku);
                    Excel_Class.Content(index, 7, item.Satuan);
                    Excel_Class.Content(index, 8, item.Kategori);
                    Excel_Class.Content(index, 9, item.Jenis);
                    Excel_Class.Content(index, 10, item.Jumlah);
                    Excel_Class.Content(index, 11, item.Keterangan);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBStokBahanBaku,
                item.TBJenisPerpindahanStok,
                item.TBSatuan
            }).Select(item => new
            {
                Tempat = item.Key.TBStokBahanBaku.TBTempat.Nama,
                KodeBahanBaku = item.Key.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBStokBahanBaku.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(item.Key.TBStokBahanBaku, null),
                Jenis = item.Key.TBJenisPerpindahanStok.Nama,
                Status = item.Key.TBJenisPerpindahanStok.Status,
                Jumlah = item.Sum(x => item.Key.TBJenisPerpindahanStok.Status.Value ? x.Jumlah : x.Jumlah * -1).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Perpindahan Stok Produk", Periode, 8);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Tempat");
                Excel_Class.Header(3, "Kode");
                Excel_Class.Header(4, "Bahan Baku");
                Excel_Class.Header(5, "Satuan");
                Excel_Class.Header(6, "Ketegori");
                Excel_Class.Header(7, "Jenis");
                Excel_Class.Header(8, "Jumlah");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Tempat);
                    Excel_Class.Content(index, 3, item.KodeBahanBaku);
                    Excel_Class.Content(index, 4, item.BahanBaku);
                    Excel_Class.Content(index, 5, item.Satuan);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.Jenis);
                    Excel_Class.Content(index, 8, item.Jumlah);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("Jumlah", Data.Sum(item => item.TBJenisPerpindahanStok.Status.Value ? item.Jumlah : item.Jumlah * -1).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiProduk(int idTempat, string idPOProduksiProduk, int enumJenisPoProduksi, int idVendor, int idPenggunaPending, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiProduks
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiProduk))
            Data = Data.Where(item => item.IDPOProduksiProduk.Contains(idPOProduksiProduk)).ToArray();

        if (enumJenisPoProduksi != 0)
            Data = Data.Where(item => item.EnumJenisProduksi == enumJenisPoProduksi).ToArray();

        if (idVendor != 0)

            Data = Data.Where(item => item.IDVendor == idVendor).ToArray();
        if (idPenggunaPending != 0)
            Data = Data.Where(item => item.IDPengguna == idPenggunaPending).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiProduk=" + idPOProduksiProduk;
        tempPencarian += "&IDVendor=" + idVendor;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPoProduksi;
        tempPencarian += "&IDPenggunaPending=" + idPenggunaPending;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPOProduksiProduk,
            item.Nomor,
            item.EnumJenisProduksi,
            Tempat = item.TBTempat.Nama,
            Vendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
            Pengguna = item.TBPengguna.NamaLengkap,
            TanggalPending = item.Tanggal.ToFormatTanggalJam(),
            TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
            TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
            TotalJumlahRevisi = item.TotalJumlah.ToFormatHargaBulat(),
            Grandtotal = item.Grandtotal.ToFormatHarga(),
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, Pengaturan.JenisPOProduksi(enumJenisPoProduksi, "Produk"), Periode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID PO");
            Excel_Class.Header(3, "Tempat");
            Excel_Class.Header(4, "Vendor");
            Excel_Class.Header(5, "Pegawai");
            Excel_Class.Header(6, "Tanggal");
            Excel_Class.Header(7, "Tanggal Jatuh Tempo");
            Excel_Class.Header(8, "Tanggal Pengiriman");
            Excel_Class.Header(9, "Jumlah");
            Excel_Class.Header(10, "Grandtotal");
            Excel_Class.Header(11, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPOProduksiProduk);
                Excel_Class.Content(index, 3, item.Tempat);
                Excel_Class.Content(index, 4, item.Vendor);
                Excel_Class.Content(index, 5, item.Pengguna);
                Excel_Class.Content(index, 6, item.TanggalPending);
                Excel_Class.Content(index, 7, item.TanggalJatuhTempo);
                Excel_Class.Content(index, 8, item.TanggalPengiriman);
                Excel_Class.Content(index, 9, item.TotalJumlahRevisi);
                Excel_Class.Content(index, 10, item.Grandtotal);
                Excel_Class.Content(index, 11, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("TotalJumlah", Data.Sum(item => item.TotalJumlah).ToFormatHargaBulat());
        Result.Add("Grandtotal", Data.Sum(item => item.Grandtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiProdukDetail(int idTempat, string idPOProduksiProduk, int enumJenisPoProduksi, int idVendor, int idPenggunaPending, int enumStatusProduksi, string kode, int idPemilikProduk, int idProduk, int idAtributProduk, int idKategoriProduk, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiProdukDetails
            .Where(item =>
                item.TBPOProduksiProduk.Tanggal.Date >= tanggalAwal &&
                item.TBPOProduksiProduk.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiProduk))
            Data = Data.Where(item => item.IDPOProduksiProduk.Contains(idPOProduksiProduk)).ToArray();

        if (enumJenisPoProduksi != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.EnumJenisProduksi == enumJenisPoProduksi).ToArray();

        if (idVendor != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.IDVendor == idVendor).ToArray();

        if (idPenggunaPending != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.IDPengguna == idPenggunaPending).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(kode)).ToArray();

        if (idPemilikProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();

        if (idProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        if (idKategoriProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == idKategoriProduk) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiProduk=" + idPOProduksiProduk;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPoProduksi;
        tempPencarian += "&IDPenggunaPending=" + idPenggunaPending;
        tempPencarian += "&EnumStatusProduksi=" + enumStatusProduksi;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDKategoriProduk=" + idKategoriProduk;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDPOProduksiProduk,
                Pengguna = item.TBPOProduksiProduk.TBPengguna.NamaLengkap,
                TanggalPending = item.TBPOProduksiProduk.Tanggal.ToFormatTanggalJam(),
                item.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.TBKombinasiProduk),
                HargaPokokKomposisi = item.HargaPokokKomposisi.ToFormatHarga(),
                BiayaTambahan = item.BiayaTambahan.ToFormatHarga(),
                HargaVendor = item.HargaVendor.ToFormatHarga(),
                PotonganHargaVendor = item.PotonganHargaVendor.ToFormatHarga(),
                Jumlah = item.Jumlah.ToFormatHargaBulat(),
                Subtotal = (item.SubtotalHPP + item.SubtotalHargaVendor).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, Pengaturan.JenisPOProduksi(enumJenisPoProduksi, "Produk") + " Detail", Periode, 14);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID PO");
                Excel_Class.Header(3, "Tanggal Pending");
                Excel_Class.Header(4, "Kode");
                Excel_Class.Header(5, "Brand");
                Excel_Class.Header(6, "Produk");
                Excel_Class.Header(7, "Varian");
                Excel_Class.Header(8, "Kategori");
                Excel_Class.Header(9, "Komposisi");
                Excel_Class.Header(10, "Biaya");
                Excel_Class.Header(11, "Harga");
                Excel_Class.Header(12, "Potongan");
                Excel_Class.Header(13, "Jumlah");
                Excel_Class.Header(14, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDPOProduksiProduk);
                    Excel_Class.Content(index, 3, item.TanggalPending);
                    Excel_Class.Content(index, 4, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 5, item.PemilikProduk);
                    Excel_Class.Content(index, 6, item.Produk);
                    Excel_Class.Content(index, 7, item.AtributProduk);
                    Excel_Class.Content(index, 8, item.Kategori);
                    Excel_Class.Content(index, 9, item.HargaPokokKomposisi);
                    Excel_Class.Content(index, 10, item.BiayaTambahan);
                    Excel_Class.Content(index, 11, item.HargaVendor);
                    Excel_Class.Content(index, 12, item.PotonganHargaVendor);
                    Excel_Class.Content(index, 13, item.Jumlah);
                    Excel_Class.Content(index, 14, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBKombinasiProduk
            })
            .Select(item => new
            {
                KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.Key.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                Jumlah = item.Sum(x => x.Jumlah).ToFormatHargaBulat(),
                Subtotal = item.Sum(x => x.SubtotalHPP + x.SubtotalHargaVendor).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, Pengaturan.JenisPOProduksi(enumJenisPoProduksi, "Produk") + " Detail", Periode, 8);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Brand");
                Excel_Class.Header(4, "Produk");
                Excel_Class.Header(5, "Varian");
                Excel_Class.Header(6, "Kategori");
                Excel_Class.Header(7, "Jumlah");
                Excel_Class.Header(8, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 3, item.PemilikProduk);
                    Excel_Class.Content(index, 4, item.Produk);
                    Excel_Class.Content(index, 5, item.AtributProduk);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.Jumlah);
                    Excel_Class.Content(index, 8, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("TotalJumlah", Data.Sum(item => item.Jumlah).ToFormatHargaBulat());
        Result.Add("Grandtotal", Data.Sum(item => item.SubtotalHPP + item.SubtotalHargaVendor).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiProdukRetur(int idTempat, string idPOProduksiProdukRetur, int idVendor, int idPengguna, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiProdukReturs
            .Where(item =>
                item.TanggalRetur.Value.Date >= tanggalAwal &&
                item.TanggalRetur.Value.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiProdukRetur))
            Data = Data.Where(item => item.IDPOProduksiProdukRetur.Contains(idPOProduksiProdukRetur)).ToArray();

        if (idVendor != 0)
            Data = Data.Where(item => item.IDVendor == idVendor).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiProdukRetur=" + idPOProduksiProdukRetur;
        tempPencarian += "&IDVendor=" + idVendor;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPOProduksiProdukRetur,
            item.Nomor,
            Tempat = item.TBTempat.Nama,
            Vendor = item.TBVendor.Nama,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.TanggalRetur.Value.ToFormatTanggalJam(),
            EnumStatusRetur = item.EnumStatusRetur,
            Status = Pengaturan.StatusPOProduksi(item.EnumStatusRetur.ToString()),
            Grandtotal = item.Grandtotal.ToFormatHarga(),
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Retur Produk", Periode, 9);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Retur");
            Excel_Class.Header(3, "Tempat");
            Excel_Class.Header(4, "Vendor");
            Excel_Class.Header(5, "Pegawai");
            Excel_Class.Header(6, "Tanggal");
            Excel_Class.Header(7, "Status");
            Excel_Class.Header(8, "Grandtotal");
            Excel_Class.Header(9, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPOProduksiProdukRetur);
                Excel_Class.Content(index, 3, item.Tempat);
                Excel_Class.Content(index, 4, item.Vendor);
                Excel_Class.Content(index, 5, item.Pengguna);
                Excel_Class.Content(index, 6, item.Tanggal);
                Excel_Class.Content(index, 7, item.Status);
                Excel_Class.Content(index, 8, item.Grandtotal);
                Excel_Class.Content(index, 9, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Grandtotal", Data.Sum(item => item.Grandtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiProdukPenagihan(int idTempat, string idPOProduksiProdukPenagihan, int idVendor, int idPengguna, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiProdukPenagihans
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiProdukPenagihan))
            Data = Data.Where(item => item.IDPOProduksiProdukPenagihan.Contains(idPOProduksiProdukPenagihan)).ToArray();

        if (idVendor != 0)
            Data = Data.Where(item => item.IDVendor == idVendor).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiProdukPenagihan=" + idPOProduksiProdukPenagihan;
        tempPencarian += "&IDVendorr=" + idVendor;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPOProduksiProdukPenagihan,
            item.Nomor,
            Tempat = item.TBTempat.Nama,
            Vendor = item.TBVendor.Nama,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.Tanggal.ToFormatTanggalJam(),
            TotalPenerimaan = item.TotalPenerimaan.ToFormatHarga(),
            TotalRetur = item.TotalRetur.ToFormatHarga(),
            TotalDownPayment = item.TotalDownPayment.ToFormatHarga(),
            TotalTagihan = item.TotalTagihan.ToFormatHarga(),
            TotalBayar = item.TotalBayar.ToFormatHarga(),
            StatusPembayaran = item.StatusPembayaran,
            Status = item.StatusPembayaran == false ? "<label class=\"label label-warning\">Tagihan</label>" : "<label class=\"label label-success\">Lunas</label>",
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Penagihan PO Produk", Periode, 13);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Retur");
            Excel_Class.Header(3, "Tempat");
            Excel_Class.Header(4, "Vendor");
            Excel_Class.Header(5, "Pegawai");
            Excel_Class.Header(6, "Tanggal");
            Excel_Class.Header(7, "TotalPenerimaan");
            Excel_Class.Header(8, "TotalRetur");
            Excel_Class.Header(9, "TotalDownPayment");
            Excel_Class.Header(10, "TotalTagihan");
            Excel_Class.Header(11, "TotalBayar");
            Excel_Class.Header(12, "Status");
            Excel_Class.Header(13, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPOProduksiProdukPenagihan);
                Excel_Class.Content(index, 3, item.Tempat);
                Excel_Class.Content(index, 4, item.Vendor);
                Excel_Class.Content(index, 5, item.Pengguna);
                Excel_Class.Content(index, 6, item.Tanggal);
                Excel_Class.Content(index, 7, item.TotalPenerimaan);
                Excel_Class.Content(index, 8, item.TotalRetur);
                Excel_Class.Content(index, 9, item.TotalDownPayment);
                Excel_Class.Content(index, 10, item.TotalTagihan);
                Excel_Class.Content(index, 11, item.TotalBayar);
                Excel_Class.Content(index, 12, item.Status);
                Excel_Class.Content(index, 13, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }

    public Dictionary<string, dynamic> POProduksiBahanBaku(int idTempat, string idPOProduksiBahanBaku, int enumJenisPoProduksi, int idSupplier, int idPenggunaPending, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiBahanBakus
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiBahanBaku))
            Data = Data.Where(item => item.IDPOProduksiBahanBaku.Contains(idPOProduksiBahanBaku)).ToArray();

        if (enumJenisPoProduksi != 0)
            Data = Data.Where(item => item.EnumJenisProduksi == enumJenisPoProduksi).ToArray();

        if (idSupplier != 0)

            Data = Data.Where(item => item.IDSupplier == idSupplier).ToArray();
        if (idPenggunaPending != 0)
            Data = Data.Where(item => item.IDPengguna == idPenggunaPending).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiBahanBaku=" + idPOProduksiBahanBaku;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPoProduksi;
        tempPencarian += "&IDSupplier=" + idSupplier;
        tempPencarian += "&IDPenggunaPending=" + idPenggunaPending;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPOProduksiBahanBaku,
            item.Nomor,
            item.EnumJenisProduksi,
            Tempat = item.TBTempat.Nama,
            Supplier = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.Tanggal.ToFormatTanggalJam(),
            TanggalJatuhTempo = item.TanggalJatuhTempo.ToFormatTanggal(),
            TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
            TotalJumlah = item.TotalJumlah.ToFormatHarga(),
            Grandtotal = item.Grandtotal.ToFormatHarga(),
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, Pengaturan.JenisPOProduksi(enumJenisPoProduksi, "BahanBaku"), Periode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID PO");
            Excel_Class.Header(3, "Tempat");
            Excel_Class.Header(4, "Supplier");
            Excel_Class.Header(5, "Pegawai");
            Excel_Class.Header(6, "Tanggal");
            Excel_Class.Header(7, "Tanggal Jatuh Tempo");
            Excel_Class.Header(8, "Tanggal Pengiriman");
            Excel_Class.Header(9, "Jumlah");
            Excel_Class.Header(10, "Grandtotal");
            Excel_Class.Header(11, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPOProduksiBahanBaku);
                Excel_Class.Content(index, 3, item.Tempat);
                Excel_Class.Content(index, 4, item.Supplier);
                Excel_Class.Content(index, 5, item.Pengguna);
                Excel_Class.Content(index, 6, item.Tanggal);
                Excel_Class.Content(index, 7, item.TanggalJatuhTempo);
                Excel_Class.Content(index, 8, item.TanggalPengiriman);
                Excel_Class.Content(index, 9, item.TotalJumlah);
                Excel_Class.Content(index, 10, item.Grandtotal);
                Excel_Class.Content(index, 11, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("TotalJumlah", Data.Sum(item => item.TotalJumlah).ToFormatHarga());
        Result.Add("Grandtotal", Data.Sum(item => item.Grandtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiBahanBakuDetail(int idTempat, string idPOProduksiBahanBaku, int enumJenisPoProduksi, int idSupplier, int idPenggunaPending, int enumStatusProduksi, string kode, int idBahanBaku, int idSatuan, int idKategoriBahanBaku, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiBahanBakuDetails
            .Where(item =>
                item.TBPOProduksiBahanBaku.Tanggal.Date >= tanggalAwal &&
                item.TBPOProduksiBahanBaku.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiBahanBaku))
            Data = Data.Where(item => item.IDPOProduksiBahanBaku.Contains(idPOProduksiBahanBaku)).ToArray();

        if (enumJenisPoProduksi != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.EnumJenisProduksi == enumJenisPoProduksi).ToArray();

        if (idSupplier != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.IDSupplier == idSupplier).ToArray();

        if (idPenggunaPending != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.IDPengguna == idPenggunaPending).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDSatuan == idSatuan).ToArray();

        if (idKategoriBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategoriBahanBaku) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiBahanBaku=" + idPOProduksiBahanBaku;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPoProduksi;
        tempPencarian += "&IDPenggunaPending=" + idPenggunaPending;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idBahanBaku;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&IDKategoriBahanBaku=" + idKategoriBahanBaku;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDPOProduksiBahanBaku,
                Pengguna = item.TBPOProduksiBahanBaku.TBPengguna.NamaLengkap,
                Tanggal = item.TBPOProduksiBahanBaku.Tanggal.ToFormatTanggalJam(),
                item.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.TBBahanBaku),
                HargaPokokKomposisi = item.HargaPokokKomposisi.ToFormatHarga(),
                BiayaTambahan = item.BiayaTambahan.ToFormatHarga(),
                HargaSupplier = item.HargaSupplier.ToFormatHarga(),
                PotonganHargaSupplier = item.PotonganHargaSupplier.ToFormatHarga(),
                Jumlah = item.Jumlah.ToFormatHarga(),
                Subtotal = (item.SubtotalHPP + item.SubtotalHargaSupplier).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, Pengaturan.JenisPOProduksi(enumJenisPoProduksi, "BahanBaku") + " Detail", Periode, 13);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID PO");
                Excel_Class.Header(3, "Tanggal Pending");
                Excel_Class.Header(4, "Kode");
                Excel_Class.Header(5, "Bahan Baku");
                Excel_Class.Header(6, "Satuan");
                Excel_Class.Header(7, "Kategori");
                Excel_Class.Header(8, "Komposisi");
                Excel_Class.Header(9, "Biaya");
                Excel_Class.Header(10, "Harga");
                Excel_Class.Header(11, "Potongan");
                Excel_Class.Header(12, "Jumlah");
                Excel_Class.Header(13, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDPOProduksiBahanBaku);
                    Excel_Class.Content(index, 3, item.Tanggal);
                    Excel_Class.Content(index, 4, item.KodeBahanBaku);
                    Excel_Class.Content(index, 6, item.BahanBaku);
                    Excel_Class.Content(index, 7, item.Satuan);
                    Excel_Class.Content(index, 8, item.Kategori);
                    Excel_Class.Content(index, 9, item.HargaPokokKomposisi);
                    Excel_Class.Content(index, 10, item.BiayaTambahan);
                    Excel_Class.Content(index, 11, item.HargaSupplier);
                    Excel_Class.Content(index, 12, item.PotonganHargaSupplier);
                    Excel_Class.Content(index, 13, item.Jumlah);
                    Excel_Class.Content(index, 14, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                KodeBahanBaku = item.Key.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.Key.TBBahanBaku),
                Jumlah = item.Sum(x => x.Jumlah).ToFormatHarga(),
                Subtotal = item.Sum(x => x.SubtotalHPP + x.SubtotalHargaSupplier).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, Pengaturan.JenisPOProduksi(enumJenisPoProduksi, "BahanBaku") + " Detail", Periode, 7);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Bahan Baku");
                Excel_Class.Header(4, "Satuan");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Jumlah");
                Excel_Class.Header(7, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeBahanBaku);
                    Excel_Class.Content(index, 3, item.BahanBaku);
                    Excel_Class.Content(index, 4, item.Satuan);
                    Excel_Class.Content(index, 5, item.Kategori);
                    Excel_Class.Content(index, 6, item.Jumlah);
                    Excel_Class.Content(index, 7, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("TotalJumlah", Data.Sum(item => item.Jumlah).ToFormatHarga());
        Result.Add("Grandtotal", Data.Sum(item => item.SubtotalHPP + item.SubtotalHargaSupplier).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> PenerimaanPOProduksiBahanBaku(int idTempat, string idPenerimaanPOProduksiBahanBaku, string idPOProduksiBahanBaku, int enumJenisPOProduksi, int idSupplier, int idPengguna, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPenerimaanPOProduksiBahanBakus
            .Where(item =>
                item.TanggalDatang.Date >= tanggalAwal &&
                item.TanggalDatang.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPenerimaanPOProduksiBahanBaku))
            Data = Data.Where(item => item.IDPenerimaanPOProduksiBahanBaku.Contains(idPenerimaanPOProduksiBahanBaku)).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiBahanBaku))
            Data = Data.Where(item => item.IDPOProduksiBahanBaku.Contains(idPOProduksiBahanBaku)).ToArray();

        if (enumJenisPOProduksi != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.EnumJenisProduksi == enumJenisPOProduksi).ToArray();

        if (idSupplier != 0)
            Data = Data.Where(item => item.TBPOProduksiBahanBaku.IDSupplier == idSupplier).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPenggunaDatang == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.Contains(keterangan)).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiBahanBaku=" + idPOProduksiBahanBaku;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPOProduksi;
        tempPencarian += "&IDSupplier=" + idSupplier;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPenerimaanPOProduksiBahanBaku,
            item.Nomor,
            item.IDPOProduksiBahanBaku,
            item.TBPOProduksiBahanBaku.EnumJenisProduksi,
            Tempat = item.TBPOProduksiBahanBaku.TBTempat.Nama,
            Supplier = item.TBPOProduksiBahanBaku.IDSupplier != null ? item.TBPOProduksiBahanBaku.TBSupplier.Nama : string.Empty,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.TanggalDatang.ToFormatTanggalJam(),
            TotalDatang = item.TotalDatang.ToFormatHarga(),
            TotalDiterima = item.TotalDiterima.ToFormatHarga(),
            TotalTolakKeSupplier = item.TotalTolakKeSupplier.ToFormatHarga(),
            TotalTolakKeGudang = item.TotalTolakKeGudang.ToFormatHarga(),
            Grandtotal = item.Grandtotal.ToFormatHarga(),
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Penerimaan " + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "BahanBaku"), Periode, 13);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Penerimaan");
            Excel_Class.Header(3, "ID PO");
            Excel_Class.Header(4, "Tempat");
            Excel_Class.Header(5, "Supplier");
            Excel_Class.Header(6, "Pegawai");
            Excel_Class.Header(7, "Tanggal");
            Excel_Class.Header(8, "Total Datang");
            Excel_Class.Header(9, "Total Diterima");
            Excel_Class.Header(10, "Tolak Supplier");
            Excel_Class.Header(11, "Tolak Gudang");
            Excel_Class.Header(12, "Grandtotal");
            Excel_Class.Header(13, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPenerimaanPOProduksiBahanBaku);
                Excel_Class.Content(index, 3, item.IDPOProduksiBahanBaku);
                Excel_Class.Content(index, 4, item.Tempat);
                Excel_Class.Content(index, 5, item.Supplier);
                Excel_Class.Content(index, 6, item.Pengguna);
                Excel_Class.Content(index, 7, item.Tanggal);
                Excel_Class.Content(index, 8, item.TotalDatang);
                Excel_Class.Content(index, 9, item.TotalDiterima);
                Excel_Class.Content(index, 10, item.TotalTolakKeSupplier);
                Excel_Class.Content(index, 11, item.TotalTolakKeGudang);
                Excel_Class.Content(index, 12, item.Grandtotal);
                Excel_Class.Content(index, 13, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("TotalDatang", Data.Sum(item => item.TotalDatang).ToFormatHarga());
        Result.Add("TotalDiterima", Data.Sum(item => item.TotalDiterima).ToFormatHarga());
        Result.Add("TotalTolakKeSupplier", Data.Sum(item => item.TotalTolakKeSupplier).ToFormatHarga());
        Result.Add("TotalTolakKeGudang", Data.Sum(item => item.TotalTolakKeGudang).ToFormatHarga());
        Result.Add("Grandtotal", Data.Sum(item => item.Grandtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> PenerimaanPOProduksiBahanBakuDetail(int idTempat, string idPenerimaanPOProduksiBahanBaku, string idPOProduksiBahanBaku, int enumJenisPOProduksi, int idSupplier, int idPengguna, string kode, int idBahanBaku, int idSatuan, int idKategoriBahanBaku, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPenerimaanPOProduksiBahanBakuDetails
            .Where(item =>
                item.TBPenerimaanPOProduksiBahanBaku.TanggalDatang.Date >= tanggalAwal &&
                item.TBPenerimaanPOProduksiBahanBaku.TanggalDatang.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPenerimaanPOProduksiBahanBaku))
            Data = Data.Where(item => item.IDPenerimaanPOProduksiBahanBaku.Contains(idPenerimaanPOProduksiBahanBaku)).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiBahanBaku))
            Data = Data.Where(item => item.TBPenerimaanPOProduksiBahanBaku.IDPOProduksiBahanBaku.Contains(idPOProduksiBahanBaku)).ToArray();

        if (enumJenisPOProduksi != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.EnumJenisProduksi == enumJenisPOProduksi).ToArray();

        if (idSupplier != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDSupplier == idSupplier).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiBahanBaku.IDPenggunaDatang == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDSatuan == idSatuan).ToArray();

        if (idKategoriBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategoriBahanBaku) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDSupplier=" + idSupplier;
        tempPencarian += "&IDPenerimaanPOProduksiBahanBaku=" + idPenerimaanPOProduksiBahanBaku;
        tempPencarian += "&IDPOProduksiBahanBaku=" + idPOProduksiBahanBaku;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPOProduksi;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idBahanBaku;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&IDKategoriBahanBaku=" + idKategoriBahanBaku;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDPenerimaanPOProduksiBahanBaku,
                item.TBPenerimaanPOProduksiBahanBaku.IDPOProduksiBahanBaku,
                Tempat = item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.TBTempat.Nama,
                Supplier = item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDSupplier != null ? item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.TBSupplier.Nama : string.Empty,
                Pengguna = item.TBPenerimaanPOProduksiBahanBaku.TBPengguna.NamaLengkap,
                Tanggal = item.TBPenerimaanPOProduksiBahanBaku.TanggalDatang.ToFormatTanggalJam(),
                item.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.TBBahanBaku),
                Datang = item.Datang.ToFormatHarga(),
                Diterima = item.Diterima.ToFormatHarga(),
                TolakKeSupplier = item.TolakKeSupplier.ToFormatHarga(),
                Subtotal = (item.SubtotalHPP + item.SubtotalHargaSupplier).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Penerimaan " + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "BahanBaku") + " Detail", Periode, 16);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID Penerimaan");
                Excel_Class.Header(3, "ID PO");
                Excel_Class.Header(4, "Tempat");
                Excel_Class.Header(5, "Supplier");
                Excel_Class.Header(6, "Pengguna");
                Excel_Class.Header(7, "Tanggal");
                Excel_Class.Header(8, "Kode");
                Excel_Class.Header(9, "Bahan Baku");
                Excel_Class.Header(10, "Satuan");
                Excel_Class.Header(11, "Kategori");
                Excel_Class.Header(12, "Datang");
                Excel_Class.Header(13, "Diterima");
                Excel_Class.Header(14, "Tolak Supplier");
                Excel_Class.Header(15, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDPenerimaanPOProduksiBahanBaku);
                    Excel_Class.Content(index, 3, item.IDPOProduksiBahanBaku);
                    Excel_Class.Content(index, 4, item.Tempat);
                    Excel_Class.Content(index, 5, item.Supplier);
                    Excel_Class.Content(index, 6, item.Pengguna);
                    Excel_Class.Content(index, 7, item.Tanggal);
                    Excel_Class.Content(index, 8, item.KodeBahanBaku);
                    Excel_Class.Content(index, 9, item.BahanBaku);
                    Excel_Class.Content(index, 10, item.Satuan);
                    Excel_Class.Content(index, 11, item.Kategori);
                    Excel_Class.Content(index, 12, item.Datang);
                    Excel_Class.Content(index, 13, item.Diterima);
                    Excel_Class.Content(index, 14, item.TolakKeSupplier);
                    Excel_Class.Content(index, 15, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                KodeBahanBaku = item.Key.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.Key.TBBahanBaku),
                Datang = item.Sum(x => x.Datang).ToFormatHarga(),
                Diterima = item.Sum(x => x.Diterima).ToFormatHarga(),
                TolakKeSupplier = item.Sum(x => x.TolakKeSupplier).ToFormatHarga(),
                Subtotal = item.Sum(x => x.SubtotalHPP + x.SubtotalHargaSupplier).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Penerimaan " + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "BahanBaku") + " Detail", Periode, 10);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Bahan Baku");
                Excel_Class.Header(4, "Satuan");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Datang");
                Excel_Class.Header(7, "Diterima");
                Excel_Class.Header(8, "Tolak Supplier");
                Excel_Class.Header(9, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeBahanBaku);
                    Excel_Class.Content(index, 3, item.BahanBaku);
                    Excel_Class.Content(index, 4, item.Satuan);
                    Excel_Class.Content(index, 5, item.Kategori);
                    Excel_Class.Content(index, 6, item.Datang);
                    Excel_Class.Content(index, 7, item.Diterima);
                    Excel_Class.Content(index, 8, item.TolakKeSupplier);
                    Excel_Class.Content(index, 9, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("TotalDatang", Data.Sum(item => item.Datang).ToFormatHarga());
        Result.Add("TotalDiterima", Data.Sum(item => item.Diterima).ToFormatHarga());
        Result.Add("TotalTolakKeSupplier", Data.Sum(item => item.TolakKeSupplier).ToFormatHarga());
        Result.Add("Subtotal", Data.Sum(item => item.SubtotalHPP + item.SubtotalHargaSupplier).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> PenerimaanPOProduksiProduk(int idTempat, string idPenerimaanPOProduksiProduk, string idPOProduksiProduk, int enumJenisPOProduksi, int idVendor, int idPengguna, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPenerimaanPOProduksiProduks
            .Where(item =>
                item.TanggalDatang.Date >= tanggalAwal &&
                item.TanggalDatang.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPenerimaanPOProduksiProduk))
            Data = Data.Where(item => item.IDPenerimaanPOProduksiProduk.Contains(idPenerimaanPOProduksiProduk)).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiProduk))
            Data = Data.Where(item => item.IDPOProduksiProduk.Contains(idPOProduksiProduk)).ToArray();

        if (enumJenisPOProduksi != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.EnumJenisProduksi == enumJenisPOProduksi).ToArray();

        if (idVendor != 0)
            Data = Data.Where(item => item.TBPOProduksiProduk.IDVendor == idVendor).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPenggunaDatang == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.Contains(keterangan)).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiProduk=" + idPOProduksiProduk;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPOProduksi;
        tempPencarian += "&IDVendor=" + idVendor;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPenerimaanPOProduksiProduk,
            item.Nomor,
            item.IDPOProduksiProduk,
            item.TBPOProduksiProduk.EnumJenisProduksi,
            Tempat = item.TBPOProduksiProduk.TBTempat.Nama,
            Vendor = item.TBPOProduksiProduk.IDVendor != null ? item.TBPOProduksiProduk.TBVendor.Nama : string.Empty,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.TanggalDatang.ToFormatTanggalJam(),
            TotalDatang = item.TotalDatang.ToFormatHargaBulat(),
            TotalDiterima = item.TotalDiterima.ToFormatHargaBulat(),
            TotalTolakKeVendor = item.TotalTolakKeVendor.ToFormatHargaBulat(),
            TotalTolakKeGudang = item.TotalTolakKeGudang.ToFormatHargaBulat(),
            Grandtotal = item.Grandtotal.ToFormatHarga(),
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Penerimaan " + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "Produk"), Periode, 13);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Penerimaan");
            Excel_Class.Header(3, "ID PO");
            Excel_Class.Header(4, "Tempat");
            Excel_Class.Header(5, "Vendor");
            Excel_Class.Header(6, "Pegawai");
            Excel_Class.Header(7, "Tanggal");
            Excel_Class.Header(8, "Total Datang");
            Excel_Class.Header(9, "Total Diterima");
            Excel_Class.Header(10, "Tolak Supplier");
            Excel_Class.Header(11, "Tolak Gudang");
            Excel_Class.Header(12, "Grandtotal");
            Excel_Class.Header(13, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPenerimaanPOProduksiProduk);
                Excel_Class.Content(index, 3, item.IDPOProduksiProduk);
                Excel_Class.Content(index, 4, item.Tempat);
                Excel_Class.Content(index, 5, item.Vendor);
                Excel_Class.Content(index, 6, item.Pengguna);
                Excel_Class.Content(index, 7, item.Tanggal);
                Excel_Class.Content(index, 8, item.TotalDatang);
                Excel_Class.Content(index, 9, item.TotalDiterima);
                Excel_Class.Content(index, 10, item.TotalTolakKeVendor);
                Excel_Class.Content(index, 11, item.TotalTolakKeGudang);
                Excel_Class.Content(index, 12, item.Grandtotal);
                Excel_Class.Content(index, 13, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("TotalDatang", Data.Sum(item => item.TotalDatang).ToFormatHargaBulat());
        Result.Add("TotalDiterima", Data.Sum(item => item.TotalDiterima).ToFormatHargaBulat());
        Result.Add("TotalTolakKeVendor", Data.Sum(item => item.TotalTolakKeVendor).ToFormatHargaBulat());
        Result.Add("TotalTolakKeGudang", Data.Sum(item => item.TotalTolakKeGudang).ToFormatHargaBulat());
        Result.Add("Grandtotal", Data.Sum(item => item.Grandtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> PenerimaanPOProduksiProdukDetail(int idTempat, string idPenerimaanPOProduksiProduk, string idPOProduksiProduk, int enumJenisPOProduksi, int idVendor, int idPengguna, string kode, int idProduk, int idAtributProduk, int idKategoriProduk, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPenerimaanPOProduksiProdukDetails
            .Where(item =>
                item.TBPenerimaanPOProduksiProduk.TanggalDatang.Date >= tanggalAwal &&
                item.TBPenerimaanPOProduksiProduk.TanggalDatang.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPenerimaanPOProduksiProduk))
            Data = Data.Where(item => item.IDPenerimaanPOProduksiProduk.Contains(idPenerimaanPOProduksiProduk)).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiProduk))
            Data = Data.Where(item => item.TBPenerimaanPOProduksiProduk.IDPOProduksiProduk.Contains(idPOProduksiProduk)).ToArray();

        if (enumJenisPOProduksi != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.EnumJenisProduksi == enumJenisPOProduksi).ToArray();

        if (idVendor != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDVendor == idVendor).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.TBPenerimaanPOProduksiProduk.IDPenggunaDatang == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(kode)).ToArray();

        if (idProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        if (idKategoriProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == idKategoriProduk) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDVendor=" + idVendor;
        tempPencarian += "&IDPenerimaanPOProduksiProduk=" + idPenerimaanPOProduksiProduk;
        tempPencarian += "&IDPOProduksiProduk=" + idPOProduksiProduk;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPOProduksi;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDKategoriProduk=" + idKategoriProduk;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDPenerimaanPOProduksiProduk,
                item.TBPenerimaanPOProduksiProduk.IDPOProduksiProduk,
                Tempat = item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.TBTempat.Nama,
                Vendor = item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDVendor != null ? item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.TBVendor.Nama : string.Empty,
                Pengguna = item.TBPenerimaanPOProduksiProduk.TBPengguna.NamaLengkap,
                Tanggal = item.TBPenerimaanPOProduksiProduk.TanggalDatang.ToFormatTanggalJam(),
                item.TBKombinasiProduk.KodeKombinasiProduk,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.TBKombinasiProduk),
                Datang = item.Datang.ToFormatHargaBulat(),
                Diterima = item.Diterima.ToFormatHargaBulat(),
                TolakKeVendor = item.TolakKeVendor.ToFormatHargaBulat(),
                TolakKeGudang = item.TolakKeGudang.ToFormatHargaBulat(),
                Subtotal = (item.SubtotalHPP + item.SubtotalHargaVendor).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Penerimaan " + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "Produk") + " Detail", Periode, 16);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID Penerimaan");
                Excel_Class.Header(3, "ID PO");
                Excel_Class.Header(4, "Tempat");
                Excel_Class.Header(5, "Vendor");
                Excel_Class.Header(6, "Pengguna");
                Excel_Class.Header(7, "Tanggal");
                Excel_Class.Header(8, "Kode");
                Excel_Class.Header(9, "Produk");
                Excel_Class.Header(10, "Varian");
                Excel_Class.Header(11, "Kategori");
                Excel_Class.Header(12, "Datang");
                Excel_Class.Header(13, "Diterima");
                Excel_Class.Header(14, "Tolak Vendor");
                Excel_Class.Header(15, "Tolak Gudang");
                Excel_Class.Header(16, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDPenerimaanPOProduksiProduk);
                    Excel_Class.Content(index, 3, item.IDPOProduksiProduk);
                    Excel_Class.Content(index, 4, item.Tempat);
                    Excel_Class.Content(index, 5, item.Vendor);
                    Excel_Class.Content(index, 6, item.Pengguna);
                    Excel_Class.Content(index, 7, item.Tanggal);
                    Excel_Class.Content(index, 8, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 9, item.Produk);
                    Excel_Class.Content(index, 10, item.AtributProduk);
                    Excel_Class.Content(index, 11, item.Kategori);
                    Excel_Class.Content(index, 12, item.Datang);
                    Excel_Class.Content(index, 13, item.Diterima);
                    Excel_Class.Content(index, 14, item.TolakKeVendor);
                    Excel_Class.Content(index, 15, item.TolakKeGudang);
                    Excel_Class.Content(index, 16, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBKombinasiProduk,
            })
            .Select(item => new
            {
                KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                Datang = item.Sum(x => x.Datang).ToFormatHargaBulat(),
                Diterima = item.Sum(x => x.Diterima).ToFormatHargaBulat(),
                TolakKeVendor = item.Sum(x => x.TolakKeVendor).ToFormatHargaBulat(),
                TolakKeGudang = item.Sum(x => x.TolakKeGudang).ToFormatHargaBulat(),
                Subtotal = item.Sum(x => x.SubtotalHPP + x.SubtotalHargaVendor).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Penerimaan " + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "Produk") + " Detail", Periode, 10);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Produk");
                Excel_Class.Header(4, "Varian");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Datang");
                Excel_Class.Header(7, "Diterima");
                Excel_Class.Header(8, "Tolak Vendor");
                Excel_Class.Header(9, "Tolak Gudang");
                Excel_Class.Header(10, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 3, item.Produk);
                    Excel_Class.Content(index, 4, item.AtributProduk);
                    Excel_Class.Content(index, 5, item.Kategori);
                    Excel_Class.Content(index, 6, item.Datang);
                    Excel_Class.Content(index, 7, item.Diterima);
                    Excel_Class.Content(index, 8, item.TolakKeVendor);
                    Excel_Class.Content(index, 9, item.TolakKeGudang);
                    Excel_Class.Content(index, 10, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("TotalDatang", Data.Sum(item => item.Datang).ToFormatHargaBulat());
        Result.Add("TotalDiterima", Data.Sum(item => item.Diterima).ToFormatHargaBulat());
        Result.Add("TotalTolakKeVendor", Data.Sum(item => item.TolakKeVendor).ToFormatHargaBulat());
        Result.Add("TotalTolakKeGudang", Data.Sum(item => item.TolakKeGudang).ToFormatHargaBulat());
        Result.Add("Subtotal", Data.Sum(item => item.SubtotalHPP + item.SubtotalHargaVendor).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> PemakaianBahanBakuProduksiBahanBaku(int idTempat, string kode, int idBahanBaku, int idSatuan, int idKategoriBahanBaku, bool retail)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPerpindahanStokBahanBakus
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idBahanBaku != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.IDSatuan == idSatuan).ToArray();

        if (idKategoriBahanBaku != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategoriBahanBaku) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idBahanBaku;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&IDKategoriBahanBaku=" + idKategoriBahanBaku;

        if (retail == true)
        {
            var DataResult = Data.Where(item => item.IDJenisPerpindahanStok == 13 && item.IDJenisPerpindahanStok == 21).GroupBy(item => new
            {
                item.TBTempat,
                item.TBStokBahanBaku.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                Tempat = item.Key.TBTempat.Nama,
                KodeBahanBaku = item.Key.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.Key.TBBahanBaku),
                Jumlah = (item.Where(x => x.IDJenisPerpindahanStok == 21).Sum(x => x.Jumlah) - item.Where(x => x.IDJenisPerpindahanStok == 13).Sum(x => x.Jumlah)).ToFormatHarga(),
                Subtotal = (item.Where(x => x.IDJenisPerpindahanStok == 21).Sum(x => x.HargaBeli * x.Jumlah) - item.Where(x => x.IDJenisPerpindahanStok == 13).Sum(x => x.HargaBeli * x.Jumlah)).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Pemakaian Bahan Baku dari Produksi Bahan Baku", Periode, 7);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Bahan Baku");
                Excel_Class.Header(4, "Satuan");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Jumlah");
                Excel_Class.Header(7, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeBahanBaku);
                    Excel_Class.Content(index, 3, item.BahanBaku);
                    Excel_Class.Content(index, 4, item.Satuan);
                    Excel_Class.Content(index, 5, item.Kategori);
                    Excel_Class.Content(index, 6, item.Jumlah);
                    Excel_Class.Content(index, 7, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            Result.Add("TotalSubtotalKirim", Data.Sum(item => item.HargaBeli * item.Jumlah).ToFormatHarga());
        }
        else
        {

        }

        return Result;
    }
    public Dictionary<string, dynamic> PemakaianBahanBakuProduksiProduk(int idTempat, string kode, int idBahanBaku, int idSatuan, int idKategoriBahanBaku, bool retail)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPengirimanPOProduksiProdukDetails
            .Where(item =>
                item.TBPengirimanPOProduksiProduk.Tanggal.Date >= tanggalAwal &&
                item.TBPengirimanPOProduksiProduk.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.TBPengirimanPOProduksiProduk.TBPOProduksiProduk.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDSatuan == idSatuan).ToArray();

        if (idKategoriBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategoriBahanBaku) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idBahanBaku;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&IDKategoriBahanBaku=" + idKategoriBahanBaku;

        if (retail == true)
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBPengirimanPOProduksiProduk.TBPOProduksiProduk.TBTempat,
                item.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                Tempat = item.Key.TBTempat.Nama,
                KodeBahanBaku = item.Key.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.Key.TBBahanBaku),
                Jumlah = item.Sum(x => x.Kirim).ToFormatHarga(),
                Subtotal = item.Sum(x => x.SubtotalKirim).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Pemakaian Bahan Baku dari Produksi Produk", Periode, 7);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Bahan Baku");
                Excel_Class.Header(4, "Satuan");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Jumlah");
                Excel_Class.Header(7, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeBahanBaku);
                    Excel_Class.Content(index, 3, item.BahanBaku);
                    Excel_Class.Content(index, 4, item.Satuan);
                    Excel_Class.Content(index, 5, item.Kategori);
                    Excel_Class.Content(index, 6, item.Jumlah);
                    Excel_Class.Content(index, 7, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            Result.Add("TotalSubtotalKirim", Data.Sum(item => item.SubtotalKirim).ToFormatHarga());
        }
        else
        {
            var DataBatal = Data.GroupBy(item => new
            {
                item.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                IDBahanBaku = item.Key.TBBahanBaku.IDBahanBaku,
                IDSatuan = item.Key.TBSatuan.IDSatuan,
                Jumlah = item.Sum(x => x.Kirim),
                Subtotal = item.Sum(x => x.SubtotalKirim)
            });

            var DataResult = Data.GroupBy(item => new
            {
                item.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                KodeBahanBaku = item.Key.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.Key.TBBahanBaku),
                Jumlah = (item.Sum(x => x.Kirim) - (DataBatal.FirstOrDefault(data => data.IDBahanBaku == item.Key.TBBahanBaku.IDBahanBaku && data.IDSatuan == item.Key.TBSatuan.IDSatuan).Jumlah)).ToFormatHarga(),
                Subtotal = (item.Sum(x => x.SubtotalKirim) - (DataBatal.FirstOrDefault(data => data.IDBahanBaku == item.Key.TBBahanBaku.IDBahanBaku && data.IDSatuan == item.Key.TBSatuan.IDSatuan).Subtotal)).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);
        }

        return Result;
    }
    public Dictionary<string, dynamic> PembuanganBahanBakuDetail(int idTempat, int idPengguna, string kode, int idBahanBaku, int idSatuan, int idKategori, string keterangan, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPerpindahanStokBahanBakus
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir &&
                item.IDJenisPerpindahanStok == (int)EnumJenisPerpindahanStok.PembuanganBarangRusak)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idBahanBaku != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.IDBahanBaku == idBahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.IDSatuan == idSatuan).ToArray();

        if (idKategori != 0)
            Data = Data.Where(item => item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategori) != null).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idBahanBaku;
        tempPencarian += "&IDKategori=" + idKategori;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&Keterangan=" + keterangan;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                OrderTanggal = item.Tanggal,
                Tanggal = item.Tanggal.ToFormatTanggalJam(),
                Tempat = item.TBTempat.Nama,
                Pengguna = item.TBPengguna.NamaLengkap,
                KodeBahanBaku = item.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBStokBahanBaku.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(item.TBStokBahanBaku, null),
                Jenis = item.TBJenisPerpindahanStok.Nama,
                HargaBeli = item.HargaBeli.ToFormatHarga(),
                Status = item.TBJenisPerpindahanStok.Status,
                Jumlah = (item.TBJenisPerpindahanStok.Status.Value ? item.Jumlah : item.Jumlah * -1).ToFormatHarga(),
                Subtotal = (item.Jumlah * item.HargaBeli).ToFormatHarga(),
                item.Keterangan
            }).OrderByDescending(item => item.OrderTanggal);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Pembuangan Bahan Baku Detail", Periode, 13);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Tanggal");
                Excel_Class.Header(3, "Tempat");
                Excel_Class.Header(4, "Pegawai");
                Excel_Class.Header(5, "Kode");
                Excel_Class.Header(6, "Bahan Baku");
                Excel_Class.Header(7, "Satuan");
                Excel_Class.Header(8, "Kategori");
                Excel_Class.Header(9, "Jenis");
                Excel_Class.Header(10, "HargaBeli");
                Excel_Class.Header(11, "Jumlah");
                Excel_Class.Header(12, "Subtotal");
                Excel_Class.Header(13, "Keterangan");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Tanggal);
                    Excel_Class.Content(index, 3, item.Tempat);
                    Excel_Class.Content(index, 4, item.Pengguna);
                    Excel_Class.Content(index, 5, item.KodeBahanBaku);
                    Excel_Class.Content(index, 6, item.BahanBaku);
                    Excel_Class.Content(index, 7, item.Satuan);
                    Excel_Class.Content(index, 8, item.Kategori);
                    Excel_Class.Content(index, 9, item.Jenis);
                    Excel_Class.Content(index, 10, item.HargaBeli);
                    Excel_Class.Content(index, 11, item.Jumlah);
                    Excel_Class.Content(index, 12, item.Subtotal);
                    Excel_Class.Content(index, 13, item.Keterangan);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBStokBahanBaku,
                item.TBJenisPerpindahanStok,
                item.TBSatuan
            }).Select(item => new
            {
                Tempat = item.Key.TBStokBahanBaku.TBTempat.Nama,
                KodeBahanBaku = item.Key.TBStokBahanBaku.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBStokBahanBaku.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(item.Key.TBStokBahanBaku, null),
                Jenis = item.Key.TBJenisPerpindahanStok.Nama,
                Status = item.Key.TBJenisPerpindahanStok.Status,
                Jumlah = item.Sum(x => item.Key.TBJenisPerpindahanStok.Status.Value ? x.Jumlah : x.Jumlah * -1).ToFormatHarga(),
                Subtotal = item.Sum(x => x.Jumlah * x.HargaBeli).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Pembuangan Bahan Baku", Periode, 9);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Tempat");
                Excel_Class.Header(3, "Kode");
                Excel_Class.Header(4, "Bahan Baku");
                Excel_Class.Header(5, "Satuan");
                Excel_Class.Header(6, "Ketegori");
                Excel_Class.Header(7, "Jenis");
                Excel_Class.Header(8, "Jumlah");
                Excel_Class.Header(9, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Tempat);
                    Excel_Class.Content(index, 3, item.KodeBahanBaku);
                    Excel_Class.Content(index, 4, item.BahanBaku);
                    Excel_Class.Content(index, 5, item.Satuan);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.Jenis);
                    Excel_Class.Content(index, 8, item.Jumlah);
                    Excel_Class.Content(index, 9, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("Jumlah", Data.Sum(item => item.TBJenisPerpindahanStok.Status.Value ? item.Jumlah : item.Jumlah * -1).ToFormatHarga());
        Result.Add("Subtotal", Data.Sum(item => item.Jumlah * item.HargaBeli).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiBahanBakuRetur(int idTempat, string idPOProduksiBahanBakuRetur, int idSupplier, int idPengguna, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiBahanBakuReturs
            .Where(item =>
                item.TanggalRetur.Value.Date >= tanggalAwal &&
                item.TanggalRetur.Value.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiBahanBakuRetur))
            Data = Data.Where(item => item.IDPOProduksiBahanBakuRetur.Contains(idPOProduksiBahanBakuRetur)).ToArray();

        if (idSupplier != 0)
            Data = Data.Where(item => item.IDSupplier == idSupplier).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiBahanBakuRetur=" + idPOProduksiBahanBakuRetur;
        tempPencarian += "&IDSupplier=" + idSupplier;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPOProduksiBahanBakuRetur,
            item.Nomor,
            Tempat = item.TBTempat.Nama,
            Supplier = item.TBSupplier.Nama,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.TanggalRetur.Value.ToFormatTanggalJam(),
            EnumStatusRetur = item.EnumStatusRetur,
            Status = Pengaturan.StatusPOProduksi(item.EnumStatusRetur.ToString()),
            Grandtotal = item.Grandtotal.ToFormatHarga(),
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Retur Bahan Baku", Periode, 9);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Retur");
            Excel_Class.Header(3, "Tempat");
            Excel_Class.Header(4, "Supplier");
            Excel_Class.Header(5, "Pegawai");
            Excel_Class.Header(6, "Tanggal");
            Excel_Class.Header(7, "Status");
            Excel_Class.Header(8, "Grandtotal");
            Excel_Class.Header(9, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPOProduksiBahanBakuRetur);
                Excel_Class.Content(index, 3, item.Tempat);
                Excel_Class.Content(index, 4, item.Supplier);
                Excel_Class.Content(index, 5, item.Pengguna);
                Excel_Class.Content(index, 6, item.Tanggal);
                Excel_Class.Content(index, 7, item.Status);
                Excel_Class.Content(index, 8, item.Grandtotal);
                Excel_Class.Content(index, 9, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Grandtotal", Data.Sum(item => item.Grandtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> POProduksiBahanBakuPenagihan(int idTempat, string idPOProduksiBahanBakuPenagihan, int idSupplier, int idPengguna, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiBahanBakuPenagihans
            .Where(item =>
                item.Tanggal.Date >= tanggalAwal &&
                item.Tanggal.Date <= tanggalAkhir)
            .ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (!string.IsNullOrEmpty(idPOProduksiBahanBakuPenagihan))
            Data = Data.Where(item => item.IDPOProduksiBahanBakuPenagihan.Contains(idPOProduksiBahanBakuPenagihan)).ToArray();

        if (idSupplier != 0)
            Data = Data.Where(item => item.IDSupplier == idSupplier).ToArray();

        if (idPengguna != 0)
            Data = Data.Where(item => item.IDPengguna == idPengguna).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDPOProduksiBahanBakuPenagihan=" + idPOProduksiBahanBakuPenagihan;
        tempPencarian += "&IDSupplier=" + idSupplier;
        tempPencarian += "&IDPengguna=" + idPengguna;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDPOProduksiBahanBakuPenagihan,
            item.Nomor,
            Tempat = item.TBTempat.Nama,
            Supplier = item.TBSupplier.Nama,
            Pengguna = item.TBPengguna.NamaLengkap,
            Tanggal = item.Tanggal.ToFormatTanggalJam(),
            TotalPenerimaan = item.TotalPenerimaan.ToFormatHarga(),
            TotalRetur = item.TotalRetur.ToFormatHarga(),
            TotalDownPayment = item.TotalDownPayment.ToFormatHarga(),
            TotalTagihan = item.TotalTagihan.ToFormatHarga(),
            TotalBayar = item.TotalBayar.ToFormatHarga(),
            StatusPembayaran = item.StatusPembayaran,
            Status = item.StatusPembayaran == false ? "<label class=\"label label-warning\">Tagihan</label>" : "<label class=\"label label-success\">Lunas</label>",
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Penagihan PO Bahan Baku", Periode, 13);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Retur");
            Excel_Class.Header(3, "Tempat");
            Excel_Class.Header(4, "Supplier");
            Excel_Class.Header(5, "Pegawai");
            Excel_Class.Header(6, "Tanggal");
            Excel_Class.Header(7, "TotalPenerimaan");
            Excel_Class.Header(8, "TotalRetur");
            Excel_Class.Header(9, "TotalDownPayment");
            Excel_Class.Header(10, "TotalTagihan");
            Excel_Class.Header(11, "TotalBayar");
            Excel_Class.Header(12, "Status");
            Excel_Class.Header(13, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDPOProduksiBahanBakuPenagihan);
                Excel_Class.Content(index, 3, item.Tempat);
                Excel_Class.Content(index, 4, item.Supplier);
                Excel_Class.Content(index, 5, item.Pengguna);
                Excel_Class.Content(index, 6, item.Tanggal);
                Excel_Class.Content(index, 7, item.TotalPenerimaan);
                Excel_Class.Content(index, 8, item.TotalRetur);
                Excel_Class.Content(index, 9, item.TotalDownPayment);
                Excel_Class.Content(index, 10, item.TotalTagihan);
                Excel_Class.Content(index, 11, item.TotalBayar);
                Excel_Class.Content(index, 12, item.Status);
                Excel_Class.Content(index, 13, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }

    public Dictionary<string, dynamic> TransferProduk(string idTransfer, int idTempatPengirim, int idPengirim, int idTempatPenerima, int idPenerima, int enumStatusTransfer, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransferProduks
            .Where(item =>
                item.TanggalKirim.Date >= tanggalAwal &&
                item.TanggalKirim.Date <= tanggalAkhir)
            .ToArray();

        if (idTempatPengirim != 0)
            Data = Data.Where(item => item.IDTempatPengirim == idTempatPengirim).ToArray();

        if (idTempatPenerima != 0)
            Data = Data.Where(item => item.IDTempatPenerima == idTempatPenerima).ToArray();

        if (!string.IsNullOrEmpty(idTransfer))
            Data = Data.Where(item => item.IDTransferProduk.Contains(idTransfer)).ToArray();

        if (idPengirim != 0)
            Data = Data.Where(item => item.IDPengirim == idPengirim).ToArray();

        if (idPenerima != 0)
            Data = Data.Where(item => item.IDPenerima == idPenerima).ToArray();

        if (enumStatusTransfer != 0)
            Data = Data.Where(item => item.EnumJenisTransfer == enumStatusTransfer).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTransfer=" + idTransfer;
        tempPencarian += "&IDTempatPengirim=" + idTempatPengirim;
        tempPencarian += "&IDPengirim=" + idPengirim;
        tempPencarian += "&IDTempatPenerima=" + idTempatPenerima;
        tempPencarian += "&IDPenerima=" + idPenerima;
        tempPencarian += "&EnumStatusTransfer=" + enumStatusTransfer;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDTransferProduk,
            item.Nomor,
            TempatPengirim = item.TBTempat.Nama,
            Pengirim = item.TBPengguna.NamaLengkap,
            TanggalKirim = item.TanggalKirim.ToFormatTanggalJam(),
            TempatPenerima = item.TBTempat1.Nama,
            Penerima = item.IDPenerima != null ? item.TBPengguna1.NamaLengkap : string.Empty,
            TanggalTerima = item.TanggalTerima.ToFormatTanggalJam(),
            TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
            GrandtotalHargaBeli = item.GrandTotalHargaBeli.ToFormatHarga(),
            GrandtotalHargaJual = item.GrandTotalHargaJual.ToFormatHarga(),
            Status = Pengaturan.StatusTransfer(item.EnumJenisTransfer),
            item.EnumJenisTransfer,
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Transfer Produk", Periode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Transfer");
            Excel_Class.Header(3, "Tempat Pengirim");
            Excel_Class.Header(4, "Pengirim");
            Excel_Class.Header(5, "Tanggal Kirim");
            Excel_Class.Header(6, "Tempat Penerima");
            Excel_Class.Header(7, "Penerima");
            Excel_Class.Header(8, "Tanggal Terima");
            Excel_Class.Header(9, "Jumlah");
            Excel_Class.Header(10, "Grandtotal");
            Excel_Class.Header(11, "Status");
            Excel_Class.Header(12, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransferProduk);
                Excel_Class.Content(index, 3, item.TempatPengirim);
                Excel_Class.Content(index, 4, item.Pengirim);
                Excel_Class.Content(index, 5, item.TanggalKirim);
                Excel_Class.Content(index, 6, item.TempatPenerima);
                Excel_Class.Content(index, 7, item.Penerima);
                Excel_Class.Content(index, 8, item.TanggalTerima);
                Excel_Class.Content(index, 9, item.TotalJumlah);
                Excel_Class.Content(index, 10, item.GrandtotalHargaJual);
                Excel_Class.Content(index, 11, item.Status);
                Excel_Class.Content(index, 12, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Jumlah", Data.Sum(item => item.TotalJumlah).ToFormatHargaBulat());
        Result.Add("GrandTotalHargaBeli", Data.Sum(item => item.GrandTotalHargaBeli).ToFormatHarga());
        Result.Add("GrandTotalHargaJual", Data.Sum(item => item.GrandTotalHargaJual).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> TransferProdukDetail(string idTransfer, int idTempatPengirim, int idPengirim, int idTempatPenerima, int idPenerima, int enumStatusTransfer, string kode, int idPemilikProduk, int idProduk, int idAtributProduk, int idKategoriProduk, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransferProdukDetails
            .Where(item =>
                item.TBTransferProduk.TanggalKirim.Date >= tanggalAwal &&
                item.TBTransferProduk.TanggalKirim.Date <= tanggalAkhir)
            .ToArray();

        if (idTempatPengirim != 0)
            Data = Data.Where(item => item.TBTransferProduk.IDTempatPengirim == idTempatPengirim).ToArray();

        if (idTempatPenerima != 0)
            Data = Data.Where(item => item.TBTransferProduk.IDTempatPenerima == idTempatPenerima).ToArray();

        if (!string.IsNullOrEmpty(idTransfer))
            Data = Data.Where(item => item.IDTransferProduk.Contains(idTransfer)).ToArray();

        if (idPengirim != 0)
            Data = Data.Where(item => item.TBTransferProduk.IDPengirim == idPengirim).ToArray();

        if (idPenerima != 0)
            Data = Data.Where(item => item.TBTransferProduk.IDPenerima == idPenerima).ToArray();

        if (enumStatusTransfer != 0)
            Data = Data.Where(item => item.TBTransferProduk.EnumJenisTransfer == enumStatusTransfer).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(kode)).ToArray();

        if (idPemilikProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == idPemilikProduk).ToArray();

        if (idProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDProduk == idProduk).ToArray();

        if (idAtributProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.IDAtributProduk == idAtributProduk).ToArray();

        if (idKategoriProduk != 0)
            Data = Data.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == idKategoriProduk) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTransferProduk=" + idTransfer;
        tempPencarian += "&IDTempatPengirim=" + idTempatPengirim;
        tempPencarian += "&IDPengirim=" + idPengirim;
        tempPencarian += "&IDTempatPenerima=" + idTempatPenerima;
        tempPencarian += "&IDPenerima=" + idPenerima;
        tempPencarian += "&EnumStatusTransfer=" + enumStatusTransfer;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDPemilikProduk=" + idPemilikProduk;
        tempPencarian += "&IDProduk=" + idProduk;
        tempPencarian += "&IDAtributProduk=" + idAtributProduk;
        tempPencarian += "&IDKategoriProduk=" + idKategoriProduk;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDTransferProduk,
                TempatPengirim = item.TBTransferProduk.TBTempat.Nama,
                Pengirim = item.TBTransferProduk.TBPengguna.NamaLengkap,
                TanggalKirim = item.TBTransferProduk.TanggalKirim.ToFormatTanggalJam(),
                TempatPenerima = item.TBTransferProduk.TBTempat1.Nama,
                Penerima = item.TBTransferProduk.IDPenerima != null ? item.TBTransferProduk.TBPengguna1.NamaLengkap : string.Empty,
                TanggalTerima = item.TBTransferProduk.TanggalTerima.ToFormatTanggalJam(),
                item.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.TBKombinasiProduk),
                Jumlah = item.Jumlah.ToFormatHargaBulat(),
                SubtotalHargaBeli = item.SubtotalHargaBeli.ToFormatHarga(),
                SubtotalHargaJual = item.SubtotalHargaJual.ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Transfer Produk Detail", Periode, 15);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID Transfer");
                Excel_Class.Header(3, "Tempat Pengirim");
                Excel_Class.Header(4, "Pengirim");
                Excel_Class.Header(5, "Tanggal Kirim");
                Excel_Class.Header(6, "Tempat Penerima");
                Excel_Class.Header(7, "Penerima");
                Excel_Class.Header(8, "Tanggal Terima");
                Excel_Class.Header(9, "Kode");
                Excel_Class.Header(10, "Brand");
                Excel_Class.Header(11, "Produk");
                Excel_Class.Header(12, "Varian");
                Excel_Class.Header(13, "Kategori");
                Excel_Class.Header(14, "Jumlah");
                Excel_Class.Header(15, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDTransferProduk);
                    Excel_Class.Content(index, 3, item.TempatPengirim);
                    Excel_Class.Content(index, 4, item.Pengirim);
                    Excel_Class.Content(index, 5, item.TanggalKirim);
                    Excel_Class.Content(index, 6, item.TempatPenerima);
                    Excel_Class.Content(index, 7, item.Penerima);
                    Excel_Class.Content(index, 8, item.TanggalTerima);
                    Excel_Class.Content(index, 9, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 10, item.PemilikProduk);
                    Excel_Class.Content(index, 11, item.Produk);
                    Excel_Class.Content(index, 12, item.AtributProduk);
                    Excel_Class.Content(index, 13, item.Kategori);
                    Excel_Class.Content(index, 14, item.Jumlah);
                    Excel_Class.Content(index, 15, item.SubtotalHargaJual);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBKombinasiProduk
            })
            .Select(item => new
            {
                KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                Warna = item.Key.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                Jumlah = item.Sum(x => x.Jumlah).ToFormatHargaBulat(),
                SubtotalHargaBeli = item.Sum(x => x.SubtotalHargaBeli).ToFormatHarga(),
                SubtotalHargaJual = item.Sum(x => x.SubtotalHargaJual).ToFormatHarga()
            }).OrderBy(item => item.Produk);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Transfer Produk Detail", Periode, 8);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Brand");
                Excel_Class.Header(4, "Produk");
                Excel_Class.Header(5, "Varian");
                Excel_Class.Header(6, "Kategori");
                Excel_Class.Header(7, "Jumlah");
                Excel_Class.Header(8, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeKombinasiProduk);
                    Excel_Class.Content(index, 3, item.PemilikProduk);
                    Excel_Class.Content(index, 4, item.Produk);
                    Excel_Class.Content(index, 5, item.AtributProduk);
                    Excel_Class.Content(index, 6, item.Kategori);
                    Excel_Class.Content(index, 7, item.Jumlah);
                    Excel_Class.Content(index, 8, item.SubtotalHargaJual);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("Jumlah", Data.Sum(item => item.Jumlah).ToFormatHargaBulat());
        Result.Add("SubtotalHargaBeli", Data.Sum(item => item.SubtotalHargaBeli).ToFormatHarga());
        Result.Add("SubtotalHargaJual", Data.Sum(item => item.SubtotalHargaJual).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> TransferBahanBaku(string idTransfer, int idTempatPengirim, int idPengirim, int idTempatPenerima, int idPenerima, int enumStatusTransfer, string keterangan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransferBahanBakus
            .Where(item =>
                item.TanggalKirim.Date >= tanggalAwal &&
                item.TanggalKirim.Date <= tanggalAkhir)
            .ToArray();

        if (idTempatPengirim != 0)
            Data = Data.Where(item => item.IDTempatPengirim == idTempatPengirim).ToArray();

        if (idTempatPenerima != 0)
            Data = Data.Where(item => item.IDTempatPenerima == idTempatPenerima).ToArray();

        if (!string.IsNullOrEmpty(idTransfer))
            Data = Data.Where(item => item.IDTransferBahanBaku.Contains(idTransfer)).ToArray();

        if (idPengirim != 0)
            Data = Data.Where(item => item.IDPengirim == idPengirim).ToArray();

        if (idPenerima != 0)
            Data = Data.Where(item => item.IDPenerima == idPenerima).ToArray();

        if (enumStatusTransfer != 0)
            Data = Data.Where(item => item.EnumJenisTransfer == enumStatusTransfer).ToArray();

        if (!string.IsNullOrEmpty(keterangan))
            Data = Data.Where(item => item.Keterangan.ToLower().Contains(keterangan.ToLower())).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTransfer=" + idTransfer;
        tempPencarian += "&IDTempatPengirim=" + idTempatPengirim;
        tempPencarian += "&IDPengirim=" + idPengirim;
        tempPencarian += "&IDTempatPenerima=" + idTempatPenerima;
        tempPencarian += "&IDPenerima=" + idPenerima;
        tempPencarian += "&EnumStatusTransfer=" + enumStatusTransfer;
        tempPencarian += "&Keterangan=" + keterangan;

        var DataResult = Data.Select(item => new
        {
            item.IDTransferBahanBaku,
            item.Nomor,
            TempatPengirim = item.TBTempat.Nama,
            Pengirim = item.TBPengguna.NamaLengkap,
            TanggalKirim = item.TanggalKirim.ToFormatTanggalJam(),
            TempatPenerima = item.TBTempat1.Nama,
            Penerima = item.IDPenerima != null ? item.TBPengguna1.NamaLengkap : string.Empty,
            TanggalTerima = item.TanggalTerima.ToFormatTanggalJam(),
            TotalJumlah = item.TotalJumlah.ToFormatHarga(),
            Grandtotal = item.GrandTotal.ToFormatHarga(),
            Status = Pengaturan.StatusTransfer(item.EnumJenisTransfer),
            item.EnumJenisTransfer,
            item.Keterangan
        }).OrderByDescending(item => item.Nomor);

        Result.Add("Data", DataResult);

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Transfer Produk", Periode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "ID Transfer");
            Excel_Class.Header(3, "Tempat Pengirim");
            Excel_Class.Header(4, "Pengirim");
            Excel_Class.Header(5, "Tanggal Kirim");
            Excel_Class.Header(6, "Tempat Penerima");
            Excel_Class.Header(7, "Penerima");
            Excel_Class.Header(8, "Tanggal Terima");
            Excel_Class.Header(9, "Jumlah");
            Excel_Class.Header(10, "Grandtotal");
            Excel_Class.Header(11, "Status");
            Excel_Class.Header(12, "Keterangan");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransferBahanBaku);
                Excel_Class.Content(index, 3, item.TempatPengirim);
                Excel_Class.Content(index, 4, item.Pengirim);
                Excel_Class.Content(index, 5, item.TanggalKirim);
                Excel_Class.Content(index, 6, item.TempatPenerima);
                Excel_Class.Content(index, 7, item.Penerima);
                Excel_Class.Content(index, 8, item.TanggalTerima);
                Excel_Class.Content(index, 9, item.TotalJumlah);
                Excel_Class.Content(index, 10, item.Grandtotal);
                Excel_Class.Content(index, 11, item.Status);
                Excel_Class.Content(index, 12, item.Keterangan);
                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Jumlah", Data.Sum(item => item.TotalJumlah).ToFormatHarga());
        Result.Add("GrandTotal", Data.Sum(item => item.GrandTotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> TransferBahanBakuDetail(string idTransfer, int idTempatPengirim, int idPengirim, int idTempatPenerima, int idPenerima, int enumStatusTransfer, string kode, int idbahanBaku, int idSatuan, int idKategoriBahanBaku, bool group)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransferBahanBakuDetails
            .Where(item =>
                item.TBTransferBahanBaku.TanggalKirim.Date >= tanggalAwal &&
                item.TBTransferBahanBaku.TanggalKirim.Date <= tanggalAkhir)
            .ToArray();

        if (idTempatPengirim != 0)
            Data = Data.Where(item => item.TBTransferBahanBaku.IDTempatPengirim == idTempatPengirim).ToArray();

        if (idTempatPenerima != 0)
            Data = Data.Where(item => item.TBTransferBahanBaku.IDTempatPenerima == idTempatPenerima).ToArray();

        if (!string.IsNullOrEmpty(idTransfer))
            Data = Data.Where(item => item.IDTransferBahanBaku.Contains(idTransfer)).ToArray();

        if (idPengirim != 0)
            Data = Data.Where(item => item.TBTransferBahanBaku.IDPengirim == idPengirim).ToArray();

        if (idPenerima != 0)
            Data = Data.Where(item => item.TBTransferBahanBaku.IDPenerima == idPenerima).ToArray();

        if (enumStatusTransfer != 0)
            Data = Data.Where(item => item.TBTransferBahanBaku.EnumJenisTransfer == enumStatusTransfer).ToArray();

        if (!string.IsNullOrEmpty(kode))
            Data = Data.Where(item => item.TBBahanBaku.KodeBahanBaku.Contains(kode)).ToArray();

        if (idbahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDBahanBaku == idbahanBaku).ToArray();

        if (idSatuan != 0)
            Data = Data.Where(item => item.TBBahanBaku.IDSatuan == idSatuan).ToArray();

        if (idKategoriBahanBaku != 0)
            Data = Data.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.IDKategoriBahanBaku == idKategoriBahanBaku) != null).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTransferProduk=" + idTransfer;
        tempPencarian += "&IDTempatPengirim=" + idTempatPengirim;
        tempPencarian += "&IDPengirim=" + idPengirim;
        tempPencarian += "&IDTempatPenerima=" + idTempatPenerima;
        tempPencarian += "&IDPenerima=" + idPenerima;
        tempPencarian += "&EnumStatusTransfer=" + enumStatusTransfer;
        tempPencarian += "&Kode=" + kode;
        tempPencarian += "&IDBahanBaku=" + idbahanBaku;
        tempPencarian += "&IDSatuan=" + idSatuan;
        tempPencarian += "&IDKategoriBahanBaku=" + idKategoriBahanBaku;

        if (group == false)
        {
            var DataResult = Data.Select(item => new
            {
                item.IDTransferBahanBaku,
                TempatPengirim = item.TBTransferBahanBaku.TBTempat.Nama,
                Pengirim = item.TBTransferBahanBaku.TBPengguna.NamaLengkap,
                TanggalKirim = item.TBTransferBahanBaku.TanggalKirim.ToFormatTanggalJam(),
                TempatPenerima = item.TBTransferBahanBaku.TBTempat1.Nama,
                Penerima = item.TBTransferBahanBaku.IDPenerima != null ? item.TBTransferBahanBaku.TBPengguna1.NamaLengkap : string.Empty,
                TanggalTerima = item.TBTransferBahanBaku.TanggalTerima.ToFormatTanggalJam(),
                item.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.TBBahanBaku),
                Jumlah = item.Jumlah.ToFormatHarga(),
                Subtotal = item.Subtotal.ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Transfer Bahan Baku Detail", Periode, 14);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "ID Transfer");
                Excel_Class.Header(3, "Tempat Pengirim");
                Excel_Class.Header(4, "Pengirim");
                Excel_Class.Header(5, "Tanggal Kirim");
                Excel_Class.Header(6, "Tempat Penerima");
                Excel_Class.Header(7, "Penerima");
                Excel_Class.Header(8, "Tanggal Terima");
                Excel_Class.Header(9, "Kode");
                Excel_Class.Header(10, "Bahan Baku");
                Excel_Class.Header(11, "Satuan");
                Excel_Class.Header(12, "Kategori");
                Excel_Class.Header(13, "Jumlah");
                Excel_Class.Header(14, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.IDTransferBahanBaku);
                    Excel_Class.Content(index, 3, item.TempatPengirim);
                    Excel_Class.Content(index, 4, item.Pengirim);
                    Excel_Class.Content(index, 5, item.TanggalKirim);
                    Excel_Class.Content(index, 6, item.TempatPenerima);
                    Excel_Class.Content(index, 7, item.Penerima);
                    Excel_Class.Content(index, 8, item.TanggalTerima);
                    Excel_Class.Content(index, 9, item.KodeBahanBaku);
                    Excel_Class.Content(index, 10, item.BahanBaku);
                    Excel_Class.Content(index, 11, item.Satuan);
                    Excel_Class.Content(index, 12, item.Kategori);
                    Excel_Class.Content(index, 13, item.Jumlah);
                    Excel_Class.Content(index, 14, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        else
        {
            var DataResult = Data.GroupBy(item => new
            {
                item.TBBahanBaku,
                item.TBSatuan
            })
            .Select(item => new
            {
                KodeBahanBaku = item.Key.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.Key.TBBahanBaku.Nama,
                Satuan = item.Key.TBSatuan.Nama,
                Kategori = GabungkanSemuaKategoriBahanBaku(null, item.Key.TBBahanBaku),
                Jumlah = item.Sum(x => x.Jumlah).ToFormatHarga(),
                Subtotal = item.Sum(x => x.Subtotal).ToFormatHarga()
            }).OrderBy(item => item.BahanBaku);

            Result.Add("Data", DataResult);

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Transfer Bahan Baku Detail", Periode, 7);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kode");
                Excel_Class.Header(3, "Bahan Baku");
                Excel_Class.Header(4, "Satuan");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Jumlah");
                Excel_Class.Header(7, "Subtotal");

                int index = 2;

                foreach (var item in DataResult)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.KodeBahanBaku);
                    Excel_Class.Content(index, 3, item.BahanBaku);
                    Excel_Class.Content(index, 4, item.Satuan);
                    Excel_Class.Content(index, 5, item.Kategori);
                    Excel_Class.Content(index, 6, item.Jumlah);
                    Excel_Class.Content(index, 7, item.Subtotal);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }

        Result.Add("Jumlah", Data.Sum(item => item.Jumlah).ToFormatHarga());
        Result.Add("Subtotal", Data.Sum(item => item.Subtotal).ToFormatHarga());

        return Result;
    }
    public Dictionary<string, dynamic> UmurProduk()
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

        var BatasBulanProduk = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.BatasBulanProduk).ToInt();

        var ListProduk = db.TBStokProduks
            .GroupBy(item => new
            {
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                TanggalDaftar = item.TBKombinasiProduk.TBProduk._TanggalInsert,
                JumlahHari = (DateTime.Now.Date - item.TBKombinasiProduk.TBProduk._TanggalInsert).Days
            })
            .Select(item => new
            {
                item.Key,
                JumlahStok = item.Sum(item2 => item2.Jumlah)
            })
            .Where(item =>
                item.JumlahStok != 0 &&
                item.Key.TanggalDaftar.AddMonths(BatasBulanProduk).Date <= DateTime.Now.Date)
            .OrderBy(item => item.Key.TanggalDaftar)
            .ToArray();

        //tempPencarian += "?TanggalAwal=" + tanggalAwal;
        //tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        Result.Add("Data", ListProduk);

        Result.Add("TotalJumlahStok", ListProduk.Sum(item => item.JumlahStok));

        //if (excel)
        //{
        //    #region EXCEL
        //    Excel_Class Excel_Class = new Excel_Class(pengguna, "Gross Profit Store", Periode, 9);
        //    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

        //    Excel_Class.Header(1, "No.");
        //    Excel_Class.Header(2, "Lokasi");
        //    Excel_Class.Header(3, "Quantity");
        //    Excel_Class.Header(4, "Sales Before Disc.");
        //    Excel_Class.Header(5, "Disc.");
        //    Excel_Class.Header(6, "Sales After Disc.");
        //    Excel_Class.Header(7, "COGS");
        //    Excel_Class.Header(8, "Gross Profit");
        //    Excel_Class.Header(9, "%");

        //    int index = 2;

        //    foreach (var item in ListPenjualan)
        //    {
        //        Excel_Class.Content(index, 1, index - 1);
        //        Excel_Class.Content(index, 2, item.Key.Nama);
        //        Excel_Class.Content(index, 3, item.Quantity);
        //        Excel_Class.Content(index, 4, item.BeforeDiscount);
        //        Excel_Class.Content(index, 5, item.Discount);
        //        Excel_Class.Content(index, 6, item.AfterDiscount.Value);
        //        Excel_Class.Content(index, 7, item.COGS);
        //        Excel_Class.Content(index, 8, item.GrossProfit.Value);
        //        Excel_Class.Content(index, 9, item.AfterDiscount.Value > 0 ? (item.GrossProfit.Value / item.AfterDiscount.Value * 100) : 0);

        //        index++;
        //    }

        //    Excel_Class.Save();

        //    linkDownload = Excel_Class.LinkDownload;
        //    #endregion
        //}

        return Result;
    }


    //====================== ERI CAKEP ==========================
    public Dictionary<string, dynamic> SisaPOProduksiBahanBaku(int idTempat, int idSupplier, string IDPOProduksiBahanBaku, int enumJenisPOProduksi, string statusSisa)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiBahanBakus.Where(item => item.Tanggal.Date >= tanggalAwal.Date &&
                    item.Tanggal.Date <= tanggalAkhir.Date).ToArray();

        if (enumJenisPOProduksi != 0)
            Data = Data.Where(item => item.EnumJenisProduksi == enumJenisPOProduksi).ToArray();

        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (idSupplier != 0)
            Data = Data.Where(item => item.IDSupplier == idSupplier).ToArray();

        if (!string.IsNullOrEmpty(IDPOProduksiBahanBaku))
            Data = Data.Where(item => item.IDPOProduksiBahanBaku.Contains(IDPOProduksiBahanBaku)).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDSupplier=" + idSupplier;
        tempPencarian += "&IDPOProduksiBahanBaku=" + IDPOProduksiBahanBaku;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPOProduksi;
        tempPencarian += "&StatusSisa=" + statusSisa;

        var DataResult = Data
            .Select(item => new
            {
                Link = Pengaturan.LinkJenisPOProduksi(enumJenisPOProduksi, "BahanBaku"),
                item.IDPOProduksiBahanBaku,
                item.Tanggal,
                Supplier = item.TBSupplier == null ? "" : item.TBSupplier.Nama,
                Tempat = item.TBTempat.Nama,
                CountBahanBaku = item.TBPOProduksiBahanBakuDetails.Where(x => (statusSisa == "Tersisa" ? x.Sisa > 0 : x.Sisa == 0)).Count(),
                BahanBaku = item.TBPOProduksiBahanBakuDetails.Where(x => (statusSisa == "Tersisa" ? x.Sisa > 0 : x.Sisa == 0))
                    .Select(item2 => new
                    {
                        item2.TBBahanBaku.KodeBahanBaku,
                        item2.TBBahanBaku.Nama,
                        Satuan = item2.TBSatuan.Nama,
                        Harga = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP : item2.TotalHargaSupplier,
                        item2.Jumlah,
                        item2.Sisa,
                        Terima = item2.Jumlah - item2.Sisa,
                        Subtotal = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP * item2.Sisa : item2.TotalHargaSupplier * item2.Sisa,
                    }).FirstOrDefault(),

                Detail = item.TBPOProduksiBahanBakuDetails.Where(x => (statusSisa == "Tersisa" ? x.Sisa > 0 : x.Sisa == 0)).Skip(1)
                    .Select(item2 => new
                    {
                        item2.TBBahanBaku.KodeBahanBaku,
                        item2.TBBahanBaku.Nama,
                        Satuan = item2.TBSatuan.Nama,
                        Harga = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP : item2.TotalHargaSupplier,
                        item2.Jumlah,
                        item2.Sisa,
                        Terima = item2.Jumlah - item2.Sisa,
                        Subtotal = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP * item2.Sisa : item2.TotalHargaSupplier * item2.Sisa,
                    }).ToArray()
            }).Where(item => item.BahanBaku != null).OrderBy(item => item.Tanggal).ToArray();

        Result.Add("Data", DataResult);

        if (excel)
        {
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Laporan Sisa PO Produksi (" + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "BahanBaku"), Periode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Tanggal");
            Excel_Class.Header(3, "ID");
            Excel_Class.Header(4, enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? "Tempat" : "Supplier");
            Excel_Class.Header(5, "Kode");
            Excel_Class.Header(6, "BahanBaku");
            Excel_Class.Header(7, "Satuan");
            Excel_Class.Header(8, "Harga");
            Excel_Class.Header(9, "Jumlah");
            Excel_Class.Header(10, "Terima");
            Excel_Class.Header(11, "Sisa");
            Excel_Class.Header(12, "Subtotal");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 3, item.IDPOProduksiBahanBaku);
                Excel_Class.Content(index, 2, item.Tanggal);
                Excel_Class.Content(index, 4, enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.Tempat : item.Supplier);
                Excel_Class.Content(index, 5, item.BahanBaku.KodeBahanBaku);
                Excel_Class.Content(index, 6, item.BahanBaku.Nama);
                Excel_Class.Content(index, 7, item.BahanBaku.Satuan);
                Excel_Class.Content(index, 8, item.BahanBaku.Harga);
                Excel_Class.Content(index, 9, item.BahanBaku.Jumlah);
                Excel_Class.Content(index, 10, item.BahanBaku.Terima);
                Excel_Class.Content(index, 11, item.BahanBaku.Sisa);
                Excel_Class.Content(index, 12, item.BahanBaku.Subtotal);

                index++;

                foreach (var item2 in item.Detail)
                {
                    Excel_Class.Content(index, 5, item2.KodeBahanBaku);
                    Excel_Class.Content(index, 6, item2.Nama);
                    Excel_Class.Content(index, 7, item2.Satuan);
                    Excel_Class.Content(index, 8, item2.Harga);
                    Excel_Class.Content(index, 9, item2.Jumlah);
                    Excel_Class.Content(index, 10, item2.Terima);
                    Excel_Class.Content(index, 11, item2.Sisa);
                    Excel_Class.Content(index, 12, item2.Subtotal);

                    index++;
                }
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
        }

        Result.Add("Subtotal", DataResult.Sum(item => item.Detail.Sum(item2 => item2.Subtotal) + item.BahanBaku.Subtotal).ToFormatHarga());

        return Result;
    }

    public Dictionary<string, dynamic> SisaPOProduksiProduk(int idTempat, int idVendor, string IDPOProduksiProduk, int enumJenisPOProduksi, string statusSisa)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBPOProduksiProduks.Where(item => item.Tanggal.Date >= tanggalAwal.Date &&
                    item.Tanggal.Date <= tanggalAkhir.Date &&
                    item.EnumJenisProduksi == enumJenisPOProduksi).ToArray();

        if (enumJenisPOProduksi != 0)
            Data = Data.Where(item => item.EnumJenisProduksi == enumJenisPOProduksi).ToArray();
        if (idTempat != 0)
            Data = Data.Where(item => item.IDTempat == idTempat).ToArray();

        if (idVendor != 0)
            Data = Data.Where(item => item.IDVendor == idVendor).ToArray();

        if (!string.IsNullOrEmpty(IDPOProduksiProduk))
            Data = Data.Where(item => item.IDPOProduksiProduk.Contains(IDPOProduksiProduk)).ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;
        tempPencarian += "&IDTempat=" + idTempat;
        tempPencarian += "&IDVendor=" + idVendor;
        tempPencarian += "&IDPOProduksiProduk=" + IDPOProduksiProduk;
        tempPencarian += "&EnumJenisProduksi=" + enumJenisPOProduksi;
        tempPencarian += "&StatusSisa=" + statusSisa;

        var DataResult = Data
            .Select(item => new
            {
                Link = Pengaturan.LinkJenisPOProduksi(enumJenisPOProduksi, "Produk"),
                item.IDPOProduksiProduk,
                item.Tanggal,
                Vendor = item.TBVendor == null ? "" : item.TBVendor.Nama,
                Tempat = item.TBTempat.Nama,
                CountProduk = item.TBPOProduksiProdukDetails.Where(x => (statusSisa == "Tersisa" ? x.Sisa > 0 : x.Sisa == 0)).Count(),
                Produk = item.TBPOProduksiProdukDetails.Where(x => (statusSisa == "Tersisa" ? x.Sisa > 0 : x.Sisa == 0))
                    .Select(item2 => new
                    {
                        item2.TBKombinasiProduk.KodeKombinasiProduk,
                        item2.TBKombinasiProduk.TBProduk.Nama,
                        AtributProduk = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                        Harga = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP : item2.TotalHargaVendor,
                        item2.Jumlah,
                        item2.Sisa,
                        Terima = item2.Jumlah - item2.Sisa,
                        Subtotal = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP * item2.Sisa : item2.TotalHargaVendor * item2.Sisa,
                    }).FirstOrDefault(),

                Detail = item.TBPOProduksiProdukDetails.Where(x => (statusSisa == "Tersisa" ? x.Sisa > 0 : x.Sisa == 0)).Skip(1)
                    .Select(item2 => new
                    {
                        item2.TBKombinasiProduk.KodeKombinasiProduk,
                        item2.TBKombinasiProduk.TBProduk.Nama,
                        AtributProduk = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                        Harga = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP : item2.TotalHargaVendor,
                        item2.Jumlah,
                        item2.Sisa,
                        Terima = item2.Jumlah - item2.Sisa,
                        Subtotal = enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item2.TotalHPP * item2.Sisa : item2.TotalHargaVendor * item2.Sisa,
                    }).ToArray()
            }).Where(item => item.Produk != null).OrderBy(item => item.Tanggal).ToArray();

        Result.Add("Data", DataResult);

        if (excel)
        {
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Laporan Sisa PO Produksi (" + Pengaturan.JenisPOProduksi(enumJenisPOProduksi, "Produk"), Periode, 12);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Tanggal");
            Excel_Class.Header(3, "ID");
            Excel_Class.Header(4, enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? "Tempat" : "Vendor");
            Excel_Class.Header(5, "Kode");
            Excel_Class.Header(6, "Produk");
            Excel_Class.Header(7, "Varian");
            Excel_Class.Header(8, "Harga");
            Excel_Class.Header(9, "Jumlah");
            Excel_Class.Header(10, "Terima");
            Excel_Class.Header(11, "Sisa");
            Excel_Class.Header(12, "Subtotal");

            int index = 2;

            foreach (var item in DataResult)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 3, item.IDPOProduksiProduk);
                Excel_Class.Content(index, 2, item.Tanggal);
                Excel_Class.Content(index, 4, enumJenisPOProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.Tempat : item.Vendor);
                Excel_Class.Content(index, 5, item.Produk.KodeKombinasiProduk);
                Excel_Class.Content(index, 6, item.Produk.Nama);
                Excel_Class.Content(index, 7, item.Produk.AtributProduk);
                Excel_Class.Content(index, 8, item.Produk.Harga);
                Excel_Class.Content(index, 9, item.Produk.Jumlah);
                Excel_Class.Content(index, 10, item.Produk.Terima);
                Excel_Class.Content(index, 11, item.Produk.Sisa);
                Excel_Class.Content(index, 12, item.Produk.Subtotal);

                index++;

                foreach (var item2 in item.Detail)
                {
                    Excel_Class.Content(index, 5, item2.KodeKombinasiProduk);
                    Excel_Class.Content(index, 6, item2.Nama);
                    Excel_Class.Content(index, 7, item2.AtributProduk);
                    Excel_Class.Content(index, 8, item2.Harga);
                    Excel_Class.Content(index, 9, item2.Jumlah);
                    Excel_Class.Content(index, 10, item2.Terima);
                    Excel_Class.Content(index, 11, item2.Sisa);
                    Excel_Class.Content(index, 12, item2.Subtotal);

                    index++;
                }
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
        }

        Result.Add("Subtotal", DataResult.Sum(item => item.Detail.Sum(item2 => item2.Subtotal) + item.Produk.Subtotal).ToFormatHarga());

        return Result;
    }
    //============ END ERI CAKEP ===================
}