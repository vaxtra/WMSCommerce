using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Pelanggan_Class : BaseWMSClass
{
    #region DEFAULT
    public Pelanggan_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Pelanggan_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public Pelanggan_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBPelanggan Pelanggan)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Pelanggan " + Pelanggan.NamaLengkap + " berhasil");

            Pelanggan._IDWMSStore = this.Pengguna.IDWMSStore;
            Pelanggan._IDWMS = Guid.NewGuid();

            Pelanggan._Urutan = db.TBPelanggans.Count() + 1;

            Pelanggan._TanggalInsert = DateTime.Now;
            Pelanggan._IDTempatInsert = this.Pengguna.IDTempat;
            Pelanggan._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Pelanggan._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Pelanggan " + Pelanggan.NamaLengkap + " berhasil");

        Pelanggan._TanggalUpdate = DateTime.Now;
        Pelanggan._IDTempatUpdate = this.Pengguna.IDTempat;
        Pelanggan._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBPelanggan Tambah(int IDGrupPelanggan, int IDPenggunaPIC, string NamaLengkap, string Username, string Password, string Email, string Handphone, string TeleponLain, DateTime TanggalLahir, decimal Deposit, string Catatan, bool _IsActive)
    {
        TBPelanggan Pelanggan = new TBPelanggan
        {
            IDGrupPelanggan = IDGrupPelanggan,
            IDPenggunaPIC = IDPenggunaPIC,
            NamaLengkap = NamaLengkap,
            Username = Username,
            Password = Password,
            Email = Email,
            Handphone = Handphone,
            TeleponLain = TeleponLain,
            TanggalLahir = TanggalLahir,
            TanggalDaftar = DateTime.Now,
            Deposit = Deposit,
            Catatan = Catatan,

            _IsActive = _IsActive
        };

        NotifikasiLog(EnumInsertUpdate.Insert, Pelanggan);

        db.TBPelanggans.InsertOnSubmit(Pelanggan);

        return Pelanggan;
    }
    #endregion

    #region UBAH
    public TBPelanggan Ubah(int IDPelanggan, int IDGrupPelanggan, int IDPenggunaPIC, string NamaLengkap, string Username, string Password, string Email, string Handphone, string TeleponLain, DateTime TanggalLahir, decimal Deposit, string Catatan, bool _IsActive)
    {
        var Pelanggan = Cari(IDPelanggan);

        if (Pelanggan != null)
        {
            Pelanggan.IDGrupPelanggan = IDGrupPelanggan;
            Pelanggan.IDPenggunaPIC = IDPenggunaPIC;
            Pelanggan.NamaLengkap = NamaLengkap;
            Pelanggan.Username = Username;
            Pelanggan.Password = Password;
            Pelanggan.Email = Email;
            Pelanggan.Handphone = Handphone;
            Pelanggan.TeleponLain = TeleponLain;
            Pelanggan.TanggalLahir = TanggalLahir;
            Pelanggan.TanggalDaftar = DateTime.Now;
            Pelanggan.Deposit = Deposit;
            Pelanggan.Catatan = Catatan;

            Pelanggan._IsActive = _IsActive;

            NotifikasiLog(EnumInsertUpdate.Update, Pelanggan);

            return Pelanggan;
        }
        else
            return null;
    }

    public TBPelanggan Ubah(int IDPelanggan, int IDGrupPelanggan, string NamaLengkap, string Handphone)
    {
        var Pelanggan = Cari(IDPelanggan);

        if (Pelanggan != null)
        {
            Pelanggan.IDGrupPelanggan = IDGrupPelanggan;
            Pelanggan.NamaLengkap = NamaLengkap;
            Pelanggan.Handphone = Handphone;
            Pelanggan.TanggalDaftar = DateTime.Now;

            //_IsActive;

            NotifikasiLog(EnumInsertUpdate.Update, Pelanggan);

            return Pelanggan;
        }
        else
            return null;
    }

    public TBPelanggan Ubah(int IDPelanggan, string NamaLengkap, string Email, string Handphone)
    {
        var Pelanggan = Cari(IDPelanggan);

        if (Pelanggan != null)
        {
            Pelanggan.NamaLengkap = NamaLengkap;
            Pelanggan.Email = Email;
            Pelanggan.Handphone = Handphone;
            Pelanggan.TanggalDaftar = DateTime.Now;

            //_IsActive;

            NotifikasiLog(EnumInsertUpdate.Update, Pelanggan);

            return Pelanggan;
        }
        else
            return null;
    }

    public TBPelanggan Ubah(int IDPelanggan, int IDGrupPelanggan, string NamaLengkap, string Email, string Handphone)
    {
        var Pelanggan = Cari(IDPelanggan);

        if (Pelanggan != null)
        {
            Pelanggan.IDGrupPelanggan = IDGrupPelanggan;
            Pelanggan.NamaLengkap = NamaLengkap;
            Pelanggan.Email = Email;
            Pelanggan.Handphone = Handphone;
            Pelanggan.TanggalDaftar = DateTime.Now;

            //_IsActive;

            NotifikasiLog(EnumInsertUpdate.Update, Pelanggan);

            return Pelanggan;
        }
        else
            return null;
    }
    #endregion

    #region CARI
    public TBPelanggan Cari(int IDPelanggan)
    {
        return db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == IDPelanggan);
    }

    public TBPelanggan Cari(string Email, string Handphone)
    {
        return db.TBPelanggans.FirstOrDefault(item => item.Email.ToLower() == Email.ToLower() || item.Handphone == Pengaturan.InputHandphone(Handphone));
    }

    public TBPelanggan Cari(Guid IDWMS)
    {
        return db.TBPelanggans.FirstOrDefault(item => item._IDWMS == IDWMS);
    }
    #endregion

    public TBPelanggan TambahUbah(Guid IDWMS, Guid IDWMSGrupPelanggan, string NamaLengkap, string Username, string Password, string Email, DateTime TanggalLahir, string Handphone)
    {
        var Pelanggan = Cari(IDWMS);

        GrupPelanggan_Class ClassGrupPelanggan = new GrupPelanggan_Class(db, Pengguna);

        var GrupPelanggan = ClassGrupPelanggan.Cari(IDWMSGrupPelanggan);

        if (GrupPelanggan == null)
            ErrorMessage = "Grup Pelanggan tidak ditemukan";

        if (Pelanggan == null)
        {
            Pelanggan = new TBPelanggan
            {
                //IDPelanggan
                TBGrupPelanggan = GrupPelanggan,
                NamaLengkap = NamaLengkap,
                Username = Username,
                Password = Password,
                Email = Email,
                TanggalLahir = TanggalLahir,
                Handphone = Handphone,

                //DEFAULT
                IDPenggunaPIC = 1,
                Deposit = 0,
                TeleponLain = "",
                TanggalDaftar = DateTime.Now,
                Catatan = ""
            };

            NotifikasiLog(EnumInsertUpdate.Insert, Pelanggan);

            //IDWMS SESUAI PARAMETER
            Pelanggan._IDWMS = IDWMS;

            db.TBPelanggans.InsertOnSubmit(Pelanggan);
        }
        else
        {
            Pelanggan.TBGrupPelanggan = GrupPelanggan;
            Pelanggan.NamaLengkap = NamaLengkap;
            Pelanggan.Username = Username;
            Pelanggan.Password = Password;
            Pelanggan.Email = Email;
            Pelanggan.TanggalLahir = TanggalLahir;
            Pelanggan.Handphone = Handphone;

            NotifikasiLog(EnumInsertUpdate.Update, Pelanggan);
        }

        return Pelanggan;
    }

    public void UbahStatus(int IDPelanggan)
    {
        var Pelanggan = Cari(IDPelanggan);

        try
        {
            if (Pelanggan != null && Pelanggan.IDPelanggan != (int)EnumPelanggan.GeneralCustomer)
            {
                Pelanggan._IsActive = !Pelanggan._IsActive;

                NotifikasiLog(EnumInsertUpdate.Update, Pelanggan);

                db.SubmitChanges();
            }
            else
            {
                Notifikasi(EnumAlert.danger, Pengguna.IDPengguna, "Proses ubah data gagal");

                db.SubmitChanges();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Hapus(int idPelanggan)
    {
        var Pelanggan = Cari(idPelanggan);

        try
        {
            if (Pelanggan != null && Pelanggan.IDPelanggan != (int)EnumPelanggan.GeneralCustomer)
            {
                db.TBAlamats.DeleteAllOnSubmit(db.TBAlamats.Where(item => item.IDPelanggan == idPelanggan));
                db.TBPelanggans.DeleteOnSubmit(Pelanggan);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data berhasil");

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

    public decimal Deposit(int IDPelanggan)
    {
        //MENGEMBALIKAN SISA DEPOSIT
        if (IDPelanggan > (int)EnumPelanggan.GeneralCustomer)
        {
            var Pelanggan = Cari(IDPelanggan);

            if (Pelanggan.Deposit > 0)
            {
                var PenggunaanDeposit = db.TBTransaksiJenisPembayarans
                    .Where(item =>
                        item.Tanggal.Value.Year == DateTime.Now.Year &&
                        item.Tanggal.Value.Month == DateTime.Now.Month &&
                        item.TBTransaksi.IDPelanggan == Pelanggan.IDPelanggan &&
                        item.IDJenisPembayaran == (int)EnumJenisPembayaran.Deposit &&
                        item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                    .Sum(item => item.Total);

                if (PenggunaanDeposit.HasValue)
                    return Pelanggan.Deposit.Value - PenggunaanDeposit.Value;
                else
                    return Pelanggan.Deposit.Value;
            }
            else
                return 0; //PELANGGAN TIDAK MEMILIKI DEPOSIT
        }
        else
            return 0; //GENERAL CUSTOMER TIDAK MEMILIKI DEPOSIT
    }

    public void DropDownList(DropDownList dropDownList, bool isLaporan = false)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDPelanggan";
        dropDownList.DataTextField = "NamaLengkap";
        dropDownList.DataBind();

        if (isLaporan)
            dropDownList.Items.Insert(0, new ListItem { Value = "0", Text = " - Semua Pelanggan -" });
    }

    public TBPelanggan[] Data()
    {
        return db.TBPelanggans.OrderBy(item => item.NamaLengkap).ToArray();
    }

    public ListItem[] DataDropDownListNamaHandphone()
    {
        List<ListItem> Pelanggan = new List<ListItem>();

        Pelanggan.Add(new ListItem { Value = "0", Text = "- Tambah Baru -" });

        Pelanggan.AddRange(Data().Select(item => new ListItem
        {
            Value = item.IDPelanggan.ToString(),
            Text = item.NamaLengkap + " (" + item.Handphone + ")"
        }));

        return Pelanggan.ToArray();
    }
}