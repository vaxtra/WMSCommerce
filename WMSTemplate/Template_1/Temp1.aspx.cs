using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Produk = db.TBProduks.Select(item => new
                {
                    item.IDProduk,
                    item.Nama,
                    Foto = "/images/cover/" + item.IDProduk + ".jpg",
                    Harga = item.TBKombinasiProduks.FirstOrDefault().TBStokProduks.FirstOrDefault(item2 => item2.IDTempat == 1).HargaJual
                }).ToArray();

                RepeaterProduk.DataSource = Produk;
                RepeaterProduk.DataBind();

                //BARBAR
                //////var cariPost = db.TBPosts.Where(x => x.IDPage == 1).Select(item => new
                //////{
                //////    id = item.IDPost,
                //////    judul=item.Judul,
                //////    deskripsi=item.Deskripsi,



                //////    //picture=item.TBPostDetails.Where(item.IDPost
                //////}).ToArray();
                //////var cariPostDetail=db.TBPostDetails.Select(items=>new {
                //////    idPost=items.IDPost,
                //////}).Where(y=>y.idPost==cariPost.)
                RepeaterPost.DataSource = db.TBPostDetails.Where(x => x.TBPost.IDPage == 1).Select(item => new
                {
                    ClassPost = item.Jenis == 1 ? "form-group" : "hidden",
                    Nama = item.Nama,
                    Konten = item.Konten,
                    ClassImages = item.Jenis == 2 ? "form-group" : "hidden",
                    DataSourceImages = item.TBPostDetailImages.Select(item2 => new
                    {
                        DefaultURL = item2.DefaultURL
                    })
                });
                RepeaterPost.DataBind();

            }
        }
    }
}