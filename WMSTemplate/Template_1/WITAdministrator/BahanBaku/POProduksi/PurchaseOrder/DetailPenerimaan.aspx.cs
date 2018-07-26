using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_PurchaseOrder_DetailPenerimaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPenerimaanPOProduksiBahanBaku penerimaan = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPenerimaanPOProduksiBahanBaku == Request.QueryString["id"]);

                TextBoxIDProyeksi.Text = penerimaan.TBPOProduksiBahanBaku.IDProyeksi != null ? penerimaan.TBPOProduksiBahanBaku.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiBahanBaku.Text = penerimaan.IDPOProduksiBahanBaku;
                TextBoxPegawai.Text = penerimaan.TBPengguna1.NamaLengkap;
                TextBoxTanggal.Text = penerimaan.TanggalTerima.ToFormatTanggal();
                TextBoxStatus.Text = penerimaan.TBPOProduksiBahanBakuPenagihan != null ? penerimaan.TBPOProduksiBahanBakuPenagihan.StatusPembayaran == false ? "Kontra" : "Lunas" : "Baru";
                TextBoxIDPOProduksiBahanBakuPenagihan.Text = penerimaan.TBPOProduksiBahanBakuPenagihan == null ? string.Empty : penerimaan.IDPOProduksiBahanBakuPenagihan;
                
                TextBoxSupplier.Text = penerimaan.TBPOProduksiBahanBaku.TBSupplier.Nama;
                TextBoxEmail.Text = penerimaan.TBPOProduksiBahanBaku.TBSupplier.Email;
                TextBoxAlamat.Text = penerimaan.TBPOProduksiBahanBaku.TBSupplier.Alamat;
                TextBoxTelepon1.Text = penerimaan.TBPOProduksiBahanBaku.TBSupplier.Telepon1;
                TextBoxTelepon2.Text = penerimaan.TBPOProduksiBahanBaku.TBSupplier.Telepon2;

                RepeaterDetail.DataSource = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.ToArray();
                RepeaterDetail.DataBind();

                TextBoxKeterangan.Text = penerimaan.Keterangan;
            }
        }
    }
}