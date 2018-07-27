using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Master_PelangganPembelianProdukPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region FIND CONTROL
            Label LabelJudul = (Label)Page.Master.FindControl("LabelJudul");
            Label LabelSubJudul = (Label)Page.Master.FindControl("LabelSubJudul");
            Label LabelStoreTempat = (Label)Page.Master.FindControl("LabelStoreTempat");

            Label LabelPrintTanggal = (Label)Page.Master.FindControl("LabelPrintTanggal");
            Label LabelPrintPengguna = (Label)Page.Master.FindControl("LabelPrintPengguna");
            Label LabelPrintStoreTempat = (Label)Page.Master.FindControl("LabelPrintStoreTempat");

            Label LabelPeriode = (Label)Page.Master.FindControl("LabelPeriode");

            HtmlGenericControl PanelPengirimHeader = (HtmlGenericControl)Page.Master.FindControl("PanelPengirimHeader");
            HtmlGenericControl PanelPengirimFooter = (HtmlGenericControl)Page.Master.FindControl("PanelPengirimFooter");

            Label LabelPengirimTempat = (Label)Page.Master.FindControl("LabelPengirimTempat");
            Label LabelPengirimPengguna = (Label)Page.Master.FindControl("LabelPengirimPengguna");
            Label LabelPengirimPengguna1 = (Label)Page.Master.FindControl("LabelPengirimPengguna1");
            Label LabelPengirimTanggal = (Label)Page.Master.FindControl("LabelPengirimTanggal");
            Label LabelPengirimAlamat = (Label)Page.Master.FindControl("LabelPengirimAlamat");
            Label LabelPengirimTelepon = (Label)Page.Master.FindControl("LabelPengirimTelepon");
            Label LabelPengirimEmail = (Label)Page.Master.FindControl("LabelPengirimEmail");

            HtmlGenericControl PanelKeterangan = (HtmlGenericControl)Page.Master.FindControl("PanelKeterangan");
            Label LabelPengirimKeterangan = (Label)Page.Master.FindControl("LabelPengirimKeterangan");

            HtmlGenericControl PanelPenerimaHeader = (HtmlGenericControl)Page.Master.FindControl("PanelPenerimaHeader");
            HtmlGenericControl PanelPenerimaFooter = (HtmlGenericControl)Page.Master.FindControl("PanelPenerimaFooter");

            Label LabelPenerimaTempat = (Label)Page.Master.FindControl("LabelPenerimaTempat");
            Label LabelPenerimaPengguna = (Label)Page.Master.FindControl("LabelPenerimaPengguna");
            Label LabelPenerimaPengguna1 = (Label)Page.Master.FindControl("LabelPenerimaPengguna1");
            Label LabelPenerimaTanggal = (Label)Page.Master.FindControl("LabelPenerimaTanggal");
            Label LabelPenerimaAlamat = (Label)Page.Master.FindControl("LabelPenerimaAlamat");
            Label LabelPenerimaTelepon = (Label)Page.Master.FindControl("LabelPenerimaTelepon");
            #endregion

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DateTime tanggalAwal, tanggalAkhir;
                int idPelanggan, idTempat, idJenis, idStatus;

                DateTime.TryParse(Request.QueryString["TanggalAwal"], out tanggalAwal);
                DateTime.TryParse(Request.QueryString["TanggalAkhir"], out tanggalAkhir);
                int.TryParse(Request.QueryString["pelanggan"], out idPelanggan);
                int.TryParse(Request.QueryString["tempat"], out idTempat);
                int.TryParse(Request.QueryString["jenis"], out idJenis);
                int.TryParse(Request.QueryString["status"], out idStatus);

                LabelJudul.Text = "PEMBELIAN PRODUK PELANGGAN";

                if (idTempat > 0)
                    LabelStoreTempat.Text = db.TBTempats.FirstOrDefault(item => item.IDTempat == idTempat).Nama;
                else
                    LabelStoreTempat.Text = "Semua Tempat";

                if (idPelanggan > 0)
                    LabelSubJudul.Text = "Pelanggan : " + db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == idPelanggan).NamaLengkap;
                else
                    LabelSubJudul.Text = "Pelanggan : Semua Pelanggan";

                if (idJenis > 0)
                    LabelSubJudul.Text += "<br/>Transaksi : " + db.TBStatusTransaksis.FirstOrDefault(item => item.IDStatusTransaksi == idStatus).Nama;
                else
                    LabelSubJudul.Text += "<br/>Transaksi : Semua Jenis";

                if (idStatus > 0)
                    LabelSubJudul.Text += "<br/>Status : " + db.TBStatusTransaksis.FirstOrDefault(item => item.IDStatusTransaksi == idStatus).Nama;
                else
                    LabelSubJudul.Text += "<br/>Status : Semua Status";

                LoadDataProduk(db, tanggalAwal, tanggalAkhir, idPelanggan, idTempat, idJenis, idStatus);

                LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();
                LabelPrintPengguna.Text = Pengguna.NamaLengkap;
                LabelPrintStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

                if (Request.QueryString["TanggalAwal"].ToDateTime().Date == Request.QueryString["TanggalAkhir"].ToDateTime().Date)
                    LabelPeriode.Text = Request.QueryString["TanggalAwal"].ToFormatTanggal();
                else
                    LabelPeriode.Text = Request.QueryString["TanggalAwal"].ToFormatTanggal() + " - " + Request.QueryString["TanggalAkhir"].ToFormatTanggal();

            }

            PanelPengirimHeader.Visible = false;
            PanelPengirimFooter.Visible = false;

            PanelPenerimaHeader.Visible = false;
            PanelPenerimaFooter.Visible = false;
        }
    }

    private void LoadDataProduk(DataClassesDatabaseDataContext db, DateTime tanggalAwal, DateTime tanggalAkhir, int idPelanggan, int idTempat, int idJenis, int idStatus)
    {
        var pembelianProduk = db.TBTransaksiDetails.Where(item => item.TBTransaksi.TanggalOperasional >= tanggalAwal && item.TBTransaksi.TanggalOperasional <= tanggalAkhir).Select(item => new
        {
            item.TBTransaksi.IDTempat,
            item.TBTransaksi.TBPelanggan.IDPelanggan,
            item.TBTransaksi.TBPelanggan.NamaLengkap,
            item.TBTransaksi.IDJenisTransaksi,
            item.TBTransaksi.IDStatusTransaksi,
            item.TBTransaksi.TanggalTransaksi,
            item.TBTransaksi.TanggalPembayaran,
            Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
            Produk = item.TBKombinasiProduk.Nama,
            HargaJual = item.HargaJual,
            JumlahProduk = item.Quantity,
            Subtotal = item.Subtotal
        }).ToArray();

        if (idTempat > 0)
            pembelianProduk = pembelianProduk.Where(item => item.IDTempat == idTempat).ToArray();

        if (idPelanggan > 0)
            pembelianProduk = pembelianProduk.Where(item => item.IDPelanggan == idPelanggan).ToArray();

        if (idJenis > 0)
            pembelianProduk = pembelianProduk.Where(item => item.IDJenisTransaksi == idJenis).ToArray();

        if (idStatus > 0)
            pembelianProduk = pembelianProduk.Where(item => item.IDStatusTransaksi == idStatus).ToArray();

        var hasil = db.TBPelanggans.AsEnumerable().Where(itemPelanggan => pembelianProduk.FirstOrDefault(item => item.IDPelanggan == itemPelanggan.IDPelanggan) != null).Select(itemPelanggan => new
        {
            itemPelanggan.IDPelanggan,
            NamaPelanggan = itemPelanggan.NamaLengkap,
            Produk = pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).OrderBy(item => item.TanggalTransaksi),
            TotalJumlahProduk = pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Count() == 0 ? 0 : pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Sum(item => item.JumlahProduk),
            TotalJumlahSubtotal = pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Count() == 0 ? 0 : pembelianProduk.Where(itemPembelianProduk => itemPembelianProduk.IDPelanggan == itemPelanggan.IDPelanggan).Sum(item => item.Subtotal),
        }).ToArray();

        RepeaterPembelianProduk.DataSource = hasil;
        RepeaterPembelianProduk.DataBind();
    }
}