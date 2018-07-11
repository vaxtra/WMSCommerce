using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AkunGrup_Class
{
    public TBAkunGrup[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBAkunGrups.ToArray();
    }
    public TBAkunGrup[] Data(DataClassesDatabaseDataContext db, PilihanJenisAkunGrup pilihanJenisAkunGrup)
    {
        return db.TBAkunGrups.Where(item => item.EnumJenisAkunGrup == (int)pilihanJenisAkunGrup).ToArray();
    }
    public TBAkunGrup Tambah(DataClassesDatabaseDataContext db, string nama, int idAkunGrupParent, int EnumJenisAkunGrup, int EnumSaldoNormal)
    {
        if (idAkunGrupParent != 0)
        {
            var AkunGrup = new TBAkunGrup
            {
                //IDAkun
                IDAkunGrupParent = idAkunGrupParent,
                Nama = nama,
                EnumJenisAkunGrup = EnumJenisAkunGrup,
                EnumSaldoNormal = EnumSaldoNormal
            };

            db.TBAkunGrups.InsertOnSubmit(AkunGrup);
            return AkunGrup;
        }
        else
        {
            var AkunGrup = new TBAkunGrup
            {
                //IDAkun
                Nama = nama,
                EnumJenisAkunGrup = EnumJenisAkunGrup,
                EnumSaldoNormal = EnumSaldoNormal
            };

            db.TBAkunGrups.InsertOnSubmit(AkunGrup);
            return AkunGrup;
        }
    }

    public TBAkunGrup Ubah(DataClassesDatabaseDataContext db, string _idAkunGrup, string _nama, string _idAkunGrupParent, int EnumJenisAkunGrup, int EnumSaldoNormal)
    {
        var AkunGrup = Cari(db, _idAkunGrup.ToInt());

        if (AkunGrup != null)
        {
            if (AkunGrup.IDAkunGrupParent == null)
            {
                AkunGrup.Nama = _nama;
                AkunGrup.EnumJenisAkunGrup = EnumJenisAkunGrup;
                AkunGrup.EnumSaldoNormal = EnumSaldoNormal;
            }
            else
            {
                AkunGrup.Nama = _nama;
                AkunGrup.IDAkunGrupParent = _idAkunGrupParent.ToInt();
                AkunGrup.EnumJenisAkunGrup = EnumJenisAkunGrup;
                AkunGrup.EnumSaldoNormal = EnumSaldoNormal;
            }
        }

        return AkunGrup;
    }

    public TBAkunGrup Cari(DataClassesDatabaseDataContext db, int idAkunGrup)
    {
        return db.TBAkunGrups.FirstOrDefault(item => item.IDAkunGrup == idAkunGrup);
    }
}
