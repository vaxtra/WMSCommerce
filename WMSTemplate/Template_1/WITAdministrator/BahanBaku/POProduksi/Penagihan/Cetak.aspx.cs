using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Penagihan_Cetak : System.Web.UI.Page
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

                TBPOProduksiBahanBakuPenagihan poProduksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.FirstOrDefault(item => item.IDPOProduksiBahanBakuPenagihan == Request.QueryString["id"]);

                LabelIDPOProduksiBahanBakuPenagihan.Text = poProduksiBahanBakuPenagihan.IDPOProduksiBahanBakuPenagihan;

                LabelNamaSupplier.Text = poProduksiBahanBakuPenagihan.TBSupplier.Nama;
                LabelAlamatSupplier.Text = poProduksiBahanBakuPenagihan.TBSupplier.Alamat;

                LabelPegawai.Text = poProduksiBahanBakuPenagihan.TBPengguna.NamaLengkap;
                LabelStatus.Text = poProduksiBahanBakuPenagihan.StatusPembayaran == false ? "Tagihan" : "Lunas";

                LabelKeterangan.Text = poProduksiBahanBakuPenagihan.Keterangan;

                RepeaterDetail.DataSource = poProduksiBahanBakuPenagihan.TBPenerimaanPOProduksiBahanBakus.Select(item => new
                {
                    item.IDPenerimaanPOProduksiBahanBaku,
                    item.TanggalTerima,
                    item.Grandtotal
                });
                RepeaterDetail.DataBind();
                LabelTotalPenerimaan.Text = poProduksiBahanBakuPenagihan.TotalPenerimaan.ToFormatHarga();

                RepeaterRetur.DataSource = poProduksiBahanBakuPenagihan.TBPOProduksiBahanBakuReturs.Select(item => new
                {
                    item.IDPOProduksiBahanBakuRetur,
                    item.TanggalRetur,
                    item.Grandtotal
                });
                RepeaterRetur.DataBind();
                LabelTotalRetur.Text = poProduksiBahanBakuPenagihan.TotalRetur.ToFormatHarga();

                RepeaterDownPayment.DataSource = poProduksiBahanBakuPenagihan.TBPenerimaanPOProduksiBahanBakus.Select(item => item.TBPOProduksiBahanBaku).Distinct().Where(item => item.IDPOProduksiBahanBakuPenagihan == poProduksiBahanBakuPenagihan.IDPOProduksiBahanBakuPenagihan).Select(item => new
                {
                    item.IDPOProduksiBahanBaku,
                    item.TanggalDownPayment,
                    item.DownPayment
                });
                RepeaterDownPayment.DataBind();
                LabelTotalDownPayment.Text = poProduksiBahanBakuPenagihan.TotalDownPayment.ToFormatHarga();

                RepeaterPembayaran.DataSource = poProduksiBahanBakuPenagihan.TBPOProduksiBahanBakuPenagihanDetails.Select(item => new
                {
                    Pegawai = item.TBPengguna.NamaLengkap,
                    item.Tanggal,
                    JenisPembayaran = item.TBJenisPembayaran.Nama,
                    item.Bayar
                });
                RepeaterPembayaran.DataBind();
                LabelTotalBayar.Text = poProduksiBahanBakuPenagihan.TotalBayar.ToFormatHarga();
            }
        }
    }
}