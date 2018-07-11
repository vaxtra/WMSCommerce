using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for POProduksiDetail_Model
/// </summary>
/// 

[Serializable]
public class POProduksiDetail_Model
{
    #region Produk
    public int IDProduk { get; set; }
    public int IDKombinasiProduk { get; set; }
    public int IDStokProduk { get; set; }
    public string Produk { get; set; }
    public string Atribut { get; set; }
    public string KombinasiProduk { get; set; }
    #endregion

    #region Bahan Baku
    public int IDBahanBaku { get; set; }
    public int IDSatuan { get; set; }
    public int IDStokBahanBaku { get; set; }
    public string BahanBaku { get; set; }
    public string Satuan { get; set; }
    #endregion

    public string Kode { get; set; }  
    public decimal HargaPokokKomposisi { get; set; }
    public decimal BiayaTambahan { get; set; }
    public decimal TotalHPP { get; set; }
    public decimal Harga { get; set; }
    public decimal PotonganHarga { get; set; }
    public decimal TotalHarga { get; set; }
    public decimal Jumlah { get; set; }
    public decimal SubtotalHPP
    {
        get { return (HargaPokokKomposisi + BiayaTambahan) * Jumlah; }
    }
    public decimal SubtotalHarga
    {
        get { return (Harga - PotonganHarga) * Jumlah; }
    }
    public decimal Subtotal
    {
        get { return ((HargaPokokKomposisi + BiayaTambahan) + (Harga - PotonganHarga)) * Jumlah; }
    }
    public decimal Sisa { get; set; }
}