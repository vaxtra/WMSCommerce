using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Rekomendasi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (Request.QueryString["jenis"] == "kategori")
                {
                    LabelJudulRekomendasi.Text = "Kategori";
                    LabelJenisRekomendasi.Text = "Kategori";

                    RepeaterKategori.DataSource = db.TBKategoriProduks
                        .Where(item => item.TBRekomendasiKategoriProduks.Count > 0 && item.TBRekomendasiKategoriProduks1.Count > 0)
                        .Select(item => new
                        {
                            Nama = item.Nama,
                            Jumlah = (item.TBRekomendasiKategoriProduks.Count > 0 ? item.TBRekomendasiKategoriProduks.Sum(item2 => item2.Jumlah) : 0) + (item.TBRekomendasiKategoriProduks1.Count > 0 ? item.TBRekomendasiKategoriProduks1.Sum(item2 => item2.Jumlah) : 0),
                            Nilai = (item.TBRekomendasiKategoriProduks.Count > 0 ? item.TBRekomendasiKategoriProduks.Sum(item2 => item2.Nilai) : 0) + (item.TBRekomendasiKategoriProduks1.Count > 0 ? item.TBRekomendasiKategoriProduks1.Sum(item2 => item2.Nilai) : 0),
                            Rekomendasi = GabungRekomendasi(item.TBRekomendasiKategoriProduks.ToList(), item.TBRekomendasiKategoriProduks1.ToList())
                        })
                        .OrderByDescending(item => item.Jumlah)
                        .ToArray();
                    RepeaterKategori.DataBind();
                }
                else
                {
                    LabelJudulRekomendasi.Text = "Produk";
                    LabelJenisRekomendasi.Text = "Produk";

                    RepeaterProduk.DataSource = db.TBProduks
                        .Where(item => item.TBRekomendasiProduks.Count > 0 || item.TBRekomendasiProduks1.Count > 0)
                        .Select(item => new
                        {
                            Nama = item.Nama,
                            Jumlah = (item.TBRekomendasiProduks.Count > 0 ? item.TBRekomendasiProduks.Sum(item2 => item2.Jumlah) : 0) + (item.TBRekomendasiProduks1.Count > 0 ? item.TBRekomendasiProduks1.Sum(item2 => item2.Jumlah) : 0),
                            Nilai = (item.TBRekomendasiProduks.Count > 0 ? item.TBRekomendasiProduks.Sum(item2 => item2.Nilai) : 0) + (item.TBRekomendasiProduks1.Count > 0 ? item.TBRekomendasiProduks1.Sum(item2 => item2.Nilai) : 0),
                            Rekomendasi = GabungRekomendasi(item.TBRekomendasiProduks.ToList(), item.TBRekomendasiProduks1.ToList())
                        })
                        .OrderByDescending(item => item.Jumlah)
                        .ToArray();
                    RepeaterProduk.DataBind();
                }
            }
        }
    }

    private List<TBRekomendasiProduk> GabungRekomendasi(List<TBRekomendasiProduk> rekomendasi1, List<TBRekomendasiProduk> rekomendasi2)
    {
        rekomendasi1.AddRange(rekomendasi2.Select(item => new TBRekomendasiProduk
         {
             TBProduk = item.TBProduk1,
             TBProduk1 = item.TBProduk,
             Jumlah = item.Jumlah,
             Nilai = item.Nilai
         }));

        return rekomendasi1.OrderByDescending(item => item.Jumlah).ToList();
    }
    private List<TBRekomendasiKategoriProduk> GabungRekomendasi(List<TBRekomendasiKategoriProduk> rekomendasi1, List<TBRekomendasiKategoriProduk> rekomendasi2)
    {
        rekomendasi1.AddRange(rekomendasi2.Select(item => new TBRekomendasiKategoriProduk
        {
            TBKategoriProduk = item.TBKategoriProduk1,
            TBKategoriProduk1 = item.TBKategoriProduk,
            Jumlah = item.Jumlah,
            Nilai = item.Nilai
        }));

        return rekomendasi1.OrderByDescending(item => item.Jumlah).ToList();
    }
}