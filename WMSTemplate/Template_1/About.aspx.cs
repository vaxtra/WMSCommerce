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
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterPost.DataSource = db.TBPosts.Where(item => item.IDPost == 6).Select(item => new
            {
                Judul = item.Judul,
                DataSourcePostDetail = item.TBPostDetails.Select(item2 => new
                {
                    Urutan = item2.Urutan,
                    ClassSingleImage = item2.Jenis == 2 ? "row" : "hidden",
                    ClassTeks = item2.Jenis == 1 ? "entry-content" : "hidden",
                    ClassSlider = item2.Jenis == 3 ? "row" : "hidden",
                    ImgaeSingleDefaultURL = item2.Jenis == 2 ? item2.TBPostDetailImages.FirstOrDefault().DefaultURL : string.Empty,
                    Konten = item2.Jenis == 1 ? item2.Konten : string.Empty,
                    DataSourcePostDetailImage = item2.Jenis == 3 ? item2.TBPostDetailImages.Select(item3 => new { ImgaeSliderDefaultURL = item3.DefaultURL }) : null
                }).OrderBy(item2 => item2.Urutan)
            });
            RepeaterPost.DataBind();
        }
    }
}