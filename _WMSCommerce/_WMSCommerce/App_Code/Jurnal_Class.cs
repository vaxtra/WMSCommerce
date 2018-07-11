using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Jurnal_Class
{
    public TBJurnal Tambah(DataClassesDatabaseDataContext db, DateTime tanggal, string keterangan, int idPengguna, string referensi, List<TBJurnalDetail> Detail)
    {
        TBJurnal Jurnal = new TBJurnal
        {
            Tanggal = tanggal,
            Keterangan = keterangan,
            IDPengguna = idPengguna,
            Referensi = referensi
        };

        Jurnal.TBJurnalDetails.AddRange(Detail);

        db.TBJurnals.InsertOnSubmit(Jurnal);

        return Jurnal;

        ////1 - KAS
        //Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        //{
        //    IDAkun = 1,
        //    Debit = GrandTotal,
        //    Kredit = 0
        //});

        ////7 - PENJUALAN
        //Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
        //{
        //    IDAkun = 7,
        //    Debit = 0,
        //    Kredit = GrandTotal
        //});
    }

    public TBJurnal[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBJurnals.ToArray();
    }
    public TBJurnal Cari(DataClassesDatabaseDataContext db, int idJurnal)
    {
        return db.TBJurnals.FirstOrDefault(item => item.IDJurnal == idJurnal);
    }
}