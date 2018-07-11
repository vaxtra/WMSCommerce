using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_Wholesale_Transaksi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
            LoadData();
        }
    }
    private void LoadData()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
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
                            item.IDJenisTransaksi == (int)EnumJenisTransaksi.Wholesale &&
                            item.IDTempat == Pengguna.IDTempat &&
                            item.TanggalTransaksi.Value.Date >= ((DateTime)ViewState["TanggalAwal"]) &&
                            item.TanggalTransaksi.Value.Date <= ((DateTime)ViewState["TanggalAkhir"])).ToArray();
                }
                else
                {
                    //SESUAI DENGAN PILIHAN
                    DataTransaksi = db.TBTransaksis
                        .Where(item =>
                            item.IDJenisTransaksi == (int)EnumJenisTransaksi.Wholesale &&
                            item.IDTempat == Pengguna.IDTempat &&
                            item.TanggalTransaksi.Value.Date >= ((DateTime)ViewState["TanggalAwal"]) &&
                            item.TanggalTransaksi.Value.Date <= ((DateTime)ViewState["TanggalAkhir"]) &&
                            item.IDStatusTransaksi == DropDownListStatusTransaksi.SelectedValue.ToInt()).ToArray();
                }

                if (!string.IsNullOrWhiteSpace(TextBoxIDTransaksi.Text))
                {
                    DataTransaksi = DataTransaksi.Where(item => item.IDTransaksi.ToLower().Contains(TextBoxIDTransaksi.Text.ToLower())).ToArray();
                    TextBoxIDTransaksi.Focus();
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

                if (!string.IsNullOrWhiteSpace(TextBoxJumlah.Text))
                {
                    DataTransaksi = DataTransaksi.Where(item => item.JumlahProduk == Parse.Decimal(TextBoxJumlah.Text)).ToArray();
                    TextBoxJumlah.Focus();
                }

                if (!string.IsNullOrWhiteSpace(TextBoxGrandtotal.Text))
                {
                    DataTransaksi = DataTransaksi.Where(item => item.GrandTotal == Parse.Decimal(TextBoxGrandtotal.Text)).ToArray();
                    TextBoxGrandtotal.Focus();
                }

                RepeaterTransaksi.DataSource = DataTransaksi
                    .Select(item => new
                    {
                        item.Nomor,
                        item.IDTransaksi,
                        item.IDStatusTransaksi,
                        item.TanggalTransaksi,
                        Pelanggan = item.TBPelanggan.NamaLengkap,
                        ClassStatus = item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete ? "fitSize text-middle success" : item.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled ? "fitSize text-middle danger" : "fitSize text-middle warning",
                        Status = item.TBStatusTransaksi.Nama,
                        item.JumlahProduk,
                        item.GrandTotal,
                        item.Keterangan,

                        UbahOrder = item.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment,
                        TransaksiBaru = item.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled,
                        Batal = item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled
                    })
                    .OrderByDescending(item => item.Nomor)
                    .ToArray();

                RepeaterTransaksi.DataBind();
            }
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

            Transaksi_Model Transaksi = new Transaksi_Model(HiddenFieldIDTransaksi.Value, Pengguna.IDPengguna);

            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;

            Transaksi.ConfirmTransaksi();

            LoadData();

            HiddenFieldIDTransaksi.Value = null;
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }

    #region Default
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
}