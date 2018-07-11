using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class PemilikProduk_Class : BaseWMSClass
{
    #region DEFAULT
    public PemilikProduk_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public PemilikProduk_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public PemilikProduk_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBPemilikProduk PemilikProduk)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Pemilik Produk " + PemilikProduk.Nama + " berhasil");

            PemilikProduk._IDWMSStore = this.Pengguna.IDWMSStore;
            PemilikProduk._IDWMS = Guid.NewGuid();

            PemilikProduk._Urutan = db.TBPemilikProduks.Count() + 1;

            PemilikProduk._TanggalInsert = DateTime.Now;
            PemilikProduk._IDTempatInsert = this.Pengguna.IDTempat;
            PemilikProduk._IDPenggunaInsert = this.Pengguna.IDPengguna;

            PemilikProduk._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Pemilik Produk " + PemilikProduk.Nama + " berhasil");

        PemilikProduk._TanggalUpdate = DateTime.Now;
        PemilikProduk._IDTempatUpdate = this.Pengguna.IDTempat;
        PemilikProduk._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBPemilikProduk Tambah(string Nama, string Alamat, string Email, string Telepon1, string Telepon2)
    {
        var PemilikProduk = new TBPemilikProduk
        {
            Nama = Nama,
            Alamat = Alamat,
            Email = Email,
            Telepon1 = Telepon1,
            Telepon2 = Telepon2,
            _IsActive = true
        };

        NotifikasiLog(EnumInsertUpdate.Insert, PemilikProduk);

        db.TBPemilikProduks.InsertOnSubmit(PemilikProduk);

        return PemilikProduk;
    }

    public TBPemilikProduk Tambah(string Nama)
    {
        return Tambah(Nama, "", "", "", "");
    }

    public TBPemilikProduk CariTambah(string Nama)
    {
        var PemilikProduk = Cari(Nama);

        if (PemilikProduk == null)
            PemilikProduk = Tambah(Nama);

        return PemilikProduk;
    }
    #endregion

    #region UBAH

    #endregion

    #region CARI
    public TBPemilikProduk Cari(int IDPemilikProduk)
    {
        return db.TBPemilikProduks.FirstOrDefault(item => item.IDPemilikProduk == IDPemilikProduk);
    }
    public TBPemilikProduk Cari(string Nama)
    {
        return db.TBPemilikProduks.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion

    public bool Hapus(int IDPemilikProduk)
    {
        var PemilikProduk = Cari(IDPemilikProduk);

        if (PemilikProduk != null)
        {
            db.TBPemilikProduks.DeleteOnSubmit(PemilikProduk);

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data Pemilik Produk berhasil");

            return true;
        }
        else
            return false;
    }

    public TBPemilikProduk[] Data()
    {
        return db.TBPemilikProduks.OrderBy(item => item.Nama).ToArray();
    }

    public ListItem[] Dropdownlist()
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua - "
        });

        ListData.AddRange(db.TBPemilikProduks.Select(item => new ListItem
        {
            Value = item.IDPemilikProduk.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }
}