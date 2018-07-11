using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class StoreKey_Class : BaseWMSClass
{
    #region MESSAGE
    public string MessageWarning
    {
        get
        {
            return "Silahkan lakukan Pembayaran Maintenance WIT. Management System. Hubungi : <a href=\"mailto:information@wit.co.id?subject=Informasi Pembayaran Maintenance WIT. Management System\">information@wit.co.id</a>";
        }
    }
    public string MessageDanger
    {
        get
        {
            return "Untuk sementara Anda tidak dapat mengakses aplikasi. Silahkan lakukan Pembayaran Maintenance WIT. Management System. Hubungi : <a href=\"mailto:information@wit.co.id?subject=Informasi Pembayaran Maintenance WIT. Management System\">information@wit.co.id</a>";
        }
    }
    #endregion

    #region DEFAULT
    public StoreKey_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public StoreKey_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public StoreKey_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    public void Generate()
    {
        if (db.TBStoreKeys.Count(item => item.IsAktif == false) == 0)
        {
            Store_Class ClassStore = new Store_Class(db, true);
            PengirimanEmail_Class PengirimanEmailClass = new PengirimanEmail_Class(db, true);
            List<TBStoreKey> ListStoreKey = new List<TBStoreKey>();

            var Data = db.TBStoreKeys.ToArray();

            var Tanggal = DateTime.Now.AddMonths(-1);

            while (ListStoreKey.Count < 12)
            {
                var Key = Guid.NewGuid();
                Tanggal = Tanggal.AddMonths(1);

                if (ListStoreKey.FirstOrDefault(item => item.IDStoreKey == Key) == null && Data.FirstOrDefault(item => item.IDStoreKey == Key || item.TanggalKey.Date == Tanggal.Date) == null)
                    ListStoreKey.Add(new TBStoreKey
                    {
                        IDStore = 1,
                        //IDPenggunaAktif
                        IDStoreKey = Key,
                        TanggalKey = Tanggal
                        //IsAktif
                        //TanggalAktif
                    });
            }

            db.TBStoreKeys.InsertAllOnSubmit(ListStoreKey);
            db.SubmitChanges();

            #region KIRIM EMAIL STORE KEY
            string Pesan = "Dear All,<br/><br/>Berikut Store Key untuk " + ClassStore.Data().Nama + " : <br/><br/>";

            foreach (var item in ListStoreKey)
            {
                Pesan += "<b>" + item.TanggalKey.ToFormatTanggal() + "</b><br/>" + item.IDStoreKey + "<br/><br/>";
            }

            PengirimanEmailClass.Kirim("herdiawan.rendy@gmail.com", "Store Key " + ClassStore.Data().Nama, Pesan);
            #endregion

            
        }
    }

    public string Validasi(out EnumAlert enumAlert)
    {
        if (Pengguna.IDGrupPengguna != 1)
        {
            enumAlert = Validasi();

            var Data = db.TBStoreKeys.Where(item => item.IsAktif == false && item.TanggalKey <= DateTime.Now).Count();

            if (enumAlert == EnumAlert.warning)
                return MessageWarning;
            else if (enumAlert == EnumAlert.danger)
                return MessageDanger;
            else
                return "";
        }
        else
        {
            enumAlert = EnumAlert.info;
            return "";
        }
    }

    public EnumAlert Validasi()
    {
        var Data = db.TBStoreKeys.Where(item => item.IsAktif == false && item.TanggalKey <= DateTime.Now.Date).Count();

        if (Data == 0)
            return EnumAlert.info;
        else if (Data <= 3)
            return EnumAlert.warning;
        else if (Data > 3)
            return EnumAlert.danger;

        else
            return EnumAlert.info;
    }

    public string Verifikasi(string storeKey)
    {
        var Data = db.TBStoreKeys.AsEnumerable().FirstOrDefault(item => item.IDStoreKey.ToString().Equals(storeKey, StringComparison.InvariantCulture));

        if (Data != null)
        {
            if (Data.IsAktif)
            {
                ErrorMessage = "Store Key sudah diaktifkan";
                return "";
            }
            else
            {
                Data.TanggalAktif = DateTime.Now;
                Data.IsAktif = true;

                db.SubmitChanges();

                return "Store Key " + Data.TanggalKey.ToFormatTanggal() + " berhasil diaktifkan";
            }
        }
        else
        {
            ErrorMessage = "Store Key tidak ditemukan";
            return "";
        }
    }
}