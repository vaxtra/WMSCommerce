using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_Transaksi : System.Web.UI.Page
{
    #region Default
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
            LoadData();
        }
    }
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    protected void DropDownListStatusTransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    #endregion

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadData(db);
        }
    }
    private void LoadData(DataClassesDatabaseDataContext db)
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            if (ViewState["TanggalAwal"].ToString() == ViewState["TanggalAkhir"].ToString())
                LabelPeriode.Text = Pengaturan.FormatTanggalRingkas(ViewState["TanggalAwal"]);
            else
                LabelPeriode.Text = Pengaturan.FormatTanggalRingkas(ViewState["TanggalAwal"]) + " - " + Pengaturan.FormatTanggalRingkas(ViewState["TanggalAkhir"]);

            TBTransaksi[] DataTransaksi;

            if (DropDownListStatusTransaksi.SelectedValue == "0")
            {
                //SEMUA TRANSAKSI
                DataTransaksi = db.TBTransaksis
                    .Where(item =>
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TanggalTransaksi.Value.Date >= ((DateTime)ViewState["TanggalAwal"]) &&
                        item.TanggalTransaksi.Value.Date <= ((DateTime)ViewState["TanggalAkhir"]))
                     .ToArray();
            }
            else if (Parse.Int(DropDownListStatusTransaksi.SelectedValue) == (int)EnumStatusTransaksi.AwaitingPayment)
            {
                DataTransaksi = db.TBTransaksis
                    .Where(item =>
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TanggalTransaksi.Value.Date >= ((DateTime)ViewState["TanggalAwal"]) &&
                        item.TanggalTransaksi.Value.Date <= ((DateTime)ViewState["TanggalAkhir"]) &&
                        item.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment &&
                        item.TBTransaksiJenisPembayarans.Count == 0) //BELUM ADA PEMBAYARAN
                    .ToArray();
            }
            else if (Parse.Int(DropDownListStatusTransaksi.SelectedValue) == (int)EnumStatusTransaksi.AwaitingPaymentVerification)
            {
                DataTransaksi = db.TBTransaksis
                    .Where(item =>
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TanggalTransaksi.Value.Date >= ((DateTime)ViewState["TanggalAwal"]) &&
                        item.TanggalTransaksi.Value.Date <= ((DateTime)ViewState["TanggalAkhir"]) &&
                        item.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment &&
                        item.TBTransaksiJenisPembayarans.Count > 0) //SUDAH ADA PEMBAYARAN
                    .ToArray();
            }
            else
            {
                //SESUAI DENGAN PILIHAN
                DataTransaksi = db.TBTransaksis
                    .Where(item =>
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TanggalTransaksi.Value.Date >= ((DateTime)ViewState["TanggalAwal"]) &&
                        item.TanggalTransaksi.Value.Date <= ((DateTime)ViewState["TanggalAkhir"]) &&
                        item.IDStatusTransaksi == Parse.Int(DropDownListStatusTransaksi.SelectedValue)).ToArray();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxIDTransaksi.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.IDTransaksi.ToLower().Contains(TextBoxIDTransaksi.Text.ToLower())).ToArray();
                TextBoxIDTransaksi.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxJenisTransaksi.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.TBJenisTransaksi.Nama.ToLower().Contains(TextBoxJenisTransaksi.Text.ToLower())).ToArray();
                TextBoxJenisTransaksi.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxMeja.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.TBMeja.Nama.ToLower().Contains(TextBoxMeja.Text.ToLower())).ToArray();
                TextBoxMeja.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxPengirim.Text))
            {
                DataTransaksi = DataTransaksi
                    .Where(item =>
                        item.IDTempatPengirim.HasValue &&
                        item.TBTempat1.Nama.ToLower().Contains(TextBoxPengirim.Text.ToLower()))
                    .ToArray();
                TextBoxPengirim.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxPenggunaTransaksi.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.TBPengguna.NamaLengkap.ToLower().Contains(TextBoxPenggunaTransaksi.Text.ToLower())).ToArray();
                TextBoxPenggunaTransaksi.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxPelanggan.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.TBPelanggan.NamaLengkap.ToLower().Contains(TextBoxPelanggan.Text.ToLower())).ToArray();
                TextBoxPelanggan.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxKeterangan.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.Keterangan.ToLower().Contains(TextBoxKeterangan.Text.ToLower())).ToArray();
                TextBoxKeterangan.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxGrandtotal.Text))
            {
                DataTransaksi = DataTransaksi.Where(item => item.GrandTotal == Parse.Decimal(TextBoxGrandtotal.Text)).ToArray();
                TextBoxGrandtotal.Focus();
            }

            RepeaterTransaksi.DataSource = DataTransaksi
                .Select(item => new
                {
                    item.IDTransaksi,
                    JenisTransaksi = item.TBJenisTransaksi.Nama,
                    Meja = item.TBMeja.Nama,
                    item.IDStatusTransaksi,
                    item.TanggalTransaksi,
                    PenggunaTransaksi = item.TBPengguna.NamaLengkap,
                    Pelanggan = item.TBPelanggan.NamaLengkap,
                    ClassStatus = item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete ? "fitSize success" : item.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled ? "fitSize danger" : "fitSize warning",
                    Status = item.TBStatusTransaksi.Nama,
                    item.JumlahProduk,
                    item.GrandTotal,
                    item.Keterangan,
                    Pengirim = item.IDTempatPengirim.HasValue ? item.TBTempat1.Nama : "",

                    UbahOrder = item.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment,
                    TransaksiBaru = item.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled,
                    Batal = item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled,
                    Retur = Pengguna.PointOfSales == TipePointOfSales.Retail && item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete && item.TBTransaksiDetails.Where(item2 => item2.Quantity > 0).Count() > 0
                })
                .OrderByDescending(item => item.TanggalTransaksi)
                .ToArray();

            RepeaterTransaksi.DataBind();
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
    protected void RepeaterTransaksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Batal")
        {
            HiddenFieldIDTransaksi.Value = e.CommandArgument.ToString();
            LabelKeterangan.Text = "Batal Transaksi #" + e.CommandArgument.ToString();

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

            if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.MembatalkanTransaksi))
                BatalTransaksi();
            else
            {
                TextBoxUsername.Text = string.Empty;
                TextBoxPassword.Text = string.Empty;
                TextBoxUsername.Focus();
                ModalPopupExtenderSupervisor.Show();
            }
        }
    }
    protected void ButtonBatal_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = new PenggunaLogin(TextBoxUsername.Text, TextBoxPassword.Text);

        if (Pengguna.IDPengguna > 0)
        {
            Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

            if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.MembatalkanTransaksi))
                BatalTransaksi();
            else
                ModalPopupExtenderSupervisor.Show();
        }
        else
            ModalPopupExtenderSupervisor.Show();
    }
    private void BatalTransaksi()
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Transaksi_Class Transaksi = new Transaksi_Class(HiddenFieldIDTransaksi.Value, Pengguna.IDPengguna);

            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;

            Transaksi.ConfirmTransaksi();

            if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
            {
                //PENGURANGAN BAHAN BAKU
                StokBahanBaku_Class.ProduksiByTransaksiBatal(Transaksi.IDPenggunaTransaksi, Transaksi.IDTempat, HiddenFieldIDTransaksi.Value);
            }

            LoadData();

            HiddenFieldIDTransaksi.Value = null;
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }

    protected void RepeaterTransaksi_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

            bool Konfigurasi = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSTempatPengirim);

            PanelPengirim1.Visible = Konfigurasi;
            PanelPengirim2.Visible = Konfigurasi;
            HtmlTableCell PanelPengirim = (HtmlTableCell)e.Item.FindControl("PanelPengirim");
            PanelPengirim.Visible = Konfigurasi;
        }
    }
}