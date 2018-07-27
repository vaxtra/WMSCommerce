using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_CetakPenerimaan : System.Web.UI.Page
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

                TBPenerimaanPOProduksiProduk penerimaanPOProduksiProduk = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item => item.IDPenerimaanPOProduksiProduk == Request.QueryString["id"]);

                LabelJudul.Text = "(PRODUCT)";
                LabelIDPenerimaanPOProduksiProduk.Text = penerimaanPOProduksiProduk.IDPenerimaanPOProduksiProduk;

                LabelNamaVendor.Text = penerimaanPOProduksiProduk.TBPOProduksiProduk.IDVendor != null ? penerimaanPOProduksiProduk.TBPOProduksiProduk.TBVendor.Nama : string.Empty;
                LabelAlamatVendor.Text = penerimaanPOProduksiProduk.TBPOProduksiProduk.IDVendor != null ? penerimaanPOProduksiProduk.TBPOProduksiProduk.TBVendor.Alamat : string.Empty;

                LabelIDProduksi.Text = penerimaanPOProduksiProduk.IDPOProduksiProduk;
                LabelIDPenerimaan.Text = penerimaanPOProduksiProduk.IDPenerimaanPOProduksiProduk;
                LabelTanggal.Text = penerimaanPOProduksiProduk.TanggalDatang.ToFormatTanggal();
                LabelPenerima.Text = penerimaanPOProduksiProduk.TBPengguna.NamaLengkap;

                RepeaterDetail.DataSource = penerimaanPOProduksiProduk.TBPenerimaanPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();
            }
        }
    }
}