using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TemplateKeterangan_Class
{
    public TBTemplateKeterangan[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBTemplateKeterangans.ToArray();
    }
    public TBTemplateKeterangan Cari(DataClassesDatabaseDataContext db, string isi)
    {
        return db.TBTemplateKeterangans.FirstOrDefault(item => item.Isi.ToString().ToLower() == isi.ToLower());
    }
    public TBTemplateKeterangan Cari(DataClassesDatabaseDataContext db, int idTemplateKeterangan)
    {
        return db.TBTemplateKeterangans.FirstOrDefault(item => item.IDTemplateKeterangan == idTemplateKeterangan);
    }
    public TBTemplateKeterangan CariTambah(DataClassesDatabaseDataContext db, string isi)
    {
        var TemplateKeterangan = Cari(db, isi);

        if (TemplateKeterangan == null)
            TemplateKeterangan = Tambah(db, isi);

        return TemplateKeterangan;
    }
    public TBTemplateKeterangan Tambah(DataClassesDatabaseDataContext db, string isi)
    {
        TBTemplateKeterangan TemplateKeterangan = new TBTemplateKeterangan
        {
            Isi = isi
        };

        db.TBTemplateKeterangans.InsertOnSubmit(TemplateKeterangan);

        return TemplateKeterangan;
    }

    public TBTemplateKeterangan Ubah(DataClassesDatabaseDataContext db, int idTemplateKeterangan, string isi)
    {
        var TemplateKeterangan = Cari(db, idTemplateKeterangan);

        if (TemplateKeterangan != null)
            TemplateKeterangan.Isi = isi;

        return TemplateKeterangan;
    }

    public bool Hapus(DataClassesDatabaseDataContext db, int idTemplateKeterangan)
    {
        var TemplateKeterangan = Cari(db, idTemplateKeterangan);

        db.TBTemplateKeterangans.DeleteOnSubmit(TemplateKeterangan);

        return true;
    }
}
