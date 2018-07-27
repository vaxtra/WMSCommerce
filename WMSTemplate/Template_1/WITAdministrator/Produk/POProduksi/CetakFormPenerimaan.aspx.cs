using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_CetakFormPenerimaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBStore store = db.TBStores.FirstOrDefault();

                LabelNamaStore.Text = store.Nama;
                LabelAlamatStore.Text = store.Alamat;

                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["id"]);

                LabelJudul.Text = "(PRODUCT)";

                LabelNamaVendor.Text = poProduksiProduk.IDVendor != null ? poProduksiProduk.TBVendor.Nama : string.Empty;
                LabelAlamatVendor.Text = poProduksiProduk.IDVendor != null ? poProduksiProduk.TBVendor.Alamat : string.Empty;

                LabelIDProyeksi.Text = poProduksiProduk.IDProyeksi;
                LabelIDProduksi.Text = poProduksiProduk.IDPOProduksiProduk;

                RepeaterDetail.DataSource = poProduksiProduk.TBPOProduksiProdukDetails.Select(item => new
                {
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.TBKombinasiProduk),
                    item.Jumlah,
                    item.Sisa
                }).ToArray();
                RepeaterDetail.DataBind();
            }
        }
    }
}