using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Cetak : System.Web.UI.Page
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

                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["id"]);

                LabelJudul.Text = Pengaturan.JenisPOProduksi(poProduksiProduk.EnumJenisProduksi, "Produk");

                LabelNamaVendor.Text = poProduksiProduk.IDVendor != null ? poProduksiProduk.TBVendor.Nama : string.Empty;
                LabelAlamatVendor.Text = poProduksiProduk.IDVendor != null ? poProduksiProduk.TBVendor.Alamat : string.Empty;

                LabelIDProyeksi.Text = poProduksiProduk.IDProyeksi != null ? poProduksiProduk.IDProyeksi : string.Empty;
                LabelIDProduksi.Text = poProduksiProduk.IDPOProduksiProduk;
                LabelTanggalJatuhTempo.Text = poProduksiProduk.TanggalJatuhTempo.ToFormatTanggal();
                LabelTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman.ToFormatTanggal();

                LabelPembuat.Text = poProduksiProduk.TBPengguna.NamaLengkap + " / " + poProduksiProduk.Tanggal.ToFormatTanggal();

                RepeaterDetail.DataSource = poProduksiProduk.TBPOProduksiProdukDetails.Select(item => new
                {
                    KodeKombinasiProduk = item.TBKombinasiProduk.KodeKombinasiProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    item.HargaPokokKomposisi,
                    item.BiayaTambahan,
                    item.HargaVendor,
                    item.PotonganHargaVendor,
                    item.Jumlah,
                    Subtotal = item.TBPOProduksiProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.SubtotalHPP : item.SubtotalHargaVendor
                }).ToArray();
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = poProduksiProduk.TotalJumlah.ToFormatHargaBulat();
                LabelTotalSubtotal.Text = poProduksiProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? poProduksiProduk.SubtotalTotalHPP.ToFormatHarga() : poProduksiProduk.SubtotalTotalHargaVendor.ToFormatHarga();

                if (poProduksiProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri)
                {
                    headerHarga.Visible = false;
                    headerPotongan.Visible = false;
                }
                else
                {
                    headerKomposisi.Visible = false;
                    headerBiaya.Visible = false;
                }

                RepeaterKomposisi.DataSource = poProduksiProduk.TBPOProduksiProdukKomposisis.OrderBy(item => item.TBBahanBaku.Nama).ToArray();
                RepeaterKomposisi.DataBind();
                LabelTotalSubtotalKomposisi.Text = poProduksiProduk.TBPOProduksiProdukKomposisis.Sum(item => item.Subtotal).ToFormatHarga();

                RepeaterBiayaTambahan.DataSource = poProduksiProduk.TBPOProduksiProdukBiayaTambahans.OrderBy(item => item.TBJenisBiayaProduksi.Nama).ToArray();
                RepeaterBiayaTambahan.DataBind();
                LabelTotalSubtotalBiayaTambahan.Text = poProduksiProduk.TBPOProduksiProdukBiayaTambahans.Sum(item => item.Nominal).ToFormatHarga();
                LabelBiayaLainLain.Text = poProduksiProduk.BiayaLainLain.ToFormatHarga();
                LabelPotongan.Text = poProduksiProduk.PotonganPOProduksiProduk.ToFormatHarga();
                LabelJudulTax.Text = "Tax (" + (poProduksiProduk.PersentaseTax * 100).ToFormatHarga() + "%)";
                LabelTax.Text = poProduksiProduk.Tax.ToFormatHarga();
                LabelGrandtotal.Text = poProduksiProduk.Grandtotal.ToFormatHarga();

                LiteralKeterangan.Text = "<b>Keterangan :</b><br/>" + poProduksiProduk.Keterangan;

                if (poProduksiProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder)
                {
                    komposisi.Visible = false;
                }
            }
        }
    }

    protected void RepeaterDetail_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["id"]);

            if (poProduksiProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri)
            {
                ((HtmlTableCell)(e.Item.FindControl("bodyHarga"))).Attributes.Add("class", "hidden");
                ((HtmlTableCell)(e.Item.FindControl("bodyPotongan"))).Attributes.Add("class", "hidden");
            }
            else
            {
                ((HtmlTableCell)(e.Item.FindControl("bodyKomposisi"))).Attributes.Add("class", "hidden");
                ((HtmlTableCell)(e.Item.FindControl("bodyBiaya"))).Attributes.Add("class", "hidden");
            }
        }
    }
}