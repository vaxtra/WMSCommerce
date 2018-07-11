using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Tanggal_Class Tanggal_Class = new Tanggal_Class();
            DropDownListTahun.Items.AddRange(Tanggal_Class.DropdownlistTahun());

            LoadData(DropDownListTahun.SelectedValue.ToInt());
        }
    }
    protected void DropDownListTahun_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData(DropDownListTahun.SelectedValue.ToInt());
    }
    protected void LoadData(int tahun)
    {
        LabelPeriode.Text = tahun.ToString();

        //Literal LiteralChart = (Literal)this.Page.Master.FindControl("LiteralChart");

        LiteralChart.Text = string.Empty;
        LiteralTempat.Text = string.Empty;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Tempat_Class ClassTempat = new Tempat_Class(db);

            #region TRANSAKSI TAHUN INI
            var TransaksiTahunIni = db.TBTransaksis
                .Where(item =>
                    item.TanggalOperasional.Value.Year == tahun &&
                    item.IDStatusTransaksi == 5)
                .ToArray();

            var TransaksiTahunIniPerBulan = TransaksiTahunIni
                .GroupBy(item => item.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.GrandTotal)
                })
                .ToArray();
            #endregion

            #region TRANSAKSI TAHUN LALU
            var TransaksiTahunLalu = db.TBTransaksis
                .Where(item =>
                    item.TanggalOperasional.Value.Year == tahun - 1 &&
                    item.IDStatusTransaksi == 5)
                .ToArray();

            var TransaksiTahunLaluPerBulan = TransaksiTahunLalu
                .GroupBy(item => item.TanggalOperasional.Value.Month)
                .Select(item => new
                {
                    Key = item.Key,
                    GrandTotal = item.Sum(item2 => item2.GrandTotal)
                })
                .ToArray();
            #endregion

            #region DATA FORECASTING
            var DataForecasting = db.TBForecasts
                .Where(item => item.Tanggal.Year == tahun)
                .ToArray();
            #endregion

            #region GRAFIK SALES HISTORY
            LiteralChart.Text += "<script> $(function () { var dataChart = [";

            for (int i = 1; i <= 12; i++)
            {
                var BulanTahunIni = TransaksiTahunIniPerBulan.FirstOrDefault(item2 => item2.Key == i);
                var BulanTahunLalu = TransaksiTahunLaluPerBulan.FirstOrDefault(item2 => item2.Key == i);

                LiteralChart.Text += "{ 'y': '" + Pengaturan.Bulan(i) + "', ";
                LiteralChart.Text += "'" + (tahun - 1) + "': " + (BulanTahunLalu != null ? BulanTahunLalu.GrandTotal : 0) + ", ";
                LiteralChart.Text += "'" + tahun + "': " + (BulanTahunIni != null ? BulanTahunIni.GrandTotal : 0) + ", ";
                LiteralChart.Text += " }, ";
            }

            LiteralChart.Text += "]; Morris.Bar({ element: 'graph', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

            LiteralChart.Text += "ykeys: [";
            LiteralChart.Text += "'" + (tahun - 1) + "', ";
            LiteralChart.Text += "'" + tahun + "', ";
            LiteralChart.Text += "], ";

            LiteralChart.Text += "labels: [";
            LiteralChart.Text += "'" + (tahun - 1) + "', ";
            LiteralChart.Text += "'" + tahun + "', ";
            LiteralChart.Text += "], ";

            LiteralChart.Text += "barColors:['#7A92A3 ','#0B62A4'], ";
            LiteralChart.Text += " parseTime: false, resize:true }); eval(dataChart); }); </script>";
            #endregion

            #region SALES TEMPAT
            foreach (var item in ClassTempat.Data())
            {
                LiteralTempat.Text += "<div class='col-lg-6'><h4>" + item.TBKategoriTempat.Nama + " - " + item.Nama + "</h4><div id='graph" + item.IDTempat + "' style='width: 100%; height: 200px;'></div></div>";

                LiteralChart.Text += "<script> $(function () { var dataChart = [";

                var DataTransaksi = TransaksiTahunIni
                    .Where(item2 => item2.IDTempat == item.IDTempat)
                    .GroupBy(item2 => item2.TanggalOperasional.Value.Month)
                    .Select(item2 => new
                    {
                        Key = item2.Key,
                        GrandTotal = item2.Sum(item3 => item3.GrandTotal)
                    })
                    .ToArray();

                var DataForecast = DataForecasting
                    .Where(item2 => item2.IDTempat == item.IDTempat)
                    .GroupBy(item2 => item2.Tanggal.Month)
                    .Select(item2 => new
                    {
                        Key = item2.Key,
                        GrandTotal = item2.Sum(item3 => item3.Nominal)
                    })
                    .ToArray();

                for (int i = 1; i <= 12; i++)
                {
                    var TempatTransaksi = DataTransaksi.FirstOrDefault(item2 => item2.Key == i);
                    var TempatForecast = DataForecast.FirstOrDefault(item2 => item2.Key == i);

                    LiteralChart.Text += "{ 'y': '" + Pengaturan.Bulan(i) + "', 'Forecast': " + Math.Round((TempatForecast != null ? TempatForecast.GrandTotal : 0)) + ", 'Actual Income': " + (TempatTransaksi != null ? TempatTransaksi.GrandTotal : 0) + " }, ";
                }

                LiteralChart.Text += "]; Morris.Bar({ element: 'graph" + item.IDTempat + "', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

                LiteralChart.Text += "ykeys: ['Forecast','Actual Income'],";
                LiteralChart.Text += "labels: ['Forecast','Actual Income'],";
                LiteralChart.Text += "barColors:['#4DA74D','#0B62A4'],";

                LiteralChart.Text += " parseTime: false, resize:true }); eval(dataChart); }); </script>";
            }
            #endregion
        }
    }
}