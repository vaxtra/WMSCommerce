using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StokBahanBaku_Model
/// </summary>
/// 

[Serializable]
public class StokBahanBaku_Model
{ 
    public int IDStokBahanBaku { get; set; }
    public int IDTempat { get; set; }
    public int IDBahanBaku { get; set; }
    public string BahanBaku { get; set; }
    public string Kode { get; set; }
    public string Kategori { get; set; }
    public int IDSatuan { get; set; }
    public string Satuan { get; set; }
    public decimal Jumlah { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal HargaPokokProduksi { get; set; }
    public decimal HargaSupplier { get; set; }
    public decimal PotonganHarga { get; set; }   
    public decimal SubtotalHargaBeli
    {
        get { return HargaBeli * Jumlah; }
    }
    public decimal SubtotalHargaPokokProduksi
    {
        get { return HargaPokokProduksi * Jumlah; }
    }
    public decimal SubtotalHargaSupplier
    {
        get { return HargaSupplier * Jumlah; }
    }
    public decimal SubtotalHarga
    {
        get { return ((HargaPokokProduksi + HargaSupplier) - PotonganHarga) * Jumlah; }
    }
    public List<KomposisiBahanBaku_Model> Komposisi { get; set; }
}