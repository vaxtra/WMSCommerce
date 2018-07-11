using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for KategoriBahanBaku_Class
/// </summary>
public class KategoriBahanBaku_Class
{
    public TBKategoriBahanBaku Cari(DataClassesDatabaseDataContext db, string nama)
    {
        return db.TBKategoriBahanBakus.FirstOrDefault(item => item.Nama.ToLower() == nama.ToLower());
    }
    public TBKategoriBahanBaku CariTambah(DataClassesDatabaseDataContext db, string nama)
    {
        var KategoriBahanBaku = Cari(db, nama);

        if (KategoriBahanBaku == null)
            KategoriBahanBaku = Tambah(db, nama);

        return KategoriBahanBaku;
    }
    public TBKategoriBahanBaku Tambah(DataClassesDatabaseDataContext db, string nama)
    {
        return Tambah(db, nama, "");
    }
    public TBKategoriBahanBaku Tambah(DataClassesDatabaseDataContext db, string nama, string deskripsi)
    {
        var KategoriBahanBaku = new TBKategoriBahanBaku
        {
            Nama = nama,
            Deskripsi = deskripsi
        };

        db.TBKategoriBahanBakus.InsertOnSubmit(KategoriBahanBaku);

        return KategoriBahanBaku;
    }
    public TBKategoriBahanBaku[] Data()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return Data(db);
        }
    }
    public TBKategoriBahanBaku[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBKategoriBahanBakus.OrderBy(item => item.Nama).ToArray();
    }
    public string Data(TBRelasiBahanBakuKategoriBahanBaku[] RelasiKategoriBahanBaku)
    {
        string result = "";

        foreach (var item in RelasiKategoriBahanBaku)
            result += item.TBKategoriBahanBaku.Nama + ",";

        if (!string.IsNullOrWhiteSpace(result))
            result = result.Substring(0, result.Length - 1);

        return result;
    }
    public void KategoriBahanBaku(DataClassesDatabaseDataContext db, TBBahanBaku BahanBaku, string listKategori)
    {
        //HAPUS JIKA SUDAH ADA RELASI BAHAN BAKU DENGAN KATEGORI
        if (BahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0)
            db.TBRelasiBahanBakuKategoriBahanBakus.DeleteAllOnSubmit(BahanBaku.TBRelasiBahanBakuKategoriBahanBakus);

        string[] _tempKategori = listKategori.Replace(" ,", ",").Replace(", ", ",").Split(',');

        foreach (var item in _tempKategori.Distinct())
        {
            var KategoriBahanBaku = CariTambah(db, item);

            BahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Add(new TBRelasiBahanBakuKategoriBahanBaku
            {
                TBKategoriBahanBaku = KategoriBahanBaku
            });
        }
    }
    public ListItem[] Dropdownlist(DataClassesDatabaseDataContext db)
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua - "
        });

        ListData.AddRange(db.TBKategoriBahanBakus.Select(item => new ListItem
        {
            Value = item.IDKategoriBahanBaku.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }

    public static void DeleteKategoriBahanBaku(DataClassesDatabaseDataContext db, int idKategoriBahanBaku)
    {
        TBKategoriBahanBaku kategoriBahanBaku = db.TBKategoriBahanBakus.FirstOrDefault(item => item.IDKategoriBahanBaku == idKategoriBahanBaku);

        if (kategoriBahanBaku.TBKategoriBahanBakus.Count == 0 &&
            kategoriBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count == 0)
        {
            db.TBKategoriBahanBakus.DeleteOnSubmit(kategoriBahanBaku);
        }
    }
}