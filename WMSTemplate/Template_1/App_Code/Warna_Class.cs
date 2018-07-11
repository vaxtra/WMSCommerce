using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Warna_Class : BaseWMSClass
{
    #region DEFAULT
    public Warna_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Warna_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public Warna_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBWarna Warna)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Warna " + Warna.Nama + " berhasil");

            Warna._IDWMSStore = this.Pengguna.IDWMSStore;
            Warna._IDWMS = Guid.NewGuid();

            Warna._Urutan = db.TBWarnas.Count() + 1;

            Warna._TanggalInsert = DateTime.Now;
            Warna._IDTempatInsert = this.Pengguna.IDTempat;
            Warna._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Warna._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Warna " + Warna.Nama + " berhasil");

        Warna._TanggalUpdate = DateTime.Now;
        Warna._IDTempatUpdate = this.Pengguna.IDTempat;
        Warna._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBWarna Tambah(string Kode, string Nama)
    {
        var Warna = new TBWarna
        {
            Kode = Kode,
            Nama = Nama,
            _IsActive = true
        };

        NotifikasiLog(EnumInsertUpdate.Insert, Warna);

        db.TBWarnas.InsertOnSubmit(Warna);

        return Warna;
    }

    public TBWarna Tambah(string Nama)
    {
        return Tambah("", Nama);
    }

    public TBWarna CariTambah(string Nama)
    {
        var Warna = Cari(Nama);

        if (Warna == null)
            Warna = Tambah(Nama);

        return Warna;
    }
    #endregion

    #region UBAH
    public TBWarna Ubah(int IDWarna, string Kode, string Nama)
    {
        var Warna = Cari(IDWarna);

        if (Warna != null)
        {
            Warna.Kode = Kode;
            Warna.Nama = Nama;

            NotifikasiLog(EnumInsertUpdate.Update, Warna);

            return Warna;
        }
        else
            return null;
    }
    #endregion

    #region CARI
    public TBWarna Cari(int IDWarna)
    {
        return db.TBWarnas.FirstOrDefault(item => item.IDWarna == IDWarna);
    }

    public TBWarna Cari(string Nama)
    {
        return db.TBWarnas.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion

    public bool Hapus(int IDWarna)
    {
        var Warna = Cari(IDWarna);

        if (Warna != null)
        {
            if (Warna.TBProduks.Count == 0)
            {
                db.TBWarnas.DeleteOnSubmit(Warna);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data Warna berhasil");

                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public TBWarna[] Data()
    {
        return db.TBWarnas.OrderBy(item => item.Nama).ToArray();
    }

    public ListItem[] Dropdownlist()
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua -"
        });

        ListData.AddRange(db.TBWarnas.Select(item => new ListItem
        {
            Value = item.IDWarna.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }
}