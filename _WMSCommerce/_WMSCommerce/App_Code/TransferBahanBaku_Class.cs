using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class TransferBahanBaku_Class
{
    public TBTransferBahanBaku Tambah(DataClassesDatabaseDataContext db, int IDPengguna, int IDTempatPengirim, int IDTempatPenerima, string keterangan)
    {
        //JIKA LABEL ID TRANSFER MASIH NULL MAKA MEMBUAT TRANSAKSI BARU
        string _IDTransferBahanBaku = "";

        db.Proc_InsertTransferBahanBaku(ref _IDTransferBahanBaku, IDPengguna, IDTempatPengirim, IDTempatPenerima, keterangan);

        return db.TBTransferBahanBakus.FirstOrDefault(item2 => item2.IDTransferBahanBaku == _IDTransferBahanBaku);
    }

    public void GenerateFile(TBTransferBahanBaku TransferBahanBaku)
    {
        //MEMBUAT FILE TRANSFER PRODUK
        FileTransferBahanBaku FileTransferBahanBaku = new FileTransferBahanBaku
        {
            IDTransferBahanBaku = TransferBahanBaku.IDTransferBahanBaku,
            EnumJenisTransfer = TransferBahanBaku.EnumJenisTransfer,
            TanggalDaftar = TransferBahanBaku.TanggalDaftar,
            TanggalKirim = TransferBahanBaku.TanggalKirim,
            TanggalTerima = TransferBahanBaku.TanggalTerima,
            TanggalUpdate = TransferBahanBaku.TanggalUpdate,
            Keterangan = TransferBahanBaku.Keterangan,

            FileTempatPengirim = new FileTempat
            {
                Alamat = TransferBahanBaku.TBTempat.Alamat,
                BiayaTambahan1 = TransferBahanBaku.TBTempat.BiayaTambahan1,
                BiayaTambahan2 = TransferBahanBaku.TBTempat.BiayaTambahan2,
                BiayaTambahan3 = TransferBahanBaku.TBTempat.BiayaTambahan3,
                BiayaTambahan4 = TransferBahanBaku.TBTempat.BiayaTambahan4,
                Email = TransferBahanBaku.TBTempat.Email,
                EnumBiayaTambahan1 = TransferBahanBaku.TBTempat.EnumBiayaTambahan1,
                EnumBiayaTambahan2 = TransferBahanBaku.TBTempat.EnumBiayaTambahan2,
                EnumBiayaTambahan3 = TransferBahanBaku.TBTempat.EnumBiayaTambahan3,
                EnumBiayaTambahan4 = TransferBahanBaku.TBTempat.EnumBiayaTambahan4,
                FooterPrint = TransferBahanBaku.TBTempat.FooterPrint,
                IDKategoriTempat = TransferBahanBaku.TBTempat.IDKategoriTempat,
                IDStore = TransferBahanBaku.TBTempat.IDStore,
                IDWMS = TransferBahanBaku.TBTempat._IDWMS,
                KeteranganBiayaTambahan1 = TransferBahanBaku.TBTempat.KeteranganBiayaTambahan1,
                KeteranganBiayaTambahan2 = TransferBahanBaku.TBTempat.KeteranganBiayaTambahan2,
                KeteranganBiayaTambahan3 = TransferBahanBaku.TBTempat.KeteranganBiayaTambahan3,
                KeteranganBiayaTambahan4 = TransferBahanBaku.TBTempat.KeteranganBiayaTambahan4,
                Kode = TransferBahanBaku.TBTempat.Kode,
                Latitude = TransferBahanBaku.TBTempat.Latitude,
                Longitude = TransferBahanBaku.TBTempat.Longitude,
                Nama = TransferBahanBaku.TBTempat.Nama,
                TanggalDaftar = TransferBahanBaku.TBTempat._TanggalInsert,
                TanggalUpdate = TransferBahanBaku.TBTempat._TanggalUpdate,
                Telepon1 = TransferBahanBaku.TBTempat.Telepon1,
                Telepon2 = TransferBahanBaku.TBTempat.Telepon2
            },

            FileTempatPenerima = new FileTempat
            {
                Alamat = TransferBahanBaku.TBTempat1.Alamat,
                BiayaTambahan1 = TransferBahanBaku.TBTempat1.BiayaTambahan1,
                BiayaTambahan2 = TransferBahanBaku.TBTempat1.BiayaTambahan2,
                BiayaTambahan3 = TransferBahanBaku.TBTempat1.BiayaTambahan3,
                BiayaTambahan4 = TransferBahanBaku.TBTempat1.BiayaTambahan4,
                Email = TransferBahanBaku.TBTempat1.Email,
                EnumBiayaTambahan1 = TransferBahanBaku.TBTempat1.EnumBiayaTambahan1,
                EnumBiayaTambahan2 = TransferBahanBaku.TBTempat1.EnumBiayaTambahan2,
                EnumBiayaTambahan3 = TransferBahanBaku.TBTempat1.EnumBiayaTambahan3,
                EnumBiayaTambahan4 = TransferBahanBaku.TBTempat1.EnumBiayaTambahan4,
                FooterPrint = TransferBahanBaku.TBTempat1.FooterPrint,
                IDKategoriTempat = TransferBahanBaku.TBTempat1.IDKategoriTempat,
                IDStore = TransferBahanBaku.TBTempat1.IDStore,
                IDWMS = TransferBahanBaku.TBTempat1._IDWMS,
                KeteranganBiayaTambahan1 = TransferBahanBaku.TBTempat1.KeteranganBiayaTambahan1,
                KeteranganBiayaTambahan2 = TransferBahanBaku.TBTempat1.KeteranganBiayaTambahan2,
                KeteranganBiayaTambahan3 = TransferBahanBaku.TBTempat1.KeteranganBiayaTambahan3,
                KeteranganBiayaTambahan4 = TransferBahanBaku.TBTempat1.KeteranganBiayaTambahan4,
                Kode = TransferBahanBaku.TBTempat1.Kode,
                Latitude = TransferBahanBaku.TBTempat1.Latitude,
                Longitude = TransferBahanBaku.TBTempat1.Longitude,
                Nama = TransferBahanBaku.TBTempat1.Nama,
                TanggalDaftar = TransferBahanBaku.TBTempat1._TanggalInsert,
                TanggalUpdate = TransferBahanBaku.TBTempat1._TanggalUpdate,
                Telepon1 = TransferBahanBaku.TBTempat1.Telepon1,
                Telepon2 = TransferBahanBaku.TBTempat1.Telepon2
            },

            TransferBahanBakuDetails = TransferBahanBaku.TBTransferBahanBakuDetails.Select(item => new FileTransferBahanBakuDetail
            {
                Kode = item.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBBahanBaku.Nama,
                SatuanKecil = item.TBBahanBaku.TBSatuan.Nama,
                Konversi = item.TBBahanBaku.Konversi.Value,
                SatuanBesar = item.TBSatuan.Nama,
                Kategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault().TBKategoriBahanBaku.Nama : "",
                TanggalDaftar = item.TBBahanBaku.TanggalDaftar.HasValue ? item.TBBahanBaku.TanggalDaftar.Value : DateTime.Now,
                TanggalUpdate = item.TBBahanBaku.TanggalUpdate.HasValue ? item.TBBahanBaku.TanggalUpdate.Value : DateTime.Now,
                Berat = item.TBBahanBaku.Berat.HasValue ? item.TBBahanBaku.Berat.Value : 0,
                Jumlah = item.Jumlah,
                HargaBeli = item.HargaBeli,
                Keterangan = item.TBBahanBaku.Deskripsi
            }).ToList(),

            FilePenggunaPengirim = new FilePengguna
            {
                IDGrupPengguna = TransferBahanBaku.TBPengguna.IDGrupPengguna,
                NamaLengkap = TransferBahanBaku.TBPengguna.NamaLengkap,
                Password = TransferBahanBaku.TBPengguna.Password,
                PIN = TransferBahanBaku.TBPengguna.PIN,
                Status = TransferBahanBaku.TBPengguna._IsActive,
                Username = TransferBahanBaku.TBPengguna.Username
            }
        };

        string Folder = HttpContext.Current.Server.MapPath("~/Files/Transfer Bahan Baku/Transfer/");

        if (!Directory.Exists(Folder))
            Directory.CreateDirectory(Folder);

        string NamaFile = TransferBahanBaku.TBTempat.Nama + " " + TransferBahanBaku.IDTransferBahanBaku + " " + (TransferBahanBaku.TanggalKirim).ToString("d MMMM yyyy HH.mm") + ".WIT";
        string ExtensiFile = ".zip";
        string Path = Folder + NamaFile + ExtensiFile;
        string Json = JsonConvert.SerializeObject(FileTransferBahanBaku);

        File.WriteAllText(Path, Json);

        string Output = Folder + NamaFile + "_enc" + ExtensiFile;

        EncryptDecrypt.Encrypt(Path, Output);

        File.Delete(Path);
    }
}