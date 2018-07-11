using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class PenggunaLogin
{
    #region PROPERTY
    private int iDPengguna;
    public int IDPengguna
    {
        get { return iDPengguna; }
    }

    private int iDGrupPengguna;
    public int IDGrupPengguna
    {
        get { return iDGrupPengguna; }
    }

    private int iDTempat;
    public int IDTempat
    {
        get { return iDTempat; }
    }

    private string namaLengkap;
    public string NamaLengkap
    {
        get { return namaLengkap; }
    }

    private string username;
    public string Username
    {
        get { return username; }
    }

    private string tempat;
    public string Tempat
    {
        get { return tempat; }
    }

    private string store;
    public string Store
    {
        get { return store; }
    }

    private Guid idWMSStore;
    public Guid IDWMSStore
    {
        get { return idWMSStore; }
    }

    #region IDStore
    private int idStore;
    public int IDStore
    {
        get { return idStore; }
    }
    #endregion

    public string EnkripsiIDPengguna
    {
        get { return EncryptDecrypt.Encrypt(IDPengguna.ToString()); }
    }

    private TipePointOfSales pointOfSales;
    public TipePointOfSales PointOfSales
    {
        get { return pointOfSales; }
    }
    #endregion

    /// <summary>
    /// Login Berdasarkan Username dan Password
    /// </summary>
    public PenggunaLogin(string username, string password)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            var TipePointOfSales = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.TipePointOfSales);

            var Pengguna = db.TBPenggunas
                .FirstOrDefault(item =>
                    item.Username == username &&
                    item.Password == password &&
                    item._IsActive);

            if (Pengguna != null)
            {
                //menambah LogPengguna tipe Login : 1
                db.TBLogPenggunas.InsertOnSubmit(new TBLogPengguna
                {
                    IDLogPenggunaTipe = 1,
                    TBPengguna = Pengguna,
                    Tanggal = DateTime.Now
                });

                db.SubmitChanges();

                iDGrupPengguna = Pengguna.IDGrupPengguna;
                iDPengguna = Pengguna.IDPengguna;
                iDTempat = Pengguna.IDTempat;
                namaLengkap = Pengguna.NamaLengkap;
                this.username = Pengguna.Username;
                tempat = Pengguna.TBTempat.Nama;
                store = Pengguna.TBTempat.TBStore.Nama;
                idWMSStore = Pengguna._IDWMSStore.Value;
                idStore = Pengguna.TBTempat.IDStore;
                pointOfSales = (TipePointOfSales)TipePointOfSales.ToInt();
            }
        }
    }

    /// <summary>
    /// Login Berdasarkan PIN
    /// </summary>
    public PenggunaLogin(string pin)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            var TipePointOfSales = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.TipePointOfSales);

            var Pengguna = db.TBPenggunas
                .FirstOrDefault(item =>
                    item.PIN == pin &&
                    item._IsActive);

            if (Pengguna != null)
            {
                iDGrupPengguna = Pengguna.IDGrupPengguna;
                iDPengguna = Pengguna.IDPengguna;
                iDTempat = Pengguna.IDTempat;
                namaLengkap = Pengguna.NamaLengkap;
                username = Pengguna.Username;
                tempat = Pengguna.TBTempat.Nama;
                store = Pengguna.TBTempat.TBStore.Nama;
                idWMSStore = Pengguna._IDWMSStore.Value;
                idStore = Pengguna.TBTempat.IDStore;
                pointOfSales = (TipePointOfSales)TipePointOfSales.ToInt();
            }
        }
    }

    /// <summary>
    /// Login Berdasarkan IDPengguna dan status Enkripsi
    /// </summary>
    public PenggunaLogin(string idPengguna, bool enkripsi)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            var TipePointOfSales = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.TipePointOfSales);

            TBPengguna Pengguna;

            if (enkripsi)
            {
                Pengguna = db.TBPenggunas
                    .FirstOrDefault(item =>
                        item.IDPengguna == EncryptDecrypt.Decrypt(idPengguna).ToInt());
            }
            else
            {
                Pengguna = db.TBPenggunas
                    .FirstOrDefault(item =>
                        item.IDPengguna == idPengguna.ToInt());
            }

            if (Pengguna != null)
            {
                iDGrupPengguna = Pengguna.IDGrupPengguna;
                iDPengguna = Pengguna.IDPengguna;
                iDTempat = Pengguna.IDTempat;
                namaLengkap = Pengguna.NamaLengkap;
                username = Pengguna.Username;
                tempat = Pengguna.TBTempat.Nama;
                store = Pengguna.TBTempat.TBStore.Nama;
                idWMSStore = Pengguna._IDWMSStore.Value;
                idStore = Pengguna.TBTempat.IDStore;
                pointOfSales = (TipePointOfSales)TipePointOfSales.ToInt();
            }
        }
    }

    /// <summary>
    /// Login Berdasarkan IDPengguna dan IDTempat
    /// </summary>
    public PenggunaLogin(int idPengguna, int idTempat)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            var TipePointOfSales = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.TipePointOfSales);

            var Pengguna = db.TBPenggunas
                .FirstOrDefault(item => item.IDPengguna == idPengguna);

            var Tempat = db.TBTempats
                .FirstOrDefault(item => item.IDTempat == idTempat);

            if (Pengguna != null)
            {
                iDGrupPengguna = Pengguna.IDGrupPengguna;
                iDPengguna = Pengguna.IDPengguna;
                iDTempat = Tempat.IDTempat;
                namaLengkap = Pengguna.NamaLengkap;
                username = Pengguna.Username;
                tempat = Tempat.Nama;
                store = Tempat.TBStore.Nama;
                idWMSStore = Pengguna._IDWMSStore.Value;
                idStore = Pengguna.TBTempat.IDStore;
                pointOfSales = (TipePointOfSales)TipePointOfSales.ToInt();
            }
        }
    }
}