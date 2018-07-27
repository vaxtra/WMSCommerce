using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Realtime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataBind();

                DropDownListTempat.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });
            }

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (!string.IsNullOrWhiteSpace(Request.QueryString["idTempat"]))
                DropDownListTempat.SelectedValue = Request.QueryString["idTempat"];
            else
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

            if (!string.IsNullOrWhiteSpace(Request.QueryString["refresh"]))
            {
                DropDownListDurasiRefresh.SelectedValue = Request.QueryString["refresh"];
                LiteralRefresh.Text = "<meta http-equiv=\"refresh\" content=\"" + Request.QueryString["refresh"].ToInt() * 60 + "\">";
            }
            else
            {
                DropDownListDurasiRefresh.SelectedValue = "1";
                LiteralRefresh.Text = "<meta http-equiv=\"refresh\" content=\"60\">";
            }

            LoadData();
        }
    }
    private void LoadData()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        //Literal LiteralChart = (Literal)this.Page.Master.FindControl("LiteralChart");
        LiteralChart.Text = string.Empty;

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
            PanelAktifitasTransaksi2.Visible = true;
            PanelAktifitasTransaksi3.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var _transaksiBulanIni = db.TBTransaksis
                            .Where(item =>
                                item.TanggalTransaksi.Value.Date >= BulanIni[0] &&
                                item.TanggalTransaksi.Value.Date <= BulanIni[1] &&
                                item.IDStatusTransaksi == 5)
                            .Select(item => new
                            {
                                item.IDTempat,
                                item.TanggalTransaksi,
                                item.JumlahProduk,
                                item.GrandTotal
                            }).ToArray();

                if (DropDownListTempat.SelectedValue != "0")
                    _transaksiBulanIni = _transaksiBulanIni.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();

                var _transaksiHariIni = _transaksiBulanIni
                                            .Where(item =>
                                                item.TanggalTransaksi.Value.Date >= HariIni[0] &&
                                                item.TanggalTransaksi.Value.Date <= HariIni[1]).ToArray();

                var _transaksiKemarin = _transaksiBulanIni
                                            .Where(item =>
                                                item.TanggalTransaksi.Value.Date >= Kemarin[0] &&
                                                item.TanggalTransaksi.Value.Date <= Kemarin[1]).ToArray();

                var _transaksiBulanLalu = db.TBTransaksis
                                            .Where(item =>
                                                item.TanggalTransaksi.Value.Date >= BulanLalu[0] &&
                                                item.TanggalTransaksi.Value.Date <= BulanLalu[1] &&
                                                item.IDStatusTransaksi == 5)
                                            .Select(item => new
                                            {
                                                item.IDTempat,
                                                item.TanggalTransaksi,
                                                item.JumlahProduk,
                                                item.GrandTotal
                                            }).ToArray();

                if (DropDownListTempat.SelectedValue != "0")
                    _transaksiBulanLalu = _transaksiBulanLalu.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();

                var _pelangganBulanIni = db.TBPelanggans
                                            .Where(item =>
                                                item.TanggalDaftar.Value.Date >= BulanIni[0] &&
                                                item.TanggalDaftar.Value.Date <= BulanIni[1]).ToArray();

                var _pelangganHariIni = _pelangganBulanIni
                                            .Where(item =>
                                                item.TanggalDaftar.Value.Date >= HariIni[0] &&
                                                item.TanggalDaftar.Value.Date <= HariIni[1]).Count();

                var _pelangganKemarin = _pelangganBulanIni
                                            .Where(item =>
                                                item.TanggalDaftar.Value.Date >= Kemarin[0] &&
                                                item.TanggalDaftar.Value.Date <= Kemarin[1]).Count();

                var _pelangganBulanLalu = db.TBPelanggans
                            .Where(item =>
                                item.TanggalDaftar.Value.Date >= BulanLalu[0] &&
                                item.TanggalDaftar.Value.Date <= BulanLalu[1]).Count();

                #region GRAFIK TRANSAKSI
                string ResultTransaksi = string.Empty;

                ResultTransaksi += "var dataChart = [";

                var _dataTransaksi = _transaksiBulanIni
                                        .GroupBy(item => item.TanggalTransaksi.Value.Date)
                                        .Select(item => new
                                        {
                                            Key = item.Key,
                                            GrandTotal = item.Sum(item2 => item2.GrandTotal) ?? 0
                                        }).ToArray();

                for (DateTime date = BulanIni[0]; date <= BulanIni[1]; date = date.AddDays(1))
                {
                    var _transaksi = _dataTransaksi.FirstOrDefault(item => item.Key.Date == date.Date);
                    decimal _grandTotal = 0;

                    if (_transaksi != null)
                        _grandTotal = _transaksi.GrandTotal;

                    ResultTransaksi += "{ 'y': '" + date.Day + "', 'Transaksi': " + _grandTotal + " }, ";
                }

                ResultTransaksi += "]; Morris.Area({ element: 'graph2', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

                ResultTransaksi += "ykeys: ['Transaksi'], ";
                ResultTransaksi += "labels: ['Transaksi'], ";
                ResultTransaksi += "lineColors:['#0aa699'], ";
                ResultTransaksi += " parseTime: false}); eval(dataChart);";

                LiteralChart.Text += "<script>";
                LiteralChart.Text += "$(document).ready(function () { " + ResultTransaksi + " }); ";
                LiteralChart.Text += "function pageLoad(sender, args) { if (args.get_isPartialLoad()) { " + ResultTransaksi + " }}; ";
                LiteralChart.Text += "</script>";
                #endregion

                #region GRAFIK TRANSAKSI PELANGGAN
                LiteralChart.Text += "<script> $(function () { var dataChart = [";

                var dataTransaksi = _transaksiBulanIni
                    .GroupBy(item => item.TanggalTransaksi.Value.Date)
                    .Select(item => new
                    {
                        Key = item.Key,
                        Jumlah = item.Count()
                    }).ToArray();

                var dataPelanggan = _pelangganBulanIni
                    .GroupBy(item => item.TanggalDaftar.Value.Date)
                    .Select(item => new
                    {
                        Key = item.Key,
                        Jumlah = item.Count()
                    }).ToArray();

                for (DateTime date = BulanIni[0]; date <= BulanIni[1]; date = date.AddDays(1))
                {
                    var _transaksi = dataTransaksi.FirstOrDefault(item => item.Key.Date == date.Date);
                    int _jumlahTransaksi = 0;

                    if (_transaksi != null)
                        _jumlahTransaksi = _transaksi.Jumlah;

                    var _pelanggan = dataPelanggan.FirstOrDefault(item => item.Key.Date == date.Date);
                    int _jumlahPelanggan = 0;

                    if (_pelanggan != null)
                        _jumlahPelanggan = _pelanggan.Jumlah;

                    LiteralChart.Text += "{ 'y': '" + date.Day + "', 'Transaksi': " + _jumlahTransaksi + ", 'Pelanggan': " + _jumlahPelanggan + " },";
                }

                LiteralChart.Text += "]; Morris.Line({ element: 'graph', data: dataChart, xkey: 'y',";

                LiteralChart.Text += "ykeys: ['Transaksi', 'Pelanggan'],";
                LiteralChart.Text += "labels: ['Transaksi', 'Pelanggan'],";
                LiteralChart.Text += "lineColors:['#0aa699','#d1dade'],";

                LiteralChart.Text += " parseTime: false}); eval(dataChart); });";
                LiteralChart.Text += "</script>";
                #endregion

                var GrandTotalBulanIni = _transaksiBulanIni.Sum(item => item.GrandTotal);
                var GrandTotalBulanLalu = _transaksiBulanLalu.Sum(item => item.GrandTotal);

                if (GrandTotalBulanIni >= GrandTotalBulanLalu)
                {
                    LabelAktifitasBulanIni.Attributes.Add("class", "label label-success");
                    LabelAktifitasBulanLalu.Attributes.Add("class", "label label-danger");
                }
                else
                {
                    LabelAktifitasBulanIni.Attributes.Add("class", "label label-danger");
                    LabelAktifitasBulanLalu.Attributes.Add("class", "label label-success");
                }

                LabelPenjualanBulanIni.Text = GrandTotalBulanIni.ToFormatHarga();
                LabelQuantityBulanIni.Text = _transaksiBulanIni.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganBulanIni.Text = _pelangganBulanIni.Count().ToFormatHargaBulat();
                LabelTransaksiBulanIni.Text = _transaksiBulanIni.Count().ToFormatHargaBulat();

                LabelPenjualanBulanLalu.Text = GrandTotalBulanLalu.ToFormatHarga();
                LabelQuantityBulanLalu.Text = _transaksiBulanLalu.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganBulanLalu.Text = _pelangganBulanLalu.ToFormatHarga();
                LabelTransaksiBulanLalu.Text = _transaksiBulanLalu.Count().ToFormatHargaBulat();

                var GrandTotalHariIni = _transaksiHariIni.Sum(item => item.GrandTotal);
                var GrandTotalKemarin = _transaksiKemarin.Sum(item => item.GrandTotal);

                if (GrandTotalHariIni >= GrandTotalKemarin)
                {
                    LabelAktifitasHariIni.Attributes.Add("class", "label label-success");
                    LabelAktifitasKemarin.Attributes.Add("class", "label label-danger");
                }
                else
                {
                    LabelAktifitasHariIni.Attributes.Add("class", "label label-danger");
                    LabelAktifitasKemarin.Attributes.Add("class", "label label-success");
                }

                LabelPenjualanHariIni.Text = GrandTotalHariIni.ToFormatHarga();
                LabelQuantityHariIni.Text = _transaksiHariIni.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganHariIni.Text = _pelangganHariIni.ToFormatHarga();
                LabelTransaksiHariIni.Text = _transaksiHariIni.Count().ToFormatHargaBulat();

                LabelPenjualanKemarin.Text = GrandTotalKemarin.ToFormatHarga();
                LabelQuantityKemarin.Text = _transaksiKemarin.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                LabelPelangganKemarin.Text = _pelangganKemarin.ToFormatHarga();
                LabelTransaksiKemarin.Text = _transaksiKemarin.Count().ToFormatHargaBulat();
            }
        }
        else
        {
            PanelAktifitasTransaksi1.Visible = false;
            PanelAktifitasTransaksi2.Visible = false;
            PanelAktifitasTransaksi3.Visible = false;
        }
        #endregion

        #region Stok Produk Habis
        if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.StokProdukHabis))
        {
            panelStokProdukHabis.Visible = true;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var StokProdukHabis = db.TBStokProduks
                                .Where(item => item.Jumlah <= item.JumlahMinimum)
                                .Select(item => new
                                {
                                    item.IDTempat,
                                    Tempat = item.TBTempat.Nama,
                                    item.TBKombinasiProduk.Nama,
                                    item.Jumlah
                                })
                                .OrderBy(item => item.Jumlah)
                                .ToArray();

                if (DropDownListTempat.SelectedValue != "0")
                    StokProdukHabis = StokProdukHabis.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();

                LabelJumlahStokProdukHabis.Text = StokProdukHabis.Count().ToFormatHargaBulat();

                RepeaterDataStokProduk.DataSource = StokProdukHabis;
                RepeaterDataStokProduk.DataBind();
            }
        }
        else
            panelStokProdukHabis.Visible = false;
        #endregion
    }
    protected void DropDownListTempat_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Realtime.aspx?idTempat=" + DropDownListTempat.SelectedValue + "&refresh=" + DropDownListDurasiRefresh.SelectedValue);
    }
    protected void DropDownListDurasiRefresh_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Realtime.aspx?idTempat=" + DropDownListTempat.SelectedValue + "&refresh=" + DropDownListDurasiRefresh.SelectedValue);
    }
}