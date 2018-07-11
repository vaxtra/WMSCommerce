using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Meja_Class
{
    public TBMeja[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBMejas.ToArray();
    }
    public TBMeja Cari(DataClassesDatabaseDataContext db, int idMeja)
    {
        return db.TBMejas.FirstOrDefault(item => item.IDMeja == idMeja);
    }
    public bool Hapus(DataClassesDatabaseDataContext db, int idMeja)
    {
        var Meja = Cari(db, idMeja);

        if (Meja != null)
        {
            if (Meja.TBTransaksis.Count == 0 && Meja.TBWaitingLists.Count == 0)
            {
                db.TBMejas.DeleteOnSubmit(Meja);
                db.SubmitChanges();

                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    public TBMeja Ubah(DataClassesDatabaseDataContext db, int idMeja, string nama, int jumlahKursi, bool vip, bool status)
    {
        var Meja = Cari(db, idMeja);

        if (Meja != null)
        {
            //IDMeja
            //IDStatusMeja
            Meja.Nama = nama;
            Meja.JumlahKursi = jumlahKursi;
            Meja.VIP = vip;
            Meja.Status = status;
        }

        return Meja;
    }
    public TBMeja Tambah(DataClassesDatabaseDataContext db, string nama, int jumlahKursi, bool vip, bool status)
    {
        TBMeja Meja = new TBMeja
        {
            //IDMeja
            IDStatusMeja = 1, //1 : OPEN
            JumlahKursi = jumlahKursi,
            Nama = nama,
            VIP = vip,
            Status = status
        };

        db.TBMejas.InsertOnSubmit(Meja);

        return Meja;
    }

    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db)
    {
        List<ListItem> Pelanggan = new List<ListItem>();

        Pelanggan.Add(new ListItem { Value = "0", Text = "- Semua Meja -" });

        Pelanggan.AddRange(Data(db).Select(item => new ListItem
        {
            Value = item.IDMeja.ToString(),
            Text = item.Nama
        }));

        return Pelanggan.ToArray();
    }
}