using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class KombinasiProduk_Class
{
    #region CARI
    public TBKombinasiProduk Cari(DataClassesDatabaseDataContext db, string nama)
    {
        return db.TBKombinasiProduks.FirstOrDefault(item => item.Nama.ToLower() == nama.ToLower());
    }
    public TBKombinasiProduk Cari(DataClassesDatabaseDataContext db, int idKombinasiProduk)
    {
        return db.TBKombinasiProduks.FirstOrDefault(item => item.IDKombinasiProduk == idKombinasiProduk);
    }
    public TBKombinasiProduk CariKode(DataClassesDatabaseDataContext db, string kode)
    {
        return db.TBKombinasiProduks.FirstOrDefault(item => item.KodeKombinasiProduk == kode);
    }
    #endregion

    #region TAMBAH
    public TBKombinasiProduk Tambah(DataClassesDatabaseDataContext db, TBProduk Produk, TBAtributProduk AtributProduk, DateTime tanggalDaftar, DateTime tanggalUpdate, string kodeKombinasiProduk, decimal berat, string deskripsi)
    {
        var KombinasiProduk = new TBKombinasiProduk
        {
            //IDKombinasiProduk
            TBProduk = Produk,
            TBAtributProduk = AtributProduk,
            //IDAtributProduk1 =,
            //IDAtributProduk2 =,
            //IDAtributProduk3 =,
            IDWMS = Guid.NewGuid(),
            KodeKombinasiProduk = kodeKombinasiProduk,
            Nama = NamaKombinasiProduk(Produk.Nama, AtributProduk.Nama),
            Berat = berat,
            Deskripsi = deskripsi,
            TanggalDaftar = tanggalDaftar,
            TanggalUpdate = tanggalUpdate,
            Urutan = db.TBKombinasiProduks.Count() + 1,
        };

        db.TBKombinasiProduks.InsertOnSubmit(KombinasiProduk);

        return KombinasiProduk;
    }

    public TBKombinasiProduk Tambah(DataClassesDatabaseDataContext db, TBProduk Produk, string AtributProdukGrup, string AtributProduk, string kodeKombinasiProduk, decimal berat, string deskripsi)
    {
        AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

        var Atribut = ClassAtributProduk.CariTambah(AtributProdukGrup, AtributProduk);

        return Tambah(db, Produk, Atribut, DateTime.Now, DateTime.Now, kodeKombinasiProduk, berat, deskripsi);
    }

    public TBKombinasiProduk Tambah(DataClassesDatabaseDataContext db, TBProduk Produk, string AtributProdukGrup1, string AtributProduk1, string AtributProdukGrup2, string AtributProduk2, string AtributProdukGrup3, string AtributProduk3)
    {

        AtributProdukGrup_Class ClassAtributProdukGrup = new AtributProdukGrup_Class(db);
        AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);



        //string[] _tempAtributProduk2 = AtributProduk2.Replace(" ,", ",").Replace(", ", ",").Split(',');

        //foreach (var item in _tempAtributProduk2.Distinct())
        //{
        //    ClassAtributProduk.CariTambah(AtributProdukGrup2, item);
        //}

        //string[] _tempAtributProduk3 = AtributProduk3.Replace(" ,", ",").Replace(", ", ",").Split(',');

        //foreach (var item in _tempAtributProduk3.Distinct())
        //{
        //    ClassAtributProduk.CariTambah(AtributProdukGrup3, item);
        //}



        if (!string.IsNullOrWhiteSpace(AtributProdukGrup1))
        {
            var Grup1 = ClassAtributProdukGrup.Cari(AtributProdukGrup1);

            if (Grup1 == null)
                Grup1 = ClassAtributProdukGrup.Tambah(AtributProdukGrup1);

            string[] _tempAtributProduk1 = AtributProduk1.Replace(" ,", ",").Replace(", ", ",").Split(',');

            foreach (var item in _tempAtributProduk1.Distinct())
            {
                ClassAtributProduk.Tambah(Grup1, item);
            }
        }

        //var KombinasiProduk = new TBKombinasiProduk
        //{
        //    //IDKombinasiProduk
        //    TBProduk = Produk,
        //    TBAtributProduk = AtributProduk,
        //    //IDAtributProduk1 =,
        //    //IDAtributProduk2 =,
        //    //IDAtributProduk3 =,
        //    IDWMS = Guid.NewGuid(),
        //    KodeKombinasiProduk = kodeKombinasiProduk,
        //    Nama = NamaKombinasiProduk(Produk.Nama, AtributProduk.Nama),
        //    Berat = berat,
        //    Deskripsi = deskripsi,
        //    TanggalDaftar = tanggalDaftar,
        //    TanggalUpdate = tanggalUpdate,
        //    Urutan = db.TBKombinasiProduks.Count() + 1,
        //};

        //db.TBKombinasiProduks.InsertOnSubmit(KombinasiProduk);

        //return Tambah(db, Produk, Atribut, DateTime.Now, DateTime.Now, "", 0, "");

        return null;
    }


    #endregion

    #region UBAH
    public TBKombinasiProduk Ubah(DataClassesDatabaseDataContext db, int idTempat,
        int idKombinasiProduk, string AtributProdukGrup, string AtributProduk, string kodeKombinasiProduk,
        decimal berat, string deskripsi)
    {
        var KombinasiProduk = Cari(db, idKombinasiProduk);

        if (KombinasiProduk != null)
        {
            AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

            var Atribut = ClassAtributProduk.CariTambah(AtributProdukGrup, AtributProduk);

            KombinasiProduk = Ubah(db, idTempat, KombinasiProduk, KombinasiProduk.TBProduk, Atribut, KombinasiProduk.TanggalDaftar.Value, KombinasiProduk.TanggalUpdate.Value, kodeKombinasiProduk, berat, deskripsi);
        }

        return KombinasiProduk;
    }

    public TBKombinasiProduk Ubah(DataClassesDatabaseDataContext db, int idTempat,
        TBKombinasiProduk KombinasiProduk, TBProduk Produk, string AtributProdukGrup, string AtributProduk,
        string kodeKombinasiProduk, decimal berat, string deskripsi)
    {
        AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

        var Atribut = ClassAtributProduk.CariTambah(AtributProdukGrup, AtributProduk);

        return Ubah(db, idTempat, KombinasiProduk, Produk, Atribut, KombinasiProduk.TanggalDaftar.Value, KombinasiProduk.TanggalUpdate.Value, kodeKombinasiProduk, berat, deskripsi);
    }

    /// <summary>
    /// Ubah Kombinasi Produk Normal
    /// </summary>
    public TBKombinasiProduk Ubah(DataClassesDatabaseDataContext db, int idTempat,
        TBKombinasiProduk KombinasiProduk, TBProduk Produk, TBAtributProduk AtributProduk,
        DateTime tanggalDaftar, DateTime tanggalUpdate, string kodeKombinasiProduk,
        decimal berat, string deskripsi)
    {
        //IDWMS
        KombinasiProduk.TBProduk = Produk;
        KombinasiProduk.TBAtributProduk = AtributProduk;
        //TBRakPenyimpanan
        KombinasiProduk.TanggalDaftar = tanggalDaftar;
        KombinasiProduk.TanggalUpdate = tanggalUpdate;
        //Urutan

        if (string.IsNullOrWhiteSpace(kodeKombinasiProduk))
            KombinasiProduk.KodeKombinasiProduk = Barcode(db, idTempat, KombinasiProduk);
        else
            KombinasiProduk.KodeKombinasiProduk = kodeKombinasiProduk;

        KombinasiProduk.Nama = NamaKombinasiProduk(Produk.Nama, AtributProduk.Nama);

        KombinasiProduk.Berat = berat;
        KombinasiProduk.Deskripsi = deskripsi;

        return KombinasiProduk;
    }
    #endregion

    public string NamaKombinasiProduk(string namaProduk, string namaAtributProduk)
    {
        if (!string.IsNullOrWhiteSpace(namaAtributProduk))
            return namaProduk + " (" + namaAtributProduk + ")";
        else
            return namaProduk;
    }
    public bool Hapus(DataClassesDatabaseDataContext db, int idKombinasiProduk)
    {
        var KombinasiProduk = Cari(db, idKombinasiProduk);

        if (KombinasiProduk != null)
        {
            if (
                KombinasiProduk.TBTransaksiDetails.Count == 0 &&
                KombinasiProduk.TBKomposisiKombinasiProduks.Count == 0 &&
                KombinasiProduk.TBRelasiJenisBiayaProduksiKombinasiProduks.Count == 0 &&
                KombinasiProduk.TBTransferProdukDetails.Count == 0
                )
            {
                db.TBFotoKombinasiProduks.DeleteAllOnSubmit(KombinasiProduk.TBFotoKombinasiProduks);

                foreach (var item in KombinasiProduk.TBStokProduks.ToArray())
                    db.TBPerpindahanStokProduks.DeleteAllOnSubmit(item.TBPerpindahanStokProduks);

                db.TBStokProduks.DeleteAllOnSubmit(KombinasiProduk.TBStokProduks);

                db.TBTransaksiPrints.DeleteAllOnSubmit(KombinasiProduk.TBTransaksiPrints);
                db.TBWholesales.DeleteAllOnSubmit(KombinasiProduk.TBWholesales);

                db.TBKombinasiProduks.DeleteOnSubmit(KombinasiProduk);

                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    public TBKombinasiProduk[] Data(DataClassesDatabaseDataContext db, int idProduk)
    {
        return db.TBKombinasiProduks.Where(item => item.IDProduk == idProduk).ToArray();
    }
    public List<TBKombinasiProduk> TambahList(DataClassesDatabaseDataContext db, int idTempat, int idPengguna, TBProduk Produk, string listAtributProduk)
    {
        string[] _tempAtributProduk = listAtributProduk.Replace(" ,", ",").Replace(", ", ",").Split(',');

        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

        List<TBKombinasiProduk> ListKombinasiProduk = new List<TBKombinasiProduk>();

        foreach (var item in _tempAtributProduk.Distinct())
        {
            var KombinasiProduk = Tambah(db, Produk, "", item, "", 0, "");

            var StokProduk = StokProduk_Class.MembuatStok(0, idTempat, idPengguna, KombinasiProduk, 0, 0, "");

            db.SubmitChanges();

            PengaturanBarcode(db, idTempat, KombinasiProduk);

            ListKombinasiProduk.Add(KombinasiProduk);
        }

        return ListKombinasiProduk;
    }

    #region BARCODE
    private string Barcode(DataClassesDatabaseDataContext db, int idTempat, TBKombinasiProduk KombinasiProduk)
    {
        int idKombinasiProduk = KombinasiProduk.IDKombinasiProduk;
        string kode = "";

        do
        {
            kode = DateTime.Now.ToString("yyMM") + idTempat.ToString() + idKombinasiProduk.ToString();
            idKombinasiProduk++;

        } while (CariKode(db, kode) != null);

        return kode;
    }
    public void PengaturanBarcode(DataClassesDatabaseDataContext db, int idTempat, TBKombinasiProduk KombinasiProduk)
    {
        string kode = Barcode(db, idTempat, KombinasiProduk);

        KombinasiProduk.KodeKombinasiProduk = kode;

        db.SubmitChanges();
    }
    #endregion
}