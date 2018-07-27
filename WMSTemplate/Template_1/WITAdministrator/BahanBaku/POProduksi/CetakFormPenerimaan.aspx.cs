using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_CetakFormPenerimaan : System.Web.UI.Page
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

                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]);

                LabelJudul.Text = "(RAW MATERIAL)";

                LabelNamaSupplier.Text = poProduksiBahanBaku.IDSupplier != null ? poProduksiBahanBaku.TBSupplier.Nama : string.Empty;
                LabelAlamatSupplier.Text = poProduksiBahanBaku.IDSupplier != null ? poProduksiBahanBaku.TBSupplier.Alamat : string.Empty;

                LabelIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi;
                LabelIDProduksi.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;

                RepeaterDetail.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Select(item => new
                {
                    item.TBBahanBaku.KodeBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    Satuan = item.TBSatuan.Nama,
                    Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(db, null, item.TBBahanBaku),
                    item.Jumlah,
                    item.Sisa
                }).ToArray();
                RepeaterDetail.DataBind();
            }
        }
    }
}