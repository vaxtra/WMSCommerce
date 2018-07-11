using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class KategoriTempat_Class
{
    public TBKategoriTempat[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBKategoriTempats.OrderBy(item => item.Nama).ToArray();
    }
    public TBKategoriTempat Cari(DataClassesDatabaseDataContext db, int idKategoriTempat)
    {
        return db.TBKategoriTempats.FirstOrDefault(item => item.IDKategoriTempat == idKategoriTempat);
    }
    public TBKategoriTempat Tambah(DataClassesDatabaseDataContext db, string nama)
    {
        var KategoriTempat = new TBKategoriTempat
        {
            Nama = nama
        };

        db.TBKategoriTempats.InsertOnSubmit(KategoriTempat);

        return KategoriTempat;
    }
    public TBKategoriTempat Ubah(DataClassesDatabaseDataContext db, int idKategoriTempat, string nama)
    {
        var KategoriTempat = Cari(db, idKategoriTempat);

        KategoriTempat.Nama = nama;

        return KategoriTempat;
    }
    public bool Hapus(int idKategoriTempat)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var KategoriTempat = Cari(db, idKategoriTempat);

            try
            {
                db.TBKategoriTempats.DeleteOnSubmit(KategoriTempat);
                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
