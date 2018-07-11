using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Vendor_Class : BaseWMSClass
{
    #region DEFAULT
    public Vendor_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Vendor_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public Vendor_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }
    #endregion

    public TBVendor Cari(int IDVendor)
    {
        return db.TBVendors.FirstOrDefault(item => item.IDVendor == IDVendor);
    }

    public TBVendor Cari(string Nama)
    {
        return db.TBVendors.FirstOrDefault(item => item.Nama.ToLower() == Nama.ToLower());
    }

    public TBVendor CariTambah(string Nama)
    {
        var Vendor = Cari(Nama);

        if (Vendor == null)
            return Tambah(Nama);
        else
            return Vendor;
    }

    public TBVendor Tambah(string Nama)
    {
        return Tambah(Nama, "", "", "", "");
    }

    public TBVendor Tambah(string nama, string email, string alamat, string telepon1, string telepon2)
    {
        TBVendor Vendor = new TBVendor
        {
            Nama = nama,
            Email = email,
            Alamat = alamat,
            Telepon1 = telepon1,
            Telepon2 = telepon2
        };

        db.TBVendors.InsertOnSubmit(Vendor);

        return Vendor;
    }

    public void Hapus(int idVendor)
    {
        var Vendor = Cari(idVendor);

        try
        {
            if (Vendor != null)
            {
                db.TBVendors.DeleteOnSubmit(Vendor);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + Vendor.Nama + " berhasil");

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

    public TBVendor[] Data()
    {
        return db.TBVendors.OrderBy(item => item.Nama).ToArray();
    }
}