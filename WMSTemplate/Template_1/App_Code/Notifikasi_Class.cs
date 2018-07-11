using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Notifikasi_Class
{
    public Notifikasi_Class(DataClassesDatabaseDataContext db, int idPenggunaPengirim, int idPenggunaPenerima, EnumAlert enumAlert, string isi)
    {
        try
        {
            TBNotifikasi Notifikasi = new TBNotifikasi
            {
                IDWMS = Guid.NewGuid(),
                IDPenggunaPengirim = idPenggunaPengirim,
                IDPenggunaPenerima = idPenggunaPenerima,
                TanggalDaftar = DateTime.Now,
                EnumAlert = (int)enumAlert,
                Isi = isi
            };

            db.TBNotifikasis.InsertOnSubmit(Notifikasi);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Notifikasi_Class(DataClassesDatabaseDataContext db, int idPenggunaPengirim, EnumAlert enumAlert, string isi)
    {
        try
        {
            Guid idWMS = Guid.NewGuid();
            DateTime tanggalDaftar = DateTime.Now;

            foreach (var item in db.TBPenggunas.Where(item => item._IsActive).ToArray())
            {
                db.TBNotifikasis.InsertOnSubmit(new TBNotifikasi
                {
                    IDWMS = idWMS,
                    IDPenggunaPengirim = idPenggunaPengirim,
                    IDPenggunaPenerima = item.IDPengguna,
                    TanggalDaftar = tanggalDaftar,
                    EnumAlert = (int)enumAlert,
                    Isi = isi
                });
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Notifikasi_Class(DataClassesDatabaseDataContext db, EnumAlert enumAlert, string isi)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)HttpContext.Current.Session["PenggunaLogin"];

        try
        {
            TBNotifikasi Notifikasi = new TBNotifikasi
            {
                IDWMS = Guid.NewGuid(),
                IDPenggunaPengirim = Pengguna.IDPengguna,
                IDPenggunaPenerima = Pengguna.IDPengguna,
                TanggalDaftar = DateTime.Now,
                EnumAlert = (int)enumAlert,
                Isi = isi
            };

            db.TBNotifikasis.InsertOnSubmit(Notifikasi);
        }
        catch (Exception)
        {
            throw;
        }
    }
}