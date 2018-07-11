using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class ProdukKategori_Class : BaseWMSClass
{
    #region DEFAULT
    public ProdukKategori_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public ProdukKategori_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    public ProdukKategori_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
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

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBProdukKategori ProdukKategori)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Tambah Kategori Produk " + ProdukKategori.Nama + " berhasil");

            ProdukKategori._IDWMSStore = this.Pengguna.IDWMSStore;
            ProdukKategori._IDWMS = Guid.NewGuid();

            ProdukKategori._Urutan = db.TBProdukKategoris.Count() + 1;

            ProdukKategori._TanggalInsert = DateTime.Now;
            ProdukKategori._IDTempatInsert = this.Pengguna.IDTempat;
            ProdukKategori._IDPenggunaInsert = this.Pengguna.IDPengguna;

            ProdukKategori._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Kategori Produk " + ProdukKategori.Nama + " berhasil");

        ProdukKategori._TanggalUpdate = DateTime.Now;
        ProdukKategori._IDTempatUpdate = this.Pengguna.IDTempat;
        ProdukKategori._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBProdukKategori Tambah(int IDProdukKategoriParent, string Nama, string Deskripsi)
    {
        TBProdukKategori ProdukKategori = new TBProdukKategori
        {
            //IDProdukKategori
            //IDProdukKategoriParent
            Nama = Nama,
            Deskripsi = Deskripsi
        };

        if (IDProdukKategoriParent > 0)
            ProdukKategori.IDProdukKategoriParent = IDProdukKategoriParent;

        NotifikasiLog(EnumInsertUpdate.Insert, ProdukKategori);

        db.TBProdukKategoris.InsertOnSubmit(ProdukKategori);

        return ProdukKategori;
    }

    public TBProdukKategori CariTambah(string Nama)
    {
        var Data = Cari(Nama);

        if (Data == null)
            return Tambah(0, Nama, "");
        else
            return Data;
    }
    #endregion

    #region UBAH
    public TBProdukKategori Ubah(int IDProdukKategori, int IDProdukKategoriParent, string Nama, string Deskripsi, bool IsActive)
    {
        var ProdukKategori = Cari(IDProdukKategori);

        if (ProdukKategori != null)
        {
            //IDProdukKategori
            //IDProdukKategoriParent
            ProdukKategori.Nama = Nama;
            ProdukKategori.Deskripsi = Deskripsi;

            if (IDProdukKategoriParent > 0)
                ProdukKategori.IDProdukKategoriParent = IDProdukKategoriParent;
            else
                ProdukKategori.IDProdukKategoriParent = null;

            ProdukKategori._IsActive = IsActive;

            NotifikasiLog(EnumInsertUpdate.Update, ProdukKategori);

            return ProdukKategori;
        }
        else
            return null;
    }
    #endregion

    #region CARI
    public TBProdukKategori Cari(int IDProdukKategori)
    {
        return db.TBProdukKategoris.FirstOrDefault(item => item.IDProdukKategori == IDProdukKategori);
    }

    public TBProdukKategori Cari(string Nama)
    {
        return db.TBProdukKategoris.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }
    #endregion

    public TBProdukKategori[] Data()
    {
        return db.TBProdukKategoris.OrderBy(item => item.Nama).ToArray();
    }

    public void DropDownList(DropDownList DropDownListInput)
    {
        DropDownListInput.DataSource = Data();
        DropDownListInput.DataValueField = "IDProdukKategori";
        DropDownListInput.DataTextField = "Nama";
        DropDownListInput.DataBind();
    }

    public void Hapus(int IDProdukKategori)
    {
        var ProdukKategori = Cari(IDProdukKategori);

        try
        {
            if (ProdukKategori != null)
            {
                db.TBProdukKategoris.DeleteOnSubmit(ProdukKategori);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + ProdukKategori.Nama + " berhasil");

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
}