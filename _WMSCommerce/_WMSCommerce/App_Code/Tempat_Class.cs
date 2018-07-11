using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Tempat_Class : BaseWMSClass
{
    #region DEFAULT
    public Tempat_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Tempat_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public Tempat_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBTempat Tempat)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Tempat " + Tempat.Nama + " berhasil");

            Tempat._IDWMSStore = this.Pengguna.IDWMSStore;
            Tempat._IDWMS = Guid.NewGuid();

            Tempat._Urutan = db.TBTempats.Count() + 1;

            Tempat._TanggalInsert = DateTime.Now;
            Tempat._IDTempatInsert = this.Pengguna.IDTempat;
            Tempat._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Tempat._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Tempat " + Tempat.Nama + " berhasil");

        Tempat._TanggalUpdate = DateTime.Now;
        Tempat._IDTempatUpdate = this.Pengguna.IDTempat;
        Tempat._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBTempat Tambah(int IDKategoriTempat, string Kode, string Nama, string Alamat, string Email, string Telepon1, string Telepon2, string KeteranganBiayaTambahan1, decimal BiayaTambahan1, string KeteranganBiayaTambahan2, decimal BiayaTambahan2, string KeteranganBiayaTambahan3, decimal BiayaTambahan3, string KeteranganBiayaTambahan4, decimal BiayaTambahan4, string Latitude, string Longitude, string FooterPrint, bool _IsActive)
    {
        TBTempat Tempat = new TBTempat
        {
            //DEFAULT DARI PENGGUNA
            IDStore = this.Pengguna.IDStore,

            IDKategoriTempat = IDKategoriTempat,
            Kode = string.IsNullOrWhiteSpace(Kode) ? DateTime.Now.ToString("MMyyyy") + db.TBTempats.Count().ToString() : Kode,
            Nama = Nama,
            Alamat = Alamat,
            Email = Email,
            Telepon1 = Telepon1,
            Telepon2 = Telepon2,

            EnumBiayaTambahan1 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan1) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase,
            KeteranganBiayaTambahan1 = KeteranganBiayaTambahan1,
            BiayaTambahan1 = BiayaTambahan1,

            EnumBiayaTambahan2 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan2) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase,
            KeteranganBiayaTambahan2 = KeteranganBiayaTambahan2,
            BiayaTambahan2 = BiayaTambahan2,

            EnumBiayaTambahan3 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan3) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase,
            KeteranganBiayaTambahan3 = KeteranganBiayaTambahan3,
            BiayaTambahan3 = BiayaTambahan3,

            EnumBiayaTambahan4 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan4) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase,
            KeteranganBiayaTambahan4 = KeteranganBiayaTambahan4,
            BiayaTambahan4 = BiayaTambahan4,

            Latitude = !string.IsNullOrWhiteSpace(Latitude) ? Latitude : "0",
            Longitude = !string.IsNullOrWhiteSpace(Longitude) ? Longitude : "0",

            FooterPrint = FooterPrint,

            _IsActive = _IsActive
        };

        #region TEMPAT JARAK
        foreach (var item in db.TBTempats.ToArray())
        {
            db.TBTempatJaraks.InsertOnSubmit(new TBTempatJarak
            {
                TBTempat = Tempat,
                TBTempat1 = item,
                Jarak = "",
                JarakNilai = 0,
                Durasi = "",
                DurasiNilai = 0
            });
        }
        #endregion

        NotifikasiLog(EnumInsertUpdate.Insert, Tempat);

        db.TBTempats.InsertOnSubmit(Tempat);

        return Tempat;
    }

    public TBTempat Tambah(string Nama)
    {
        return Tambah((int)EnumKategoriTempat.Store, "", Nama, "", "", "", "", "", 0, "", 0, "", 0, "", 0, "", "", "", true);
    }
    #endregion

    #region UBAH
    public TBTempat Ubah(int IDTempat, int IDKategoriTempat, string Kode, string Nama, string Alamat, string Email, string Telepon1, string Telepon2, string KeteranganBiayaTambahan1, decimal BiayaTambahan1, string KeteranganBiayaTambahan2, decimal BiayaTambahan2, string KeteranganBiayaTambahan3, decimal BiayaTambahan3, string KeteranganBiayaTambahan4, decimal BiayaTambahan4, string Latitude, string Longitude, string FooterPrint)
    {
        var Tempat = Cari(IDTempat);

        if (Tempat != null)
        {
            //DEFAULT DARI PENGGUNA
            Tempat.IDStore = this.Pengguna.IDStore;

            Tempat.IDKategoriTempat = IDKategoriTempat;
            Tempat.Kode = string.IsNullOrWhiteSpace(Kode) ? DateTime.Now.ToString("MMyyyy") + db.TBTempats.Count().ToString() : Kode;
            Tempat.Nama = Nama;
            Tempat.Alamat = Alamat;
            Tempat.Email = Email;
            Tempat.Telepon1 = Telepon1;
            Tempat.Telepon2 = Telepon2;

            Tempat.EnumBiayaTambahan1 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan1) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase;
            Tempat.KeteranganBiayaTambahan1 = KeteranganBiayaTambahan1;
            Tempat.BiayaTambahan1 = BiayaTambahan1;

            Tempat.EnumBiayaTambahan2 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan2) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase;
            Tempat.KeteranganBiayaTambahan2 = KeteranganBiayaTambahan2;
            Tempat.BiayaTambahan2 = BiayaTambahan2;

            Tempat.EnumBiayaTambahan3 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan3) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase;
            Tempat.KeteranganBiayaTambahan3 = KeteranganBiayaTambahan3;
            Tempat.BiayaTambahan3 = BiayaTambahan3;

            Tempat.EnumBiayaTambahan4 = string.IsNullOrWhiteSpace(KeteranganBiayaTambahan4) ? (int)EnumBiayaTambahan.TidakAda : (int)EnumBiayaTambahan.Persentase;
            Tempat.KeteranganBiayaTambahan4 = KeteranganBiayaTambahan4;
            Tempat.BiayaTambahan4 = BiayaTambahan4;

            Tempat.Latitude = !string.IsNullOrWhiteSpace(Latitude) ? Latitude : "0";
            Tempat.Longitude = !string.IsNullOrWhiteSpace(Longitude) ? Longitude : "0";

            Tempat.FooterPrint = FooterPrint;

            //_IsActive

            #region TEMPAT JARAK
            if ((Tempat.Latitude != Latitude) || (Tempat.Longitude != Longitude))
            {
                //TERJADI PERUBAHAN LATITUDE DAN LONGITUDE
                //MAKA MELAKUKAN PERUBAHAN JARAK

                foreach (var item in Tempat.TBTempatJaraks.ToArray())
                {
                    item.Jarak = "";
                    item.JarakNilai = 0;
                    item.Durasi = "";
                    item.DurasiNilai = 0;
                }

                foreach (var item in Tempat.TBTempatJaraks1.ToArray())
                {
                    item.Jarak = "";
                    item.JarakNilai = 0;
                    item.Durasi = "";
                    item.DurasiNilai = 0;
                }
            }
            #endregion

            NotifikasiLog(EnumInsertUpdate.Update, Tempat);

            return Tempat;
        }
        else
            return null;
    }
    #endregion

    #region CARI
    public TBTempat Cari(int IDTempat)
    {
        return db.TBTempats.FirstOrDefault(item => item.IDTempat == IDTempat);
    }

    public TBTempat Cari(Guid _IDWMS)
    {
        return db.TBTempats.FirstOrDefault(item => item._IDWMS == _IDWMS);
    }

    public TBTempat Cari(string Nama)
    {
        return db.TBTempats.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion

    public void Hapus(int IDTempat)
    {
        var Tempat = Cari(IDTempat);

        try
        {
            if (Tempat != null)
            {
                db.TBTempatJaraks.DeleteAllOnSubmit(Tempat.TBTempatJaraks);
                db.TBTempatJaraks.DeleteAllOnSubmit(Tempat.TBTempatJaraks1);

                db.TBTempats.DeleteOnSubmit(Tempat);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data Tempat berhasil");

                db.SubmitChanges();
            }
            else
                ErrorMessage = "Data tidak ditemukan";
        }
        catch (Exception ex)
        {
            if (ex.Message.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                ErrorMessage = "Data tidak bisa dihapus karena digunakan data lain";
            else
                throw;
        }
    }

    public TBTempat[] Data()
    {
        return db.TBTempats.OrderBy(item => item.Nama).ToArray();
    }

    public TBTempat[] Data(int IDStore)
    {
        return db.TBTempats.Where(item => item.IDStore == IDStore).OrderBy(item => item.Nama).ToArray();
    }

    public ListItem[] DataDropDownList()
    {
        List<ListItem> Tempat = new List<ListItem>();

        Tempat.Add(new ListItem { Value = "0", Text = "- Semua -" });

        Tempat.AddRange(Data().Select(item => new ListItem
        {
            Value = item.IDTempat.ToString(),
            Text = item.Nama
        }));

        return Tempat.ToArray();
    }

    public void DropDownList(DropDownList dropDownList)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDTempat";
        dropDownList.DataTextField = "Nama";
        dropDownList.DataBind();

        dropDownList.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua Tempat -" });
    }

    public void DataListBox(ListBox listBox)
    {
        listBox.DataSource = Data();
        listBox.DataValueField = "IDTempat";
        listBox.DataTextField = "Nama";
        listBox.DataBind();
    }
}