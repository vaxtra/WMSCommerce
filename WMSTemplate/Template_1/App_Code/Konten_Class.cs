using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Konten_Class : BaseWMSClass
{
    public Konten_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public Konten_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public TBKonten Tambah(EnumKontenJenis enumKontenJenis, string judul, string isi)
    {
        TBKonten Konten = new TBKonten
        {
            EnumKontenJenis = (int)enumKontenJenis,
            Tanggal = DateTime.Now,
            Judul = judul,
            IsiSingkat = Pengaturan.Ringkasan(isi),
            Isi = isi
        };

        db.TBKontens.InsertOnSubmit(Konten);

        return Konten;
    }
    public TBKonten Ubah(int idKonten, EnumKontenJenis enumKontenJenis, string judul, string isi)
    {
        TBKonten Konten = Cari(idKonten);

        if (Konten != null)
        {
            Konten.EnumKontenJenis = (int)enumKontenJenis;
            Konten.Judul = judul;
            Konten.IsiSingkat = Pengaturan.Ringkasan(isi);
            Konten.Isi = isi;

            return Konten;
        }
        else
            return null;
    }

    public TBKonten Cari(int idKonten)
    {
        return db.TBKontens.FirstOrDefault(item => item.IDKonten == idKonten);
    }

    public void Hapus(int idKonten)
    {
        var Konten = Cari(idKonten);

        try
        {
            if (Konten != null)
            {
                db.TBKontens.DeleteOnSubmit(Konten);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + Konten.Judul + " berhasil");

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

    public TBKonten[] Data(EnumKontenJenis enumKontenJenis)
    {
        return db.TBKontens.Where(item => item.EnumKontenJenis == (int)enumKontenJenis).ToArray();
    }
}