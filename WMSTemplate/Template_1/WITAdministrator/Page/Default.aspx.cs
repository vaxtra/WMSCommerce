using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RendFramework;

public partial class WITAdministrator_Page_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PageTemplate_Class PageTemplate_Class = new PageTemplate_Class(db);
                PageTemplate_Class.DropDownList(DropDownListPageTemplate, string.Empty);

                LoadData(db);
            }
        }
    }

    private void LoadData(DataClassesDatabaseDataContext db)
    {
        RepeaterPage.DataSource = db.TBPages
            .Select(item => new
            {
                IDPage = item.IDPage,
                Nama = item.Nama,
                Template = item.TBPageTemplate.Nama,
                Count = item.TBPosts.Count(),
                Body = item.TBPosts.Select(item2 => new
                {
                    IDPage = item2.IDPage,
                    IDPost = item2.IDPost,
                    Judul = item2.Judul,
                    Deskripsi = item2.Deskripsi,
                    PostDetail = GetAllPostDetail(item2),
                    Urutan = item2.Urutan,
                    VisibleHapus = item2.TBPostDetails.Count == 0 ? true : false
                }).OrderBy(item2 => item2.Urutan),
            })
            .OrderByDescending(item => item.Nama)
            .ToArray();

        RepeaterPage.DataBind();
    }

    private string GetAllPostDetail(TBPost Post)
    {
        string hasil = string.Empty;
        bool awal = true;
        foreach (var item in Post.TBPostDetails)
        {
            if (awal)
            {
                hasil += item.Nama;
                awal = false;
            }
            else
                hasil += ", " + item.Nama;
        }
        return hasil;
    }
    protected void RepeaterProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "UbahStatus")
            {
                Produk_Class ClassProduk = new Produk_Class(db);

                ClassProduk.UbahStatus(e.CommandArgument.ToInt());

                db.SubmitChanges();

                LoadData(db);
            }
        }
    }

    protected void ButtonPageSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (ButtonPageSimpan.Text == "Tambah")
                    db.TBPages.InsertOnSubmit(new TBPage { IDPageTemplate = DropDownListPageTemplate.SelectedValue.ToInt(), Nama = TextBoxPageNama.Text, Deskripsi = null });
                else if (ButtonPageSimpan.Text == "Ubah")
                {
                    TBPage data = db.TBPages.FirstOrDefault(item => item.IDPage == HiddenFieldIDPage.Value.ToInt());
                    data.IDPageTemplate = DropDownListPageTemplate.SelectedValue.ToInt();
                    data.Nama = TextBoxPageNama.Text;
                    data.Deskripsi = null;
                }
                db.SubmitChanges();

                HiddenFieldIDPage.Value = null;
                DropDownListPageTemplate.SelectedIndex = 0;
                TextBoxPageNama.Text = string.Empty;
                ButtonPageSimpan.Text = "Tambah";

                LoadData(db);
            }
        }
    }

    protected void RepeaterPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {
                TBPage Page = db.TBPages.FirstOrDefault(item => item.IDPage == e.CommandArgument.ToInt());
                HiddenFieldIDPage.Value = Page.IDPage.ToString();
                DropDownListPageTemplate.SelectedValue = Page.IDPageTemplate.ToString();
                TextBoxPageNama.Text = Page.Nama;
                TextBoxPageNama.Focus();

                ButtonPageSimpan.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                Page_Class Page_Class = new Page_Class(db);
                Page_Class.DeleteData(e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadData(db);
            }
        }
    }

    protected void RepeaterBody_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Post_Class Post_Class = new Post_Class(db);
                Post_Class.DeleteData(e.CommandArgument.ToInt());
                db.SubmitChanges();

                LoadData(db);
            }
        }
    }
}