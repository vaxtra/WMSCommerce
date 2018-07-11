using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Stok_Model
/// </summary>
public class Stok_Model
{
    public int IndexClass { get; set; }

    #region Data Produk
    public string KodeKombinasiProduk { get; set; }
    public int IDKombinasiProduk { get; set; }
    public string NamaProduk { get; set; }
    public string NamaKombinasiProduk { get; set; }
    public string Warna { get; set; }
    public string Brand { get; set; }
    #endregion

    #region Data Bahan Baku
    public int IDBahanBaku { get; set; }
    public string KodeBahanBaku { get; set; }
    public string NamaBahanBaku { get; set; }
    public string SatuanKecil { get; set; }
    #endregion

    public string Kategori { get; set; }

    public decimal StokMasuk { get; set; }
    public decimal StokKeluar { get; set; }
    public decimal StokSaatIni { get; set; }
    public decimal StokBertambahSO { get; set; }
    public decimal StokBerkurangSO { get; set; }
    public decimal StokBerkurangBertambahSO
    {
        get
        {
            return StokBertambahSO - StokBerkurangSO;
        }
    }
    public decimal StokSebelumSO
    {
        get; set;
    }
    public decimal StokSetelahSO { get; set; }
    public Stok_Model()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}