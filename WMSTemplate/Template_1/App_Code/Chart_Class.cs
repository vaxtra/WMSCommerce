using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Chart_Class
{
    private string judul;
    private string subJudul;

    public string JudulX { get; set; }
    public string DataX { get; set; }
    public string JudulY { get; set; }
    public string DataY { get; set; }
    public string Tooltip { get; set; }

    private string javascript;
    public string Javascript
    {
        get { return javascript; }
    }

    public Chart_Class()
    {

    }
    public Chart_Class(string _judul)
    {
        judul = _judul;
        subJudul = "";
    }
    public Chart_Class(string _judul, string _subJudul)
    {
        judul = _judul;
        subJudul = _subJudul;
    }
    public void ChartHorizontal()
    {
        javascript += "<script type=\"text/javascript\">";
        javascript += "$(function () { $('#container').highcharts({";
        javascript += "        chart: { type: 'bar' },";
        javascript += "        title: { text: '" + judul + "' },";
        javascript += "        subtitle: { text: '" + subJudul + "' },";
        javascript += "        xAxis: { categories: [" + DataX + "] },";
        javascript += "        yAxis: { min: 0, title: { text: '" + JudulY + "' } },";
        javascript += "        tooltip: { valueSuffix: '" + Tooltip + "' },";
        javascript += "        legend: { reversed: true },";
        javascript += "        plotOptions: { series: { stacking: 'normal', turboThreshold: 0 }, bar: { dataLabels: { enabled: true } } },";
        javascript += "        credits: { enabled: false },";
        javascript += "        exporting: { enabled: false },";
        javascript += "        series: [";
        javascript += "		   {";
        javascript += "            name: '" + JudulX + "',";
        javascript += "            data: [" + DataY + "]";
        javascript += "        },";
        javascript += "		           ]";
        javascript += "    }); });";
        javascript += "</script>";
    }
}
