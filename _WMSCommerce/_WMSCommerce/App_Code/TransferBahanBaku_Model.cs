using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassTempTransferBahanBaku
/// </summary>
/// 
[Serializable]
public class TransferBahanBaku_Model
{
    public string IDTransferBahanBaku { get; set; }
    public int IDTempatPengirim { get; set; }
    public string NamaPengirim { get; set; }
    public string TempatPengirim { get; set; }
    public string TempatPenerima { get; set; }
    public int EnumJenisTransfer { get; set; }
    public DateTime TanggalKirim { get; set; }
    public decimal TotalJumlah { get; set; }
    public decimal GrandTotal { get; set; }
    public string Keterangan { get; set; }
    public List<TransferBahanBakuDetail_Model> TransferBahanBakuDetail { get; set; }
}