using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_DownPayment_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]);

                TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiBahanBaku.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;
                TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiBahanBaku.EnumJenisHPP.Value);
                TextBoxPegawaiPIC.Text = poProduksiBahanBaku.TBPengguna1.NamaLengkap;
                TextBoxPembuat.Text = poProduksiBahanBaku.TBPengguna.NamaLengkap + " / " + poProduksiBahanBaku.Tanggal.ToFormatTanggal();
                TextBoxTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman.ToFormatTanggal();

                TextBoxSupplier.Text = poProduksiBahanBaku.TBSupplier.Nama;
                TextBoxEmail.Text = poProduksiBahanBaku.TBSupplier.Email;
                TextBoxAlamat.Text = poProduksiBahanBaku.TBSupplier.Alamat;
                TextBoxTelepon1.Text = poProduksiBahanBaku.TBSupplier.Telepon1;
                TextBoxTelepon2.Text = poProduksiBahanBaku.TBSupplier.Telepon2;

                RepeaterDetail.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.ToArray();
                RepeaterDetail.DataBind();
                LabelTotalSubtotal.Text = poProduksiBahanBaku.SubtotalTotalHargaSupplier.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(data => data.Sisa).ToFormatHarga();

                TextBoxKeterangan.Text = poProduksiBahanBaku.Keterangan;
                TextBoxBiayaLainLain.Text = poProduksiBahanBaku.BiayaLainLain.ToFormatHarga();
                TextBoxPotonganPO.Text = poProduksiBahanBaku.PotonganPOProduksiBahanBaku.ToFormatHarga();
                LabelTax.Text = "Tax (" + (poProduksiBahanBaku.PersentaseTax * 100).ToFormatHarga() + "%)";
                TextBoxTax.Text = poProduksiBahanBaku.Tax.ToFormatHarga();
                TextBoxGrandtotal.Text = poProduksiBahanBaku.Grandtotal.ToFormatHarga();

                TextBoxPembayar.Text = poProduksiBahanBaku.IDPenggunaDP != null ? poProduksiBahanBaku.TBPengguna2.NamaLengkap : string.Empty;
                TextBoxTanggalDP.Text = poProduksiBahanBaku.TanggalDownPayment != null ? poProduksiBahanBaku.TanggalDownPayment.ToFormatTanggal() : string.Empty;
                TextBoxJenisPembayaran.Text = poProduksiBahanBaku.IDJenisPembayaran != null ? poProduksiBahanBaku.TBJenisPembayaran.Nama : string.Empty;
                TextBoxDownPayment.Text = poProduksiBahanBaku.DownPayment != null ? poProduksiBahanBaku.DownPayment.ToFormatHarga() : string.Empty;
            }
        }
    }
}