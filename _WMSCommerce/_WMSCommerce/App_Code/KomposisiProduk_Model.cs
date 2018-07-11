using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for KomposisiProduk_Model
/// </summary>
/// 

[Serializable]
public class KomposisiProduk_Model
{
    public int LevelProduksi { get; set; }
    public int IDProduk { get; set; }
    public int IDKombinasiProduk { get; set; }
    public int IDBahanBaku { get; set; }
    public int IDSatuan { get; set; }
    public int IDTempat { get; set; }
    public string Kode { get; set; }
    public string Produk { get; set; }
    public string Atribut { get; set; }
    public string BahanBaku { get; set; }
    public string Satuan { get; set; }
    public string Kategori { get; set; }
    public int JumlahProduksi { get; set; }
    public decimal JumlahKomposisi { get; set; }
    public decimal JumlahPemakaian { get; set; }
    public decimal JumlahKurang { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal SubtotalPemakaian
    {
        get { return JumlahPemakaian * HargaBeli; }
    }
    public decimal SubtotalKurang
    {
        get { return JumlahKurang * HargaBeli; }
    }
    public decimal SisaBelumPO { get; set; }
    public decimal Stok { get; set; }
    public decimal Kurang { get; set; }
    public bool BahanBakuDasar { get; set; }
}