using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassHargaSupplier
/// </summary>
public static class HargaSupplier_Class
{
    public static void Update(DataClassesDatabaseDataContext db, TBSupplier supplier, TBStokBahanBaku stokBahanBaku, decimal harga)
    {
        if (supplier != null)
        {
            TBHargaSupplier hargaSupplier = db.TBHargaSuppliers.FirstOrDefault(item => item.TBSupplier == supplier && item.TBStokBahanBaku == stokBahanBaku);

            if (hargaSupplier == null)
            {
                hargaSupplier = new TBHargaSupplier
                {
                    TBSupplier = supplier,
                    Tanggal = DateTime.Now,
                    Harga = harga,
                    TBStokBahanBaku = stokBahanBaku
                };

                db.TBHargaSuppliers.InsertOnSubmit(hargaSupplier);
            }
            else
            {
                hargaSupplier.Tanggal = DateTime.Now;
                hargaSupplier.Harga = harga;
            }
        }
    }

    public static TBHargaSupplier CariHargaSupplier(DataClassesDatabaseDataContext db, TBSupplier supplier, TBStokBahanBaku stokBahanBaku)
    {
        return db.TBHargaSuppliers.FirstOrDefault(item => item.TBSupplier == supplier && item.TBStokBahanBaku == stokBahanBaku);
    }

    public static TBHargaSupplier CariHargaSupplier(DataClassesDatabaseDataContext db, int IDSupplier, int IDStokBahanBaku, int IDBahanBaku)
    {
        if (IDStokBahanBaku != 0)
            return db.TBHargaSuppliers.FirstOrDefault(item => item.IDSupplier == IDSupplier && item.IDStokBahanBaku == IDStokBahanBaku);
        else if (IDBahanBaku != 0)
            return db.TBHargaSuppliers.FirstOrDefault(item => item.IDSupplier == IDSupplier && item.TBStokBahanBaku.IDBahanBaku == IDBahanBaku);
        else
            return null;
    }
}