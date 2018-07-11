using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Retur_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBakuRetur POProduksiBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item => item.IDPOProduksiBahanBakuRetur == Request.QueryString["id"]);

                TextBoxIDPOProduksiBahanBakuRetur.Text = POProduksiBahanBakuRetur.IDPOProduksiBahanBakuRetur;
                TextBoxIDPenerimaanPOProduksiBahanBaku.Text = POProduksiBahanBakuRetur.IDPenerimaanPOProduksiBahanBaku != null ? POProduksiBahanBakuRetur.IDPenerimaanPOProduksiBahanBaku : string.Empty;
                TextBoxPegawai.Text = POProduksiBahanBakuRetur.TBPengguna.NamaLengkap;
                TextBoxTanggal.Text = POProduksiBahanBakuRetur.TanggalRetur.ToFormatTanggal();
                TextBoxIDPOProduksiBahanBakuPenagihan.Text = POProduksiBahanBakuRetur.IDPOProduksiBahanBakuPenagihan != null ? POProduksiBahanBakuRetur.IDPOProduksiBahanBakuPenagihan : string.Empty;
                TextBoxStatus.Text = Pengaturan.StatusPOProduksi(POProduksiBahanBakuRetur.EnumStatusRetur.Value); ;

                TextBoxSupplier.Text = POProduksiBahanBakuRetur.TBSupplier.Nama;
                TextBoxEmail.Text = POProduksiBahanBakuRetur.TBSupplier.Email;
                TextBoxAlamat.Text = POProduksiBahanBakuRetur.TBSupplier.Alamat;
                TextBoxTelepon1.Text = POProduksiBahanBakuRetur.TBSupplier.Telepon1;
                TextBoxTelepon2.Text = POProduksiBahanBakuRetur.TBSupplier.Telepon2;

                RepeaterDetail.DataSource = POProduksiBahanBakuRetur.TBPOProduksiBahanBakuReturDetails.Select(item => new
                {
                    item.TBStokBahanBaku.TBBahanBaku.Nama,
                    item.HargaRetur,
                    item.Jumlah,
                    item.Subtotal,
                    Satuan = item.TBSatuan.Nama
                }); ;
                RepeaterDetail.DataBind();
                LabelTotalSubtotal.Text = POProduksiBahanBakuRetur.Grandtotal.Value.ToFormatHarga();

                TextBoxKeterangan.Text = string.Empty;
            }         
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}