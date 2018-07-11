using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class TransaksiPrint_Model
{
    public int IDKombinasiProduk { get; set; }
    public int Quantity { get; set; }
    public PilihanStatusPrint EnumStatusPrint { get; set; }
    public string Keterangan { get; set; }
    public string Station { get; set; }
}