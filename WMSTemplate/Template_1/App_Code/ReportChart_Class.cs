using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Chart_ClassReport
/// </summary>
public class ReportChart_Class
{
    public ReportChart_Class()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetChartTrendAnalysis(string IDChart, string Judul, string LabelX, string LabelY, string[] Labels, ReportChartLine_Class Datasets)
    {
        string Line = string.Empty;
        bool awal1 = true;

        Line += "<script>";
        Line += "var line = document.getElementById(\"" + IDChart + "\");";
        Line += "var lineChart = new Chart(line, {";
        Line += "type: 'line',";
        Line += "data: {";
        Line += "labels: [";
        foreach (var item in Labels)
        {
            if (awal1)
            {
                Line += "\"" + item + "\"";
                awal1 = false;
            }
            else
                Line += ", \"" + item + "\"";
        }
        Line += "],";
        Line += "datasets: [";
        Line += "{";
        Line += "data: [";
        awal1 = true;
        foreach (var itemValueY in Datasets.Data)
        {
            if (awal1)
            {
                Line += itemValueY.ToString();
                awal1 = false;
            }
            else
                Line += ", " + itemValueY.ToString();
        }
        Line += "],";
        Line += "label: '" + Datasets.Label + "',";
        Line += "lineTension: 0.25,";
        Line += "backgroundColor: 'rgba(220,220,220, 0.25)',";
        Line += "borderColor: '" + Datasets.Color + "',";
        Line += "pointBackgroundColor: '" + Datasets.Color + "',";
        Line += "pointBorderColor: '#FFFFFF',";
        Line += "}";
        Line += "]";
        Line += "},";
        Line += "options: {";
        Line += "responsive: true,";
        if (!string.IsNullOrEmpty(Judul))
        {
            Line += "title: {";
            Line += "display: true,";
            Line += "text: '" + Judul + "'";
            Line += "},";
        }
        Line += "tooltips: {";
        Line += "mode: 'index',";
        Line += "intersect: false,";
        Line += "},";
        Line += "hover: {";
        Line += "mode: 'nearest',";
        Line += "intersect: true";
        Line += "},";
        Line += "scales: {";
        Line += "xAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelX + "'";
        Line += "}";
        Line += "}],";
        Line += "yAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelY + "'";
        Line += "}";
        Line += "}]";
        Line += "}";
        Line += "}";
        Line += "});";
        Line += "</script>";

        return Line;
    }

    public string GetChartLine(string IDChart, string Judul, string LabelX, string LabelY, string[] Labels, List<ReportChartLine_Class> Datasets)
    {
        string Line = string.Empty;
        bool awal1 = true;
        bool awal2 = true;

        Line += "<script>";
        Line += "var line = document.getElementById(\"" + IDChart + "\");";
        Line += "var lineChart = new Chart(line, {";
        Line += "type: 'line',";
        Line += "data: {";
        Line += "labels: [";
        foreach (var item in Labels)
        {
            if (awal1)
            {
                Line += "\"" + item + "\"";
                awal1 = false;
            }
            else
                Line += ", \"" + item + "\"";
        }
        Line += "],";
        Line += "datasets: [";
        awal1 = true;
        foreach (var itemReportLine in Datasets)
        {
            if (awal1)
            {
                Line += "{";
                Line += "data: [";
                awal2 = true;
                foreach (var itemValueY in itemReportLine.Data)
                {
                    if (awal2)
                    {
                        Line += itemValueY.ToString();
                        awal2 = false;
                    }
                    else
                        Line += ", " + itemValueY.ToString();
                }
                Line += "],";
                Line += "label: '" + itemReportLine.Label + "',";
                Line += "lineTension: 0,";
                Line += "backgroundColor: 'transparent',";
                Line += "borderColor: '" + itemReportLine.Color + "',";
                Line += "pointBackgroundColor: '" + itemReportLine.Color + "'";
                Line += "}";
                awal1 = false;
            }
            else
            {
                Line += ", {";
                Line += "data: [";
                awal2 = true;
                foreach (var item2 in itemReportLine.Data)
                {
                    if (awal2)
                    {
                        Line += item2.ToString();
                        awal2 = false;
                    }
                    else
                        Line += ", " + item2.ToString();
                }
                Line += "],";
                Line += "label: '" + itemReportLine.Label + "',";
                Line += "lineTension: 0,";
                Line += "backgroundColor: 'transparent',";
                Line += "borderColor: '" + itemReportLine.Color + "',";
                Line += "pointBackgroundColor: '" + itemReportLine.Color + "'";
                Line += "}";
            }
        }
        Line += "]";
        Line += "},";
        Line += "options: {";
        Line += "responsive: true,";
        Line += "title: {";
        Line += "display: true,";
        Line += "text: '" + Judul + "'";
        Line += "},";
        Line += "tooltips: {";
        Line += "mode: 'index',";
        Line += "intersect: false,";
        Line += "},";
        Line += "hover: {";
        Line += "mode: 'nearest',";
        Line += "intersect: true";
        Line += "},";
        Line += "scales: {";
        Line += "xAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelX + "'";
        Line += "}";
        Line += "}],";
        Line += "yAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelY + "'";
        Line += "}";
        Line += "}]";
        Line += "}";
        Line += "}";
        Line += "});";
        Line += "</script>";

        return Line;
    }

    public string GetChartPie(string IDChart, string Judul, string[] LabelsPie, ReportChartPie_Class Datasets)
    {
        string Pie = string.Empty;
        bool awal = true;

        Pie += "<script>";
        Pie += "var pie = document.getElementById(\"" + IDChart + "\");";
        Pie += "var pieChart = new Chart(pie, {";
        Pie += "type: 'pie',";
        Pie += "data: {";
        Pie += "datasets: [";

        Pie += "{ ";
        Pie += "data: [";
        awal = true;
        foreach (var itemValue in Datasets.Data)
        {
            if (awal)
            {
                Pie += itemValue;
                awal = false;
            }
            else
                Pie += ", " + itemValue;
        }
        Pie += "],";
        Pie += "backgroundColor: [";
        awal = true;
        foreach (var itemColor in Datasets.Color)
        {
            if (awal)
            {
                Pie += "'" + itemColor + "'";
                awal = false;
            }
            else
                Pie += ", '" + itemColor + "'";
        }
        Pie += "],";
        Pie += "label: '" + Datasets.Label + "'";
        Pie += "}";
        Pie += "],";
        Pie += "labels: [";
        awal = true;
        foreach (var item in LabelsPie)
        {
            if (awal)
            {
                Pie += "'" + item + "'";
                awal = false;
            }
            else
                Pie += ", '" + item + "'";
        }
        Pie += "]";
        Pie += "},";
        Pie += "options: {";
        Pie += "responsive: true,";
        Pie += "title: {";
        Pie += "display: true,";
        Pie += "text: '" + Judul + "'";
        Pie += "}";
        Pie += "}";
        Pie += "});";
        Pie += "</script>";

        return Pie;
    }

    public string GetChartBarVertical(string IDChart, string Judul, string LabelX, string LabelY, string[] Labels, List<ReportChartBar_Class> Datasets)
    {
        string Bar = string.Empty;
        bool awal1 = true;
        bool awal2 = true;

        Bar += "<script>";
        Bar += "var line = document.getElementById(\"" + IDChart + "\");";
        Bar += "var lineChart = new Chart(line, {";
        Bar += "type: 'bar',";
        Bar += "data: {";
        Bar += "labels: [";
        foreach (var item in Labels)
        {
            if (awal1)
            {
                Bar += "\"" + item + "\"";
                awal1 = false;
            }
            else
                Bar += ", \"" + item + "\"";
        }
        Bar += "],";
        Bar += "datasets: [";
        awal1 = true;
        foreach (var itemReportLine in Datasets)
        {
            if (awal1)
            {
                Bar += "{";
                Bar += "data: [";
                awal2 = true;
                foreach (var itemValueY in itemReportLine.Data)
                {
                    if (awal2)
                    {
                        Bar += itemValueY.ToString();
                        awal2 = false;
                    }
                    else
                        Bar += ", " + itemValueY.ToString();
                }
                Bar += "],";
                Bar += "label: '" + itemReportLine.Label + "',";
                Bar += "backgroundColor: '" + itemReportLine.Color + "',";
                Bar += "borderColor: '" + itemReportLine.Color + "',";
                Bar += "}";
                awal1 = false;
            }
            else
            {
                Bar += ", {";
                Bar += "data: [";
                awal2 = true;
                foreach (var item2 in itemReportLine.Data)
                {
                    if (awal2)
                    {
                        Bar += item2.ToString();
                        awal2 = false;
                    }
                    else
                        Bar += ", " + item2.ToString();
                }
                Bar += "],";
                Bar += "label: '" + itemReportLine.Label + "',";
                Bar += "backgroundColor: '" + itemReportLine.Color + "',";
                Bar += "borderColor: '" + itemReportLine.Color + "',";
                Bar += "}";
            }
        }
        Bar += "]";
        Bar += "},";
        Bar += "options: {";
        Bar += "responsive: true,";
        Bar += "title: {";
        Bar += "display: true,";
        Bar += "text: '" + Judul + "'";
        Bar += "},";
        Bar += "tooltips: {";
        Bar += "mode: 'index',";
        Bar += "intersect: false,";
        Bar += "},";
        Bar += "hover: {";
        Bar += "mode: 'nearest',";
        Bar += "intersect: true";
        Bar += "},";
        Bar += "scales: {";
        Bar += "xAxes: [{";
        Bar += "display: true,";
        Bar += "scaleLabel: {";
        Bar += "display: true,";
        Bar += "labelString: '" + LabelX + "'";
        Bar += "}";
        Bar += "}],";
        Bar += "yAxes: [{";
        Bar += "display: true,";
        Bar += "scaleLabel: {";
        Bar += "display: true,";
        Bar += "labelString: '" + LabelY + "'";
        Bar += "}";
        Bar += "}]";
        Bar += "}";
        Bar += "}";
        Bar += "});";
        Bar += "</script>";

        return Bar;
    }

    public string GetChartBarVertical(string IDChart, string Judul, string LabelX, string LabelY, string[] Labels, ReportChartBar_Class Datasets)
    {
        string Bar = string.Empty;
        bool awal1 = true;

        Bar += "<script>";
        Bar += "var line = document.getElementById(\"" + IDChart + "\");";
        Bar += "var lineChart = new Chart(line, {";
        Bar += "type: 'bar',";
        Bar += "data: {";
        Bar += "labels: [";
        foreach (var item in Labels)
        {
            if (awal1)
            {
                Bar += "\"" + item + "\"";
                awal1 = false;
            }
            else
                Bar += ", \"" + item + "\"";
        }
        Bar += "],";
        Bar += "datasets: [";
        Bar += "{";
        Bar += "data: [";
        awal1 = true;
        foreach (var itemValueY in Datasets.Data)
        {
            if (awal1)
            {
                Bar += itemValueY.ToString();
                awal1 = false;
            }
            else
                Bar += ", " + itemValueY.ToString();
        }
        Bar += "],";
        Bar += "label: '" + Datasets.Label + "',";
        Bar += "backgroundColor: '" + Datasets.Color + "',";
        Bar += "borderColor: '" + Datasets.Color + "',";
        Bar += "}";
        Bar += "]";
        Bar += "},";
        Bar += "options: {";
        Bar += "responsive: true,";
        Bar += "title: {";
        Bar += "display: true,";
        Bar += "text: '" + Judul + "'";
        Bar += "},";
        Bar += "tooltips: {";
        Bar += "mode: 'index',";
        Bar += "intersect: false,";
        Bar += "},";
        Bar += "hover: {";
        Bar += "mode: 'nearest',";
        Bar += "intersect: true";
        Bar += "},";
        Bar += "scales: {";
        Bar += "xAxes: [{";
        Bar += "display: true,";
        Bar += "scaleLabel: {";
        Bar += "display: true,";
        Bar += "labelString: '" + LabelX + "'";
        Bar += "}";
        Bar += "}],";
        Bar += "yAxes: [{";
        Bar += "display: true,";
        Bar += "scaleLabel: {";
        Bar += "display: true,";
        Bar += "labelString: '" + LabelY + "'";
        Bar += "}";
        Bar += "}]";
        Bar += "}";
        Bar += "}";
        Bar += "});";
        Bar += "</script>";

        return Bar;
    }

    public string GetChartBarHorizontal(string IDChart, string Judul, string LabelX, string LabelY, string[] Labels, List<ReportChartBar_Class> Datasets)
    {
        string Line = string.Empty;
        bool awal1 = true;
        bool awal2 = true;

        Line += "<script>";
        Line += "var line = document.getElementById(\"" + IDChart + "\");";
        Line += "var lineChart = new Chart(line, {";
        Line += "type: 'horizontalBar',";
        Line += "data: {";
        Line += "labels: [";
        foreach (var item in Labels)
        {
            if (awal1)
            {
                Line += "\"" + item + "\"";
                awal1 = false;
            }
            else
                Line += ", \"" + item + "\"";
        }
        Line += "],";
        Line += "datasets: [";
        awal1 = true;
        foreach (var itemReportLine in Datasets)
        {
            if (awal1)
            {
                Line += "{";
                Line += "data: [";
                awal2 = true;
                foreach (var itemValueY in itemReportLine.Data)
                {
                    if (awal2)
                    {
                        Line += itemValueY.ToString();
                        awal2 = false;
                    }
                    else
                        Line += ", " + itemValueY.ToString();
                }
                Line += "],";
                Line += "label: '" + itemReportLine.Label + "',";
                Line += "backgroundColor: '" + itemReportLine.Color + "',";
                Line += "borderColor: '" + itemReportLine.Color + "',";
                Line += "}";
                awal1 = false;
            }
            else
            {
                Line += ", {";
                Line += "data: [";
                awal2 = true;
                foreach (var item2 in itemReportLine.Data)
                {
                    if (awal2)
                    {
                        Line += item2.ToString();
                        awal2 = false;
                    }
                    else
                        Line += ", " + item2.ToString();
                }
                Line += "],";
                Line += "label: '" + itemReportLine.Label + "',";
                Line += "backgroundColor: '" + itemReportLine.Color + "',";
                Line += "borderColor: '" + itemReportLine.Color + "',";
                Line += "}";
            }
        }
        Line += "]";
        Line += "},";
        Line += "options: {";
        Line += "responsive: true,";
        Line += "title: {";
        Line += "display: true,";
        Line += "text: '" + Judul + "'";
        Line += "},";
        Line += "tooltips: {";
        Line += "mode: 'index',";
        Line += "intersect: false,";
        Line += "},";
        Line += "hover: {";
        Line += "mode: 'nearest',";
        Line += "intersect: true";
        Line += "},";
        Line += "scales: {";
        Line += "xAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelX + "'";
        Line += "}";
        Line += "}],";
        Line += "yAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelY + "'";
        Line += "}";
        Line += "}]";
        Line += "}";
        Line += "}";
        Line += "});";
        Line += "</script>";

        return Line;
    }

    public string GetChartBarHorizontal(string IDChart, string Judul, string LabelX, string LabelY, string[] Labels, ReportChartBar_Class Datasets)
    {
        string Line = string.Empty;
        bool awal1 = true;

        Line += "<script>";
        Line += "var line = document.getElementById(\"" + IDChart + "\");";
        Line += "var lineChart = new Chart(line, {";
        Line += "type: 'horizontalBar',";
        Line += "data: {";
        Line += "labels: [";
        foreach (var item in Labels)
        {
            if (awal1)
            {
                Line += "\"" + item + "\"";
                awal1 = false;
            }
            else
                Line += ", \"" + item + "\"";
        }
        Line += "],";
        Line += "datasets: [";
        Line += "{";
        Line += "data: [";
        awal1 = true;
        foreach (var itemValueY in Datasets.Data)
        {
            if (awal1)
            {
                Line += itemValueY.ToString();
                awal1 = false;
            }
            else
                Line += ", " + itemValueY.ToString();
        }
        Line += "],";
        Line += "label: '" + Datasets.Label + "',";
        Line += "backgroundColor: '" + Datasets.Color + "',";
        Line += "borderColor: '" + Datasets.Color + "',";
        Line += "}";
        Line += "]";
        Line += "},";
        Line += "options: {";
        Line += "responsive: true,";
        Line += "title: {";
        Line += "display: true,";
        Line += "text: '" + Judul + "'";
        Line += "},";
        Line += "tooltips: {";
        Line += "mode: 'index',";
        Line += "intersect: false,";
        Line += "},";
        Line += "hover: {";
        Line += "mode: 'nearest',";
        Line += "intersect: true";
        Line += "},";
        Line += "scales: {";
        Line += "xAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelX + "'";
        Line += "}";
        Line += "}],";
        Line += "yAxes: [{";
        Line += "display: true,";
        Line += "scaleLabel: {";
        Line += "display: true,";
        Line += "labelString: '" + LabelY + "'";
        Line += "}";
        Line += "}]";
        Line += "}";
        Line += "}";
        Line += "});";
        Line += "</script>";

        return Line;
    }
}

public class ReportChartLine_Class
{
    public ReportChartLine_Class()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Label { get; set; }
    public string Color { get; set; }
    public List<string> Data { get; set; }
}

public class ReportChartPie_Class
{
    public ReportChartPie_Class()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Label { get; set; }
    public List<string> Color { get; set; }
    public List<int> Data { get; set; }
}

public class ReportChartBar_Class
{
    public ReportChartBar_Class()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Label { get; set; }
    public string Color { get; set; }
    public List<int> Data { get; set; }
}