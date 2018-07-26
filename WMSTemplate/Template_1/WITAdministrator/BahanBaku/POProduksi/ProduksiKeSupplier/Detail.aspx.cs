using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_ProduksiKeSupplier_Detail : System.Web.UI.Page
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
                LabelTotalJumlah.Text = poProduksiBahanBaku.TotalJumlah.ToFormatHarga();
                LabelTotalSubtotal.Text = poProduksiBahanBaku.SubtotalTotalHargaSupplier.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(data => data.Sisa).ToFormatHarga();

                RepeaterKomposisi.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.OrderBy(item => item.TBBahanBaku.Nama).ToArray();
                RepeaterKomposisi.DataBind();
                LabelTotalSubtotalKomposisi.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.Sum(item => item.Subtotal).ToFormatHarga();

                RepeaterBiayaTambahan.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.OrderBy(item => item.TBJenisBiayaProduksi.Nama).ToArray();
                RepeaterBiayaTambahan.DataBind();
                LabelTotalSubtotalBiayaTambahan.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans.Sum(item => item.Nominal).ToFormatHarga();

                TextBoxKeterangan.Text = poProduksiBahanBaku.Keterangan;
                TextBoxBiayaLainLain.Text = poProduksiBahanBaku.BiayaLainLain.ToFormatHarga();
                TextBoxPotonganPO.Text = poProduksiBahanBaku.PotonganPOProduksiBahanBaku.ToFormatHarga();
                LabelTax.Text = "Tax (" + (poProduksiBahanBaku.PersentaseTax * 100).ToFormatHarga() + "%)";
                TextBoxTax.Text = poProduksiBahanBaku.Tax.ToFormatHarga();
                TextBoxGrandtotal.Text = poProduksiBahanBaku.Grandtotal.ToFormatHarga();

                RepeaterPengiriman.DataSource = db.TBPengirimanPOProduksiBahanBakus.Where(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]).Select(item => new
                {
                    item.IDPengirimanPOProduksiBahanBaku,
                    item.Tanggal,
                    Pegawai = item.TBPengguna.NamaLengkap,
                    Cetak = "return popitup('../CetakPengiriman.aspx?id=" + item.IDPengirimanPOProduksiBahanBaku + "')"
                }).ToArray();
                RepeaterPengiriman.DataBind();

                RepeaterPenerimaan.DataSource = db.TBPenerimaanPOProduksiBahanBakus.Where(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]).Select(item => new
                {
                    item.IDPenerimaanPOProduksiBahanBaku,
                    item.TanggalDatang,
                    Pegawai = item.TBPengguna.NamaLengkap,
                    Cetak = "return popitup('../CetakPenerimaan.aspx?id=" + item.IDPenerimaanPOProduksiBahanBaku + "')"
                });
                RepeaterPenerimaan.DataBind();

                if (poProduksiBahanBaku.TBPenerimaanPOProduksiBahanBakus.Count > 0 || poProduksiBahanBaku.TBPengirimanPOProduksiBahanBakus.Count > 0)
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

                RepeaterPengirimanDetail.DataSource = db.TBPengirimanPOProduksiBahanBakuDetails.Where(item => item.IDPengirimanPOProduksiBahanBaku == e.CommandArgument.ToString()).Select(item => new
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

                TBPenerimaanPOProduksiBahanBaku penerimaan = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPenerimaanPOProduksiBahanBaku == e.CommandArgument.ToString());

                RepeaterPenerimaanDetail.DataSource = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Select(item => new
                {
                    BahanBaku = item.TBBahanBaku.Nama,
                    Satuan = item.TBSatuan.Nama,
                    item.Datang,
                    item.Diterima,
                    item.TolakKeSupplier
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