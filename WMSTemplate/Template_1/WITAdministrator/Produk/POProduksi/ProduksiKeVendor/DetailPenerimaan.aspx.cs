using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_ProduksiKeSupplier_DetailPenerimaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPenerimaanPOProduksiProduk penerimaan = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item => item.IDPenerimaanPOProduksiProduk == Request.QueryString["id"]);

                TextBoxIDProyeksi.Text = penerimaan.TBPOProduksiProduk.IDProyeksi != null ? penerimaan.TBPOProduksiProduk.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiProduk.Text = penerimaan.IDPOProduksiProduk;
                TextBoxPegawai.Text = penerimaan.TBPengguna1.NamaLengkap;
                TextBoxTanggal.Text = penerimaan.TanggalTerima.ToFormatTanggal();
                TextBoxStatus.Text = penerimaan.TBPOProduksiProdukPenagihan != null ? penerimaan.TBPOProduksiProdukPenagihan.StatusPembayaran == false ? "Kontra" : "Lunas" : "Baru";
                TextBoxIDPOProduksiProdukPenagihan.Text = penerimaan.TBPOProduksiProdukPenagihan == null ? string.Empty : penerimaan.IDPOProduksiProdukPenagihan;

                TextBoxVendor.Text = penerimaan.TBPOProduksiProduk.TBVendor.Nama;
                TextBoxEmail.Text = penerimaan.TBPOProduksiProduk.TBVendor.Email;
                TextBoxAlamat.Text = penerimaan.TBPOProduksiProduk.TBVendor.Alamat;
                TextBoxTelepon1.Text = penerimaan.TBPOProduksiProduk.TBVendor.Telepon1;
                TextBoxTelepon2.Text = penerimaan.TBPOProduksiProduk.TBVendor.Telepon2;

                RepeaterDetail.DataSource = penerimaan.TBPenerimaanPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();

                TextBoxKeterangan.Text = penerimaan.Keterangan;
            }
        }
    }
}