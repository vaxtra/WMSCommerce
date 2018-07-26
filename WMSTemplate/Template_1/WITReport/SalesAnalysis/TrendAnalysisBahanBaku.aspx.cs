using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_TrendAnalysisBahanBaku : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LabelHeader.Text = "RAW MATERIAL TREND ANALYSIS " + DateTime.Now.Year.ToString();

                var _dataKombinasiProdukDatabase = db.TBBahanBakus.ToArray();
                //var _dataKombinasiProdukDatabase = db.TBKombinasiProduks.Where(item => item.Nama.Contains("Brownies")).ToArray();
                DropDownListKombinasiProduk.DataSource = _dataKombinasiProdukDatabase;
                DropDownListKombinasiProduk.DataTextField = "Nama";
                DropDownListKombinasiProduk.DataValueField = "IDBahanBaku";
                DropDownListKombinasiProduk.DataBind();
                DropDownListKombinasiProduk.Items.Insert(0, new ListItem("All Raw Materials", "0"));

                DropDownListTempat.DataSource = db.TBSuppliers.ToArray();
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataValueField = "IDSupplier";
                DropDownListTempat.DataBind();
                DropDownListTempat.Items.Insert(0, new ListItem("All Suppliers", "0"));

                int index = 0;
                for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
                {
                    DropDownListTahun.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                    index++;
                }
                DropDownListTahun.SelectedIndex = 5;
            }
        }
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        LoadChartProduk(DropDownListTahun.SelectedValue.ToInt(), DropDownListKombinasiProduk.SelectedValue.ToInt(), DropDownListTempat.SelectedValue.ToInt());
    }

    public string Pertumbuhan(decimal pertumbuhan)
    {
        if (pertumbuhan < 0)
            return "<span class='label label-danger'>" + pertumbuhan.ToFormatHarga() + "</span>";
        else
            return "<span class='label label-success'>" + pertumbuhan.ToFormatHarga() + "</span>";
    }
    protected void LoadChartProduk(int tahun, int idbahanbaku, int idtempat)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LabelHeader.Text = "RAW MATERIAL TREND ANALYSIS " + tahun.ToString().ToUpper();
            //Literal LiteralChart = (Literal)this.Page.Master.FindControl("LiteralChart");
            LiteralChart.Text = string.Empty;
            TBHargaSupplier[] _dataHargaSupplierDataBase;
            TBHargaSupplier[] _dataHargaSupplierDataBaseDefault;

            if (idbahanbaku != 0)
            {
                //Data Harga Bahan Baku 2 tahun, sesuai dropdown
                _dataHargaSupplierDataBase = db.TBHargaSuppliers
                    .Where(item => item.Tanggal.Value.Year >= tahun - 1
                            && item.TBStokBahanBaku.TBBahanBaku.IDBahanBaku == idbahanbaku).AsEnumerable().ToArray();
            }
            else
            {
                //Data Harga Bahan Baku 2 tahun, sesuai dropdown
                _dataHargaSupplierDataBase = db.TBHargaSuppliers
                    .Where(item => item.Tanggal.Value.Year >= tahun - 1).AsEnumerable().ToArray();
            }

            //Detail transaksi tahun ini & tahun - 1, FILTERING tempat
            if (idtempat != 0)
            {
                _dataHargaSupplierDataBaseDefault = _dataHargaSupplierDataBase; //Digunakan untuk sales per channel
                _dataHargaSupplierDataBase = _dataHargaSupplierDataBase.Where(item => item.IDSupplier == idtempat).ToArray();
            }
            else
                _dataHargaSupplierDataBaseDefault = _dataHargaSupplierDataBase; //Digunakan untuk sales per channel

            //Group by Month
            var _dataHargaSupplierTahunIni = _dataHargaSupplierDataBase
                                   .Where(item => item.Tanggal.Value.Year == tahun)
                                   .GroupBy(item => item.Tanggal.Value.Month)
                                   .Select(item => new
                                   {
                                       Key = item.Key,
                                       GrandTotal = item.Sum(item2 => item2.Harga) == 0 ? 0 : item.Sum(item2 => item2.Harga) / _dataHargaSupplierDataBase.Where(item2 => item2.Tanggal.Value.Month == item.Key).Count()
                                   }).ToArray();


            //Group by Month
            var _dataHargaSupplierTahunLalu = _dataHargaSupplierDataBase
                                   .Where(item => item.Tanggal.Value.Year == tahun - 1)
                                   .GroupBy(item => item.Tanggal.Value.Month)
                                   .Select(item => new
                                   {
                                       Key = item.Key,
                                       GrandTotal = item.Sum(item2 => item2.Harga) == 0 ? 0 : item.Sum(item2 => item2.Harga) / _dataHargaSupplierDataBase.Where(item2 => item2.Tanggal.Value.Month == item.Key).Count()
                                   }).ToArray();


            #region Line Chart - Trend Analysis Monthly (tahun ini dan tahun lalu)
            LiteralChart.Text += "<script> $(function () { var dataChart = [";

            //If searched by Penjualan
            for (int i = 1; i <= 12; i++)
            {
                var _transaksiTahunIni = _dataHargaSupplierTahunIni.FirstOrDefault(item => item.Key == i);
                var _transaksiTahunLalu = _dataHargaSupplierTahunLalu.FirstOrDefault(item => item.Key == i);

                decimal _grandTotal = 0, _grandTotalTahunLalu = 0;

                if (_transaksiTahunIni != null)
                    _grandTotal = (decimal)_transaksiTahunIni.GrandTotal;

                if (_transaksiTahunLalu != null)
                    _grandTotalTahunLalu = (decimal)_transaksiTahunLalu.GrandTotal;

                LiteralChart.Text += "{ 'y': '" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), i, 1).ToString("MMM") + "', '" + (tahun - 1) + "': " + _grandTotalTahunLalu + ", '" + tahun + "': " + _grandTotal + " }, ";
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
            var _dataDetail2Tahun = _dataHargaSupplierDataBase
                                          .GroupBy(item => new
                                          {
                                              item.Tanggal.Value.Month,
                                              item.Tanggal.Value.Year
                                          })
                                          .Select(item => new
                                          {
                                              Key = item.Key,
                                              GrandTotal = item.Sum(item2 => item2.Harga) == 0 ? 0 : item.Sum(item2 => item2.Harga) / _dataHargaSupplierDataBase.Where(item2 => item2.Tanggal.Value.Month == item.Key.Month).Count()
                                          }).ToArray();

            List<int> Bulan = new List<int>();
            for (int i = 1; i <= 12; i++)
                Bulan.Add(i);

            var Data = Bulan.Select(item => new
            {
                Bulan = new DateTime(DropDownListTahun.SelectedValue.ToInt(), item, 1).ToString("MMMM"),
                GrandTotalTahunLalu = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun - 1).Sum(item2 => item2.GrandTotal),
                GrandTotalTahunIni = _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Count() == 0 ? 0 : _dataDetail2Tahun.Where(item2 => item2.Key.Month == item && item2.Key.Year == tahun).Sum(item2 => item2.GrandTotal),
            }).ToArray();

            RepeaterDetail.DataSource = Data;
            RepeaterDetail.DataBind();

            LabelGrandTotalTahunIni.Text = (Data.Sum(item => item.GrandTotalTahunIni) / 12).ToFormatHarga();
            LabelGrandTotalTahunLalu.Text = (Data.Sum(item => item.GrandTotalTahunLalu) / 12).ToFormatHarga();
            LabelTotalSelisih.Text = Pertumbuhan((decimal)Data.Sum(item => item.GrandTotalTahunIni - item.GrandTotalTahunLalu));

            #endregion

            //#region Bar Chart - Sales per channel
            //TBTempat[] tempatDatabase = Database.db.TBTempats.ToArray();
            //List<int> datatempat = new List<int>();
            //for (int i = 1; i <= tempatDatabase.Count(); i++)
            //{
            //    datatempat.Add(i);
            //}
            ////Detail Transaksi tahun ini, Group by Tempat
            //var _dataDetailTransaksiTahunIniGroupByIDTempat = _dataHargaSupplierDataBaseDefault
            //                        .Where(item => item.Tanggal.Value.Year == tahun)
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
            //var _dataDetailTransaksiTahunLaluGroupByIDTempat = _dataHargaSupplierDataBaseDefault
            //                       .Where(item => item.TBTransaksi.TanggalTransaksi.Value.Year == tahun - 1)
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

            //if (DropDownListFilter.SelectedValue) == 1)
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

            //#endregion

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