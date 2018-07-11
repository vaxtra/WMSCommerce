using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class KurirBiaya_Class : BaseWMSClass
{
    #region DEFAULT
    public KurirBiaya_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public KurirBiaya_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public KurirBiaya_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBKurirBiaya KurirBiaya)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Biaya Kurir " + KurirBiaya.TBKurir.Nama + " berhasil");

            KurirBiaya._IDWMSStore = this.Pengguna.IDWMSStore;
            KurirBiaya._IDWMS = Guid.NewGuid();

            KurirBiaya._Urutan = db.TBKurirBiayas.Count() + 1;

            KurirBiaya._TanggalInsert = DateTime.Now;
            KurirBiaya._IDTempatInsert = this.Pengguna.IDTempat;
            KurirBiaya._IDPenggunaInsert = this.Pengguna.IDPengguna;

            KurirBiaya._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Biaya Kurir " + KurirBiaya.TBKurir.Nama + " berhasil");

        KurirBiaya._TanggalUpdate = DateTime.Now;
        KurirBiaya._IDTempatUpdate = this.Pengguna.IDTempat;
        KurirBiaya._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBKurirBiaya TambahUbah(Guid IDWMS, Guid IDWMSKurir, Guid IDWMSWilayah, decimal Biaya)
    {
        var KurirBiaya = Cari(IDWMS);

        Kurir_Class ClassKurir = new Kurir_Class(db, Pengguna);
        Wilayah_Class ClassWilayah = new Wilayah_Class(db, Pengguna);

        var DataKurir = ClassKurir.Cari(IDWMSKurir);

        if (DataKurir == null)
            ErrorMessage = "Kurir tidak ditemukan";

        var DataWilayah = ClassWilayah.Cari(IDWMSWilayah);

        if (DataWilayah == null)
            ErrorMessage = "Wilayah tidak ditemukan";

        if (KurirBiaya == null)
        {
            KurirBiaya = new TBKurirBiaya
            {
                //IDKurirBiaya
                //IDKurir
                TBKurir = DataKurir,

                //IDWilayah
                TBWilayah = DataWilayah,
                Biaya = Biaya
            };

            NotifikasiLog(EnumInsertUpdate.Insert, KurirBiaya);

            //IDWMS SESUAI PARAMETER
            KurirBiaya._IDWMS = IDWMS;

            db.TBKurirBiayas.InsertOnSubmit(KurirBiaya);
        }
        else
        {
            KurirBiaya.TBKurir = DataKurir;
            KurirBiaya.TBWilayah = DataWilayah;
            KurirBiaya.Biaya = Biaya;

            NotifikasiLog(EnumInsertUpdate.Update, KurirBiaya);
        }

        return KurirBiaya;
    }
    #endregion

    #region CARI
    public TBKurirBiaya Cari(Guid IDWMS)
    {
        return db.TBKurirBiayas.FirstOrDefault(item => item._IDWMS == IDWMS);
    }
    #endregion
}