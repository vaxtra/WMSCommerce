using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for KomposisiBahanBaku_Model
/// </summary>
/// 

[Serializable]
public class KomposisiBahanBaku_Model
{
    public int IDTempat { get; set; }
    public int IDBahanBakuProduksi { get; set; }
    public int IDBahanBaku { get; set; }
    public int IDSatuan { get; set; }
    public string Kode { get; set; }
    public string BahanBakuProduksi { get; set; }
    public string BahanBaku { get; set; }
    public string Satuan { get; set; }
    public decimal JumlahProduksi { get; set; }
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
}