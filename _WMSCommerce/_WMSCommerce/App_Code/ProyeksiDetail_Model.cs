using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProyeksiDetail_Model
/// </summary>
/// 

[Serializable]
public class ProyeksiDetail_Model
{
    public int IDProduk { get; set; }
    public int IDKombinasiProduk { get; set; }
    public int IDKategoriProyek { get; set; }
    public int IDStokProduk { get; set; }
    public string Kode { get; set; }
    public string Kategori { get; set; }
    public string Produk { get; set; }
    public string Atribut { get; set; }
    public string Warna { get; set; }
    public string KombinasiProduk { get; set; }
    public decimal HargaJual { get; set; }
    public decimal HargaBeli { get; set; }
    public int Jumlah { get; set; }
    public int SisaBelumProduksi { get; set; }
    public decimal Subtotal
    {
        get { return HargaJual * Jumlah; }
    }
}