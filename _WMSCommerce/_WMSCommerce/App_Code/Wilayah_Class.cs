using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Wilayah_Class : BaseWMSClass
{
    #region DEFAULT
    public Wilayah_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Wilayah_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public Wilayah_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBWilayah Wilayah)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Wilayah " + Wilayah.Nama + " berhasil");

            Wilayah._IDWMSStore = this.Pengguna.IDWMSStore;
            Wilayah._IDWMS = Guid.NewGuid();

            Wilayah._Urutan = db.TBWilayahs.Count() + 1;

            Wilayah._TanggalInsert = DateTime.Now;
            Wilayah._IDTempatInsert = this.Pengguna.IDTempat;
            Wilayah._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Wilayah._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Wilayah " + Wilayah.Nama + " berhasil");

        Wilayah._TanggalUpdate = DateTime.Now;
        Wilayah._IDTempatUpdate = this.Pengguna.IDTempat;
        Wilayah._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    public TBWilayah Negara(string negara)
    {
        return db.TBWilayahs
            .FirstOrDefault(item =>
                item.IDGrupWilayah == (int)EnumWilayahGrup.Negara &&
                item.Nama.ToLower() == negara.ToLower()); //NEGARA
    }

    public TBWilayah Provinsi(string negara, string provinsi)
    {
        return db.TBWilayahs
            .FirstOrDefault(item =>
                item.IDGrupWilayah == (int)EnumWilayahGrup.Provinsi &&
                item.Nama.ToLower() == provinsi.ToLower() && //PROVINSI
                item.TBWilayah1.Nama.ToLower() == negara.ToLower()); //NEGARA
    }

    public TBWilayah Kota(string negara, string provinsi, string kota)
    {
        return db.TBWilayahs
            .FirstOrDefault(item =>
                item.IDGrupWilayah == (int)EnumWilayahGrup.Kota &&
                item.Nama.ToLower() == kota.ToLower() && //KOTA
                item.TBWilayah1.Nama.ToLower() == provinsi.ToLower() && //PROVINSI
                item.TBWilayah1.TBWilayah1.Nama.ToLower() == negara.ToLower()); //NEGARA
    }

    public TBWilayah Zona(string negara, string provinsi, string kota, string zona)
    {
        return db.TBWilayahs
            .FirstOrDefault(item =>
            item.IDGrupWilayah == (int)EnumWilayahGrup.Zona &&
            item.Nama.ToLower() == zona.ToLower() && //ZONA
            item.TBWilayah1.Nama.ToLower() == kota.ToLower() && //KOTA
            item.TBWilayah1.TBWilayah1.Nama.ToLower() == provinsi.ToLower() && //PROVINSI
            item.TBWilayah1.TBWilayah1.TBWilayah1.Nama.ToLower() == negara.ToLower()); //NEGARA
    }

    #region CARI
    public TBWilayah Cari(Guid IDWMS)
    {
        return db.TBWilayahs.FirstOrDefault(item => item._IDWMS == IDWMS);
    }
    #endregion

    public TBWilayah TambahUbah(Guid IDWMS, EnumWilayahGrup enumWilayahGrup, Guid? IDWMSWilayahParent, string Nama)
    {
        var Wilayah = Cari(IDWMS);

        if (Wilayah == null)
        {
            Wilayah = new TBWilayah
            {
                IDGrupWilayah = (int)enumWilayahGrup,
                Nama = Nama
            };

            NotifikasiLog(EnumInsertUpdate.Insert, Wilayah);

            //IDWMS SESUAI PARAMETER
            Wilayah._IDWMS = IDWMS;

            db.TBWilayahs.InsertOnSubmit(Wilayah);
        }
        else
        {
            Wilayah.IDGrupWilayah = (int)enumWilayahGrup;
            Wilayah.Nama = Nama;

            NotifikasiLog(EnumInsertUpdate.Update, Wilayah);
        }

        //JIKA BUKAN NEGARA MAKA PARENT WILAYAH DIISI
        if (enumWilayahGrup != EnumWilayahGrup.Negara)
        {
            var WilayahParent = Cari(IDWMSWilayahParent.Value);

            if (WilayahParent == null)
                ErrorMessage = "Wilayah Parent tidak ditemukan";

            Wilayah.TBWilayah1 = WilayahParent;
        }
        else
            Wilayah.TBWilayah1 = null;

        return Wilayah;
    }
}