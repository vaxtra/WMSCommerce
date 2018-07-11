using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Satuan_Class
/// </summary>
public class Satuan_Class
{
    public static void DeleteSatuan(DataClassesDatabaseDataContext db, int idSatuan)
    {
        TBSatuan satuan = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == idSatuan);

        if (satuan.TBBahanBakus.Count == 0 &&
            satuan.TBBahanBakus1.Count == 0 &&
            satuan.TBPenerimaanPOProduksiBahanBakuDetails.Count == 0 &&
            satuan.TBPengirimanPOProduksiBahanBakuDetails.Count == 0 &&
            satuan.TBPengirimanPOProduksiProdukDetails.Count == 0 &&
            satuan.TBPerpindahanStokBahanBakus.Count == 0 &&
            satuan.TBPOProduksiBahanBakuDetails.Count == 0 &&
            satuan.TBPOProduksiBahanBakuKomposisis.Count == 0 &&
            satuan.TBPOProduksiProdukKomposisis.Count == 0 &&
            satuan.TBProyeksiKomposisis.Count == 0 &&
            satuan.TBTransferBahanBakuDetails.Count == 0)
        {
            db.TBSatuans.DeleteOnSubmit(satuan);
        }
    }
}