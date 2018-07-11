using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TransaksiPrintLog_Class : BaseWMSClass
{
    #region DEFAULT
    public TransaksiPrintLog_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public TransaksiPrintLog_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    public TBTransaksiPrintLog Tambah(EnumPrintLog enumPrintLog, string idTransaksi)
    {
        TBTransaksiPrintLog TransaksiPrintLog = new TBTransaksiPrintLog
        {
            //IDTransaksiPrintLog
            EnumPrintLog = (int)enumPrintLog,
            IDPengguna = Pengguna.IDPengguna,
            IDTempat = Pengguna.IDTempat,
            IDTransaksi = idTransaksi,
            IDWMS = Guid.NewGuid(),
            Tanggal = DateTime.Now
        };

        db.TBTransaksiPrintLogs.InsertOnSubmit(TransaksiPrintLog);

        return TransaksiPrintLog;
    }
}