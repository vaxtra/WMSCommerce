using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Akun_Class
{
    public TBAkun Cari(DataClassesDatabaseDataContext db, int idAkun)
    {
        return db.TBAkuns.FirstOrDefault(item => item.IDAkun == idAkun);
    }
    public bool Hapus(DataClassesDatabaseDataContext db, int idAkun)
    {
        var Akun = Cari(db, idAkun);

        if (Akun != null)
        {
            if (Akun.TBJurnalDetails.Count <= 0)
            {
                db.TBAkuns.DeleteOnSubmit(Akun);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public TBAkun Tambah(DataClassesDatabaseDataContext db, int idAkunGrup, string kode, string nama, int idtempat)
    {

        var Akun = new TBAkun
        {
            //IDAkun
            IDTempat = idtempat,
            IDAkunGrup = idAkunGrup,
            Kode = kode,
            Nama = nama,
        };

        db.TBAkuns.InsertOnSubmit(Akun);

        return Akun;
    }

    public TBAkun Ubah(DataClassesDatabaseDataContext db, int idAkun, int idAkunGrup, string kode, string nama)
    {
        var Akun = Cari(db, idAkun);

        if (Akun != null)
        {
            //IDAkun
            Akun.IDAkunGrup = idAkunGrup;
            Akun.Kode = kode;
            Akun.Nama = nama;
        }

        return Akun;
    }

    public TBAkun[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBAkuns.ToArray();
    }
}
