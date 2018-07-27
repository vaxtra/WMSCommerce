using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_Role : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListGrupPengguna.DataSource = db.TBGrupPenggunas.Where(item => item.IDGrupPengguna > 2).OrderBy(item => item.Nama).ToArray();
                DropDownListGrupPengguna.DataTextField = "Nama";
                DropDownListGrupPengguna.DataValueField = "IDGrupPengguna";
                DropDownListGrupPengguna.DataBind();

                Menubar_Class ClassMenubar = new Menubar_Class(db);

                RepeaterMenu.DataSource = ClassMenubar.Administrator();
                RepeaterMenu.DataBind();

                LoadRole(DropDownListGrupPengguna.SelectedValue.ToInt());
            }
        }
    }

    private void LoadRole(int idGrupPengguna)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBMenubarPenggunaGrup[] menu = db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == idGrupPengguna).ToArray();

            foreach (RepeaterItem item in RepeaterMenu.Items)
            {
                Repeater RepeaterMenuLevel2 = (Repeater)item.FindControl("RepeaterMenuLevel2");
                CheckBox CheckBoxPilihLevel1 = (CheckBox)item.FindControl("CheckBoxPilih");
                HiddenField HiddenFieldIDMenuLevel1 = (HiddenField)item.FindControl("HiddenFieldIDMenu");

                TBMenubar menuLevel1 = db.TBMenubars.FirstOrDefault(lebel1 => lebel1.IDMenubar == HiddenFieldIDMenuLevel1.Value.ToInt());

                if (menuLevel1.TBMenubars.Count > 0)
                {
                    foreach (RepeaterItem item2 in RepeaterMenuLevel2.Items)
                    {
                        Repeater RepeaterMenuLevel3 = (Repeater)item2.FindControl("RepeaterMenuLevel3");
                        CheckBox CheckBoxPilihLevel2 = (CheckBox)item2.FindControl("CheckBoxPilih");
                        HiddenField HiddenFieldIDMenuLevel2 = (HiddenField)item2.FindControl("HiddenFieldIDMenu");

                        TBMenubar menuLevel2 = db.TBMenubars.FirstOrDefault(lebel1 => lebel1.IDMenubar == HiddenFieldIDMenuLevel2.Value.ToInt());

                        if (menuLevel2.TBMenubars.Count > 0)
                        {
                            foreach (RepeaterItem item3 in RepeaterMenuLevel3.Items)
                            {
                                CheckBox CheckBoxPilihLevel3 = (CheckBox)item3.FindControl("CheckBoxPilih");
                                HiddenField HiddenFieldIDMenuLevel3 = (HiddenField)item3.FindControl("HiddenFieldIDMenu");

                                TBMenubarPenggunaGrup cariMenuLevel3 = menu.FirstOrDefault(level3 => level3.IDMenubar == HiddenFieldIDMenuLevel3.Value.ToInt());
                                if (cariMenuLevel3 != null)
                                    CheckBoxPilihLevel3.Checked = true;
                                else
                                    CheckBoxPilihLevel3.Checked = false;
                            }
                        }
                        else
                        {
                            TBMenubarPenggunaGrup cariMenuLevel2 = menu.FirstOrDefault(level2 => level2.IDMenubar == HiddenFieldIDMenuLevel2.Value.ToInt());
                            if (cariMenuLevel2 != null)
                                CheckBoxPilihLevel2.Checked = true;
                            else
                                CheckBoxPilihLevel2.Checked = false;
                        }
                    }
                }
                else
                {
                    TBMenubarPenggunaGrup cariMenuLevel1 = menu.FirstOrDefault(level1 => level1.IDMenubar == HiddenFieldIDMenuLevel1.Value.ToInt());
                    if (cariMenuLevel1 != null)
                        CheckBoxPilihLevel1.Checked = true;
                    else
                        CheckBoxPilihLevel1.Checked = false;
                }
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBMenubarPenggunaGrups.DeleteAllOnSubmit(db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == DropDownListGrupPengguna.SelectedValue.ToInt()));
            db.SubmitChanges();

            int subMenu1;
            int subMenu2;

            foreach (RepeaterItem item in RepeaterMenu.Items)
            {
                subMenu1 = 0;
                Repeater RepeaterMenuLevel2 = (Repeater)item.FindControl("RepeaterMenuLevel2");
                CheckBox CheckBoxPilihLevel1 = (CheckBox)item.FindControl("CheckBoxPilih");
                HiddenField HiddenFieldIDMenuLevel1 = (HiddenField)item.FindControl("HiddenFieldIDMenu");

                TBMenubar menuLevel1 = db.TBMenubars.FirstOrDefault(lebel1 => lebel1.IDMenubar == HiddenFieldIDMenuLevel1.Value.ToInt());

                if (menuLevel1.TBMenubars.Count > 0)
                {
                    foreach (RepeaterItem item2 in RepeaterMenuLevel2.Items)
                    {
                        subMenu2 = 0;
                        Repeater RepeaterMenuLevel3 = (Repeater)item2.FindControl("RepeaterMenuLevel3");
                        CheckBox CheckBoxPilihLevel2 = (CheckBox)item2.FindControl("CheckBoxPilih");
                        HiddenField HiddenFieldIDMenuLevel2 = (HiddenField)item2.FindControl("HiddenFieldIDMenu");

                        TBMenubar menuLevel2 = db.TBMenubars.FirstOrDefault(lebel1 => lebel1.IDMenubar == HiddenFieldIDMenuLevel2.Value.ToInt());

                        if (menuLevel2.TBMenubars.Count > 0)
                        {
                            foreach (RepeaterItem item3 in RepeaterMenuLevel3.Items)
                            {
                                CheckBox CheckBoxPilihLevel3 = (CheckBox)item3.FindControl("CheckBoxPilih");
                                HiddenField HiddenFieldIDMenuLevel3 = (HiddenField)item3.FindControl("HiddenFieldIDMenu");

                                TBMenubar menuLevel3 = db.TBMenubars.FirstOrDefault(lebel1 => lebel1.IDMenubar == HiddenFieldIDMenuLevel3.Value.ToInt());

                                if (CheckBoxPilihLevel3.Checked == true)
                                {
                                    db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel3, IDGrupPengguna = DropDownListGrupPengguna.SelectedValue.ToInt() });
                                    subMenu2++;
                                }
                            }
                        }
                        else
                        {
                            if (CheckBoxPilihLevel2.Checked == true)
                            {
                                db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel2, IDGrupPengguna = DropDownListGrupPengguna.SelectedValue.ToInt() });
                                subMenu1++;
                            }
                        }

                        if (subMenu2 > 0)
                        {
                            db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel2, IDGrupPengguna = DropDownListGrupPengguna.SelectedValue.ToInt() });
                            subMenu1++;
                        }
                    }
                }
                else
                {
                    if (CheckBoxPilihLevel1.Checked == true)
                    {
                        db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel1, IDGrupPengguna = DropDownListGrupPengguna.SelectedValue.ToInt() });
                    }
                }

                if (subMenu1 > 0)
                {
                    db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel1, IDGrupPengguna = DropDownListGrupPengguna.SelectedValue.ToInt() });
                }
            }

            db.SubmitChanges();
        }

        LoadRole(DropDownListGrupPengguna.SelectedValue.ToInt());
    }

    protected void DropDownListGrupPengguna_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListGrupPengguna.SelectedValue != "0")
        {
            LoadRole(DropDownListGrupPengguna.SelectedValue.ToInt());
        }
        else
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Menubar_Class ClassMenubar = new Menubar_Class(db);

                RepeaterMenu.DataSource = ClassMenubar.Administrator();
                RepeaterMenu.DataBind();
            }
        }
    }
}