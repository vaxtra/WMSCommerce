using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Retur_Cetak : System.Web.UI.Page
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


                TBPOProduksiBahanBakuRetur dataRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item => item.IDPOProduksiBahanBakuRetur == Request.QueryString["id"]);

                var detailRetur = dataRetur.TBPOProduksiBahanBakuReturDetails.Select(item => new
                {
                    item.TBStokBahanBaku.TBBahanBaku.Nama,
                    item.HargaRetur,
                    item.Jumlah,
                    item.Subtotal,
                    Satuan = item.TBStokBahanBaku.TBBahanBaku.TBSatuan1.Nama
                });

                RepeaterDetail.DataSource = detailRetur;
                RepeaterDetail.DataBind();

                LabelIDReturBahanBaku.Text = dataRetur.IDPOProduksiBahanBakuRetur;
                LabelPengguna.Text = dataRetur.TBPengguna.NamaLengkap;
                LabelIDPenerimaanPOProduksiBahanBaku.Text = dataRetur.IDPenerimaanPOProduksiBahanBaku == null ? "-" : dataRetur.IDPenerimaanPOProduksiBahanBaku;
                LabelSupplier.Text = dataRetur.TBSupplier.Nama;
                LabelTanggalRetur.Text = dataRetur.TanggalRetur.ToFormatTanggalJam();
                LabelIDPenagihan.Text = dataRetur.IDPOProduksiBahanBakuPenagihan == null ? "-" : dataRetur.IDPOProduksiBahanBakuPenagihan;
                LabelTotalSubtotal.Text = dataRetur.Grandtotal.Value.ToFormatHarga();
                LabelStatusRetur.Text = Pengaturan.StatusPOProduksi(dataRetur.EnumStatusRetur.Value);
                LabelKeterangan.Text = dataRetur.Keterangan;
            }


        }
    }
}