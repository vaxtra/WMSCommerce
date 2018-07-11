using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Menu_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadData();

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    db.TBMenubarPenggunaGrups.DeleteAllOnSubmit(db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == 1));
                    db.SubmitChanges();
                    foreach (var item in db.TBMenubars)
                    {
                        db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup { IDMenubar = item.IDMenubar, IDGrupPengguna = 1 });
                    }
                    db.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Menubar_Class ClassMenubar = new Menubar_Class(db);

            RepeaterMenu.DataSource = ClassMenubar.Data();
            RepeaterMenu.DataBind();

            RepeaterMenuDefault.DataSource = ClassMenubar.Data();
            RepeaterMenuDefault.DataBind();

            LoadRole();
        }
    }

    protected void RepeaterMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Ubah")
                Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument.ToString());
            else if (e.CommandName == "Hapus")
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Menubar_Class ClassMenubar = new Menubar_Class(db);

                    ClassMenubar.Hapus(e.CommandArgument.ToInt());
                }

                LoadData();
            }
            else if (e.CommandName == "Urutkan")
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Menubar_Class ClassMenubar = new Menubar_Class(db);

                    TextBox TextBoxUrutan = (TextBox)e.Item.FindControl("TextBoxUrutan");

                    ClassMenubar.PengaturanUrutan(e.CommandArgument.ToInt(), TextBoxUrutan.Text.ToInt());

                    db.SubmitChanges();
                }

                LoadData();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    private void LoadRole()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBMenubarPenggunaGrup[] menu = db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == 2).ToArray();

            foreach (RepeaterItem item in RepeaterMenuDefault.Items)
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
            db.TBMenubarPenggunaGrups.DeleteAllOnSubmit(db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == 2));
            db.SubmitChanges();

            int subMenu1;
            int subMenu2;

            foreach (RepeaterItem item in RepeaterMenuDefault.Items)
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
                                    db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel3, IDGrupPengguna = 2 });
                                    subMenu2++;
                                }
                            }
                        }
                        else
                        {
                            if (CheckBoxPilihLevel2.Checked == true)
                            {
                                db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel2, IDGrupPengguna = 2 });
                                subMenu1++;
                            }
                        }

                        if (subMenu2 > 0)
                        {
                            db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel2, IDGrupPengguna = 2 });
                            subMenu1++;
                        }
                    }
                }
                else
                {
                    if (CheckBoxPilihLevel1.Checked == true)
                    {
                        db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel1, IDGrupPengguna = 2 });
                    }
                }

                if (subMenu1 > 0)
                {
                    db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { TBMenubar = menuLevel1, IDGrupPengguna = 2 });
                }
            }

            db.SubmitChanges();
        }

        LoadRole();
    }
}