using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_CetakPenerimaan : System.Web.UI.Page
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

                TBPenerimaanPOProduksiBahanBaku penerimaanPOProduksiBahanBaku = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPenerimaanPOProduksiBahanBaku == Request.QueryString["id"]);

                LabelJudul.Text = "(RAW MATERIAL)";
                LabelIDPenerimaanPOProduksiBahanBaku.Text = penerimaanPOProduksiBahanBaku.IDPenerimaanPOProduksiBahanBaku;

                LabelNamaSupplier.Text = penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDSupplier != null ? penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.TBSupplier.Nama : string.Empty;
                LabelAlamatSupplier.Text = penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDSupplier != null ? penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.TBSupplier.Alamat : string.Empty;

                LabelIDProduksi.Text = penerimaanPOProduksiBahanBaku.IDPOProduksiBahanBaku;
                LabelIDPenerimaan.Text = penerimaanPOProduksiBahanBaku.IDPenerimaanPOProduksiBahanBaku;
                LabelTanggal.Text = penerimaanPOProduksiBahanBaku.TanggalDatang.ToFormatTanggal();
                LabelPenerima.Text = penerimaanPOProduksiBahanBaku.TBPengguna.NamaLengkap;

                RepeaterDetail.DataSource = penerimaanPOProduksiBahanBaku.TBPenerimaanPOProduksiBahanBakuDetails.ToArray();
                RepeaterDetail.DataBind();
            }
        }
    }
}