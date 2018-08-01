using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Satuan_Class
/// </summary>
public class Satuan_Class
{
    public TBSatuan Cari(DataClassesDatabaseDataContext db, string nama)
    {
        return db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == nama.ToLower());
    }
    public TBSatuan CariTambah(DataClassesDatabaseDataContext db, string nama)
    {
        var Satuan = Cari(db, nama);

        if (Satuan == null)
            Satuan = Tambah(db, nama);

        return Satuan;
    }

    public TBSatuan Tambah(DataClassesDatabaseDataContext db, string nama)
    {
        var Satuan = new TBSatuan
        {
            Nama = nama
        };

        db.TBSatuans.InsertOnSubmit(Satuan);

        return Satuan;
    }
    public TBSatuan[] Data()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return Data(db);
        }
    }
    public TBSatuan[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBSatuans.OrderBy(item => item.Nama).ToArray();
    }

    public ListItem[] Dropdownlist(DataClassesDatabaseDataContext db)
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua - "
        });

        ListData.AddRange(db.TBSatuans.Select(item => new ListItem
        {
            Value = item.IDSatuan.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }
 
    public static void DeleteSatuan(DataClassesDatabaseDataContext db, int idSatuan)
    {
        TBSatuan satuan = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == idSatuan);

        if (satuan.TBBahanBakus.Count == 0 &&
            satuan.TBBahanBakus1.Count == 0 &&
            satuan.TBPenerimaanPOProduksiBahanBakuDetails.Count == 0 &&
            satuan.TBPengirimanPOProduksiBahanBakuDetails.Count == 0 &&
            satuan.TBPengirimanPOProduksiProdukDetails.Count == 0 &&
            satuan.TBPerpindahanStokBahanBakus.Count == 0 &&
            satuan.TBPOProduksiBahanBakuDetails.Count == 0 &&
            satuan.TBPOProduksiBahanBakuKomposisis.Count == 0 &&
            satuan.TBPOProduksiProdukKomposisis.Count == 0 &&
            satuan.TBProyeksiKomposisis.Count == 0 &&
            satuan.TBTransferBahanBakuDetails.Count == 0)
        {
            db.TBSatuans.DeleteOnSubmit(satuan);
        }
    }
}