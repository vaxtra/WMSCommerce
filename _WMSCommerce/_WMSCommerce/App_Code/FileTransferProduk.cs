using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class FileTransferProduk
{
    public string IDTransferProduk { get; set; }
    //Nomor
    public FilePengguna FilePenggunaPengirim { get; set; }
    public FilePengguna FilePenggunaPenerima { get; set; }
    public FileTempat FileTempatPengirim { get; set; }
    public FileTempat FileTempatPenerima { get; set; }
    public DateTime TanggalDaftar { get; set; }
    public DateTime TanggalUpdate { get; set; }
    public int EnumJenisTransfer { get; set; }
    public DateTime TanggalKirim { get; set; }
    public DateTime? TanggalTerima { get; set; }
    public int TotalJumlah
    {
        get
        {
            return TransferProdukDetails.Sum(item => item.Jumlah);
        }
    }
    public decimal GrandTotalHargaBeli
    {
        get
        {
            return TransferProdukDetails.Sum(item => item.SubtotalHargaBeli);
        }
    }
    public decimal GrandTotalHargaJual
    {
        get
        {
            return TransferProdukDetails.Sum(item => item.SubtotalHargaJual);
        }
    }
    public string Keterangan { get; set; }
    public List<FileTransferProdukDetail> TransferProdukDetails { get; set; }
}

[Serializable]
public class FileTempat
{
    //IDTempat
    public Guid IDWMS { get; set; }
    public int IDStore { get; set; }
    public int IDKategoriTempat { get; set; }
    public DateTime TanggalDaftar { get; set; }
    public DateTime TanggalUpdate { get; set; }
    public string Kode { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Email { get; set; }
    public string Telepon1 { get; set; }
    public string Telepon2 { get; set; }
    public int? EnumBiayaTambahan1 { get; set; }
    public string KeteranganBiayaTambahan1 { get; set; }
    public decimal? BiayaTambahan1 { get; set; }
    public int? EnumBiayaTambahan2 { get; set; }
    public string KeteranganBiayaTambahan2 { get; set; }
    public decimal? BiayaTambahan2 { get; set; }
    public int? EnumBiayaTambahan3 { get; set; }
    public string KeteranganBiayaTambahan3 { get; set; }
    public decimal? BiayaTambahan3 { get; set; }
    public int? EnumBiayaTambahan4 { get; set; }
    public string KeteranganBiayaTambahan4 { get; set; }
    public decimal? BiayaTambahan4 { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string FooterPrint { get; set; }
}

[Serializable]
public class FilePengguna
{
    public int IDGrupPengguna { get; set; }
    public string NamaLengkap { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PIN { get; set; }
    public bool Status { get; set; }
}

[Serializable]
public class FileTransferProdukDetail
{
    public string Kode { get; set; }
    public string Produk { get; set; }
    public string Atribut { get; set; }
    public string KombinasiProduk { get; set; }
    public string Warna { get; set; }
    public string Kategori { get; set; }
    public string PemilikProduk { get; set; }
    public DateTime TanggalDaftar { get; set; }
    public DateTime TanggalUpdate { get; set; }
    public decimal Berat { get; set; }
    public int Jumlah { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal HargaJual { get; set; }
    public decimal PersentaseKonsinyasi { get; set; }
    public decimal SubtotalHargaBeli
    {
        get { return HargaBeli * Jumlah; }
    }
    public decimal SubtotalHargaJual
    {
        get { return HargaJual * Jumlah; }
    }
    public string Keterangan { get; set; }
}