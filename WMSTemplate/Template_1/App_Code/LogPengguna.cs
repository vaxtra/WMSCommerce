using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public static partial class LogPengguna
{
    public static TBLogPengguna Tambah(int IDLogPenggunaTipe, int IDPengguna)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var LogPengguna = new TBLogPengguna
            {
                IDLogPenggunaTipe = IDLogPenggunaTipe,
                IDPengguna = IDPengguna,
                Tanggal = DateTime.Now
            };

            db.TBLogPenggunas.InsertOnSubmit(LogPengguna);
            db.SubmitChanges();

            return LogPengguna;
        }
    }
}