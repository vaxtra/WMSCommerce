using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_CetakPengiriman : System.Web.UI.Page
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

                TBPengirimanPOProduksiBahanBaku pengirimanPOProduksiBahanBaku = db.TBPengirimanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPengirimanPOProduksiBahanBaku == Request.QueryString["id"]);

                LabelJudul.Text = "DELIVERY RAW MATERIALS TO SUPPLIER";
                LabelIDPengirimanPOProduksiBahanBaku.Text = pengirimanPOProduksiBahanBaku.IDPengirimanPOProduksiBahanBaku;

                LabelNamaSupplier.Text = pengirimanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDSupplier != null ? pengirimanPOProduksiBahanBaku.TBPOProduksiBahanBaku.TBSupplier.Nama : string.Empty;
                LabelAlamatSupplier.Text = pengirimanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDSupplier != null ? pengirimanPOProduksiBahanBaku.TBPOProduksiBahanBaku.TBSupplier.Alamat : string.Empty;

                LabelIDProduksi.Text = pengirimanPOProduksiBahanBaku.IDPOProduksiBahanBaku;
                LabelIDPengiriman.Text = pengirimanPOProduksiBahanBaku.IDPengirimanPOProduksiBahanBaku;
                LabelTanggal.Text = pengirimanPOProduksiBahanBaku.Tanggal.ToFormatTanggal();
                LabelPengirim.Text = pengirimanPOProduksiBahanBaku.TBPengguna.NamaLengkap;

                RepeaterDetail.DataSource = pengirimanPOProduksiBahanBaku.TBPengirimanPOProduksiBahanBakuDetails.ToArray();
                RepeaterDetail.DataBind();
            }
        }
    }
}