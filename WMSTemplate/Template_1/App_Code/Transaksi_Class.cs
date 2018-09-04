using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

[Serializable]
public partial class Transaksi_Class
{
    #region arie
    public bool isRetur { get; set; }
    public decimal PembulatanJurnal { get; set; }
    #endregion

    public string IDTransaksi { get; set; }
    public int IDPenggunaTransaksi { get; set; }

    private int? iDPenggunaUpdate;  //BISA NULL
    public int? IDPenggunaUpdate
    {
        get { return iDPenggunaUpdate; }
    }

    public int IDTempat { get; set; }
    public int IDTempatPengirim { get; set; }
    public int IDStatusTransaksi { get; set; }
    public int IDJenisTransaksi { get; set; }
    public int IDKurir { get; set; }

    //TanggalOperasional OTOMATIS

    public DateTime TanggalTransaksi { get; set; }

    //TanggalUpdate OTOMATIS DATETIME.NOW

    public int JumlahTamu { get; set; }
    public int JumlahProduk
    {
        get
        {
            return Detail.Sum(item => item.Quantity);
        }
    }

    #region BERAT
    public decimal BeratSubtotal
    {
        get
        {
            return Detail.Sum(item => item.TotalBerat);
        }
    }
    public decimal BeratTotal
    {
        get
        {
            //BATAS TOLERANSI 0.2 DIBULATKAN KEBAWAH
            if ((BeratSubtotal - (decimal)0.2) < 0)
                return 1;
            else
                return Math.Ceiling(BeratSubtotal);
        }
    }
    public decimal BeratPembulatan
    {
        get
        {
            return BeratTotal - BeratSubtotal;
        }
    }
    #endregion

    #region BiayaTambahan1
    public EnumBiayaTambahan EnumBiayaTambahan1 { get; set; }
    public decimal NilaiBiayaTambahan1 { get; set; }
    public decimal BiayaTambahan1
    {
        get
        {
            switch (EnumBiayaTambahan1)
            {
                case EnumBiayaTambahan.Harga: return NilaiBiayaTambahan1;
                case EnumBiayaTambahan.Persentase: return Subtotal * NilaiBiayaTambahan1 / 100;
                default: return 0;
            }
        }
    }
    #endregion

    #region BiayaTambahan2
    public EnumBiayaTambahan EnumBiayaTambahan2 { get; set; }
    public decimal NilaiBiayaTambahan2 { get; set; }
    public decimal BiayaTambahan2
    {
        get
        {
            switch (EnumBiayaTambahan2)
            {
                case EnumBiayaTambahan.Harga: return NilaiBiayaTambahan2;
                case EnumBiayaTambahan.Persentase: return (Subtotal + BiayaTambahan1) * NilaiBiayaTambahan2 / 100;
                default: return 0;
            }
        }
    }
    #endregion

    #region BiayaTambahan3
    public EnumBiayaTambahan EnumBiayaTambahan3 { get; set; }
    public decimal NilaiBiayaTambahan3 { get; set; }
    public decimal BiayaTambahan3
    {
        get
        {
            switch (EnumBiayaTambahan3)
            {
                case EnumBiayaTambahan.Harga: return NilaiBiayaTambahan3;
                case EnumBiayaTambahan.Persentase: return (Subtotal + BiayaTambahan1 + BiayaTambahan2) * NilaiBiayaTambahan3 / 100;
                default: return 0;
            }
        }
    }
    #endregion

    #region BiayaTambahan4
    public EnumBiayaTambahan EnumBiayaTambahan4 { get; set; }
    public decimal NilaiBiayaTambahan4 { get; set; }
    public decimal BiayaTambahan4
    {
        get
        {
            switch (EnumBiayaTambahan4)
            {
                case EnumBiayaTambahan.Harga: return NilaiBiayaTambahan4;
                case EnumBiayaTambahan.Persentase: return (Subtotal + BiayaTambahan1 + BiayaTambahan2 + BiayaTambahan3) * NilaiBiayaTambahan4 / 100;
                default: return 0;
            }
        }
    }
    #endregion

    #region PotonganTransaksi
    public PilihanPotonganTransaksi EnumPotonganTransaksi { get; set; }
    public decimal NilaiPotonganTransaksi { get; set; }
    public decimal PotonganTransaksi
    {
        get
        {
            switch (EnumPotonganTransaksi)
            {
                case PilihanPotonganTransaksi.Harga: return NilaiPotonganTransaksi;
                case PilihanPotonganTransaksi.Persentase: return SebelumDiscountTransaksi * NilaiPotonganTransaksi / 100;
                default: return 0;
            }
        }
    }
    #endregion

    public decimal TotalPotonganHargaJualDetail
    {
        get
        {
            return Detail.Sum(item => item.TotalDiscount);
        }
    }
    public decimal TotalDiscountVoucher
    {
        get
        {
            return Voucher.Sum(item => item.Discount);
        }
    }

    #region IsPembulatan
    private bool isPembulatan;
    public bool IsPembulatan
    {
        get
        {
            return isPembulatan;
        }
    }
    #endregion

    #region Pembulatan
    private decimal pembulatan;
    public decimal Pembulatan
    {
        get
        {
            if (isPembulatan)
            {
                decimal sebelumPembulatan = SebelumDiscountTransaksi - PotonganTransaksi;
                decimal setelahPembulatan = Math.Round(sebelumPembulatan, 0, MidpointRounding.AwayFromZero);

                int panjang = setelahPembulatan.ToString().Length;
                decimal angkaTerakhir = 0;

                if (panjang > 1)
                    angkaTerakhir = Parse.Decimal(setelahPembulatan.ToString().Substring(panjang - 2));

                if (angkaTerakhir >= 50)
                    setelahPembulatan += 100 - angkaTerakhir; //PEMBULATAN KE ATAS
                else
                    setelahPembulatan -= angkaTerakhir; //PEMBULATAN KE BAWAH

                return sebelumPembulatan >= 0 ? setelahPembulatan - sebelumPembulatan : sebelumPembulatan - setelahPembulatan;
            }
            else
                return pembulatan;
        }
    }
    #endregion

    public decimal Subtotal
    {
        get
        {
            return Detail.Sum(item => item.Subtotal);
        }
    }
    public decimal TotalHargaBeli
    {
        get
        {
            return Detail.Sum(item => item.TotalHargaBeli);
        }
    }
    public decimal GrandTotal
    {
        get
        {
            return SebelumDiscountTransaksi - PotonganTransaksi + Pembulatan;
        }
    }
    public decimal TotalPembayaran
    {
        get
        {
            return Pembayaran.Sum(item => item.Total);
        }
    }
    public string Keterangan { get; set; }
    public string Station { get; set; }

    #region KodeTransfer
    private decimal kodeTransfer;
    public decimal KodeTransfer
    {
        get
        {
            return kodeTransfer;
        }
    }
    #endregion

    #region IDWMSTransaksi
    private Guid iDWMSTransaksi;
    public Guid IDWMSTransaksi
    {
        get
        {
            return iDWMSTransaksi;
        }
    }
    #endregion

    //TAMBAHAN

    public decimal SisaPembayaran
    {
        get
        {
            return GrandTotal - TotalPembayaran;
        }
    }

    //MENGHITUNG SEBELUM DISCOUNT TRANSAKSI
    public decimal SebelumDiscountTransaksi
    {
        get
        {
            return Subtotal + BiayaPengiriman + BiayaTambahan1 + BiayaTambahan2 + BiayaTambahan3 + BiayaTambahan4;
        }
    }

    #region PELANGGAN
    private TransaksiPelangganRetail_Model pelanggan;
    public TransaksiPelangganRetail_Model Pelanggan
    {
        get { return pelanggan; }
    }
    #endregion

    #region MEJA
    private Meja_Model meja;
    public Meja_Model Meja
    {
        get { return meja; }
    }
    #endregion

    #region DETAIL
    private List<TransaksiRetailDetail_Model> detail;
    public List<TransaksiRetailDetail_Model> Detail
    {
        get { return detail; }
    }
    #endregion

    #region VOUCHER
    private List<TransaksiVoucherRetail_Model> voucher;
    public List<TransaksiVoucherRetail_Model> Voucher
    {
        get { return voucher; }
    }
    #endregion

    #region PEMBAYARAN
    private List<TransaksiPembayaranRetail_Model> pembayaran;
    public List<TransaksiPembayaranRetail_Model> Pembayaran
    {
        get { return pembayaran; }
    }
    #endregion

    #region PRINT
    private List<TransaksiPrint_Model> print;
    public List<TransaksiPrint_Model> Print
    {
        get { return print; }
    }
    #endregion

    #region IDDetailTransaksi
    private int iDDetailTransaksiTemp;
    public int IDDetailTransaksiTemp
    {
        get { return ++iDDetailTransaksiTemp; }
    }
    #endregion

    #region IDPembayaranTransaksi
    private int iDPembayaranTransaksiTemp;
    public int IDPembayaranTransaksiTemp
    {
        get { return ++iDPembayaranTransaksiTemp; }
    }
    #endregion

    public bool StatusPrint { get; set; }
    public decimal BiayaPengirimanPerKg { get; set; }

    #region BiayaPengiriman
    private decimal biayaPengiriman;
    public decimal BiayaPengiriman
    {
        get
        {
            if (BiayaPengirimanPerKg > 0 && BeratTotal > 0)
                biayaPengiriman = BiayaPengirimanPerKg * BeratTotal;

            return biayaPengiriman;
        }

        set
        {
            biayaPengiriman = value;

            //RESET SEMUA BIAYA PENGIRIMAN
            if (biayaPengiriman == 0)
                BiayaPengirimanPerKg = 0;
        }
    }
    #endregion

    private int? idStatusTransaksiSebelumnya; //MENCATAT ID TRANSAKSI AWAL SEBELUM DIRUBAH

    //MEMBUKA TRANSAKSI YANG SUDAH TERSIMPAN DI DATABASE
    public Transaksi_Class(string idTransaksi, int idPenggunaUpdate)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Transaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == idTransaksi);

            if (Transaksi != null)
            {
                var TransaksiDetail = Transaksi.TBTransaksiDetails.ToArray();
                var TransaksiVoucher = Transaksi.TBTransaksiVouchers.ToArray();
                var TransaksiPembayaran = Transaksi.TBTransaksiJenisPembayarans.ToArray();

                IDTransaksi = Transaksi.IDTransaksi;

                if (Transaksi.IDPenggunaTransaksi.HasValue)
                    IDPenggunaTransaksi = Transaksi.IDPenggunaTransaksi.Value;

                iDPenggunaUpdate = idPenggunaUpdate;

                if (Transaksi.IDTempat.HasValue)
                    IDTempat = Transaksi.IDTempat.Value;

                if (Transaksi.IDTempatPengirim.HasValue)
                    IDTempatPengirim = Transaksi.IDTempatPengirim.Value;

                if (Transaksi.IDStatusTransaksi.HasValue)
                {
                    IDStatusTransaksi = Transaksi.IDStatusTransaksi.Value;
                    idStatusTransaksiSebelumnya = Transaksi.IDStatusTransaksi.Value; //MENCATAT ID STATUS TRANSAKSI SEBELUMNYA
                }

                if (Transaksi.IDJenisTransaksi.HasValue)
                    IDJenisTransaksi = Transaksi.IDJenisTransaksi.Value;

                IDKurir = Transaksi.IDKurir.Value;

                //TanggalOperasional //AUTO

                if (Transaksi.TanggalTransaksi.HasValue)
                    TanggalTransaksi = Transaksi.TanggalTransaksi.Value;

                //TanggalUpdate OTOMATIS DATETIME.NOW

                if (Transaksi.JumlahTamu.HasValue)
                    JumlahTamu = Transaksi.JumlahTamu.Value;

                //JumlahProduk //AUTO

                //BeratSubtotal //AUTO
                //BeratPembulatan //AUTO
                //BeratTotal //AUTO

                EnumBiayaTambahan1 = (EnumBiayaTambahan)Transaksi.EnumBiayaTambahan1;

                if (Transaksi.NilaiBiayaTambahan1.HasValue)
                    NilaiBiayaTambahan1 = Transaksi.NilaiBiayaTambahan1.Value;

                //BiayaTambahan1 //AUTO

                EnumBiayaTambahan2 = (EnumBiayaTambahan)Transaksi.EnumBiayaTambahan2;

                if (Transaksi.NilaiBiayaTambahan2.HasValue)
                    NilaiBiayaTambahan2 = Transaksi.NilaiBiayaTambahan2.Value;

                //BiayaTambahan2 //AUTO

                EnumBiayaTambahan3 = (EnumBiayaTambahan)Transaksi.EnumBiayaTambahan3;

                if (Transaksi.NilaiBiayaTambahan3.HasValue)
                    NilaiBiayaTambahan3 = Transaksi.NilaiBiayaTambahan3.Value;

                //BiayaTambahan3 //AUTO

                EnumBiayaTambahan4 = (EnumBiayaTambahan)Transaksi.EnumBiayaTambahan4;

                if (Transaksi.NilaiBiayaTambahan4.HasValue)
                    NilaiBiayaTambahan4 = Transaksi.NilaiBiayaTambahan4.Value;

                //BiayaTambahan4 //AUTO

                EnumPotonganTransaksi = (PilihanPotonganTransaksi)Transaksi.EnumPotonganTransaksi;

                if (Transaksi.PersentasePotonganTransaksi.HasValue)
                    NilaiPotonganTransaksi = Transaksi.PersentasePotonganTransaksi.Value;

                //PotonganTransaksi //AUTO
                //TotalPotonganHargaJualDetail //AUTO
                //TotalDiscountVoucher //AUTO

                if (Transaksi.BiayaPengiriman.HasValue)
                    BiayaPengiriman = Transaksi.BiayaPengiriman.Value;

                //Pembulatan  //AUTO
                //Subtotal //AUTO
                //GrandTotal //AUTO
                //TotalPembayaran AUTO

                kodeTransfer = Transaksi.KodeTransfer;

                Keterangan = Transaksi.Keterangan;

                iDWMSTransaksi = Transaksi._IDWMSTransaksi;

                //SebelumDiscountTransaksi //AUTO

                //PEMBULATAN
                StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();
                isPembulatan = Parse.Bool(StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.Pembulatan));

                if (!isPembulatan)
                    pembulatan = Transaksi.Pembulatan.Value;

                pelanggan = new TransaksiPelangganRetail_Model((int)Transaksi.IDPelanggan);

                meja = new Meja_Model(Transaksi.IDMeja.Value);

                Pengguna_Class ClassPengguna = new Pengguna_Class(db, true);
                var _tempPengguna = ClassPengguna.Cari(IDPenggunaUpdate.Value);

                Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(_tempPengguna.IDGrupPengguna);
                bool ubahQuantity = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.VoidItem);

                //MEMASUKKAN DETAIL TRANSAKSI
                detail = new List<TransaksiRetailDetail_Model>();

                Detail.AddRange(TransaksiDetail.Select(item => new TransaksiRetailDetail_Model
                {
                    IDDetailTransaksi = IDDetailTransaksiTemp,
                    IDKombinasiProduk = item.IDKombinasiProduk,
                    IDStokProduk = item.IDStokProduk,
                    Barcode = item.TBKombinasiProduk.KodeKombinasiProduk,
                    Nama = item.TBKombinasiProduk.Nama,
                    Quantity = item.Quantity,
                    Berat = item.Berat,
                    HargaBeliKotor = item.HargaBeliKotor,
                    HargaJual = item.HargaJual,
                    DiscountStore = item.DiscountStore,
                    DiscountKonsinyasi = item.DiscountKonsinyasi,
                    Keterangan = item.Keterangan,
                    UbahQuantity = ubahQuantity
                }));

                voucher = new List<TransaksiVoucherRetail_Model>();

                foreach (var item in TransaksiVoucher)
                {
                    TambahVoucher(item.TBVoucher.Kode);
                }

                pembayaran = new List<TransaksiPembayaranRetail_Model>();

                Pembayaran.AddRange(TransaksiPembayaran.Select(item => new TransaksiPembayaranRetail_Model
                {
                    IDTransaksiJenisPembayaran = item.IDTransaksiJenisPembayaran,
                    IDJenisPembayaran = item.IDJenisPembayaran,
                    IDPengguna = item.IDPengguna,
                    Tanggal = item.Tanggal.Value,
                    Bayar = item.Bayar.Value,
                    Keterangan = item.Keterangan
                }));

                print = new List<TransaksiPrint_Model>();
            }
        }

        StatusPrint = false; //DEFAULT
    }

    //MEMBUAT TRANSAKSI DENGAN DEFAULT VALUE
    public Transaksi_Class(int idPenggunaTransaksi, int idTempat, DateTime tanggalTransaksi)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //IDTransaksi

            IDPenggunaTransaksi = idPenggunaTransaksi;

            //IDPenggunaUpdate //BISA NULL

            IDTempat = idTempat;

            IDTempatPengirim = idTempat;

            IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;

            IDJenisTransaksi = (int)EnumJenisTransaksi.PointOfSales;

            IDKurir = 1; //DEFAULT KURIR

            //TanggalOperasional //AUTO

            TanggalTransaksi = tanggalTransaksi;

            //TanggalUpdate OTOMATIS DATETIME.NOW

            JumlahTamu = 1;

            //JumlahProduk //AUTO

            //BeratSubtotal //AUTO
            //BeratPembulatan //AUTO
            //BeratTotal //AUTO

            var Tempat = db.TBTempats.FirstOrDefault(item => item.IDTempat == idTempat);

            EnumBiayaTambahan1 = (EnumBiayaTambahan)Tempat.EnumBiayaTambahan1;
            NilaiBiayaTambahan1 = (decimal)Tempat.BiayaTambahan1;

            //BiayaTambahan1 //AUTO

            EnumBiayaTambahan2 = (EnumBiayaTambahan)Tempat.EnumBiayaTambahan2;
            NilaiBiayaTambahan2 = (decimal)Tempat.BiayaTambahan2;

            //BiayaTambahan2 //AUTO

            EnumBiayaTambahan3 = (EnumBiayaTambahan)Tempat.EnumBiayaTambahan3;
            NilaiBiayaTambahan3 = (decimal)Tempat.BiayaTambahan3;

            //BiayaTambahan3 //AUTO

            EnumBiayaTambahan4 = (EnumBiayaTambahan)Tempat.EnumBiayaTambahan4;
            NilaiBiayaTambahan4 = (decimal)Tempat.BiayaTambahan4;

            //BiayaTambahan4 //AUTO

            EnumPotonganTransaksi = PilihanPotonganTransaksi.TidakAda;

            NilaiPotonganTransaksi = 0;

            //PotonganTransaksi //AUTO
            //TotalPotonganHargaJualDetail //AUTO
            //TotalDiscountVoucher //AUTO

            BiayaPengiriman = 0;

            //Pembulatan  //AUTO
            //Subtotal //AUTO
            //GrandTotal //AUTO
            //TotalPembayaran AUTO

            Keterangan = "";

            //SebelumDiscountTransaksi AUTO

            //PEMBULATAN
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();
            isPembulatan = Parse.Bool(StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.Pembulatan).Pengaturan);

            pelanggan = new TransaksiPelangganRetail_Model((int)EnumPelanggan.GeneralCustomer);

            //DEFAULT MEJA : TAKE AWAY
            meja = new Meja_Model((int)EnumMeja.TakeAway);

            detail = new List<TransaksiRetailDetail_Model>();

            voucher = new List<TransaksiVoucherRetail_Model>();

            pembayaran = new List<TransaksiPembayaranRetail_Model>();

            print = new List<TransaksiPrint_Model>();
        }

        StatusPrint = false; //DEFAULT
    }

    public string ConfirmTransaksi(DataClassesDatabaseDataContext db)
    {
        return ConfirmTransaksi(db, " ", true);
    }

    public string ConfirmTransaksi(DataClassesDatabaseDataContext db, string idTransaksi, bool statusStok)
    {
        StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

        string _IDTransaksi = idTransaksi; //RETURN ID TRANSAKSI
        int? _nomorTransaksi = 0; //RETURN AUTO INCREMENT

        //TRUE : MERUBAH
        //FALSE : MENAMBAH BARU
        bool statusUbah = true;

        TBTransaksi Transaksi;

        Pengguna_Class ClassPengguna = new Pengguna_Class(db, true);
        var tempPengguna = ClassPengguna.Cari(IDPenggunaTransaksi);

        Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(tempPengguna.IDGrupPengguna);

        var KonfigurasiPOSUniqueCode = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSUniqueCode);

        if (string.IsNullOrWhiteSpace(IDTransaksi))
        {
            statusUbah = false;

            //TRANSAKSI BARU
            //INSERT TRANSAKSI MELALUI STORE PROCEDURE
            db.Proc_InsertTransaksi(IDTempat, TanggalTransaksi, ref _IDTransaksi, ref _nomorTransaksi);

            IDTransaksi = _IDTransaksi;
        }

        //MENCARI TRANSAKSI YANG BARU DIMASUKKAN
        Transaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == IDTransaksi);

        //IDTransaksi

        Transaksi.IDPenggunaTransaksi = IDPenggunaTransaksi;

        if (IDPenggunaUpdate.HasValue)
            Transaksi.IDPenggunaUpdate = IDPenggunaUpdate.Value;

        Transaksi.IDTempat = IDTempat;
        Transaksi.IDTempatPengirim = IDTempatPengirim;

        //JIKA DETAIL TRANSAKSI TIDAK ADA MAKA TRANSAKSI OTOMATIS CANCELED
        if (Detail.Count == 0)
            IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;

        //JIKA STATUS SEBELUMNYA BUKAN CANCELED DAN MELAKUKAN CANCELED MAKA TERJADI VOID
        if (Transaksi.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled && IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
            PrintOrder(PilihanStatusPrint.Void);

        Transaksi.IDStatusTransaksi = IDStatusTransaksi;

        Transaksi.IDJenisTransaksi = IDJenisTransaksi; //POINT OF SALES

        Transaksi.IDKurir = IDKurir;

        #region TRANSAKSI BARU
        if (!statusUbah)
        {
            //JIKA UPDATE : TANGGAL OPERASIONAL DAN TANGGAL TRANSAKSI TIDAK BERUBAH

            string KonfigurasiJamBuka = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.JamBuka).Pengaturan;
            string KonfigurasiJamTutup = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.JamTutup).Pengaturan;

            TimeSpan JamBuka = TimeSpan.Parse(KonfigurasiJamBuka);
            TimeSpan JamTutup = TimeSpan.Parse(KonfigurasiJamTutup);

            if (JamBuka > JamTutup)
            {
                //JIKA JAM BUKA LEBIH BESAR DARI JAM TUTUP : GANTI HARI
                //JIKA TRANSAKSI LEBIH BESAR DARI JAM BUKA DAN TANGGAL TRANSAKSI SUDAH LEWAT 00:00 MAKA HARI MUNDUR
                if (TanggalTransaksi.TimeOfDay < JamBuka && TanggalTransaksi.TimeOfDay >= TimeSpan.Parse("00:00"))
                    Transaksi.TanggalOperasional = TanggalTransaksi.Date.AddDays(-1);
                else
                    Transaksi.TanggalOperasional = TanggalTransaksi;
            }
            else
                Transaksi.TanggalOperasional = TanggalTransaksi; //JAM BUKA DAN JAM TUTUP PADA HARI YANG SAMA

            Transaksi.TanggalTransaksi = TanggalTransaksi;

            #region UNIQUE CODE
            //MENGAMBIL KODE UNIK DARI 3 ANGKA DI BELAKANG INCREMENT TRANSAKSI
            if (KonfigurasiPOSUniqueCode)
            {
                if ((_nomorTransaksi.Value.ToString().Length - 3) < 0)
                {
                    Transaksi.Pembulatan = _nomorTransaksi.Value;
                    PembulatanJurnal = _nomorTransaksi.Value;
                }
                else
                {
                    Transaksi.Pembulatan = Parse.Decimal(_nomorTransaksi.Value.ToString().Substring(_nomorTransaksi.Value.ToString().Length - 3));
                    PembulatanJurnal = Parse.Decimal(_nomorTransaksi.Value.ToString().Substring(_nomorTransaksi.Value.ToString().Length - 3));
                }
            }
            #endregion

            #region ANTISIPASI MEJA DOUBLE
            if (IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment && Meja.IDMeja != (int)EnumMeja.WithoutTable && Meja.IDMeja != (int)EnumMeja.TakeAway)
            {
                //JIKA STATUSNYA ORDER DAN IDMEJA BUKAN 1 DAN 2
                //MENCARI APAKAH ADA TRANSAKSI SELAIN CANCEL DAN COMPLETE DI MEJA TUJUAN
                bool MejaTerisi = db.TBTransaksis
                    .FirstOrDefault(item =>
                        item.IDMeja == Meja.IDMeja &&
                        item.IDStatusTransaksi != (int)EnumStatusTransaksi.Complete &&
                        item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled) != null;

                if (MejaTerisi)
                {
                    Keterangan = "EXTEND " + Meja.Nama + " " + Keterangan;
                    Meja.IDMeja = (int)EnumMeja.WithoutTable;
                }
            }
            #endregion

            //KODE TRANSFER
            if (_nomorTransaksi.Value.ToString().Length <= 2)
                kodeTransfer = 100 + _nomorTransaksi.Value;
            else
                kodeTransfer = 100 + Parse.Decimal(_nomorTransaksi.Value.ToString().Substring(_nomorTransaksi.Value.ToString().Length - 2));

            Transaksi.KodeTransfer = kodeTransfer;

            //IDWMSTransaksi
            iDWMSTransaksi = Guid.NewGuid();
            Transaksi._IDWMSTransaksi = iDWMSTransaksi;
        }
        #endregion

        Transaksi.TanggalUpdate = DateTime.Now;

        Transaksi.JumlahTamu = JumlahTamu;

        Transaksi.JumlahProduk = JumlahProduk;

        Transaksi.BeratSubtotal = BeratSubtotal;
        Transaksi.BeratPembulatan = BeratPembulatan;
        //BeratTotal

        Transaksi.EnumBiayaTambahan1 = (int)EnumBiayaTambahan1;
        Transaksi.NilaiBiayaTambahan1 = NilaiBiayaTambahan1;
        Transaksi.BiayaTambahan1 = BiayaTambahan1;

        Transaksi.EnumBiayaTambahan2 = (int)EnumBiayaTambahan2;
        Transaksi.NilaiBiayaTambahan2 = NilaiBiayaTambahan2;
        Transaksi.BiayaTambahan2 = BiayaTambahan2;

        Transaksi.EnumBiayaTambahan3 = (int)EnumBiayaTambahan3;
        Transaksi.NilaiBiayaTambahan3 = NilaiBiayaTambahan3;
        Transaksi.BiayaTambahan3 = BiayaTambahan3;

        Transaksi.EnumBiayaTambahan4 = (int)EnumBiayaTambahan4;
        Transaksi.NilaiBiayaTambahan4 = NilaiBiayaTambahan4;
        Transaksi.BiayaTambahan4 = BiayaTambahan4;

        Transaksi.EnumPotonganTransaksi = (int)EnumPotonganTransaksi;

        Transaksi.PersentasePotonganTransaksi = NilaiPotonganTransaksi;

        Transaksi.PotonganTransaksi = PotonganTransaksi;

        Transaksi.TotalPotonganHargaJualDetail = TotalPotonganHargaJualDetail;

        Transaksi.TotalDiscountVoucher = TotalDiscountVoucher;

        Transaksi.BiayaPengiriman = BiayaPengiriman;

        if (!KonfigurasiPOSUniqueCode)
            Transaksi.Pembulatan = Pembulatan;

        if (isRetur == true && KonfigurasiPOSUniqueCode == true)
        {
            Transaksi.Pembulatan = 0;
            PembulatanJurnal = 0;
        }

        Transaksi.Subtotal = Subtotal;

        //GrandTotal //AUTO

        Transaksi.TotalPembayaran = TotalPembayaran;

        Transaksi.Keterangan = Keterangan;

        //SebelumDiscountTransaksi AUTO

        Transaksi.IDPelanggan = Pelanggan.IDPelanggan;
        Transaksi.IDAlamat = Pelanggan.IDAlamat;

        var MejaTransaksi = db.TBMejas.FirstOrDefault(item => item.IDMeja == Meja.IDMeja);

        if (IDStatusTransaksi == (int)EnumStatusTransaksi.Complete || IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
            MejaTransaksi.IDStatusMeja = (int)EnumStatusMeja.Open;
        else
            MejaTransaksi.IDStatusMeja = (int)EnumStatusMeja.Booked;

        Transaksi.TBMeja = MejaTransaksi;

        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

        if (statusUbah)
        {
            var TransaksiDetail = Transaksi.TBTransaksiDetails.ToArray();

            #region PRINT
            if (StatusPrint)
            {
                //GROUP BY TRANSAKSI DETAIL DATABASE
                var DetailDatabase = TransaksiDetail
                    .GroupBy(item => new
                    {
                        item.IDKombinasiProduk,
                        item.Keterangan
                    })
                    .Select(item => new
                    {
                        item.Key,
                        Quantity = item.Sum(item2 => item2.Quantity)
                    })
                    .Where(item => item.Quantity != 0)
                    .ToList();

                //GROUP BY TRANSAKSI DETAIL CLASS
                var DetailClass = Detail
                    .GroupBy(item => new
                    {
                        item.IDKombinasiProduk,
                        item.Keterangan
                    })
                    .Select(item => new
                    {
                        item.Key,
                        Quantity = item.Sum(item2 => item2.Quantity)
                    })
                    .Where(item => item.Quantity != 0)
                    .ToList();

                int jumlahData = DetailDatabase.Count; //JUMLAH TRANSAKSI DETAIL DATABASE
                int index = 0;

                while (index < jumlahData)
                {
                    var _tempDetailDatabase = DetailDatabase[index];

                    //CARI APAKAH DI CLASS ADA DETAIL YANG SAMA DENGAN DI DATABASE
                    var _tempDetailClass = DetailClass
                       .FirstOrDefault(item2 =>
                           item2.Key.IDKombinasiProduk == _tempDetailDatabase.Key.IDKombinasiProduk &&
                           item2.Key.Keterangan == _tempDetailDatabase.Key.Keterangan);

                    index++;

                    if (_tempDetailClass != null)
                    {
                        //JIKA DATA DITEMUKAN

                        if (_tempDetailDatabase.Quantity > _tempDetailClass.Quantity)
                        {
                            //JIKA DETAIL DATABASE LEBIH BANYAK DARI DETAIL CLASS : VOID
                            print.Add(new TransaksiPrint_Model
                            {
                                EnumStatusPrint = PilihanStatusPrint.Void,
                                IDKombinasiProduk = _tempDetailDatabase.Key.IDKombinasiProduk,
                                Quantity = _tempDetailClass.Quantity - _tempDetailDatabase.Quantity,
                                Keterangan = _tempDetailDatabase.Key.Keterangan,
                                Station = Station
                            });
                        }
                        else if (_tempDetailClass.Quantity > _tempDetailDatabase.Quantity)
                        {
                            //JIKA DETAIL CLASS LEBIH BANYAK DARI DETAIL DATABASE : TAMBAHAN
                            print.Add(new TransaksiPrint_Model
                            {
                                EnumStatusPrint = PilihanStatusPrint.Tambahan,
                                IDKombinasiProduk = _tempDetailClass.Key.IDKombinasiProduk,
                                Quantity = _tempDetailClass.Quantity - _tempDetailDatabase.Quantity,
                                Keterangan = _tempDetailClass.Key.Keterangan,
                                Station = Station
                            });
                        }

                        //ELIMINASI
                        DetailDatabase.Remove(_tempDetailDatabase);
                        DetailClass.Remove(_tempDetailClass);

                        jumlahData = DetailDatabase.Count;
                        index--;
                    }
                }

                print.AddRange(DetailDatabase.Select(item => new TransaksiPrint_Model
                {
                    EnumStatusPrint = PilihanStatusPrint.Void,
                    IDKombinasiProduk = item.Key.IDKombinasiProduk,
                    Keterangan = item.Key.Keterangan,
                    Quantity = item.Quantity * -1,
                    Station = Station
                }));

                print.AddRange(DetailClass.Select(item => new TransaksiPrint_Model
                {
                    EnumStatusPrint = PilihanStatusPrint.Tambahan,
                    IDKombinasiProduk = item.Key.IDKombinasiProduk,
                    Keterangan = item.Key.Keterangan,
                    Quantity = item.Quantity,
                    Station = Station
                }));
            }
            #endregion

            #region MENGHAPUS DETAIL LAMA
            if (statusStok)
            {
                foreach (var item in TransaksiDetail)
                {
                    if (item.Quantity != 0)
                    {
                        EnumJenisPerpindahanStok tempStatus;

                        if (item.Quantity > 0)
                        {
                            if (IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                                tempStatus = EnumJenisPerpindahanStok.TransaksiBatal;
                            else
                                tempStatus = EnumJenisPerpindahanStok.PerubahanTransaksi;
                        }
                        else
                        {
                            if (IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                                tempStatus = EnumJenisPerpindahanStok.ReturDariPembeliBatal;
                            else
                                tempStatus = EnumJenisPerpindahanStok.PerubahanReturDariPembeli;
                        }

                        StokProduk_Class.BertambahBerkurang(IDTempat, IDPenggunaUpdate.Value, item.IDKombinasiProduk, item.Quantity, item.HargaBeli.Value, item.HargaJual, tempStatus, "Transaksi #" + Transaksi.IDTransaksi);
                    }
                }

                db.TBTransaksiDetails.DeleteAllOnSubmit(TransaksiDetail);
            }
            
            #endregion

            #region MENGHAPUS VOUCHER LAMA
            var TransaksiVoucher = Transaksi.TBTransaksiVouchers.ToArray();

            foreach (var item in TransaksiVoucher)
            {
                item.TBVoucher.Pemakaian += 1;
            }

            db.TBTransaksiVouchers.DeleteAllOnSubmit(TransaksiVoucher);
            #endregion

            #region MENGHAPUS PEMBAYARAN LAMA
            //MENGHAPUS PEMBAYARAN
            var TransaksiPembayaran = Transaksi.TBTransaksiJenisPembayarans.ToArray();

            db.TBTransaksiJenisPembayarans.DeleteAllOnSubmit(TransaksiPembayaran);
            #endregion
        }
        else if (StatusPrint)
            PrintOrder(PilihanStatusPrint.Order);

        #region DETAIL
        if (statusStok)
        {
            var TipePenghitunganConsignment = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.TipePenghitunganConsignment).Pengaturan;

            //PENGHITUNGAN CONSIGNMENT
            if (IDStatusTransaksi == (int)EnumStatusTransaksi.Complete && TipePenghitunganConsignment.ToInt() == (int)EnumTipePenghitunganConsignment.ConsignmentSubtotal)
                PenghitunganConsignment();

            Transaksi.TBTransaksiDetails.AddRange(Detail.Select(item => new TBTransaksiDetail
            {
                //IDDetailTransaksi
                //IDTransaksi
                IDKombinasiProduk = item.IDKombinasiProduk,
                IDStokProduk = item.IDStokProduk,
                Quantity = item.Quantity,
                Berat = item.Berat,
                HargaBeliKotor = item.HargaBeliKotor,
                //HargaBeli
                HargaJual = item.HargaJual,
                //HargaJualBersih
                DiscountStore = item.DiscountStore,
                DiscountKonsinyasi = item.DiscountKonsinyasi,
                //Discount
                //Revenue
                //TotalBerat
                //TotalHargaBeliKotor
                //TotalHargaBeli
                //TotalHargaJual
                //TotalDiscount
                //TotalRevenue
                //Subtotal
                Keterangan = item.Keterangan
            }));

            if (IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled)
            {
                //JIKA TIDAK STATUS CANCELED MAKA STOK BERKURANG LAGI

                foreach (var item in Detail)
                {
                    if (item.Quantity != 0)
                    {
                        EnumJenisPerpindahanStok tempStatus;

                        if (item.Quantity > 0)
                            tempStatus = EnumJenisPerpindahanStok.Transaksi;
                        else
                            tempStatus = EnumJenisPerpindahanStok.ReturDariPembeli;

                        StokProduk_Class.BertambahBerkurang(IDTempat, IDPenggunaUpdate.HasValue ? IDPenggunaUpdate.Value : IDPenggunaTransaksi, item.IDKombinasiProduk, item.Quantity, item.HargaBeli, item.HargaJual, tempStatus, "Transaksi #" + Transaksi.IDTransaksi);
                    }
                }
            }
        }
        #endregion

        #region PRINT
        Transaksi.TBTransaksiPrints.AddRange(Print.Select(item => new TBTransaksiPrint
        {
            EnumStatusPrint = (int)item.EnumStatusPrint,
            IDKombinasiProduk = item.IDKombinasiProduk,
            Keterangan = item.Keterangan,
            Quantity = item.Quantity,
            Station = item.Station

        }));
        #endregion

        #region VOUCHER
        Transaksi.TBTransaksiVouchers.AddRange(Voucher.Select(item => new TBTransaksiVoucher
        {
            IDVoucher = item.IDVoucher
        }));

        foreach (var item in Voucher)
        {
            db.TBVouchers.FirstOrDefault(item2 => item2.IDVoucher == item.IDVoucher).Pemakaian -= 1;
        }
        #endregion

        #region HAPUS PERUBAHAN TABLE
        if (Pembayaran != null && Pembayaran.Count > 0)
        {
            var _tempPembayaran = Pembayaran.FirstOrDefault();

            Transaksi.IDPenggunaPembayaran = _tempPembayaran.IDPengguna;
            Transaksi.IDJenisPembayaran = _tempPembayaran.IDJenisPembayaran;
            Transaksi.IDJenisBebanBiaya = (int)_tempPembayaran.IDJenisBebanBiaya;
            Transaksi.TanggalPembayaran = _tempPembayaran.Tanggal;
            Transaksi.EnumBiayaJenisPembayaran = (int)_tempPembayaran.EnumBiayaJenisPembayaran;
            Transaksi.PersentaseBiayaJenisPembayaran = _tempPembayaran.NilaiBiayaJenisPembayaran;
            Transaksi.BiayaJenisPembayaran = _tempPembayaran.BiayaJenisPembayaran;
        }
        else
        {
            Transaksi.IDPenggunaPembayaran = IDPenggunaTransaksi;
            Transaksi.IDJenisPembayaran = (int)EnumJenisPembayaran.Cash;
            Transaksi.IDJenisBebanBiaya = 1;
            Transaksi.TanggalPembayaran = TanggalTransaksi;
            Transaksi.EnumBiayaJenisPembayaran = (int)PilihanBiayaJenisPembayaran.TidakAda;
            Transaksi.PersentaseBiayaJenisPembayaran = 0;
            Transaksi.BiayaJenisPembayaran = 0;
        }
        #endregion

        #region PEMBAYARAN
        if (Pembayaran != null && Pembayaran.Count > 0)
        {
            Transaksi.TBTransaksiJenisPembayarans.AddRange(Pembayaran.Select(item => new TBTransaksiJenisPembayaran
            {
                Bayar = item.Bayar,
                BiayaJenisPembayaran = item.BiayaJenisPembayaran,
                EnumBiayaJenisPembayaran = (int)item.EnumBiayaJenisPembayaran,
                IDJenisBebanBiaya = (int)item.IDJenisBebanBiaya,
                IDJenisPembayaran = item.IDJenisPembayaran,
                IDPengguna = item.IDPengguna,
                Keterangan = item.Keterangan,
                PersentaseBiayaJenisPembayaran = item.NilaiBiayaJenisPembayaran,
                Tanggal = item.Tanggal
            }));
        }
        #endregion

        #region AKUNTANSI
        ////////////int IDAkunPembayaran = 0;
        ////////////var KonfigurasiAkun = db.TBKonfigurasiAkuns.Where(item => item.IDTempat == 1);

        ////////////if (KonfigurasiAkun != null)
        ////////////{
        ////////////    if (Pembayaran.Count > 0)
        ////////////        IDAkunPembayaran = (int)db.TBKonfigurasiAkuns.FirstOrDefault(item => item.Nama == Pembayaran.FirstOrDefault().IDJenisPembayaran.ToString()
        ////////////        && item.IDTempat == 1).IDAkun;

        ////////////    #region BARU > COMPLETE
        ////////////    //TRANSAKSI BARU > COMPLETED
        ////////////    if (IDStatusTransaksi == (int)EnumStatusTransaksi.Complete
        ////////////        && Pembayaran.Count > 0 && !idStatusTransaksiSebelumnya.HasValue)
        ////////////    {
        ////////////        var _pembayaran = Pembayaran.FirstOrDefault();

        ////////////        TBJurnal Jurnal = new TBJurnal
        ////////////        {
        ////////////            IDTempat = Transaksi.IDTempat,
        ////////////            Tanggal = _pembayaran.Tanggal,
        ////////////            Keterangan = isRetur == true ? Transaksi.Keterangan : "",
        ////////////            IDPengguna = _pembayaran.IDPengguna,
        ////////////            Referensi = IDTransaksi
        ////////////        };

        ////////////        #region DISCOUNT
        ////////////        //DEBIT     : KAS & POTONGAN/DISKON PENJUALAN & HPP
        ////////////        //KREDIT    : PENJUALAN & PERSEDIAAN
        ////////////        if (TotalPotonganHargaJualDetail > 0)
        ////////////        {
        ////////////            if (Pembayaran.Count > 1)
        ////////////            {
        ////////////                foreach (var item in Pembayaran)
        ////////////                {
        ////////////                    //387 - KAS
        ////////////                    Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                    {
        ////////////                        IDAkun = (int)db.TBKonfigurasiAkuns.FirstOrDefault(x => x.Nama == item.IDJenisPembayaran.ToString()
        ////////////                        && x.IDTempat == 1).IDAkun,
        ////////////                        Debit = item.Bayar,
        ////////////                        Kredit = 0
        ////////////                    });
        ////////////                }
        ////////////            }
        ////////////            else if (Pembayaran.Count == 1)
        ////////////            {
        ////////////                //387 - KAS
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = IDAkunPembayaran,
        ////////////                    Debit = GrandTotal,
        ////////////                    Kredit = 0
        ////////////                });
        ////////////            }

        ////////////            //403 - POTONGAN/DISKON PENJUALAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "POTONGAN/DISKON PENJUALAN").IDAkun,
        ////////////                Debit = TotalPotonganHargaJualDetail,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });


        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + TotalPotonganHargaJualDetail + BiayaPengiriman + Pembulatan
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });
        ////////////        }
        ////////////        #endregion

        ////////////        #region NO DISCOUNT
        ////////////        //DEBIT     : KAS & HPP
        ////////////        //KREDIT    : PENJUALAN & PERSEDIAAN && TAX && SERVICES
        ////////////        else
        ////////////        {
        ////////////            if (Pembayaran.Count > 1)
        ////////////            {
        ////////////                foreach (var item in Pembayaran)
        ////////////                {
        ////////////                    //387 - KAS
        ////////////                    Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                    {
        ////////////                        IDAkun = (int)db.TBKonfigurasiAkuns.FirstOrDefault(x => x.Nama == item.IDJenisPembayaran.ToString()
        ////////////                        && x.IDTempat == 1).IDAkun,
        ////////////                        Debit = item.Bayar,
        ////////////                        Kredit = 0
        ////////////                    });
        ////////////                }
        ////////////            }
        ////////////            else if (Pembayaran.Count == 1)
        ////////////            {
        ////////////                //387 - KAS
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = IDAkunPembayaran,
        ////////////                    Debit = GrandTotal,
        ////////////                    Kredit = 0
        ////////////                });
        ////////////            }

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });


        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + Pembulatan + PembulatanJurnal
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });
        ////////////        }
        ////////////        #endregion

        ////////////        db.TBJurnals.InsertOnSubmit(Jurnal);
        ////////////    }
        ////////////    #endregion

        ////////////    #region BARU > AWAITING PAYMENT
        ////////////    //TRANSAKSI BARU > AWAITING PAYMENT
        ////////////    else if (IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment
        ////////////        && Pembayaran.Count == 0 && !idStatusTransaksiSebelumnya.HasValue)
        ////////////    {
        ////////////        TBJurnal Jurnal = new TBJurnal
        ////////////        {
        ////////////            IDTempat = Transaksi.IDTempat,
        ////////////            Tanggal = TanggalTransaksi,
        ////////////            Keterangan = "",
        ////////////            IDPengguna = IDPenggunaTransaksi,
        ////////////            Referensi = IDTransaksi
        ////////////        };

        ////////////        #region DISCOUNT
        ////////////        //DEBIT     : PIUTANG AFILIASI & POTONGAN/DISKON PENJUALAN & HPP
        ////////////        //KREDIT    : PERSEDIAAN & PENJUALAN
        ////////////        if (TotalPotonganHargaJualDetail > 0)
        ////////////        {
        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + TotalPotonganHargaJualDetail + BiayaPengiriman + Pembulatan + PembulatanJurnal
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });

        ////////////            //412 - PIUTANG AFILIASI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = GrandTotal + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //403 - POTONGAN/DISKON PENJUALAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "POTONGAN/DISKON PENJUALAN").IDAkun,
        ////////////                Debit = TotalPotonganHargaJualDetail,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });
        ////////////        }
        ////////////        #endregion

        ////////////        #region NO DISCOUNT
        ////////////        //DEBIT     : PIUTANG AFILIASI & HPP
        ////////////        //KREDIT    : PERSEDIAAN & PENJUALAN
        ////////////        else
        ////////////        {
        ////////////            //412 - PIUTANG AFILIASI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = GrandTotal + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + Pembulatan + PembulatanJurnal
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });


        ////////////        }
        ////////////        #endregion

        ////////////        db.TBJurnals.InsertOnSubmit(Jurnal);
        ////////////    }
        ////////////    #endregion

        ////////////    #region AWAITING PAYMENT > COMPLETED
        ////////////    //TRANSAKSI AWAITING PAYMENT > COMPLETED
        ////////////    else if (IDStatusTransaksi == (int)EnumStatusTransaksi.Complete
        ////////////        && Pembayaran.Count > 0
        ////////////        && idStatusTransaksiSebelumnya.HasValue
        ////////////        && idStatusTransaksiSebelumnya == (int)EnumStatusTransaksi.AwaitingPayment)
        ////////////    {
        ////////////        var _pembayaran = Pembayaran.FirstOrDefault();

        ////////////        TBJurnal Jurnal = new TBJurnal
        ////////////        {
        ////////////            IDTempat = Transaksi.IDTempat,
        ////////////            Tanggal = _pembayaran.Tanggal,
        ////////////            Keterangan = "",
        ////////////            IDPengguna = _pembayaran.IDPengguna,
        ////////////            Referensi = IDTransaksi
        ////////////        };

        ////////////        #region DISCOUNT
        ////////////        //DEBIT     : KAS
        ////////////        //KREDIT    : PIUTANG  
        ////////////        if (TotalPotonganHargaJualDetail > 0)
        ////////////        {

        ////////////            #region EDIT JURNAL PAS AWAITING PAYMENT NYA HARUS DI EDIT, KARENA ADA PERUBAHAN DISCOUNT DI AKHIR
        ////////////            var DataJurnalAwaitingPayment = db.TBJurnals.FirstOrDefault(dataJurnal => dataJurnal.Referensi == Transaksi.IDTransaksi && dataJurnal.TBJurnalDetails.Count > 3);
        ////////////            db.TBJurnalDetails.DeleteAllOnSubmit(DataJurnalAwaitingPayment.TBJurnalDetails);
        ////////////            db.SubmitChanges();

        ////////////            //388 - Penjualan
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + TotalPotonganHargaJualDetail + BiayaPengiriman + Pembulatan + PembulatanJurnal
        ////////////            });

        ////////////            //TAX
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });

        ////////////            //412 - PIUTANG
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = GrandTotal + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //403 - POTONGAN/DISKON PENJUALAN
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "POTONGAN/DISKON PENJUALAN").IDAkun,
        ////////////                Debit = TotalPotonganHargaJualDetail,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            DataJurnalAwaitingPayment.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            db.TBJurnalDetails.InsertAllOnSubmit(DataJurnalAwaitingPayment.TBJurnalDetails);
        ////////////            #endregion

        ////////////            #region ADD JURNAL PEMBAYARAN
        ////////////            //387 - KAS
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = IDAkunPembayaran,
        ////////////                Debit = GrandTotal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //412 - PIUTANG
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = GrandTotal
        ////////////            });
        ////////////            #endregion
        ////////////        }
        ////////////        #endregion

        ////////////        #region NO DISCOUNT
        ////////////        else
        ////////////        {
        ////////////            //DEBIT     : KAS
        ////////////            //KREDIT    : PIUTANG AFILIASI
        ////////////            //387 - KAS
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = IDAkunPembayaran,
        ////////////                Debit = GrandTotal + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //412 - PIUTANG AFILIASI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = GrandTotal + PembulatanJurnal
        ////////////            });

        ////////////        }
        ////////////        #endregion

        ////////////        db.TBJurnals.InsertOnSubmit(Jurnal);
        ////////////    }
        ////////////    #endregion

        ////////////    #region AWAITING PAYMENT > AWAITING PAYMENT (UBAH ORDER)
        ////////////    //TRANSAKSI AWAITING PAYMENT > AWAITING PAYMENT
        ////////////    else if (IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment
        ////////////        && idStatusTransaksiSebelumnya == (int)EnumStatusTransaksi.AwaitingPayment)
        ////////////    {
        ////////////        TBJurnal Jurnal = db.TBJurnals.FirstOrDefault(item => item.Referensi.Contains(IDTransaksi));
        ////////////        db.TBJurnalDetails.DeleteAllOnSubmit(Jurnal.TBJurnalDetails);
        ////////////        db.SubmitChanges();

        ////////////        #region DISCOUNT
        ////////////        //DEBIT     : PIUTANG AFILIASI & POTONGAN/DISKON PENJUALAN & HPP
        ////////////        //KREDIT    : PERSEDIAAN & PENJUALAN
        ////////////        if (TotalPotonganHargaJualDetail > 0)
        ////////////        {
        ////////////            //412 - PIUTANG AFILIASI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = GrandTotal + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //403 - POTONGAN/DISKON PENJUALAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "POTONGAN/DISKON PENJUALAN").IDAkun,
        ////////////                Debit = TotalPotonganHargaJualDetail,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + TotalPotonganHargaJualDetail + BiayaPengiriman + Pembulatan + PembulatanJurnal
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });
        ////////////        }
        ////////////        #endregion

        ////////////        #region NO DISCOUNT
        ////////////        //DEBIT     : PIUTANG AFILIASI & HPP
        ////////////        //KREDIT    : PERSEDIAAN & PENJUALAN
        ////////////        else
        ////////////        {
        ////////////            //412 - PIUTANG AFILIASI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                Debit = GrandTotal + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = Subtotal + Pembulatan + PembulatanJurnal
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan2
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = BiayaTambahan1
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });

        ////////////        }
        ////////////        #endregion

        ////////////        db.TBJurnalDetails.InsertAllOnSubmit(Jurnal.TBJurnalDetails);
        ////////////    }
        ////////////    #endregion

        ////////////    #region AWAITING PAYMENT > CANCELED
        ////////////    //TRANSAKSI AWAITING PAYMENT > CANCELED
        ////////////    else if (IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled && idStatusTransaksiSebelumnya.HasValue
        ////////////        && idStatusTransaksiSebelumnya == (int)EnumStatusTransaksi.AwaitingPayment)
        ////////////    {
        ////////////        //INI HANYA DIPANGGIL, KETIKA KLIK TOMBOL BATAL TRANSAKSI
        ////////////        if (Transaksi.JumlahProduk != 0)
        ////////////        {
        ////////////            TBJurnal Jurnal = new TBJurnal
        ////////////            {
        ////////////                IDTempat = Transaksi.IDTempat,
        ////////////                Tanggal = DateTime.Now,
        ////////////                Keterangan = "VOID TRANSAKSI (AWAITING PAYMENT) - #" + IDTransaksi,
        ////////////                IDPengguna = IDPenggunaTransaksi,
        ////////////                Referensi = IDTransaksi
        ////////////            };

        ////////////            #region DISCOUNT
        ////////////            //DEBIT     : PIUTANG AFILIASI & POTONGAN/DISKON PENJUALAN & HPP
        ////////////            //KREDIT    : PERSEDIAAN & PENJUALAN
        ////////////            if (TotalPotonganHargaJualDetail > 0)
        ////////////            {
        ////////////                //388 - Penjualan
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                    Debit = Subtotal + TotalPotonganHargaJualDetail + BiayaPengiriman + Pembulatan + PembulatanJurnal,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //TAX
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                    Debit = BiayaTambahan2,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //SERVICES
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                    Debit = BiayaTambahan1,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //400 - PERSEDIAAN
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                    Debit = TotalHargaBeli,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //412 - PIUTANG AFILIASI
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                    Debit = 0,
        ////////////                    Kredit = GrandTotal + PembulatanJurnal
        ////////////                });

        ////////////                //403 - POTONGAN/DISKON PENJUALAN
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "POTONGAN/DISKON PENJUALAN").IDAkun,
        ////////////                    Debit = 0,
        ////////////                    Kredit = TotalPotonganHargaJualDetail
        ////////////                });

        ////////////                //404 - HARGA POKOK PRODUKSI
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                    Debit = 0,
        ////////////                    Kredit = TotalHargaBeli
        ////////////                });
        ////////////            }
        ////////////            #endregion

        ////////////            #region NO DISCOUNT
        ////////////            //DEBIT     : PIUTANG AFILIASI & HPP
        ////////////            //KREDIT    : PERSEDIAAN & PENJUALAN
        ////////////            else
        ////////////            {
        ////////////                //412 - PIUTANG AFILIASI
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PIUTANG").IDAkun,
        ////////////                    Debit = 0,
        ////////////                    Kredit = GrandTotal + PembulatanJurnal
        ////////////                });

        ////////////                //404 - HARGA POKOK PRODUKSI
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                    Debit = 0,
        ////////////                    Kredit = TotalHargaBeli
        ////////////                });

        ////////////                //388 - Penjualan
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                    Debit = Subtotal + Pembulatan + PembulatanJurnal,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //TAX
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                    Debit = BiayaTambahan2,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //SERVICES
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                    Debit = BiayaTambahan1,
        ////////////                    Kredit = 0
        ////////////                });

        ////////////                //400 - PERSEDIAAN
        ////////////                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////                {
        ////////////                    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                    Debit = TotalHargaBeli,
        ////////////                    Kredit = 0
        ////////////                });
        ////////////            }
        ////////////            #endregion

        ////////////            db.TBJurnals.InsertOnSubmit(Jurnal);

        ////////////        }
        ////////////        else //KEJADIAN SPLITBILL ALL ITEM KE TABLE BARU, DAN TRANSFER ALL ITEM.. TP BOCOR KALAU CROSS SEMUA ITEM LALU TEKAN ORDER LG
        ////////////        {
        ////////////            TBJurnal DataJurnal = db.TBJurnals.FirstOrDefault(item => item.Referensi == Transaksi.IDTransaksi);

        ////////////            db.TBJurnalDetails.DeleteAllOnSubmit(DataJurnal.TBJurnalDetails);
        ////////////            db.TBJurnals.DeleteOnSubmit(DataJurnal);

        ////////////            //DataJurnal.Keterangan = "VOID TRANSAKSI (TRANSFER ITEM / SPLITBILL ELSE BAWAH) - #" + IDTransaksi;

        ////////////            //foreach (var item in DataJurnal.TBJurnalDetails)
        ////////////            //{
        ////////////            //    if (item.Debit != 0)
        ////////////            //    {
        ////////////            //        item.Kredit = item.Debit;
        ////////////            //        item.Debit = 0;
        ////////////            //    }
        ////////////            //    else
        ////////////            //    {
        ////////////            //        item.Debit = item.Kredit;
        ////////////            //        item.Kredit = 0;
        ////////////            //    }
        ////////////            //}
        ////////////            db.SubmitChanges();
        ////////////        }

        ////////////    }
        ////////////    #endregion

        ////////////    #region COMPLETED > CANCELED
        ////////////    //TRANSAKSI COMPLETED > CANCELED
        ////////////    else if (IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled && idStatusTransaksiSebelumnya.HasValue
        ////////////        && idStatusTransaksiSebelumnya == (int)EnumStatusTransaksi.Complete)
        ////////////    {
        ////////////        TBJurnal Jurnal = new TBJurnal
        ////////////        {
        ////////////            IDTempat = Transaksi.IDTempat,
        ////////////            Tanggal = DateTime.Now,
        ////////////            Keterangan = "VOID TRANSAKSI - #" + IDTransaksi,
        ////////////            IDPengguna = IDPenggunaTransaksi,
        ////////////            Referensi = IDTransaksi
        ////////////        };

        ////////////        #region DISCOUNT
        ////////////        //DEBIT     : PENJUALAN & PERSEDIAAN & POTONGAN/DISKON PENJUALAN
        ////////////        //KREDIT    : KAS & HPP
        ////////////        if (TotalPotonganHargaJualDetail > 0)
        ////////////        {
        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = Subtotal + TotalPotonganHargaJualDetail + BiayaPengiriman + Pembulatan + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = BiayaTambahan2,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = BiayaTambahan1,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //KAS
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = IDAkunPembayaran,
        ////////////                Debit = 0,
        ////////////                Kredit = GrandTotal
        ////////////            });

        ////////////            //403 - POTONGAN/DISKON PENJUALAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "POTONGAN/DISKON PENJUALAN").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalPotonganHargaJualDetail
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });
        ////////////        }
        ////////////        #endregion

        ////////////        #region NO DISCOUNT
        ////////////        //DEBIT     : PENJUALAN & PERSEDIAAN
        ////////////        //KREDIT    : KAS & HPP
        ////////////        else
        ////////////        {
        ////////////            //KAS
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = IDAkunPembayaran,
        ////////////                Debit = 0,
        ////////////                Kredit = GrandTotal
        ////////////            });

        ////////////            //404 - HARGA POKOK PRODUKSI
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HPP").IDAkun,
        ////////////                Debit = 0,
        ////////////                Kredit = TotalHargaBeli
        ////////////            });

        ////////////            //388 - Penjualan
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PENJUALAN").IDAkun,
        ////////////                Debit = Subtotal + Pembulatan + PembulatanJurnal,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //TAX
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG TAX").IDAkun,
        ////////////                Debit = BiayaTambahan2,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //SERVICES
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG SERVICE").IDAkun,
        ////////////                Debit = BiayaTambahan1,
        ////////////                Kredit = 0
        ////////////            });

        ////////////            //400 - PERSEDIAAN
        ////////////            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        ////////////            {
        ////////////                IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "PERSEDIAAN").IDAkun,
        ////////////                Debit = TotalHargaBeli,
        ////////////                Kredit = 0
        ////////////            });

        ////////////        }
        ////////////        #endregion

        ////////////        db.TBJurnals.InsertOnSubmit(Jurnal);
        ////////////    }
        ////////////    #endregion
        ////////////}
        #endregion

        return IDTransaksi;
    }

    public string ConfirmTransaksi()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            ConfirmTransaksi(db);

            db.SubmitChanges();
        }

        return IDTransaksi;
    }

    public void UbahJumlahProduk(int idDetailTransaksi, int jumlahProduk)
    {
        if (idDetailTransaksi > 0 && Detail.Count > 0)
        {
            //ID DETAIL TRANSAKSI VALID DAN DETAIL ADA

            var TransaksiDetail = Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == idDetailTransaksi);

            if (TransaksiDetail != null)
            {
                if (jumlahProduk == 0)
                    Detail.Remove(TransaksiDetail); //JIKA PERUBAHAN MENJADI 0 MAKA DIHAPUS
                else
                    TransaksiDetail.Quantity = jumlahProduk;
            }
        }
    }

    public void TambahKurangJumlahProduk(int idDetailTransaksi, int jumlahProduk)
    {
        if (idDetailTransaksi > 0 && Detail.Count > 0)
        {
            //ID DETAIL TRANSAKSI VALID DAN DETAIL ADA

            var TransaksiDetail = Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == idDetailTransaksi);

            if (TransaksiDetail != null)
            {
                TransaksiDetail.Quantity += jumlahProduk;

                if (TransaksiDetail.Quantity == 0)
                    Detail.Remove(TransaksiDetail); //JIKA PERUBAHAN MENJADI 0 MAKA DIHAPUS
            }
        }
    }

    public void UbahHargaJualTampilProduk(int idDetailTransaksi, decimal hargaJualTampil)
    {
        if (idDetailTransaksi > 0 && Detail.Count > 0)
        {
            //ID DETAIL TRANSAKSI VALID DAN DETAIL ADA

            var TransaksiDetail = Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == idDetailTransaksi);

            if (TransaksiDetail != null)
                TransaksiDetail.PengaturanHargaJualBersih(hargaJualTampil);
        }
    }

    public void UbahPotonganHargaJualProduk(int idDetailTransaksi, string potonganHarga)
    {
        if (idDetailTransaksi > 0 && Detail.Count > 0)
        {
            //ID DETAIL TRANSAKSI VALID DAN DETAIL ADA

            var TransaksiDetail = Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == idDetailTransaksi);

            if (TransaksiDetail != null)
                TransaksiDetail.PengaturanDiscount(potonganHarga);
        }
    }

    public void UbahHargaSubtotalProduk(int idDetailTransaksi, decimal subtotal)
    {
        if (idDetailTransaksi > 0 && Detail.Count > 0)
        {
            //ID DETAIL TRANSAKSI VALID DAN DETAIL ADA

            var TransaksiDetail = Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == idDetailTransaksi);

            if (TransaksiDetail != null)
                TransaksiDetail.PengaturanSubtotal(subtotal);
        }
    }

    public int TambahDetailTransaksi(int idKombinasiProduk, int jumlahProduk)
    {
        if (idKombinasiProduk > 0)
        {
            //MENGAMBIL AUTO INCREMENT
            int IDDetailTransaksi = IDDetailTransaksiTemp;

            //MEMASUKKAN KE DETAIL TRANSAKI
            var DetailTransaksi = new TransaksiRetailDetail_Model((int)IDTempat, IDDetailTransaksi, idKombinasiProduk, jumlahProduk, true);

            if (DetailTransaksi.Quantity != 0)
            {
                Detail.Add(DetailTransaksi);

                return IDDetailTransaksi;
            }
            else
                return 0;
        }
        else
            return 0;
    }

    public int TambahDetailTransaksiMarketing(int idKombinasiProduk, int jumlahProduk)
    {
        if (idKombinasiProduk > 0)
        {
            //MENGAMBIL AUTO INCREMENT
            int IDDetailTransaksi = IDDetailTransaksiTemp;

            //MEMASUKKAN KE DETAIL TRANSAKI
            var DetailTransaksi = new TransaksiRetailDetail_Model((int)IDTempat, IDDetailTransaksi, idKombinasiProduk, jumlahProduk, true);

            if (DetailTransaksi.Quantity != 0)
            {
                //PENGATURAN HARGA JUAL = HARGA BELI
                DetailTransaksi.HargaJual = DetailTransaksi.HargaBeli;

                Detail.Add(DetailTransaksi);

                return IDDetailTransaksi;
            }
            else
                return 0;
        }
        else
            return 0;
    }

    public void TambahPembayaran(DateTime tanggal, int idPenggunaPembayaran, int idJenisPembayaran, decimal bayar, string keterangan)
    {
        Pembayaran.Add(new TransaksiPembayaranRetail_Model
        {
            IDTransaksiJenisPembayaran = IDPembayaranTransaksiTemp,
            IDPengguna = idPenggunaPembayaran,
            IDJenisPembayaran = idJenisPembayaran,
            Tanggal = tanggal,
            Keterangan = keterangan,
            Bayar = bayar
        });
    }

    public void HapusPembayaran(TransaksiPembayaranRetail_Model _pembayaran)
    {
        Pembayaran.Remove(_pembayaran);
    }

    public void TambahVoucher(string kode)
    {
        var _voucher = new TransaksiVoucherRetail_Model(kode, TanggalTransaksi, Pelanggan.IDPelanggan, GrandTotal);

        //JIKA VOUCHER VALID DAN TIDAK ADA VOUCHER YANG SAMA DALAM 1 TRANSAKSI
        if (_voucher.IDVoucher != 0 && Voucher.FirstOrDefault(item => item.IDVoucher == _voucher.IDVoucher) == null)
            Voucher.Add(_voucher);
    }

    public void PengaturanPelanggan(int idPelanggan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            pelanggan = new TransaksiPelangganRetail_Model(idPelanggan);

            if (Detail != null && Detail.Count > 0)
            {
                foreach (var item in Detail)
                {
                    var TransaksiDetail = Detail.FirstOrDefault(item2 => item2.IDDetailTransaksi == item.IDDetailTransaksi);

                    if (TransaksiDetail.HargaJual != TransaksiDetail.DiscountStore)
                    {
                        List<decimal> tempDiscount = new List<decimal>();

                        tempDiscount.Add(TransaksiDetail.DiscountStore);
                        tempDiscount.Add(TransaksiDetail.HargaJual * pelanggan.NilaiDiscount / 100);

                        var Data = db.TBKombinasiProduks.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).TBProduk.TBProdukKategori.TBDiscountProdukKategoris.FirstOrDefault(item2 => item2.IDGrupPelanggan == pelanggan.IDGrupPelanggan);

                        if (Data != null)
                            tempDiscount.Add(TransaksiDetail.HargaJual * Data.Discount / 100);

                        UbahPotonganHargaJualProduk(item.IDDetailTransaksi, Pengaturan.FormatHarga(tempDiscount.Max()));
                    }
                }
            }

        }
    }

    public void PrintOrder(PilihanStatusPrint pilihanStatusPrint)
    {
        var DetailClass = Detail
            .GroupBy(item => new
            {
                item.IDKombinasiProduk,
                item.Keterangan
            })
            .Select(item => new
            {
                item.Key,
                Quantity = item.Sum(item2 => item2.Quantity)
            }).ToArray();

        print.AddRange(DetailClass.Select(item => new TransaksiPrint_Model
        {
            EnumStatusPrint = pilihanStatusPrint,
            IDKombinasiProduk = item.Key.IDKombinasiProduk,
            Quantity = item.Quantity,
            Keterangan = item.Key.Keterangan,
            Station = Station
        }));
    }

    public void ResetTransaksiDetail()
    {
        detail = new List<TransaksiRetailDetail_Model>();
    }

    public void ResetPembayaran()
    {
        pembayaran = new List<TransaksiPembayaranRetail_Model>();
    }

    public bool PeriksaDeposit()
    {
        //MENGHITUNG PENGGUNAAN DEPOSIT
        var PenggunaanDeposit = Pembayaran.Where(item => item.IDJenisPembayaran == (int)EnumJenisPembayaran.Deposit).Sum(item => item.Total);

        if (PenggunaanDeposit > 0)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
                var SisaDeposit = ClassPelanggan.Deposit(Pelanggan.IDPelanggan);

                if (PenggunaanDeposit <= SisaDeposit) //PENGGUNAAN DEPOSIT LEBIH KECIL ATAU SAMA DENGAN DEPOSIT
                    return true;
                else
                {
                    //PENGGUNAAN DEPOSIT LEBIH BESAR DARI SISA DEPOSIT

                    //MENGHAPUS SEMUA DEPOSIT LAMA
                    Pembayaran.RemoveAll(item => item.IDJenisPembayaran == (int)EnumJenisPembayaran.Deposit);

                    //JIKA SISA DEPOSIT LEBIH DARI 0 MEMASUKKAN SESUAI SISA DEPOSIT
                    if (SisaDeposit > 0)
                        TambahPembayaran(DateTime.Now, IDPenggunaUpdate.HasValue ? IDPenggunaUpdate.Value : IDPenggunaTransaksi, 2, SisaDeposit, "");

                    return false;
                }
            }
        }
        else
            return true;
    }

    private void PenghitunganConsignment()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //MENGAMBIL DATA PERSENTASE CONSIGNMENT KOMBINASI PRODUK
            var PersentaseConsignment = db.TBStokProduks
                .Where(item =>
                    item.IDTempat == IDTempat &&
                    Detail.Select(item2 => item2.IDKombinasiProduk).Contains(item.IDKombinasiProduk))
                .Select(item => new
                {
                    item.IDKombinasiProduk,
                    PersentaseKonsinyasi = item.PersentaseKonsinyasi.Value
                })
                .ToArray();

            foreach (var item in Detail)
            {
                decimal TempPersentaseConsignment = PersentaseConsignment.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).PersentaseKonsinyasi;

                if (item.DiscountKonsinyasi != 0)
                {
                    decimal TempDiscountStore = (item.DiscountKonsinyasi * TempPersentaseConsignment);

                    item.DiscountStore += TempDiscountStore;
                    item.DiscountKonsinyasi = (item.DiscountKonsinyasi - TempDiscountStore);
                }
            }
        }
    }
}