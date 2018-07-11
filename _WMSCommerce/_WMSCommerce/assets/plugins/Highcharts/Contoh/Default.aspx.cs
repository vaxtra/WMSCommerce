using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plugins_highcharts_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LiteralChart.Text = string.Empty;

            string Judul = "Historic World Population by Region";
            string SubJudul = "Source: Wikipedia.org";

            string JudulX = "X Title";
            string DataX = "'Africa', 'America', 'Asia', 'Europe', 'Oceania'";

            string JudulY = "Population (millions)";

            string DataY = @"
            {
                name: 'Year 1800',
                data: [107, 31, 635, 203, 2]
            },
            {
                name: 'Year 1900',
                data: [133, 156, 947, 408, 6]
            },
            {
                name: 'Year 2008',
                data: [973, 914, 4054, 732, 34]
            }";

            string Tooltip = " millions";

            LiteralChart.Text += "<script type=\"text/javascript\">";
            LiteralChart.Text += "$(function () { $('#container').highcharts({";
            LiteralChart.Text += "        chart: { type: 'bar' },";
            LiteralChart.Text += "        title: { text: '" + Judul + "' },";
            LiteralChart.Text += "        subtitle: { text: '" + SubJudul + "' },";
            LiteralChart.Text += "        xAxis: { categories: [" + DataX + "], title: { text: '" + JudulX + "' } },";
            LiteralChart.Text += "        yAxis: {  min: 0, title: { text: '" + JudulY + "', align: 'high' }, labels: { overflow: 'justify' } },";
            LiteralChart.Text += "        tooltip: { valueSuffix: '" + Tooltip + "' },";
            LiteralChart.Text += "        plotOptions: { bar: { dataLabels: { enabled: true } } },";
            LiteralChart.Text += "        credits: { enabled: false },";
            LiteralChart.Text += "        exporting: { enabled: false },";
            LiteralChart.Text += "        series: [" + DataY + "]";
            LiteralChart.Text += "    }); });";
            LiteralChart.Text += "</script>";
        }
    }
}