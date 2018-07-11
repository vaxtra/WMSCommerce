using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DiscountProdukKategori_Class : BaseWMSClass
{
    #region DEFAULT
    public DiscountProdukKategori_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public DiscountProdukKategori_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public DiscountProdukKategori_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBDiscountProdukKategori DiscountProdukKategori)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            DiscountProdukKategori._IDWMSStore = this.Pengguna.IDWMSStore;
            DiscountProdukKategori._IDWMS = Guid.NewGuid();

            DiscountProdukKategori._Urutan = db.TBPelanggans.Count() + 1;

            DiscountProdukKategori._TanggalInsert = DateTime.Now;
            DiscountProdukKategori._IDTempatInsert = this.Pengguna.IDTempat;
            DiscountProdukKategori._IDPenggunaInsert = this.Pengguna.IDPengguna;

            DiscountProdukKategori._IsActive = true;
        }

        DiscountProdukKategori._TanggalUpdate = DateTime.Now;
        DiscountProdukKategori._IDTempatUpdate = this.Pengguna.IDTempat;
        DiscountProdukKategori._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    public TBDiscountProdukKategori[] Data(int idGrupPelanggan)
    {
        return db.TBDiscountProdukKategoris.Where(item => item.IDGrupPelanggan == idGrupPelanggan).ToArray();
    }

    public void Hapus(int idGrupPelanggan)
    {
        db.TBDiscountProdukKategoris.DeleteAllOnSubmit(Data(idGrupPelanggan));
    }

    public TBDiscountProdukKategori Tambah(int IDGrupPelanggan, int IDProdukKategori, decimal Discount)
    {
        var DiscountProdukKategori = new TBDiscountProdukKategori
        {
            //IDDiscountGrupPelanggan
            IDGrupPelanggan = IDGrupPelanggan,
            IDProdukKategori = IDProdukKategori,
            Discount = Discount
        };

        NotifikasiLog(EnumInsertUpdate.Insert, DiscountProdukKategori);

        db.TBDiscountProdukKategoris.InsertOnSubmit(DiscountProdukKategori);

        return DiscountProdukKategori;
    }
}