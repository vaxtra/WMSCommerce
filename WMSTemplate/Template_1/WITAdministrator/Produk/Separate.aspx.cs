using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OfficeOpenXml;
using ExcelLibrary.SpreadSheet;
using OfficeOpenXml.Style;
using System.Drawing;


public partial class WITAdministrator_Produk_Separate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {

        string Folder = Server.MapPath("/file_excel/Produk/Import/");
        string LokasiFile = Folder + " Import Produk " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";

        FileUploadTransfer.SaveAs(LokasiFile);

        Workbook Book = Workbook.Load(LokasiFile);
        Worksheet Sheet = Book.Worksheets[0];

        LabelRows.Text = (Sheet.Cells.LastRowIndex % 200).ToString();

        List<int> separator = new List<int>();


        for (int rowIndex = 1; rowIndex < Sheet.Cells.LastRowIndex; rowIndex++)
        {
            if (rowIndex % 200 == 0)
            {
                separator.Add(rowIndex);
            }
        }

        separator.Add(Sheet.Cells.LastRowIndex);

        var test = separator;

        List<int> start = new List<int>();

        foreach (var item in separator)
        {
            start.Add(item + 1);
        }

        var test2 = start;

        int idxawal = 1;



        foreach (var item in separator)
        {
            int idxakhir = item;


            //if (item == separator[separator.Count - 1])
            //{
            //    if (Sheet.Cells.LastRowIndex - item < 200)
            //    {
            //        idxawal = item + 1;
            //        idxakhir = item + (Sheet.Cells.LastRowIndex - separator[separator.Count - 1]);
            //    }
            //    else
            //    {
            //        idxakhir = item;
            //    }
            //}
            //else
            //{
            //    idxakhir = item;
            //}



            List<StokProduk_Model> testing = new List<StokProduk_Model>();

            for (int i = idxawal; i <= idxakhir; i++)
            {


                //baca excel
                Row row = Sheet.Cells.GetRow(i);

                Cell _nomor = row.GetCell(0);
                Cell _kode = row.GetCell(1);
                Cell _brand = row.GetCell(2);
                Cell _produk = row.GetCell(3);
                Cell _varian = row.GetCell(4);
                Cell _warna = row.GetCell(5);
                Cell _berat = row.GetCell(6);
                Cell _quantity = row.GetCell(7);
                Cell _hargaBeli = row.GetCell(8);
                Cell _hargaJual = row.GetCell(9);
                Cell _kategori = row.GetCell(10);
                Cell _keterangan = row.GetCell(11);

                testing.Add(new StokProduk_Model
                {
                    Kode = _kode.StringValue,
                    PemilikProduk = _brand.StringValue,
                    Produk = _produk.StringValue,
                    Atribut = _varian.StringValue,
                    Warna = _warna.StringValue,
                    Berat = decimal.Parse(_berat.StringValue),
                    Jumlah = int.Parse(_quantity.StringValue),
                    HargaBeli = decimal.Parse(_hargaBeli.StringValue),
                    HargaJual = decimal.Parse(_hargaJual.StringValue),
                    Kategori = _kategori.StringValue,
                    Keterangan = _keterangan.StringValue

                });
            }


            //buat excel baru
            string NamaWorksheet = "Produk";

            #region Default
            string NamaFile = "SEPARATE" + idxawal + NamaWorksheet + " " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";
            string Folderx = Server.MapPath("~/file_excel/" + NamaWorksheet + "/Separate/");

            if (!Directory.Exists(Folderx))
                Directory.CreateDirectory(Folderx);

            string Pathx = Folderx + NamaFile;
            string Judul = "Laporan " + DateTime.Now.ToString("d MMMM yyyy");

            FileInfo newFile = new FileInfo(Pathx);

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(NamaWorksheet);

                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 2].Value = "Kode";
                worksheet.Cells[1, 3].Value = "Brand";
                worksheet.Cells[1, 4].Value = "Produk";
                worksheet.Cells[1, 5].Value = "Varian";
                worksheet.Cells[1, 6].Value = "Warna";
                worksheet.Cells[1, 7].Value = "Berat";
                worksheet.Cells[1, 8].Value = "Jumlah";
                worksheet.Cells[1, 9].Value = "Harga Beli";
                worksheet.Cells[1, 10].Value = "Harga Jual";
                worksheet.Cells[1, 11].Value = "Kategori";
                worksheet.Cells[1, 12].Value = "Keterangan";

                int index = 2;

                foreach (var itemx in testing)
                {
                    //No
                    worksheet.Cells[index, 1].Value = index - 1;
                    worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                    //Kode
                    worksheet.Cells[index, 2].Value = itemx.Kode;
                    worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                    //Brand
                    worksheet.Cells[index, 3].Value = itemx.PemilikProduk;
                    worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                    //Produk
                    worksheet.Cells[index, 4].Value = itemx.Produk;
                    worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                    //Varian
                    worksheet.Cells[index, 5].Value = itemx.Atribut;
                    worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                    //Warna
                    worksheet.Cells[index, 6].Value = itemx.Warna;
                    worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                    //Berat
                    worksheet.Cells[index, 7].Value = itemx.Berat;

                    if (itemx.Berat.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 7].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 7].Style.Numberformat.Format = "#,##0";

                    //Jumlah
                    worksheet.Cells[index, 8].Value = itemx.Jumlah;

                    worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0";

                    //Harga Beli
                    worksheet.Cells[index, 9].Value = itemx.HargaBeli;

                    if (itemx.HargaBeli.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";

                    //Harga Jual
                    worksheet.Cells[index, 10].Value = itemx.HargaJual;

                    if (itemx.HargaJual.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0";

                    //Kategori

                    worksheet.Cells[index, 11].Value = itemx.Kategori;
                    worksheet.Cells[index, 11].Style.Numberformat.Format = "@";

                    //Keterangan
                    worksheet.Cells[index, 12].Value = itemx.Keterangan;
                    worksheet.Cells[index, 12].Style.Numberformat.Format = "@";

                    index++;
                }
                using (var range = worksheet.Cells[1, 1, 1, 12])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                worksheet.Cells["B1:L1"].AutoFilter = true;

                #region Default
                worksheet.Cells.AutoFitColumns(0);

                worksheet.HeaderFooter.OddHeader.CenteredText = "&16&\"Tahoma,Regular Bold\"" + Judul;
                worksheet.HeaderFooter.OddFooter.LeftAlignedText = "Print : " + DateTime.Now.ToString("d MMMM yyyy hh:mm");

                worksheet.HeaderFooter.OddFooter.RightAlignedText = "WIT. Warehouse Management System - " + string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                package.Workbook.Properties.Title = NamaWorksheet;
                package.Workbook.Properties.Author = "WIT. Warehouse Management System";
                package.Workbook.Properties.Comments = Judul;

                package.Workbook.Properties.Company = "WIT. Warehouse Management System";
                package.Save();

                #endregion
            }
            #endregion

            idxawal = item + 1;
        }
    }
}