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
                    Kategori = item.TBProdukKategori.Nama,
                    Foto = "/images/cover/" + item.IDProduk + ".jpg",
                    Deskripsi = item.DeskripsiSingkat,
                    Harga = item.TBKombinasiProduks.FirstOrDefault().TBStokProduks.FirstOrDefault(item2 => item2.IDTempat == 1).HargaJual
                }).ToArray().Take(5);

                RepeaterProduk.DataSource = Produk;
                RepeaterProduk.DataBind();
                RepeaterImage.DataSource = db.TBPostDetailImages.Where(x => x.TBPostDetail.IDPostDetail == 1 ).Select(item => new
                {
                    DefaultURL = item.DefaultURL,
                    Judul = item.Judul,
                    Deskripsi = item.Deskripsi,
                    Jenis=item.TBPostDetail.Jenis
                }).Where(y=> y.Jenis == (int)EnumJenisPostDetail.ImageSlider);
                RepeaterImage.DataBind();

                RepeaterPostHome.DataSource = db.TBPosts.Where(item => item.IDPage == 2).Select(item => new
                {
                    ss=item.IDPost,
                    Judul = item.Judul,
                    //Gambar=item.TBPostDetails.Where(x=>x.IDPost==item.IDPost ).Where(x=>x.Jenis == (int)EnumJenisPostDetail.SingleImage).FirstOrDefault().
                    DataSourcePostDetail = item.TBPostDetails.Select(item2 => new
                    {
                        Urutan = item2.Urutan,
                        ClassSingleImage = item2.Jenis == (int)EnumJenisPostDetail.SingleImage ? "b-advantages-1" : "hidden",
                        //ClassTeks = item2.Jenis == 1 ? "entry-content" : "hidden",
                        //ClassSlider = item2.Jenis == 3 ? "row" : "hidden",
                        ImgaeSingleDefaultURL = item2.Jenis == (int)EnumJenisPostDetail.SingleImage ? item2.TBPostDetailImages.FirstOrDefault().DefaultURL : string.Empty,
                        //Konten = item2.Jenis == 1 ? item2.Konten : string.Empty,
                        //DataSourcePostDetailImage = item2.Jenis == 3 ? item2.TBPostDetailImages.Select(item3 => new { ImgaeSliderDefaultURL = item3.DefaultURL }) : null
                    }).OrderBy(item2 => item2.Urutan).Take(1)
                });
                RepeaterPostHome.DataBind();
          

            }
        }
    }
}