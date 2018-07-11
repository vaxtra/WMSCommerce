using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Produk_Class : BaseWMSClass
{
    #region DEFAULT
    public Produk_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Produk_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public Produk_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }
    #endregion

    private void NotifikasiLog(EnumInsertUpdate enumInsertUpdate, TBProduk Produk)
    {
        if (enumInsertUpdate == EnumInsertUpdate.Insert)
        {
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Tambah Produk " + Produk.Nama + " berhasil");

            Produk._IDWMSStore = this.Pengguna.IDWMSStore;
            Produk._IDWMS = Guid.NewGuid();

            Produk._Urutan = db.TBProduks.Count() + 1;

            Produk._TanggalInsert = DateTime.Now;
            Produk._IDTempatInsert = this.Pengguna.IDTempat;
            Produk._IDPenggunaInsert = this.Pengguna.IDPengguna;

            Produk._IsActive = true;
        }
        else if (enumInsertUpdate == EnumInsertUpdate.Update)
            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Ubah Produk " + Produk.Nama + " berhasil");

        Produk._TanggalUpdate = DateTime.Now;
        Produk._IDTempatUpdate = this.Pengguna.IDTempat;
        Produk._IDPenggunaUpdate = this.Pengguna.IDPengguna;
    }

    #region TAMBAH
    public TBProduk Tambah(TBWarna Warna, TBPemilikProduk PemilikProduk, TBProdukKategori ProdukKategori, string KodeProduk, string Nama, string Deskripsi)
    {
        TBProduk Produk = new TBProduk
        {
            //IDWarna
            TBWarna = Warna,

            //IDPemilikProduk
            TBPemilikProduk = PemilikProduk,

            //IDProdukKategori
            TBProdukKategori = ProdukKategori,

            KodeProduk = KodeProduk,
            Nama = Nama,
            Deskripsi = Deskripsi,
            DeskripsiSingkat = Pengaturan.Ringkasan(Deskripsi),
            Dilihat = 0
        };

        NotifikasiLog(EnumInsertUpdate.Insert, Produk);

        db.TBProduks.InsertOnSubmit(Produk);

        return Produk;
    }

    public TBProduk Tambah(string produkKategori, string warna, string pemilikProduk, string Nama)
    {
        Warna_Class ClassWarna = new Warna_Class(db);
        PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
        ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

        //PRODUK KATEGORI
        string[] _tempKategori = produkKategori.Replace(" ,", ",").Replace(", ", ",").Split(',');

        var ProdukKategori = ClassProdukKategori.CariTambah(_tempKategori[0]);

        //WARNA
        var Warna = ClassWarna.CariTambah(warna);

        //PEMILIK PRODUK
        var PemilikProduk = ClassPemilikProduk.CariTambah(pemilikProduk);

        return Tambah(
            Warna: Warna,
            PemilikProduk: PemilikProduk,
            ProdukKategori: ProdukKategori,
            KodeProduk: "",
            Nama: Nama,
            Deskripsi: ""
            );
    }

    public TBProduk Tambah(string produkKategori, string warna, string pemilikProduk, string KodeProduk, string Nama, string Deskripsi)
    {
        Warna_Class ClassWarna = new Warna_Class(db);
        PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
        ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

        //PRODUK KATEGORI
        var ProdukKategori = ClassProdukKategori.CariTambah(produkKategori);

        //WARNA
        var Warna = ClassWarna.CariTambah(warna);

        //PEMILIK PRODUK
        var PemilikProduk = ClassPemilikProduk.CariTambah(pemilikProduk);

        return Tambah(
            Warna: Warna,
            PemilikProduk: PemilikProduk,
            ProdukKategori: ProdukKategori,
            KodeProduk: KodeProduk,
            Nama: Nama,
            Deskripsi: Deskripsi
            );
    }
    #endregion

    #region UBAH
    public TBProduk Ubah(int IDProduk, string warna, string pemilikProduk, string produkKategori, string KodeProduk, string Nama, string Deskripsi)
    {
        var Produk = Cari(IDProduk);

        if (Produk != null)
        {
            Warna_Class ClassWarna = new Warna_Class(db);
            PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
            ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

            //WARNA
            var Warna = ClassWarna.CariTambah(warna);

            //PEMILIK PRODUK
            var PemilikProduk = ClassPemilikProduk.CariTambah(pemilikProduk);

            //PRODUK KATEGORI
            var ProdukKategori = ClassProdukKategori.CariTambah(produkKategori);

            //IDWarna
            Produk.TBWarna = Warna;

            //IDPemilikProduk
            Produk.TBPemilikProduk = PemilikProduk;

            //IDProdukKategori
            Produk.TBProdukKategori = ProdukKategori;

            //Urutan

            Produk.KodeProduk = KodeProduk;  
            Produk.Deskripsi = Deskripsi;
            Produk.DeskripsiSingkat = Pengaturan.Ringkasan(Deskripsi);

            //Dilihat

            //_IsActive

            #region UBAH NAMA KOMBINASI PRODUK
            if (Produk.Nama != Nama)
            {
                //JIKA MERUBAH NAMA PRODUK MAKA NAMA KOMBINASI PRODUK BERUBAH
                foreach (var item in Produk.TBKombinasiProduks.ToArray())
                {
                    string atribut = "";

                    if (!string.IsNullOrWhiteSpace(item.TBAtributProduk.Nama))
                        atribut = " (" + item.TBAtributProduk.Nama + ")";

                    item.Nama = Nama + atribut;
                }
            }
            #endregion

            Produk.Nama = Nama;

            NotifikasiLog(EnumInsertUpdate.Update, Produk);

            return Produk;
        }
        else
            return null;
    }

    public TBProduk Ubah(TBProduk Produk, string warna, string pemilikProduk)
    {
        Warna_Class ClassWarna = new Warna_Class(db);
        PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

        Produk.TBWarna = ClassWarna.CariTambah(warna);
        Produk.TBPemilikProduk = ClassPemilikProduk.CariTambah(pemilikProduk);

        //_IsActive

        NotifikasiLog(EnumInsertUpdate.Update, Produk);

        return Produk;
    }
    #endregion

    #region CARI
    public TBProduk Cari(string Nama)
    {
        return db.TBProduks.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }

    public TBProduk Cari(int IDProduk)
    {
        return db.TBProduks.FirstOrDefault(item => item.IDProduk == IDProduk);
    }
    #endregion

    public void UbahStatus(int IDProduk)
    {
        var Produk = Cari(IDProduk);

        if (Produk != null)
            Produk._IsActive = !Produk._IsActive;

        Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses ubah data Produk berhasil");
    }
}