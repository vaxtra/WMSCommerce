using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class AtributProduk_Class : BaseWMSClass
{
    #region DEFAULT
    public AtributProduk_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public AtributProduk_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public AtributProduk_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBAtributProduk AtributProduk)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Atribut Produk " + AtributProduk.Nama + " berhasil");

            AtributProduk._IDWMSStore = this.Pengguna.IDWMSStore;
            AtributProduk._IDWMS = Guid.NewGuid();

            AtributProduk._Urutan = db.TBAtributProduks.Count() + 1;

            AtributProduk._TanggalInsert = DateTime.Now;
            AtributProduk._IDTempatInsert = this.Pengguna.IDTempat;
            AtributProduk._IDPenggunaInsert = this.Pengguna.IDPengguna;

            AtributProduk._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Atribut Produk " + AtributProduk.Nama + " berhasil");

        AtributProduk._TanggalUpdate = DateTime.Now;
        AtributProduk._IDTempatUpdate = this.Pengguna.IDTempat;
        AtributProduk._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBAtributProduk Tambah(string AtributProdukGrup, string Nama)
    {
        AtributProdukGrup_Class ClassAtributProdukGrup = new AtributProdukGrup_Class(db);

        return Tambah(ClassAtributProdukGrup.CariTambah(AtributProdukGrup), Nama);
    }

    public TBAtributProduk Tambah(TBAtributProdukGrup AtributProdukGrup, string Nama)
    {
        var AtributProduk = new TBAtributProduk
        {
            TBAtributProdukGrup = AtributProdukGrup,
            Nama = Nama,
            _IsActive = true
        };

        NotifikasiLog(EnumInsertUpdate.Insert, AtributProduk);

        db.TBAtributProduks.InsertOnSubmit(AtributProduk);

        return AtributProduk;
    }

    public TBAtributProduk CariTambah(string AtributProdukGrup, string Nama)
    {
        var AtributProduk = Cari(Nama);

        if (AtributProduk == null)
            AtributProduk = Tambah(AtributProdukGrup, Nama);

        return AtributProduk;
    }
    #endregion

    #region UBAH
    public TBAtributProduk Ubah(int IDAtributProduk, string Nama)
    {
        var AtributProduk = Cari(IDAtributProduk);

        AtributProduk.Nama = Nama;

        NotifikasiLog(EnumInsertUpdate.Update, AtributProduk);

        return AtributProduk;
    }
    #endregion

    #region CARI
    public TBAtributProduk Cari(int IDAtributProduk)
    {
        return db.TBAtributProduks.FirstOrDefault(item => item.IDAtributProduk == IDAtributProduk);
    }

    public TBAtributProduk Cari(string Nama)
    {
        return db.TBAtributProduks.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion

    public ListItem[] Dropdownlist()
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua - "
        });

        ListData.AddRange(db.TBAtributProduks.Select(item => new ListItem
        {
            Value = item.IDAtributProduk.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }

    public bool Hapus(int idAtributProduk)
    {
        var AtributProduk = Cari(idAtributProduk);

        if (AtributProduk != null)
        {
            if (AtributProduk.TBKombinasiProduks.Count == 0)
            {
                db.TBAtributProduks.DeleteOnSubmit(AtributProduk);

                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public TBAtributProduk[] Data()
    {
        return db.TBAtributProduks.OrderBy(item => item._Urutan).ToArray();
    }
}