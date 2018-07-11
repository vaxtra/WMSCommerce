using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Pengguna_Class : BaseWMSClass
{
    #region DEFAULT
    public Pengguna_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Pengguna_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public Pengguna_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBPengguna Pengguna)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, this.Pengguna.IDPengguna, notifikasiMessage = "Tambah Pengguna " + Pengguna.NamaLengkap + " berhasil");

            Pengguna._IDWMSStore = this.Pengguna.IDWMSStore;
            Pengguna._IDWMS = Guid.NewGuid();

            Pengguna._Urutan = db.TBPenggunas.Count() + 1;

            Pengguna._TanggalInsert = DateTime.Now;
            Pengguna._IDTempatInsert = this.Pengguna.IDTempat;
            Pengguna._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Pengguna._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, this.Pengguna.IDPengguna, notifikasiMessage = "Tambah Pengguna " + Pengguna.NamaLengkap + " berhasil");

        Pengguna._TanggalUpdate = DateTime.Now;
        Pengguna._IDTempatUpdate = this.Pengguna.IDTempat;
        Pengguna._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    private void ValidasiInput(int IDGrupPengguna, int IDTempat, string NamaLengkap, string Username, string Handphone)
    {
        if (IDGrupPengguna == 0)
            ErrorMessage = "Grup Pegawai harus diisi";

        if (IDTempat == 0)
            ErrorMessage = "Lokasi harus diisi";

        if (string.IsNullOrWhiteSpace(NamaLengkap))
            ErrorMessage = "Nama Lengkap harus diisi";

        if (string.IsNullOrWhiteSpace(Username))
            ErrorMessage = "Username harus diisi";

        if (string.IsNullOrWhiteSpace(Handphone))
            ErrorMessage = "Handphone harus diisi";
    }

    #region TAMBAH
    public TBPengguna Tambah(int IDGrupPengguna, int IDTempat, string NomorIdentitas, string NomorNPWP, string NomorRekening, string NamaBank, string NamaRekening, string NamaLengkap, string TempatLahir, DateTime TanggalLahir, bool JenisKelamin, string Alamat, string Agama, string Telepon, string Handphone, string Email, string StatusPerkawinan, string Kewarganegaraan, string PendidikanTerakhir, DateTime TanggalBekerja, string Username, string Password, string PIN, string Catatan)
    {
        //VALIDASI
        ValidasiInput(IDGrupPengguna, IDTempat, NamaLengkap, Username, Handphone);

        #region VALIDASI
        if (string.IsNullOrWhiteSpace(Password))
            ErrorMessage = "Password harus diisi";
        #endregion

        #region VALIDASI USERNAME DUPLIKAT
        TBPengguna Pengguna = db.TBPenggunas.FirstOrDefault(item => item.Username == Username);

        if (Pengguna != null)
            ErrorMessage = "Gunakan Username lain, Username ini sudah digunakan";
        #endregion

        Pengguna = new TBPengguna
        {
            //IDPengguna
            //IDPenggunaParent
            IDGrupPengguna = IDGrupPengguna,
            IDTempat = IDTempat,
            NomorIdentitas = NomorIdentitas,
            NomorNPWP = NomorNPWP,
            NomorRekening = NomorRekening,
            NamaBank = NamaBank,
            NamaRekening = NamaRekening,
            NamaLengkap = NamaLengkap,
            TempatLahir = TempatLahir,
            TanggalLahir = TanggalLahir,
            JenisKelamin = JenisKelamin,
            Alamat = Alamat,
            Agama = Agama,
            Telepon = Telepon,
            Handphone = Handphone,
            Email = Email,
            StatusPerkawinan = StatusPerkawinan,
            Kewarganegaraan = Kewarganegaraan,
            PendidikanTerakhir = PendidikanTerakhir,
            TanggalBekerja = TanggalBekerja,
            Username = Username,
            Password = Password,
            PIN = PIN,
            Catatan = Catatan,

            EkstensiFoto = "",
            RFID = "",
            SidikJari = "",
            GajiPokok = 0,
            JaminanHariTua = 0,
            JaminanKecelakaan = 0,
            PPH21 = 0,
            TunjanganHariRaya = 0,
            TunjanganMakan = 0,
            TunjanganTransportasi = 0
        };

        NotifikasiLog(EnumInsertUpdate.Insert, Pengguna);

        db.TBPenggunas.InsertOnSubmit(Pengguna);

        return Pengguna;
    }
    #endregion

    #region UBAH
    public TBPengguna Ubah(int IDPengguna, int IDGrupPengguna, int IDTempat, string NomorIdentitas, string NomorNPWP, string NomorRekening, string NamaBank, string NamaRekening, string NamaLengkap, string TempatLahir, DateTime TanggalLahir, bool JenisKelamin, string Alamat, string Agama, string Telepon, string Handphone, string Email, string StatusPerkawinan, string Kewarganegaraan, string PendidikanTerakhir, DateTime TanggalBekerja, string Username, string Password, string PIN, string Catatan, bool _IsActive)
    {
        //VALIDASI
        ValidasiInput(IDGrupPengguna, IDTempat, NamaLengkap, Username, Handphone);

        #region VALIDASI USERNAME DUPLIKAT
        TBPengguna Pengguna = db.TBPenggunas
            .FirstOrDefault(item =>
                item.IDPengguna != IDPengguna &&
                item.Username == Username);

        if (Pengguna != null)
            ErrorMessage = "Gunakan Username lain, Username ini sudah digunakan";
        #endregion

        Pengguna = Cari(IDPengguna);

        //IDPengguna
        //IDPenggunaParent
        Pengguna.IDGrupPengguna = IDGrupPengguna;
        Pengguna.IDTempat = IDTempat;
        Pengguna.NomorIdentitas = NomorIdentitas;
        Pengguna.NomorNPWP = NomorNPWP;
        Pengguna.NomorRekening = NomorRekening;
        Pengguna.NamaBank = NamaBank;
        Pengguna.NamaRekening = NamaRekening;
        Pengguna.NamaLengkap = NamaLengkap;
        Pengguna.TempatLahir = TempatLahir;
        Pengguna.TanggalLahir = TanggalLahir;
        Pengguna.JenisKelamin = JenisKelamin;
        Pengguna.Alamat = Alamat;
        Pengguna.Agama = Agama;
        Pengguna.Telepon = Telepon;
        Pengguna.Handphone = Handphone;
        Pengguna.Email = Email;
        Pengguna.StatusPerkawinan = StatusPerkawinan;
        Pengguna.Kewarganegaraan = Kewarganegaraan;
        Pengguna.PendidikanTerakhir = PendidikanTerakhir;
        Pengguna.TanggalBekerja = TanggalBekerja;
        Pengguna.Username = Username;

        //PASSWORD
        if (!string.IsNullOrWhiteSpace(Password))
            Pengguna.Password = Password;

        Pengguna.PIN = PIN;
        Pengguna.Catatan = Catatan;

        Pengguna.EkstensiFoto = "";
        Pengguna.RFID = "";
        Pengguna.SidikJari = "";
        Pengguna.GajiPokok = 0;
        Pengguna.JaminanHariTua = 0;
        Pengguna.JaminanKecelakaan = 0;
        Pengguna.PPH21 = 0;
        Pengguna.TunjanganHariRaya = 0;
        Pengguna.TunjanganMakan = 0;
        Pengguna.TunjanganTransportasi = 0;

        Pengguna._IsActive = _IsActive;

        NotifikasiLog(EnumInsertUpdate.Update, Pengguna);

        return Pengguna;
    }
    #endregion

    #region CARI
    public TBPengguna Cari(int IDPengguna)
    {
        return db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == IDPengguna);
    }

    public TBPengguna CariUsername(string Username)
    {
        return db.TBPenggunas.FirstOrDefault(item => item.Username == Username);
    }
    #endregion

    public TBPengguna[] Data()
    {
        return db.TBPenggunas.OrderBy(item => item.NamaLengkap).ToArray();
    }

    public ListItem[] DropDownList(bool isLaporan = false)
    {
        List<ListItem> Pengguna = new List<ListItem>();

        if (isLaporan)
            Pengguna.Add(new ListItem { Value = "0", Text = "- Semua -" });

        Pengguna.AddRange(Data().Select(item => new ListItem
        {
            Value = item.IDPengguna.ToString(),
            Text = item.NamaLengkap
        }));

        return Pengguna.ToArray();
    }
}