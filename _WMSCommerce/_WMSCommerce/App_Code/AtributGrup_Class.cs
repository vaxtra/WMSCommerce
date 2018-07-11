using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class AtributGrup_Class : BaseWMSClass
{
    public AtributGrup_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public AtributGrup_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public TBAtributGrup Cari(int idAtributGrup)
    {
        return db.TBAtributGrups.FirstOrDefault(item => item.IDAtributGrup == idAtributGrup);
    }
    public TBAtributGrup Tambah(string nama)
    {
        TBAtributGrup AtributGrup = new TBAtributGrup
        {
            IDWMS = Guid.NewGuid(),
            TanggalDaftar = DateTime.Now,
            TanggalUpdate = DateTime.Now,
            Nama = nama
        };

        db.TBAtributGrups.InsertOnSubmit(AtributGrup);

        Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses tambah data " + AtributGrup.Nama + " berhasil");

        return AtributGrup;
    }
    public TBAtributGrup Ubah(int idAtributGrup, string nama)
    {
        var AtributGrup = Cari(idAtributGrup);

        if (AtributGrup != null)
        {
            AtributGrup.TanggalUpdate = DateTime.Now;
            AtributGrup.Nama = nama;

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses ubah data " + AtributGrup.Nama + " berhasil");

            return AtributGrup;
        }
        else
            return null;
    }
    public void Hapus(int idAtributGrup)
    {
        var AtributGrup = Cari(idAtributGrup);

        try
        {
            if (AtributGrup != null)
            {
                db.TBAtributGrups.DeleteOnSubmit(AtributGrup);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + AtributGrup.Nama + " berhasil");

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
    public TBAtributGrup[] Data()
    {
        return db.TBAtributGrups.ToArray();
    }
    public void DropDownList(DropDownList dropDownList)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDAtributGrup";
        dropDownList.DataTextField = "Nama";
        dropDownList.DataBind();
    }
    public void DropDownList(DropDownList dropDownList, bool isLaporan)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDAtributGrup";
        dropDownList.DataTextField = "Nama";
        dropDownList.DataBind();
        dropDownList.Items.Insert(0, new ListItem { Value = "0", Text = " - Semua -" });
    }
}
