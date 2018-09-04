using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Transaksi_JenisPembayaran : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);
                JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListJenisTransaksi.Items.AddRange(ClassJenisTransaksi.DataDropDownList(db));
            }

            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            LoadData(false);
        }
        else
            LinkDownload.Visible = false;
    }
    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData(false);
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData(false);
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData(false);
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData(false);
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData(false);
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData(false);
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData(false);
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData(false);
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData(false);
    }
    #endregion

    protected void ButtonTabel_Click(object sender, EventArgs e)
    {
        divTabel.Visible = true;
        divChart.Visible = false;
        LoadData(false);
    }
    protected void ButtonChart_Click(object sender, EventArgs e)
    {
        divTabel.Visible = false;
        divChart.Visible = true;
        LoadData(false);
    }

    private void LoadData(bool excel)
    {
        if (divTabel.Visible == true)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                #region DEFAULT
                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

                if (ViewState["TanggalAwal"].ToString() == ViewState["TanggalAkhir"].ToString())
                    LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal();
                else
                    LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal() + " - " + ViewState["TanggalAkhir"].ToFormatTanggal();

                string Pencarian = "";
                #endregion

                var DataJenisPembayaran = db.TBTransaksiJenisPembayarans
                    .Where(item =>
                        item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                        item.Tanggal.Value.Date >= (DateTime)ViewState["TanggalAwal"] &&
                        item.Tanggal.Value.Date <= (DateTime)ViewState["TanggalAkhir"])
                    .ToArray();

                Pencarian += "?Awal=" + ViewState["TanggalAwal"];
                Pencarian += "&Akhir=" + ViewState["TanggalAkhir"];

                #region FILTER
                if (DropDownListTempat.SelectedValue != "0")
                {
                    DataJenisPembayaran = DataJenisPembayaran
                        .Where(item => item.TBTransaksi.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();

                    Pencarian += "&IDTempat=" + DropDownListTempat.SelectedValue;
                }

                if (DropDownListJenisTransaksi.SelectedValue != "0")
                {
                    DataJenisPembayaran = DataJenisPembayaran
                        .Where(item => item.TBTransaksi.IDJenisTransaksi == DropDownListJenisTransaksi.SelectedValue.ToInt()).ToArray();

                    Pencarian += "&IDJenisPembayaran=" + DropDownListJenisTransaksi.SelectedValue;
                }
                #endregion

                var ListJenisPembayaran = DataJenisPembayaran
                    .GroupBy(item => new
                    {
                        IDJenisPembayaran = item.IDJenisPembayaran,
                        Tanggal = item.Tanggal.Value.Date
                    })
                    .Select(item => new
                    {
                        Key = item.Key,
                        Total = item.Sum(item2 => item2.Total)
                    });

                //ButtonPrint.OnClientClick = "return popitup('JenisPembayaranPrint.aspx" + _tempPencarian + "')";

                JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);
                var JenisPembayaran = ClassJenisPembayaran.Data();

                #region USER INTERFACE
                LiteralHeaderTabel.Text = string.Empty;
                LiteralLaporan.Text = string.Empty;

                foreach (var item in JenisPembayaran)
                    LiteralHeaderTabel.Text += "<th class='text-right'>" + item.Nama + "</th>";

                LiteralHeaderTabel.Text += "<th class='text-right'>TOTAL</th>";

                int index = 1;

                #region SUMARY
                string JumlahTotal = string.Empty;
                JumlahTotal += "<tr>";
                JumlahTotal += "<td class='text-right' colspan='3'></td>";

                foreach (var item in JenisPembayaran)
                {
                    JumlahTotal += "<td class='text-right table-success' style='font-size: 12px;'><b>" + ListJenisPembayaran.Where(item2 => item2.Key.IDJenisPembayaran == item.IDJenisPembayaran).Sum(item2 => item2.Total).ToFormatHarga() + "</b></td>";
                }

                JumlahTotal += "<td class='text-right table-success' style='font-size: 12px;'><b>" + ListJenisPembayaran.Sum(item2 => item2.Total).ToFormatHarga() + "</b></td>";
                JumlahTotal += "</tr>";

                LiteralLaporan.Text += JumlahTotal;
                #endregion

                #region PERSENTASE
                string JumlahPersentase = string.Empty;
                decimal GrandTotal = ListJenisPembayaran.Sum(item => item.Total.Value);

                if (GrandTotal > 0)
                {
                    JumlahPersentase += "<tr>";
                    JumlahPersentase += "<td class='text-right' colspan='3'></td>";

                    decimal TotalPersentase = 0;

                    foreach (var item in JenisPembayaran)
                    {
                        decimal Persentase = ListJenisPembayaran.Where(item2 => item2.Key.IDJenisPembayaran == item.IDJenisPembayaran).Sum(item2 => item2.Total.Value) / GrandTotal * 100;
                        JumlahPersentase += "<td class='text-right table-secondary' style='font-size: 10px;'><b>" + Persentase.ToFormatHarga() + " %</b></td>";

                        TotalPersentase += Persentase;
                    }

                    JumlahPersentase += "<td class='text-right table-secondary' style='font-size: 10px;'><b>" + TotalPersentase.ToFormatHarga() + " %</b></td>";
                    JumlahPersentase += "</tr>";
                }

                LiteralLaporan.Text += JumlahPersentase;
                #endregion

                #region JENIS PEMBAYARAN
                for (DateTime i = (DateTime)ViewState["TanggalAwal"]; i <= (DateTime)ViewState["TanggalAkhir"]; i = i.AddDays(1))
                {
                    LiteralLaporan.Text += "<tr>";

                    LiteralLaporan.Text += "<td>" + index++ + "</td>";
                    LiteralLaporan.Text += "<td>" + i.ToString("dddd") + "</td>";
                    LiteralLaporan.Text += "<td>" + i.ToFormatTanggal() + "</td>";

                    decimal Total = 0;

                    foreach (var item in JenisPembayaran)
                    {
                        var Pembayaran = ListJenisPembayaran.FirstOrDefault(item2 => item2.Key.Tanggal == i.Date && item2.Key.IDJenisPembayaran == item.IDJenisPembayaran);

                        if (Pembayaran != null)
                        {
                            LiteralLaporan.Text += "<td class='text-right'>" + Pembayaran.Total.ToFormatHarga() + "</td>";
                            Total += Pembayaran.Total.Value;
                        }
                        else
                            LiteralLaporan.Text += "<td></td>";
                    }

                    LiteralLaporan.Text += "<td class='text-right table-warning'><b>" + (Total > 0 ? Total.ToFormatHarga() : "") + "</b></td>";

                    LiteralLaporan.Text += "</tr>";
                }
                #endregion

                LiteralLaporan.Text += JumlahPersentase;
                LiteralLaporan.Text += JumlahTotal;
                #endregion

                #region EXCEL
                if (excel)
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    string FileNama = "Laporan Jenis Pembayaran " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";
                    string FolderPath = Server.MapPath("~/file_excel/Jenis Pembayaran/Export/");

                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    string FilePath = FolderPath + FileNama;
                    string WorksheetNama = "Laporan Jenis Pembayaran";
                    string Judul = "Laporan Jenis Pembayaran " + Pengguna.Store + " - " + Pengguna.Tempat + " " + DateTime.Now.ToString("d MMMM yyyy");

                    FileInfo newFile = new FileInfo(FilePath);

                    using (ExcelPackage package = new ExcelPackage(newFile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(WorksheetNama);

                        worksheet.Cells[1, 1].Value = "No.";
                        worksheet.Cells[1, 2].Value = "Hari";
                        worksheet.Cells[1, 3].Value = "Tanggal";

                        int Kolom = 4;

                        foreach (var item in JenisPembayaran)
                            worksheet.Cells[1, Kolom++].Value = item.Nama;

                        worksheet.Cells[1, Kolom].Value = "TOTAL";

                        index = 2;

                        for (DateTime i = (DateTime)ViewState["TanggalAwal"]; i <= (DateTime)ViewState["TanggalAkhir"]; i = i.AddDays(1))
                        {
                            worksheet.Cells[index, 1].Value = index - 1;
                            worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                            worksheet.Cells[index, 2].Value = i.ToString("dddd");
                            worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                            worksheet.Cells[index, 3].Value = i;
                            worksheet.Cells[index, 3].Style.Numberformat.Format = "d MMMM yyyy";

                            decimal Total = 0;
                            int index2 = 4;

                            foreach (var item in JenisPembayaran)
                            {
                                var Pembayaran = ListJenisPembayaran.FirstOrDefault(item2 => item2.Key.Tanggal == i.Date && item2.Key.IDJenisPembayaran == item.IDJenisPembayaran);

                                if (Pembayaran != null)
                                {
                                    worksheet.Cells[index, index2].Value = Pembayaran.Total.Value;
                                    if (Pembayaran.Total.Value.ToFormatHarga().Contains(","))
                                        worksheet.Cells[index, index2].Style.Numberformat.Format = "#,##0.00";
                                    else
                                        worksheet.Cells[index, index2].Style.Numberformat.Format = "#,##0";

                                    Total += Pembayaran.Total.Value;
                                }
                                else
                                {
                                    worksheet.Cells[index, index2].Value = 0;
                                    if (Parse.ToFormatHarga(0).Contains(","))
                                        worksheet.Cells[index, index2].Style.Numberformat.Format = "#,##0.00";
                                    else
                                        worksheet.Cells[index, index2].Style.Numberformat.Format = "#,##0";
                                }

                                index2++;
                            }

                            worksheet.Cells[index, index2].Value = Total;
                            if (Total.ToFormatHarga().Contains(","))
                                worksheet.Cells[index, index2].Style.Numberformat.Format = "#,##0.00";
                            else
                                worksheet.Cells[index, index2].Style.Numberformat.Format = "#,##0";

                            index++;
                        }

                        using (var range = worksheet.Cells[1, 1, 1, Kolom])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                            range.Style.Font.Color.SetColor(Color.White);
                        }

                        worksheet.Cells.AutoFitColumns(0);

                        worksheet.HeaderFooter.OddHeader.CenteredText = "&16&\"Tahoma,Regular Bold\"" + Judul;
                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = "Print : " + Pengguna.NamaLengkap + " - " + Pengguna.Tempat + " - " + DateTime.Now.ToString("d MMMM yyyy hh:mm");

                        worksheet.HeaderFooter.OddFooter.RightAlignedText = "WIT. Warehouse Management System - " + string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                        package.Workbook.Properties.Title = WorksheetNama;
                        package.Workbook.Properties.Author = "WIT. Warehouse Management System";
                        package.Workbook.Properties.Comments = Judul;

                        package.Workbook.Properties.Company = "WIT. Warehouse Management System";
                        package.Save();
                    }

                    LinkDownload.Visible = true;
                    LinkDownload.HRef = "/file_excel/Jenis Pembayaran/Export/" + FileNama;
                }
                #endregion
            }
        }
        else if (divChart.Visible == true)
        {
            #region DEFAULT
            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            if (ViewState["TanggalAwal"].ToString() == ViewState["TanggalAkhir"].ToString())
                LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal();
            else
                LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal() + " - " + ViewState["TanggalAkhir"].ToFormatTanggal();
            #endregion

            //Literal LiteralChart = (Literal)Page.Master.FindControl("LiteralChart");
            LiteralChart.Text = string.Empty;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataJenisPembayaran = db.TBTransaksiJenisPembayarans
                    .Where(item =>
                        item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                        item.Tanggal.Value.Date >= (DateTime)ViewState["TanggalAwal"] &&
                        item.Tanggal.Value.Date <= (DateTime)ViewState["TanggalAkhir"])
                    .ToArray();

                #region FILTER
                if (DropDownListTempat.SelectedValue != "0")
                    DataJenisPembayaran = DataJenisPembayaran.Where(item => item.TBTransaksi.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();

                if (DropDownListJenisTransaksi.SelectedValue != "0")
                    DataJenisPembayaran = DataJenisPembayaran.Where(item => item.TBTransaksi.IDJenisTransaksi == DropDownListJenisTransaksi.SelectedValue.ToInt()).ToArray();
                #endregion

                var JenisPembayaran = DataJenisPembayaran
                    .GroupBy(item => new
                    {
                        item.IDJenisPembayaran,
                        item.TBJenisPembayaran.Nama
                    })
                    .Select(item => new
                    {
                        item.Key,
                        Total = item.Sum(item2 => item2.Total)
                    })
                    .OrderByDescending(item => item.Total)
                    .ToArray();

                int Height = JenisPembayaran.Count() * 30;
                divChart.Attributes.Add("style", "width: auto; height: " + (Height > 600 ? Height : 600) + "px; margin: 0 auto;");

                string Judul = "";
                string SubJudul = "";
                string JudulX = "Jenis Pembayaran";
                string DataX = "";

                string JudulY = "Total Jenis Pembayaran";

                string DataY = "";
                string Tooltip = "";

                foreach (var item in JenisPembayaran)
                {
                    DataX += "'" + item.Key.Nama + "',";
                    DataY += item.Total + ",";
                }

                LiteralChart.Text += "<script type=\"text/javascript\">";
                LiteralChart.Text += "$(function () { $('#divChart').highcharts({";
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
            }
        }
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
}