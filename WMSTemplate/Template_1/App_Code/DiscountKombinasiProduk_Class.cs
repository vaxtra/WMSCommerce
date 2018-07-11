using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DiscountKombinasiProduk_Class : BaseWMSClass
{
    #region DEFAULT
    public DiscountKombinasiProduk_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public DiscountKombinasiProduk_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public DiscountKombinasiProduk_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBDiscountKombinasiProduk DiscountKombinasiProduk)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            DiscountKombinasiProduk._IDWMSStore = this.Pengguna.IDWMSStore;
            DiscountKombinasiProduk._IDWMS = Guid.NewGuid();

            DiscountKombinasiProduk._Urutan = db.TBPelanggans.Count() + 1;

            DiscountKombinasiProduk._TanggalInsert = DateTime.Now;
            DiscountKombinasiProduk._IDTempatInsert = this.Pengguna.IDTempat;
            DiscountKombinasiProduk._IDPenggunaInsert = this.Pengguna.IDPengguna;

            DiscountKombinasiProduk._IsActive = true;
        }

        DiscountKombinasiProduk._TanggalUpdate = DateTime.Now;
        DiscountKombinasiProduk._IDTempatUpdate = this.Pengguna.IDTempat;
        DiscountKombinasiProduk._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    public TBDiscountKombinasiProduk[] Data(int idGrupPelanggan)
    {
        return db.TBDiscountKombinasiProduks.Where(item => item.IDGrupPelanggan == idGrupPelanggan).ToArray();
    }

    public void Hapus(int idGrupPelanggan)
    {
        db.TBDiscountKombinasiProduks.DeleteAllOnSubmit(Data(idGrupPelanggan));
    }

    public TBDiscountKombinasiProduk Tambah(int IDGrupPelanggan, int IDKombinasiProduk, decimal Discount)
    {
        var DiscountKombinasiProduk = new TBDiscountKombinasiProduk
        {
            //IDDiscountGrupPelanggan
            IDGrupPelanggan = IDGrupPelanggan,
            IDKombinasiProduk = IDKombinasiProduk,
            Discount = Discount
        };

        NotifikasiLog(EnumInsertUpdate.Insert, DiscountKombinasiProduk);

        db.TBDiscountKombinasiProduks.InsertOnSubmit(DiscountKombinasiProduk);

        return DiscountKombinasiProduk;
    }
}