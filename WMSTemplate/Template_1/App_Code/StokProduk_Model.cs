using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class StokProduk_Model
{
    public int IDStokProduk { get; set; }
    public int IDProduk { get; set; }
    public string Produk { get; set; }
    public string Kode { get; set; }
    public int IDKombinasiProduk { get; set; }
    public string KombinasiProduk { get; set; }
    public int IDAtribut { get; set; }
    public string Atribut { get; set; }
    public int IDWarna { get; set; }
    public string Warna { get; set; }
    public int IDKategori { get; set; }
    public string Kategori { get; set; }
    public int IDPemilikProduk { get; set; }
    public string PemilikProduk { get; set; }
    public int Jumlah { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal HargaPokokProduksi { get; set; }
    public decimal HargaVendor { get; set; }
    public decimal HargaJual { get; set; }
    public decimal PotonganHarga { get; set; }
    public decimal SubtotalHargaBeli
    {
        get { return HargaBeli * Jumlah; }
    }
    public decimal SubtotalHargaVendor
    {
        get { return (HargaVendor - PotonganHarga) * Jumlah; }
    }
    public decimal Subtotal
    {
        get { return HargaJual * Jumlah; }
    }

    public decimal Berat { get; set; }
    public string Keterangan { get; set; }
}