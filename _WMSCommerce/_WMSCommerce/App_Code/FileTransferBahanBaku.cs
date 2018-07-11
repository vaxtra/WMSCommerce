using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
/// <summary>
/// Summary description for FileTransferBahanBaku
/// </summary>
public class FileTransferBahanBaku
{
    public string IDTransferBahanBaku { get; set; }
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
    public decimal TotalJumlah
    {
        get
        {
            return TransferBahanBakuDetails.Sum(item => item.Jumlah);
        }
    }
    public decimal GrandTotal
    {
        get
        {
            return TransferBahanBakuDetails.Sum(item => item.SubtotalHargaBeli);
        }
    }
    public string Keterangan { get; set; }
    public List<FileTransferBahanBakuDetail> TransferBahanBakuDetails { get; set; }
}

[Serializable]
public class FileTransferBahanBakuDetail
{
    public string Kode { get; set; }
    public string BahanBaku { get; set; }
    public string SatuanKecil { get; set; }
    public decimal Konversi { get; set; }
    public string SatuanBesar { get; set; }
    public string Kategori { get; set; }
    public DateTime TanggalDaftar { get; set; }
    public DateTime TanggalUpdate { get; set; }
    public decimal Berat { get; set; }
    public decimal Jumlah { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal SubtotalHargaBeli
    {
        get { return HargaBeli * Jumlah; }
    }
    public string Keterangan { get; set; }
}