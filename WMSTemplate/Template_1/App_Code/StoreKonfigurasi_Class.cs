using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class StoreKonfigurasi_Class
{
    public TBStoreKonfigurasi[] LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return db.TBStoreKonfigurasis.ToArray();
        }
    }
    public TBStoreKonfigurasi Cari(DataClassesDatabaseDataContext db, EnumStoreKonfigurasi enumStoreKonfigurasi)
    {
        return db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)enumStoreKonfigurasi);
    }
    public TBStoreKonfigurasi UbahPengaturan(DataClassesDatabaseDataContext db, int idStoreKonfigurasi, string pengaturan)
    {
        var StoreKonfigurasi = Cari(db, (EnumStoreKonfigurasi)idStoreKonfigurasi);

        if (StoreKonfigurasi != null)
            StoreKonfigurasi.Pengaturan = pengaturan;

        return StoreKonfigurasi;
    }
    public string Pengaturan(DataClassesDatabaseDataContext db, EnumStoreKonfigurasi enumStoreKonfigurasi)
    {
        var StoreKonfigurasi = Cari(db, enumStoreKonfigurasi);

        if (StoreKonfigurasi != null)
            return StoreKonfigurasi.Pengaturan;
        else
            return "";
    }
}