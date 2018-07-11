using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Berlangganan_Class
{
    public TBBerlangganan[] LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return db.TBBerlangganans.ToArray();
        }
    }

    public TBBerlangganan Cari(DataClassesDatabaseDataContext db, int idBerlangganan)
    {
        return db.TBBerlangganans.FirstOrDefault(item => item.IDBerlangganan == idBerlangganan);
    }

    public TBBerlangganan Cari(int idBerlangganan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return db.TBBerlangganans.FirstOrDefault(item => item.IDBerlangganan == idBerlangganan);
        }
    }

    public TBBerlangganan Tambah(DataClassesDatabaseDataContext db, string email, string noTelepon)
    {
        TBBerlangganan Berlangganan = new TBBerlangganan
        {
            Email = email,
            NoTelepon = noTelepon
        };

        db.TBBerlangganans.InsertOnSubmit(Berlangganan);

        return Berlangganan;
    }

    public bool Hapus(int idBerlangganan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Berlangganan = Cari(db, idBerlangganan);

            if (Berlangganan != null)
            {
                db.TBBerlangganans.DeleteOnSubmit(Berlangganan);
                db.SubmitChanges();

                return true;
            }
            else
                return false;
        }
    }

    public TBBerlangganan Ubah(DataClassesDatabaseDataContext db, int idBerlangganan, string email, string noTelepon)
    {
        var Berlangganan = Cari(db, idBerlangganan);

        if (Berlangganan != null)
        {
            Berlangganan.Email = email;
            Berlangganan.NoTelepon = noTelepon;
        }

        return Berlangganan;
    }
}