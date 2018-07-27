using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_CetakPengiriman : System.Web.UI.Page
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

                TBPengirimanPOProduksiProduk pengirimanPOProduksiBahanProduk = db.TBPengirimanPOProduksiProduks.FirstOrDefault(item => item.IDPengirimanPOProduksiProduk == Request.QueryString["id"]);

                LabelJudul.Text = "DELIVERY RAW MATERIALS TO VENDOR";
                LabelIDPengirimanPOProduksiProduk.Text = pengirimanPOProduksiBahanProduk.IDPengirimanPOProduksiProduk;

                LabelNamaVendor.Text = pengirimanPOProduksiBahanProduk.TBPOProduksiProduk.IDVendor != null ? pengirimanPOProduksiBahanProduk.TBPOProduksiProduk.TBVendor.Nama : string.Empty;
                LabelAlamatVendor.Text = pengirimanPOProduksiBahanProduk.TBPOProduksiProduk.IDVendor != null ? pengirimanPOProduksiBahanProduk.TBPOProduksiProduk.TBVendor.Alamat : string.Empty;

                LabelIDProduksi.Text = pengirimanPOProduksiBahanProduk.IDPOProduksiProduk;
                LabelIDPengiriman.Text = pengirimanPOProduksiBahanProduk.IDPengirimanPOProduksiProduk;
                LabelTanggal.Text = pengirimanPOProduksiBahanProduk.Tanggal.ToFormatTanggal();
                LabelPengirim.Text = pengirimanPOProduksiBahanProduk.TBPengguna.NamaLengkap;

                RepeaterDetail.DataSource = pengirimanPOProduksiBahanProduk.TBPengirimanPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();
            }
        }
    }
}