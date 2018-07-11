using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Atribut_Class : BaseWMSClass
{
    #region DEFAULT
    public Atribut_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public Atribut_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    public Atribut_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }
    #endregion

    public TBAtribut Cari(int idAtribut)
    {
        return db.TBAtributs.FirstOrDefault(item => item.IDAtribut == idAtribut);
    }
    public TBAtribut Tambah(int idAtributGrup, string nama, bool pilihan)
    {
        TBAtribut Atribut = new TBAtribut
        {
            IDWMS = Guid.NewGuid(),
            IDAtributGrup = idAtributGrup,
            TanggalDaftar = DateTime.Now,
            TanggalUpdate = DateTime.Now,
            Nama = nama,
            Pilihan = pilihan
        };

        db.TBAtributs.InsertOnSubmit(Atribut);

        Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses tambah data " + Atribut.Nama + " berhasil");

        return Atribut;
    }
    public TBAtribut Ubah(int idAtribut, int idAtributGrup, string nama, bool pilihan)
    {
        var Atribut = Cari(idAtribut);

        if (Atribut != null)
        {
            Atribut.IDAtributGrup = idAtributGrup;
            Atribut.TanggalUpdate = DateTime.Now;
            Atribut.Nama = nama;
            Atribut.Pilihan = pilihan;

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses ubah data " + Atribut.Nama + " berhasil");

            return Atribut;
        }
        else
            return null;
    }
    public void Hapus(int idAtribut)
    {
        var Atribut = Cari(idAtribut);

        try
        {
            if (Atribut != null)
            {
                db.TBAtributs.DeleteOnSubmit(Atribut);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Proses hapus data " + Atribut.Nama + " berhasil");

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
    public TBAtribut[] Data()
    {
        return db.TBAtributs.OrderBy(item => item.IDAtributGrup).ToArray();
    }

    public void DropDownList(DropDownList dropDownList, bool isLaporan = false)
    {
        dropDownList.DataSource = Data();
        dropDownList.DataValueField = "IDAtribut";
        dropDownList.DataTextField = "Nama";
        dropDownList.DataBind();

        if (isLaporan)
            dropDownList.Items.Insert(0, new ListItem { Value = "0", Text = " - Semua -" });
    }
    public string DropdownListSelect2(GrupAtribut grupAtribut)
    {
        var result = "";
        var tempAtribut = "";
        foreach (var item in db.TBAtributs.Where(item => item.IDAtributGrup == (int)grupAtribut && item.Pilihan))
        {
            var tempAtributPilihan = "";

            foreach (var item2 in item.TBAtributPilihans.ToArray())
            {
                tempAtributPilihan += "\"" + item2.Nama + "\",";
            }

            tempAtribut += "$('.Atribut" + item.IDAtribut + "').select2({ tags: [" + tempAtributPilihan + "], tokenSeparators: [\",\"], maximumSelectionSize: 1 });";
        }

        result = "<script>";
        result += "$(document).ready(function () { jQuery(function ($) {";
        result += tempAtribut;
        result += "}); });";

        result += "function pageLoad(sender, args) { if (args.get_isPartialLoad()) { jQuery(function ($) {";
        result += tempAtribut;
        result += "}); } };";
        result += "</script>";

        return result;
    }
}
