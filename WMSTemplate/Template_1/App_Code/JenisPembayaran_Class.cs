using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class JenisPembayaran_Class : BaseWMSClass
{
    public JenisPembayaran_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public JenisPembayaran_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public TBJenisPembayaran Cari(int idJenisPembayaran)
    {
        return db.TBJenisPembayarans.FirstOrDefault(item => item.IDJenisPembayaran == idJenisPembayaran);
    }
    public TBJenisPembayaran Tambah(int idJenisBebanBiaya, string nama, decimal persentaseBiaya, int idAkun)
    {
        TBJenisPembayaran JenisPembayaran = new TBJenisPembayaran
        {
            IDJenisBebanBiaya = idJenisBebanBiaya,
            IDAkun = idAkun,
            Nama = nama,
            PersentaseBiaya = persentaseBiaya
        };

        TBKonfigurasiAkun KonfigurasiAkun = new TBKonfigurasiAkun
        {
            IDAkun = idAkun,
            IDTempat = 1,
            Nama = nama
        };

        db.TBJenisPembayarans.InsertOnSubmit(JenisPembayaran);
        db.TBKonfigurasiAkuns.InsertOnSubmit(KonfigurasiAkun);

        Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses tambah data " + JenisPembayaran.Nama + " berhasil");

        return JenisPembayaran;
    }
    public TBJenisPembayaran Ubah(int idJenisPembayaran, int idJenisBebanBiaya, string nama, decimal persentaseBiaya, int idAkun)
    {
        var JenisPembayaran = Cari(idJenisPembayaran);

        if (JenisPembayaran != null)
        {
            JenisPembayaran.IDJenisBebanBiaya = idJenisBebanBiaya;
            JenisPembayaran.IDAkun = idAkun;
            JenisPembayaran.Nama = nama;
            JenisPembayaran.PersentaseBiaya = persentaseBiaya;

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses ubah data " + JenisPembayaran.Nama + " berhasil");

            return JenisPembayaran;
        }
        else
            return null;
    }
    public void Hapus(int idJenisPembayaran)
    {
        var JenisPembayaran = Cari(idJenisPembayaran);

        try
        {
            if (JenisPembayaran != null)
            {
                db.TBJenisPembayarans.DeleteOnSubmit(JenisPembayaran);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + JenisPembayaran.Nama + " berhasil");

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

    public TBJenisPembayaran[] Data()
    {
        return db.TBJenisPembayarans.ToArray();
    }
    public void DropDownList(DropDownList dropDownList, bool isLaporan = false)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDJenisPembayaran";
        dropDownList.DataTextField = "Nama";
        dropDownList.DataBind();

        if (isLaporan)
            dropDownList.Items.Insert(0, new ListItem { Value = "0", Text = " - Semua -" });
    }

    public void DataListBox(ListBox listBox)
    {
        listBox.DataSource = Data();
        listBox.DataValueField = "IDJenisPembayaran";
        listBox.DataTextField = "Nama";
        listBox.DataBind();
    }
}

