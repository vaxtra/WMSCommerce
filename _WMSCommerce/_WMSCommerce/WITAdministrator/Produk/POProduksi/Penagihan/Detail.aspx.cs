using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Penagihan_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProdukPenagihan poProduksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == Request.QueryString["id"]);

                TextBoxIDPOProduksiBahanBakuPenagihan.Text = poProduksiProdukPenagihan.IDPOProduksiProdukPenagihan;
                TextBoxPegawai.Text = poProduksiProdukPenagihan.TBPengguna.NamaLengkap;
                TextBoxStatus.Text = poProduksiProdukPenagihan.StatusPembayaran == false ? "Tagihan" : "Lunas";

                TextBoxVendor.Text = poProduksiProdukPenagihan.TBVendor.Nama;
                TextBoxAlamat.Text = poProduksiProdukPenagihan.TBVendor.Alamat;
                TextBoxEmail.Text = poProduksiProdukPenagihan.TBVendor.Email;
                TextBoxTelepon1.Text = poProduksiProdukPenagihan.TBVendor.Telepon1;
                TextBoxTelepon2.Text = poProduksiProdukPenagihan.TBVendor.Telepon2;

                TextBoxKeterangan.Text = poProduksiProdukPenagihan.Keterangan;

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