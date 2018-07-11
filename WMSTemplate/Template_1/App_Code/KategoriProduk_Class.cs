using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class KategoriProduk_Class
{
    public TBKategoriProduk Cari(DataClassesDatabaseDataContext db, string nama)
    {
        return db.TBKategoriProduks.FirstOrDefault(item => item.Nama.ToLower() == nama.ToLower());
    }
    public TBKategoriProduk CariTambah(DataClassesDatabaseDataContext db, string nama)
    {
        var KategoriProduk = Cari(db, nama);

        if (KategoriProduk == null)
            KategoriProduk = Tambah(db, nama);

        return KategoriProduk;
    }
    public TBKategoriProduk Tambah(DataClassesDatabaseDataContext db, string nama)
    {
        return Tambah(db, nama, "");
    }
    public TBKategoriProduk Tambah(DataClassesDatabaseDataContext db, string nama, string deskripsi)
    {
        var KategoriProduk = new TBKategoriProduk
        {
            Nama = nama,
            Deskripsi = deskripsi
        };

        db.TBKategoriProduks.InsertOnSubmit(KategoriProduk);

        return KategoriProduk;
    }
    public TBKategoriProduk[] Data()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return Data(db);
        }
    }
    public TBKategoriProduk[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBKategoriProduks.OrderBy(item => item.Nama).ToArray();
    }
    public string Data(TBRelasiProdukKategoriProduk[] RelasiKategoriProduk)
    {
        string result = "";

        foreach (var item in RelasiKategoriProduk)
            result += item.TBKategoriProduk.Nama + ",";

        if (!string.IsNullOrWhiteSpace(result))
            result = result.Substring(0, result.Length - 1);

        return result;
    }
    public void KategoriProduk(DataClassesDatabaseDataContext db, TBProduk Produk, string listKategori)
    {
        //HAPUS JIKA SUDAH ADA RELASI PRODUK DENGAN KATEGORI
        if (Produk.TBRelasiProdukKategoriProduks.Count > 0)
            db.TBRelasiProdukKategoriProduks.DeleteAllOnSubmit(Produk.TBRelasiProdukKategoriProduks);

        string[] _tempKategori = listKategori.Replace(" ,", ",").Replace(", ", ",").Split(',');

        if (_tempKategori.Count() > 0)
        {
            foreach (var item in _tempKategori.Distinct())
            {
                var KategoriProduk = CariTambah(db, item);

                Produk.TBRelasiProdukKategoriProduks.Add(new TBRelasiProdukKategoriProduk
                {
                    TBKategoriProduk = KategoriProduk
                });
            }
        }
    }
    public ListItem[] Dropdownlist(DataClassesDatabaseDataContext db)
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua -"
        });

        ListData.AddRange(db.TBKategoriProduks.Select(item => new ListItem
        {
            Value = item.IDKategoriProduk.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }

    public static void DeleteKategoriProduk(DataClassesDatabaseDataContext db, int idKategoriProduk)
    {
        TBKategoriProduk kategoriProduk = db.TBKategoriProduks.FirstOrDefault(item => item.IDKategoriProduk == idKategoriProduk);

        if (kategoriProduk.TBKategoriProduks.Count == 0 &&
            kategoriProduk.TBRekomendasiKategoriProduks.Count == 0 &&
            kategoriProduk.TBRelasiProdukKategoriProduks.Count == 0)
        {
            db.TBKategoriProduks.DeleteOnSubmit(kategoriProduk);
        }
    }
}