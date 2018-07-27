using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;

public partial class WITReport_Transaksi_IndexHutang : System.Web.UI.Page
{
    public string SearchString { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchString = TextBoxCari.Text;
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LinkDownloadVendor.Visible = false;
        SearchString = TextBoxCari.Text;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class config = new StoreKonfigurasi_Class();
            var jatuhTempo = config.Cari(db, EnumStoreKonfigurasi.HariJatuhTempo);

            var totalSales = from t in db.TBTransaksis
                             select new
                             {
                                 t.GrandTotal,
                                 t.IDPelanggan,
                             };

            var totalBayar = from t in db.TBTransaksiJenisPembayarans
                             group t by new { t.Bayar, t.IDTransaksi } into g
                             select new
                             {
                                 IDTransaksi = g.Key.IDTransaksi,
                                 Total = g.Sum(i => i.Bayar)
                             };

            var totalHutang = from t in db.TBTransaksis
                              where t.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment
                              select new
                              {
                                  t.IDPelanggan,
                                  t.IDTransaksi,
                                  t.TanggalTransaksi,
                                  Sisa = totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Count() == 0 ? t.GrandTotal : t.GrandTotal - totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Sum(i => i.Total),
                                  HutangUnreal = totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Count() == 0 ? t.GrandTotal * (t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)) < DateTime.Now ? (DateTime.Now - t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan))).Days : 0) : (t.GrandTotal - totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Sum(i => i.Total)) * (t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)) < DateTime.Now ? (DateTime.Now - t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan))).Days : 0),
                                  //TanggalJatuhTempo = t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)),
                                  //Hari = t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)) < DateTime.Now ? (DateTime.Now - t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan))).Days : 0,
                              };


            if (string.IsNullOrEmpty(SearchString))
            {
                var dataPelanggan = from item in db.TBPelanggans
                                    select new
                                    {
                                        Nama = item.NamaLengkap,
                                        DataSales = totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal) == null ? 0 : totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal),
                                        DataHutang = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa),
                                        HutangUnreal = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal),
                                        Index = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) / totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal)
                                    };

                RepeaterIndexHutang.DataSource = dataPelanggan;
                RepeaterIndexHutang.DataBind();
            }
            else
            {
                var dataPelanggan = from item in db.TBPelanggans
                                    where item.NamaLengkap.ToLower().Contains(SearchString)
                                    select new
                                    {
                                        Nama = item.NamaLengkap,
                                        DataSales = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal),
                                        DataHutang = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa),
                                        HutangUnreal = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal),
                                        Index = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) / totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal)
                                    };

                RepeaterIndexHutang.DataSource = dataPelanggan;
                RepeaterIndexHutang.DataBind();
            }
        }


    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            string NamaWorksheet = "Index Hutang";

            #region Default
            string NamaFile = "Laporan " + NamaWorksheet + " " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";
            string Folder = Server.MapPath("~/file_excel/" + NamaWorksheet + "/Export/");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            string Path = Folder + NamaFile;
            string Judul = "Laporan " + NamaWorksheet + " " + DateTime.Now.ToString("d MMMM yyyy");


            int indexRow = 2;
            FileInfo newFile = new FileInfo(Path);
            #endregion

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(NamaWorksheet);

                StoreKonfigurasi_Class config = new StoreKonfigurasi_Class();
                var jatuhTempo = config.Cari(db, EnumStoreKonfigurasi.HariJatuhTempo);

                var totalSales = from t in db.TBTransaksis
                                 select new
                                 {
                                     t.GrandTotal,
                                     t.IDPelanggan,
                                 };

                var totalBayar = from t in db.TBTransaksiJenisPembayarans
                                 group t by new { t.Bayar, t.IDTransaksi } into g
                                 select new
                                 {
                                     IDTransaksi = g.Key.IDTransaksi,
                                     Total = g.Sum(i => i.Bayar)
                                 };

                var totalHutang = from t in db.TBTransaksis
                                  where t.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment
                                  select new
                                  {
                                      t.IDPelanggan,
                                      t.IDTransaksi,
                                      t.TanggalTransaksi,
                                      Sisa = totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Count() == 0 ? t.GrandTotal : t.GrandTotal - totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Sum(i => i.Total),
                                      HutangUnreal = totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Count() == 0 ? t.GrandTotal * (t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)) < DateTime.Now ? (DateTime.Now - t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan))).Days : 0) : (t.GrandTotal - totalBayar.Where(s => s.IDTransaksi == t.IDTransaksi).Sum(i => i.Total)) * (t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)) < DateTime.Now ? (DateTime.Now - t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan))).Days : 0),
                                      //TanggalJatuhTempo = t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)),
                                      //Hari = t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan)) < DateTime.Now ? (DateTime.Now - t.TanggalTransaksi.Value.AddDays(double.Parse(jatuhTempo.Pengaturan))).Days : 0,
                                  };

                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 2].Value = "Pelanggan";
                worksheet.Cells[1, 3].Value = "Total Sales (Rp.)";
                worksheet.Cells[1, 4].Value = "Total Hutang (Rp.)";
                worksheet.Cells[1, 5].Value = "Hutang Tidak Ril (Rp.)";
                worksheet.Cells[1, 6].Value = "Index Hutang";

                if (string.IsNullOrEmpty(SearchString))
                {
                    var dataPelanggan = from item in db.TBPelanggans
                                        select new
                                        {
                                            Nama = item.NamaLengkap,
                                            DataSales = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal),
                                            DataHutang = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa),
                                            HutangUnreal = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal),
                                            Index = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) / totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal)
                                        };


                    foreach (var itemRow in dataPelanggan)
                    {
                        //No
                        worksheet.Cells[indexRow, 1].Value = indexRow - 1;
                        worksheet.Cells[indexRow, 1].Style.Numberformat.Format = "@";

                        //Pelanggan
                        worksheet.Cells[indexRow, 2].Value = itemRow.Nama;
                        worksheet.Cells[indexRow, 2].Style.Numberformat.Format = "@";

                        //Total Sales
                        worksheet.Cells[indexRow, 3].Value = itemRow.DataSales;
                        worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "@";

                        //Total Hutang
                        worksheet.Cells[indexRow, 4].Value = itemRow.DataHutang;
                        worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "@";

                        //Total Hari
                        worksheet.Cells[indexRow, 5].Value = itemRow.HutangUnreal;
                        worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "@";

                        //Index
                        worksheet.Cells[indexRow, 6].Value = itemRow.Index == null ? 0 : itemRow.Index;
                        worksheet.Cells[indexRow, 6].Style.Numberformat.Format = "@";

                        if (itemRow.DataSales.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "#,##0";

                        if (itemRow.DataHutang.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "#,##0";

                        if (itemRow.HutangUnreal.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0";

                        indexRow++;
                    }
                }
                else
                {
                    var dataPelanggan = from item in db.TBPelanggans
                                        where item.NamaLengkap.ToLower().Contains(SearchString)
                                        select new
                                        {
                                            Nama = item.NamaLengkap,
                                            DataSales = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal),
                                            DataHutang = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.Sisa),
                                            HutangUnreal = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) == null ? 0 : totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal),
                                            Index = totalHutang.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.HutangUnreal) / totalSales.Where(t => t.IDPelanggan == item.IDPelanggan).Sum(g => g.GrandTotal)
                                        };

                    foreach (var itemRow in dataPelanggan)
                    {
                        //No
                        worksheet.Cells[indexRow, 1].Value = indexRow - 1;
                        worksheet.Cells[indexRow, 1].Style.Numberformat.Format = "@";

                        //Pelanggan
                        worksheet.Cells[indexRow, 2].Value = itemRow.Nama;
                        worksheet.Cells[indexRow, 2].Style.Numberformat.Format = "@";

                        //Total Sales
                        worksheet.Cells[indexRow, 3].Value = itemRow.DataSales;
                        worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "@";

                        //Total Hutang
                        worksheet.Cells[indexRow, 4].Value = itemRow.DataHutang;
                        worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "@";

                        //Total Hari
                        worksheet.Cells[indexRow, 5].Value = itemRow.HutangUnreal;
                        worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "@";

                        //Index
                        worksheet.Cells[indexRow, 6].Value = itemRow.Index == null ? 0 : itemRow.Index;
                        worksheet.Cells[indexRow, 6].Style.Numberformat.Format = "@";

                        if (itemRow.DataSales.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "#,##0";

                        if (itemRow.DataHutang.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "#,##0";

                        if (itemRow.HutangUnreal.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0";

                        indexRow++;
                    }
                }

                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                }

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

            //ButtonExcel.Visible = false;
            LinkDownloadVendor.Visible = true;
            LinkDownloadVendor.HRef = "/file_excel/" + NamaWorksheet + "/Export/" + NamaFile;
        }
    }
}