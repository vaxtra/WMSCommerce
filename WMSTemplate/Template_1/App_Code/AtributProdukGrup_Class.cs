using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AtributProdukGrup_Class : BaseWMSClass
{
    #region DEFAULT
    public AtributProdukGrup_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public AtributProdukGrup_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public AtributProdukGrup_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBAtributProdukGrup AtributProdukGrup)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Grup Atribut Produk " + AtributProdukGrup.Nama + " berhasil");

            AtributProdukGrup._IDWMSStore = this.Pengguna.IDWMSStore;
            AtributProdukGrup._IDWMSAtributProdukGrup = Guid.NewGuid();

            AtributProdukGrup._Urutan = db.TBAtributProdukGrups.Count() + 1;

            AtributProdukGrup._TanggalInsert = DateTime.Now;
            AtributProdukGrup._IDTempatInsert = this.Pengguna.IDTempat;
            AtributProdukGrup._IDPenggunaInsert = this.Pengguna.IDPengguna;

            AtributProdukGrup._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Grup Atribut Produk " + AtributProdukGrup.Nama + " berhasil");

        AtributProdukGrup._TanggalUpdate = DateTime.Now;
        AtributProdukGrup._IDTempatUpdate = this.Pengguna.IDTempat;
        AtributProdukGrup._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBAtributProdukGrup Tambah(string Nama)
    {
        var AtributProdukGrup = new TBAtributProdukGrup
        {
            Nama = Nama,
            _IsActive = true
        };

        NotifikasiLog(EnumInsertUpdate.Insert, AtributProdukGrup);

        db.TBAtributProdukGrups.InsertOnSubmit(AtributProdukGrup);

        return AtributProdukGrup;
    }

    public TBAtributProdukGrup CariTambah(string Nama)
    {
        var AtributProdukGrup = Cari(Nama);

        if (AtributProdukGrup == null)
            AtributProdukGrup = Tambah(Nama);

        return AtributProdukGrup;
    }
    #endregion

    #region CARI
    public TBAtributProdukGrup Cari(string Nama)
    {
        return db.TBAtributProdukGrups.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion

    public TBAtributProdukGrup[] Data()
    {
        return db.TBAtributProdukGrups.OrderBy(item => item._Urutan).ToArray();
    }
}