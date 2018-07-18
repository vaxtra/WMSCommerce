using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BlogDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {

                RepeaterPost.DataSource = db.TBPosts.Where(x => x.IDPage == 2).Select(item => new
                {
                    //ClassPost = item.Urutan == 1 ? "form-group" : "hidden",
                    IDPost=item.IDPost,
                    Judul = item.Judul,
                    Deskripsi=item.Deskripsi,
                    DataSourceDetailPost=item.TBPostDetails.Select(item2 => new {
                        urutan1=item2.Urutan== 1? item2.Konten : item2.Konten,
                        urutan2 = item2.Urutan == 2 ? item2.Nama : item2.Konten

                    })
                    //Konten = item.Konten,
                    //ClassImages = item.Jenis == 2 ? "form-group" : "hidden",
                    //DataSourceImages = item.TBPostDetailImages.Select(item2 => new
                    //{
                    //    DefaultURL = item2.DefaultURL
                    //})
                });
                RepeaterPost.DataBind();

            }
        }
    }

    protected void RepeaterPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        Repeater RepeaterPostDetail = (Repeater)e.Item.FindControl("RepeaterPostDetail");
        HiddenField hiddenID = (HiddenField)e.Item.FindControl("hiddenID");
        Dictionary<string, string> ListImage = new Dictionary<string, string>();
        string stringImage = "";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var PostDetail = db.TBPostDetails.Where(x => x.IDPost == int.Parse(hiddenID.Value));
            foreach (var detail in PostDetail)
            {
                if (detail.Jenis == 2)
                {
                    stringImage = "";
                    foreach (var gambar in detail.TBPostDetailImages)
                    {
                        stringImage += "<li>" + gambar.DefaultURL + "</li>";
                    }
                    ListImage.Add(detail.IDPostDetail.ToString(), stringImage);
                }
            }
            RepeaterPostDetail.DataSource = db.TBPostDetails.Where(item => item.IDPost == int.Parse(hiddenID.Value)).Select(item => new
            {
                Konten = item.Jenis == 2 ? "<ul>" + ListImage["6"] + "</ul>" : item.Konten,
                Urutan=item.Urutan

             
            }).OrderByDescending(item =>item.Urutan);
            RepeaterPostDetail.DataBind();
        }
    }
}