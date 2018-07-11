using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for POProduksiBiayaTambahan_Model
/// </summary>
/// 

[Serializable]
public class POProduksiBiayaTambahan_Model
{
    public int IDKombinasiProduk { get; set; }
    public int IDBahanBakuProduksi { get; set; }
    public int IDJenisBiayaProduksi { get; set; }
    public string Nama { get; set; }
    public string Jenis { get; set; }
    public decimal JumlahProduksi { get; set; }
    public int EnumBiayaProduksi { get; set; }
    public decimal Persentase { get; set; }
    public decimal Nominal { get; set; }
    public decimal Biaya { get; set; }
    public decimal SubtotalBiaya
    {
        get { return JumlahProduksi * Biaya; }
    }
}