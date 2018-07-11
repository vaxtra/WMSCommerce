using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Penagihan_Cetak : System.Web.UI.Page
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

                TBPOProduksiProdukPenagihan poProduksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == Request.QueryString["id"]);

                LabelIDPOProduksiProdukPenagihan.Text = poProduksiProdukPenagihan.IDPOProduksiProdukPenagihan;

                LabelNamaVendor.Text = poProduksiProdukPenagihan.TBVendor.Nama;
                LabelAlamatVendor.Text = poProduksiProdukPenagihan.TBVendor.Alamat;

                LabelPegawai.Text = poProduksiProdukPenagihan.TBPengguna.NamaLengkap;
                LabelStatus.Text = poProduksiProdukPenagihan.StatusPembayaran == false ? "Tagihan" : "Lunas";

                LabelKeterangan.Text = poProduksiProdukPenagihan.Keterangan;

                RepeaterDetail.DataSource = poProduksiProdukPenagihan.TBPenerimaanPOProduksiProduks.Select(item => new
                {
                    item.IDPenerimaanPOProduksiProduk,
                    item.TanggalTerima,
                    item.Grandtotal
                });
                RepeaterDetail.DataBind();
                LabelTotalPenerimaan.Text = poProduksiProdukPenagihan.TotalPenerimaan.ToFormatHarga();

                RepeaterRetur.DataSource = poProduksiProdukPenagihan.TBPOProduksiProdukReturs.Select(item => new
                {
                    item.IDPOProduksiProdukRetur,
                    item.TanggalRetur,
                    item.Grandtotal
                });
                RepeaterRetur.DataBind();
                LabelTotalRetur.Text = poProduksiProdukPenagihan.TotalRetur.ToFormatHarga();

                RepeaterDownPayment.DataSource =
                RepeaterDownPayment.DataSource = poProduksiProdukPenagihan.TBPenerimaanPOProduksiProduks.Select(item => item.TBPOProduksiProduk).Distinct().Where(item => item.IDPOProduksiProdukPenagihan == poProduksiProdukPenagihan.IDPOProduksiProdukPenagihan).Select(item => new
                {
                    item.IDPOProduksiProduk,
                    item.TanggalDownPayment,
                    item.DownPayment
                });
                RepeaterDownPayment.DataBind();
                LabelTotalDownPayment.Text = poProduksiProdukPenagihan.TotalDownPayment.ToFormatHarga();

                RepeaterPembayaran.DataSource = poProduksiProdukPenagihan.TBPOProduksiProdukPenagihanDetails.Select(item => new
                {
                    Pegawai = item.TBPengguna.NamaLengkap,
                    item.Tanggal,
                    JenisPembayaran = item.TBJenisPembayaran.Nama,
                    item.Bayar
                });
                RepeaterPembayaran.DataBind();
                LabelTotalBayar.Text = poProduksiProdukPenagihan.TotalBayar.ToFormatHarga();
            }
        }
    }
}