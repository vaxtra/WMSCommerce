using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_ProduksiSendiri_Detail : System.Web.UI.Page
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
                TextBoxKeterangan.Text = poProduksiProduk.Keterangan;

                RepeaterDetail.DataSource = poProduksiProduk.TBPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = poProduksiProduk.TotalJumlah.ToFormatHargaBulat();
                LabelTotalSubtotal.Text = poProduksiProduk.SubtotalTotalHargaVendor.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiProduk.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat();

                RepeaterKomposisi.DataSource = poProduksiProduk.TBPOProduksiProdukKomposisis.OrderBy(item => item.TBBahanBaku.Nama).ToArray();
                RepeaterKomposisi.DataBind();
                LabelTotalSubtotalKomposisi.Text = poProduksiProduk.TBPOProduksiProdukKomposisis.Sum(item => item.Subtotal).ToFormatHarga();

                RepeaterBiayaTambahan.DataSource = poProduksiProduk.TBPOProduksiProdukBiayaTambahans.OrderBy(item => item.TBJenisBiayaProduksi.Nama).ToArray();
                RepeaterBiayaTambahan.DataBind();
                LabelTotalSubtotalBiayaTambahan.Text = poProduksiProduk.TBPOProduksiProdukBiayaTambahans.Sum(item => item.Nominal).ToFormatHarga();

                RepeaterPengiriman.DataSource = db.TBPengirimanPOProduksiProduks.Where(item => item.IDPOProduksiProduk == Request.QueryString["id"]).Select(item => new
                {
                    item.IDPengirimanPOProduksiProduk,
                    item.Tanggal,
                    Pegawai = item.TBPengguna.NamaLengkap,
                    Cetak = "return popitup('../CetakPengiriman.aspx?id=" + item.IDPengirimanPOProduksiProduk + "')"
                }).ToArray();
                RepeaterPengiriman.DataBind();

                RepeaterPenerimaan.DataSource = db.TBPenerimaanPOProduksiProduks.Where(item => item.IDPOProduksiProduk == Request.QueryString["id"]).Select(item => new
                {
                    item.IDPenerimaanPOProduksiProduk,
                    item.TanggalDatang,
                    Pegawai = item.TBPengguna.NamaLengkap,
                    Cetak = "return popitup('../CetakPenerimaan.aspx?id=" + item.IDPenerimaanPOProduksiProduk + "')"
                });
                RepeaterPenerimaan.DataBind();

                if (poProduksiProduk.TBPenerimaanPOProduksiProduks.Count > 0 || poProduksiProduk.TBPengirimanPOProduksiProduks.Count > 0)
                    ButtonEdit.Visible = false;
            }
        }
    }

    protected void RepeaterPengiriman_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LabelIDPengiriman.Text = e.CommandArgument.ToString();

                RepeaterPengirimanDetail.DataSource = db.TBPengirimanPOProduksiProdukDetails.Where(item => item.IDPengirimanPOProduksiProduk == e.CommandArgument.ToString()).Select(item => new
                {
                    BahanBaku = item.TBBahanBaku.Nama,
                    Satuan = item.TBSatuan.Nama,
                    item.Kirim
                }).ToArray();
                RepeaterPengirimanDetail.DataBind();
            }
        }
    }

    protected void RepeaterPenerimaan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LabelIDPenerimaan.Text = e.CommandArgument.ToString();

                TBPenerimaanPOProduksiProduk penerimaan = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item => item.IDPenerimaanPOProduksiProduk == e.CommandArgument.ToString());

                RepeaterPenerimaanDetail.DataSource = penerimaan.TBPenerimaanPOProduksiProdukDetails.Select(item => new
                {
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    item.Datang,
                    item.Diterima,
                    item.TolakKeGudang
                }).ToArray();
                RepeaterPenerimaanDetail.DataBind();

                TextBoxKeteranganPenerimaan.Text = penerimaan.Keterangan;
            }
        }
    }

    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx?edit=" + Request.QueryString["id"]);
    }
}