using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JenisBiayaProduksi_Model
/// </summary>
/// 

[Serializable]
public class JenisBiayaProduksi_Model
{
    public int IDJenisBiayaProduksi { get; set; }
    public string Nama { get; set; }
    public int EnumBiayaProduksi { get; set; }
    public decimal Persentase { get; set; }
    public decimal Nominal { get; set; }
    public decimal Biaya { get; set; }
}