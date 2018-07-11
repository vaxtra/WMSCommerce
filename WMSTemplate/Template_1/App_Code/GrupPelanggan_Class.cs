using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class GrupPelanggan_Class : BaseWMSClass
{
    #region DEFAULT
    public GrupPelanggan_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public GrupPelanggan_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public GrupPelanggan_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBGrupPelanggan GrupPelanggan)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Grup Pelanggan " + GrupPelanggan.Nama + " berhasil");

            GrupPelanggan._IDWMSStore = this.Pengguna.IDWMSStore;
            GrupPelanggan._IDWMS = Guid.NewGuid();

            GrupPelanggan._Urutan = db.TBGrupPelanggans.Count() + 1;

            GrupPelanggan._TanggalInsert = DateTime.Now;
            GrupPelanggan._IDTempatInsert = this.Pengguna.IDTempat;
            GrupPelanggan._IDPenggunaInsert = this.Pengguna.IDPengguna;

            GrupPelanggan._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Grup Pelanggan " + GrupPelanggan.Nama + " berhasil");

        GrupPelanggan._TanggalUpdate = DateTime.Now;
        GrupPelanggan._IDTempatUpdate = this.Pengguna.IDTempat;
        GrupPelanggan._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region CARI
    public TBGrupPelanggan Cari(Guid IDWMS)
    {
        return db.TBGrupPelanggans.FirstOrDefault(item => item._IDWMS == IDWMS);
    }
    #endregion

    public TBGrupPelanggan TambahUbah(Guid IDWMS, string Nama)
    {
        var GrupPelanggan = Cari(IDWMS);

        if (GrupPelanggan == null)
        {
            GrupPelanggan = new TBGrupPelanggan
            {
                //IDGrupPelanggan
                EnumBonusGrupPelanggan = (int)PilihanBonusGrupPelanggan.Deposit,
                LimitTransaksi = 0,
                Nama = Nama,
                Persentase = 0,
            };

            NotifikasiLog(EnumInsertUpdate.Insert, GrupPelanggan);

            //IDWMS SESUAI PARAMETER
            GrupPelanggan._IDWMS = IDWMS;

            db.TBGrupPelanggans.InsertOnSubmit(GrupPelanggan);
        }
        else
        {
            GrupPelanggan.Nama = Nama;

            NotifikasiLog(EnumInsertUpdate.Update, GrupPelanggan);
        }

        return GrupPelanggan;
    }

    public TBGrupPelanggan[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBGrupPelanggans.OrderBy(item => item.Nama).ToArray();
    }
    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db)
    {
        List<ListItem> PelangganGrup = new List<ListItem>();

        PelangganGrup.Add(new ListItem { Value = "0", Text = "- Semua -" });

        PelangganGrup.AddRange(Data(db).Select(item => new ListItem
        {
            Value = item.IDGrupPelanggan.ToString(),
            Text = item.Nama
        }));

        return PelangganGrup.ToArray();
    }
    public ListItem[] DataDropDownListNamaPotongan(DataClassesDatabaseDataContext db)
    {
        List<ListItem> PelangganGrup = new List<ListItem>();

        PelangganGrup.AddRange(Data(db).Select(item => new ListItem
        {
            Value = item.IDGrupPelanggan.ToString(),
            Text = item.Nama + " (" + item.Persentase.ToFormatHarga() + "%)"
        }));

        return PelangganGrup.ToArray();
    }

    public TBGrupPelanggan Cari(DataClassesDatabaseDataContext db, int idGrupPelanggan)
    {
        return db.TBGrupPelanggans.FirstOrDefault(item => item.IDGrupPelanggan == idGrupPelanggan);
    }

    public bool Hapus(int idGrupPelanggan)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var GrupPelanggan = Cari(db, idGrupPelanggan);

                if (GrupPelanggan != null)
                {
                    db.TBGrupPelanggans.DeleteOnSubmit(GrupPelanggan);
                    db.SubmitChanges();

                    return true;
                }
                else
                    return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}