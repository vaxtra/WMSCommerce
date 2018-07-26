using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Menubar_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Menubar_Class ClassMenubar = new Menubar_Class(db);
                    ClassMenubar.EnumMenubarModulDropdownList(DropDownListEnumMenubarModul);
                    DropDownListMenuLevel1.DataSource = ClassMenubar.DataParent().Where(item => item.IDMenubar != Request.QueryString["id"].ToInt());
                    DropDownListMenuLevel1.DataTextField = "Nama";
                    DropDownListMenuLevel1.DataValueField = "IDMenubar";
                    DropDownListMenuLevel1.DataBind();
                    DropDownListMenuLevel1.Items.Insert(0, new ListItem { Value = "0", Text = "- Parent Level 1-" });
                    DropDownListMenuLevel2.Items.Insert(0, new ListItem { Value = "0", Text = "- Parent Level 2-" });
                    var Menubar = ClassMenubar.Cari(Request.QueryString["id"].ToInt());

                    if (Menubar != null)
                    {
                        if (Menubar.LevelMenu == 2)
                        {
                            DropDownListMenuLevel1.SelectedValue = Menubar.IDMenubarParent.ToString();
                            PanelIconVisible();
                        }
                        else if (Menubar.LevelMenu == 3)
                        {
                            DropDownListMenuLevel1.SelectedValue = Menubar.TBMenubar1.IDMenubarParent.ToString();
                            PanelIconVisible();
                            DropDownListMenuLevel2.SelectedValue = Menubar.IDMenubarParent.ToString();

                        }

                        DropDownListEnumMenubarModul.SelectedValue = Menubar.EnumMenubarModul.ToString();
                        TextBoxUrutan.Text = Menubar.Urutan.ToString();
                        TextBoxKode.Text = Menubar.Kode;
                        TextBoxNama.Text = Menubar.Nama;
                        TextBoxUrl.Text = Menubar.Url;
                        TextBoxIcon.Text = Menubar.Icon;

                        LabelKeterangan.Text = "Ubah";
                        ButtonSimpan.Text = "Ubah";
                    }
                    else
                    {
                        LabelKeterangan.Text = "Tambah";
                        ButtonSimpan.Text = "Tambah";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Menubar_Class ClassMenubar = new Menubar_Class(db);

                if (ButtonSimpan.Text == "Tambah")
                {
                    if (DropDownListMenuLevel1.SelectedValue == "0")
                    {
                        ClassMenubar.Tambah(DropDownListMenuLevel1.SelectedValue.ToInt(), (EnumMenubarModul)DropDownListEnumMenubarModul.SelectedValue.ToInt(), TextBoxUrutan.Text.ToInt(), TextBoxKode.Text, TextBoxNama.Text, TextBoxUrl.Text, TextBoxIcon.Text, 1);
                    }
                    else
                    {
                        if (DropDownListMenuLevel2.SelectedValue == "0")
                        {
                            ClassMenubar.Tambah(DropDownListMenuLevel1.SelectedValue.ToInt(), (EnumMenubarModul)DropDownListEnumMenubarModul.SelectedValue.ToInt(), TextBoxUrutan.Text.ToInt(), TextBoxKode.Text, TextBoxNama.Text, TextBoxUrl.Text, TextBoxIcon.Text, 2);
                        }
                        else
                        {
                            ClassMenubar.Tambah(DropDownListMenuLevel2.SelectedValue.ToInt(), (EnumMenubarModul)DropDownListEnumMenubarModul.SelectedValue.ToInt(), TextBoxUrutan.Text.ToInt(), TextBoxKode.Text, TextBoxNama.Text, TextBoxUrl.Text, TextBoxIcon.Text, 3);

                        }
                    }

                    db.TBMenubarPenggunaGrups.DeleteAllOnSubmit(db.TBMenubarPenggunaGrups.Where(item => item.IDGrupPengguna == 1));

                    foreach (var item in db.TBMenubars)
                    {
                        db.TBMenubarPenggunaGrups.InsertOnSubmit(new TBMenubarPenggunaGrup() { IDGrupPengguna = 1, IDMenubar = item.IDMenubar });
                    }
                }
                else if (ButtonSimpan.Text == "Ubah")
                {
                    if (DropDownListMenuLevel1.SelectedValue == "0")
                    {
                        ClassMenubar.Ubah(Request.QueryString["id"].ToInt(), DropDownListMenuLevel1.SelectedValue.ToInt(), (EnumMenubarModul)DropDownListEnumMenubarModul.SelectedValue.ToInt(), TextBoxUrutan.Text.ToInt(), TextBoxKode.Text, TextBoxNama.Text, TextBoxUrl.Text, TextBoxIcon.Text, 1);
                    }
                    else
                    {

                        if (DropDownListMenuLevel2.SelectedValue == "0")
                        {
                            ClassMenubar.Ubah(Request.QueryString["id"].ToInt(), DropDownListMenuLevel1.SelectedValue.ToInt(), (EnumMenubarModul)DropDownListEnumMenubarModul.SelectedValue.ToInt(), TextBoxUrutan.Text.ToInt(), TextBoxKode.Text, TextBoxNama.Text, TextBoxUrl.Text, TextBoxIcon.Text, 2);
                        }
                        else
                        {
                            ClassMenubar.Ubah(Request.QueryString["id"].ToInt(), DropDownListMenuLevel2.SelectedValue.ToInt(), (EnumMenubarModul)DropDownListEnumMenubarModul.SelectedValue.ToInt(), TextBoxUrutan.Text.ToInt(), TextBoxKode.Text, TextBoxNama.Text, TextBoxUrl.Text, TextBoxIcon.Text, 3);
                        }
                    }
                }

                db.SubmitChanges();

                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default.aspx");
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void DropDownListMenubarParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PanelIconVisible();
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    private void PanelIconVisible()
    {
        if (DropDownListMenuLevel1.SelectedValue == "0") //SEBAGAI PARENT
        {
            PanelIcon.Visible = true;
            TextBoxIcon.Text = "";

            DropDownListMenuLevel2.Items.Clear();
            DropDownListMenuLevel2.Items.Insert(0, new ListItem { Value = "0", Text = "- Parent Level 2-" });

            PanelLevel2.Visible = false;
        }
        else //SEBAGAI CHILD
        {
            PanelIcon.Visible = false;
            TextBoxIcon.Text = "";

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListMenuLevel2.DataSource = db.TBMenubars.Where(item => item.IDMenubarParent == DropDownListMenuLevel1.SelectedValue.ToInt() && item.IDMenubar != Request.QueryString["id"].ToInt());
                DropDownListMenuLevel2.DataTextField = "Nama";
                DropDownListMenuLevel2.DataValueField = "IDMenubar";
                DropDownListMenuLevel2.DataBind();
                DropDownListMenuLevel2.Items.Insert(0, new ListItem { Value = "0", Text = "- Parent Level 2-" });
            }

            PanelLevel2.Visible = true;
        }
    }
}