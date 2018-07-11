using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SyncData_Class : BaseWMSClass
{
    #region DEFAULT
    public SyncData_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public SyncData_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public SyncData_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    #region NOTIFIKASI MESSAGE
    private string notifikasiMessage;
    public string NotifikasiMessage
    {
        get
        {
            return notifikasiMessage;
        }
    }
    #endregion

    #region TAMBAH
    public TBSyncData Tambah(Guid IDWMSStore, Guid IDWMSTempat, Guid IDWMSPengguna, string Data)
    {
        TBSyncData SyncData = new TBSyncData
        {
            //IDSyncData
            IDWMSSyncData = Guid.NewGuid(),
            IDWMSStore = IDWMSStore,
            IDWMSTempat = IDWMSTempat,
            IDWMSPengguna = IDWMSPengguna,
            TanggalUpload = DateTime.Now,

            //TanggalUploadFinish
            //TanggalSync

            Data = Data
        };

        db.TBSyncDatas.InsertOnSubmit(SyncData);

        return SyncData;
    }
    #endregion

    #region CARI
    public TBSyncData Cari(Guid IDWMSSyncData)
    {
        return db.TBSyncDatas.FirstOrDefault(item => item.IDWMSSyncData == IDWMSSyncData);
    }
    #endregion

    #region PROSES
    public TBSyncData UploadFinish(Guid IDWMSSyncData)
    {
        var SyncData = Cari(IDWMSSyncData);

        //JIKA SUDAH DI UPDATE TANGGAL UPLOAD FINISH TIDAK PERLU UPDATE LAGI
        if (SyncData != null && !SyncData.TanggalUploadFinish.HasValue)
            SyncData.TanggalUploadFinish = DateTime.Now;

        return SyncData;
    }
    #endregion
}