using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class TransferProduk_Class
{
    public TBTransferProduk Cari(DataClassesDatabaseDataContext db, string idTransferProduk)
    {
        var TransferProduk = db.TBTransferProduks.FirstOrDefault(item => item.IDTransferProduk == idTransferProduk);

        if (TransferProduk != null)
            return TransferProduk;
        else
            return null;
    }

    public TBTransferProduk Tambah(DataClassesDatabaseDataContext db, int IDPengguna, int IDTempatPengirim, int IDTempatPenerima, string keterangan)
    {
        //JIKA LABEL ID TRANSFER MASIH NULL MAKA MEMBUAT TRANSAKSI BARU
        string _IDTransferProduk = "";

        db.Proc_InsertTransferProduk(ref _IDTransferProduk, IDPengguna, IDTempatPengirim, IDTempatPenerima, keterangan);

        return db.TBTransferProduks.FirstOrDefault(item2 => item2.IDTransferProduk == _IDTransferProduk);
    }

    public void GenerateFile(TBTransferProduk TransferProduk)
    {
        //MEMBUAT FILE TRANSFER PRODUK
        FileTransferProduk FileTransferProduk = new FileTransferProduk
        {
            IDTransferProduk = TransferProduk.IDTransferProduk,
            EnumJenisTransfer = TransferProduk.EnumJenisTransfer,
            TanggalDaftar = TransferProduk.TanggalDaftar,
            TanggalKirim = TransferProduk.TanggalKirim,
            TanggalTerima = TransferProduk.TanggalTerima,
            TanggalUpdate = TransferProduk.TanggalUpdate,
            Keterangan = TransferProduk.Keterangan,

            FileTempatPengirim = new FileTempat
            {
                Alamat = TransferProduk.TBTempat.Alamat,
                BiayaTambahan1 = TransferProduk.TBTempat.BiayaTambahan1,
                BiayaTambahan2 = TransferProduk.TBTempat.BiayaTambahan2,
                BiayaTambahan3 = TransferProduk.TBTempat.BiayaTambahan3,
                BiayaTambahan4 = TransferProduk.TBTempat.BiayaTambahan4,
                Email = TransferProduk.TBTempat.Email,
                EnumBiayaTambahan1 = TransferProduk.TBTempat.EnumBiayaTambahan1,
                EnumBiayaTambahan2 = TransferProduk.TBTempat.EnumBiayaTambahan2,
                EnumBiayaTambahan3 = TransferProduk.TBTempat.EnumBiayaTambahan3,
                EnumBiayaTambahan4 = TransferProduk.TBTempat.EnumBiayaTambahan4,
                FooterPrint = TransferProduk.TBTempat.FooterPrint,
                IDKategoriTempat = TransferProduk.TBTempat.IDKategoriTempat,
                IDStore = TransferProduk.TBTempat.IDStore,
                IDWMS = TransferProduk.TBTempat._IDWMS,
                KeteranganBiayaTambahan1 = TransferProduk.TBTempat.KeteranganBiayaTambahan1,
                KeteranganBiayaTambahan2 = TransferProduk.TBTempat.KeteranganBiayaTambahan2,
                KeteranganBiayaTambahan3 = TransferProduk.TBTempat.KeteranganBiayaTambahan3,
                KeteranganBiayaTambahan4 = TransferProduk.TBTempat.KeteranganBiayaTambahan4,
                Kode = TransferProduk.TBTempat.Kode,
                Latitude = TransferProduk.TBTempat.Latitude,
                Longitude = TransferProduk.TBTempat.Longitude,
                Nama = TransferProduk.TBTempat.Nama,
                TanggalDaftar = TransferProduk.TBTempat._TanggalInsert,
                TanggalUpdate = TransferProduk.TBTempat._TanggalUpdate,
                Telepon1 = TransferProduk.TBTempat.Telepon1,
                Telepon2 = TransferProduk.TBTempat.Telepon2
            },

            FileTempatPenerima = new FileTempat
            {
                Alamat = TransferProduk.TBTempat1.Alamat,
                BiayaTambahan1 = TransferProduk.TBTempat1.BiayaTambahan1,
                BiayaTambahan2 = TransferProduk.TBTempat1.BiayaTambahan2,
                BiayaTambahan3 = TransferProduk.TBTempat1.BiayaTambahan3,
                BiayaTambahan4 = TransferProduk.TBTempat1.BiayaTambahan4,
                Email = TransferProduk.TBTempat1.Email,
                EnumBiayaTambahan1 = TransferProduk.TBTempat1.EnumBiayaTambahan1,
                EnumBiayaTambahan2 = TransferProduk.TBTempat1.EnumBiayaTambahan2,
                EnumBiayaTambahan3 = TransferProduk.TBTempat1.EnumBiayaTambahan3,
                EnumBiayaTambahan4 = TransferProduk.TBTempat1.EnumBiayaTambahan4,
                FooterPrint = TransferProduk.TBTempat1.FooterPrint,
                IDKategoriTempat = TransferProduk.TBTempat1.IDKategoriTempat,
                IDStore = TransferProduk.TBTempat1.IDStore,
                IDWMS = TransferProduk.TBTempat1._IDWMS,
                KeteranganBiayaTambahan1 = TransferProduk.TBTempat1.KeteranganBiayaTambahan1,
                KeteranganBiayaTambahan2 = TransferProduk.TBTempat1.KeteranganBiayaTambahan2,
                KeteranganBiayaTambahan3 = TransferProduk.TBTempat1.KeteranganBiayaTambahan3,
                KeteranganBiayaTambahan4 = TransferProduk.TBTempat1.KeteranganBiayaTambahan4,
                Kode = TransferProduk.TBTempat1.Kode,
                Latitude = TransferProduk.TBTempat1.Latitude,
                Longitude = TransferProduk.TBTempat1.Longitude,
                Nama = TransferProduk.TBTempat1.Nama,
                TanggalDaftar = TransferProduk.TBTempat1._TanggalInsert,
                TanggalUpdate = TransferProduk.TBTempat1._TanggalUpdate,
                Telepon1 = TransferProduk.TBTempat1.Telepon1,
                Telepon2 = TransferProduk.TBTempat1.Telepon2
            },

            TransferProdukDetails = TransferProduk.TBTransferProdukDetails.Select(item => new FileTransferProdukDetail
            {
                Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
                KombinasiProduk = item.TBKombinasiProduk.Nama,
                Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                TanggalDaftar = item.TBKombinasiProduk.TanggalDaftar.HasValue ? item.TBKombinasiProduk.TanggalDaftar.Value : DateTime.Now,
                TanggalUpdate = item.TBKombinasiProduk.TanggalUpdate.HasValue ? item.TBKombinasiProduk.TanggalUpdate.Value : DateTime.Now,
                Berat = item.TBKombinasiProduk.Berat.HasValue ? item.TBKombinasiProduk.Berat.Value : 0,
                Jumlah = item.Jumlah,
                HargaBeli = item.HargaBeli,
                HargaJual = item.HargaJual,
                PersentaseKonsinyasi = item.TBKombinasiProduk.TBStokProduks.FirstOrDefault(item2 => item2.IDTempat == item.TBTransferProduk.IDTempatPengirim).PersentaseKonsinyasi.Value,
                Keterangan = item.TBKombinasiProduk.Deskripsi
            }).ToList(),

            FilePenggunaPengirim = new FilePengguna
            {
                IDGrupPengguna = TransferProduk.TBPengguna.IDGrupPengguna,
                NamaLengkap = TransferProduk.TBPengguna.NamaLengkap,
                Password = TransferProduk.TBPengguna.Password,
                PIN = TransferProduk.TBPengguna.PIN,
                Status = TransferProduk.TBPengguna._IsActive,
                Username = TransferProduk.TBPengguna.Username
            }
        };

        string Folder = HttpContext.Current.Server.MapPath("~/Files/Transfer Produk/Transfer/");

        if (!Directory.Exists(Folder))
            Directory.CreateDirectory(Folder);

        string NamaFile = TransferProduk.TBTempat.Nama + " " + TransferProduk.IDTransferProduk + " " + (TransferProduk.TanggalKirim).ToString("d MMMM yyyy HH.mm") + ".WIT";
        string ExtensiFile = ".zip";
        string Path = Folder + NamaFile + ExtensiFile;
        string Json = JsonConvert.SerializeObject(FileTransferProduk);

        File.WriteAllText(Path, Json);

        string Output = Folder + NamaFile + "_enc" + ExtensiFile;

        EncryptDecrypt.Encrypt(Path, Output);

        File.Delete(Path);
    }
}