using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StokBahanBaku_Class
/// </summary>
public class StokBahanBaku_Class
{
    #region PELENGKAP
    public static decimal HitungRataRataHargaBeli(decimal jumlahStokLama, decimal hargaBeliLama, decimal jumlahStokBaru, decimal hargaBeliBaru)
    {
        if (jumlahStokBaru == 0)
        {
            return hargaBeliLama;
        }
        else if (jumlahStokLama + jumlahStokBaru <= 0 || jumlahStokLama <= 0)
        {
            return hargaBeliBaru;
        }
        else
        {
            return ((jumlahStokLama * hargaBeliLama) + (jumlahStokBaru * hargaBeliBaru)) / (jumlahStokLama + jumlahStokBaru);
        }
    }
    public static string OutPutKategori(DataClassesDatabaseDataContext db, TBStokBahanBaku stokBahanBaku, TBBahanBaku bahanBaku)
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
    #endregion

    #region INSERT
    public static TBStokBahanBaku InsertStokBahanBaku(DataClassesDatabaseDataContext db, DateTime tanggal, int idPengguna, int idTempat, TBBahanBaku bahanBaku, decimal hargaBeli, decimal jumlahStokAwal, decimal jumlahMinimum, string keterangan)
    {
        TBStokBahanBaku stokBahanBaku = new TBStokBahanBaku
        {
            IDTempat = idTempat,
            TBBahanBaku = bahanBaku,
            HargaBeli = hargaBeli,
            Jumlah = 0,
            JumlahMinimum = jumlahMinimum,
        };
        db.TBStokBahanBakus.InsertOnSubmit(stokBahanBaku);

        if (jumlahStokAwal > 0)
            UpdateBertambahBerkurang(db, tanggal, idPengguna, stokBahanBaku, jumlahStokAwal, stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.StokOpnameBertambah, keterangan);
        else
            UpdateBertambahBerkurang(db, tanggal, idPengguna, stokBahanBaku, jumlahStokAwal, stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.StokOpnameBerkurang, keterangan);

        return stokBahanBaku;
    }

    public static void InsertPerpindahanStok(DataClassesDatabaseDataContext db, int idJenisPerindahanStok, TBStokBahanBaku stokBahanBaku, int idTempat, int idPengguna, int idSatuan, DateTime tanggal, decimal hargaBeli, decimal jumlah, string keterangan)
    {
        db.TBPerpindahanStokBahanBakus.InsertOnSubmit(new TBPerpindahanStokBahanBaku
        {
            IDWMS = Guid.NewGuid(),
            IDJenisPerpindahanStok = idJenisPerindahanStok,
            TBStokBahanBaku = stokBahanBaku,
            IDTempat = idTempat,
            IDPengguna = idPengguna,
            IDSatuan = idSatuan,
            Tanggal = tanggal,
            HargaBeli = hargaBeli,
            Jumlah = jumlah,
            Keterangan = keterangan
        });
    }

    //public static void InsertStokBahanBakuDetail(DataClassesDatabaseDataContext db, string kodeStokBahanBaku, int idStokBahanBaku, int idSupplier, DateTime tanggalDaftar, decimal hargaBeli, decimal jumlah)
    //{
    //    db.TBStokBahanBakuDetails.InsertOnSubmit(new TBStokBahanBakuDetail
    //    {
    //        KodeStokBahanBaku = kodeStokBahanBaku,
    //        IDStokBahanBaku = idStokBahanBaku,
    //        IDSupplier = idSupplier,
    //        TanggalDaftar = tanggalDaftar,
    //        HargaBeli = hargaBeli,
    //        Jumlah = jumlah,
    //        Aktif = true
    //    });
    //}

    //public static string InsertStokBahanBakuDetailSP(DataClassesDatabaseDataContext db, TBStokBahanBaku stokBahanBaku, int idSupplier, string kodeStokBahanBaku, DateTime tanggalDaftar, decimal hargaBeli, decimal jumlah, bool satuanBesar)
    //{
    //    #region Konversi Ke Satuan Stok Bahan Baku
    //    if (satuanBesar == true)
    //    {
    //        jumlah *= stokBahanBaku.TBBahanBaku.Konversi.Value;
    //        hargaBeli /= stokBahanBaku.TBBahanBaku.Konversi.Value;
    //    }
    //    #endregion

    //    db.Proc_InsertStokBahanBakuDetail(ref kodeStokBahanBaku, stokBahanBaku.IDStokBahanBaku, idSupplier, tanggalDaftar, hargaBeli, jumlah);

    //    return kodeStokBahanBaku;
    //}
    #endregion

    #region UPDATE
    public static TBStokBahanBaku UpdateBertambahBerkurang(DataClassesDatabaseDataContext db, DateTime tanggal, int idPengguna, TBStokBahanBaku stokBahanBaku, decimal jumlahStok, decimal hargaBeli, bool satuanBesar, EnumJenisPerpindahanStok enumJenisPerpindahanStok, string keterangan)
    {
        if (stokBahanBaku != null)
        {
            if (jumlahStok > 0)
            {
                //MENCARI JENIS PERPINDAHAN STOK
                TBJenisPerpindahanStok JenisPerpindahanStok = db.TBJenisPerpindahanStoks.FirstOrDefault(item => item.IDJenisPerpindahanStok == (int)enumJenisPerpindahanStok);

                if (JenisPerpindahanStok != null)
                {
                    #region Konversi Ke Satuan Stok Bahan Baku
                    if (satuanBesar == true)
                    {
                        jumlahStok *= stokBahanBaku.TBBahanBaku.Konversi.Value;
                        hargaBeli /= stokBahanBaku.TBBahanBaku.Konversi.Value;
                    }
                    #endregion

                    decimal hargaBeliPerpindahanStok = 0;
                    if ((bool)JenisPerpindahanStok.Status)
                    {
                        hargaBeliPerpindahanStok = hargaBeli;
                        stokBahanBaku.HargaBeli = HitungRataRataHargaBeli(stokBahanBaku.Jumlah.Value, stokBahanBaku.HargaBeli.Value, jumlahStok, hargaBeli);
                        stokBahanBaku.Jumlah += jumlahStok;
                    }
                    else
                    {
                        hargaBeliPerpindahanStok = stokBahanBaku.HargaBeli.Value;
                        stokBahanBaku.Jumlah -= jumlahStok;
                    }

                    //MENCATAT DI PERPINDAHAN STOK PRODUK
                    InsertPerpindahanStok(
                        db: db,
                        idJenisPerindahanStok: JenisPerpindahanStok.IDJenisPerpindahanStok,
                        stokBahanBaku: stokBahanBaku,
                        idTempat: stokBahanBaku.IDTempat.Value,
                        idPengguna: idPengguna,
                        idSatuan: stokBahanBaku.TBBahanBaku.IDSatuan,
                        tanggal: tanggal,
                        hargaBeli: hargaBeliPerpindahanStok,
                        jumlah: jumlahStok,
                        keterangan: keterangan);
                }
            }
        }

        return stokBahanBaku;
    }
    
    //public static TBStokBahanBakuDetail UpdateBertambahBerkurangDetail(DataClassesDatabaseDataContext db, DateTime tanggal, int idPengguna, TBStokBahanBakuDetail stokBahanBakuDetail, decimal jumlahStok, decimal hargaBeli, bool satuanBesar, EnumJenisPerpindahanStok enumJenisPerpindahanStok, string keterangan)
    //{
    //    if (stokBahanBakuDetail != null)
    //    {
    //        if (jumlahStok > 0)
    //        {
    //            //MENCARI JENIS PERPINDAHAN STOK
    //            TBJenisPerpindahanStok JenisPerpindahanStok = db.TBJenisPerpindahanStoks.FirstOrDefault(item => item.IDJenisPerpindahanStok == (int)enumJenisPerpindahanStok);

    //            if (JenisPerpindahanStok != null)
    //            {
    //                #region Konversi Ke Satuan Stok Bahan Baku
    //                if (satuanBesar == true)
    //                {
    //                    jumlahStok *= stokBahanBakuDetail.TBStokBahanBaku.TBBahanBaku.Konversi.Value;
    //                    hargaBeli /= stokBahanBakuDetail.TBStokBahanBaku.TBBahanBaku.Konversi.Value;
    //                }
    //                #endregion

    //                decimal hargaBeliPerpindahanStok = 0;
    //                if ((bool)JenisPerpindahanStok.Status)
    //                {
    //                    hargaBeliPerpindahanStok = hargaBeli;
    //                    stokBahanBakuDetail.TBStokBahanBaku.HargaBeli = HitungRataRataHargaBeli(stokBahanBakuDetail.TBStokBahanBaku.Jumlah.Value, stokBahanBakuDetail.TBStokBahanBaku.HargaBeli.Value, jumlahStok, hargaBeli);
    //                    stokBahanBakuDetail.TBStokBahanBaku.Jumlah += jumlahStok;
    //                    stokBahanBakuDetail.Jumlah += jumlahStok;
    //                }
    //                else
    //                {
    //                    hargaBeliPerpindahanStok = stokBahanBakuDetail.HargaBeli;
    //                    stokBahanBakuDetail.TBStokBahanBaku.Jumlah -= jumlahStok;
    //                    stokBahanBakuDetail.Jumlah -= jumlahStok;
    //                }

    //                //MENCATAT DI PERPINDAHAN STOK PRODUK
    //                InsertPerpindahanStok(
    //                    db: db,
    //                    idJenisPerindahanStok: JenisPerpindahanStok.IDJenisPerpindahanStok,
    //                    idStokBahanBaku: stokBahanBakuDetail.IDStokBahanBaku,
    //                    idTempat: stokBahanBakuDetail.TBStokBahanBaku.IDTempat.Value,
    //                    idPengguna: idPengguna,
    //                    idSatuan: stokBahanBakuDetail.TBStokBahanBaku.TBBahanBaku.IDSatuan,
    //                    tanggal: tanggal,
    //                    hargaBeli: hargaBeliPerpindahanStok,
    //                    jumlah: jumlahStok,
    //                    keterangan: keterangan);
    //            }
    //        }
    //    }

    //    return stokBahanBakuDetail;
    //}

    public static TBStokBahanBaku UpdateStockOpname(DataClassesDatabaseDataContext db, DateTime tanggal, int idPengguna, TBStokBahanBaku stokBahanBaku, decimal jumlahStokTerbaru, bool satuanBesar, string keterangan)
    {
        #region Konversi Ke Satuan Stok Bahan Baku
        if (satuanBesar == true)
        {
            jumlahStokTerbaru *= stokBahanBaku.TBBahanBaku.Konversi.Value;
        }
        #endregion

        if (stokBahanBaku.Jumlah < jumlahStokTerbaru)
        {
            decimal selisihStok = jumlahStokTerbaru - stokBahanBaku.Jumlah.Value;
            stokBahanBaku.Jumlah = jumlahStokTerbaru;

            //MENCATAT DI PERPINDAHAN STOK PRODUK
            InsertPerpindahanStok(
                db: db,
                idJenisPerindahanStok: (int)EnumJenisPerpindahanStok.StokOpnameBertambah,
                stokBahanBaku: stokBahanBaku,
                idTempat: stokBahanBaku.IDTempat.Value,
                idPengguna: idPengguna,
                idSatuan: stokBahanBaku.TBBahanBaku.IDSatuan,
                tanggal: tanggal,
                hargaBeli: stokBahanBaku.HargaBeli.Value,
                jumlah: selisihStok,
                keterangan: keterangan);
        }
        else if (stokBahanBaku.Jumlah > jumlahStokTerbaru)
        {
            decimal selisihStok = stokBahanBaku.Jumlah.Value - jumlahStokTerbaru;
            stokBahanBaku.Jumlah = jumlahStokTerbaru;

            //MENCATAT DI PERPINDAHAN STOK PRODUK
            InsertPerpindahanStok(
                db: db,
                idJenisPerindahanStok: (int)EnumJenisPerpindahanStok.StokOpnameBerkurang,
                stokBahanBaku: stokBahanBaku,
                idTempat: stokBahanBaku.IDTempat.Value,
                idPengguna: idPengguna,
                idSatuan: stokBahanBaku.TBBahanBaku.IDSatuan,
                tanggal: tanggal,
                hargaBeli: stokBahanBaku.HargaBeli.Value,
                jumlah: selisihStok,
                keterangan: keterangan);
        }

        return stokBahanBaku;
    }
    #endregion

    #region POProduksiBahanBaku
    public static void PengaturanJumlahStokPenerimaanPOBahanBaku(DataClassesDatabaseDataContext db, DateTime tanggal, int idPengguna, int idTempat, TBStokBahanBaku stokBahanBaku, decimal hargaBeli, TBSatuan satuanPODetail, decimal jumlahDatang, decimal jumlahTolakKeSupplier, string keterangan)
    {
        if (stokBahanBaku != null)
        {
            #region Konversi Ke Satuan Stok Bahan Baku
            if (satuanPODetail != stokBahanBaku.TBBahanBaku.TBSatuan)
            {
                jumlahDatang *= stokBahanBaku.TBBahanBaku.Konversi.Value;
                jumlahTolakKeSupplier *= stokBahanBaku.TBBahanBaku.Konversi.Value;
                hargaBeli /= stokBahanBaku.TBBahanBaku.Konversi.Value;
            }
            #endregion

            if (jumlahDatang - jumlahTolakKeSupplier > 0)
                stokBahanBaku.HargaBeli = HitungRataRataHargaBeli(stokBahanBaku.Jumlah.Value, stokBahanBaku.HargaBeli.Value, (jumlahDatang - jumlahTolakKeSupplier), hargaBeli);


            if (jumlahDatang > 0)
            {
                stokBahanBaku.Jumlah += jumlahDatang;

                db.TBPerpindahanStokBahanBakus.InsertOnSubmit(new TBPerpindahanStokBahanBaku { IDJenisPerpindahanStok = 31, IDWMS = Guid.NewGuid(), TBStokBahanBaku = stokBahanBaku, IDTempat = idTempat, IDPengguna = idPengguna, TBSatuan = stokBahanBaku.TBBahanBaku.TBSatuan, Tanggal = tanggal, HargaBeli = hargaBeli, Jumlah = jumlahDatang, Keterangan = "Penerimaan PO #" + keterangan });
            }

            if (jumlahTolakKeSupplier > 0)
            {
                stokBahanBaku.Jumlah -= jumlahTolakKeSupplier;

                db.TBPerpindahanStokBahanBakus.InsertOnSubmit(new TBPerpindahanStokBahanBaku { IDJenisPerpindahanStok = 32, IDWMS = Guid.NewGuid(), TBStokBahanBaku = stokBahanBaku, IDTempat = idTempat, IDPengguna = idPengguna, TBSatuan = stokBahanBaku.TBBahanBaku.TBSatuan, Tanggal = tanggal, HargaBeli = hargaBeli, Jumlah = jumlahTolakKeSupplier, Keterangan = "Penerimaan PO Tolak Ke Vendor #" + keterangan });
            }
        }
    }

    public static void PengaturanJumlahStokPenerimaanPOBahanBakuTolakKeGudang(DataClassesDatabaseDataContext db, DateTime tanggal, int idPengguna, int idTempat, TBStokBahanBaku stokBahanBaku, decimal hargaBeli, TBSatuan satuanPODetail, decimal jumlahTolakKeGudang, string keterangan)
    {
        if (stokBahanBaku != null)
        {
            #region Konversi Ke Satuan Stok Bahan Baku
            if (satuanPODetail != stokBahanBaku.TBBahanBaku.TBSatuan)
            {
                jumlahTolakKeGudang *= stokBahanBaku.TBBahanBaku.Konversi.Value;
                hargaBeli /= stokBahanBaku.TBBahanBaku.Konversi.Value;
            }
            #endregion

            if (jumlahTolakKeGudang > 0)
            {
                stokBahanBaku.Jumlah -= jumlahTolakKeGudang;

                db.TBPerpindahanStokBahanBakus.InsertOnSubmit(new TBPerpindahanStokBahanBaku
                {
                    IDJenisPerpindahanStok = 3,
                    IDWMS = Guid.NewGuid(),
                    TBStokBahanBaku = stokBahanBaku,
                    IDTempat = idTempat,
                    IDPengguna = idPengguna,
                    TBSatuan = stokBahanBaku.TBBahanBaku.TBSatuan,
                    Tanggal = tanggal,
                    HargaBeli = hargaBeli,
                    Jumlah = jumlahTolakKeGudang,
                    Keterangan = keterangan
                });
            }
        }
    }
    #endregion

    #region HPP
    public static decimal HitungHargaPokokKomposisi(DataClassesDatabaseDataContext db, int idTempat, TBBahanBaku bahanBaku)
    {
        decimal hargaBeli = 0;

        foreach (var komposisi in bahanBaku.TBKomposisiBahanBakus.ToArray())
        {
            hargaBeli = hargaBeli + (komposisi.TBBahanBaku1.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == idTempat && item.IDBahanBaku == komposisi.IDBahanBaku).HargaBeli.Value * komposisi.Jumlah.Value);
        }

        return hargaBeli;
    }

    public static decimal HitungBiayaProduksi(DataClassesDatabaseDataContext db, int idTempat, TBBahanBaku bahanBaku)
    {
        var listBiayaProduksi = bahanBaku.TBRelasiJenisBiayaProduksiBahanBakus.ToArray();

        decimal hasil = 0;

        foreach (var item in listBiayaProduksi)
        {
            PilihanBiayaProduksi biayaProduksi = (PilihanBiayaProduksi)item.EnumBiayaProduksi;

            switch (biayaProduksi)
            {
                case PilihanBiayaProduksi.TidakAda:
                    hasil = hasil + 0;
                    break;
                case PilihanBiayaProduksi.Persen:
                    hasil = hasil + (item.Persentase.Value * HitungHargaPokokKomposisi(db, idTempat, bahanBaku));
                    break;
                case PilihanBiayaProduksi.Harga:
                    hasil = hasil + item.Nominal.Value;
                    break;
            }
        }

        return hasil;
    }

    public static decimal HitungHargaPokokProduksi(DataClassesDatabaseDataContext db, int idTempat, TBBahanBaku bahanBaku)
    {
        return (HitungHargaPokokKomposisi(db, idTempat, bahanBaku) + HitungBiayaProduksi(db, idTempat, bahanBaku));
    }

    public static void PerbaharuiHargaBeli(DataClassesDatabaseDataContext db, int idTempat, int idBahanBaku)
    {
        TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == idTempat && item.IDBahanBaku == idBahanBaku);

        stokBahanBaku.HargaBeli = (HitungHargaPokokProduksi(db, idTempat, stokBahanBaku.TBBahanBaku) / stokBahanBaku.TBBahanBaku.Konversi.Value);
    }

    public static void PerbaharuiSemuaHargaBeli(DataClassesDatabaseDataContext db, int idTempat)
    {
        var dataStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == idTempat && item.TBBahanBaku.TBKomposisiBahanBakus.Count > 0);

        foreach (var item in dataStokBahanBaku)
        {
            item.HargaBeli = (HitungHargaPokokProduksi(db, item.IDTempat.Value, item.TBBahanBaku) / item.TBBahanBaku.Konversi.Value);
        }
    }
    #endregion

    #region PRODUKSI
    public static void ProduksiByTransaksiBerhasil(int IDPenggunaTransaksi, int IDTempat, string IDTransaksi)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<POProduksiKomposisi_Model> daftarProduksiKomposisi = new List<POProduksiKomposisi_Model>();

            var hasil = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == IDTransaksi).TBTransaksiDetails.GroupBy(item => item.TBKombinasiProduk).Select(item => new { KombinasiProduk = item.Key, Quantity = item.Sum(item2 => item2.Quantity) });
            foreach (var itemKombinasiProduk in hasil)
            {
                foreach (var itemKomposisi in itemKombinasiProduk.KombinasiProduk.TBKomposisiKombinasiProduks)
                {
                    POProduksiKomposisi_Model komposisi = daftarProduksiKomposisi.FirstOrDefault(item => item.IDBahanBaku == itemKomposisi.IDBahanBaku);

                    if (komposisi == null)
                    {
                        komposisi = new POProduksiKomposisi_Model()
                        {
                            IDBahanBaku = itemKomposisi.IDBahanBaku,
                            JumlahKurang = itemKomposisi.Jumlah.Value * itemKombinasiProduk.Quantity
                        };

                        daftarProduksiKomposisi.Add(komposisi);
                    }
                    else
                    {
                        komposisi.JumlahKurang += itemKomposisi.Jumlah.Value * itemKombinasiProduk.Quantity;
                    }
                }
            }

            foreach (var itemBahanBaku in daftarProduksiKomposisi)
            {
                //KURANGI STOK BAHAN BAKU
                TBStokBahanBaku cariStokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == IDTempat && item.IDBahanBaku == itemBahanBaku.IDBahanBaku);
                UpdateBertambahBerkurang(db, DateTime.Now, IDPenggunaTransaksi, cariStokBahanBaku, itemBahanBaku.JumlahKurang, cariStokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.Transaksi, "Produksi Produk POS #" + IDTransaksi);
            }

            db.SubmitChanges();
        }
    }
    public static void ProduksiByTransaksiBatal(int IDPenggunaTransaksi, int IDTempat, string IDTransaksi)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<POProduksiKomposisi_Model> daftarProduksiKomposisi = new List<POProduksiKomposisi_Model>();

            var hasil = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == IDTransaksi).TBTransaksiDetails.GroupBy(item => item.TBKombinasiProduk).Select(item => new { KombinasiProduk = item.Key, Quantity = item.Sum(item2 => item2.Quantity) });
            foreach (var itemKombinasiProduk in hasil)
            {
                foreach (var itemKomposisi in itemKombinasiProduk.KombinasiProduk.TBKomposisiKombinasiProduks)
                {
                    POProduksiKomposisi_Model komposisi = daftarProduksiKomposisi.FirstOrDefault(item => item.IDBahanBaku == itemKomposisi.IDBahanBaku);

                    if (komposisi == null)
                    {
                        komposisi = new POProduksiKomposisi_Model()
                        {
                            IDBahanBaku = itemKomposisi.IDBahanBaku,
                            JumlahKurang = itemKomposisi.Jumlah.Value * itemKombinasiProduk.Quantity
                        };

                        daftarProduksiKomposisi.Add(komposisi);
                    }
                    else
                    {
                        komposisi.JumlahKurang += itemKomposisi.Jumlah.Value * itemKombinasiProduk.Quantity;
                    }
                }
            }

            foreach (var itemBahanBaku in daftarProduksiKomposisi)
            {
                //KURANGI STOK BAHAN BAKU
                TBStokBahanBaku cariStokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == IDTempat && item.IDBahanBaku == itemBahanBaku.IDBahanBaku);
                UpdateBertambahBerkurang(db, DateTime.Now, IDPenggunaTransaksi, cariStokBahanBaku, itemBahanBaku.JumlahKurang, cariStokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.TransaksiBatal, "Produksi Produk POS #" + IDTransaksi);
            }

            db.SubmitChanges();
        }
    }
    #endregion

    #region Output
    public static string GabungkanSemuaKategoriBahanBaku(DataClassesDatabaseDataContext db, TBStokBahanBaku stokBahanBaku, TBBahanBaku bahanBaku)
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
    public static string StatusStokBahanBaku(string statusStokBahanBaku)
    {
        switch (statusStokBahanBaku)
        {
            case "Cukup": return "<span class=\"badge badge-success\">Cukup</span>";
            case "Butuh Restok": return "<span class=\"badge badge-danger\">Butuh Restok</span>";
            default: return string.Empty;
        }
    }

    public static string StatusHargaPokokProduksi(string hargaPokokSaatIni, string hargaPokokProduksi)
    {
        if (hargaPokokSaatIni == hargaPokokProduksi)
            return "success";
        else
            return "danger";
    }
    #endregion
}