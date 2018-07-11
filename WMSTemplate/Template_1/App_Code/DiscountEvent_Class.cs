using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class DiscountEvent_Class : BaseWMSClass
{
    public DiscountEvent_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public DiscountEvent_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public TBDiscountEvent Cari(int idDiscountEvent)
    {
        return db.TBDiscountEvents.FirstOrDefault(item => item.IDDiscountEvent == idDiscountEvent);
    }
    public TBDiscountEvent Tambah(int idPengguna, int idTempat, string nama, DateTime tanggalAwal, DateTime tanggalAkhir, EnumStatusDiscountEvent enumStatusDiscountEvent)
    {
        TBDiscountEvent DiscountEvent = new TBDiscountEvent
        {
            //IDDiscountEvent
            IDPengguna = idPengguna,
            IDTempat = idTempat,
            IDWMS = Guid.NewGuid(),
            TanggalDaftar = DateTime.Now,
            TanggalUpdate = DateTime.Now,
            Nama = nama,
            TanggalAwal = tanggalAwal,
            TanggalAkhir = tanggalAkhir,
            EnumStatusDiscountEvent = (int)enumStatusDiscountEvent
        };

        db.TBDiscountEvents.InsertOnSubmit(DiscountEvent);

        Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses tambah data " + DiscountEvent.Nama + " berhasil");

        return DiscountEvent;
    }
    public TBDiscountEvent Ubah(int idDiscountEvent, int idPengguna, int idTempat, string nama, DateTime tanggalAwal, DateTime tanggalAkhir, EnumStatusDiscountEvent enumStatusDiscountEvent)
    {
        var DiscountEvent = Cari(idDiscountEvent);

        if (DiscountEvent != null)
        {
            //IDDiscountEvent
            DiscountEvent.IDPengguna = idPengguna;
            DiscountEvent.IDTempat = idTempat;
            //IDWMS
            //TanggalDaftar
            DiscountEvent.TanggalUpdate = DateTime.Now;
            DiscountEvent.Nama = nama;
            DiscountEvent.TanggalAwal = tanggalAwal;
            DiscountEvent.TanggalAkhir = tanggalAkhir;
            DiscountEvent.EnumStatusDiscountEvent = (int)enumStatusDiscountEvent;

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses ubah data " + DiscountEvent.Nama + " berhasil");

            return DiscountEvent;
        }
        else
            return null;
    }
    public void Hapus(int idDiscountEvent)
    {
        var DiscountEvent = Cari(idDiscountEvent);

        try
        {
            if (DiscountEvent != null)
            {
                db.TBDiscountEvents.DeleteOnSubmit(DiscountEvent);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + DiscountEvent.Nama + " berhasil");

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
    public TBDiscountEvent[] Data()
    {
        return db.TBDiscountEvents.ToArray();
    }
    public void DropDownList(DropDownList dropDownList)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDDiscountEvent";
        dropDownList.DataTextField = "Nama";
        dropDownList.DataBind();
    }
    public void DropDownList(DropDownList dropDownList, bool isLaporan)
    {
        DropDownList(dropDownList);
        dropDownList.Items.Insert(0, new ListItem { Value = "0", Text = " - Semua -" });
    }
}