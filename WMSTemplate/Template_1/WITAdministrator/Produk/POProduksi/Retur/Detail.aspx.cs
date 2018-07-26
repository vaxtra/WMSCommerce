using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Retur_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProdukRetur POProduksiProdukRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item => item.IDPOProduksiProdukRetur == Request.QueryString["id"]);

                TextBoxIDPOProduksiProdukRetur.Text = POProduksiProdukRetur.IDPOProduksiProdukRetur;
                TextBoxIDPenerimaanPOProduksiProduk.Text = POProduksiProdukRetur.IDPenerimaanPOProduksiProduk != null ? POProduksiProdukRetur.IDPenerimaanPOProduksiProduk : string.Empty;
                TextBoxPegawai.Text = POProduksiProdukRetur.TBPengguna.NamaLengkap;
                TextBoxTanggal.Text = POProduksiProdukRetur.TanggalRetur.ToFormatTanggal();
                TextBoxIDPOProduksiProdukPenagihan.Text = POProduksiProdukRetur.IDPOProduksiProdukPenagihan != null ? POProduksiProdukRetur.IDPOProduksiProdukPenagihan : string.Empty;
                TextBoxStatus.Text = Pengaturan.StatusPOProduksi(POProduksiProdukRetur.EnumStatusRetur.Value); ;

                TextBoxVendor.Text = POProduksiProdukRetur.TBVendor.Nama;
                TextBoxEmail.Text = POProduksiProdukRetur.TBVendor.Email;
                TextBoxAlamat.Text = POProduksiProdukRetur.TBVendor.Alamat;
                TextBoxTelepon1.Text = POProduksiProdukRetur.TBVendor.Telepon1;
                TextBoxTelepon2.Text = POProduksiProdukRetur.TBVendor.Telepon2;

                RepeaterDetail.DataSource = POProduksiProdukRetur.TBPOProduksiProdukReturDetails.Select(item => new
                {
                    Produk = item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBStokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
                    item.HargaRetur,
                    item.Jumlah,
                    item.Subtotal
                }); ;
                RepeaterDetail.DataBind();
                LabelTotalSubtotal.Text = POProduksiProdukRetur.Grandtotal.Value.ToFormatHarga();

                TextBoxKeterangan.Text = string.Empty;
            }         
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}