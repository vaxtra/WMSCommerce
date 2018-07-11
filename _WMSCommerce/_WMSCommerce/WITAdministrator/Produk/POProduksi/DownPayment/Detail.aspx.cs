using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_DownPayment_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["id"]);

                TextBoxIDProyeksi.Text = poProduksiProduk.IDProyeksi != null ? poProduksiProduk.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiProduk.Text = poProduksiProduk.IDPOProduksiProduk;
                TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiProduk.EnumJenisHPP.Value);
                TextBoxPegawaiPIC.Text = poProduksiProduk.TBPengguna1.NamaLengkap;
                TextBoxPembuat.Text = poProduksiProduk.TBPengguna.NamaLengkap + " / " + poProduksiProduk.Tanggal.ToFormatTanggal();
                TextBoxTanggalJatuhTempo.Text = poProduksiProduk.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman.ToFormatTanggal();

                TextBoxVendor.Text = poProduksiProduk.TBVendor.Nama;
                TextBoxEmail.Text = poProduksiProduk.TBVendor.Email;
                TextBoxAlamat.Text = poProduksiProduk.TBVendor.Alamat;
                TextBoxTelepon1.Text = poProduksiProduk.TBVendor.Telepon1;
                TextBoxTelepon2.Text = poProduksiProduk.TBVendor.Telepon2;

                RepeaterDetail.DataSource = poProduksiProduk.TBPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = poProduksiProduk.TotalJumlah.ToFormatHargaBulat();
                LabelTotalSubtotal.Text = poProduksiProduk.SubtotalTotalHargaVendor.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiProduk.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat();

                TextBoxKeterangan.Text = poProduksiProduk.Keterangan;
                TextBoxBiayaLainLain.Text = poProduksiProduk.BiayaLainLain.ToFormatHarga();
                TextBoxPotonganPO.Text = poProduksiProduk.PotonganPOProduksiProduk.ToFormatHarga();
                LabelTax.Text = "Tax (" + (poProduksiProduk.PersentaseTax * 100).ToFormatHarga() + "%)";
                TextBoxTax.Text = poProduksiProduk.Tax.ToFormatHarga();
                TextBoxGrandtotal.Text = poProduksiProduk.Grandtotal.ToFormatHarga();

                TextBoxPembayar.Text = poProduksiProduk.IDPenggunaDP != null ? poProduksiProduk.TBPengguna2.NamaLengkap : string.Empty;
                TextBoxTanggalDP.Text = poProduksiProduk.TanggalDownPayment != null ? poProduksiProduk.TanggalDownPayment.ToFormatTanggal() : string.Empty;
                TextBoxJenisPembayaran.Text = poProduksiProduk.IDJenisPembayaran != null ? poProduksiProduk.TBJenisPembayaran.Nama : string.Empty;
                TextBoxDownPayment.Text = poProduksiProduk.DownPayment != null ? poProduksiProduk.DownPayment.ToFormatHarga() : string.Empty;
            }
        }
    }
}