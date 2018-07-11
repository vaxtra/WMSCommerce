using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Kurir_Class : BaseWMSClass
{
    #region DEFAULT
    public Kurir_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Kurir_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public Kurir_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBKurir Kurir)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Kurir " + Kurir.Nama + " berhasil");

            Kurir._IDWMSStore = this.Pengguna.IDWMSStore;
            Kurir._IDWMS = Guid.NewGuid();

            Kurir._Urutan = db.TBKurirs.Count() + 1;

            Kurir._TanggalInsert = DateTime.Now;
            Kurir._IDTempatInsert = this.Pengguna.IDTempat;
            Kurir._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Kurir._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Kurir " + Kurir.Nama + " berhasil");

        Kurir._TanggalUpdate = DateTime.Now;
        Kurir._IDTempatUpdate = this.Pengguna.IDTempat;
        Kurir._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBKurir Tambah(string Nama, string Deskripsi)
    {
        TBKurir Kurir = new TBKurir
        {
            Nama = Nama,
            Deskripsi = Deskripsi,
        };

        NotifikasiLog(EnumInsertUpdate.Insert, Kurir);

        db.TBKurirs.InsertOnSubmit(Kurir);

        return Kurir;
    }

    public TBKurir CariTambah(string Nama)
    {
        var Data = Cari(Nama);

        if (Data == null)
            return Tambah(Nama, "");
        else
            return Data;
    }

    public TBKurir CariTambah(Guid IDWMS, string Nama)
    {
        var Data = Cari(IDWMS);

        if (Data == null)
        {
            Data = Tambah(Nama, "");

            //IDWMS SESUAI PARAMETER
            Data._IDWMS = IDWMS;
        }

        return Data;
    }
    #endregion

    #region CARI
    public TBKurir Cari(int IDKurir)
    {
        return db.TBKurirs.FirstOrDefault(item => item.IDKurir == IDKurir);
    }

    public TBKurir Cari(Guid IDWMS)
    {
        return db.TBKurirs.FirstOrDefault(item => item._IDWMS == IDWMS);
    }

    public TBKurir Cari(string Nama)
    {
        return db.TBKurirs.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion
}