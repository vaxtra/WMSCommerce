using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Menu_Pengguna : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Menubar_Class ClassMenubar = new Menubar_Class(db);

                DropDownListGrupPengguna.DataSource = db.TBGrupPenggunas.ToArray();
                DropDownListGrupPengguna.DataTextField = "Nama";
                DropDownListGrupPengguna.DataValueField = "IDGrupPengguna";
                DropDownListGrupPengguna.DataBind();

                RepeaterMenu.DataSource = ClassMenubar.Data();
                RepeaterMenu.DataBind();

                PengaturanHakAkses();
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Menubar_Class ClassMenubar = new Menubar_Class(db);

            //RESET TABLE MENUBAR PENGGUNA GRUP
            ClassMenubar.ResetHakAksesPenggunaGrup(DropDownListGrupPengguna.SelectedValue.ToInt(), EnumMenubarModul.WITAdministrator_Sidebar);

            foreach (RepeaterItem item in RepeaterMenu.Items)
            {
                Repeater RepeaterSubMenubar = (Repeater)item.FindControl("RepeaterSubMenubar");
                CheckBox CheckBoxPilihParent = (CheckBox)item.FindControl("CheckBoxPilihParent");
                HiddenField HiddenFieldIDMenuParent = (HiddenField)item.FindControl("HiddenFieldIDMenuParent");

                //JIKA TIDAK MEMILIKI SUB MENU
                if (CheckBoxPilihParent.Checked)
                    ClassMenubar.TambahHakAksesPenggunaGrup(DropDownListGrupPengguna.SelectedValue.ToInt(), HiddenFieldIDMenuParent.Value.ToInt());
                else //JIKA MEMILIKI SUB MENU
                {
                    //MEMASUKKAN SUB MENU
                    foreach (RepeaterItem item2 in RepeaterSubMenubar.Items)
                    {
                        CheckBox CheckBoxPilih = (CheckBox)item2.FindControl("CheckBoxPilih");

                        if (CheckBoxPilih.Checked)
                        {
                            HiddenField HiddenFieldIDMenu = (HiddenField)item2.FindControl("HiddenFieldIDMenu");

                            ClassMenubar.TambahHakAksesPenggunaGrup(DropDownListGrupPengguna.SelectedValue.ToInt(), HiddenFieldIDMenu.Value.ToInt());
                        }
                    }
                }
            }

            db.SubmitChanges();
        }
    }

    protected void DropDownListGrupPengguna_SelectedIndexChanged(object sender, EventArgs e)
    {
        PengaturanHakAkses();
    }

    private void PengaturanHakAkses()
    {
        #region RESET REPEATER
        foreach (RepeaterItem item in RepeaterMenu.Items)
        {
            Repeater RepeaterSubMenubar = (Repeater)item.FindControl("RepeaterSubMenubar");
            CheckBox CheckBoxPilihParent = (CheckBox)item.FindControl("CheckBoxPilihParent");

            CheckBoxPilihParent.Checked = false;

            foreach (RepeaterItem item2 in RepeaterSubMenubar.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item2.FindControl("CheckBoxPilih");
                CheckBoxPilih.Checked = false;
            }
        }
        #endregion

        TBMenubarPenggunaGrup[] MenubarPenggunaGrup;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Menubar_Class ClassMenubar = new Menubar_Class(db);

            MenubarPenggunaGrup = ClassMenubar.HakAksesPenggunaGrup(DropDownListGrupPengguna.SelectedValue.ToInt(), EnumMenubarModul.WITAdministrator_Sidebar);
        }

        if (MenubarPenggunaGrup.Count() > 0)
        {
            foreach (RepeaterItem item in RepeaterMenu.Items)
            {
                Repeater RepeaterSubMenubar = (Repeater)item.FindControl("RepeaterSubMenubar");
                CheckBox CheckBoxPilihParent = (CheckBox)item.FindControl("CheckBoxPilihParent");
                HiddenField HiddenFieldIDMenuParent = (HiddenField)item.FindControl("HiddenFieldIDMenuParent");

                if (MenubarPenggunaGrup.FirstOrDefault(item2 => item2.IDMenubar == HiddenFieldIDMenuParent.Value.ToInt()) != null)
                    CheckBoxPilihParent.Checked = true;

                foreach (RepeaterItem item2 in RepeaterSubMenubar.Items)
                {
                    CheckBox CheckBoxPilih = (CheckBox)item2.FindControl("CheckBoxPilih");
                    HiddenField HiddenFieldIDMenu = (HiddenField)item2.FindControl("HiddenFieldIDMenu");

                    if (MenubarPenggunaGrup.FirstOrDefault(item3 => item3.IDMenubar == HiddenFieldIDMenu.Value.ToInt()) != null)
                        CheckBoxPilih.Checked = true;
                }
            }
        }
    }

    protected void RepeaterMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PilihSemua" || e.CommandName == "HapusPilihan")
        {
            Repeater RepeaterSubMenubar = (Repeater)e.Item.FindControl("RepeaterSubMenubar");

            foreach (RepeaterItem item2 in RepeaterSubMenubar.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item2.FindControl("CheckBoxPilih");

                if (e.CommandName == "PilihSemua")
                    CheckBoxPilih.Checked = true;
                else if (e.CommandName == "HapusPilihan")
                    CheckBoxPilih.Checked = false;
            }
        }
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}