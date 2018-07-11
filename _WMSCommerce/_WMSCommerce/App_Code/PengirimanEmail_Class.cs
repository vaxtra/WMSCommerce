using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PengirimanEmail_Class : BaseWMSClass
{
    #region DEFAULT
    public PengirimanEmail_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public PengirimanEmail_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public PengirimanEmail_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    public TBPengirimanEmail Kirim(DateTime TanggalKirim, string Tujuan, string Judul, string Isi)
    {
        var PengirimanEmail = new TBPengirimanEmail
        {
            //IDPengirimanEmail
            TanggalKirim = TanggalKirim,
            Tujuan = Tujuan,
            Judul = Judul,
            Isi = Isi
        };

        db.TBPengirimanEmails.InsertOnSubmit(PengirimanEmail);

        return PengirimanEmail;
    }

    public TBPengirimanEmail Kirim(string Tujuan, string Judul, string Isi)
    {
        return Kirim(DateTime.Now, Tujuan, Judul, Isi);
    }
}