using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassTempTransferBahanBakuDetail
/// </summary>
/// 
[Serializable]
public class TransferBahanBakuDetail_Model
{
    public int IDBahanBaku { get; set; }
    public string Kode { get; set; }
    public string BahanBaku { get; set; }
    public string Satuan { get; set; }
    public string Kategori { get; set; }
    public string Supplier { get; set; }
    public decimal Berat { get; set; }
    public decimal Jumlah { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal JumlahMinimum { get; set; }
    public int JumlahHariHabis { get; set; }
    public string Keterangan { get; set; }
}