using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

public class Excel_Class
{
    //INPUT
    private PenggunaLogin pengguna;
    private string worksheetNama;
    private string periode;
    private int jumlahKolom;

    //TEMP
    private string judul;
    private string fileNama;

    //RESULT
    public ExcelPackage Package { get; set; }
    public ExcelWorksheet Worksheet { get; set; }
    public string LinkDownload
    {
        get { return "/file_excel/" + worksheetNama + "/Export/" + fileNama; }
    }
    public Excel_Class(PenggunaLogin _pengguna, string _worksheetNama, string _periode, int _jumlahKolom)
    {
        pengguna = _pengguna;
        worksheetNama = _worksheetNama;
        periode = _periode;
        jumlahKolom = _jumlahKolom;

        fileNama = worksheetNama + " " + periode.Replace(":", ".") + " (" + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ").xlsx";
        string Folder = HttpContext.Current.Server.MapPath("~/file_excel/" + worksheetNama + "/Export/");

        if (!Directory.Exists(Folder))
            Directory.CreateDirectory(Folder);

        string FilePath = Folder + fileNama;

        judul = worksheetNama + " " + pengguna.Store + " - " + pengguna.Tempat + " " + DateTime.Now.ToString("d MMMM yyyy");

        Package = new ExcelPackage(new FileInfo(FilePath));

        Worksheet = Package.Workbook.Worksheets.Add(worksheetNama);
    }
    public void Save()
    {
        Worksheet.Cells[1, 2, 1, jumlahKolom].AutoFilter = true;

        for (int i = 1; i <= jumlahKolom; i++)
        {
            if (Worksheet.Cells[1, i].Value != null)
            {
                Worksheet.Cells[1, i].Style.Font.Bold = true;
                Worksheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(Color.Black);
                Worksheet.Cells[1, i].Style.Font.Color.SetColor(Color.White);
            }
        }

        if (Worksheet.Dimension != null)
        {
            int IndexFooter = Worksheet.Dimension.End.Row + 2;

            #region NAMA WORKSHEET
            using (var range = Worksheet.Cells[IndexFooter, 1, IndexFooter, jumlahKolom])
            {
                range.Style.Font.Bold = true;
                range.Merge = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.Size = 14;
            }

            Content(IndexFooter, 1, worksheetNama + " " + periode);

            IndexFooter++;
            #endregion

            #region TANGGAL
            using (var range = Worksheet.Cells[IndexFooter, 1, IndexFooter, jumlahKolom])
            {
                range.Style.Font.Bold = true;
                range.Merge = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.Size = 10;
            }

            Content(IndexFooter, 1, DateTime.Now.ToFormatTanggal());

            IndexFooter++;
            #endregion

            #region PENGGUNA
            using (var range = Worksheet.Cells[IndexFooter, 1, IndexFooter, jumlahKolom])
            {
                range.Style.Font.Bold = true;
                range.Merge = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.Size = 10;
            }

            Content(IndexFooter, 1, pengguna.NamaLengkap);

            IndexFooter++;
            #endregion

            #region TEMPAT
            using (var range = Worksheet.Cells[IndexFooter, 1, IndexFooter, jumlahKolom])
            {
                range.Style.Font.Bold = true;
                range.Merge = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.Size = 10;
            }

            Content(IndexFooter, 1, pengguna.Store + " - " + pengguna.Tempat);

            IndexFooter++;
            #endregion

            #region FOOTER
            using (var range = Worksheet.Cells[IndexFooter, 1, IndexFooter + 1, jumlahKolom])
            {
                range.Style.Font.Bold = true;
                range.Merge = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            Content(IndexFooter, 1, "Warehouse Management System Powered by WIT.");
            #endregion
        }

        Worksheet.Cells.AutoFitColumns(0);

        Worksheet.HeaderFooter.OddHeader.CenteredText = "&16&\"Tahoma,Regular Bold\"" + judul;
        Worksheet.HeaderFooter.OddFooter.LeftAlignedText = "Print : " + pengguna.NamaLengkap + " - " + pengguna.Tempat + " - " + DateTime.Now.ToString("d MMMM yyyy hh:mm");

        Worksheet.HeaderFooter.OddFooter.RightAlignedText = "WIT. Warehouse Management System - " + string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

        Package.Workbook.Properties.Title = worksheetNama;
        Package.Workbook.Properties.Author = "WIT. Warehouse Management System";
        Package.Workbook.Properties.Comments = judul;

        Package.Workbook.Properties.Company = "WIT. Warehouse Management System";
        Package.Save();
    }
    public void SetColor(int baris, int kolom, Color warna)
    {
        Worksheet.Cells[baris, kolom].Style.Font.Color.SetColor(warna);
    }
    public void SetBackground(int baris, int kolom, Color warna)
    {
        Worksheet.Cells[baris, kolom].Style.Fill.PatternType = ExcelFillStyle.Solid;
        Worksheet.Cells[baris, kolom].Style.Fill.BackgroundColor.SetColor(warna);
    }
    private void SetValue(int baris, int kolom, string value)
    {
        Worksheet.Cells[baris, kolom].Value = value;
        Worksheet.Cells[baris, kolom].Style.Numberformat.Format = "@";
    }
    private void SetValue(int baris, int kolom, decimal value)
    {
        Worksheet.Cells[baris, kolom].Value = value;

        if (value.ToFormatHarga().Contains("."))
            Worksheet.Cells[baris, kolom].Style.Numberformat.Format = "#,##0.00";
        else
            Worksheet.Cells[baris, kolom].Style.Numberformat.Format = "#,##0";

        if (value < 0)
            SetColor(baris, kolom, Color.Red);
    }
    private void SetValue(int baris, int kolom, DateTime value)
    {
        if (value.Hour == 0 && value.Minute == 0 && value.Second == 0)
        {
            Worksheet.Cells[baris, kolom].Value = value;
            Worksheet.Cells[baris, kolom].Style.Numberformat.Format = "d MMMM yyyy";
        }
        else
        {
            Worksheet.Cells[baris, kolom].Value = value;
            Worksheet.Cells[baris, kolom].Style.Numberformat.Format = "d MMMM yyyy HH:mm";
        }
    }
    public void Header(int kolom, string value)
    {
        SetValue(1, kolom, value);
    }
    public void Content(int baris, int kolom, string value)
    {
        SetValue(baris, kolom, value);
    }
    public void Content(int baris, int kolom, decimal value)
    {
        SetValue(baris, kolom, value);
    }
    public void Content(int baris, int kolom, decimal? value)
    {
        SetValue(baris, kolom, value.HasValue ? value.Value : 0);
    }
    public void Content(int baris, int kolom, DateTime value)
    {
        SetValue(baris, kolom, value);
    }
    public void Content(int baris, int kolom, int value)
    {
        SetValue(baris, kolom, (decimal)value);
    }
}