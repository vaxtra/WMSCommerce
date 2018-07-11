using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

public class AkuntansiJurnal_Class
{
    public TBAkuntansiJurnal Tambah(DataClassesDatabaseDataContext db, int idTempat, int idPengguna, DateTime tanggal, string keterangan, List<TBAkuntansiJurnalDetail> Detail)
    {
        if (Detail.Sum(item => item.Debit) == Detail.Sum(item => item.Kredit))
        {
            var AkuntansiJurnal = new TBAkuntansiJurnal
            {
                //IDAkuntansiJurnal
                IDPengguna = idPengguna,
                IDTempat = idTempat,
                IDWMS = Guid.NewGuid(),
                Keterangan = keterangan,
                Tanggal = tanggal,
                TanggalDaftar = DateTime.Now
            };

            AkuntansiJurnal.TBAkuntansiJurnalDetails.AddRange(Detail);

            db.TBAkuntansiJurnals.InsertOnSubmit(AkuntansiJurnal);

            return AkuntansiJurnal;
        }
        else
            return null;
    }

    public TBAkuntansiJurnal[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBAkuntansiJurnals.ToArray();
    }
    public TBAkuntansiJurnal Cari(DataClassesDatabaseDataContext db, int idAkuntansiJurnal)
    {
        return db.TBAkuntansiJurnals.FirstOrDefault(item => item.IDAkuntansiJurnal == idAkuntansiJurnal);
    }
    public bool Hapus(DataClassesDatabaseDataContext db, int idAkuntansiJurnal)
    {
        var Jurnal = Cari(db, idAkuntansiJurnal);

        if (Jurnal != null)
        {
            foreach (var item in Jurnal.TBAkuntansiDokumens.ToArray())
            {
                string FileNama = HttpContext.Current.Server.MapPath("~/files/Akuntansi/") + item.IDAkuntansiDokumen + item.Format;

                if (File.Exists(FileNama))
                    File.Delete(FileNama);
            }

            db.TBAkuntansiDokumens.DeleteAllOnSubmit(Jurnal.TBAkuntansiDokumens);
            db.TBAkuntansiJurnalDetails.DeleteAllOnSubmit(Jurnal.TBAkuntansiJurnalDetails);
            db.TBAkuntansiJurnals.DeleteOnSubmit(Jurnal);

            return true;
        }
        else
            return false;
    }
}
