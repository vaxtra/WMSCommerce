using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Cetak : System.Web.UI.Page
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

                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]);

                LabelJudul.Text = Pengaturan.JenisPOProduksi(poProduksiBahanBaku.EnumJenisProduksi, "BahanBaku");
                LabelIDPOProduksiBahanBaku.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;

                LabelNamaSupplier.Text = poProduksiBahanBaku.IDSupplier != null ? poProduksiBahanBaku.TBSupplier.Nama : string.Empty;
                LabelAlamatSupplier.Text = poProduksiBahanBaku.IDSupplier != null ? poProduksiBahanBaku.TBSupplier.Alamat : string.Empty;

                LabelIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : string.Empty;
                LabelIDProduksi.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;
                LabelTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo.ToFormatTanggal();
                LabelTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman.ToFormatTanggal();

                LabelPembuat.Text = poProduksiBahanBaku.TBPengguna.NamaLengkap + " / " + poProduksiBahanBaku.Tanggal.ToFormatTanggal();

                RepeaterDetail.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Select(item => new
                {
                    item.TBBahanBaku.KodeBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    Satuan = item.TBSatuan.Nama,
                    item.HargaPokokKomposisi,
                    item.BiayaTambahan,
                    item.HargaSupplier,
                    item.PotonganHargaSupplier,
                    item.Jumlah,
                    Subtotal = item.TBPOProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.SubtotalHPP : item.SubtotalHargaSupplier
                }).ToArray();
                RepeaterDetail.DataBind();
                LabelTotalSubtotal.Text = poProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? poProduksiBahanBaku.SubtotalTotalHPP.ToFormatHarga() : poProduksiBahanBaku.SubtotalTotalHargaSupplier.ToFormatHarga();

                if (poProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri)
                {
                    headerHarga.Visible = false;
                    headerPotongan.Visible = false;
                }
                else
                {
                    headerKomposisi.Visible = false;
                    headerBiaya.Visible = false;

                }
                RepeaterKomposisi.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.OrderBy(item => item.TBBahanBaku.Nama).ToArray();
                RepeaterKomposisi.DataBind();
                LabelTotalSubtotalKomposisi.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.Sum(item => item.Subtotal).ToFormatHarga();

                RepeaterBiayaTambahan.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.OrderBy(item => item.TBJenisBiayaProduksi.Nama).ToArray();
                RepeaterBiayaTambahan.DataBind();
                LabelTotalSubtotalBiayaTambahan.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.Sum(item => item.Nominal).ToFormatHarga();
                LabelBiayaLainLain.Text = poProduksiBahanBaku.BiayaLainLain.ToFormatHarga();
                LabelPotongan.Text = poProduksiBahanBaku.PotonganPOProduksiBahanBaku.ToFormatHarga();
                LabelJudulTax.Text = "Tax (" + (poProduksiBahanBaku.PersentaseTax * 100).ToFormatHarga() + "%)";
                LabelTax.Text = poProduksiBahanBaku.Tax.ToFormatHarga();
                LabelGrandtotal.Text = poProduksiBahanBaku.Grandtotal.ToFormatHarga();

                LiteralKeterangan.Text = "<b>Keterangan :</b><br/>" + poProduksiBahanBaku.Keterangan;

                if (poProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder)
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
            TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]);

            if (poProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri)
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