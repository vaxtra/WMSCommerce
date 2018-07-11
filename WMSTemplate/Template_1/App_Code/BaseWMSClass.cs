using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public abstract class BaseWMSClass
{
    internal DataClassesDatabaseDataContext db;
    internal PenggunaLogin Pengguna;
    public string ErrorMessage
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                throw new Exception("[WMS Error] " + value);
        }
    }

    #region CONSTRUCTOR
    public BaseWMSClass(DataClassesDatabaseDataContext _db)
    {
        db = _db;

        HttpContext context = HttpContext.Current;

        if (context != null && context.Session["PenggunaLogin"] != null)
            Pengguna = (PenggunaLogin)context.Session["PenggunaLogin"];
        else
            ErrorMessage = "Session Pengguna tidak ditemukan";
    }
    public BaseWMSClass(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna)
    {
        db = _db;

        if (_pengguna != null)
            Pengguna = _pengguna;
        else
            ErrorMessage = "Pengguna tidak ditemukan";
    }

    public BaseWMSClass(DataClassesDatabaseDataContext _db, bool isDisablePengguna)
    {
        if (isDisablePengguna)
        {
            db = _db;
            Pengguna = null;
        }

    }
    #endregion

    internal void Notifikasi(EnumAlert enumAlert, int idPenggunaPenerima, string isi)
    {
        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, Pengguna.IDPengguna, idPenggunaPenerima, enumAlert, isi);
    }
    internal void NotifikasiAll(EnumAlert enumAlert, string isi)
    {
        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, Pengguna.IDPengguna, enumAlert, isi);
    }
}