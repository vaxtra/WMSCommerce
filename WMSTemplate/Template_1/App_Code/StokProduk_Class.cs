using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class StokProduk_Class
{
    private DataClassesDatabaseDataContext db;
    public StokProduk_Class(DataClassesDatabaseDataContext _db)
    {
        db = _db;
    }
    public bool UbahStatus(int idStokProduk)
    {
        var StokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == idStokProduk);

        if (StokProduk != null)
        {
            StokProduk.Status = !StokProduk.Status;
            db.SubmitChanges();

            return true;
        }
        else
            return false;
    }

    #region PENCARIAN PRODUK
    public TBStokProduk Cari(int idTempat, int idKombinasiProduk)
    {
        if (idTempat <= 0 || idKombinasiProduk <= 0)
            return null;

        //PENCARIAN STOK PRODUK BERDASARKAN IDTempat IDKombinasiProduk
        var StokProduk = db.TBStokProduks
            .FirstOrDefault(item =>
                item.IDTempat == idTempat &&
                item.IDKombinasiProduk == idKombinasiProduk);

        if (StokProduk != null)
            return StokProduk;
        else
            return null;
    }
    public TBStokProduk Cari(int idStokProduk)
    {
        //PENCARIAN STOK PRODUK BERDASARKAN IDTempat IDKombinasiProduk
        var StokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == idStokProduk);

        if (StokProduk != null)
            return StokProduk;
        else
            return null;
    }
    public TBStokProduk Cari(int idTempat, TBKombinasiProduk KombinasiProduk)
    {
        if (idTempat <= 0 || KombinasiProduk == null)
            return null;

        return KombinasiProduk.TBStokProduks.FirstOrDefault(item => item.IDTempat == idTempat);
    }
    public bool CekJumlahStok(int idTempat, int idKombinasiProduk, int jumlahStok)
    {
        //JUMLAH STOK HARUS LEBIH BESAR DARI 0
        if (jumlahStok > 0)
        {
            var StokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDTempat == idTempat && item.IDKombinasiProduk == idKombinasiProduk && item.Jumlah >= jumlahStok);

            if (StokProduk != null)
                return true;
            else
                return false;
        }
        else
            return false; //JIKA JUMLAH STOK TIDAK VALID AKAN RETURN FALSE
    }
    #endregion

    #region PENYESUAIAN PRODUK
    public bool Penyesuaian(int idTempat, int idPengguna, TBStokProduk StokProduk, int jumlahStok, string keterangan)
    {
        //PENCARIAN STOK PRODUK BERDASARKAN IDTempat IDKombinasiProduk
        if (StokProduk != null)
        {
            //PENYESUAIAN STOK PRODUK
            EnumJenisPerpindahanStok enumJenisPerpindahanStok;

            //menghitung selisih stok
            int selisihStok = (int)(StokProduk.Jumlah - jumlahStok);

            if (selisihStok != 0)
            {
                if (selisihStok < 0)
                    enumJenisPerpindahanStok = EnumJenisPerpindahanStok.StokOpnameBertambah;
                else
                    enumJenisPerpindahanStok = EnumJenisPerpindahanStok.StokOpnameBerkurang;

                var JenisPerpindahanStok = db.TBJenisPerpindahanStoks.FirstOrDefault(item => item.IDJenisPerpindahanStok == (int)enumJenisPerpindahanStok);

                if (JenisPerpindahanStok != null)
                {
                    //UPDATE JUMLAH STOK PRODUK
                    StokProduk.Jumlah = jumlahStok;

                    //MUTASI STOK PRODUK
                    MutasiStokProduk(idPengguna, StokProduk, enumJenisPerpindahanStok, selisihStok, StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, keterangan);

                    //MENCATAT DI PERPINDAHAN STOK PRODUK
                    db.TBPerpindahanStokProduks.InsertOnSubmit(new TBPerpindahanStokProduk
                    {
                        IDWMS = Guid.NewGuid(),
                        TBJenisPerpindahanStok = JenisPerpindahanStok,
                        TBStokProduk = StokProduk,
                        IDTempat = idTempat,
                        IDPengguna = idPengguna,
                        Tanggal = DateTime.Now,
                        Jumlah = Math.Abs(selisihStok),
                        Keterangan = keterangan
                    });

                    return true;
                }
                else
                    return false; //JENIS PERPINDAHAN STOK PRODUK TIDAK DITEMUKAN
            }
            else
                return false; //TIDAK ADA PERUBAHAN JUMLAH STOK
        }
        else
            return false;  //STOK PRODUK TIDAK DITEMUKAN
    }
    public bool Penyesuaian(int idTempat, int idPengguna, int idKombinasiProduk, int jumlahStok, string keterangan)
    {
        //PENCARIAN STOK PRODUK BERDASARKAN IDTempat IDKombinasiProduk
        var StokProduk = Cari(idTempat, idKombinasiProduk);

        return Penyesuaian(idTempat, idPengguna, StokProduk, jumlahStok, keterangan);
    }
    public bool Penyesuaian(int idTempat, int idPengguna, TBStokProduk StokProduk, TextBox TextBoxJumlahStok, string keterangan)
    {
        //JIKA JUMLAH STOK BERUPA TEXTBOX
        int TempJumlahStok;

        if (!string.IsNullOrWhiteSpace(TextBoxJumlahStok.Text) && int.TryParse(TextBoxJumlahStok.Text.Replace(",", ""), out TempJumlahStok))
            return Penyesuaian(idTempat, idPengguna, StokProduk, TextBoxJumlahStok.Text.ToDecimal().ToInt(), keterangan);
        else
            return false;
    }
    public bool Penyesuaian(int idTempat, int idPengguna, int idKombinasiProduk, TextBox TextBoxJumlahStok, string keterangan)
    {
        //JIKA JUMLAH STOK BERUPA TEXTBOX

        int TempJumlahStok;

        if (!string.IsNullOrWhiteSpace(TextBoxJumlahStok.Text) && int.TryParse(TextBoxJumlahStok.Text.Replace(",", ""), out TempJumlahStok))
            return Penyesuaian(idTempat, idPengguna, idKombinasiProduk, TextBoxJumlahStok.Text.ToDecimal().ToInt(), keterangan);
        else
            return false;
    }
    #endregion

    #region BERTAMBAH BERKURANG
    public bool BertambahBerkurang(int idTempat, int idPengguna, int idKombinasiProduk, int jumlahStok, decimal hargaBeli, decimal hargaJual, EnumJenisPerpindahanStok enumJenisPerpindahanStok, string keterangan)
    {
        //PENCARIAN STOK PRODUK BERDASARKAN IDTempat IDKombinasiProduk
        var StokProduk = Cari(idTempat, idKombinasiProduk);

        return BertambahBerkurang(idTempat, idPengguna, StokProduk, jumlahStok, hargaBeli, hargaJual, enumJenisPerpindahanStok, keterangan);
    }
    public bool BertambahBerkurang(int idTempat, int idPengguna, TBStokProduk StokProduk, int jumlahStok, decimal hargaBeli, decimal hargaJual, EnumJenisPerpindahanStok enumJenisPerpindahanStok, string keterangan)
    {
        return BertambahBerkurang(DateTime.Now, idTempat, idPengguna, StokProduk, jumlahStok, hargaBeli, hargaJual, enumJenisPerpindahanStok, keterangan);
    }
    public bool BertambahBerkurang(DateTime tanggal, int idTempat, int idPengguna, TBStokProduk StokProduk, int jumlahStok, decimal hargaBeli, decimal hargaJual, EnumJenisPerpindahanStok enumJenisPerpindahanStok, string keterangan)
    {
        if (StokProduk != null)
        {
            jumlahStok = Math.Abs(jumlahStok); //STOK HARUS LEBIH DARI 0

            if (jumlahStok > 0)
            {
                //MENCARI JENIS PERPINDAHAN STOK
                var JenisPerpindahanStok = db.TBJenisPerpindahanStoks.FirstOrDefault(item => item.IDJenisPerpindahanStok == (int)enumJenisPerpindahanStok);

                if (JenisPerpindahanStok != null)
                {
                    //MUTASI STOK PRODUK
                    MutasiStokProduk(idPengguna, StokProduk, enumJenisPerpindahanStok, jumlahStok, hargaBeli, hargaJual, keterangan);

                    if (JenisPerpindahanStok.Status.Value)
                    {
                        StokProduk.HargaBeli = PenghitunganAverage(StokProduk.Jumlah.Value, StokProduk.HargaBeli.Value, jumlahStok, hargaBeli);

                        StokProduk.Jumlah += jumlahStok;
                    }
                    else
                        StokProduk.Jumlah -= jumlahStok;

                    //MENCATAT DI PERPINDAHAN STOK PRODUK
                    db.TBPerpindahanStokProduks.InsertOnSubmit(new TBPerpindahanStokProduk
                    {
                        IDWMS = Guid.NewGuid(),
                        TBJenisPerpindahanStok = JenisPerpindahanStok,
                        TBStokProduk = StokProduk,
                        IDTempat = idTempat,
                        IDPengguna = idPengguna,
                        Tanggal = tanggal,
                        Jumlah = jumlahStok,
                        Keterangan = keterangan
                    });

                    return true;
                }
                else
                    return false; //JENIS PERPINDAHAN STOK PRODUK TIDAK DITEMUKAN
            }
            else
                return false; //JUMLAH STOK TIDAK VALID
        }
        else
            return false; //STOK PRODUK TIDAK DITEMUKAN
    }
    #endregion

    private void MutasiStokProduk(int idPengguna, TBStokProduk StokProduk, EnumJenisPerpindahanStok enumJenisPerpindahanStok, int jumlahStok, decimal hargaBeli, decimal hargaJual, string keterangan)
    {
        var StokProdukMutasi = new TBStokProdukMutasi
        {
            //IDStokProdukMutasi
            //IDStokProduk
            TBStokProduk = StokProduk,
            IDPengguna = idPengguna,
            IDWMS = Guid.NewGuid(),
            Tanggal = DateTime.Now,
            HargaBeli = hargaBeli,
            HargaJual = hargaJual,
            Keterangan = keterangan
        };

        //KREDIT
        if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.TransferStokKeluar) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Transfer;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.TransferStokMasuk) //BERKURANG
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Transfer;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.TransferBatal) //BERKURANG
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Transfer;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.Transaksi) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Transaksi;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.PerubahanTransaksi) //BERKURANG
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Transaksi;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.TransaksiBatal) //BERKURANG
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Transaksi;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.TolakPenerimaanPO) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.ReturPurchaseOrder;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.PembuanganBarangRusak) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.PembuanganRusak;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.ReturKeTempatProduksi) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.ReturProduksi;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.PenguranganProduksi) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.PenguranganUntukProduksi;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }

        //DEBIT
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.RestockBarang) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Restock;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.ReturDariPembeli) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.ReturPelanggan;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.ReturDariPembeliBatal) //BERKURANG
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.ReturPelanggan;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.PerubahanReturDariPembeli) //BERKURANG
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.ReturPelanggan;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = jumlahStok;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.PenerimaanPO) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.PurchaseOrder;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.HasilProduksi) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.Produksi;
            StokProdukMutasi.Debit = jumlahStok;
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.StokOpnameBertambah) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.StokOpname;
            StokProdukMutasi.Debit = Math.Abs(jumlahStok);
            StokProdukMutasi.Kredit = 0;

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
        else if (enumJenisPerpindahanStok == EnumJenisPerpindahanStok.StokOpnameBerkurang) //BERTAMBAH
        {
            StokProdukMutasi.IDJenisStokMutasi = (int)EnumJenisStokMutasi.StokOpname;
            StokProdukMutasi.Debit = 0;
            StokProdukMutasi.Kredit = Math.Abs(jumlahStok);

            db.TBStokProdukMutasis.InsertOnSubmit(StokProdukMutasi);
        }
    }

    #region MEMBUAT STOK PRODUK
    public TBStokProduk MembuatStokKonsinyasi(int jumlahAwal, int idTempat, int idPengguna, int idKombinasiProduk, decimal persentaseKonsinyasi, decimal hargaJual, string keterangan)
    {
        decimal hargaBeli = hargaJual - (hargaJual * persentaseKonsinyasi);

        return MembuatBaru(jumlahAwal, idTempat, idPengguna, idKombinasiProduk, hargaBeli, hargaJual, persentaseKonsinyasi, keterangan, true);
    }
    public TBStokProduk MembuatStokKonsinyasi(int jumlahAwal, int idTempat, int idPengguna, TBKombinasiProduk kombinasiProduk, decimal persentaseKonsinyasi, decimal hargaJual, string keterangan)
    {
        decimal hargaBeli = hargaJual - (hargaJual * persentaseKonsinyasi);

        return MembuatBaru(jumlahAwal, idTempat, idPengguna, kombinasiProduk, hargaBeli, hargaJual, persentaseKonsinyasi, keterangan, true);
    }
    public TBStokProduk MembuatStok(int jumlahAwal, int idTempat, int idPengguna, int idKombinasiProduk, decimal hargaBeli, decimal hargaJual, string keterangan)
    {
        return MembuatBaru(jumlahAwal, idTempat, idPengguna, idKombinasiProduk, hargaBeli, hargaJual, 0, keterangan, true);
    }
    public TBStokProduk MembuatStok(int jumlahAwal, int idTempat, int idPengguna, TBKombinasiProduk kombinasiProduk, decimal hargaBeli, decimal hargaJual, string keterangan)
    {
        return MembuatBaru(jumlahAwal, idTempat, idPengguna, kombinasiProduk, hargaBeli, hargaJual, 0, keterangan, true);
    }
    private TBStokProduk MembuatBaru(int jumlahAwal, int idTempat, int idPengguna, int idKombinasiProduk, decimal hargaBeli, decimal hargaJual, decimal persentaseKonsinyasi, string keterangan, bool status)
    {
        KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
        var KombinasiProduk = KombinasiProduk_Class.Cari(db, idKombinasiProduk);

        return MembuatBaru(jumlahAwal, idTempat, idPengguna, KombinasiProduk, hargaBeli, hargaJual, persentaseKonsinyasi, keterangan, true);
    }
    private TBStokProduk MembuatBaru(int jumlahAwal, int idTempat, int idPengguna, TBKombinasiProduk kombinasiProduk, decimal hargaBeli, decimal hargaJual, decimal persentaseKonsinyasi, string keterangan, bool status)
    {
        StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

        string KonfigurasiMinimumStok = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.MinimumStok).Pengaturan;

        decimal JumlahMinimum;
        decimal JumlahStokAwal = (jumlahAwal < 0) ? 0 : jumlahAwal;

        //MENGATUR STOK MINIMUM
        if (KonfigurasiMinimumStok.Contains("%"))
            JumlahMinimum = Math.Ceiling(JumlahStokAwal * KonfigurasiMinimumStok.Replace("%", "").ToDecimal() / 100);
        else
            JumlahMinimum = KonfigurasiMinimumStok.ToDecimal();

        //MEMBUAT STOK PRODUK
        TBStokProduk StokProduk = new TBStokProduk
        {
            HargaBeli = hargaBeli,
            HargaJual = hargaJual,
            TBKombinasiProduk = kombinasiProduk,
            IDTempat = idTempat,
            Jumlah = 0,
            JumlahMinimum = (int)JumlahMinimum,
            PersentaseKonsinyasi = persentaseKonsinyasi,
            Status = true,
            EnumDiscountStore = (int)EnumDiscount.TidakAda,
            DiscountStore = 0,
            EnumDiscountKonsinyasi = (int)EnumDiscount.TidakAda,
            DiscountKonsinyasi = 0
        };

        if (jumlahAwal > 0)
            BertambahBerkurang(idTempat, idPengguna, StokProduk, jumlahAwal, StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.StokOpnameBertambah, keterangan);
        else
            BertambahBerkurang(idTempat, idPengguna, StokProduk, jumlahAwal, StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.StokOpnameBerkurang, keterangan);

        return StokProduk;
    }
    #endregion

    public List<string> ValidasiStokProdukTransaksi(TBTransaksiECommerceDetail[] TransaksiDetail)
    {
        List<string> PesanStokHabis = new List<string>();

        foreach (var item in TransaksiDetail)
        {
            var StokProduk = db.TBStokProduks
                .FirstOrDefault(item2 =>
                    item2.IDStokProduk == item.IDStokProduk);

            if (StokProduk.TBKombinasiProduk.TBProduk._IsActive)
            {
                if (StokProduk.Jumlah == 0)
                    PesanStokHabis.Add(StokProduk.TBKombinasiProduk.Nama + " sold out");
                else if (StokProduk.Jumlah < item.Quantity)
                    PesanStokHabis.Add(StokProduk.TBKombinasiProduk.Nama + " melebihi jumlah stok yang tersedia");
            }
            else
                PesanStokHabis.Add(StokProduk.TBKombinasiProduk.Nama + " tidak ditemukan, silahkan hapus dari keranjang belanja");
        }

        return PesanStokHabis;
    }

    public TBStokProduk Ubah(int idTempat, int idPengguna, TBKombinasiProduk KombinasiProduk, decimal hargaBeli, decimal hargaJual)
    {
        if (KombinasiProduk == null)
            return null;

        var StokProduk = Cari(idTempat, KombinasiProduk);

        if (StokProduk == null)
            StokProduk = MembuatStok(0, idTempat, idPengguna, KombinasiProduk, hargaBeli, hargaJual, "");
        else
        {
            //IDTempat
            //IDKombinasiProduk
            StokProduk.HargaBeli = hargaBeli;
            StokProduk.HargaJual = hargaJual;
            //PersentaseKonsinyasi
            //Jumlah
            //JumlahMinimum
        }

        return StokProduk;
    }

    //BANU RUSMAN
    #region POProduksiProduk
    public void PengaturanJumlahStokPenerimaanPOProduk(DateTime tanggal, int idPengguna, int idTempat, TBStokProduk stokProduk, decimal hargaBeli, int jumlahDatang, int jumlahTolakKeVendor, string keterangan)
    {
        if (stokProduk != null)
        {
            if (jumlahDatang - jumlahTolakKeVendor > 0)
                stokProduk.HargaBeli = HitungRataRataHargaBeli(stokProduk.Jumlah.Value, stokProduk.HargaBeli.Value, (jumlahDatang - jumlahTolakKeVendor), hargaBeli);

            if (jumlahDatang > 0)
                BertambahBerkurang(tanggal, idTempat, idPengguna, stokProduk, jumlahDatang, hargaBeli, stokProduk.HargaJual.Value, EnumJenisPerpindahanStok.PenerimaanPO, "Penerimaan #" + keterangan);

            if (jumlahTolakKeVendor > 0)
                BertambahBerkurang(tanggal, idTempat, idPengguna, stokProduk, jumlahTolakKeVendor, hargaBeli, stokProduk.HargaJual.Value, EnumJenisPerpindahanStok.TolakPenerimaanPO, "Penerimaan Tolak Ke Vendor #" + keterangan);
        }
    }
    public void PengaturanJumlahStokPenerimaanPOProdukTolakKeGudang(DateTime tanggal, int idPengguna, int idTempat, TBStokProduk stokProduk, decimal hargaBeli, int jumlahTolakKeGudang, string keterangan)
    {
        if (stokProduk != null)
        {
            if (jumlahTolakKeGudang > 0)
                BertambahBerkurang(tanggal, idTempat, idPengguna, stokProduk, jumlahTolakKeGudang, hargaBeli, stokProduk.HargaJual.Value, EnumJenisPerpindahanStok.TransferStokKeluar, keterangan);
        }
    }
    #endregion

    public decimal PenghitunganAverage(decimal jumlahStokLama, decimal hargaBeliStokLama, decimal jumlahStokBaru, decimal hargaBeliStokBaru)
    {
        if (jumlahStokBaru == 0)
        {
            return hargaBeliStokLama;
        }
        else if (jumlahStokLama < 0 || (jumlahStokLama + jumlahStokBaru <= 0))
        {
            return hargaBeliStokBaru;
        }
        else
        {
            return ((jumlahStokLama * hargaBeliStokLama) + (jumlahStokBaru * hargaBeliStokBaru)) / (jumlahStokLama + jumlahStokBaru);
        }
    }

    #region HPP
    public static decimal HitungRataRataHargaBeli(decimal jumlahStokLama, decimal hargaBeliLama, decimal jumlahStokBaru, decimal hargaBeliBaru)
    {
        if (jumlahStokBaru == 0)
            return hargaBeliLama;
        else if (jumlahStokLama + jumlahStokBaru <= 0)
            return hargaBeliBaru;
        else
            return ((jumlahStokLama * hargaBeliLama) + (jumlahStokBaru * hargaBeliBaru)) / (jumlahStokLama + jumlahStokBaru);
    }

    public static decimal HitungRataRataHargaJual(decimal jumlahStokLama, decimal hargaJualLama, decimal jumlahStokBaru, decimal hargaJualBaru)
    {
        if (jumlahStokBaru == 0)
            return hargaJualLama;
        else if (jumlahStokLama + jumlahStokBaru <= 0)
            return hargaJualBaru;
        else
            return ((jumlahStokLama * hargaJualLama) + (jumlahStokBaru * hargaJualBaru)) / (jumlahStokLama + jumlahStokBaru);
    }

    public static decimal HitungHargaPokokKomposisi(DataClassesDatabaseDataContext db, int idTempat, TBKombinasiProduk kombinasiProduk)
    {
        decimal hargaBeli = 0;

        foreach (var komposisi in kombinasiProduk.TBKomposisiKombinasiProduks.ToArray())
        {
            hargaBeli = hargaBeli + (komposisi.TBBahanBaku.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == idTempat && item.IDBahanBaku == komposisi.IDBahanBaku).HargaBeli.Value * komposisi.Jumlah.Value);
        }

        return hargaBeli;
    }

    public static decimal HitungBiayaProduksi(DataClassesDatabaseDataContext db, int idTempat, TBKombinasiProduk kombinasiProduk)
    {
        var listBiayaProduksi = kombinasiProduk.TBRelasiJenisBiayaProduksiKombinasiProduks.ToArray();

        decimal hasil = 0;

        foreach (var item in listBiayaProduksi)
        {
            PilihanBiayaProduksi enumBiayaProduksi = (PilihanBiayaProduksi)item.EnumBiayaProduksi;

            switch (enumBiayaProduksi)
            {
                case PilihanBiayaProduksi.TidakAda:
                    hasil = hasil + 0;
                    break;
                case PilihanBiayaProduksi.Persen:
                    hasil = hasil + (decimal)(item.Persentase * HitungHargaPokokKomposisi(db, idTempat, kombinasiProduk));
                    break;
                case PilihanBiayaProduksi.Harga:
                    hasil = hasil + (decimal)item.Nominal;
                    break;
            }
        }

        return hasil;
    }

    public static decimal HitungHargaPokokProduksi(DataClassesDatabaseDataContext db, int idTempat, TBKombinasiProduk kombinasiProduk)
    {
        return (HitungHargaPokokKomposisi(db, idTempat, kombinasiProduk) + HitungBiayaProduksi(db, idTempat, kombinasiProduk));
    }

    public static void PerbaharuiHargaBeli(DataClassesDatabaseDataContext db, int idTempat, int idkombinasiProduk)
    {
        TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDTempat == idTempat && item.IDKombinasiProduk == idkombinasiProduk);

        stokProduk.HargaBeli = HitungHargaPokokProduksi(db, idTempat, stokProduk.TBKombinasiProduk);
    }

    public static void PerbaharuiSemuaHargaBeli(DataClassesDatabaseDataContext db, int idTempat)
    {
        var dataStokProduk = db.TBStokProduks.Where(item => item.IDTempat == idTempat && item.TBKombinasiProduk.TBKomposisiKombinasiProduks.Count > 0);

        foreach (var item in dataStokProduk)
        {
            item.HargaBeli = HitungHargaPokokProduksi(db, item.IDTempat, item.TBKombinasiProduk);
        }
    }
    #endregion

    #region Output
    public static string GabungkanSemuaKategoriProduk(DataClassesDatabaseDataContext db, TBStokProduk stokProduk, TBKombinasiProduk kombinasiProduk)
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
    public static string StatusHargaPokokProduksi(string hargaPokokSaatIni, string hargaPokokProduksi)
    {
        if (hargaPokokSaatIni == hargaPokokProduksi)
            return "text-right success";
        else
            return "text-right danger";
    }
    #endregion
}