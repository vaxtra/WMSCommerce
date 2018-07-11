using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for POProduksiKomposisi_Model
/// </summary>
/// 

[Serializable]
public class POProduksiKomposisi_Model
{
    public int IDKombinasiProduk { get; set; }
    public int IDBahanBakuProduksi { get; set; }
    public int IDBahanBaku { get; set; }
    public int IDSatuan { get; set; }
    public string BahanBaku { get; set; }
    public string Satuan { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal JumlahProduksi { get; set; }
    public decimal JumlahKomposisi { get; set; }
    public decimal SubtotalKomposisi
    {
        get { return JumlahKomposisi * HargaBeli; }
    }
    public decimal JumlahKebutuhan { get; set; }
    public decimal SubtotalKebutuhan
    {
        get { return JumlahKebutuhan * HargaBeli; }
    }
    public decimal JumlahSisa { get; set; }
    public decimal SubtotalSisa
    {
        get { return JumlahSisa * HargaBeli; }
    }
    public decimal JumlahKurang { get; set; }
}