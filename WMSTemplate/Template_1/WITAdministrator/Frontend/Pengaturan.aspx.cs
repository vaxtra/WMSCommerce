using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Frontend_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                MultiViewPage.SetActiveView(ViewPost);

                //PageTemplate_Class PageTemplate_Class = new PageTemplate_Class(db);
                //PageTemplate_Class.DropDownList(DropDownListTemplate, "-Pilih Template-");

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    LabelKeterangan.Text = "Tambah";
                }
                else
                {
                    TBPost Post = db.TBPosts.FirstOrDefault(item => item.IDPost == Request.QueryString["id"].ToInt());

                    TextBoxJudul.Text = Post.Judul;
                    DropDownListAlign.SelectedValue = Post.Align.ToString();
                    TextBoxDeskripsi.Text = Post.Deskripsi;

                    DivDetail.Visible = true;

                    LoadPost();

                    LabelKeterangan.Text = "Ubah";
                }
            }
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    #region POST
    private void LoadPost()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterPostDetail.DataSource = db.TBPostDetails.Where(item => item.IDPost == Request.QueryString["id"].ToInt()).Select(item => new
            {
                item.IDPostDetail,
                Urutan = item.Urutan,
                Pegawai = item.TBPengguna.NamaLengkap,
                Tanggal = item.Tanggal,
                Nama = item.Nama,
                Jenis = item.Jenis,
                JenisBadge = Manage.HTMLJenisPostDetail(item.Jenis),
                Konten = item.Konten,
                Images = item.TBPostDetailImages
            }).OrderBy(item => item.Urutan);
            RepeaterPostDetail.DataBind();
        }
    }

    protected void RepeaterPostDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                MultiViewPage.SetActiveView(ViewPostDetail);

                HiddenFieldIDPostDetail.Value = e.CommandArgument.ToString();
                PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
                TBPostDetail PostDetail = PostDetail_Class.GetData(HiddenFieldIDPostDetail.Value.ToInt());
                TextBoxDetailNama.Text = PostDetail.Nama;
                DropDownListDetailJenis.SelectedValue = PostDetail.Jenis.ToString();
                TextBoxDetailKonten.Text = PostDetail.Konten;

                if (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.Text)
                {
                    DivKonten.Visible = true;
                    DivSingleImage.Visible = false;
                    DivMultipleImage.Visible = false;
                }
                else if (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.SingleImage)
                {
                    DivKonten.Visible = false;
                    DivSingleImage.Visible = true;
                    DivMultipleImage.Visible = false;

                    LoadDataFoto();
                }
                else
                {
                    DivKonten.Visible = false;
                    DivSingleImage.Visible = false;
                    DivMultipleImage.Visible = true;

                    LoadDataFoto();
                }
                DropDownListDetailJenis.Enabled = false;
            }
        }
        else
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
                TBPostDetail PostDetail = PostDetail_Class.GetData(e.CommandArgument.ToInt());

                if (PostDetail.Jenis == (int)EnumJenisPostDetail.Text)
                {
                    PostDetail_Class.DeleteData(PostDetail);
                }
                else
                {
                    PostDetailImage_Class PostDetailImage_Class = new PostDetailImage_Class(db);
                    TBPostDetailImage[] ListPostDetailImage = PostDetailImage_Class.GetAll().Where(item => item.IDPostDetail == e.CommandArgument.ToInt()).ToArray();

                    foreach (var item in ListPostDetailImage)
                    {
                        FileInfo file = new FileInfo(Server.MapPath("/images/PostDetail/") + (item.IDPostDetail + "-" + item.IDPostDetailImage) + ".jpg");
                        if (file.Exists)
                            file.Delete();
                    }

                    PostDetailImage_Class.DeleteAllData(ListPostDetailImage);
                    PostDetail_Class.DeleteData(e.CommandArgument.ToInt());
                }

                db.SubmitChanges();
            }

            LoadPost();
        }
    }

    protected void ButtonTambahPostDetail_Click(object sender, EventArgs e)
    {
        MultiViewPage.SetActiveView(ViewPostDetail);

        DivKonten.Visible = true;
        DivSingleImage.Visible = false;
        DivMultipleImage.Visible = false;

        HiddenFieldIDPostDetail.Value = string.Empty;
        TextBoxDetailNama.Text = string.Empty;
        DropDownListDetailJenis.Enabled = true;
        DropDownListDetailJenis.SelectedValue = "1";
        TextBoxDetailKonten.Text = string.Empty;

        ButtonUploadMultipleImage.Text = "Tambah";
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            Page_Class Page_Class = new Page_Class(db);
            TBPage Halaman = Page_Class.GetData(Request.QueryString["idPage"].ToInt());

            Post_Class Post_Class = new Post_Class(db);
            TBPost Post = Post_Class.GetData(Request.QueryString["id"].ToInt());
            if (Post == null)
            {
                Post = Post_Class.InsertData(Request.QueryString["idPage"].ToInt(), PenggunaLogin.IDPengguna, Halaman.TBPosts.Count + 1, DateTime.Now, TextBoxJudul.Text, TextBoxDeskripsi.Text, DropDownListAlign.SelectedValue, TextBoxTags.Text);
            }
            else
            {
                Post.IDPengguna = PenggunaLogin.IDPengguna;
                Post.Urutan = Post.Urutan;
                Post.Tanggal = DateTime.Now;
                Post.Judul = TextBoxJudul.Text;
                Post.Deskripsi = TextBoxDeskripsi.Text;
                Post.Align = DropDownListAlign.SelectedValue;
                Post.Tags = TextBoxTags.Text;
            }

            db.SubmitChanges();

            Response.Redirect("Pengaturan.aspx?idPage=" + Request.QueryString["idPage"] + "&id=" + Post.IDPost);
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    #endregion

    #region POST DETAIL
    protected void DropDownListDetailJenis_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.Text)
        {
            DivKonten.Visible = true;
            DivSingleImage.Visible = false;
            DivMultipleImage.Visible = false;
        }
        else if (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.SingleImage)
        {
            DivKonten.Visible = false;
            DivSingleImage.Visible = true;
            DivMultipleImage.Visible = false;
        }
        else
        {
            DivKonten.Visible = false;
            DivSingleImage.Visible = false;
            DivMultipleImage.Visible = true;
        }
    }

    protected void ButtonDetailSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            Post_Class Post_Class = new Post_Class(db);
            TBPost Post = Post_Class.GetData(Request.QueryString["id"].ToInt());

            PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
            if (string.IsNullOrEmpty(HiddenFieldIDPostDetail.Value))
            {
                PostDetail_Class.InsertData(Post.IDPost, PenggunaLogin.IDPengguna, Post.TBPostDetails.Count + 1, DateTime.Now, TextBoxDetailNama.Text, DropDownListDetailJenis.SelectedValue.ToInt(), (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.Text ? TextBoxDetailKonten.Text : null));
            }
            else
            {
                TBPostDetail PostDetail = PostDetail_Class.GetData(HiddenFieldIDPostDetail.Value.ToInt());
                PostDetail.Nama = TextBoxDetailNama.Text;
                if (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.Text)
                {
                    PostDetail.Konten = HttpUtility.HtmlDecode(TextBoxDetailKonten.Text);
                }
            }
            db.SubmitChanges();

            MultiViewPage.SetActiveView(ViewPost);

            LoadPost();
        }
    }

    protected void ButtonDetailKembali_Click(object sender, EventArgs e)
    {
        MultiViewPage.SetActiveView(ViewPost);

        LoadPost();
    }

    #region SINGLE IMAGE
    protected void ButtonUploadSingleImage_Click(object sender, EventArgs e)
    {
        if (FileUploadSingleImage.HasFile)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

                string Folder = Server.MapPath("/images/PostDetail/");

                if (!Directory.Exists(Folder))
                    Directory.CreateDirectory(Folder);

                Post_Class Post_Class = new Post_Class(db);
                TBPost Post = Post_Class.GetData(Request.QueryString["id"].ToInt());

                PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
                TBPostDetail PostDetail = null;
                if (string.IsNullOrEmpty(HiddenFieldIDPostDetail.Value))
                {
                    PostDetail = PostDetail_Class.InsertData(Post.IDPost, PenggunaLogin.IDPengguna, Post.TBPostDetails.Count + 1, DateTime.Now, TextBoxDetailNama.Text, DropDownListDetailJenis.SelectedValue.ToInt(), (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.Text ? TextBoxDetailKonten.Text : null));
                    db.SubmitChanges();
                }
                else
                {
                    PostDetail = PostDetail_Class.GetData(HiddenFieldIDPostDetail.Value.ToInt());
                }

                PostDetailImage_Class PostDetailImage_Class = new PostDetailImage_Class(db);
                TBPostDetailImage PostDetailImage = PostDetail.TBPostDetailImages.FirstOrDefault();
                if (PostDetailImage == null)
                {
                    PostDetailImage = PostDetailImage_Class.InsertData(PostDetail.IDPostDetail, 0, string.Empty, null, null, null, null);
                    db.SubmitChanges();
                }

                FileUploadSingleImage.SaveAs(Folder + (PostDetailImage.IDPostDetail + "-" + PostDetailImage.IDPostDetailImage) + ".jpg");

                PostDetailImage.DefaultURL = "/images/PostDetail/" + (PostDetailImage.IDPostDetail + "-" + PostDetailImage.IDPostDetailImage) + ".jpg";

                db.SubmitChanges();

                HiddenFieldIDPostDetail.Value = PostDetail.IDPostDetail.ToString();
                DropDownListDetailJenis.Enabled = false;
            }

            LoadDataFoto();
        }
    }

    #endregion

    #region MULTIPLE IMAGE
    protected void RepeaterImageMultiple_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PostDetailImage_Class PostDetailImage_Class = new PostDetailImage_Class(db);
            TBPostDetailImage PostDetailImage = PostDetailImage_Class.GetData(e.CommandArgument.ToInt());

            if (e.CommandName == "Hapus")
            {
                FileInfo file = new FileInfo(Server.MapPath("/images/PostDetail/") + (PostDetailImage.IDPostDetail + "-" + PostDetailImage.IDPostDetailImage) + ".jpg");
                if (file.Exists)
                    file.Delete();

                PostDetailImage_Class.DeleteData(PostDetailImage);
            }
            else
            {
                HiddenFieldPostDetailImage.Value = e.CommandArgument.ToString();
                TextBoxImageJudul.Text = PostDetailImage.Judul;
                TextBoxImageLink.Text = PostDetailImage.Link;
                TextBoxImageAlt.Text = PostDetailImage.Alt;
                TextBoxImageDeskripsi.Text = PostDetailImage.Deskripsi;
                TextBoxImageJudul.Focus();

                ButtonUploadMultipleImage.Text = "Ubah";
            }

            db.SubmitChanges();
            LoadDataFoto();
        }
    }
    protected void ButtonUploadMultipleImage_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            string Folder = Server.MapPath("/images/PostDetail/");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            Post_Class Post_Class = new Post_Class(db);
            TBPost Post = Post_Class.GetData(Request.QueryString["id"].ToInt());

            PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
            TBPostDetail PostDetail = null;
            if (string.IsNullOrEmpty(HiddenFieldIDPostDetail.Value))
            {
                PostDetail = PostDetail_Class.InsertData(Post.IDPost, PenggunaLogin.IDPengguna, Post.TBPostDetails.Count + 1, DateTime.Now, TextBoxDetailNama.Text, DropDownListDetailJenis.SelectedValue.ToInt(), (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.Text ? TextBoxDetailKonten.Text : null));
                db.SubmitChanges();
            }
            else
            {
                PostDetail = PostDetail_Class.GetData(HiddenFieldIDPostDetail.Value.ToInt());
            }

            PostDetailImage_Class PostDetailImage_Class = new PostDetailImage_Class(db);
            TBPostDetailImage PostDetailImage = null;

            if (ButtonUploadMultipleImage.Text == "Tambah")
            {
                PostDetailImage = PostDetailImage_Class.InsertData(PostDetail.IDPostDetail, PostDetail.TBPostDetailImages.Count + 1, string.Empty, TextBoxImageJudul.Text, HttpUtility.HtmlDecode(TextBoxImageDeskripsi.Text), TextBoxImageLink.Text, TextBoxImageAlt.Text);
            }
            else
            {
                PostDetailImage = PostDetailImage_Class.GetData(HiddenFieldPostDetailImage.Value.ToInt());
                PostDetailImage.Judul = TextBoxImageJudul.Text;
                PostDetailImage.Deskripsi = HttpUtility.HtmlDecode(TextBoxImageDeskripsi.Text);
                PostDetailImage.Link = TextBoxImageLink.Text;
                PostDetailImage.Alt = TextBoxImageAlt.Text;
            }

            db.SubmitChanges();

            if (FileUploadMultipleImage.HasFile)
            {
                FileUploadMultipleImage.SaveAs(Folder + (PostDetailImage.IDPostDetail + "-" + PostDetailImage.IDPostDetailImage) + ".jpg");
            }

            PostDetailImage.DefaultURL = "/images/PostDetail/" + (PostDetailImage.IDPostDetail + "-" + PostDetailImage.IDPostDetailImage) + ".jpg";

            db.SubmitChanges();

            HiddenFieldIDPostDetail.Value = PostDetail.IDPostDetail.ToString();
            HiddenFieldPostDetailImage.Value = string.Empty;
            DropDownListDetailJenis.Enabled = false;
            TextBoxImageJudul.Text = string.Empty;
            TextBoxImageLink.Text = string.Empty;
            TextBoxImageAlt.Text = string.Empty;
            TextBoxImageDeskripsi.Text = string.Empty;

            ButtonUploadMultipleImage.Text = "Tambah";
        }

        LoadDataFoto();
    }
    private void LoadDataFoto()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PostDetail_Class PostDetail_Class = new PostDetail_Class(db);
            TBPostDetail PostDetail = PostDetail_Class.GetData(HiddenFieldIDPostDetail.Value.ToInt());
            if (DropDownListDetailJenis.SelectedValue.ToInt() == (int)EnumJenisPostDetail.SingleImage)
            {
                ImagePhotoProfile.ImageUrl = PostDetail.TBPostDetailImages.FirstOrDefault().DefaultURL + "?w=250";
            }
            else
            {
                RepeaterImageMultiple.DataSource = PostDetail.TBPostDetailImages
                .Select(item => new
                {
                    item.IDPostDetailImage,
                    item.Urutan,
                    item.DefaultURL,
                    item.Judul,
                    item.Link,
                    item.Alt,
                    item.Deskripsi
                }).OrderBy(item => item.Urutan);
                RepeaterImageMultiple.DataBind();
            }
        }
    }
    #endregion

    #endregion
}