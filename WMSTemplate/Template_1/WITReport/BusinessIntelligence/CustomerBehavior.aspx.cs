using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_BusinessIntelligence_CustomerBehavior : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                DropDownListPelanggan.DataSource = ClassPelanggan.Data();
                DropDownListPelanggan.DataTextField = "NamaLengkap";
                DropDownListPelanggan.DataValueField = "IDPelanggan";
                DropDownListPelanggan.DataBind();
                DropDownListPelanggan.Items.Insert(0, new ListItem("All Customers", "0"));

                //DropDownListGender.Items.Insert(0, new ListItem("All Genders", "0"));
                //DropDownListGender.Items.Insert(1, new ListItem("Male", "1"));
                //DropDownListGender.Items.Insert(2, new ListItem("Female", "2"));

                int index = 0;
                for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
                {
                    DropDownListTahun.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                    index++;
                }
                DropDownListTahun.SelectedIndex = 5;

                LabelHeader.Text = "CUSTOMER BEHAVIOR ANALYSIS " + DropDownListTahun.SelectedValue.ToString();
            }
        }
    }
    public void QueryData(int IDpelanggan, int searchBY, int tahun)
    {
        #region Pengaturan Label Tahun
        LabelTahunIni1.Text = tahun.ToString();
        LabelTahunIni2.Text = tahun.ToString();
        LabelTahunLalu1.Text = (tahun - 1).ToString();
        LabelTahunLalu2.Text = (tahun - 1).ToString();
        #endregion

        Literal LiteralChart = (Literal)this.Page.Master.FindControl("LiteralChart");
        LiteralChart.Text = string.Empty;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBTransaksiDetail[] DetailTransaksiDB;
            var KombinasiProdukDB = db.TBKombinasiProduks.ToArray();

            #region Query perilaku belanja pelanggan [average budget, average quantity, most buy category]

            #region Master Query
            //AMBIL DATA MASTER DARI DB
            if (IDpelanggan != 0)
                DetailTransaksiDB = db.TBTransaksiDetails.Where(item => item.TBTransaksi.IDPelanggan == IDpelanggan
                    && item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete
                    && (item.TBTransaksi.TanggalTransaksi.Value.Year == tahun || item.TBTransaksi.TanggalTransaksi.Value.Year == tahun - 1)).ToArray();
            else
                DetailTransaksiDB = db.TBTransaksiDetails.Where(item => item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete
                    && (item.TBTransaksi.TanggalTransaksi.Value.Year == tahun || item.TBTransaksi.TanggalTransaksi.Value.Year == tahun - 1)).ToArray();

            if (DetailTransaksiDB.Count() != 0)
            {
                //TAHUN INI
                var _dataDetailTransaksiTahunIni = DetailTransaksiDB
                                       .Where(item => item.TBTransaksi.TanggalTransaksi.Value.Year == tahun)
                                       .GroupBy(item => item.TBTransaksi.TanggalTransaksi.Value.Month)
                                       .Select(item => new
                                       {
                                           Key = item.Key,
                                           GrandTotal = item.Sum(item2 => item2.Subtotal) == 0 ? 0 : item.Sum(item2 => item2.Subtotal),
                                           TotalQty = item.Sum(item2 => item2.Quantity) == 0 ? 0 : item.Sum(item2 => item2.Quantity),
                                           AverageGrandTotal = item.Average(item2 => item2.TBTransaksi.GrandTotal),
                                           AverageQty = item.Average(item2 => item2.TBTransaksi.JumlahProduk)
                                       }).ToArray();

                var _dataDetailTransaksiTahunIniNoFilter = DetailTransaksiDB
                               .Where(item => item.TBTransaksi.TanggalTransaksi.Value.Year == tahun)
                               .GroupBy(item => item.TBTransaksi.TanggalTransaksi.Value.Month)
                               .Select(item => new
                               {
                                   Key = item.Key,
                                   GrandTotal = item.Sum(item2 => item2.Subtotal) == 0 ? 0 : item.Sum(item2 => item2.Subtotal),
                                   TotalQty = item.Sum(item2 => item2.Quantity) == 0 ? 0 : item.Sum(item2 => item2.Quantity),
                                   AverageGrandTotal = item.Average(item2 => item2.TBTransaksi.GrandTotal),
                                   AverageQty = item.Average(item2 => item2.TBTransaksi.JumlahProduk)
                               }).ToArray();
                //TAHUN LALU
                var _dataDetailTransaksiTahunLalu = DetailTransaksiDB
                                       .Where(item => item.TBTransaksi.TanggalTransaksi.Value.Year == tahun - 1)
                                       .GroupBy(item => item.TBTransaksi.TanggalTransaksi.Value.Month)
                                       .Select(item => new
                                       {
                                           Key = item.Key,
                                           GrandTotal = item.Sum(item2 => item2.Subtotal) == 0 ? 0 : item.Sum(item2 => item2.Subtotal),
                                           TotalQty = item.Sum(item2 => item2.Quantity) == 0 ? 0 : item.Sum(item2 => item2.Quantity),
                                           AverageGrandTotal = item.Average(item2 => item2.TBTransaksi.GrandTotal),
                                           AverageQty = item.Average(item2 => item2.TBTransaksi.JumlahProduk)
                                       }).ToArray();

                var _dataDetailTransaksiTahunLaluNoFilter = DetailTransaksiDB
                               .Where(item => item.TBTransaksi.TanggalTransaksi.Value.Year == tahun - 1)
                               .GroupBy(item => item.TBTransaksi.TanggalTransaksi.Value.Month)
                               .Select(item => new
                               {
                                   Key = item.Key,
                                   GrandTotal = item.Sum(item2 => item2.Subtotal) == 0 ? 0 : item.Sum(item2 => item2.Subtotal),
                                   TotalQty = item.Sum(item2 => item2.Quantity) == 0 ? 0 : item.Sum(item2 => item2.Quantity),
                                   AverageGrandTotal = item.Average(item2 => item2.TBTransaksi.GrandTotal),
                                   AverageQty = item.Average(item2 => item2.TBTransaksi.JumlahProduk)
                               }).ToArray();
                #endregion

                var averageBudget = DetailTransaksiDB.Average(item => item.TBTransaksi.GrandTotal);
                var averageQty = DetailTransaksiDB.Average(item => item.TBTransaksi.JumlahProduk);
                var mostBuyCategory = DetailTransaksiDB.GroupBy(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 1 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama)
                    .Select(item => new
                    {
                        Key = item.Key,
                        Quantity = item.Sum(item2 => item2.Quantity),
                        TotalPenjualan = item.Sum(item2 => item2.Subtotal)
                    }).OrderByDescending(item => item.TotalPenjualan).FirstOrDefault().Key;
                #endregion

                #region Query rekomendasi [ algo people also buy, top product , dead stock ]
                List<string> ListSuggestProduk = new List<string>();

                var rekomendasiByCategory = db.TBKategoriProduks
                            .Where(item => item.TBRekomendasiKategoriProduks.Count > 0 && item.TBRekomendasiKategoriProduks1.Count > 0)
                            .Select(item => new
                            {
                                Nama = item.Nama,
                                Jumlah = (item.TBRekomendasiKategoriProduks.Count > 0 ? item.TBRekomendasiKategoriProduks.Sum(item2 => item2.Jumlah) : 0) + (item.TBRekomendasiKategoriProduks1.Count > 0 ? item.TBRekomendasiKategoriProduks1.Sum(item2 => item2.Jumlah) : 0),
                                Nilai = (item.TBRekomendasiKategoriProduks.Count > 0 ? item.TBRekomendasiKategoriProduks.Sum(item2 => item2.Nilai) : 0) + (item.TBRekomendasiKategoriProduks1.Count > 0 ? item.TBRekomendasiKategoriProduks1.Sum(item2 => item2.Nilai) : 0),
                                Rekomendasi = GabungRekomendasi(item.TBRekomendasiKategoriProduks.ToList(), item.TBRekomendasiKategoriProduks1.ToList())
                            })
                            .OrderByDescending(item => item.Jumlah)
                            .FirstOrDefault();

                var topProduk = db.TBTransaksiDetails.Where(item => item.HargaJual <= averageBudget
                                                ).GroupBy(item => new
                                                {
                                                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                                                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk != null ?
                                                    item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : string.Empty,
                                                    HargaJual = item.TBKombinasiProduk.TBStokProduks.FirstOrDefault().HargaJual
                                                })
                                                .Select(item => new
                                                {
                                                    Key = item.Key,
                                                    Quantity = item.Sum(item2 => item2.Quantity),
                                                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                                                    TotalPenjualan = item.Sum(item2 => item2.Subtotal)
                                                }).OrderByDescending(item => item.TotalPenjualan).Take(20).ToArray();

                var DeadStockProduct = KombinasiProdukDB.Where(item => item.TBStokProduks.FirstOrDefault().HargaJual <= averageBudget)
                .OrderBy(item => item.TanggalDaftar).Take(20).ToArray();

                //var suggest1_TopProduk_FilteredByPeopleAlsoBuy = topProduk.Where(item => item.Key.HargaJual <= averageBudget &&
                //                                                 item.Key.Kategori == rekomendasiByCategory.Nama).Take(2);

                for (int i = 0; i < topProduk.Count(); i++)
                {
                    if (ListSuggestProduk.Count != 0)
                    {
                        if (ListSuggestProduk[i].ToString() != topProduk[i].Key.Produk)
                        {
                            ListSuggestProduk.Add(topProduk[i].Key.Produk);
                        }

                        if (ListSuggestProduk[i].ToString() != DeadStockProduct[i].Nama)
                        {
                            ListSuggestProduk.Add(DeadStockProduct[i].Nama);
                        }
                    }
                    else
                    {
                        ListSuggestProduk.Add(topProduk[i].Key.Produk);
                        ListSuggestProduk.Add(DeadStockProduct[i].Nama);
                    }
                }

                RepeaterSuggestProduct.DataSource = ListSuggestProduk.Select(item => new
                {
                    Nama = item.ToString()
                }).Take(10).ToArray();
                RepeaterSuggestProduct.DataBind();

                //var suggest1_TopProduk_FilteredByPeopleAlsoBuy = topProduk.Where(item => item.Key.HargaJual <= averageBudget).Take(2);

                //var suggest2_TopProduk = topProduk.Where(item => item.Key.HargaJual <= averageBudget).Take(2);

                //var suggest3_deadStock = KombinasiProdukDB.Where(item => item.TBProduk.TBRelasiProdukKategoriProduks.Count == 1 ?
                //    item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama == rekomendasiByCategory.Nama && item.TBStokProduks.FirstOrDefault().HargaJual <= averageBudget :
                //    item.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama == rekomendasiByCategory.Nama && item.TBStokProduks.FirstOrDefault().HargaJual <= averageBudget)
                //    .OrderBy(item => item.TanggalDaftar).Take(2);

                var suggest3_deadStock = KombinasiProdukDB.Where(item => item.TBProduk.TBRelasiProdukKategoriProduks.Count == 1 ?
                    item.TBStokProduks.FirstOrDefault().HargaJual <= averageBudget :
                item.TBStokProduks.FirstOrDefault().HargaJual <= averageBudget)
                .OrderBy(item => item.TanggalDaftar).Take(2);

                #endregion

                #region Line Chart - Customer Analysis Monthly (tahun ini dan tahun lalu)
                LiteralChart.Text += "<script> $(function () { var dataChart = [";

                //If searched by Penjualan
                if (searchBY == 1)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var _transaksiTahunIni = _dataDetailTransaksiTahunIniNoFilter.FirstOrDefault(item => item.Key == i);
                        var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluNoFilter.FirstOrDefault(item => item.Key == i);

                        decimal averagePenjualanTahunIni = 0, averagePenjualanTahunLalu = 0;

                        if (_transaksiTahunIni != null)
                            averagePenjualanTahunIni = (decimal)_transaksiTahunIni.AverageGrandTotal;

                        if (_transaksiTahunLalu != null)
                            averagePenjualanTahunLalu = (decimal)_transaksiTahunLalu.AverageGrandTotal;

                        LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + Math.Ceiling(averagePenjualanTahunLalu) + ", '" + tahun + "': " + Math.Ceiling(averagePenjualanTahunIni) + " }, ";
                    }
                }
                else //searched by Qty
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var _transaksiTahunIni = _dataDetailTransaksiTahunIniNoFilter.FirstOrDefault(item => item.Key == i);
                        var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluNoFilter.FirstOrDefault(item => item.Key == i);

                        decimal averageQtyTahunIni = 0, averageQtyTahunLalu = 0;

                        if (_transaksiTahunIni != null)
                            averageQtyTahunIni = (decimal)_transaksiTahunIni.AverageQty;

                        if (_transaksiTahunLalu != null)
                            averageQtyTahunLalu = (decimal)_transaksiTahunLalu.AverageQty;

                        LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + Math.Ceiling(averageQtyTahunLalu) + ", '" + tahun + "': " + Math.Ceiling(averageQtyTahunIni) + " }, ";
                    }
                }

                LiteralChart.Text += "]; Morris.Line({ element: 'graph11', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

                LiteralChart.Text += "ykeys: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                LiteralChart.Text += "labels: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                LiteralChart.Text += "lineColors:['#7FC4C1','#FF625D'],";
                LiteralChart.Text += " parseTime: false}); eval(dataChart); }); </script>";
                #endregion

                #region RepeaterDetail dari Trend Analysis Monthly
                PanelDetail.Visible = true;
                //Group by Month & Year, ini digunakan untuk tampilan repeater detail
                var _dataDetail2Tahun = DetailTransaksiDB
                                              .GroupBy(item => new
                                              {
                                                  item.TBTransaksi.TanggalTransaksi.Value.Month,
                                                  item.TBTransaksi.TanggalTransaksi.Value.Year
                                              })
                                              .Select(item => new
                                              {
                                                  Key = item.Key,
                                                  GrandTotal = item.Sum(item2 => item2.Subtotal),
                                                  TotalTransaksi = item.Count(),
                                                  TotalNominalDiscount = item.Sum(item2 => item2.Discount) == 0 ? 0 : item.Sum(item2 => item2.Discount * item2.Quantity),
                                                  AverageGrandTotal = item.Average(item2 => item2.TBTransaksi.GrandTotal),
                                                  AverageQty = item.Average(item2 => item2.TBTransaksi.JumlahProduk),
                                              }).ToArray();

                List<int> Bulan = new List<int>();
                for (int i = 1; i <= 12; i++)
                    Bulan.Add(i);

                if (searchBY == 1)
                {
                    PanelReportPenjualan.Visible = true;
                    PanelReportQty.Visible = false;

                    var Data = Bulan.Select(item => new
                    {
                        Bulan = new DateTime(DropDownListTahun.SelectedValue.ToInt(), item, 1).ToString("MMMM"),
                        TotalTransaksiTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.TotalTransaksi),
                        TotalTransaksiTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.TotalTransaksi),
                        averageGrandTotalTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : Math.Ceiling((decimal)_dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.AverageGrandTotal)),
                        averageGrandTotalTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : Math.Ceiling((decimal)_dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.AverageGrandTotal)),
                    }).ToArray();

                    RepeaterDetail.DataSource = Data;
                    RepeaterDetail.DataBind();

                    #region Labelling
                    LabelAverageSalesTahunIni.Text = Math.Ceiling(Data.Average(item => item.averageGrandTotalTahunIni)).ToFormatHarga();
                    LabelAverageSalesTahunLalu.Text = Math.Ceiling(Data.Average(item => item.averageGrandTotalTahunLalu)).ToFormatHarga();
                    LabelTotalAverageSales.Text = Pertumbuhan(Math.Ceiling(Data.Average(item => item.averageGrandTotalTahunIni - item.averageGrandTotalTahunLalu)).ToDecimal());
                    LabelTotalGrowthSalesVolumePanel1.Text = Pertumbuhan(Math.Ceiling(Data.Average(item => item.TotalTransaksiTahunIni - item.TotalTransaksiTahunLalu)).ToDecimal());
                    LabelSalesVolumeTahunIniPanel1.Text = Math.Ceiling(Data.Average(item => item.TotalTransaksiTahunIni)).ToFormatHargaBulat();
                    LabelSalesVolumeTahunLaluPanel1.Text = Math.Ceiling(Data.Average(item => item.TotalTransaksiTahunLalu)).ToFormatHargaBulat();
                    #endregion
                }
                else
                {
                    PanelReportPenjualan.Visible = false;
                    PanelReportQty.Visible = true;

                    var Data = Bulan.Select(item => new
                    {
                        Bulan = new DateTime(DropDownListTahun.SelectedValue.ToInt(), item, 1).ToString("MMMM"),
                        TotalTransaksiTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.TotalTransaksi),
                        TotalTransaksiTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.TotalTransaksi),
                        averageGrandTotalTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : Math.Ceiling(_dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.AverageQty).ToDecimal()),
                        averageGrandTotalTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : Math.Ceiling(_dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.AverageQty).ToDecimal()),
                    }).ToArray();

                    RepeaterDetailQty.DataSource = Data;
                    RepeaterDetailQty.DataBind();

                    #region Labelling
                    LabelAverageQtyTahunIni.Text = Math.Ceiling(Data.Average(item => item.averageGrandTotalTahunIni)).ToFormatHargaBulat();
                    LabelAverageQtyTahunLalu.Text = Math.Ceiling(Data.Average(item => item.averageGrandTotalTahunLalu)).ToFormatHargaBulat();
                    LabelTotalAvgItems.Text = Pertumbuhan(Math.Ceiling(Data.Average(item => item.averageGrandTotalTahunIni - item.averageGrandTotalTahunLalu)).ToDecimal());
                    LabelTotalGrowthSalesVolumePanel2.Text = Pertumbuhan(Math.Ceiling(Data.Average(item => item.TotalTransaksiTahunIni - item.TotalTransaksiTahunLalu)).ToDecimal());
                    LabelSalesVolumeTahunIniPanel2.Text = Math.Ceiling(Data.Average(item => item.TotalTransaksiTahunIni)).ToFormatHargaBulat();
                    LabelSalesVolumeTahunLaluPanel2.Text = Math.Ceiling(Data.Average(item => item.TotalTransaksiTahunLalu)).ToFormatHargaBulat();
                    #endregion

                }
                #endregion

                #region DONUT TOP PRODUCT
                var _dataDetailTransaksiTahunIniDatabase = DetailTransaksiDB
                                    .Where(item => item.TBTransaksi.TanggalTransaksi.Value.Year == tahun && item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete);

                var TotalQty = _dataDetailTransaksiTahunIniDatabase.Sum(item => item.Quantity);
                var TotalPenjualan = _dataDetailTransaksiTahunIniDatabase.Sum(item => item.Subtotal);

                var _data = _dataDetailTransaksiTahunIniDatabase.GroupBy(item => new
                {
                    Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    Varian = item.TBKombinasiProduk.TBAtributProduk.Nama ?? string.Empty,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama ?? string.Empty,
                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama ?? string.Empty,
                    Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama ?? string.Empty

                })
                    .Select(item => new
                    {
                        Key = item.Key,
                        Quantity = item.Sum(item2 => item2.Quantity),
                        TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                        TotalPenjualan = item.Sum(item2 => item2.Subtotal),
                        PersentaseQty = ((item.Sum(item2 => item2.Quantity)) * 100) / TotalQty,
                        PersentasePenjualan = ((item.Sum(item2 => item2.Subtotal)) * 100) / TotalPenjualan
                    });
                //Quantity
                var _sortedDataQty = _data.OrderByDescending(item => item.Quantity).Take(10).ToArray().ToArray();
                //Penjualan
                var _sortedDataPenjualan = _data.OrderByDescending(item => item.TotalPenjualan).Take(10).ToArray().ToArray();

                LiteralChart.Text += "<script> $(function () { var dataChart = [";
                foreach (var item in _sortedDataQty)
                {
                    LiteralChart.Text += "{ 'label': '" + item.Key.Produk + "', 'value': " + item.Quantity + "}, ";
                }
                LiteralChart.Text += "]; Morris.Donut({ element: 'graph7', behaveLikeLine: true, data: dataChart, xkey: 'y', ";


                LiteralChart.Text += "colors:['#A9ED9B','#7FC4C1','#C320C4','#6168FF','#EDE18E','#73C489','#74A5FF','#FFC7FE','#00FFF7','#FFCBA0'], ";
                LiteralChart.Text += "resize:true});}); </script>";

                LiteralChart.Text += "<script> $(function () { var dataChart = [";
                foreach (var item in _sortedDataPenjualan)
                {
                    LiteralChart.Text += "{ 'label': '" + item.Key.Produk + "', 'value': " + item.TotalPenjualan + "}, ";
                }
                LiteralChart.Text += "]; Morris.Donut({ element: 'graph8', behaveLikeLine: true, data: dataChart, xkey: 'y', ";


                LiteralChart.Text += "colors:['#A9ED9B','#7FC4C1','#C320C4','#6168FF','#EDE18E','#73C489','#74A5FF','#FFC7FE','#00FFF7','#FFCBA0'], ";
                LiteralChart.Text += "resize:true});}); </script>";
                #endregion

                //#region MARKET SHARE
                //List<string> ListKota = new List<string>();
                //var KotaDB = db.TBAlamats.Select(item => item.Kota).Distinct();

                //foreach (var item in KotaDB)
                //{
                //    ListKota.Add(item);
                //}

                //var DataSalesDB = db.TBTransaksis.Where(item => item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).GroupBy(item => new
                //                              {
                //                                  item.TBPelanggan.TBAlamats.FirstOrDefault().Kota
                //                              })
                //                              .Select(item => new
                //                              {
                //                                  Key = item.Key,
                //                                  GrandTotal = item.Sum(item2 => item2.GrandTotal),
                //                                  TotalTransaksi = item.Where(item2 => item2.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).Count(),
                //                                  TotalNominalDiscount = item.Sum(item2 => item2.TotalPotonganHargaJualDetail),
                //                                  JumlahProduk = item.Sum(item2 => item2.JumlahProduk)
                //                              }).OrderByDescending(item=> item.GrandTotal).ToArray();

                //var DataSales = ListKota.Select(item => new
                //{
                //    Kota = item,
                //    JumlahTransaksi = DataSalesDB.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesDB.Where(item2 => item2.Key.Kota == item).Sum(item2 => item2.TotalTransaksi),
                //    GrandTotal = DataSalesDB.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesDB.Where(item2 => item2.Key.Kota == item).Sum(item2 => item2.GrandTotal),
                //    JumlahProduk = DataSalesDB.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesDB.Where(item2 => item2.Key.Kota == item).Sum(item2 => item2.JumlahProduk),
                //    SumJumlahProduk = DataSalesDB.Sum(item2 => item2.JumlahProduk),
                //    SumJumlahTransaksi = DataSalesDB.Sum(item2 => item2.TotalTransaksi),
                //    SumGrandTotal = DataSalesDB.Sum(item2 => item2.GrandTotal)
                //}).ToArray();

                //RepeaterMarketShare.DataSource = DataSales.OrderByDescending(item2 => item2.GrandTotal).ToArray();
                //RepeaterMarketShare.DataBind();

                //var Data5Kota = DataSales.OrderByDescending(item2 => item2.GrandTotal).Take(5).ToArray();
                //List<string> Top5_Kota = new List<string>();

                //for (int i = 0; i < Data5Kota.Count(); i++)
                //{
                //    Top5_Kota.Add(Data5Kota[i].Kota);
                //}

                //LiteralChart.Text += "<script> $(function () { var dataChart = [";

                //for (int i = 0; i < Top5_Kota.Count(); i++)
                //{
                //    var _transaksiTahunIni = DataSalesDB.FirstOrDefault(item => item.Key.Kota == Top5_Kota[i].ToString());
                //    //var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.FirstOrDefault(item => item.Key.IDTempat == tempatDatabase[i].IDTempat);

                //    decimal _grandTotal = 0, _grandTotalTahunLalu = 0;

                //    if (_transaksiTahunIni != null)
                //        _grandTotal = (decimal)_transaksiTahunIni.GrandTotal;

                //    //if (_transaksiTahunLalu != null)
                //    //    _grandTotalTahunLalu = (decimal)_transaksiTahunLalu.GrandTotal;

                //    LiteralChart.Text += "{ 'y': '" + Top5_Kota[i].ToString() + "', '" + (tahun - 1) + "': " + _grandTotalTahunLalu + ", '" + tahun + "': " + _grandTotal + " }, ";
                //}
                //LiteralChart.Text += "]; Morris.Bar({ element: 'graph13', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

                //LiteralChart.Text += "ykeys: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                //LiteralChart.Text += "labels: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                //LiteralChart.Text += "barColors:['#7FC4C1','#FF625D'],";
                //LiteralChart.Text += " parseTime: false,resize:true}); eval(dataChart); }); </script>";

                //#region Labelling
                //LabelTotalJumlahTransaksiHeader.Text = DataSales.Sum(item => item.JumlahTransaksi).ToFormatHargaBulat();
                //LabelTotalGrandTotalHeader.Text = DataSales.Sum(item => item.GrandTotal).ToFormatHarga();
                //LabelJumlahProdukHeader.Text = DataSales.Sum(item => item.JumlahProduk).ToFormatHargaBulat();

                //LabelTotalJumlahTransaksiFooter.Text = DataSales.Sum(item => item.JumlahTransaksi).ToFormatHargaBulat();
                //LabelTotalGrandTotalFooter.Text = DataSales.Sum(item => item.GrandTotal).ToFormatHarga();
                //LabelJumlahProdukFooter.Text = DataSales.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                //#endregion

                //#endregion
            }


        }
    }

    #region Method Poeple Also Buy [rendy herdiawan]
    private List<TBRekomendasiProduk> GabungRekomendasi(List<TBRekomendasiProduk> rekomendasi1, List<TBRekomendasiProduk> rekomendasi2)
    {
        rekomendasi1.AddRange(rekomendasi2.Select(item => new TBRekomendasiProduk
        {
            TBProduk = item.TBProduk1,
            TBProduk1 = item.TBProduk,
            Jumlah = item.Jumlah,
            Nilai = item.Nilai
        }));

        return rekomendasi1.OrderByDescending(item => item.Jumlah).ToList();
    }
    private List<TBRekomendasiKategoriProduk> GabungRekomendasi(List<TBRekomendasiKategoriProduk> rekomendasi1, List<TBRekomendasiKategoriProduk> rekomendasi2)
    {
        rekomendasi1.AddRange(rekomendasi2.Select(item => new TBRekomendasiKategoriProduk
        {
            TBKategoriProduk = item.TBKategoriProduk1,
            TBKategoriProduk1 = item.TBKategoriProduk,
            Jumlah = item.Jumlah,
            Nilai = item.Nilai
        }));

        return rekomendasi1.OrderByDescending(item => item.Jumlah).ToList();
    }
    #endregion

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        QueryData(int.Parse(DropDownListPelanggan.SelectedValue), int.Parse(DropDownListFilter.SelectedValue), int.Parse(DropDownListTahun.SelectedValue));
    }

    public string Pertumbuhan(decimal pertumbuhan)
    {
        if (pertumbuhan < 0)
            return "<span class='label label-danger'>" + pertumbuhan.ToFormatHarga() + "</span>";
        else
            return "<span class='label label-success'>" + pertumbuhan.ToFormatHarga() + "</span>";
    }
}