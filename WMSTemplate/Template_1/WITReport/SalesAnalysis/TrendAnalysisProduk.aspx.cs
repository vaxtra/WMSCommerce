using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_TrendAnalysisProduk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);
                Tanggal_Class Tanggal_Class = new Tanggal_Class();

                LabelHeader.Text = "SALES TREND ANALYSIS " + DateTime.Now.Year.ToString();

                var _dataKombinasiProdukDatabase = db.TBKombinasiProduks.ToArray();

                DropDownListKombinasiProduk.DataSource = _dataKombinasiProdukDatabase;
                DropDownListKombinasiProduk.DataTextField = "Nama";
                DropDownListKombinasiProduk.DataValueField = "IDKombinasiProduk";
                DropDownListKombinasiProduk.DataBind();
                DropDownListKombinasiProduk.Items.Insert(0, new ListItem("All Products", "0"));

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTahun.Items.AddRange(Tanggal_Class.DropdownlistTahun());

                LabelHeaderByPenjualanTahunIni.Text = DropDownListTahun.SelectedItem.Text;
                LabelHeaderByPenjualanTahunKemarin.Text = (LabelHeaderByPenjualanTahunIni.Text.ToInt() - 1).ToString();

                LabelHeaderByQtyTahunIni.Text = LabelHeaderByPenjualanTahunIni.Text;
                LabelHeaderByQtyTahunKemarin.Text = LabelHeaderByPenjualanTahunKemarin.Text;

                //LabelFooterByPenjualanTahunIni.Text = LabelHeaderByPenjualanTahunIni.Text;
                //LabelFooterByPenjualanTahunKemarin.Text = LabelHeaderByPenjualanTahunKemarin.Text;

                //LabelFooterByQtyTahunIni.Text = LabelHeaderByPenjualanTahunIni.Text;
                //LabelFooterByQtyTahunKemarin.Text = LabelHeaderByPenjualanTahunKemarin.Text;
            }
        }
    }
    protected void LoadChartProduk(int tahun, int idkombinasiproduk, int idtempat, int searchby)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (searchby == 1)
            {
                PanelReportPenjualan.Visible = true;
                PanelReportQty.Visible = false;
                //PanelSalesPerChannelPenjualan.Visible = true;
                //PanelSalesPerChannelQty.Visible = false;
            }
            else
            {
                PanelReportPenjualan.Visible = false;
                PanelReportQty.Visible = true;
                //PanelSalesPerChannelPenjualan.Visible = false;
                //PanelSalesPerChannelQty.Visible = true;
            }

            LabelHeader.Text = "SALES TREND ANALYSIS " + tahun.ToString().ToUpper();

            //Literal LiteralChart = (Literal)this.Page.Master.FindControl("LiteralChart");
            LiteralChart.Text = string.Empty;

            TBTransaksiDetail[] _dataDetailTransaksi2TahunDataBase;
            TBTransaksiDetail[] _dataDetailTransaksi2TahunDataBaseDefault;

            if (idkombinasiproduk != 0)
            {
                //Detail transaksi tahun ini, dan setahun yg lalu.. di seluruh tempat
                _dataDetailTransaksi2TahunDataBase = db.TBTransaksiDetails
                    .Where(item =>
                        item.TBTransaksi.TanggalOperasional.Value.Year >= tahun - 1 &&
                        item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                        item.IDKombinasiProduk == idkombinasiproduk)
                    .AsEnumerable()
                    .ToArray();
            }
            else
            {
                //Detail transaksi tahun ini, dan setahun yg lalu.. di seluruh tempat
                _dataDetailTransaksi2TahunDataBase = db.TBTransaksiDetails
                    .Where(item =>
                        item.TBTransaksi.TanggalOperasional.Value.Year >= tahun - 1 &&
                        item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                    .ToArray();
            }

            //Detail transaksi tahun ini & tahun - 1, FILTERING tempat
            if (idtempat != 0)
            {
                _dataDetailTransaksi2TahunDataBaseDefault = _dataDetailTransaksi2TahunDataBase; //Digunakan untuk sales per channel
                _dataDetailTransaksi2TahunDataBase = _dataDetailTransaksi2TahunDataBase.Where(item => item.TBTransaksi.IDTempat == idtempat).ToArray();
            }
            else
                _dataDetailTransaksi2TahunDataBaseDefault = _dataDetailTransaksi2TahunDataBase; //Digunakan untuk sales per channel

            //Group by Month
            var _dataDetailTransaksiTahunIni = _dataDetailTransaksi2TahunDataBase
                .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun)
                .GroupBy(item => item.TBTransaksi.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.Subtotal),
                    TotalQty = item.Sum(item2 => item2.Quantity)
                })
                .ToArray();

            var _dataDetailTransaksiTahunIniNoFilter = _dataDetailTransaksi2TahunDataBase
                .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun)
                .GroupBy(item => item.TBTransaksi.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.Subtotal),
                    TotalQty = item.Sum(item2 => item2.Quantity)
                })
                .ToArray();

            //Group by Month
            var _dataDetailTransaksiTahunLalu = _dataDetailTransaksi2TahunDataBase
                .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun - 1)
                .GroupBy(item => item.TBTransaksi.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.Subtotal),
                    TotalQty = item.Sum(item2 => item2.Quantity)
                })
                .ToArray();

            var _dataDetailTransaksiTahunLaluNoFilter = _dataDetailTransaksi2TahunDataBase
                .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun - 1)
                .GroupBy(item => item.TBTransaksi.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.Subtotal),
                    TotalQty = item.Sum(item2 => item2.Quantity)
                })
                .ToArray();

            #region Line Chart - Trend Analysis Monthly (tahun ini dan tahun lalu)
            LiteralChart.Text += "<script> $(function () { var dataChart = [";
            if (idtempat == 0)
            {
                //If searched by Penjualan
                if (searchby == 1)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var _transaksiTahunIni = _dataDetailTransaksiTahunIniNoFilter.FirstOrDefault(item => item.Key == i);
                        var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluNoFilter.FirstOrDefault(item => item.Key == i);

                        decimal _grandTotal = 0, _grandTotalTahunLalu = 0;

                        if (_transaksiTahunIni != null)
                            _grandTotal = (decimal)_transaksiTahunIni.GrandTotal;

                        if (_transaksiTahunLalu != null)
                            _grandTotalTahunLalu = (decimal)_transaksiTahunLalu.GrandTotal;

                        LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + _grandTotalTahunLalu + ", '" + tahun + "': " + _grandTotal + " }, ";
                    }
                }
                else //searched by Qty
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var _transaksiTahunIni = _dataDetailTransaksiTahunIniNoFilter.FirstOrDefault(item => item.Key == i);
                        var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluNoFilter.FirstOrDefault(item => item.Key == i);

                        decimal _totalQty = 0, _totalQtyTahunLalu = 0;

                        if (_transaksiTahunIni != null)
                            _totalQty = (decimal)_transaksiTahunIni.TotalQty;

                        if (_transaksiTahunLalu != null)
                            _totalQtyTahunLalu = (decimal)_transaksiTahunLalu.TotalQty;

                        LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + _totalQtyTahunLalu + ", '" + tahun + "': " + _totalQty + " }, ";
                    }
                }

                LiteralChart.Text += "]; Morris.Line({ element: 'graph11', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

                LiteralChart.Text += "ykeys: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                LiteralChart.Text += "labels: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                LiteralChart.Text += "lineColors:['#7FC4C1','#FF625D'],";
                LiteralChart.Text += " parseTime: false}); eval(dataChart); }); </script>";
            }
            else
            {
                //If searched by Penjualan
                if (searchby == 1)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var _transaksiTahunIni = _dataDetailTransaksiTahunIni.FirstOrDefault(item => item.Key == i);
                        var _transaksiTahunLalu = _dataDetailTransaksiTahunLalu.FirstOrDefault(item => item.Key == i);

                        decimal _grandTotal = 0, _grandTotalTahunLalu = 0;

                        if (_transaksiTahunIni != null)
                            _grandTotal = (decimal)_transaksiTahunIni.GrandTotal;

                        if (_transaksiTahunLalu != null)
                            _grandTotalTahunLalu = (decimal)_transaksiTahunLalu.GrandTotal;

                        LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + _grandTotalTahunLalu + ", '" + tahun + "': " + _grandTotal + " }, ";
                    }
                }
                else //searched by Qty
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var _transaksiTahunIni = _dataDetailTransaksiTahunIni.FirstOrDefault(item => item.Key == i);
                        var _transaksiTahunLalu = _dataDetailTransaksiTahunLalu.FirstOrDefault(item => item.Key == i);

                        decimal _totalQty = 0, _totalQtyTahunLalu = 0;

                        if (_transaksiTahunIni != null)
                            _totalQty = (decimal)_transaksiTahunIni.TotalQty;

                        if (_transaksiTahunLalu != null)
                            _totalQtyTahunLalu = (decimal)_transaksiTahunLalu.TotalQty;

                        LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + _totalQtyTahunLalu + ", '" + tahun + "': " + _totalQty + " }, ";
                    }
                }

                LiteralChart.Text += "]; Morris.Line({ element: 'graph11', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

                LiteralChart.Text += "ykeys: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                LiteralChart.Text += "labels: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
                LiteralChart.Text += "lineColors:['#7FC4C1','#FF625D'],";
                LiteralChart.Text += " parseTime: false}); eval(dataChart); }); </script>";
            }
            #endregion

            #region RepeaterDetail dari Trend Analysis Monthly
            PanelDetail.Visible = true;
            //Group by Month & Year, ini digunakan untuk tampilan repeater detail
            var _dataDetail2Tahun = _dataDetailTransaksi2TahunDataBase
                .GroupBy(item => new
                {
                    item.TBTransaksi.TanggalOperasional.Value.Month,
                    item.TBTransaksi.TanggalOperasional.Value.Year
                })
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.Subtotal),
                    TotalQty = item.Sum(item2 => item2.Quantity),
                    TotalNominalDiscount = item.Sum(item2 => item2.Discount) == 0 ? 0 : item.Sum(item2 => item2.Discount * item2.Quantity)
                }).ToArray();

            List<int> Bulan = new List<int>();
            for (int i = 1; i <= 12; i++)
                Bulan.Add(i);

            if (searchby == 1)
            {
                var Data = Bulan.Select(item => new
                {
                    Bulan = new DateTime(DropDownListTahun.SelectedValue.ToInt(), item, 1).ToString("MMMM"),
                    GrandTotalTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.GrandTotal),
                    GrandTotalTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.GrandTotal),
                    TotalNominalDiscountTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.TotalNominalDiscount),
                    TotalNominalDiscountTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.TotalNominalDiscount)
                }).ToArray();

                RepeaterDetail.DataSource = Data;
                RepeaterDetail.DataBind();

                LabelTotalNormalSalesTahunIni.Text = Data.Sum(item => item.GrandTotalTahunIni + item.TotalNominalDiscountTahunIni).ToFormatHarga();
                LabelTotalDiscountTahunIni.Text = Data.Sum(item => item.TotalNominalDiscountTahunIni).ToFormatHarga();
                LabelGrandTotalTahunIni.Text = Data.Sum(item => item.GrandTotalTahunIni).ToFormatHarga();
                LabelTotalNormalSalesTahunLalu.Text = Data.Sum(item => item.GrandTotalTahunLalu + item.TotalNominalDiscountTahunLalu).ToFormatHarga();
                LabelTotalDiscountTahunLalu.Text = Data.Sum(item => item.TotalNominalDiscountTahunLalu).ToFormatHarga();
                LabelGrandTotalTahunLalu.Text = Data.Sum(item => item.GrandTotalTahunLalu).ToFormatHarga();
                LabelTotalSelisih.Text = Pertumbuhan((decimal)Data.Sum(item => item.GrandTotalTahunIni - item.GrandTotalTahunLalu));

            }
            else
            {
                var Data = Bulan.Select(item => new
                {
                    Bulan = new DateTime(DropDownListTahun.SelectedValue.ToInt(), item, 1).ToString("MMMM"),
                    GrandTotalTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.TotalQty),
                    GrandTotalTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.TotalQty),
                    TotalNominalDiscountTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.TotalNominalDiscount),
                    TotalNominalDiscountTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.TotalNominalDiscount)
                }).ToArray();

                RepeaterDetailQty.DataSource = Data;
                RepeaterDetailQty.DataBind();

                LabelTotalQtyTahunIni.Text = Data.Sum(item => item.GrandTotalTahunIni).ToFormatHargaBulat();
                LabelTotalQtyTahunLalu.Text = Data.Sum(item => item.GrandTotalTahunLalu).ToFormatHargaBulat();
                LabelTotalGrowthQty.Text = Pertumbuhan((decimal)Data.Sum(item => item.GrandTotalTahunIni - item.GrandTotalTahunLalu));
            }
            #endregion

            //#region Bar Chart - Sales per channel
            //TBTempat[] tempatDatabase = db.TBTempats.ToArray();
            //List<int> datatempat = new List<int>();
            //for (int i = 0; i < tempatDatabase.Count(); i++)
            //{
            //    datatempat.Add(tempatDatabase[i].IDTempat);
            //}
            ////Detail Transaksi tahun ini, Group by Tempat
            //var _dataDetailTransaksiTahunIniGroupByIDTempat = _dataDetailTransaksi2TahunDataBaseDefault
            //                        .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun)
            //                       .GroupBy(item => new
            //                       {
            //                           item.TBTransaksi.TBTempat.IDTempat,
            //                           item.TBTransaksi.TBTempat.Nama
            //                       })
            //                       .Select(item => new
            //                       {
            //                           Key = item.Key,
            //                           GrandTotal = item.Sum(item2 => item2.Subtotal) == 0 ? 0 : item.Sum(item2 => item2.Subtotal),
            //                           TotalQty = item.Sum(item2 => item2.JumlahProduk) == 0 ? 0 : item.Sum(item2 => item2.JumlahProduk),
            //                           TotalNominalDiscount = item.Sum(item2 => item2.PotonganHargaJual) == 0 ? 0 : item.Sum(item2 => item2.PotonganHargaJual * item2.JumlahProduk)
            //                       }).ToArray();

            ////Detail Transaksi tahun lalu, Group by Tempat
            //var _dataDetailTransaksiTahunLaluGroupByIDTempat = _dataDetailTransaksi2TahunDataBaseDefault
            //                       .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun - 1)
            //                       .GroupBy(item => new
            //                       {
            //                           item.TBTransaksi.TBTempat.IDTempat,
            //                           item.TBTransaksi.TBTempat.Nama
            //                       })
            //                       .Select(item => new
            //                       {
            //                           Key = item.Key,
            //                           NormalSales = item.Sum(item2 => item2.HargaJual) * item.Sum(item2 => item2.JumlahProduk),
            //                           GrandTotal = item.Sum(item2 => item2.Subtotal) == 0 ? 0 : item.Sum(item2 => item2.Subtotal),
            //                           TotalQty = item.Sum(item2 => item2.JumlahProduk) == 0 ? 0 : item.Sum(item2 => item2.JumlahProduk),
            //                           TotalNominalDiscount = item.Sum(item2 => item2.PotonganHargaJual) == 0 ? 0 : item.Sum(item2 => item2.PotonganHargaJual * item2.JumlahProduk)
            //                       }).ToArray();
            //LiteralChart.Text += "<script> $(function () { var dataChart = [";

            //for (int i = 0; i < datatempat.Count(); i++)
            //{
            //    var _transaksiTahunIni = _dataDetailTransaksiTahunIniGroupByIDTempat.FirstOrDefault(item => item.Key.IDTempat == tempatDatabase[i].IDTempat);
            //    var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.FirstOrDefault(item => item.Key.IDTempat == tempatDatabase[i].IDTempat);

            //    decimal _grandTotal = 0, _grandTotalTahunLalu = 0;

            //    if (_transaksiTahunIni != null)
            //        _grandTotal = (decimal)_transaksiTahunIni.GrandTotal;

            //    if (_transaksiTahunLalu != null)
            //        _grandTotalTahunLalu = (decimal)_transaksiTahunLalu.GrandTotal;

            //    LiteralChart.Text += "{ 'y': '" + tempatDatabase[i].Nama + "', '" + (tahun - 1) + "': " + _grandTotalTahunLalu + ", '" + tahun + "': " + _grandTotal + " }, ";
            //}
            //LiteralChart.Text += "]; Morris.Bar({ element: 'graph13', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

            //LiteralChart.Text += "ykeys: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
            //LiteralChart.Text += "labels: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
            //LiteralChart.Text += "barColors:['#7FC4C1','#FF625D'],";
            //LiteralChart.Text += " parseTime: false,resize:true}); eval(dataChart); }); </script>";

            //if (searchby == 1)
            //{
            //    var DataSalesPerChannel = datatempat.Select(item => new
            //    {
            //        NamaTempat = tempatDatabase.FirstOrDefault(item2 => item2.IDTempat == item).Nama,
            //        GrandTotalTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.GrandTotal),
            //        GrandTotalTahunIni = _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.GrandTotal),
            //        TotalNominalDiscountTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.TotalNominalDiscount),
            //        TotalNominalDiscountTahunIni = _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.TotalNominalDiscount)
            //    }).ToArray();

            //    RepeaterSalesPerChannel.DataSource = DataSalesPerChannel;
            //    RepeaterSalesPerChannel.DataBind();

            //    LabelTotalNormalSalesTahunLaluPerChannel.Text = DataSalesPerChannel.Sum(item => item.GrandTotalTahunLalu + item.TotalNominalDiscountTahunLalu).ToFormatHarga();
            //    LabelTotalDiscountTahunLaluPerChannel.Text = DataSalesPerChannel.Sum(item => item.TotalNominalDiscountTahunLalu).ToFormatHarga();
            //    LabelGrandTotalTahunLaluPerChannel.Text = DataSalesPerChannel.Sum(item => item.GrandTotalTahunLalu).ToFormatHarga();
            //    LabelTotalNormalSalesTahunIniPerChannel.Text = DataSalesPerChannel.Sum(item => item.GrandTotalTahunIni + item.TotalNominalDiscountTahunIni).ToFormatHarga();
            //    LabelTotalDiscountTahunIniPerChannel.Text = DataSalesPerChannel.Sum(item => item.TotalNominalDiscountTahunIni).ToFormatHarga();
            //    LabelGrandTotalTahunIniPerChannel.Text = DataSalesPerChannel.Sum(item => item.GrandTotalTahunIni).ToFormatHarga();
            //    LabelTotalSelisihPerChannel.Text = Pertumbuhan((decimal)DataSalesPerChannel.Sum(item => item.GrandTotalTahunIni - item.GrandTotalTahunLalu));
            //}
            //else
            //{
            //    var DataSalesPerChannel = datatempat.Select(item => new
            //    {
            //        NamaTempat = tempatDatabase.FirstOrDefault(item2 => item2.IDTempat == item).Nama,
            //        GrandTotalTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.TotalQty),
            //        GrandTotalTahunIni = _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.TotalQty),
            //        TotalNominalDiscountTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunLaluGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.TotalNominalDiscount),
            //        TotalNominalDiscountTahunIni = _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Count() == 0 ? 0 : _dataDetailTransaksiTahunIniGroupByIDTempat.Where(item2 => item2.Key.IDTempat == item).Sum(item2 => item2.TotalNominalDiscount)
            //    }).ToArray();

            //    RepeaterSalesPerChannelQty.DataSource = DataSalesPerChannel;
            //    RepeaterSalesPerChannelQty.DataBind();

            //    LabelTotalQtySalesPerChannelTahunLalu.Text = DataSalesPerChannel.Sum(item => item.GrandTotalTahunLalu).ToFormatHargaBulat();
            //    LabelTotalQtySalesPerChannelTahunIni.Text = DataSalesPerChannel.Sum(item => item.GrandTotalTahunIni).ToFormatHargaBulat();
            //    LabelTotalGrowthQtySalesPerChannel.Text = Pertumbuhan((decimal)DataSalesPerChannel.Sum(item => item.GrandTotalTahunIni - item.GrandTotalTahunLalu));
            //}

            //#endregion

            #region Sales per kota
            List<string> ListKota = new List<string>();
            var KotaDB = db.TBAlamats.Select(item => item.TBWilayah.TBWilayah1.Nama).Distinct();

            if (KotaDB != null)
            {
                foreach (var item in KotaDB)
                {
                    ListKota.Add(item);
                }

                var DataSalesPerKota = _dataDetailTransaksi2TahunDataBaseDefault
                    .Where(item => item.TBTransaksi.TanggalOperasional.Value.Year == tahun
                    && item.TBTransaksi.IDPelanggan != 1
                    && item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah != null)
                    .GroupBy(item => new
                    {
                        Kota = item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1.Nama,
                        Provinsi = item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1.TBWilayah1.Nama,
                    })
                                              .Select(item => new
                                              {
                                                  Key = item.Key,
                                                  GrandTotal = item.Sum(item2 => item2.HargaJual * item2.Quantity),
                                                  TotalTransaksi = item.Where(item2 => item2.TBTransaksi.IDStatusTransaksi == 5).Count(),
                                                  TotalNominalDiscount = item.Sum(item2 => item2.TBTransaksi.TotalPotonganHargaJualDetail),
                                                  JumlahProduk = item.Sum(item2 => item2.Quantity)
                                              }).OrderByDescending(item => item.GrandTotal).ToArray();

                var DataSalesPerKotaSum = ListKota.Select(item => new
                {
                    Kota = item,
                    Provinsi = DataSalesPerKota.Where(item2 => item2.Key.Kota == item).Count() == 0 ? "" : DataSalesPerKota.FirstOrDefault(item2 => item2.Key.Kota == item).Key.Provinsi,
                    JumlahTransaksi = DataSalesPerKota.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesPerKota.FirstOrDefault(item2 => item2.Key.Kota == item).TotalTransaksi,
                    GrandTotal = DataSalesPerKota.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesPerKota.FirstOrDefault(item2 => item2.Key.Kota == item).GrandTotal,
                    JumlahProduk = DataSalesPerKota.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesPerKota.FirstOrDefault(item2 => item2.Key.Kota == item).JumlahProduk,
                    SumJumlahProduk = DataSalesPerKota.Sum(item2 => item2.JumlahProduk),
                    SumJumlahTransaksi = DataSalesPerKota.Sum(item2 => item2.TotalTransaksi),
                    SumGrandTotal = DataSalesPerKota.Sum(item2 => item2.GrandTotal)
                }).ToArray();

                RepeaterMarketShare.DataSource = DataSalesPerKotaSum.Where(item2 => item2.GrandTotal != 0)
                    .OrderByDescending(item2 => item2.GrandTotal).ToArray();
                RepeaterMarketShare.DataBind();

                #region Labelling
                LabelTotalJumlahTransaksiHeader.Text = DataSalesPerKotaSum.Sum(item => item.JumlahTransaksi).ToFormatHargaBulat();
                LabelTotalGrandTotalHeader.Text = DataSalesPerKotaSum.Sum(item => item.GrandTotal).ToFormatHarga();
                LabelJumlahProdukHeader.Text = DataSalesPerKotaSum.Sum(item => item.JumlahProduk).ToFormatHargaBulat();

                LabelTotalJumlahTransaksiFooter.Text = DataSalesPerKotaSum.Sum(item => item.JumlahTransaksi).ToFormatHargaBulat();
                LabelTotalGrandTotalFooter.Text = DataSalesPerKotaSum.Sum(item => item.GrandTotal).ToFormatHarga();
                LabelJumlahProdukFooter.Text = DataSalesPerKotaSum.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
                #endregion

                var SettingGrafik = DataSalesPerKotaSum.Where(item2 => item2.GrandTotal != 0)
                                    .OrderByDescending(item2 => item2.GrandTotal)
                                    .Take(20);

                int Height = SettingGrafik.Count() * 25;


                container.Attributes.Add("style", "width: auto; height: " + (Height > 600 ? Height : 600) + "px; margin: 0 auto;");

                string Judul = "";
                string SubJudul = "";
                string JudulX = "Sales Per Kota";
                string DataX = "";

                string JudulY = "Sales";

                string DataY = "";
                string Tooltip = "";

                if (DropDownListFilter.SelectedItem.Value == "1")
                {
                    foreach (var item in SettingGrafik)
                    {
                        DataX += "'" + item.Kota + "',";
                        DataY += item.GrandTotal + ",";
                    }
                }
                else
                {
                    foreach (var item in SettingGrafik)
                    {
                        DataX += "'" + item.Kota + "',";
                        DataY += item.JumlahProduk + ",";
                    }
                }


                LiteralChart.Text += "<script type=\"text/javascript\">";
                LiteralChart.Text += "$(function () { $('#container').highcharts({";
                LiteralChart.Text += "        chart: { type: 'bar' },";
                LiteralChart.Text += "        title: { text: '" + Judul + "' },";
                LiteralChart.Text += "        subtitle: { text: '" + SubJudul + "' },";
                LiteralChart.Text += "        xAxis: { categories: [" + DataX + "] },";
                LiteralChart.Text += "        yAxis: { min: 0, title: { text: '" + JudulY + "' } },";
                LiteralChart.Text += "        tooltip: { valueSuffix: '" + Tooltip + "' },";
                LiteralChart.Text += "        legend: { reversed: true },";
                LiteralChart.Text += "        plotOptions: { series: { stacking: 'normal' } },";
                LiteralChart.Text += "        credits: { enabled: false },";
                LiteralChart.Text += "        exporting: { enabled: false },";
                LiteralChart.Text += "        series: [";
                LiteralChart.Text += "		{";
                LiteralChart.Text += "            name: '" + JudulX + "',";
                LiteralChart.Text += "            data: [" + DataY + "]";
                LiteralChart.Text += "        },";
                LiteralChart.Text += "		]";
                LiteralChart.Text += "    }); });";
                LiteralChart.Text += "</script>";

                #endregion

                //#region Donut Chart - Annual Sales
                //LiteralChart.Text += "<script> $(function () { var dataChart = [";

                //if (DropDownListFilter.SelectedValue.ToInt() == 1)
                //{
                //    for (int i = tahun - 1; i <= tahun; i++)
                //    {
                //        if (i == tahun - 1)
                //            LiteralChart.Text += "{ 'label': '" + 2014 + "', 'value': " + _dataDetailTransaksiTahunLaluNoFilter.Sum(item => item.GrandTotal) + "}, ";
                //        else
                //            LiteralChart.Text += "{ 'label': '" + 2015 + "', 'value': " + _dataDetailTransaksiTahunIniNoFilter.Sum(item => item.GrandTotal) + "}, ";
                //    }
                //}
                //else
                //{
                //    for (int i = tahun - 1; i <= tahun; i++)
                //    {
                //        if (i == tahun - 1)
                //            LiteralChart.Text += "{ 'label': '" + 2014 + "', 'value': " + _dataDetailTransaksiTahunLaluNoFilter.Sum(item => item.TotalQty) + "}, ";
                //        else
                //            LiteralChart.Text += "{ 'label': '" + 2015 + "', 'value': " + _dataDetailTransaksiTahunIniNoFilter.Sum(item => item.TotalQty) + "}, ";
                //    }
                //}

                //LiteralChart.Text += "]; Morris.Donut({ element: 'graph12', behaveLikeLine: true, data: dataChart, xkey: 'y', ";
                //LiteralChart.Text += "colors:['#7FC4C1','#FF625D'], ";
                //LiteralChart.Text += "resize:true});}); </script>";
                //#endregion
            }
        }
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        LoadChartProduk(DropDownListTahun.SelectedValue.ToInt(), DropDownListKombinasiProduk.SelectedValue.ToInt(), DropDownListTempat.SelectedValue.ToInt(), DropDownListFilter.SelectedValue.ToInt());
    }

    public string Pertumbuhan(decimal pertumbuhan)
    {
        if (pertumbuhan < 0)
            return "<span class='label label-danger'>" + pertumbuhan.ToFormatHarga() + "</span>";
        else
            return "<span class='label label-success'>" + pertumbuhan.ToFormatHarga() + "</span>";
    }
}