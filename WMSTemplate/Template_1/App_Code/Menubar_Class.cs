using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Menubar_Class : BaseWMSClass
{
    #region DEFAULT
    public Menubar_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }

    public Menubar_Class(DataClassesDatabaseDataContext _db, bool isDisablePengguna) : base(_db, isDisablePengguna)
    {
    }

    public Menubar_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }
    #endregion

    #region NOTIFIKASI MESSAGE
    private string notifikasiMessage;
    public string NotifikasiMessage
    {
        get
        {
            return notifikasiMessage;
        }
    }
    #endregion

    public object Data()
    {
        return db.TBMenubars
            .Where(item => !item.IDMenubarParent.HasValue)
            .Select(item => new
            {
                PunyaSubMenubar = item.TBMenubars.Count > 0 || item.Url == "",
                item.IDMenubar,
                item.Urutan,
                item.Kode,
                item.Nama,
                item.Icon,
                item.Url,
                HaveChild = item.TBMenubars.Count > 0 ? true : false,
                MenuLevel2 = item.TBMenubars.OrderBy(item2 => item2.Urutan).Select(item2 => new
                {
                    item2.IDMenubar,
                    item2.Urutan,
                    item2.Kode,
                    item2.Nama,
                    item2.Icon,
                    item2.Url,
                    HaveChild = item2.TBMenubars.Count > 0 ? true : false,
                    MenuLevel3 = item2.TBMenubars.OrderBy(item3 => item3.Urutan)
                })
            })
            .OrderBy(item => item.Urutan)
            .ToArray();
    }

    public object Administrator()
    {
        TBMenubar[] menuAdministrator = db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == 2).Select(item => item.TBMenubar).ToArray();

        return db.TBMenubars.AsEnumerable()
            .Where(item => !item.IDMenubarParent.HasValue && menuAdministrator.Any(item2 => item2.IDMenubar == item.IDMenubar))
            .Select(item => new
            {
                PunyaSubMenubar = item.TBMenubars.Count > 0 || item.Url == "",
                item.IDMenubar,
                item.Urutan,
                item.Kode,
                item.Nama,
                item.Icon,
                item.Url,
                HaveChild = item.TBMenubars.Count > 0 ? true : false,
                MenuLevel2 = item.TBMenubars.Where(item2 => menuAdministrator.Any(item3 => item3.IDMenubar == item2.IDMenubar)).OrderBy(item2 => item2.Urutan).Select(item2 => new
                {
                    item2.IDMenubar,
                    item2.Urutan,
                    item2.Kode,
                    item2.Nama,
                    item2.Icon,
                    item2.Url,
                    HaveChild = item2.TBMenubars.Count > 0 ? true : false,
                    MenuLevel3 = item2.TBMenubars.Where(item3 => menuAdministrator.Any(item4 => item4.IDMenubar == item3.IDMenubar)).OrderBy(item3 => item3.Urutan)
                })
            })
            .OrderBy(item => item.Urutan)
            .ToArray();
    }

    public TBMenubar[] DataParent()
    {
        return db.TBMenubars
            .Where(item => !item.IDMenubarParent.HasValue)
            .OrderBy(item => item.Urutan).ToArray();
    }

    public TBMenubar PengaturanUrutan(int IDMenubar, int Urutan)
    {
        var Menubar = Cari(IDMenubar);

        if (Menubar != null)
        {
            Menubar.Urutan = Urutan;

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, notifikasiMessage = "Ubah Urutan " + Menubar.Nama + " berhasil");
        }

        return Menubar;
    }

    public TBMenubar Cari(int IDMenubar)
    {
        return db.TBMenubars.FirstOrDefault(item => item.IDMenubar == IDMenubar);
    }

    public void Hapus(int IDMenubar)
    {
        var Menubar = Cari(IDMenubar);

        try
        {
            if (Menubar != null)
            {
                db.TBMenubars.DeleteOnSubmit(Menubar);

                Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Hapus Menubar " + Menubar.Nama + " berhasil");

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

    public TBMenubar Tambah(int IDMenubarParent, EnumMenubarModul enumMenubarModul, int Urutan, string Kode, string Nama, string Url, string Icon, int LevelMenu)
    {
        TBMenubar Menubar = new TBMenubar
        {
            //IDMenubar
            IDMenubarParent = (IDMenubarParent == 0) ? (int?)null : IDMenubarParent,
            EnumMenubarModul = (int)enumMenubarModul,
            Urutan = Urutan,
            Kode = Kode,
            Nama = Nama,
            Url = Url,
            Icon = Icon,
            LevelMenu = LevelMenu
        };

        db.TBMenubars.InsertOnSubmit(Menubar);

        Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Tambah Menubar " + Menubar.Nama + " berhasil");

        return Menubar;
    }

    public TBMenubar Ubah(int IDMenubar, int IDMenubarParent, EnumMenubarModul enumMenubarModul, int Urutan, string Kode, string Nama, string Url, string Icon, int LevelMenu)
    {
        var Menubar = Cari(IDMenubar);

        if (Menubar != null)
        {
            //IDMenubar
            Menubar.IDMenubarParent = (IDMenubarParent == 0) ? (int?)null : IDMenubarParent;
            Menubar.EnumMenubarModul = (int)enumMenubarModul;
            Menubar.Urutan = Urutan;
            Menubar.Kode = Kode;
            Menubar.Nama = Nama;
            Menubar.Url = Url;
            Menubar.Icon = Icon;
            Menubar.LevelMenu = LevelMenu;

            Notifikasi(EnumAlert.success, Pengguna.IDPengguna, "Ubah Menubar " + Menubar.Nama + " berhasil");

            return Menubar;
        }
        else
            return null;
    }

    public string GenerateHTML(int IDPenggunaGrup, EnumMenubarModul enumMenubarModul)
    {
        string Result = "";

        TBMenubar[] MenubarPenggunaGrup = db.TBMenubarPenggunaGrups
            .Where(item =>
                item.IDGrupPengguna == IDPenggunaGrup &&
                item.TBMenubar.IDMenubarParent == null &&
                item.TBMenubar.EnumMenubarModul == (int)enumMenubarModul)
            .Select(item => item.TBMenubar)
            .OrderBy(item => item.Urutan)
            .ToArray();

        if (enumMenubarModul == EnumMenubarModul.WITAdministrator_Sidebar)
        {
            Result += "<ul class='nav flex-column'>";

            List<int> ListIDMenuFinish = new List<int>();

            foreach (var item in MenubarPenggunaGrup)
            {
                if (item.TBMenubars.Count == 0)
                {
                    Result += @"
                            <li class='nav-item'>
                                <a class='nav-link nav-link-parent' href='" + item.Url + @"'>
                                    <span data-feather='" + item.Icon + "' class='align-middle'></span>" + item.Nama + @"
                                </a>
                            </li>   
                        ";
                }
                else
                {
                    Result += @"
                            <li class='nav-item'>
                                <a class='nav-link nav-link-parent' data-toggle='collapse' href='#collapse" + item.IDMenubar + @"' >
                                    <span data-feather='" + item.Icon + "' class='align-middle'></span>" + item.Nama + @"
                                </a>
                                <div class='collapse bg-smoke' id='collapse" + item.IDMenubar + @"'>
                                    <ul class='nav flex-column'>";

                    Result += CariSubMenu(IDPenggunaGrup, item, 0);

                    Result += @"
                                    </ul>
                                </div>
                            </li>";
                }
            }

            Result += "</ul>";
        }

        return Result;
    }

    public string CariSubMenu(int IDPenggunaGrup, TBMenubar MenuBar, int width)
    {
        string SubMenu = string.Empty;

        foreach (var item in MenuBar.TBMenubars.OrderBy(item => item.Urutan))
        {
            if (item.TBMenubars.Count == 0)
            {
                if (item.TBMenubarPenggunaGrups.FirstOrDefault(item2 => item2.IDGrupPengguna == IDPenggunaGrup) != null)
                {
                    SubMenu += @"
                            <li class='nav-item'>
                                <a class='nav-link nav-link-child' href='" + item.Url + @"'>
                                    <span " + (width == 0 ? "data-feather='circle'" : string.Empty) + " style='padding-left:" + width + "px;'></span>" + item.Nama + @"
                                </a>
                            </li>   
                        ";
                }
            }
            else
            {
                if (item.TBMenubarPenggunaGrups.FirstOrDefault(item2 => item2.IDGrupPengguna == IDPenggunaGrup) != null)
                {
                    SubMenu += @"
                            <li class='nav-item'>
                                <a class='nav-link nav-link-child' data-toggle='collapse' href='#collapse" + item.IDMenubar + @"'>
                                    <span " + (width == 0 ? "data-feather='circle'" : string.Empty) + " style='padding-left:" + width + "px;'></span>" + item.Nama + @"
                                </a>
                                <div class='collapse bg-gainsboro' id='collapse" + item.IDMenubar + @"'>
                                    <ul class='nav flex-column'>";

                    SubMenu += CariSubMenu(IDPenggunaGrup, item, width + 24);

                    SubMenu += @"
                                    </ul>
                                </div>
                            </li>";
                }              
            }
        }

        return SubMenu;
    }

    public TBMenubarPenggunaGrup[] HakAksesPenggunaGrup(int IDPenggunaGrup, EnumMenubarModul enumMenubarModul)
    {
        return db.TBMenubarPenggunaGrups
            .Where(item =>
                item.IDGrupPengguna == IDPenggunaGrup &&
                item.TBMenubar.EnumMenubarModul == (int)enumMenubarModul)
            .ToArray();
    }

    public void ResetHakAksesPenggunaGrup(int IDPenggunaGrup, EnumMenubarModul enumMenubarModul)
    {
        db.TBMenubarPenggunaGrups.DeleteAllOnSubmit(HakAksesPenggunaGrup(IDPenggunaGrup, enumMenubarModul));
    }

    public void TambahHakAksesPenggunaGrup(int IDGrupPengguna, int IDMenubar)
    {
        db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup
        {
            IDGrupPengguna = IDGrupPengguna,
            IDMenubar = IDMenubar
        });
    }

    public void EnumStatusDiscountEventDropdownList(DropDownList dropDownList)
    {
        dropDownList.Items.Clear();

        dropDownList.Items.Add(new ListItem { Value = ((int)EnumStatusDiscountEvent.Aktif).ToString(), Text = PengaturanEnum.EnumStatusDiscountEventText(EnumStatusDiscountEvent.Aktif) });
        dropDownList.Items.Add(new ListItem { Value = ((int)EnumStatusDiscountEvent.Implementasi).ToString(), Text = PengaturanEnum.EnumStatusDiscountEventText(EnumStatusDiscountEvent.Implementasi) });
        dropDownList.Items.Add(new ListItem { Value = ((int)EnumStatusDiscountEvent.NonAktif).ToString(), Text = PengaturanEnum.EnumStatusDiscountEventText(EnumStatusDiscountEvent.NonAktif) });
    }

    public void EnumMenubarModulDropdownList(DropDownList dropDownList)
    {
        dropDownList.Items.Clear();

        foreach (EnumMenubarModul item in Enum.GetValues(typeof(EnumMenubarModul)))
        {
            dropDownList.Items.Add(new ListItem
            {
                Value = ((int)item).ToString(),
                Text = Enum.GetName(typeof(EnumMenubarModul), item).Replace("_", " ")
            });
        }
    }
}