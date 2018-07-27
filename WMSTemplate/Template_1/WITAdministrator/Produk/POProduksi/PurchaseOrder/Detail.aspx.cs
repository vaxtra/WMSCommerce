using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_PurchaseOrder_Detail : System.Web.UI.Page
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
                TextBoxTanggalJatuhTempo.Text = poProduksiProduk.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman.ToFormatTanggal();

                TextBoxVendor.Text = poProduksiProduk.TBVendor.Nama;
                TextBoxEmail.Text = poProduksiProduk.TBVendor.Email;
                TextBoxAlamat.Text = poProduksiProduk.TBVendor.Alamat;
                TextBoxTelepon1.Text = poProduksiProduk.TBVendor.Telepon1;
                TextBoxTelepon2.Text = poProduksiProduk.TBVendor.Telepon2;

                RepeaterDetail.DataSource = poProduksiProduk.TBPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = poProduksiProduk.TotalJumlah.ToFormatHargaBulat();
                LabelTotalSubtotal.Text = poProduksiProduk.SubtotalTotalHargaVendor.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiProduk.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat();

                TextBoxKeterangan.Text = poProduksiProduk.Keterangan;
                TextBoxBiayaLainLain.Text = poProduksiProduk.BiayaLainLain.ToFormatHarga();
                TextBoxPotonganPO.Text = poProduksiProduk.PotonganPOProduksiProduk.ToFormatHarga();
                LabelTax.Text = "Tax (" + (poProduksiProduk.PersentaseTax * 100).ToFormatHarga() + "%)";
                TextBoxTax.Text = poProduksiProduk.Tax.ToFormatHarga();
                TextBoxGrandtotal.Text = poProduksiProduk.Grandtotal.ToFormatHarga();

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
                    item.TolakKeVendor
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