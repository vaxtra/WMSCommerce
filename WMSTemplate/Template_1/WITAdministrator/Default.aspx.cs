using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RendFramework;

public partial class WITAdministrator_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                #region VALIDASI STORE KEY
                StoreKey_Class ClassStoreKey = new StoreKey_Class(db);

                EnumAlert enumAlert = ClassStoreKey.Validasi();

                if (enumAlert == EnumAlert.danger)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ClassStoreKey.MessageDanger);
                else if (enumAlert == EnumAlert.warning)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, ClassStoreKey.MessageWarning);
                else
                    LiteralWarning.Text = "";
                #endregion
            }

            LoadData();
        }
    }

    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadStokHabis(new Konfigurasi_Class(((PenggunaLogin)Session["PenggunaLogin"]).IDGrupPengguna));
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            DataDisplay DataDisplay = new DataDisplay();

            if (DataDisplay.Prev(DropDownListHalaman))
                LoadStokHabis(new Konfigurasi_Class(((PenggunaLogin)Session["PenggunaLogin"]).IDGrupPengguna));
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonNext_Click(object sender, EventArgs e)
    {
        try
        {
            DataDisplay DataDisplay = new DataDisplay();

            if (DataDisplay.Next(DropDownListHalaman))
                LoadStokHabis(new Konfigurasi_Class(((PenggunaLogin)Session["PenggunaLogin"]).IDGrupPengguna));
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    private void LoadData()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

        DateTime[] BulanIni = new DateTime[2];
        DateTime[] BulanLalu = new DateTime[2];
        DateTime[] HariIni = new DateTime[2];
        DateTime[] Kemarin = new DateTime[2];

        BulanIni = Pengaturan.BulanIni();
        BulanLalu = Pengaturan.BulanSebelumnya();
        HariIni = Pengaturan.HariIni();
        Kemarin = Pengaturan.HariSebelumnya();

        #region Aktifitas Transaksi
        if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.AktifitasTransaksi))
        {
            PanelAktifitasTransaksi1.Visible = true;
            //////PanelAktifitasTransaksi2.Visible = true;
            PanelAktifitasTransaksi3.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var _transaksi = db.TBTransaksis
                            .Where(item => item.IDTempat == Pengguna.IDTempat &&
                                item.TanggalOperasional.Value.Date >= BulanLalu[0] &&
                                item.TanggalOperasional.Value.Date <= BulanIni[1] &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                            .Select(item => new
                            {
                                item.IDTempat,
                                item.TanggalOperasional,
                                item.JumlahProduk,
                                item.GrandTotal
                            }).ToArray();

                var _transaksiHariIni = _transaksi
                                            .Where(item =>
                                                item.TanggalOperasional.Value.Date >= HariIni[0] &&
                                                item.TanggalOperasional.Value.Date <= HariIni[1]).ToArray();

                var _transaksiKemarin = _transaksi
                                            .Where(item =>
                                                item.TanggalOperasional.Value.Date >= Kemarin[0] &&
                                                item.TanggalOperasional.Value.Date <= Kemarin[1]).ToArray();

                var _pelanggan = db.TBPelanggans
                                            .Where(item =>
                                                item.TanggalDaftar.Value.Date >= BulanLalu[0] &&
                                                item.TanggalDaftar.Value.Date <= BulanIni[1]).ToArray();

                var _pelangganBulanLalu = db.TBPelanggans
                            .Where(item =>
                                item.TanggalDaftar.Value.Date >= BulanLalu[0] &&
                                item.TanggalDaftar.Value.Date <= BulanLalu[1]).Count();

                #region GRAFIK TRANSAKSI
                string ResultTransaksi = string.Empty;

                var _dataTransaksi = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanIni[0] && item.TanggalOperasional.Value.Date <= BulanIni[1])
                    .GroupBy(item => item.TanggalOperasional.Value.Day)
                    .Select(item => new
                    {
                        Key = item.Key,
                        GrandTotal = item.Sum(item2 => item2.GrandTotal) ?? 0
                    }).ToArray();

                Random rd = new Random();

                string[] labelsXTanggal = Manage.GetRangeDayOfMonth(Manage.GetJamServer());

                ReportChart_Class ClassReport = new ReportChart_Class();
                ReportChartLine_Class LineSingle = new ReportChartLine_Class();
                LineSingle.Label = "My dataset";
                LineSingle.Color = Manage.GetHexadecimalSAP(EnumColorSAP.Hue1);
                LineSingle.Data = new List<string>();
                foreach (var item in labelsXTanggal)
                {
                    if (_dataTransaksi.FirstOrDefault(item2 => item2.Key.ToString() == item) != null)
                    {
                        LineSingle.Data.Add(_dataTransaksi.FirstOrDefault(item2 => item2.Key.ToString() == item).GrandTotal.ToString());
                    }
                    else
                    {
                        LineSingle.Data.Add("0");
                    }
                }
                LiteralChartPenjualan.Text = ClassReport.GetChartTrendAnalysis("CanvasChartPenjualan", string.Empty, "Tanggal", "Sales", labelsXTanggal, LineSingle);
                #endregion

                #region GRAFIK TRANSAKSI PELANGGAN
                //LiteralChart.Text += "<script> $(function () { var dataChart = [";

                //var dataTransaksi = _transaksiBulanIni
                //    .GroupBy(item => item.TanggalOperasional.Value.Date)
                //    .Select(item => new
                //    {
                //        Key = item.Key,
                //        Jumlah = item.Count()
                //    }).ToArray();

                //var dataPelanggan = _pelangganBulanIni
                //    .GroupBy(item => item.TanggalDaftar.Value.Date)
                //    .Select(item => new
                //    {
                //        Key = item.Key,
                //        Jumlah = item.Count()
                //    }).ToArray();

                //for (DateTime date = BulanIni[0]; date <= BulanIni[1]; date = date.AddDays(1))
                //{
                //    var _transaksi = dataTransaksi.FirstOrDefault(item => item.Key.Date == date.Date);
                //    int _jumlahTransaksi = 0;

                //    if (_transaksi != null)
                //        _jumlahTransaksi = _transaksi.Jumlah;

                //    var _pelanggan = dataPelanggan.FirstOrDefault(item => item.Key.Date == date.Date);
                //    int _jumlahPelanggan = 0;

                //    if (_pelanggan != null)
                //        _jumlahPelanggan = _pelanggan.Jumlah;

                //    LiteralChart.Text += "{ 'y': '" + date.Day + "', 'Transaksi': " + _jumlahTransaksi + ", 'Pelanggan': " + _jumlahPelanggan + " },";
                //}

                //LiteralChart.Text += "]; Morris.Line({ element: 'graph', data: dataChart, xkey: 'y',";

                //LiteralChart.Text += "ykeys: ['Transaksi', 'Pelanggan'],";
                //LiteralChart.Text += "labels: ['Transaksi', 'Pelanggan'],";
                //LiteralChart.Text += "lineColors:['#0aa699','#d1dade'],";

                //LiteralChart.Text += " parseTime: false}); eval(dataChart); });";
                //LiteralChart.Text += "</script>";
                #endregion

                LabelPenjualanBulanIni.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanIni[0] && item.TanggalOperasional.Value.Date <= BulanIni[1]).Sum(item => item.GrandTotal).ToFormatHarga();
                LabelQuantityBulanIni.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanIni[0] && item.TanggalOperasional.Value.Date <= BulanIni[1]).Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganBulanIni.Text = _pelanggan.Where(item => item.TanggalDaftar.Value.Date >= BulanIni[0] && item.TanggalDaftar.Value.Date <= BulanIni[1]).Count().ToFormatHargaBulat();
                LabelTransaksiBulanIni.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanIni[0] && item.TanggalOperasional.Value.Date <= BulanIni[1]).Count().ToFormatHargaBulat();

                LabelPenjualanBulanLalu.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanLalu[0] && item.TanggalOperasional.Value.Date <= BulanLalu[1]).Sum(item => item.GrandTotal).ToFormatHarga();
                LabelQuantityBulanLalu.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanLalu[0] && item.TanggalOperasional.Value.Date <= BulanLalu[1]).Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganBulanLalu.Text = _pelanggan.Where(item => item.TanggalDaftar.Value.Date >= BulanLalu[0] && item.TanggalDaftar.Value.Date <= BulanLalu[1]).ToFormatHargaBulat();
                LabelTransaksiBulanLalu.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= BulanLalu[0] && item.TanggalOperasional.Value.Date <= BulanLalu[1]).Count().ToFormatHargaBulat();

                LabelPenjualanHariIni.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= HariIni[0] && item.TanggalOperasional.Value.Date <= HariIni[1]).Sum(item => item.GrandTotal).ToFormatHarga();
                LabelQuantityHariIni.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= HariIni[0] && item.TanggalOperasional.Value.Date <= HariIni[1]).Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganHariIni.Text = _pelanggan.Where(item => item.TanggalDaftar.Value.Date >= HariIni[0] && item.TanggalDaftar.Value.Date <= HariIni[1]).Count().ToFormatHargaBulat();
                LabelTransaksiHariIni.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= HariIni[0] && item.TanggalOperasional.Value.Date <= HariIni[1]).Count().ToFormatHargaBulat();

                LabelPenjualanKemarin.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= Kemarin[0] && item.TanggalOperasional.Value.Date <= Kemarin[1]).Sum(item => item.GrandTotal).ToFormatHarga();
                LabelQuantityKemarin.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= Kemarin[0] && item.TanggalOperasional.Value.Date <= Kemarin[1]).Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganKemarin.Text = _pelanggan.Where(item => item.TanggalDaftar.Value.Date >= Kemarin[0] && item.TanggalDaftar.Value.Date <= Kemarin[1]).Count().ToFormatHargaBulat();
                LabelTransaksiKemarin.Text = _transaksi.Where(item => item.TanggalOperasional.Value.Date >= Kemarin[0] && item.TanggalOperasional.Value.Date <= Kemarin[1]).Count().ToFormatHargaBulat();
            }
        }
        else
        {
            PanelAktifitasTransaksi1.Visible = false;
            //////PanelAktifitasTransaksi2.Visible = false;
            PanelAktifitasTransaksi3.Visible = false;
        }
        #endregion

        #region Transaksi Terakhir
        if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.TransaksiTerakhir))
        {
            panelTransaksiTerakhir.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var TransaksiTerakhir = db.TBTransaksis
                    .Where(item => item.IDStatusTransaksi.HasValue && item.IDTempat == Pengguna.IDTempat)
                    .Select(item => new
                    {
                        item.IDTransaksi,
                        item.IDTempat,
                        item.Nomor,
                        item.TanggalTransaksi,
                        Persentase = Persentase(item.IDStatusTransaksi.Value, item.TBStatusTransaksi.Nama),
                        item.JumlahProduk,
                        item.GrandTotal
                    }).OrderByDescending(item => item.Nomor).Take(10).ToArray();

                RepeaterOrder.DataSource = TransaksiTerakhir;
                RepeaterOrder.DataBind();
            }
        }
        else
            panelTransaksiTerakhir.Visible = false;
        #endregion

        LoadStokHabis(Konfigurasi_Class);

        #region PO Bahan BakuJatuh Tempo

        if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POBahanBakuJatuhTempo))
        {
            PanelPOBahanBakuJatuhTempo.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                decimal batas = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)EnumStoreKonfigurasi.JumlahHariSebelumJatuhTempo).Pengaturan.ToDecimal();
                RepeaterPOBahanBakuJatuhTempo.DataSource = db.TBPOProduksiBahanBakus
                                .Where(item => item.IDTempat == Pengguna.IDTempat && item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas))
                                .Select(item => new
                                {
                                    ClassWarna = Warna((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays), batas),
                                    item.IDPOProduksiBahanBaku,
                                    item.TBSupplier.Nama,
                                    item.Tanggal,
                                    item.TanggalJatuhTempo,
                                })
                                .OrderBy(item => item.Tanggal)
                                .ToArray();
                RepeaterPOBahanBakuJatuhTempo.DataBind();
            }
        }
        else
            PanelPOBahanBakuJatuhTempo.Visible = false;
        #endregion

        #region PO Bahan BakuJatuh Tempo

        if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POProdukJatuhTempo))
        {
            PanelPOBahanBakuJatuhTempo.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                decimal batas = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)EnumStoreKonfigurasi.JumlahHariSebelumJatuhTempo).Pengaturan.ToDecimal();
                RepeaterPOProdukJatuhTempo.DataSource = db.TBPOProduksiProduks
                                .Where(item => item.IDTempat == Pengguna.IDTempat && item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas))
                                .Select(item => new
                                {
                                    ClassWarna = Warna((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays), batas),
                                    item.IDPOProduksiProduk,
                                    item.TBVendor.Nama,
                                    item.Tanggal,
                                    item.TanggalJatuhTempo
                                })
                                .OrderBy(item => item.Tanggal)
                                .ToArray();
                RepeaterPOProdukJatuhTempo.DataBind();
            }
        }
        else
            PanelPOProdukJatuhTempo.Visible = false;
        #endregion
    }

    private void LoadStokHabis(Konfigurasi_Class Konfigurasi_Class)
    {
        #region Stok Produk Habis
        if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.StokProdukHabis))
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            DataDisplay DataDisplay = new DataDisplay();
            panelStokProdukHabis.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var ListData = db.TBStokProduks
                                .Where(item => item.Jumlah <= item.JumlahMinimum && item.IDTempat == Pengguna.IDTempat)
                                .Select(item => new
                                {
                                    item.IDTempat,
                                    Tempat = item.TBTempat.Nama,
                                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                                    item.JumlahMinimum,
                                    item.Jumlah
                                }).OrderBy(item => item.Jumlah).ToArray();

                int skip = 0;
                int take = 0;
                int count = ListData.Count();

                DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

                RepeaterDataStokProduk.DataSource = ListData.Skip(skip).Take(take);
                RepeaterDataStokProduk.DataBind();
            }
        }
        else
            panelStokProdukHabis.Visible = false;
        #endregion
    }
    protected void DropDownListTempat_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    private string Warna(int jumlahHari, decimal batas)
    {
        if (jumlahHari <= batas && jumlahHari > Math.Floor(batas / 2))
            return string.Empty;
        else if (jumlahHari <= Math.Floor(batas / 2) && jumlahHari > 0)
            return "warning";
        else
            return "danger";
    }

    public static string Persentase(int idStatusTransaksi, string statusTransaksi)
    {
        switch (idStatusTransaksi)
        {
            case 1: return "<div class=\"progress\"><div class=\"progress-bar bg-danger\" role=\"progressbar\" style=\"width: 20%\" aria-valuenow=\"20\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + statusTransaksi + "</div></div>";
            case 2: return "<div class=\"progress\"><div class=\"progress-bar bg-warning\" role=\"progressbar\" style=\"width: 40%\" aria-valuenow=\"40\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + statusTransaksi + "</div></div>";
            case 3: return "<div class=\"progress\"><div class=\"progress-bar bg-warning\" role=\"progressbar\" style=\"width: 60%\" aria-valuenow=\"60\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + statusTransaksi + "</div></div>";
            case 4: return "<div class=\"progress\"><div class=\"progress-bar bg-success\" role=\"progressbar\" style=\"width: 80%\" aria-valuenow=\"80\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + statusTransaksi + "</div></div>";
            case 5: return "<div class=\"progress\"><div class=\"progress-bar bg-success\" role=\"progressbar\" style=\"width: 100%\" aria-valuenow=\"100\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + statusTransaksi + "</div></div>";
            case 6: return "<div class=\"progress\"><div class=\"progress-bar bg-danger\" role=\"progressbar\" style=\"width: 100%\" aria-valuenow=\"100\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + statusTransaksi + "</div></div>";
            default: return string.Empty;
        }
    }
}