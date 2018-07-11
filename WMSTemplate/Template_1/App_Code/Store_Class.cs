using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Store_Class : BaseWMSClass
{
    #region DEFAULT
    public Store_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Store_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public Store_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    public TBStore Data()
    {
        return db.TBStores.FirstOrDefault();
    }

    public TBStore[] DataStore()
    {
        return db.TBStores.OrderBy(item => item.Nama).ToArray();
    }

    public TBStore Cari(int IDStore)
    {
        return db.TBStores.FirstOrDefault(item => item.IDStore == IDStore);
    }

    public TBStore Ubah(string Nama, string Alamat, string Email, string KodePos, string Handphone,
        string TeleponLain, string Website, string SMTPServer, int SMTPPort, string SMTPUser, string SMTPPassword, bool SecureSocketsLayer)
    {
        var Store = Data();

        //IDStore
        Store.Nama = Nama;
        Store.Alamat = Alamat;
        Store.Email = Email;
        Store.KodePos = KodePos;
        Store.Handphone = Handphone;
        Store.TeleponLain = TeleponLain;
        Store.Website = Website;
        Store.SMTPServer = SMTPServer;
        Store.SMTPPort = SMTPPort;
        Store.SMTPUser = SMTPUser;
        Store.SMTPPassword = SMTPPassword;
        Store.SecureSocketsLayer = SecureSocketsLayer;

        return Store;
    }

}