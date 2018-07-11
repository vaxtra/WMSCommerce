using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Retur_Cetak : System.Web.UI.Page
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


                TBPOProduksiProdukRetur dataRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item => item.IDPOProduksiProdukRetur == Request.QueryString["id"]);

                var detailRetur = dataRetur.TBPOProduksiProdukReturDetails.Select(item => new
                {
                    Produk = item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBStokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
                    item.HargaRetur,
                    item.Jumlah,
                    item.Subtotal
                });

                RepeaterDetail.DataSource = detailRetur;
                RepeaterDetail.DataBind();

                LabelIDReturBahanBaku.Text = dataRetur.IDPOProduksiProdukRetur;
                LabelPengguna.Text = dataRetur.TBPengguna.NamaLengkap;
                LabelIDPenerimaanPOProduksiBahanBaku.Text = dataRetur.IDPenerimaanPOProduksiProduk == null ? "-" : dataRetur.IDPenerimaanPOProduksiProduk;
                LabelVendor.Text = dataRetur.TBVendor.Nama;
                LabelTanggalRetur.Text = dataRetur.TanggalRetur.ToFormatTanggalJam();
                LabelIDPenagihan.Text = dataRetur.IDPOProduksiProdukPenagihan == null ? "-" : dataRetur.IDPOProduksiProdukPenagihan;
                LabelTotalSubtotal.Text = dataRetur.Grandtotal.Value.ToFormatHarga();
                LabelStatusRetur.Text = Pengaturan.StatusPOProduksi(dataRetur.EnumStatusRetur.Value);
                LabelKeterangan.Text = dataRetur.Keterangan;
            }


        }
    }
}