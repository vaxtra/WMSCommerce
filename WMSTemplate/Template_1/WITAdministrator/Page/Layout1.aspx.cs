using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Page_Layout1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Page_Class Page_Class = new Page_Class(db);
                Page_Class.DropDownList(DropDownListPage, "-Pilih Page-");

                DivAlertSwap.Attributes.Add("class", "alert alert-info");
            }
        }
    }

    private void LoadPost()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListPage.SelectedValue != "0")
            {
                var ListPost = db.TBPosts.Where(item => item.IDPage == DropDownListPage.SelectedValue.ToInt()).Select(item => new
                {
                    Urutan = item.Urutan,
                    Post = item.Judul,
                    Align = item.Align,
                    Deskripsi = item.Deskripsi,
                    Count = item.TBPostDetails.Count,
                    Body = item.TBPostDetails.Select(item2 => new
                    {
                        item2.IDPostDetail,
                        ClassButton = Manage.HTMLJenisPostDetailButton(item2.Jenis),
                        Urutan = item2.Urutan,
                        Pegawai = item2.TBPengguna.NamaLengkap,
                        Tanggal = item2.Tanggal,
                        Nama = item2.Nama,
                        Jenis = item2.Jenis,
                        JenisBadge = Manage.HTMLJenisPostDetail(item2.Jenis),
                        Konten = item2.Konten,
                        Images = item2.TBPostDetailImages
                    }).OrderBy(item2 => item2.Urutan)

                }).OrderBy(item => item.Urutan);
                RepeaterPage.DataSource = ListPost;
                RepeaterPage.DataBind();

                RepeateraLayout.DataSource = ListPost;
                RepeateraLayout.DataBind();

                DivAlertSwap.Attributes.Add("class", "alert alert-primary");
                LabelAlert.Text = "Pilih post yang akan di swap";
            }
            else
            {
                RepeaterPage.DataSource = null;
                RepeaterPage.DataBind();

                RepeateraLayout.DataSource = null;
                RepeateraLayout.DataBind();

                DivAlertSwap.Attributes.Add("class", "alert alert-info");
                LabelAlert.Text = "Pilih page layout";
            }
        }
    }

    protected void DropDownListPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPost();
    }

    protected void RepeaterBody_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (string.IsNullOrEmpty(HiddenFieldIDPostDetail.Value))
        {
            HiddenFieldIDPostDetail.Value = e.CommandArgument.ToString();
            DivAlertSwap.Attributes.Add("class", "alert alert-warning");
            LabelAlert.Text = "Pilih post yang akan di swap";
        }
        else
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                int urutan = 0, idPost = 0;
                PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
                TBPostDetail PostDetailSource = PostDetail_Class.GetData(HiddenFieldIDPostDetail.Value.ToInt());
                TBPostDetail PostDetailTarget = PostDetail_Class.GetData(e.CommandArgument.ToInt());

                //GANTI ID POST
                idPost = PostDetailSource.IDPost;
                PostDetailSource.IDPost = PostDetailTarget.IDPost;
                PostDetailTarget.IDPost = idPost;

                //GANTI URUTAN
                urutan = PostDetailSource.Urutan;
                PostDetailSource.Urutan = PostDetailTarget.Urutan;
                PostDetailTarget.Urutan = urutan;

                db.SubmitChanges();

                LoadPost();
            }
            HiddenFieldIDPostDetail.Value = string.Empty;
            DivAlertSwap.Attributes.Add("class", "alert alert-primary");
            LabelAlert.Text = "Pilih post yang akan di swap";
        }
    }
}